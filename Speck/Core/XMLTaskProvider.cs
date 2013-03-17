///////////////////////////////////////////////////////////
//  XMLTaskProvider.cs
//  Created on:      22-十二月-2011 16:30:40
//  Original author: zhanghao
//
//  Significant Points: given the defalult xml namespace the prefix 'task' to URI 'http://zh/task'
///////////////////////////////////////////////////////////









using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using log4net;

namespace ZteApp.ProductService.Core
{
    public class XMLTaskProvider:ITaskProvider,IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool mDisposed;
        private XmlReader mReader;
        private XPathDocument mDocument;
        XPathNavigator mRootNavigator;
        private Dictionary<string, BehaviorConfigEntry> mBehaviorConfigEntrys;
        private readonly Char[] mContractSplitSymbol;
        IUnityContainer mUnityContainer;
        private XmlNamespaceManager mXmlNamespaceManager;
#region .ctr
        public XMLTaskProvider(string uri)
        {
            mReader = XmlReader.Create(uri);
            mContractSplitSymbol = new char[] { ';' };
            Initialize();
        }
#endregion

#region .dctr
        ~XMLTaskProvider()
        {
            Dispose(false);
        }
#endregion

        protected virtual void Initialize()
        {
            mDisposed = false;
            mDocument = new XPathDocument(mReader);
            mRootNavigator = mDocument.CreateNavigator();
            
            //binding the default xmlnamespace a prefix by xpath parsing due to microsoft mechanism
            mXmlNamespaceManager = new XmlNamespaceManager(mRootNavigator.NameTable);
            mXmlNamespaceManager.AddNamespace("task","http://zh/task");

            //create unity container 
            mUnityContainer = new UnityContainer();
            mUnityContainer.LoadConfiguration();
            
            /*------------------construct behavior config group--------------------*/
            mBehaviorConfigEntrys = new Dictionary<string, BehaviorConfigEntry>();

            foreach (XPathNavigator behaviorConfigNavigator in mRootNavigator.Select("/task:TaskManifest/task:Behaviors/task:Behavior", mXmlNamespaceManager))
            {
                BehaviorConfigEntry entry = new BehaviorConfigEntry();
                entry.Name = behaviorConfigNavigator.GetAttribute("Name",string.Empty).Trim();
                entry.ImplementedConstracts = behaviorConfigNavigator.GetAttribute("ImplementedContracts", string.Empty).Trim();
                entry.CallBackTimeOut = behaviorConfigNavigator.SelectSingleNode("task:CallbackTimeOut/@TransactionTimeout", mXmlNamespaceManager).Value.Trim();

                mBehaviorConfigEntrys.Add(entry.Name, entry);
            }

            /*--------------------------------------------------------------------*/

        }
        
        public IDictionary<string, Task> GetTasks()
        {

            Dictionary<string, Task> tasks = new Dictionary<string, Task>();

            foreach (XPathNavigator taskNavigator in mRootNavigator.Select("/task:TaskManifest/task:Task", mXmlNamespaceManager))
            {
                Task task = BuildTask(taskNavigator);
                tasks.Add(taskNavigator.SelectSingleNode("@Name", mXmlNamespaceManager).Value, task);

            }

            //XPathNodeIterator nodeIterator = mRootNavigator.Select("/TaskManifest/Task");
            //while(nodeIterator.MoveNext())
            //{
            //    XPathNavigator navi = nodeIterator.Current;
            //    Task task = BuildTask(navi);
            //    tasks.Add(navi.SelectSingleNode("@Name").Value, task);
            //}
             
            return tasks;
        }

        private Task BuildTask(XPathNavigator taskNavigator)
        {
            Task newTask = new Task();
            
            //select and construct behavior
            foreach (XPathNavigator behaviorNavigator in taskNavigator.Select("task:Behavior", mXmlNamespaceManager))
            {
                Behavior behavior=BuidBehavior(behaviorNavigator);

                newTask.Add(behavior);
            }

            return newTask;

        }

        private Behavior BuidBehavior(XPathNavigator behaviorNavigator)
        {
            Behavior behavior=null;

            //Check if the Behaviors section if it has this config
            //bool runoutComparation = false;
            bool requireContractNotFind = false;
            Type requireContractNotFindType=null;
            int compareCount=1;
            string behaviorContracts = behaviorNavigator.SelectSingleNode("@Contracts", mXmlNamespaceManager).Value.Trim();
            string behaviorConfigurationName = behaviorNavigator.SelectSingleNode("@BehaviorConfiguration", mXmlNamespaceManager).Value.Trim();
            Type[] requiredContractTypes = this.GetTypesFromString(behaviorContracts);
            Type[] implementedContractTypes;
            object resolvedType;

            //resolve contract from config
            //compare to see the required contract is containted in the implemented contract list
            foreach (KeyValuePair<string, BehaviorConfigEntry> keyValuePair in mBehaviorConfigEntrys)
            {
                if (behaviorConfigurationName == keyValuePair.Key)
                {
                    implementedContractTypes = this.GetTypesFromString(keyValuePair.Value.ImplementedConstracts);
                    foreach (Type requiredContractType in requiredContractTypes)
                    {
                        if (!IsConstractContained(requiredContractType, implementedContractTypes))
                        {
                            requireContractNotFind = true;
                            requireContractNotFindType = requiredContractType;
                            break;
                        }
                    }

                    break;
                }
                compareCount++;
            }

            if (compareCount > mBehaviorConfigEntrys.Count)
            {
                throw new Exception(string.Format("BehaviorConfiguration for {0} is Not Found!", behaviorConfigurationName));
            }

            if(requireContractNotFind==true)
            {
                throw new Exception(string.Format("Specified contract \"{0}\" not found in behaviorConfiguraton \"{1}\"", requireContractNotFindType.FullName,
                    behaviorConfigurationName));
            }

            //currently only support one interface caontract
            //if (mUnityContainer.Registrations.ElementAt<ContainerRegistration>(0).Name == null)
            //{
            //    resolvedType = mUnityContainer.Resolve(requiredContractTypes[0]);
            //}
            //else
            //{
            //    resolvedType = mUnityContainer.Resolve(requiredContractTypes[0], mUnityContainer.Registrations.ElementAt<ContainerRegistration>(0).Name);
            //}
            resolvedType = mUnityContainer.Resolve(requiredContractTypes[0]);
            if (!(resolvedType is Behavior))
            { 
                throw new Exception(string.Format(Resource.NotBehaviorException,requiredContractTypes[0].FullName,
                    mUnityContainer.Registrations.ElementAt<ContainerRegistration>(0).Name!=null?mUnityContainer.Registrations.ElementAt<ContainerRegistration>(0).Name: "NotMapped"));
            }

            behavior = (Behavior)resolvedType;

            //build behavior enities
            foreach (XPathNavigator behaviorEntityNavigator in behaviorNavigator.SelectChildren("BehaviorEntity", mXmlNamespaceManager.LookupNamespace("task")))
            { 
                behavior.BehaviorEntities.Add(BuildBehaviorEntity(behaviorEntityNavigator),
                    BuildBehaviorEntityType(behaviorEntityNavigator.GetAttribute("EntityType",string.Empty)));
            }
            
            return behavior;
        }
        
        protected Component BuildBehaviorEntity(XPathNavigator behaviorEntityNavigator)
        {
            string referenceObject = behaviorEntityNavigator.GetAttribute("Reference", string.Empty);
            Type type = GetTypesFromString(referenceObject)[0];
            object entityInstance = mUnityContainer.Resolve(type);
            if (!(entityInstance is Component))
            {
                throw new Exception(string.Format(Resource.NotExpectedTypeException,entityInstance.GetType().FullName,"ZteApp.ProductService.Core.Component"));
            }
            return entityInstance as Component;
        }

        
        

        private Type[] GetTypesFromString(string typeString)
        {
            string[] typeStringArray = typeString.Split(mContractSplitSymbol,StringSplitOptions.RemoveEmptyEntries);
            if (typeStringArray.Length == 0)
            {
                //There is no valid string here, so we simply return null;
                return null;
            }
            List<Type> typesList=new List<Type>();

            foreach (string typeStr in typeStringArray)
            {
                typesList.Add(Type.GetType(typeStr));
            }

            return typesList.ToArray();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //free managed objects
            }

            //free unmanaged objects
            if (mUnityContainer != null)
            {
                mUnityContainer.Dispose();
            }
        }

#region need rewrite or delete
        private bool IsContactsContains(string mainContracts, string subContacts)
        {
            bool result = false;
            Type[] mainCtrs = this.GetTypesFromString(mainContracts);
            Type[] subCtrs = this.GetTypesFromString(subContacts);
            int compareThreshold = 1;
            foreach (Type subCtr in subCtrs)
            {
                compareThreshold++;
                if (compareThreshold > subCtrs.Length)
                {
                    //compare fail, thers is no matched contract here
                    result = false;
                    goto ComparationEnd;
                }
            }
            ComparationEnd:
            return result;
        }
#endregion

#region help functions
        private bool IsConstractContained(Type searchedType, Type[] containerTypes)
        {
            bool typeContained = false;
            for (int i= 0; i< containerTypes.Length; i++)
            {
                if(searchedType.Equals(containerTypes[i]))
                {
                    typeContained=true;
                }
            }

            return typeContained;
        }

        private BehaviorEntityType BuildBehaviorEntityType(string entityTypeString)
        {
            BehaviorEntityType result = BehaviorEntityType.Enviroment;
            switch (entityTypeString.ToLower().Trim())
            {
                case "Subject":
                    result = BehaviorEntityType.Subject;
                    break;
                case "Object":
                    result = BehaviorEntityType.Object;
                    break;
                case "Enviroment":
                    result = BehaviorEntityType.Enviroment;
                    break;
                default:
                    break;
            }
            return result;
        }
#endregion

    }

    internal class BehaviorConfigEntry
    {
        private string mName;
        private string mImplementedConstracts;
        private string mCallBackTimeOut;

        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        public string ImplementedConstracts
        {
            get
            {
                return mImplementedConstracts;
            }
            set
            {
                mImplementedConstracts = value;
            }
        }

        public string CallBackTimeOut
        {
            get
            {
                return mCallBackTimeOut;
            }
            set
            {
                mCallBackTimeOut = value;
            }
        }
    }

}

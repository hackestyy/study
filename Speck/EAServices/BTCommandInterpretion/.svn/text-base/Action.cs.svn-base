using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Speck.EAServices.BTCommandInterpretion
{
    public class Action:ITerm
    {
        private string mAction;
        private Context mContext;
        private Literal mLiterals;
        private string[] mAssemblyQualifiePath4Executoer = new string[] {typeof(Action).Namespace};

        public void Parse(Context context)
        {
            mContext = context;
            string currentTokenValue;
            while (true)
            {
                currentTokenValue = context.CurrentToken().Value.ToLower();
                if (currentTokenValue == "read" || currentTokenValue == "write" || currentTokenValue == "check")
                {
                    mAction = currentTokenValue;
                    context.SkipToken(context.CurrentToken().Value);
                }
                else if (currentTokenValue == ";")
                {
                    break;
                }
                else
                {
                    mLiterals = new Literal();
                    mLiterals.Parse(context);
                }
            }
        }

        public void Interprete()
        {
            if (mAction == "read")
            {
                if (mLiterals.Name == "bt")
                {
                    var result=ResolveExecuter(mLiterals).Read(mLiterals);
                    //need reconstruct
                    mContext.Result.Add(result);
                }
            }
            else if (mAction == "write")
            {
                if (mLiterals.Name == "bt")
                {
                    ResolveExecuter(mLiterals).Write(mLiterals);
                }
            }
            else if (mAction == "check")
            {
                if (mLiterals.Name == "bt")
                {
                    ResolveExecuter(mLiterals).Check(mLiterals);
                }
            }
        }

        public static bool IsAction(Context context)
        {
            bool result = false;
            string currentTokenValue=context.CurrentToken().Value.ToLower();
            if (currentTokenValue == "read" || currentTokenValue == "write" || currentTokenValue == "check")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public string ToString()
        {
            return mAction + mLiterals.ToString();
        }

        public IAccessiable ResolveExecuter(Literal obj)
        {
            object result = null;
            Type type;
            foreach (string qulifiedPath in mAssemblyQualifiePath4Executoer)
            {
                string triedName = qulifiedPath + "." + obj[0].ToString();
                try
                {
                    type = Type.GetType(triedName, true,/*ignore type case*/true);
                    result = Activator.CreateInstance(type);
                    break;
                }
                catch (TypeLoadException ex)
                {
                    continue;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    continue;
                }
            }

            return (result as IAccessiable);
        }

        //internal enum ActionType
        //{
        //    Read,
        //    Write,
        //    Check
        //}
    }
}
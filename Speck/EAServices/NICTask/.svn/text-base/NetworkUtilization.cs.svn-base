using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.Core;
using System.Diagnostics;
using System.Threading;
using ZteApp.ProductService.EAServices.CommunicationTask;
using System.Net.NetworkInformation;

namespace ZteApp.ProductService.EAServices.NICTask
{
    public class NetworkUtilization:Behavior,INetworkInterfaceInfo
    {
        private const int mSummationLoopCount=5;
        private const int mSampleTimeInterval = 1000;   //ms
        private PerformanceCounter mBandwidthCounter;
        private PerformanceCounter mDataSentCounter;
        private PerformanceCounter mDataReceivedCounter;
        private double mBandwidth;
        private double mCurrentUtilization;
        private double mCurrentSentRate;
        private double mCurrentReceivedRate;

        public double CurrentUtilization
        {
            get
            {
                return mCurrentUtilization;
            }
        }

        /// <summary>
        /// Send rate in bytes/sec
        /// </summary>
        public double CurrentSentRate
        {
            get
            {
                return mCurrentSentRate;
            }
        }

        public double CurrentReceivedRate
        {
            get
            {
                return mCurrentReceivedRate;
            }
        }

        public double BandWidth
        {
            get
            {
                return mBandwidth;
            }
        }



        public override void Initialize()
        {
            base.Initialize();
            mCurrentUtilization=0;
            mCurrentSentRate=0;
            mCurrentReceivedRate=0;
            //// DEBUG: Check this array to see if the network card exists 
            //PerformanceCounterCategory NetworkInterfaceCatagory =
            //    PerformanceCounterCategory.GetCategories().Where(c => c.CategoryName.Equals("Network Interface")).FirstOrDefault();
            //foreach (string instance in NetworkInterfaceCatagory.GetInstanceNames())
            //{
            //    System.Diagnostics.Debug.Print(instance);
            //}

            string networkCardName = ReturnNICName();
            networkCardName = networkCardName.Replace("\\", "_");
            networkCardName = networkCardName.Replace("/", "_");
            networkCardName = networkCardName.Replace("(", "[");
            networkCardName = networkCardName.Replace(")", "]");
            networkCardName = networkCardName.Replace("#", "_");
            mBandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCardName);
            mDataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCardName);
            mDataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCardName);
            mBandwidth = mBandwidthCounter.NextValue();
        }

        protected override void DoWork()
        {
            float sendSum = 0;
            float receiveSum = 0;
            for (int index = 0; index < mSummationLoopCount; index++)
            {
                sendSum += mDataSentCounter.NextValue();
                receiveSum += mDataReceivedCounter.NextValue();
            }

            mCurrentUtilization = (8 * (sendSum + receiveSum)) / (mBandwidth * mSummationLoopCount)*100;
            mCurrentSentRate = sendSum / mSummationLoopCount;
            mCurrentReceivedRate = receiveSum / mSummationLoopCount;

            Thread.Sleep(mSampleTimeInterval);
        }

        private string ReturnNICName()
        {
            List<NetworkInterface> Interfaces = new List<NetworkInterface>();
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    Interfaces.Add(nic);
                }
            }

            NetworkInterface result = null;
            foreach (NetworkInterface nic in Interfaces)
            {
                if (result == null)
                {
                    result = nic;
                }
                else
                {
                    try
                    {
                        if (nic.GetIPProperties().GetIPv4Properties() != null)
                        {
                            if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                                continue;
                            if (nic.GetIPProperties().GetIPv4Properties().Index < result.GetIPProperties().GetIPv4Properties().Index)
                                result = nic;
                        }
                    }
                    catch (NetworkInformationException ex)
                    {
                        continue;
                    }
                }
            }
            return result.Description;
        }
    }
}

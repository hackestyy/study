#define zhanghao_modified

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZteApp.ProductService.Core;
using ZteApp.ProductService.EAServices.Helper;
using ZteApp.ProductService.EAServices.CommandInterpretion;
using System.IO;
using ZteApp.ProductService.EAServices.Helper;
using Zte.Manufacture.Service.Common;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    internal class SocketPort : Behavior
    {
        private const int mConcurrentListenerCount = 5;
        private TcpListener mListener;
        private ICommand mDispatcher;
        private SocketCmdOperator mSocketCmdOperator;
        private const int mBufferlength = 256;
        private byte[] mReceivedBytes=new byte[mBufferlength];
        private bool mNeedClose = false;

        public SocketPort(object obj)
        {
            if (obj is ICommand)
            {
                mDispatcher = obj as ICommand;
            }
            else if (obj is SocketCmdOperator)
            {
                mSocketCmdOperator = obj as SocketCmdOperator;
            }
            else
            {
                throw new ArgumentException("argument is not the type expected:",obj.GetType().Name);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            mListener = new TcpListener(IPAddress.Any,Properties.Settings1.Default.RawSocketPort);
        }

        protected override void OnBeforeStart()
        {
            base.OnBeforeStart();

            //start listening from here
            mListener.Start(mConcurrentListenerCount);
        }

        protected override void DoWork()
        {
            Socket socket = null;
            while (!mNeedClose)
            {
                try
                {
                    socket = mListener.AcceptSocket();
                    ThreadPool.QueueUserWorkItem(ProcessCollectedSocket,socket);
                }
                catch (Exception e)
                {
                    mNeedClose = true;
                    mIsWorkFinished = true;
                }
            }
        }

        public override void Abort()
        {
            mNeedClose = true;
            mListener.Stop();
            base.Abort();
        }


        private void ProcessCollectedSocket(object collectedSocket)
        {
            if (!(collectedSocket is Socket))
            {
                return;
            }
            Socket soc = collectedSocket as Socket;
            Stream s=null;
            try
            {
                //accepted and process
                s = new NetworkStream(soc);
                BinaryReader sr = new BinaryReader(s);
                BinaryWriter sw = new BinaryWriter(s);

                while (true)
                {
                    int readInBytesCount = sr.Read(mReceivedBytes, 0, mReceivedBytes.Length);
                    if (readInBytesCount == 0)  //break connection when no bytes were received
                    {
                        break;
                    }
                    if (mDispatcher != null)
                    {
                        mDispatcher.Send(ASCIIRepresentor.ASCIIByteArray2String(mReceivedBytes, 0, readInBytesCount));
                        if (mDispatcher.Result != null)
                        {
                            sw.Write(mDispatcher.Result);
                        }
                    }
                    if (mSocketCmdOperator != null)
                    {
                        byte[] result = mSocketCmdOperator.Do(mReceivedBytes.Sub(0,readInBytesCount), readInBytesCount);
                        sw.Write(result);
                    }
                }
                s.Close();
            }
            catch (Exception e)
            {
                if (s != null)
                    s.Close();
            }
            soc.Close();
        }
    }
}

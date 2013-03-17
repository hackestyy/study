using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServicesProvision.WaveLib;

namespace ZteApp.ProductService.EAServices.BusinessWork
{
    public partial class Service:IAudioLoop
    {
        private WaveOutPlayer m_Player;
        private WaveInRecorder m_Recorder;
        private FifoStream m_Fifo = new FifoStream();

        private byte[] m_PlayBuffer;
        private byte[] m_RecBuffer;

        private void Filler(IntPtr data, int size)
        {
            if (m_PlayBuffer == null || m_PlayBuffer.Length < size)
                m_PlayBuffer = new byte[size];
            if (m_Fifo.Length >= size)
                m_Fifo.Read(m_PlayBuffer, 0, size);
            else
                for (int i = 0; i < m_PlayBuffer.Length; i++)
                    m_PlayBuffer[i] = 0;
            System.Runtime.InteropServices.Marshal.Copy(m_PlayBuffer, 0, data, size);
        }

        private void DataArrived(IntPtr data, int size)
        {
            if (m_RecBuffer == null || m_RecBuffer.Length < size)
                m_RecBuffer = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
            m_Fifo.Write(m_RecBuffer, 0, m_RecBuffer.Length);
        }

        public void Start()
        {
            Stop();
            try
            {
                WaveFormat fmt = new WaveFormat(44100, 16, 2);
                m_Player = new WaveOutPlayer(-1, fmt, 16384, 3, new BufferFillEventHandler(Filler));
                m_Recorder = new WaveInRecorder(-1, fmt, 16384, 3, new BufferDoneEventHandler(DataArrived));
            }
            catch
            {
                Stop();
                throw;
            }
        }
        public void Stop()
        {
            if (m_Player != null)
            {
                try
                {
                    m_Player.Dispose();
                }
                catch (Exception e)
                {
                  //  MessageBox.Show(e.Message);
                }
                finally
                {
                    m_Player = null;
                }
            }
            if (m_Recorder != null)
            {
                try
                {
                    m_Recorder.Dispose();
                }
                catch (Exception e)
                {
                  //  MessageBox.Show(e.Message);
                }
                finally
                {
                    m_Recorder = null;
                }
            }
            m_Fifo.Flush(); // clear all pending data
        }
    }
}

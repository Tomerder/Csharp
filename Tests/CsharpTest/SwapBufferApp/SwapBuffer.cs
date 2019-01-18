using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwapBufferApp
{
    //----------------------------------------------------------------------
    enum ActiveBufferEnum { BUFFER_1, BUFFER_2 , NUM_OF_BUFFERS };
    
    //----------------------------------------------------------------------

    class SwapBuffer : IDisposable
    {
        //----------------------------------------------------------------------
        //DEFINES
        //----------------------------------------------------------------------

        //----------------------------------------------------------------------
        //DM
        //----------------------------------------------------------------------

        private SemaphoreSlim m_semIsInactiveBufferEmpty;
        private Mutex m_mutexProtectBetweenReadAndSwapBuffer;
        private Mutex m_mutexProtectMultipleWrite;

        private List<byte[]> m_buffers;
        private ActiveBufferEnum m_activeBufferIndex;

        private readonly int m_buffersSize;

        private int m_activeBufferSpaceLeft;

        //----------------------------------------------------------------------
        //CTOR
        //----------------------------------------------------------------------
        public SwapBuffer(int _bufferSize)
        {
            m_buffersSize = _bufferSize;
            m_activeBufferSpaceLeft = _bufferSize;

            //init semaphore and mutex
            m_semIsInactiveBufferEmpty = new SemaphoreSlim(1, 1);
            m_mutexProtectBetweenReadAndSwapBuffer = new Mutex();
            m_mutexProtectMultipleWrite = new Mutex();

            //init buffers
            m_buffers = new List<byte[]>();
            for (int i=0; i< ActiveBufferEnum.NUM_OF_BUFFERS.GetHashCode(); i++)
            {
                byte[] buffer = new byte[_bufferSize];
                m_buffers.Add(buffer);
            }

            //init active buffer
            m_activeBufferIndex = ActiveBufferEnum.BUFFER_1;
        }

        //----------------------------------------------------------------------
        //Dispose - for any disposable DM 
        //----------------------------------------------------------------------
        public void Dispose()
        {
            m_semIsInactiveBufferEmpty.Dispose();
            m_mutexProtectBetweenReadAndSwapBuffer.Dispose();
            m_mutexProtectMultipleWrite.Dispose();
        }

        //----------------------------------------------------------------------
        //APIs
        //----------------------------------------------------------------------

        public bool Write(byte[] _dataToWrite, int _sizeToWrite)
        {
            //check if chunk size is bigger then buffer size
            if(_sizeToWrite > m_buffersSize)
            {
                return false;
            }

            //----------------
            //lock - protect against multiple writes
            m_mutexProtectMultipleWrite.WaitOne();

            //check if chunk size is bigger then space left on active buffer
            int chunkSizeToWrite = _sizeToWrite;
            int indexToWriteFrom = 0;
            if (_sizeToWrite > m_activeBufferSpaceLeft)
            {
                //update size left to be written
                int sizeWritten = m_activeBufferSpaceLeft;

                //write to active buffer (And fill it completely) 
                WriteToActiveBuffer(_dataToWrite, 0, sizeWritten);

                //update size and index to write from - on next write
                chunkSizeToWrite = _sizeToWrite - sizeWritten;               
                indexToWriteFrom = sizeWritten;

                //SwapBuffers
                SwapBuffers();
            }
          
            //writeToActiveBuffer
            WriteToActiveBuffer(_dataToWrite, indexToWriteFrom, chunkSizeToWrite);

            //SwapBuffers if out of space on active buffer
            if (m_activeBufferSpaceLeft == 0)
            {
                SwapBuffers();
            }
         
            //release lock - now can read again
            m_mutexProtectMultipleWrite.ReleaseMutex();
            //----------------

            return true;
        }

        //----------------------------------------------------------------------
        //will read inactive buffer (fully or not at all)
        public bool Read(out byte[] _dataRead, out int _sizeRead)
        {
            _dataRead = new byte[m_buffersSize];
            _sizeRead = 0;
           
            //exit if inactive buffer is empty (nothing to read)
            if (m_semIsInactiveBufferEmpty.CurrentCount == 1)
            {
                return false;
            }

            //----------------
            //lock - dont want to swap buffer or read again while reading
            m_mutexProtectBetweenReadAndSwapBuffer.WaitOne();

            ReadFromInactiveBuffer(_dataRead);
            _sizeRead = m_buffersSize;

            //release lock
            m_mutexProtectBetweenReadAndSwapBuffer.ReleaseMutex();
            //----------------

            //we dont want to swapBuffer before inactive buffer is read
            //now inactive buffer is empty - so we can release semaphore so swap buffer can be preformed
            m_semIsInactiveBufferEmpty.Release();

            return true;
        }

        //----------------------------------------------------------------------
        //force swap buffer and read inactive buffer
        public void ForceSwapBufferAndRead(out byte[] _dataRead, out int _sizeRead)
        {
            _dataRead = new byte[m_buffersSize];

            //read size = space filled on active buffer (before the swap)
            _sizeRead = m_buffersSize - m_activeBufferSpaceLeft;

            //swap buffers - to fill inactive buffer
            SwapBuffers();

            //read inactive buffer
            ReadFromInactiveBuffer(_dataRead);           
        }

        //----------------------------------------------------------------------
        //inner implementaion 
        //----------------------------------------------------------------------

        private void WriteToActiveBuffer(byte[] _dataToWrite, int _writeFromIndex, int _sizeToWrite)
        {
            int activeBufferIndexToWriteFrom = m_buffersSize - m_activeBufferSpaceLeft;
            //memcpy to swap buffer - active buffer
            //to => m_buffers[m_activeBuffer.GetHashCode()][activeBufferIndexToWriteFrom] 
            int activeBufferIndex = m_activeBufferIndex.GetHashCode();
            Array.Copy(_dataToWrite, _writeFromIndex, m_buffers[activeBufferIndex], activeBufferIndexToWriteFrom, _sizeToWrite);

            //update active buffer left space
            m_activeBufferSpaceLeft -= _sizeToWrite;
        }

        //----------------------------------------------------------------------

        private void ReadFromInactiveBuffer(byte[] _bufferToCopyTo)
        {
            //inactive buffer
            int inactiveBufferIndex = ActiveBufferEnum.BUFFER_1.GetHashCode();
            if (m_activeBufferIndex == ActiveBufferEnum.BUFFER_1)
            {
                inactiveBufferIndex = ActiveBufferEnum.BUFFER_2.GetHashCode();
            }
            byte[] inactiveBuffer = m_buffers[inactiveBufferIndex];

            //retrieve data from inactive buffer
            Array.Copy(inactiveBuffer, _bufferToCopyTo, m_buffersSize);
        }

        //----------------------------------------------------------------------
        private void SwapBuffers()
        {
            //only if finished reading from inactive buffer (inactive buffer is empty and ready to be swapped) 
            //semapore take
            //semaphore will be released only upon inactive buffer is read
            m_semIsInactiveBufferEmpty.Wait();

            //----------------
            //lock - dont want to swap buffer again or read while swapping
            m_mutexProtectBetweenReadAndSwapBuffer.WaitOne();

            m_activeBufferIndex += 1;
            if(m_activeBufferIndex == ActiveBufferEnum.NUM_OF_BUFFERS)
            {
                m_activeBufferIndex = ActiveBufferEnum.BUFFER_1;
            }

            //active buffer is now empty
            m_activeBufferSpaceLeft = m_buffersSize;

            //release lock
            m_mutexProtectBetweenReadAndSwapBuffer.ReleaseMutex();
            //----------------

        }

        //----------------------------------------------------------------------
    }
}

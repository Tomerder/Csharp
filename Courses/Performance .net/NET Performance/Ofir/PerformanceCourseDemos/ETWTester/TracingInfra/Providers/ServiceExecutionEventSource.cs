using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Tracing;

namespace TraceingInfra.Providers
{
    [EventSource(Name = "POC.Service", Guid = "{E4CF8AA8-F9F5-46F5-A692-03FE14FE2B71}")]
    public sealed class ServiceExecutionEventSource : EventSource
    {
        [Event(1, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.ServiceRunner)]
        public void StartServiceRunner(string serviceName) { if (IsEnabled()) WriteEvent(1, serviceName); }
        [Event(2, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.ServiceRunner)] 
        public void EndServiceRunner(string serviceName) { if (IsEnabled()) WriteEvent(2, serviceName); }

        [Event(3, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.HandleException)]
        public void StartHandlingException(string serviceName)  { if (IsEnabled()) WriteEvent(3, serviceName); }
        [Event(4, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.HandleException)]
        public void EndHandlingException(string serviceName) { if (IsEnabled()) WriteEvent(4, serviceName); }

        [Event(5, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.PipelineExecution)]
        public void StartPipelineExecution()
        {
            WriteEvent(5);
        }

        [Event(6, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.PipelineExecution)]
        public void EndPipelineExecution()
        {
            WriteEvent(6);
        }

        //[Event(7, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.ServiceExecution)]
        //public void StartServiceExecution(Type serviceType)
        //{
        //    WriteEvent(7, serviceType.FullName);
        //}

        //[Event(8, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.ServiceExecution)]
        //public void EndServiceExecution(Type serviceType)
        //{
        //    WriteEvent(8, serviceType.FullName);
        //}

        [Event(9, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.WriteResponse)]
        public void StartWritingResponse(string serviceName, bool isStream)
        {
            WriteEvent(9, serviceName, isStream.ToString());
        }

        [Event(10, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.WriteResponse)]
        public void EndWritingResponse(string serviceName, bool isStream)
        {
            WriteEvent(10, serviceName, isStream.ToString());
        }

        [Event(11, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.OpenUnitOfWork)]
        public void StartOpeningUnitOfWork()
        {
            WriteEvent(11);
        }

        [Event(12, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.OpenUnitOfWork)]
        public void EndOpeningUnitOfWork()
        {
            WriteEvent(12);
        }

        [Event(13, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.CompleteUnitOfWork)]
        public void StartCompletingUnitOfWork()
        {
            WriteEvent(13);
        }

        [Event(14, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.CompleteUnitOfWork)]
        public void EndCompletingUnitOfWork()
        {
            WriteEvent(14);
        }

        [Event(15, Opcode = EventOpcode.Start, Task = ServiceExecuterTasks.RollbackUnitOfWork)]
        public void StartRollingBackUnitOfWork()
        {
            WriteEvent(15);
        }

        [Event(16, Opcode = EventOpcode.Stop, Task = ServiceExecuterTasks.RollbackUnitOfWork)]
        public void EndRollingBackUnitOfWork()
        {
            WriteEvent(16);
        }

        //private void WriteIfEnabled(int eventId, string arg = null, string arg1 = null)
        //{
        //    if (!IsEnabled())
        //        return;

        //    if (arg == null)
        //    {
        //        WriteEvent(eventId);
        //    }
        //    else if (arg1 == null)
        //    {
        //        WriteEvent(eventId, arg);
        //    }
        //    else
        //    {
        //        WriteEvent(eventId, arg, arg1);
        //    }
        //}

    }
    internal class ServiceExecuterTasks
    {
        public const EventTask ServiceRunner = (EventTask)1;
        public const EventTask HandleException = (EventTask)2;
        public const EventTask PipelineExecution = (EventTask)3;
        public const EventTask ServiceExecution = (EventTask)4;
        public const EventTask WriteResponse = (EventTask)5;
        public const EventTask OpenUnitOfWork = (EventTask)6;
        public const EventTask CompleteUnitOfWork = (EventTask)7;
        public const EventTask RollbackUnitOfWork = (EventTask)8;
    }
}

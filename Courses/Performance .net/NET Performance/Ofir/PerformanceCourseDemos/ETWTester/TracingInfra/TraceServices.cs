using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraceingInfra.Providers;


namespace TraceingInfra
{
    public static class TraceServices
    {
        static TraceServices()
        {
            Pipeline = new PipelineEventSource();
            DataTransfer = new DataTransferEventSource();
            ServiceExecution = new ServiceExecutionEventSource();
        }

        public static PipelineEventSource Pipeline { get; private set; }
        public static DataTransferEventSource DataTransfer { get; private set; }
        public static ServiceExecutionEventSource ServiceExecution { get; set; }
    }
}

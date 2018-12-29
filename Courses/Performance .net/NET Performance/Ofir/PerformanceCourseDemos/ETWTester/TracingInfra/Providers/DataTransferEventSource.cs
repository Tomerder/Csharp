using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Tracing;

namespace TraceingInfra.Providers
{

    [EventSource(Name = "POC.DataTransfer", Guid = "{ff8d5988-ac3c-40b0-8829-c4e42a79d223}")]
    public sealed class DataTransferEventSource : EventSource
    {
        [Event(1, Opcode = EventOpcode.Info, Task = DmsTasks.GenericReport)]
        public void ReportEvent(string name, string description) { if (IsEnabled()) WriteEvent(1, name, description); }

        // ContextPropertiesPromoter
        [Event(2, Opcode = EventOpcode.Start, Task = DmsTasks.ContextPropertiesPromoter)]
        public void ContextPropertiesPromoter(string businessUnit, string serverGroup) { if (IsEnabled()) WriteEvent(2, businessUnit, serverGroup); }
        [Event(3, Opcode = EventOpcode.Stop, Task = DmsTasks.ContextPropertiesPromoter)]
        public void ContextPropertiesPromoterCompleted(string businessUnit, string serverGroup) { if (IsEnabled()) WriteEvent(3, businessUnit, serverGroup); }

        // DTOCreation
        [Event(4, Opcode = EventOpcode.Start, Task = DmsTasks.DtoCreation)]
        public void DtoCreation(string entityName) { if (IsEnabled()) WriteEvent(4, entityName); }
        [Event(5, Opcode = EventOpcode.Stop, Task = DmsTasks.DtoCreation)]
        public void DtoCreationCompleted(string entityName) { if (IsEnabled()) WriteEvent(5, entityName); }

        // Versioning
        [Event(6, Opcode = EventOpcode.Start, Task = DmsTasks.Versioning)]
        public void Versioning(string entityName) { if (IsEnabled()) WriteEvent(6, entityName); }
        [Event(7, Opcode = EventOpcode.Stop, Task = DmsTasks.Versioning)]
        public void VersioningCompleted(string entityName) { if (IsEnabled()) WriteEvent(7, entityName); }

        // Serialization
        [Event(8, Opcode = EventOpcode.Start, Task = DmsTasks.Serialization)]
        public void Serialization(string entityName) { if (IsEnabled()) WriteEvent(8, entityName); }
        [Event(9, Opcode = EventOpcode.Stop, Task = DmsTasks.Serialization)]
        public void SerializationCompleted(string entityName) { if (IsEnabled()) WriteEvent(9, entityName); }

        // PersistToOutbox
        [Event(10, Opcode = EventOpcode.Start, Task = DmsTasks.PersistToOutbox)]
        public void PersistToOutbox(string entityName) { if (IsEnabled()) WriteEvent(10, entityName); }
        [Event(11, Opcode = EventOpcode.Stop, Task = DmsTasks.PersistToOutbox)]
        public void PersistToOutboxCompleted(string entityName) { if (IsEnabled()) WriteEvent(11, entityName); }

        // GetDataServices
        [Event(12, Opcode = EventOpcode.Start, Task = DmsTasks.GetDataServices)]
        public void GetDataServices(string entityName) { if (IsEnabled()) WriteEvent(12, entityName); }
        [Event(13, Opcode = EventOpcode.Stop, Task = DmsTasks.GetDataServices)]
        public void GetDataServicesCompleted(string entityName) { if (IsEnabled()) WriteEvent(13, entityName); }

        // LoadFromOutbox
        [Event(14, Opcode = EventOpcode.Start, Task = DmsTasks.LoadFromOutbox)]
        public void LoadFromOutbox(string entityName) { if (IsEnabled()) WriteEvent(14, entityName); }
        [Event(15, Opcode = EventOpcode.Stop, Task = DmsTasks.LoadFromOutbox)]
        public void LoadFromOutboxCompleted(string entityName) { if (IsEnabled()) WriteEvent(15, entityName); }

        // SaveToInbox
        [Event(16, Opcode = EventOpcode.Start, Task = DmsTasks.SaveToInbox)]
        public void SaveToInbox(string entityName) { if (IsEnabled()) WriteEvent(16, entityName); }
        [Event(17, Opcode = EventOpcode.Stop, Task = DmsTasks.SaveToInbox)]
        public void SaveToInboxCompleted(string entityName) { if (IsEnabled()) WriteEvent(17, entityName); }

        // LoadFromInbox
        [Event(18, Opcode = EventOpcode.Start, Task = DmsTasks.LoadFromInbox)]
        public void LoadFromInbox(string entityName) { if (IsEnabled()) WriteEvent(18, entityName); }
        [Event(19, Opcode = EventOpcode.Stop, Task = DmsTasks.LoadFromInbox)]
        public void LoadFromInboxCompleted(string entityName) { if (IsEnabled()) WriteEvent(19, entityName); }

        // Deserialization
        [Event(20, Opcode = EventOpcode.Start, Task = DmsTasks.Desirialization)]
        public void Deserialization(string entityName) { if (IsEnabled()) WriteEvent(20, entityName); }
        [Event(21, Opcode = EventOpcode.Stop, Task = DmsTasks.Desirialization)]
        public void DeserializationCompleted(string entityName) { if (IsEnabled()) WriteEvent(21, entityName); }

        // Apply
        [Event(22, Opcode = EventOpcode.Start, Task = DmsTasks.Apply)]
        public void Apply(string entityName, string changeType) { if (IsEnabled()) WriteEvent(22, entityName, changeType); }
        [Event(23, Opcode = EventOpcode.Stop, Task = DmsTasks.Apply)]
        public void ApplyCompleted(string entityName, string changeType) { if (IsEnabled()) WriteEvent(23, entityName, changeType); }
    }
    
    internal class DmsTasks
    {
        public const EventTask GenericReport = (EventTask)1;
        public const EventTask ContextPropertiesPromoter = (EventTask)2; 
        public const EventTask DtoCreation = (EventTask)4;
        public const EventTask Versioning = (EventTask)8; 
        public const EventTask Serialization = (EventTask)16;
        public const EventTask PersistToOutbox = (EventTask)32;
        public const EventTask GetDataServices = (EventTask)64;
        public const EventTask LoadFromOutbox = (EventTask)128;
        public const EventTask SaveToInbox = (EventTask)256;
        public const EventTask LoadFromInbox = (EventTask)512;
        public const EventTask Desirialization = (EventTask)1024;
        public const EventTask Apply = (EventTask)2048; 
    }

    //public class DMSKeyword
    //{
    //    public const EventKeywords Record = (EventKeywords)1;
    //    public const EventKeywords Transport = (EventKeywords)2; // (EntityName, ChangeType)
    //    public const EventKeywords Apply = (EventKeywords)4;
    //}
}

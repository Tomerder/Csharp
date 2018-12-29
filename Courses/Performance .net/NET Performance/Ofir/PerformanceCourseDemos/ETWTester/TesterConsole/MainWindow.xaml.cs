using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using TraceingInfra;
using System.Threading.Tasks;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task task;
        private int counter = 0;
        public MainWindow()
        {
            InitializeComponent();

            task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        TraceServices.DataTransfer.ReportEvent("generic report title", "generic report extra data");
                        TraceServices.DataTransfer.ContextPropertiesPromoter("BU" + i, "Server group " + i);
                        TraceServices.DataTransfer.ContextPropertiesPromoterCompleted("BU" + i, "Server group " + i);
                        TraceServices.DataTransfer.Versioning("Price");
                        TraceServices.DataTransfer.VersioningCompleted("Price");
                        TraceServices.DataTransfer.Serialization("Consumable");
                        TraceServices.DataTransfer.SerializationCompleted("Consumable");
                        TraceServices.DataTransfer.PersistToOutbox("Balance");
                        TraceServices.DataTransfer.PersistToOutboxCompleted("Balance");
                        TraceServices.DataTransfer.GetDataServices("Evaluation");
                        TraceServices.DataTransfer.GetDataServicesCompleted("Evaluation");
                        TraceServices.DataTransfer.LoadFromOutbox("Tax");
                        TraceServices.DataTransfer.LoadFromOutboxCompleted("Tax");
                        TraceServices.DataTransfer.SaveToInbox("Tender");
                        TraceServices.DataTransfer.SaveToInboxCompleted("Tender");
                        TraceServices.DataTransfer.LoadFromInbox("Supplier");
                        TraceServices.DataTransfer.LoadFromInboxCompleted("Supplier");
                        TraceServices.DataTransfer.Deserialization("Promotion");
                        TraceServices.DataTransfer.DeserializationCompleted("Promotion");
                        TraceServices.DataTransfer.Apply("Product", "add");
                        TraceServices.DataTransfer.ApplyCompleted("Product", "add");


                        TraceServices.Pipeline.ReportEvent("generic report title", "generic report extra data");
                        TraceServices.Pipeline.ContextPropertiesPromoter("BU" + i, "Server group " + i);
                        TraceServices.Pipeline.ContextPropertiesPromoterCompleted("BU" + i, "Server group " + i);
                        TraceServices.Pipeline.Versioning("Price");
                        TraceServices.Pipeline.VersioningCompleted("Price");
                        TraceServices.Pipeline.Serialization("Consumable");
                        TraceServices.Pipeline.SerializationCompleted("Consumable");
                        TraceServices.Pipeline.PersistToOutbox("Balance");
                        TraceServices.Pipeline.PersistToOutboxCompleted("Balance");
                        TraceServices.Pipeline.GetDataServices("Evaluation");
                        TraceServices.Pipeline.GetDataServicesCompleted("Evaluation");
                        TraceServices.Pipeline.LoadFromOutbox("Tax");
                        TraceServices.Pipeline.LoadFromOutboxCompleted("Tax");
                        TraceServices.Pipeline.SaveToInbox("Tender");
                        TraceServices.Pipeline.SaveToInboxCompleted("Tender");
                        TraceServices.Pipeline.LoadFromInbox("Supplier");
                        TraceServices.Pipeline.LoadFromInboxCompleted("Supplier");
                        TraceServices.Pipeline.Deserialization("Promotion");
                        TraceServices.Pipeline.DeserializationCompleted("Promotion");
                        TraceServices.Pipeline.Apply("Product", "add");
                        TraceServices.Pipeline.ApplyCompleted("Product", "add");

                        TraceServices.ServiceExecution.StartCompletingUnitOfWork();
                        TraceServices.ServiceExecution.EndCompletingUnitOfWork();
                        TraceServices.ServiceExecution.StartHandlingException("myservicename");
                        TraceServices.ServiceExecution.EndHandlingException("myservicename");
                        TraceServices.ServiceExecution.StartOpeningUnitOfWork();
                        TraceServices.ServiceExecution.EndOpeningUnitOfWork();
                        TraceServices.ServiceExecution.StartPipelineExecution();
                        TraceServices.ServiceExecution.EndPipelineExecution();
                        TraceServices.ServiceExecution.StartRollingBackUnitOfWork();
                        TraceServices.ServiceExecution.EndRollingBackUnitOfWork();
                        TraceServices.ServiceExecution.StartServiceRunner("myserviceName");
                        TraceServices.ServiceExecution.EndServiceRunner("myserviceName");
                        TraceServices.ServiceExecution.StartWritingResponse("myserviceName", true);
                        TraceServices.ServiceExecution.EndWritingResponse("myserviceName", true);
                    }
                    this.Dispatcher.BeginInvoke(new Action(() => 
                    {
                        counter ++ ;
                        txtItemsCount.Text = (counter * 10000 * 60).ToString();
                    }));
                    Thread.Sleep(1000);
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FailuresMonitor;
using System.Threading;
using System.IO;

namespace FaliuresMonitor
{
    public partial class FailuresMonitorForm : Form
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        // enables thread safe call of AppendTerminalResult from Connection.ReadHandler 
        delegate void AppendResultToMessagesPanelDelegate(string _text);
        delegate void SetCheckBoxIsReceivedOkDelegate(bool _isReceviedOk);

        Thread m_threadMainFlow;

        //-------------------------------------------------------------------------------

        public FailuresMonitorForm()
        {
            InitializeComponent();

            CreateFormFieldsFromRegsMap();

            textBoxBaseAddress.Text = Program.INITIAL_BASE_ADDRESS;    
      
            //initiate main flow - called every minute
            m_threadMainFlow = new Thread(new ThreadStart(MainFlow));
            m_threadMainFlow.IsBackground = true;
            m_threadMainFlow.Start();
        }

        //-------------------------------------------------------------------------------

        public void MainFlow()
        {
            //string test = File.ReadAllText("J:\\EFVS\\Dassault\\PcSimulator\\NVRAM\\SR_NVRAM_USER_02");

            while (true)
            {
                if (Program.Connection.IsConnected())
                {
                    bool isReceivedOk = GetFailuresResultsDcmd();
                    
                    SetCheckBoxIsReceivedOk(isReceivedOk);      
                   
                    Thread.Sleep(1000);
                }
            }
        }

        //-------------------------------------------------------------------------------

        private void buttonGetFailuresResults_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<bool> resultsList; 

            if (!CommTargetAbstractLayer.GetResultsFromTarget(textBoxBaseAddress.Text, Failure.FAILURE_RESULT_BITS_LEN, Program.FailuresMap.Count, out resultsList))
            {
                AppendMessagePanel("Error retrieving Failures results");
                return;
            }
            else
            {
                AppendMessagePanel("Failures results were retrieved successfully");
            }

            List<int> failuresChangedList;
            Program.UpdateFailuresWithResults(resultsList, out failuresChangedList);

            DisplayChangesMessages(failuresChangedList);

            UpdateScreenWithResults();

            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------
        /*called every second - schedule*/
        private bool GetFailuresResultsDcmd()
        {       
            List<bool> resultsList;

            if (!CommTargetAbstractLayer.GetResultsFromTargetUsingDCmd(textBoxBaseAddress.Text, Failure.FAILURE_RESULT_BITS_LEN, Program.FailuresMap.Count, out resultsList))
            {
                return false;
            }

            List<int> failuresChangedList;
            Program.UpdateFailuresWithResults(resultsList, out failuresChangedList);

            DisplayChangesMessages(failuresChangedList);

            UpdateScreenWithResults();

            return true;
        }

        //-------------------------------------------------------------------------------

        private void DisplayChangesMessages(List<int> _failuresChangedList)
        {
            foreach (int failureNum in _failuresChangedList)
            {
                string changeMsg;
                if (Program.FailuresMap[failureNum].FailureResult == true)
                {
                    changeMsg = " - Was Turned On";
                }
                else
                {
                    changeMsg = " - Was Turned Off";
                }
                AppendMessagePanel(DateTime.Now.ToString("h:mm:ss tt") + " *** Failure " + failureNum + " : " + Program.FailuresMap[failureNum].FailureName + changeMsg);
            }
        }

        //-------------------------------------------------------------------------------

        private void UpdateScreenWithResults()
        {
            bool isHudFail = false;
            bool isSvsFail = false;
            bool isEvsFail = false;

            foreach(Failure failure in Program.FailuresMap.Values)
            {           
                //label color
                string labelName = "labelFailure" + failure.FailureNum;
                Label labelFailure = panel1.Controls[labelName] as Label;
                Label labelHud = panel1.Controls[labelName + "hudFail"] as Label;
                Label labelSvs = panel1.Controls[labelName + "SvsFail"] as Label;
                Label labelEvs = panel1.Controls[labelName + "EvsFail"] as Label;

                if (labelFailure != null)
                {
                    if(failure.FailureResult)
                    {
                        //failure label
                        labelFailure.BackColor = Color.Red;

                        //failure attributes labels
                        if (labelHud != null)
                        {
                            labelHud.ForeColor = Color.Red;
                            isHudFail = true;
                        }
                        if (labelSvs != null)
                        {
                            labelSvs.ForeColor = Color.Red;
                            isSvsFail = true;
                        }
                        if (labelEvs != null)
                        {
                            labelEvs.ForeColor = Color.Red;
                            isEvsFail = true;
                        }
                    }
                    else
                    {
                        //failure label
                        labelFailure.BackColor = Color.LightGreen;

                        //failure attributes labels
                        if (labelHud != null)
                        {
                            labelHud.ForeColor = Color.Black;
                        }
                        if (labelSvs != null)
                        {
                            labelSvs.ForeColor = Color.Black;
                        }
                        if (labelEvs != null)
                        {
                            labelEvs.ForeColor = Color.Black;
                        }
                    }                   
                }
            }// foreach(Failure failure in Program.FailuresMap.Values)

            //update labels indication at top of the screen (HUS/SVS/EVS fail)
            if (isHudFail)
            {
                labelIsHudFail.BackColor = Color.Red;
            }
            else
            {
                labelIsHudFail.BackColor = Color.Green;
            }

            if (isSvsFail)
            {
                labelIsSvsFail.BackColor = Color.Red;
            }
            else
            {
                labelIsSvsFail.BackColor = Color.Green;
            }

            if (isEvsFail)
            {
                labelIsEvsFail.BackColor = Color.Red;
            }
            else
            {
                labelIsEvsFail.BackColor = Color.Green;
            }
        }

        //-------------------------------------------------------------------------------

        private void toolStripButtonConnection_Click(object sender, EventArgs e)
        {
            if (!Program.Connection.IsConnected())
            {
                //connect
                if (Program.Connection.Connect())
                {
                    toolStripButtonConnection.Image = global::FaliuresMonitor.Properties.Resources.disconnect;
                    toolStripButtonConnection.ToolTipText = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Serial port is already in use", "Error");
                }
            }
            else
            {
                //disconnect
                Program.Connection.Disconnect();
                toolStripButtonConnection.Image = global::FaliuresMonitor.Properties.Resources.connect;
                toolStripButtonConnection.ToolTipText = "Connect";
            }

            //refresh tooltip text
            toolStripButtonConnection.Visible = false;
            toolStripButtonConnection.Visible = true;
        }

        //-------------------------------------------------------------------------------

        public void AppendMessagePanel(string _msg)
        {
            if (this.textBoxMessages.InvokeRequired)
            {
                AppendResultToMessagesPanelDelegate d = new AppendResultToMessagesPanelDelegate(AppendMessagePanel);
                this.Invoke(d, new object[] { _msg });
            }
            else
            {
                textBoxMessages.AppendText(_msg + Environment.NewLine);
                textBoxMessages.ScrollToCaret();
            }
        }

        //-------------------------------------------------------------------------------

        public void SetCheckBoxIsReceivedOk(bool _isReceivedOk)
        {
            if (this.textBoxMessages.InvokeRequired)
            {
                SetCheckBoxIsReceivedOkDelegate d = new SetCheckBoxIsReceivedOkDelegate(SetCheckBoxIsReceivedOk);
                try
                {
                    this.Invoke(d, new object[] { _isReceivedOk });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                checkBoxReceiveIndication.Checked = _isReceivedOk;
            }
        }

        //-------------------------------------------------------------------------------

        private void buttonGetFailuresResultsD_Click(object sender, EventArgs e)
        {
            if (!GetFailuresResultsDcmd())
            {
                AppendMessagePanel("Error retrieving Failures results");
            }
            else
            {
                AppendMessagePanel("Failures results were retrieved successfully");
            }
        }

        //------------------------------------------------------------------------------- 
    }
}

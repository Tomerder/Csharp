using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FailuresMonitor;

namespace FaliuresMonitor
{
    public partial class FailuresMonitorForm : Form
    {
        //-------------------------------------------------------------------------------

        public FailuresMonitorForm()
        {
            InitializeComponent();

            CreateFormFieldsFromRegsMap();

            textBoxBaseAddress.Text = Program.INITIAL_BASE_ADDRESS;
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
        private bool GetFailuresResultsD()
        {       
            List<bool> resultsList;

            if (!CommTargetAbstractLayer.GetResultsFromTargetOneCmd(textBoxBaseAddress.Text, Failure.FAILURE_RESULT_BITS_LEN, Program.FailuresMap.Count, out resultsList))
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
                AppendMessagePanel("Failure " + failureNum + " : " + Program.FailuresMap[failureNum].FailureName + changeMsg);
            }
        }

        //-------------------------------------------------------------------------------

        private void UpdateScreenWithResults()
        {
            foreach(Failure failure in Program.FailuresMap.Values)
            {           
                //checkbox
                /*string checkBoxName = "checkBoxFailure" + failure.FailureNum;
                CheckBox checkBox = panel1.Controls[checkBoxName] as CheckBox;
                if (checkBox != null)
                {
                    checkBox.Checked = failure.FailureResult;
                }*/

                //label color
                string labelName = "labelFailure" + failure.FailureNum;
                Label label = panel1.Controls[labelName] as Label;
                if (label != null)
                {
                    if(failure.FailureResult)
                    {
                        label.BackColor = Color.Red;
                    }
                    else
                    {
                        label.BackColor = Color.LightGreen ;
                    }                   
                }
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
            textBoxMessages.AppendText(_msg + Environment.NewLine);
            textBoxMessages.ScrollToCaret();
        }

        //-------------------------------------------------------------------------------

        private void buttonGetFailuresResultsD_Click(object sender, EventArgs e)
        {
            if (!GetFailuresResultsD())
            {
                AppendMessagePanel("Error retrieving Failures results");
            }
        }

        //------------------------------------------------------------------------------- 
    }
}

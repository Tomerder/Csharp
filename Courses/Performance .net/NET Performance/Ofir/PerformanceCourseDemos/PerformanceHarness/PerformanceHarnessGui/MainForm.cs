using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PerformanceHarness;
using ProgressChangedEventArgs=PerformanceHarness.ProgressChangedEventArgs;
using ProgressChangedEventHandler=PerformanceHarness.ProgressChangedEventHandler;

namespace PerformanceHarnessGui
{
    public partial class MainForm : Form
    {
        private Dictionary<string, string> _fileNameFilters = new Dictionary<string, string>();
        private Orchestrator _orchestrator = new Orchestrator();
        private RunSetupForm _runSetupForm = new RunSetupForm();

        public MainForm()
        {
            InitializeComponent();

            _fileNameFilters.Add(OutputMethod.CSV.ToString(), "CSV Files (*.csv)|*.csv");
            _fileNameFilters.Add(OutputMethod.XML.ToString(), "XML Files (*.xml)|*.xml");
            _fileNameFilters.Add(OutputMethod.Excel.ToString(), "Excel Files (*.xls)|*.xls");

            outputMethodCombo.Items.AddRange(Enum.GetNames(typeof(OutputMethod)));
            outputMethodCombo.SelectedIndex = 0;

            //startPageContent.TextBox.Text =
            //    File.ReadAllText(@"..\..\..\PerformanceHarness\BasicTemplate_MultipleSample.txt");
            RegisterToContentEvents(startPageContent);

            _orchestrator.ProgressChanged += new ProgressChangedEventHandler(orchestrator_ProgressChanged);
        }

        private void RegisterToContentEvents(TabContentControl control)
        {
            control.AddToTests += new ContentCommandEventHandler(control_AddToTests);
            control.Duplicate += new ContentCommandEventHandler(control_Duplicate);
            control.Run += new ContentCommandEventHandler(control_Run);
            control.Rename += new ContentCommandEventHandler(control_Rename);
            control.Delete += new ContentCommandEventHandler(control_Delete);
        }

        private void control_Delete(object sender, ContentCommandEventArgs e)
        {
            if (tabControl.TabCount == 1)
            {
                MessageBox.Show("Can't remove the last tab.\nAdd another tab and then remove this one.",
                                "Only one tab remaining", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tabText = (sender as TabContentControl).TestName;
            TabPage tabToRemove = null;
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Text == tabText)
                {
                    tabToRemove = tabPage;  // Can't modify the collection while enumerating it.
                    break;
                }
            }
            if (tabToRemove != null)
            {
                tabControl.TabPages.Remove(tabToRemove);
                selectedTests.Items.RemoveByKey(tabToRemove.Text);
            }
        }

        private void control_Rename(object sender, ContentCommandEventArgs e)
        {
            string oldName = tabControl.SelectedTab.Text;
            string newName = InputBoxForm.GetUserInput("Enter the new name:", oldName);
            tabControl.SelectedTab.Text = newName;
            (sender as TabContentControl).TestName = newName;

            if (selectedTests.Items.ContainsKey(oldName))
            {
                selectedTests.Items.RemoveByKey(oldName);
                selectedTests.Items.Add(newName, newName, 0).Tag = sender;
            }
        }

        private void control_Run(object sender, ContentCommandEventArgs e)
        {
            RunAndShowResults(e.TextBox.Text);
        }

        private void control_Duplicate(object sender, ContentCommandEventArgs e)
        {
            string newTabName =
                InputBoxForm.GetUserInput("Enter a name for the new tab", "Test Page " + tabControl.TabPages.Count);

            AddNewTab(newTabName, e.TextBox.Text);
        }

        private void AddNewTab(string tabName, string tabContent)
        {
            TabPage newTabPage = new TabPage(tabName);
            tabControl.TabPages.Add(newTabPage);

            TabContentControl newTabContent = new TabContentControl();
            newTabContent.TestName = tabName;
            newTabContent.TextBox.Text = tabContent;
            newTabPage.Controls.Add(newTabContent);
            newTabContent.Size = startPageContent.Size;
            newTabContent.Location = startPageContent.Location;
            newTabContent.Dock = startPageContent.Dock;
            newTabContent.Margin = startPageContent.Margin;
            newTabContent.RunEnabled = startPageContent.RunEnabled;

            RegisterToContentEvents(newTabContent);
            tabControl.SelectedTab = newTabPage;            
        }

        private void control_AddToTests(object sender, ContentCommandEventArgs e)
        {
            TabContentControl tabContentControl = sender as TabContentControl;
            Debug.Assert(tabContentControl != null);

            string testName = tabContentControl.TestName;
            if (!selectedTests.Items.ContainsKey(testName))
                selectedTests.Items.Add(testName, testName, 0).Tag = tabContentControl;
            else
                MessageBox.Show("Test already exists in the list.", "Test already exists.",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void chooseFileNameButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Title = "Choose a file name where the results will be saved...";
            saveFileDialog.Filter = _fileNameFilters[outputMethodCombo.SelectedItem.ToString()];
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _orchestrator.OutputFileName = fileNameText.Text =
                fileNameText.ToolTipText = saveFileDialog.FileName;
        }

        private void outputMethodCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _orchestrator.OutputMethod =
                (OutputMethod) Enum.Parse(typeof (OutputMethod), outputMethodCombo.SelectedItem.ToString());
        }

        private void fileNameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                ShowResults();
        }

        private void ShowResults()
        {
            if (_orchestrator.OutputMethod == OutputMethod.Excel)
                Process.Start("Excel.exe", fileNameText.Text);
            else
                Process.Start("Notepad.exe", fileNameText.Text);
        }

        private void btnRunTests_Click(object sender, EventArgs e)
        {
            if (selectedTests.Items.Count == 0)
            {
                MessageBox.Show("No tests selected for run.\nAdd some tests, then try again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] sources = new string[selectedTests.Items.Count];
            for (int i = 0; i < selectedTests.Items.Count; ++i)
            {
                sources[i] = (selectedTests.Items[i].Tag as TabContentControl).TextBox.Text;
            }
            RunAndShowResults(sources);
        }

        private void RunAndShowResults(params string[] sources)
        {
            if (_runSetupForm.ShowDialog() != DialogResult.OK)
                return;

            _orchestrator.CompileMode = _runSetupForm.CompileMode;
            _orchestrator.NumberOfInnerIterations = _runSetupForm.NumberOfInnerIterations;
            _orchestrator.NumberOfOuterIterations = _runSetupForm.NumberOfOuterIterations;

            _orchestrator.CodeTemplateTexts.Clear();
            Array.ForEach(sources, delegate(string source) { _orchestrator.CodeTemplateTexts.Add(source); });
            _orchestrator.OutputFileName = fileNameText.Text;

            EnableDisableRunButtons(false);
            MethodInvoker invoker = _orchestrator.Orchestrate;
            IAsyncResult asyncResult = null;    // Otherwise, EndInvoke errs 'might not be initialized...'
            asyncResult = invoker.BeginInvoke(
                delegate
                {
                    try
                    {
                        invoker.EndInvoke(asyncResult);
                        if (MessageBox.Show(
                                "Operation complete.  Would you like to view the results file?\n" +
                                fileNameText.Text, "Results are available", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            ShowResults();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("An error occurred during invocation:\n" + ex.Message, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Invoke(new MethodInvoker(
                                        delegate
                                        {
                                            EnableDisableRunButtons(true);
                                            testsProgressBar.Value = 0;
                                        }));
                    }
                }, null);

            //_orchestrator.Orchestrate();
        }

        private void EnableDisableRunButtons(bool isEnabled)
        {
            btnRunTests.Enabled = isEnabled;
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    TabContentControl tabContentControl = control as TabContentControl;
                    if (tabContentControl == null)
                        continue;
                    tabContentControl.RunEnabled = isEnabled;
                }
            }
        }

        private void orchestrator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStrip.Invoke(new MethodInvoker(delegate
            {
                testsProgressBar.Value = (int) e.TotalMeasurementPercentComplete;
            }));
        }

        private void itemRunThis_Click(object sender, EventArgs e)
        {
            if (selectedTests.SelectedItems.Count == 0) return;
            
            ListViewItem selectedItem = selectedTests.SelectedItems[0];
            RunAndShowResults((selectedItem.Tag as TabContentControl).TextBox.Text);
        }

        private void itemDeleteThis_Click(object sender, EventArgs e)
        {
            if (selectedTests.SelectedItems.Count == 0) return;

            selectedTests.Items.Remove(selectedTests.SelectedItems[0]);
        }

        private void itemDeleteAll_Click(object sender, EventArgs e)
        {
            selectedTests.Items.Clear();
        }

        private void addReferenceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Assemblies (*.dll)|*.dll|Executables (*.exe)|*.exe";
            openFileDialog.Title = "Browse for the assembly";
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            if (!_orchestrator.AdditionalReferences.Contains(openFileDialog.FileName))
                _orchestrator.AdditionalReferences.Add(openFileDialog.FileName);
        }

        private void saveTabGroupButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = @"C:\Temp\PerformanceHarness";
            folderBrowserDialog.Description = "Choose the folder that will contain the tabs.\nA new file will be created for each tab.";
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            if (!Directory.Exists(folderBrowserDialog.SelectedPath))
                Directory.CreateDirectory(folderBrowserDialog.SelectedPath);
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                TabContentControl control = tabPage.Controls[0] as TabContentControl;
                Debug.Assert(control != null);
                File.WriteAllText(
                    Path.Combine(folderBrowserDialog.SelectedPath, Path.ChangeExtension(control.TestName, ".txt")),
                    control.TextBox.Text);
            }
        }

        private void loadTabGroupButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @"C:\Temp\PerformanceHarness";
            folderBrowserDialog.Description = "Choose the folder that contains the tabs.";
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.txt"))
            {
                AddNewTab(Path.GetFileNameWithoutExtension(file), File.ReadAllText(file));
            }
        }
    }
}
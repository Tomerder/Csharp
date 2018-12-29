using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PerformanceHarness;

namespace PerformanceHarnessGui
{
    public partial class TabContentControl : UserControl
    {
        public TabContentControl()
        {
            InitializeComponent();
        }

        private string _testName;

        public event ContentCommandEventHandler AddToTests;
        public event ContentCommandEventHandler Run;
        public event ContentCommandEventHandler Duplicate;
        public event ContentCommandEventHandler Rename;
        public event ContentCommandEventHandler Delete;

        public string TestName
        {
            get { return _testName; }
            set { _testName = value; }
        }

        public RichTextBox TextBox
        {
            get { return contentTextBox; }
        }

        public bool RunEnabled
        {
            get { return runThisTabButton.Enabled; }
            set { runThisTabButton.Enabled = value; }
        }

        private void addTabToRunsButton_Click(object sender, EventArgs e)
        {
            if (AddToTests != null)
                AddToTests(this, new ContentCommandEventArgs(contentTextBox));
        }

        private void runThisTabButton_Click(object sender, EventArgs e)
        {
            if (Run != null)
                Run(this, new ContentCommandEventArgs(contentTextBox));
        }

        private void duplicateTabButton_Click(object sender, EventArgs e)
        {
            if (Duplicate != null)
                Duplicate(this, new ContentCommandEventArgs(contentTextBox));
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            if (Rename != null)
                Rename(this, new ContentCommandEventArgs(contentTextBox));
        }

        private void loadCodeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose a file that contains code";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                contentTextBox.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Choose a file name to save your code to";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog.FileName, contentTextBox.Text);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (Delete != null)
                Delete(this, new ContentCommandEventArgs(contentTextBox));
        }
    }

    public class ContentCommandEventArgs : EventArgs
    {
        private RichTextBox _textBox;

        public ContentCommandEventArgs(RichTextBox textBox)
        {
            _textBox = textBox;
        }

        public RichTextBox TextBox
        {
            get { return _textBox; }
        }
    }
    
    public delegate void ContentCommandEventHandler(object sender, ContentCommandEventArgs e);
}

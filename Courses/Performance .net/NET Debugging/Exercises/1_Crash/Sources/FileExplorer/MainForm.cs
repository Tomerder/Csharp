using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FileExplorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static readonly string RootPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        private void MainForm_Load(object sender, EventArgs e)
        {
            TreeNode root = treeView1.Nodes.Add(RootPath);
            RecursivelyFillTreeview(root, RootPath);
        }

        private void RecursivelyFillTreeview(TreeNode node, string path)
        {
            RecursivelyFillTreeview(node, path);
            foreach (string subFolder in Directory.GetDirectories(path))
            {
                TreeNode child = node.Nodes.Add(subFolder, Path.GetFileName(subFolder));
                RecursivelyFillTreeview(child, subFolder);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Node.Name))
            {
                listBox1.Items.Clear();
                foreach (string file in Directory.GetFiles(e.Node.Name))
                {
                    listBox1.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string file = listBox1.SelectedItem as string;
            if (String.IsNullOrEmpty(file))
                return;

            file = Path.Combine(treeView1.SelectedNode.Name, file);

            Process process = Process.Start(@"C:\windows\notepad.exe", file);
            process.WaitForExit();
        }
    }
}

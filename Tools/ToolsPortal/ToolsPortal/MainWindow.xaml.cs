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
using System.IO;
using UtilsLib;
using System.Threading;

namespace ToolsPortal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*----------------------------------------------------------------------------*/

        Thread m_threadExecute;

        Dictionary<string, Tool> g_toolsMap;

        const int COL_ICON = 0;
        const int COL_LABEL = 1;
        const int COL_EXE_FILE = 2;
        const int COL_README_FILE = 3;

        const int ICON_DIM = 32;
        /*----------------------------------------------------------------------------*/

        public MainWindow()
        {
            InitializeComponent();

            //get data from XML config file into CSCIs map          
            bool success = ConfigFileLogic.GetConfigData(out g_toolsMap);
            if (!success)
            {
                //exit 
                Close();
            }

            //build screen according to xml file (Tools list)
            success = BuildScreen(g_toolsMap);
            if (!success)
            {
                //exit 
                Close();
            }
        }

        /*----------------------------------------------------------------------------*/

        private bool BuildScreen(Dictionary<string, Tool> _toolsMap)
        {
            int toolsCount = 0;

            //iterate through tools map
            foreach (Tool tool in _toolsMap.Values)
            {
                //create raw
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(45);
                grid1.RowDefinitions.Add(gridRow);

                //create icon 
                if (tool.IconFilePath != "")
                {
                    try
                    {
                        BitmapImage bitmapImg = new BitmapImage();
                        bitmapImg.BeginInit();
                        bitmapImg.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImg.UriSource = new Uri(tool.IconFilePath);
                        bitmapImg.EndInit();

                        Image img = new Image();                       
                        img.Source = bitmapImg;
                        img.VerticalAlignment = VerticalAlignment.Top;
                        img.Height = ICON_DIM;
                        img.Width = ICON_DIM;                        

                        Grid.SetRow(img, toolsCount);
                        Grid.SetColumn(img, COL_ICON);
                        grid1.Children.Add(img);
                    }
                    catch
                    {
                        //img failed
                    }
                }

                //create label - tool name
                Label label = new Label();
                label.Name = "label" + toolsCount.ToString();
                label.Content = tool.Name;
                label.ToolTip = tool.Author;
                
                Grid.SetRow(label, toolsCount);
                Grid.SetColumn(label, COL_LABEL);
                grid1.Children.Add(label);

                //create button - execute tool (tool icon on button)        
                if (tool.ExeFilePath != "")
                {
                    Button buttonExe = new Button();
                    buttonExe.Name = "buttonExe" + toolsCount.ToString();
                    buttonExe.Click += Button_Click; 
                    buttonExe.Tag = tool.ExeFilePath;
                    buttonExe.Width = 100;
                    buttonExe.Height = 30;
                    buttonExe.VerticalAlignment = VerticalAlignment.Top;
                    buttonExe.Content = "Execute";

                    Grid.SetRow(buttonExe, toolsCount);
                    Grid.SetColumn(buttonExe, COL_EXE_FILE);
                    grid1.Children.Add(buttonExe);
                }

                //create button - readme file
                if (tool.ReadmeFilePath != "")
                {
                    Button buttonReadme = new Button();
                    buttonReadme.Name = "buttonReadme" + toolsCount.ToString();
                    buttonReadme.Click += Button_Click;
                    buttonReadme.Tag = tool.ReadmeFilePath;
                    buttonReadme.Width = 100;
                    buttonReadme.Height = 30;
                    buttonReadme.VerticalAlignment = VerticalAlignment.Top;
                    buttonReadme.Content = "Readme";

                    Grid.SetRow(buttonReadme, toolsCount);
                    Grid.SetColumn(buttonReadme, COL_README_FILE);
                    grid1.Children.Add(buttonReadme);
                }
                               
                toolsCount++;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {                 
                Button clickedbutton = sender as Button;
                string exeFilePath = clickedbutton.Tag.ToString();

                //run tool from seperate thread 
                m_threadExecute = new Thread(() => ExecuteTool(exeFilePath));
                m_threadExecute.IsBackground = true;
                m_threadExecute.Start();   
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        /*----------------------------------------------------------------------------*/

        private void ExecuteTool(string _toolPath)
        {
            try
            {
                string _fromPath;
                UtilsLib.UtilsLib.RetrievePathFromFullFilePath(_toolPath, out _fromPath);
                UtilsLib.UtilsLib.ExecuteCommand(_toolPath, "", _fromPath);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        /*----------------------------------------------------------------------------*/
    }
}

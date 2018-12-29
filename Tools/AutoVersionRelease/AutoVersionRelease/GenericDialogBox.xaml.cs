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
using System.Windows.Shapes;

namespace AutoVersionRelease
{
    /// <summary>
    /// Interaction logic for GenericDialogBox.xaml
    /// </summary>
    public partial class GenericDialogBox : Window
    {
        public GenericDialogBox()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        public string GetUserInput
        {
            get { return textBoxInputFromUser.Text; }
        }

    }
}

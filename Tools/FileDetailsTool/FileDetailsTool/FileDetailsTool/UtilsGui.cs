﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FileDetailsTool
{
    public static class UtilsGui
    {
        /*----------------------------------------------------------------------------*/
        //Enums

        public enum MessageType { NONE, SUCCESS, ERROR, HIGHLIGHT };

        /*----------------------------------------------------------------------------*/
        //delegations

        delegate void AppendMessageToPanelDelegate(MainWindow _mainWindow, RichTextBox _richTextBox, string _text, MessageType _type);
        delegate void ClearMessagesPanelDelegate(MainWindow _mainWindow, RichTextBox _messagesPanel);
        delegate string GetTextBoxDelegate(MainWindow _mainWindow, TextBox _textBox);
        delegate string GetComboBoxDelegate(MainWindow _mainWindow, ComboBox _comboBox);
        delegate string GetMessagesFromPanelDelegate(MainWindow _mainWindow, RichTextBox _messagesPanel);
        delegate void SetButtonEnableDelegate(MainWindow _mainWindow, Button _buttonToSet, bool _isEnable);
        delegate void SetMenuItemEnableDelegate(MainWindow _mainWindow, MenuItem _menuItem, bool _isEnable);
        delegate void SetTextBoxEnableDelegate(MainWindow _mainWindow, TextBox _textBox, bool _isEnable);
        delegate void SetComboBoxEnableDelegate(MainWindow _mainWindow, ComboBox _comboBox, bool _isEnable);
        delegate void SetVisibilityDelegate(MainWindow _mainWindow, Control _controlItem, bool _isEnable);        

        /*-----------------------------------------------------------------------------------*/

        //append message to rich text box (messages pannel)
        static public void AppendMessageToPanel(MainWindow _mainWindow, RichTextBox _messagesPanel, string _msg, MessageType _type)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new AppendMessageToPanelDelegate(AppendMessageToPanel), _mainWindow, _messagesPanel, _msg, _type);
            }
            else
            {
                TextRange rangeOfText = new TextRange(_messagesPanel.Document.ContentEnd, _messagesPanel.Document.ContentEnd);
                rangeOfText.Text = _msg + "\r";

                switch (_type)
                {
                    case MessageType.SUCCESS:
                        //GREEN
                        rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
                        rangeOfText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                        break;
                    case MessageType.ERROR:
                        //RED
                        rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                        rangeOfText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                        break;
                    case MessageType.HIGHLIGHT:
                        //BOLD 
                        rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Cyan);
                        rangeOfText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.ExtraBold);
                        break;
                    default:
                        rangeOfText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                        rangeOfText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                        //BLACK
                        break;
                }

                //textBoxMessages.AppendText(_msg + "\r" );//Environment.NewLine);
                _messagesPanel.ScrollToEnd();

                //Logger.Instance.Log(Logger.LOG_MODE_ENUM.ALL, _msg);
            }
        }

        /*----------------------------------------------------------------------------*/
        static public void ClearMessagesPanel(MainWindow _mainWindow, RichTextBox _messagesPanel)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new ClearMessagesPanelDelegate(ClearMessagesPanel), _mainWindow, _messagesPanel);
            }
            else
            {
                TextRange rangeOfText = new TextRange(_messagesPanel.Document.ContentStart, _messagesPanel.Document.ContentEnd);
                rangeOfText.Text = "";
            }
        }

        /*----------------------------------------------------------------------------*/

        static public string GetTextBox(MainWindow _mainWindow, TextBox _textBox)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                return (string)_mainWindow.Dispatcher.Invoke(new GetTextBoxDelegate(GetTextBox), _mainWindow, _textBox);
            }
            else
            {
                return _textBox.Text;
            }
        }

        /*----------------------------------------------------------------------------*/
        static public string GetComboBox(MainWindow _mainWindow, ComboBox _comboBox)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                return (string)_mainWindow.Dispatcher.Invoke(new GetComboBoxDelegate(GetComboBox), _mainWindow, _comboBox);
            }
            else
            {
                return _comboBox.Text;
            }
        }
        /*----------------------------------------------------------------------------*/
        static public void SetButtonEnable(MainWindow _mainWindow, Button _buttonToSet, bool _isEnable)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new SetButtonEnableDelegate(SetButtonEnable), _mainWindow, _buttonToSet, _isEnable);
            }
            else
            {
                //enable/disable button
                _buttonToSet.IsEnabled = _isEnable;
            }
        }

        /*----------------------------------------------------------------------------*/

        static public string GetMessagesFromPanel(MainWindow _mainWindow, RichTextBox _messagesPanel)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                return (string)_mainWindow.Dispatcher.Invoke(new GetMessagesFromPanelDelegate(GetMessagesFromPanel), _mainWindow, _messagesPanel);
            }
            else
            {
                TextRange textRange = new TextRange(
                                    _messagesPanel.Document.ContentStart,
                                    _messagesPanel.Document.ContentEnd
                                                                        );
                return textRange.Text;
            }
        }

        /*----------------------------------------------------------------------------*/

        static public bool GetTextFromDefaultFileOrFromUser(string _defaultFilePath, out string _txtFromFile)
        {
            _txtFromFile = "";
            bool isDefaultFileSuccess = true;
        
            //try to get file from default path 
            if (!_defaultFilePath.Equals(""))
            {
                try
                {
                    _txtFromFile = File.ReadAllText(_defaultFilePath);
                }
                catch
                {
                    //didnt get from default file
                    isDefaultFileSuccess = false;
                }
            }

            if (isDefaultFileSuccess)
            {
                return true;
            }
            else
            {

                //try to get file from user
                string title = "Please select file : " + _defaultFilePath;
                string filename;
                bool success = GetFileFromUser(title , out filename);
                if (!success)
                {
                    return false;
                }

                try
                {
                    _txtFromFile = File.ReadAllText(filename);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/
        /*
        static public bool GetFileFromUser(string title, out string _fileName)
        {
            _fileName = "";
            Stream stream = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = title;
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            try
            {
                //if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = openFileDialog1.OpenFile()) == null)
                    {
                        return false;
                    }
                }

                FileStream fs = stream as FileStream;

                _fileName = fs.Name;
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }
        */

        static public bool GetFileFromUser(string title, out string _fileName, string _defaultFileType = "txt")
        {
            bool success = false;
            _fileName = "";

            try
            {
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension 
                dlg.DefaultExt = _defaultFileType;
                dlg.Filter = _defaultFileType + " file" + "|*." + _defaultFileType + "|All Files (*.*)|*.*";
                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    _fileName = filename;
                    success = true; 
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return success;
        }

        /*-----------------------------------------------------------------------------------*/
       
        static public bool MessageBoxYesNo(string _msgTitle, string _msgText)
        {
            string msgText = _msgText;  
            string title = _msgTitle;  
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult msgBoxResult = MessageBox.Show(msgText, title, btnMessageBox, icnMessageBox);

            bool isYesChoosen = (msgBoxResult == MessageBoxResult.Yes);

            return isYesChoosen;
        }

        /*-----------------------------------------------------------------------------------*/
/*
        static public string DialogBox(string _title, string _text, string _dialogDefault)
        {
            string inputFromUser = Microsoft.VisualBasic.Interaction.InputBox(_text, _title, _dialogDefault);
            
            return inputFromUser;
        }
*/        
        /*-----------------------------------------------------------------------------------*/
/*
        static public bool GenericDialogBox() //string _title, string _text, string _dialogDefault, out string _userMessage)
        {
            //_userMessage = "";

            GenericDialogBox dialogBox = new GenericDialogBox();
            dialogBox.Show();

            return true;
        }
*/
        /*-----------------------------------------------------------------------------------*/

        internal static void SetMenuItemEnable(MainWindow _mainWindow, MenuItem _menuItem, bool _isEnable)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new SetMenuItemEnableDelegate(SetMenuItemEnable), _mainWindow, _menuItem, _isEnable);
            }
            else
            {
                //enable/disable button
                _menuItem.IsEnabled = _isEnable;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        /*-----------------------------------------------------------------------------------*/

        internal static void SetTextBoxEnable(MainWindow _mainWindow, TextBox _textBox, bool _isEnable)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new SetTextBoxEnableDelegate(SetTextBoxEnable), _mainWindow, _textBox, _isEnable);
            }
            else
            {
                //enable/disable button
                _textBox.IsEnabled = _isEnable;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        /*-----------------------------------------------------------------------------------*/

        internal static void SetComboBoxEnable(MainWindow _mainWindow, ComboBox _comboBox, bool _isEnable)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new SetComboBoxEnableDelegate(SetComboBoxEnable), _mainWindow, _comboBox, _isEnable);
            }
            else
            {
                //enable/disable button
                _comboBox.IsEnabled = _isEnable;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        internal static void SetComboBoxValuesFromEnum<ENUM>(ComboBox _comboBox, ENUM _enum)
        {
            var values = Enum.GetValues(typeof(ENUM));

            foreach (var value in values)
            {
                _comboBox.Items.Add(value);           
            }
        }

        /*-----------------------------------------------------------------------------------*/

        internal static void SetVisibility(MainWindow _mainWindow, Control _item, bool _isVisible)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!_mainWindow.Dispatcher.CheckAccess())
            {
                _mainWindow.Dispatcher.Invoke(new SetVisibilityDelegate(SetVisibility), _mainWindow, _item, _isVisible);
            }
            else
            {
                Visibility vis = Visibility.Visible;
                if (!_isVisible)
                {
                    vis = Visibility.Hidden;
                }

                _item.Visibility = vis;              
            }
        }

        /*-----------------------------------------------------------------------------------*/
    }
}
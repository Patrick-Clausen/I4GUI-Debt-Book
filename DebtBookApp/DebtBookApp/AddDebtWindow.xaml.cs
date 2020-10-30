using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DebtBookApp
{
    /// <summary>
    /// Interaction logic for AddDebtWindow.xaml
    /// </summary>
    public partial class AddDebtWindow : Window
    {
        public AddDebtWindow()
        {
            InitializeComponent();
        }

        //Inspired by https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TbxDebt_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}

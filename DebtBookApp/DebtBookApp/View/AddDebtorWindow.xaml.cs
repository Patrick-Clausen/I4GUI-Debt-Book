using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for AddDebtorWindow.xaml
    /// </summary>
    public partial class AddDebtorWindow : Window
    {
        public AddDebtorWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AddDebtorWindowViewModel;
            if (vm.IsValid)
                DialogResult = true;
            else
                MessageBox.Show("Enter values for name and initial value", "Missing data");
        }
    }
}

using System;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DebtBookApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<Debtor> debtors;

        public ObservableCollection<Debtor> Debtors
        {
            get
            {
                return debtors;
            }
            set
            {
                debtors = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>();
        }

        private ICommand addDebtorButtonCommand;
        public ICommand AddDebtorButtonPressedCommand
        {
            get
            {
                return addDebtorButtonCommand ??
                       (addDebtorButtonCommand = new DelegateCommand(AddDebtorButtonPressedCommandHandler));
            }
        }

        private void AddDebtorButtonPressedCommandHandler()
        {
            Debtor debtorUnderCreation = new Debtor();
            
            AddDebtorWindow addDebtorWindow = new AddDebtorWindow();
            AddDebtorWindowViewModel addDebtorWindowViewModel = new AddDebtorWindowViewModel(ref debtorUnderCreation, ref addDebtorWindow);
            addDebtorWindow.DataContext = addDebtorWindowViewModel;
            addDebtorWindow.Owner = Application.Current.MainWindow;

            if (addDebtorWindow.ShowDialog() == true)
            {
                debtors.Add(debtorUnderCreation);
            }
        }

        private ICommand listViewItemDoubleClickedCommand;
        public ICommand ListViewItemDoubleClickedCommand
        {
            get
            {
                return listViewItemDoubleClickedCommand ??
                       (listViewItemDoubleClickedCommand = new DelegateCommand<Debtor>(ListViewItemDoubleClickedCommandHandler));
            }
        }

        private void ListViewItemDoubleClickedCommandHandler(Debtor doubleClickedDebtor)
        {
            Debtor copiedDebtor = new Debtor(doubleClickedDebtor);
            
            AddDebtWindow addDebtWindow = new AddDebtWindow();
            AddDebtWindowViewModel addDebtWindowViewModel = new AddDebtWindowViewModel(ref copiedDebtor, ref addDebtWindow);
            addDebtWindow.DataContext = addDebtWindowViewModel;
            addDebtWindow.Owner = Application.Current.MainWindow;

            if (addDebtWindow.ShowDialog() == true)
            {
                doubleClickedDebtor.Debts = copiedDebtor.Debts;
                doubleClickedDebtor.Name = copiedDebtor.Name;
            }
        }
    }
}

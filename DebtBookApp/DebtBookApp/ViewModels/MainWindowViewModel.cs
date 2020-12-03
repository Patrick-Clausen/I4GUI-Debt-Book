using System;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace DebtBookApp
{
    public class MainWindowViewModel : BindableBase
    {

        private string fileName = string.Empty;

        private ObservableCollection<Debtor> debtors;

        public ObservableCollection<Debtor> Debtors
        {
            get
            {
                return debtors;
            }
            set
            {
                SetProperty(ref debtors, value);
            }
        }

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>();
            debtors.CollectionChanged += DebtorCollectionChanged;
        }

        private void DebtorCollectionChanged(object sender, EventArgs e)
        {
            debtors = new ObservableCollection<Debtor>(debtors.OrderBy(debtors => debtors.Name));
            debtors.CollectionChanged += DebtorCollectionChanged;
            RaisePropertyChanged("Debtors");
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
            AddDebtWindow addDebtWindow = new AddDebtWindow();
            AddDebtWindowViewModel addDebtWindowViewModel = new AddDebtWindowViewModel(ref doubleClickedDebtor, ref addDebtWindow);
            addDebtWindow.DataContext = addDebtWindowViewModel;
            addDebtWindow.Owner = Application.Current.MainWindow;

            addDebtWindow.ShowDialog();
        }

        private ICommand newMenuCommand;

        public ICommand NewMenuCommand
        {
            get
            {
                return newMenuCommand ??
                       (newMenuCommand = new DelegateCommand(NewMenuCommandHandler));
            }
        }

        public void NewMenuCommandHandler()
        {
            debtors = new ObservableCollection<Debtor>();
            fileName = string.Empty;
            RaisePropertyChanged("debtors");
        }

        private ICommand openMenuCommand;

        public ICommand OpenMenuCommand
        {
            get
            {
                return openMenuCommand ??
                       (openMenuCommand = new DelegateCommand(OpenMenuCommandHandler));
            }
        }

        public async void OpenMenuCommandHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Debt file (*.dbt)|*.dbt";
            if (openFileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(@openFileDialog.FileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                debtors = formatter.Deserialize(fs) as ObservableCollection<Debtor>;
                fs.Close();
                RaisePropertyChanged("Debtors");
                fileName = openFileDialog.FileName;
            }
            ((DelegateCommand)saveMenuCommand).RaiseCanExecuteChanged();
        }

        private ICommand saveMenuCommand;

        public ICommand SaveMenuCommand
        {
            get
            {
                return saveMenuCommand ?? (saveMenuCommand = new DelegateCommand(
                        SaveMenuCommandHandler, () => (!string.IsNullOrWhiteSpace(fileName))
                    ));
            }
        }

        private async void SaveMenuCommandHandler()
        {
            FileStream fs = new FileStream(fileName, FileMode.Truncate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Debtors);
            fs.Close();
        }

        private ICommand saveAsMenuCommand;

        public ICommand SaveAsMenuCommand
        {
            get
            {
                return saveAsMenuCommand ??
                       (saveAsMenuCommand = new DelegateCommand(SaveAsMenuCommandHandler));
            }
        }

        public async void SaveAsMenuCommandHandler()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Debt file (*.dbt)|*.dbt";
            if (saveFileDialog.ShowDialog() == true)
            {

                FileStream fs = new FileStream(@saveFileDialog.FileName, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, Debtors);
                fs.Close();
                fileName = saveFileDialog.FileName;
            }
            ((DelegateCommand)saveMenuCommand).RaiseCanExecuteChanged();
        }


        private ICommand exitMenuCommand;

        public ICommand ExitMenuCommand
        {
            get
            {
                return exitMenuCommand ??
                       (exitMenuCommand = new DelegateCommand(ExitMenuCommandHandler));
            }
        }
        public void ExitMenuCommandHandler()
        {
            Application.Current.MainWindow.Close();
        }
    }
}

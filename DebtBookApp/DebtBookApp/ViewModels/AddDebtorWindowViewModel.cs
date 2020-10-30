using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBookApp
{
    public class AddDebtorWindowViewModel : BindableBase
    {
        //NOTE ---- AddDebtorWindowViewModel is instantiated and bound to the DataContext by MainWindowViewModel, so no need to do so in XAML code
        public bool
            DebtorCreated =
                false; //Set this to true if debtor is saved correctly, false if canceled or improperly saved - Bound to window, so MainWindow is properly notified and can add if necessary

        private AddDebtorWindow _window;

        public AddDebtorWindowViewModel(ref Debtor debtorToCreate, ref AddDebtorWindow window)
        {
            DebtorToCreate = debtorToCreate;
            _window = window;
        }

        private Debtor _debtorToCreate;

        public Debtor DebtorToCreate
        {
            get { return _debtorToCreate; }
            set { SetProperty(ref _debtorToCreate, value); }
        }

        private double initialValue;
        private string initialValueString;

        public string InitialValue
        {
            get
            {
                return initialValueString;
            }
            set
            {
                try
                {
                    initialValue = Double.Parse(value);
                    SetProperty(ref initialValueString, value);
                }
                catch (Exception)
                {
                }

            }
        }


        public bool IsValid
        {
            get
            {
                return !(string.IsNullOrWhiteSpace(DebtorToCreate.Name));
            }
        }


        #region Commands

        ICommand _SaveButtonCommand;

        public ICommand SaveButtonCommand
        {
            get
            {
                return _SaveButtonCommand ?? (_SaveButtonCommand = new DelegateCommand(
                        SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                    .ObservesProperty(() => DebtorToCreate.Name)
                    .ObservesProperty(() => DebtorToCreate.Debt));
            }
        }

        private void SaveBtnCommand_Execute()
        {
            if (InitialValue != string.Empty)
            {
                DebtorToCreate.Debts.Add(new Debt(initialValue, DateTime.Now));
            }

            _window.DialogResult = true;
            _window.Close();
        }

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        private ICommand _CancelButtonCommand;

        public ICommand CancelButtonCommand
        {
            get
            {
                return _CancelButtonCommand ?? (_CancelButtonCommand = new DelegateCommand(CancelButtonCommandHandler));
            }
        }

        private void CancelButtonCommandHandler()
        {
            _window.DialogResult = false;
            _window.Close();
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Text;
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

        public AddDebtorWindowViewModel(ref Debtor debtorToCreate)
        {
            DebtorToCreate = debtorToCreate;
        }

        private Debtor _debtorToCreate;

        public Debtor DebtorToCreate
        {
            get { return _debtorToCreate; }
            set { SetProperty(ref _debtorToCreate, value); }
        }


        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(DebtorToCreate.Name))
                    isValid = false;
                if (DebtorToCreate.Debt <= 0)
                    isValid = false;

                return isValid;
            }
        }


        #region Commands

        DelegateCommand _SaveBtnCommand;

        public DelegateCommand SaveBtnCommand
        {
            get
            {
                return _SaveBtnCommand ?? (_SaveBtnCommand = new DelegateCommand(
                        SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                    .ObservesProperty(() => DebtorToCreate.Name)
                    .ObservesProperty(() => DebtorToCreate.Debt));
            }
        }

        private void SaveBtnCommand_Execute()
        {
            DebtorCreated = true;
        }

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        #endregion

    }
}
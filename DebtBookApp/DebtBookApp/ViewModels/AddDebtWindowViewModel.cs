using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace DebtBookApp
{
    public class AddDebtWindowViewModel : BindableBase
    {
        //NOTE ---- AddDebtWindowViewModel is instantiated and bound to the DataContext by MainWindowViewModel, so no need to do so in XAML code
        public Debtor DebtorToModify { get; set; }
        private Debt _currentDebt;

        public Debt CurrentDebt
        {
            get
            {
                return _currentDebt;
            }
            set
            {
                SetProperty(ref _currentDebt, value);
            }
        }

        private AddDebtWindow _window;
        public AddDebtWindowViewModel(ref Debtor debtorToModify, ref AddDebtWindow window)
        {
            CurrentDebt = new Debt();
            DebtorToModify = debtorToModify;
            _window = window;
        }

        private ICommand _closeButtonPressedCommand;

        public ICommand CloseButtonPressedCommand
        {
            get
            {
                return _closeButtonPressedCommand ??
                       (_closeButtonPressedCommand = new DelegateCommand(CloseButtonPressedCommandHandler));
            }
        }

        private void CloseButtonPressedCommandHandler()
        {
            _window.Close();
        }

        private ICommand _addDebtButtonPressedCommand;

        public ICommand AddDebtButtonPressedCommand
        {
            get
            {
                return _addDebtButtonPressedCommand ??
                       (_addDebtButtonPressedCommand = new DelegateCommand(AddDebtButtonPressedCommandHandler));
            }
        }

        private void AddDebtButtonPressedCommandHandler()
        {
            DebtorToModify.Debts.Add(CurrentDebt);
            CurrentDebt = new Debt();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DebtBookApp
{
    public class AddDebtWindowViewModel
    {
        //NOTE ---- AddDebtWindowViewModel is instantiated and bound to the DataContext by MainWindowViewModel, so no need to do so in XAML code
        public bool DebtorModified = false;//Set this to true if debtor is saved correctly, false if canceled or improperly saved - Bound to window, so MainWindow is properly notified and can add if necessary
        public Debtor DebtorToModify;
        public AddDebtWindowViewModel(ref Debtor debtorToModify)
        {
            DebtorToModify = debtorToModify;
        }
    }
}

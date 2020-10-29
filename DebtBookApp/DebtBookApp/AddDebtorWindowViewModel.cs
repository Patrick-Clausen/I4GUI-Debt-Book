using System;
using System.Collections.Generic;
using System.Text;

namespace DebtBookApp
{
    public class AddDebtorWindowViewModel
    {
        //NOTE ---- AddDebtorWindowViewModel is instantiated and bound to the DataContext by MainWindowViewModel, so no need to do so in XAML code
        public bool DebtorCreated = false;//Set this to true if debtor is saved correctly, false if canceled or improperly saved - Bound to window, so MainWindow is properly notified and can add if necessary
        public Debtor DebtorToCreate;
        public AddDebtorWindowViewModel(ref Debtor debtorToCreate)
        {
            DebtorToCreate = debtorToCreate;
        }
    }
}

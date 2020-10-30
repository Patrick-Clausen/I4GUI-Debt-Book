using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Prism.Mvvm;

namespace DebtBookApp
{
    public class Debtor : BindableBase
    {

        public Debtor()
        {
            debts = new ObservableCollection<Debt>();
            debts.CollectionChanged += CollectionChangedHandler;
        }

        public Debtor(Debtor debtorToCopy)
        {
            //Copy constructor
            debts = new ObservableCollection<Debt>(debtorToCopy.Debts);
            Name = debtorToCopy.Name;
            debts.CollectionChanged += CollectionChangedHandler;
        }

        public void CollectionChangedHandler(object sender, EventArgs e)
        {
            //Sort, resubscribe and raise property changes so list and total are updated
            debts = new ObservableCollection<Debt>(debts.OrderBy(debts => debts.Date).ToList());
            debts.CollectionChanged += CollectionChangedHandler;
            RaisePropertyChanged("debts");
            RaisePropertyChanged("Debt");
        }

        private ObservableCollection<Debt> debts;
        public ObservableCollection<Debt> Debts
        {
            get
            {
                return debts;
            }
            set
            {
                SetProperty(ref debts, value);
            }
        }

        public double Debt
        {
            get
            {
                double sum = 0;
                foreach (Debt debt in debts)
                {
                    sum += debt.Amount;
                }

                return sum;
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }
    }
}

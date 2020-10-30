using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DebtBookApp
{
    public class Debtor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Debtor()
        {
            debts = new ObservableCollection<Debt>();
        }

        public Debtor(Debtor debtorToCopy)
        {
            //Copy constructor
            Debts = new ObservableCollection<Debt>(debtorToCopy.Debts);
            Name = debtorToCopy.Name;
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
                debts = value;
                OnPropertyChanged();
                //Debt changes along with Debts
                OnPropertyChanged("Debt");
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
                name = value;
                OnPropertyChanged();
            }
        }
    }
}

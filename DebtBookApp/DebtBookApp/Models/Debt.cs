using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DebtBookApp
{
    public class Debt : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Debt()
        {
            date = DateTime.Now;
        }
        public Debt(double initialValue, DateTime timestamp)
        {
            amount = initialValue;
            date = timestamp;
        }

        private double amount;
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                stringAmount = value.ToString();
                OnPropertyChanged();
            }
        }

        private string stringAmount;
        public string StringAmount
        {
            get
            {
                return stringAmount;
            }
            set
            {
                try
                {
                    amount = Double.Parse(value);
                }
                catch (Exception) { }

                stringAmount = value;
                OnPropertyChanged();
            }
        }


        private DateTime date;
        public string StringDate
        {
            get
            {
                return date.ToShortDateString();
            }

            set
            {
                try
                {
                    date = DateTime.Parse(value);
                }
                catch (Exception) { }
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
    }
}

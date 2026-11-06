using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.ViewModel
{
    public class IncomeListModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private decimal _value;
        private string _name;
        private DateTime _lastEdited;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    // When the value was changed
                    _lastEdited = DateTime.Now;
                    OnPropertyChanged();
                
                } 
            }
        }

        public DateTime LastEdited
        {
            get => _lastEdited;
            private set
            {
                _lastEdited = value;
                OnPropertyChanged();
            }
        }

        

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }
    }
}

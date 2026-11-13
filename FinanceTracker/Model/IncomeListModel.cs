using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FinanceTracker.ViewModel
{
    public class IncomeListModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private decimal _value;
        private string _name = string.Empty;
        private Brush? _color;
        private DateTime _lastEdited;

        public Brush? Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }
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
                    LastEdited = DateTime.Now;
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



        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

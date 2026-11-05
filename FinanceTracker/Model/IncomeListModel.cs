using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.ViewModel
{
    public class IncomeListModel 
    {
        private decimal _Value;
        private DateTime _lastEdited;
        public string Name { get; set; }
        public decimal Value
        {
            get => _Value;
            set
            {
               
            }
        }
           
    }
}

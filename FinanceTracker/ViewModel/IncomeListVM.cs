using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.ViewModel
{
    class IncomeListVM
    {
        public ObservableCollection<IncomeListModel> Incomes { get; set; }
        public IncomeListVM()
        {
            Incomes = new ObservableCollection<IncomeListModel>
            {
                new IncomeListModel { Name = "Job", Value = 1000 },
                new IncomeListModel { Name = "Freelancing", Value = 250 }
            };
        }
    }
}

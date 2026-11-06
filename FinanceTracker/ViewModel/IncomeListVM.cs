using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceTracker.ViewModel
{
    class IncomeListVM
    {
        public ObservableCollection<IncomeListModel> Incomes { get; set; }
        // Defining commands
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public IncomeListVM()
        {   
            Incomes = new ObservableCollection<IncomeListModel>
            {
                new IncomeListModel { Name = "Job", Value = 1000 },
                new IncomeListModel { Name = "Freelancing", Value = 250 }
            };
            // implementing commands
            AddCommand = new RelayCommand(ExecuteAdd);
            RemoveCommand = new RelayCommand<IncomeListModel>(ExecuteRemove);
        }

        
        private void ExecuteAdd()
        {
            Incomes.Add(new IncomeListModel
            {
                Name = "New Income",
                Value = 0
            });
        }
        private void ExecuteRemove(IncomeListModel itemToRemove)
        {
            if (itemToRemove != null && Incomes.Contains(itemToRemove))
            {
                Incomes.Remove(itemToRemove);
            }
        }

    }
}

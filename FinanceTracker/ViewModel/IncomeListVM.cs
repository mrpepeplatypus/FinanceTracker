using LiveCharts;
using LiveCharts.Wpf;
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
        public SeriesCollection PieChartIncome { get; set; }
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

            // Pie Chart
            PieChartIncome = new SeriesCollection();
            foreach (var element in Incomes)
            {
                PieChartIncome.Add(new PieSeries
                {
                    Title = element.Name,
                    Values = new ChartValues<decimal> { (decimal)element.Value },
                    DataLabels = true
                });
            }

            // implementing commands
            AddCommand = new RelayCommand(ExecuteAdd);
            RemoveCommand = new RelayCommand<IncomeListModel>(ExecuteRemove);
        }

        // Adding a list
        private void ExecuteAdd()
        {
            var newIncome = new IncomeListModel
            {
                Name = "New Income",
                Value = 1000
            };
            
            Incomes.Add(newIncome);
            // Pie Chart
            PieChartIncome.Add(new PieSeries
            {
                Title = newIncome.Name,
                Values = new ChartValues<decimal> { (decimal)newIncome.Value },
                DataLabels = true
            });
        }
        // Removing a list
        private void ExecuteRemove(IncomeListModel itemToRemove)
        {
            if (itemToRemove != null && Incomes.Contains(itemToRemove))
            {
                Incomes.Remove(itemToRemove);
            }
        }

    }
}

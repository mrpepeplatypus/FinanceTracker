using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Data;
using LiveCharts.Configurations;

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

            var income = new IncomeListModel();
            Incomes.Add(income);
            AddIncomeToChart(income);

            
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
            
            // Pie Chart
            Incomes.Add(newIncome);
            AddIncomeToChart(newIncome);
        }

        // PieChart method (to listen)
        private void AddIncomeToChart(IncomeListModel item)
        {
            var series = new PieSeries
            {
                Values = new ChartValues<IncomeListModel> { item },
                Configuration = Mappers.Pie<IncomeListModel>().Value(model => (double)model.Value),
                DataLabels = true,
                Tag = item
            };

            BindingOperations.SetBinding(series, PieSeries.TitleProperty, new Binding("Name")  { Source = item });

            PieChartIncome.Add(series);
        }

        private void RemoveIncomeToChart(IncomeListModel item)
        {
            var seriesToRemove = PieChartIncome
        .OfType<PieSeries>()
        .FirstOrDefault(s => ReferenceEquals(s.Tag, item) || (s.Values != null && s.Values.Cast<object>().Contains(item)));
            if (seriesToRemove != null)
            {
                BindingOperations.ClearAllBindings(seriesToRemove);
                PieChartIncome.Remove(seriesToRemove);
            }
        }
        // Removing a list
        private void ExecuteRemove(IncomeListModel itemToRemove)
        {
            if (itemToRemove != null && Incomes.Contains(itemToRemove))
            {
                Incomes.Remove(itemToRemove);
            }
            RemoveIncomeToChart(itemToRemove);
        }

    }
}

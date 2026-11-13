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
using System.Windows.Media;
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

        // Pallet (Brush)
        private static Brush Hex(string hex)
        {
            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(hex)!;
            brush.Freeze();
            return brush;
        }

        private readonly List<Brush> _colorPalette = new()
        {
            Hex("#2ECC71"), 
            Hex("#3498DB"), 
            Hex("#E74C3C"), 
            Hex("#F1C40F"), 
            Hex("#9B59B6"), 
            Hex("#E67E22"), 
            Hex("#1ABC9C"), 
            Hex("#E84393"), 
            Hex("#34495E"), 
            Hex("#00CEC9")  
        };

        // Taking Next Colors
        private Brush GetNextColor()
        {
            var usedColors = Incomes.Select(i => i.Color).Where(c => c != null).ToHashSet();
            var available = _colorPalette.FirstOrDefault(b => !usedColors.Contains(b));
            if (available != null)
                return available;

            return _colorPalette[Incomes.Count % _colorPalette.Count];
        }

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
                AddIncomeToChart(element);
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
                Value = 1000,
                Color = GetNextColor()
            };
            
            // Pie Chart
            Incomes.Add(newIncome);
            AddIncomeToChart(newIncome);
        }

        // PieChart method (to listen)
        private void AddIncomeToChart(IncomeListModel item)
        {
            if (item.Color == null)
            {
                item.Color = GetNextColor();
            }

            var series = new PieSeries
            {
                Values = new ChartValues<IncomeListModel> { item },
                Configuration = Mappers.Pie<IncomeListModel>().Value(model => (double)model.Value),
                DataLabels = true,
                Tag = item,
                Fill = item.Color // For Brush Color
            };

            BindingOperations.SetBinding(series, PieSeries.TitleProperty, new Binding("Name")  { Source = item });
            BindingOperations.SetBinding(series, PieSeries.FillProperty, new Binding("Color")  { Source = item });

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
                RemoveIncomeToChart(itemToRemove);
            }
        }

    }
}

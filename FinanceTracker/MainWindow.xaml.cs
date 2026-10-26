using FinanceTracker.View.IncomeView;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinanceTracker
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuBar_IncomeChangeRequest(object sender, EventArgs e)
        {
            IncomeContentArea.Content = new FinanceTracker.View.IncomeView.IncomeViewUC();
        }
    }   
}
using EmployeePayslipSystem.ViewModels;
using System.Windows.Controls;

namespace EmployeePayslipSystem.Views
{
    public partial class PayslipView : UserControl
    {
        public PayslipView()
        {
            InitializeComponent();
            DataContext = new PayslipViewModel();
        }
    }
}
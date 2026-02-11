
using EmployeePayslipSystem.ViewModels;
using System.Windows.Controls;

namespace EmployeePayslipSystem.Views
{
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();
            DataContext = new EmployeeViewModel();
        }
    }
}

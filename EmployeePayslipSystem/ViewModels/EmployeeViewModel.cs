using EmployeePayslipSystem.Commands;
using EmployeePayslipSystem.Data;
using EmployeePayslipSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace EmployeePayslipSystem.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _employee = new Employee();
        public Employee Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }
        private EmployeeRepository repo = new EmployeeRepository();

        public ICommand SaveCommand { get; }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>(repo.GetEmployees());
            SaveCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
        }

        private bool CanSaveEmployee()
        {
            return !string.IsNullOrWhiteSpace(Employee?.EmployeeId) &&
                   !string.IsNullOrWhiteSpace(Employee?.EmployeeName);
        }

        public void SaveEmployee()
        {
            repo.AddEmployee(Employee);
            Employees.Add(Employee);
            Employee = new Employee(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
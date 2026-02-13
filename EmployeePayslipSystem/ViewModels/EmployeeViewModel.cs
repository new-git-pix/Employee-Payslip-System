using EmployeePayslipSystem.Commands;
using EmployeePayslipSystem.Data;
using EmployeePayslipSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private bool _isEditMode = false;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(SaveButtonText));
            }
        }

        public string SaveButtonText => IsEditMode ? "Update Employee" : "Save Employee";

        public ObservableCollection<Employee> Employees { get; set; }

        private EmployeeRepository repo = new EmployeeRepository();

        public ICommand SaveCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>(repo.GetEmployees());

            SaveCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
            EditCommand = new RelayCommand(EditEmployee, CanEditEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanDeleteEmployee);
            CancelCommand = new RelayCommand(CancelEdit);
        }

        private bool CanSaveEmployee()
        {
            return !string.IsNullOrWhiteSpace(Employee?.EmployeeId) &&
                   !string.IsNullOrWhiteSpace(Employee?.EmployeeName);
        }

        public void SaveEmployee()
        {
            if (IsEditMode)
            {
                repo.UpdateEmployee(Employee);

                var existingEmployee = Employees.FirstOrDefault(e => e.EmployeeId == Employee.EmployeeId);
                if (existingEmployee != null)
                {
                    int index = Employees.IndexOf(existingEmployee);
                    Employees[index] = Employee;
                }

                MessageBox.Show("Employee updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                IsEditMode = false;
            }
            else
            {
                repo.AddEmployee(Employee);
                Employees.Add(Employee);

                MessageBox.Show("Employee added successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Employee = new Employee();
            SelectedEmployee = null;
        }

        private bool CanEditEmployee()
        {
            return SelectedEmployee != null;
        }

        public void EditEmployee()
        {
            if (SelectedEmployee == null) return;

            Employee = new Employee
            {
                EmployeeId = SelectedEmployee.EmployeeId,
                EmployeeName = SelectedEmployee.EmployeeName,
                Designation = SelectedEmployee.Designation,
                JoiningDate = SelectedEmployee.JoiningDate,
                BasicSalary = SelectedEmployee.BasicSalary,
                HRA_Percent = SelectedEmployee.HRA_Percent,
                DA_Percent = SelectedEmployee.DA_Percent,
                OtherAllowance_Percent = SelectedEmployee.OtherAllowance_Percent,
                PF_Percent = SelectedEmployee.PF_Percent,
                ESI_Percent = SelectedEmployee.ESI_Percent
            };

            IsEditMode = true;
        }

        private bool CanDeleteEmployee()
        {
            return SelectedEmployee != null;
        }

        public void DeleteEmployee()
        {
            if (SelectedEmployee == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete employee '{SelectedEmployee.EmployeeName}'?\n\n" +
                $"Employee ID: {SelectedEmployee.EmployeeId}\n" +
                $"This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repo.DeleteEmployee(SelectedEmployee.EmployeeId);

                    Employees.Remove(SelectedEmployee);

                    MessageBox.Show("Employee deleted successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    SelectedEmployee = null;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void CancelEdit()
        {
            Employee = new Employee();
            SelectedEmployee = null;
            IsEditMode = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
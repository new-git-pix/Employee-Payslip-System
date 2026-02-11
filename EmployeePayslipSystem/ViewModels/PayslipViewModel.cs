using EmployeePayslipSystem.Commands;
using EmployeePayslipSystem.Data;
using EmployeePayslipSystem.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeePayslipSystem.ViewModels
{
    public class PayslipViewModel : INotifyPropertyChanged
    {
        private EmployeeRepository empRepo = new EmployeeRepository();
        private PayslipRepository payslipRepo = new PayslipRepository();

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                if (value != null)
                {
                    EmployeeName = value.EmployeeName;
                    Designation = value.Designation;
                    CalculateSalary();
                }
            }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private string _designation;
        public string Designation
        {
            get => _designation;
            set
            {
                _designation = value;
                OnPropertyChanged(nameof(Designation));
            }
        }

        private int _month = DateTime.Now.Month;
        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged(nameof(Month));
                CalculateTotalWorkingDays();
            }
        }

        private int _year = DateTime.Now.Year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                CalculateTotalWorkingDays();
            }
        }

        private int _totalWorkingDays = 26;
        public int TotalWorkingDays
        {
            get => _totalWorkingDays;
            set
            {
                _totalWorkingDays = value;
                OnPropertyChanged(nameof(TotalWorkingDays));
                CalculateSalary();
            }
        }

        private int _leaveDays;
        public int LeaveDays
        {
            get => _leaveDays;
            set
            {
                _leaveDays = value;
                OnPropertyChanged(nameof(LeaveDays));
                CalculateSalary();
            }
        }

        private int _workedDays;
        public int WorkedDays
        {
            get => _workedDays;
            set
            {
                _workedDays = value;
                OnPropertyChanged(nameof(WorkedDays));
            }
        }

        private decimal _basicSalary;
        public decimal BasicSalary
        {
            get => _basicSalary;
            set
            {
                _basicSalary = value;
                OnPropertyChanged(nameof(BasicSalary));
            }
        }

        private decimal _hra;
        public decimal HRA
        {
            get => _hra;
            set
            {
                _hra = value;
                OnPropertyChanged(nameof(HRA));
            }
        }

        private decimal _da;
        public decimal DA
        {
            get => _da;
            set
            {
                _da = value;
                OnPropertyChanged(nameof(DA));
            }
        }

        private decimal _otherAllowance;
        public decimal OtherAllowance
        {
            get => _otherAllowance;
            set
            {
                _otherAllowance = value;
                OnPropertyChanged(nameof(OtherAllowance));
            }
        }

        private decimal _grossSalary;
        public decimal GrossSalary
        {
            get => _grossSalary;
            set
            {
                _grossSalary = value;
                OnPropertyChanged(nameof(GrossSalary));
            }
        }

        private decimal _pf;
        public decimal PF
        {
            get => _pf;
            set
            {
                _pf = value;
                OnPropertyChanged(nameof(PF));
            }
        }

        private decimal _esi;
        public decimal ESI
        {
            get => _esi;
            set
            {
                _esi = value;
                OnPropertyChanged(nameof(ESI));
            }
        }

        private decimal _totalDeductions;
        public decimal TotalDeductions
        {
            get => _totalDeductions;
            set
            {
                _totalDeductions = value;
                OnPropertyChanged(nameof(TotalDeductions));
            }
        }

        private decimal _netSalary;
        public decimal NetSalary
        {
            get => _netSalary;
            set
            {
                _netSalary = value;
                OnPropertyChanged(nameof(NetSalary));
            }
        }

        private Payslip _selectedPayslip;
        public Payslip SelectedPayslip
        {
            get => _selectedPayslip;
            set
            {
                _selectedPayslip = value;
                OnPropertyChanged(nameof(SelectedPayslip));
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Payslip> Payslips { get; set; }

        public ICommand GeneratePayslipCommand { get; }
        public ICommand PrintPayslipCommand { get; }

        public PayslipViewModel()
        {
            Employees = new ObservableCollection<Employee>(empRepo.GetEmployees());
            Payslips = new ObservableCollection<Payslip>(payslipRepo.GetPayslips());

            GeneratePayslipCommand = new RelayCommand(GeneratePayslip, CanGeneratePayslip);
            PrintPayslipCommand = new RelayCommand(PrintPayslip, CanPrintPayslip);

            CalculateTotalWorkingDays();
        }

        private void CalculateTotalWorkingDays()
        {
            int daysInMonth = DateTime.DaysInMonth(Year, Month);
            int sundays = 0;
            for (int day = 1; day <= daysInMonth; day++)
            {
                if (new DateTime(Year, Month, day).DayOfWeek == DayOfWeek.Sunday)
                    sundays++;
            }
            TotalWorkingDays = daysInMonth - sundays;
        }

        private void CalculateSalary()
        {
            if (SelectedEmployee == null) return;

            WorkedDays = TotalWorkingDays - LeaveDays;
            if (WorkedDays < 0) WorkedDays = 0;

            decimal dailySalary = SelectedEmployee.BasicSalary / TotalWorkingDays;
            BasicSalary = Math.Round(dailySalary * WorkedDays, 2);

            HRA = Math.Round((BasicSalary * SelectedEmployee.HRA_Percent) / 100, 2);
            DA = Math.Round((BasicSalary * SelectedEmployee.DA_Percent) / 100, 2);
            OtherAllowance = Math.Round((BasicSalary * SelectedEmployee.OtherAllowance_Percent) / 100, 2);

            GrossSalary = BasicSalary + HRA + DA + OtherAllowance;

            PF = Math.Round((BasicSalary * SelectedEmployee.PF_Percent) / 100, 2);
            ESI = Math.Round((GrossSalary * SelectedEmployee.ESI_Percent) / 100, 2);

            TotalDeductions = PF + ESI;
            NetSalary = GrossSalary - TotalDeductions;
        }

        private bool CanGeneratePayslip()
        {
            return SelectedEmployee != null && WorkedDays >= 0;
        }

        private void GeneratePayslip()
        {
            var payslip = new Payslip
            {
                EmployeeId = SelectedEmployee.EmployeeId,
                EmployeeName = EmployeeName,
                Designation = Designation,
                Month = Month,
                Year = Year,
                TotalWorkingDays = TotalWorkingDays,
                LeaveDays = LeaveDays,
                WorkedDays = WorkedDays,
                BasicSalary = BasicSalary,
                HRA = HRA,
                DA = DA,
                OtherAllowance = OtherAllowance,
                GrossSalary = GrossSalary,
                PF = PF,
                ESI = ESI,
                TotalDeductions = TotalDeductions,
                NetSalary = NetSalary,
                GeneratedDate = DateTime.Now
            };

            payslipRepo.SavePayslip(payslip);
            Payslips.Insert(0, payslip);

            MessageBox.Show("Payslip generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            SelectedEmployee = null;
            LeaveDays = 0;
        }

        private bool CanPrintPayslip()
        {
            return SelectedPayslip != null;
        }

        private void PrintPayslip()
        {
            if (SelectedPayslip == null) return;

            var printWindow = new Window
            {
                Title = "Print Payslip",
                Width = 800,
                Height = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            var printContent = new Views.PayslipPrintView(SelectedPayslip);
            printWindow.Content = printContent;

            printWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
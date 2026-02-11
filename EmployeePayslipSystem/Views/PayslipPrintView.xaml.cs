using EmployeePayslipSystem.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EmployeePayslipSystem.Views
{
    public partial class PayslipPrintView : UserControl
    {
        private Payslip _payslip;

        public PayslipPrintView(Payslip payslip)
        {
            InitializeComponent();
            _payslip = payslip;
            LoadPayslipData();
        }

        private void LoadPayslipData()
        {
            txtMonthYear.Text = $"For the month of {_payslip.MonthYear}";
            txtEmployeeId.Text = _payslip.EmployeeId;
            txtEmployeeName.Text = _payslip.EmployeeName;
            txtDesignation.Text = _payslip.Designation;
            txtTotalWorkingDays.Text = _payslip.TotalWorkingDays.ToString();
            txtLeaveDays.Text = _payslip.LeaveDays.ToString();
            txtWorkedDays.Text = _payslip.WorkedDays.ToString();
            txtBasicSalary.Text = $"₹{_payslip.BasicSalary:N2}";
            txtHRA.Text = $"₹{_payslip.HRA:N2}";
            txtDA.Text = $"₹{_payslip.DA:N2}";
            txtOtherAllowance.Text = $"₹{_payslip.OtherAllowance:N2}";
            txtGrossSalary.Text = $"₹{_payslip.GrossSalary:N2}";
            txtPF.Text = $"₹{_payslip.PF:N2}";
            txtESI.Text = $"₹{_payslip.ESI:N2}";
            txtTotalDeductions.Text = $"₹{_payslip.TotalDeductions:N2}";
            txtNetSalary.Text = $"₹{_payslip.NetSalary:N2}";
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnPrint.Visibility = Visibility.Collapsed;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(this, "Employee Payslip");
                    MessageBox.Show("Payslip printed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Window.GetWindow(this)?.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing: {ex.Message}", "Print Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnPrint.Visibility = Visibility.Visible;
            }
        }
    }
}
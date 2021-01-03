using System;
using ConsoleApp.Model;

namespace ConsoleApp.Interfaces
{
    public interface IPaymentDateCalculator
    {
         DateTime CalculateNextSalaryDate(SalaryDateCalculation salaryDateCalculation);
    }
}
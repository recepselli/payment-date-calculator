using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Interfaces;
using ConsoleApp.Model;
using ConsoleApp.Enums;

namespace ConsoleApp.Services
{
    public class PaymentDateCalculator : IPaymentDateCalculator
    {
        private readonly DateTime _currentDateTime;

        private readonly IEnumerable<Holiday> _holidays;

        public PaymentDateCalculator(DateTime dateTime, List<Holiday> holidays = null)
        {
            _currentDateTime = dateTime;
            _holidays = holidays ?? new List<Holiday>();
        }

        public DateTime CalculateNextSalaryDate(SalaryDateCalculation salaryDateCalculation)
        {
            return salaryDateCalculation.SalaryFrequency switch
            {
                SalaryFrequency.SpecificDayOfMonth => GetSpecificDayOfMonth(salaryDateCalculation.Day),
                SalaryFrequency.FirstWorkingDayOfMonth => GetFirstWorkingDayOfMonth(),
                SalaryFrequency.DayBeforeLastWorkingDay => GetDayBeforeLastWorkingDay(),
                SalaryFrequency.LastWorkingDayOfMonth => GetLastWorkingDayOfMonth(),
                SalaryFrequency.FirstXDay => GetFirstXDay(salaryDateCalculation.Day),
                SalaryFrequency.LastXDay => GetLastXDay(salaryDateCalculation.Day),
                _ => throw new NotSupportedException($"{salaryDateCalculation.SalaryFrequency} is not supported.")
            };
        }

        private DateTime GetSpecificDayOfMonth(int day)
        {
            int lastDayOfMonth = DateTime.DaysInMonth(_currentDateTime.Year, _currentDateTime.Month);

            if (day < 1 || day > lastDayOfMonth)
                throw new ArgumentOutOfRangeException(nameof(day), day, $"Day must be bigger than 0 and smaller than {lastDayOfMonth}.");

            if (_currentDateTime.Day > day && _currentDateTime.Month == 12)
                return new DateTime(_currentDateTime.Year + 1, 1, day);

            else if (_currentDateTime.Day > day)
                return new DateTime(_currentDateTime.Year, _currentDateTime.Month + 1, day);

            else
                return new DateTime(_currentDateTime.Year, _currentDateTime.Month, day);
        }

        private DateTime GetFirstWorkingDayOfMonth()
        {
            var firstWorkingDayOfMonth = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).First();

            return GetXDay(firstWorkingDayOfMonth).First();
        }

        private DateTime GetLastWorkingDayOfMonth()
        {
            var nextSalaryPaymentDate = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).Last();

            return GetXDay(nextSalaryPaymentDate).Last();
        }

        private DateTime GetDayBeforeLastWorkingDay()
        {
            return GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).AsEnumerable().Reverse().Skip(1).First();
        }

        private DateTime GetFirstXDay(int day)
        {
            if (day < 1 || day > 5)
                throw new ArgumentOutOfRangeException(nameof(day), day, "Day must be bigger than 0 and smaller than 5.");

            var firstXDayOfMonth = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).First(r => (int)r.DayOfWeek == day);

            return GetXDay(firstXDayOfMonth).First(r => (int)r.DayOfWeek == day);
        }

        private DateTime GetLastXDay(int day)
        {
            if (day < 1 || day > 5)
                throw new ArgumentOutOfRangeException(nameof(day), day, "Day must be bigger than 0 and smaller than 5.");

            var lastXDayOfMonth = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).Last(r => (int)r.DayOfWeek == day);

            return GetXDay(lastXDayOfMonth).Last(r => (int)r.DayOfWeek == day);
        }

        private IEnumerable<DateTime> GetWorkingDayOfMonth(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day))
                .Where(r => r.DayOfWeek != DayOfWeek.Saturday && r.DayOfWeek != DayOfWeek.Sunday)
                .Where(r => !GetHolidayDates(year).Contains(r))
                .ToList();
        }

        private IEnumerable<DateTime> GetXDay(DateTime dateTime)
        {
            if (_currentDateTime.Date >= dateTime.Date && _currentDateTime.Month == 12)
                return GetWorkingDayOfMonth(_currentDateTime.Year + 1, 1);

            else if (_currentDateTime.Date >= dateTime.Date)
                return GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month + 1);

            return new List<DateTime>
            {
                dateTime
            };
        }

        private IEnumerable<DateTime> GetHolidayDates(int year)
        {
            return _holidays.Select(r => new DateTime(year, r.Month, r.Day));
        }
    }
}
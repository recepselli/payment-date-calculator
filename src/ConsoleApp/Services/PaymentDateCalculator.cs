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

        public PaymentDateCalculator(DateTime dateTime, IEnumerable<Holiday> holidays = null)
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

            CheckDay(max: lastDayOfMonth, day: day);

            if (_currentDateTime.Day > day && _currentDateTime.Month == 12)
                return new DateTime(_currentDateTime.Year + 1, 1, day);

            if (_currentDateTime.Day > day)
                return new DateTime(_currentDateTime.Year, _currentDateTime.Month + 1, day);

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
            var workingDays = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).ToList();
            var dayBeforeLast = workingDays[^2];

            if (_currentDateTime.Date >= dayBeforeLast.Date)
            {
                int nextYear = _currentDateTime.Month == 12 ? _currentDateTime.Year + 1 : _currentDateTime.Year;
                int nextMonth = _currentDateTime.Month == 12 ? 1 : _currentDateTime.Month + 1;

                workingDays = GetWorkingDayOfMonth(nextYear, nextMonth).ToList();
                dayBeforeLast = workingDays[^2];
            }

            return dayBeforeLast;
        }

        private DateTime GetFirstXDay(int day)
        {
            CheckDay(day: day);

            var firstXDayOfMonth = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).First(r => (int)r.DayOfWeek == day);

            return GetXDay(firstXDayOfMonth).First(r => (int)r.DayOfWeek == day);
        }

        private DateTime GetLastXDay(int day)
        {
            CheckDay(day: day);

            var lastXDayOfMonth = GetWorkingDayOfMonth(_currentDateTime.Year, _currentDateTime.Month).Last(r => (int)r.DayOfWeek == day);

            return GetXDay(lastXDayOfMonth).Last(r => (int)r.DayOfWeek == day);
        }

        private IEnumerable<DateTime> GetWorkingDayOfMonth(int year, int month)
        {
            return Enumerable
                .Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day))
                .Where(r => r.DayOfWeek != DayOfWeek.Saturday && r.DayOfWeek != DayOfWeek.Sunday && !GetHolidayDates(year).Contains(r));
        }

        private IEnumerable<DateTime> GetXDay(DateTime dateTime)
        {
            if (_currentDateTime.Date >= dateTime.Date && _currentDateTime.Month == 12)
                return GetWorkingDayOfMonth(_currentDateTime.Year + 1, 1);

            if (_currentDateTime.Date >= dateTime.Date)
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

        private void CheckDay(int min = 0, int max = 5, int day = 0)
        {
            if (day <= min || day > max)
                throw new ArgumentOutOfRangeException(nameof(day), day, $"Day must be bigger than {min} and smaller than {max}.");
        }
    }
}
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using ConsoleApp.Services;
using ConsoleApp.Model;
using ConsoleApp.UnitTests.Helpers;
using ConsoleApp.Enums;

namespace ConsoleApp.UnitTests.Services
{
    public class PaymentDateCalculatorTests
    {
        [Theory,
         MemberData(nameof(TestData.SpecificDayOfMonthData), MemberType = typeof(TestData)),
         MemberData(nameof(TestData.FirstWorkingDayOfMonthData), MemberType = typeof(TestData)),
         MemberData(nameof(TestData.DayBeforeLastWorkingDayData), MemberType = typeof(TestData)),
         MemberData(nameof(TestData.LastWorkingDayOfMonthData), MemberType = typeof(TestData)),
         MemberData(nameof(TestData.FirstXDayData), MemberType = typeof(TestData)),
         MemberData(nameof(TestData.LastXDayData), MemberType = typeof(TestData))
        ]
        public void Should_Pass_All_TestData_CalculateNextSalaryDate
        (
            DateTime currentDate,
            IEnumerable<Holiday> holidays,
            Enums.SalaryFrequency salaryFrequency,
            int day,
            DateTime expectedDate
        )
        {
            var paymentDateCalculator = new PaymentDateCalculator(currentDate, holidays);

            var nextSalaryPaymentDate = paymentDateCalculator.CalculateNextSalaryDate(new SalaryDateCalculation
            {
                SalaryFrequency = salaryFrequency,
                Day = day
            });

            nextSalaryPaymentDate.Should().Be(expectedDate);
        }

        [Fact]
        public void Should_Throw_NotSupportedException()
        {
            var paymentDateCalculator = new PaymentDateCalculator(DateTime.Now);

            var salaryFrequency = (SalaryFrequency)99;

            Action act = () => paymentDateCalculator.CalculateNextSalaryDate(new SalaryDateCalculation
            {
                SalaryFrequency = salaryFrequency
            });

            act.Should().Throw<NotSupportedException>()
            .WithMessage($"{salaryFrequency} is not supported.");
        }

        [Theory,
         MemberData(nameof(TestData.ArgumentOutOfRangeExceptionData), MemberType = typeof(TestData))
        ]
        public void Should_Throw_ArgumentOutOfRangeException(DateTime currentDate, Enums.SalaryFrequency salaryFrequency, int day, int maxValue)
        {
            var paymentDateCalculator = new PaymentDateCalculator(currentDate);

            Action act = () => paymentDateCalculator.CalculateNextSalaryDate(new SalaryDateCalculation
            {
                SalaryFrequency = salaryFrequency,
                Day = day
            });

            act.Should().Throw<ArgumentOutOfRangeException>()
               .WithMessage($"Day must be bigger than 0 and smaller than { maxValue }. (Parameter 'day')\nActual value was { day }.");
        }
    }
}
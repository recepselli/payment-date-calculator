using System;

namespace ConsoleApp.UnitTests.Helpers
{
    public class TestData
    {
        public static readonly object[][] FirstXDayData =
        {
            new object[] {new DateTime(2021, 1, 1), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2021, 1, 5)},
            new object[] {new DateTime(2021, 12, 10), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2022, 1, 4)},
            new object[] {new DateTime(2021, 1, 6), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2021, 2, 2)},
        };

        public static readonly object[][] LastXDayData =
        {
            new object[] {new DateTime(2017, 7, 14), Enums.SalaryFrequency.LastXDay, 3, new DateTime(2017, 7, 26)},
            new object[] {new DateTime(2017, 8, 18), Enums.SalaryFrequency.LastXDay, 1, new DateTime(2017, 8, 28)},
            new object[] {new DateTime(2017, 9, 21), Enums.SalaryFrequency.LastXDay, 5, new DateTime(2017, 9, 29)},
        };

        public static readonly object[][] SpecificDayOfMonthData = {
            new object[] {new DateTime(2017, 12, 21), Enums.SalaryFrequency.SpecificDayOfMonth, 5, new DateTime(2018, 1, 5)},
            new object[] {new DateTime(2017, 7, 20), Enums.SalaryFrequency.SpecificDayOfMonth, 14, new DateTime(2017, 8, 14)},
            new object[] {new DateTime(2017, 7, 8), Enums.SalaryFrequency.SpecificDayOfMonth, 12, new DateTime(2017, 7, 12)},
        };

        public static readonly object[][] FirstWorkingDayOfMonthData = {
            new object[] {new DateTime(2017, 6, 8), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2017, 7, 3)},
            new object[] {new DateTime(2017, 7, 31), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2017, 8, 1)},
            new object[] {new DateTime(2020, 12, 31), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2021, 1, 1)}
        };

        public static readonly object[][] DayBeforeLastWorkingDayData = {
            new object[] {new DateTime(2017, 6, 8), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2017, 6, 29)},
            new object[] {new DateTime(2017, 9, 20), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2017, 9, 28)},
            new object[] {new DateTime(2020, 12, 31), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2020, 12, 30)}
        };

        public static readonly object[][] LastWorkingDayOfMonthData = {
            new object[] {new DateTime(2017, 6, 8), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2017, 6, 30)},
            new object[] {new DateTime(2017, 7, 31), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2017, 8, 31)},
            new object[] {new DateTime(2020, 12, 31), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2021, 1, 29)}
        };

        public static readonly object[][] ArgumentOutOfRangeExceptionData = {
            new object[] {DateTime.Now, Enums.SalaryFrequency.FirstXDay, 0, 5},
            new object[] {DateTime.Now, Enums.SalaryFrequency.FirstXDay, 6, 5},

            new object[] {DateTime.Now, Enums.SalaryFrequency.LastXDay, -1, 5},
            new object[] {DateTime.Now, Enums.SalaryFrequency.LastXDay, 10, 5},

            new object[] {new DateTime(2021, 1, 1), Enums.SalaryFrequency.SpecificDayOfMonth, 0, 31},
            new object[] {new DateTime(2021, 1, 1), Enums.SalaryFrequency.SpecificDayOfMonth, 35, 31},
        };
    }
}
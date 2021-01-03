using System;
using System.Collections.Generic;
using ConsoleApp.Model;

namespace ConsoleApp.UnitTests.Helpers
{
    public class TestData
    {
        public static readonly object[][] FirstXDayData =
        {
            new object[] {new DateTime(2021, 1, 1), GetHolidays(), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2021, 1, 5)},
            new object[] {new DateTime(2021, 12, 10), GetHolidays(), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2022, 1, 4)},
            new object[] {new DateTime(2021, 1, 6), GetHolidays(), Enums.SalaryFrequency.FirstXDay, 2, new DateTime(2021, 2, 2)},
        };

        public static readonly object[][] LastXDayData =
        {
            new object[] {new DateTime(2017, 7, 14), GetHolidays(), Enums.SalaryFrequency.LastXDay, 3, new DateTime(2017, 7, 26)},
            new object[] {new DateTime(2017, 8, 18), GetHolidays(), Enums.SalaryFrequency.LastXDay, 1, new DateTime(2017, 8, 28)},
            new object[] {new DateTime(2017, 9, 21), GetHolidays() ,Enums.SalaryFrequency.LastXDay, 5, new DateTime(2017, 9, 29)},
        };

        public static readonly object[][] SpecificDayOfMonthData = {
            new object[] {new DateTime(2017, 12, 21), GetHolidays(), Enums.SalaryFrequency.SpecificDayOfMonth, 5, new DateTime(2018, 1, 5)},
            new object[] {new DateTime(2017, 7, 20), GetHolidays(), Enums.SalaryFrequency.SpecificDayOfMonth, 14, new DateTime(2017, 8, 14)},
            new object[] {new DateTime(2017, 7, 8), GetHolidays(), Enums.SalaryFrequency.SpecificDayOfMonth, 12, new DateTime(2017, 7, 12)},
        };

        public static readonly object[][] FirstWorkingDayOfMonthData = {
            new object[] {new DateTime(2017, 6, 8), GetHolidays(), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2017, 7, 3)},
            new object[] {new DateTime(2017, 7, 31), GetHolidays(), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2017, 8, 1)},
            new object[] {new DateTime(2020, 12, 31), GetHolidays(), Enums.SalaryFrequency.FirstWorkingDayOfMonth, 0, new DateTime(2021, 1, 4)}
        };

        public static readonly object[][] DayBeforeLastWorkingDayData = {
            new object[] {new DateTime(2017, 6, 8), GetHolidays(), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2017, 6, 29)},
            new object[] {new DateTime(2017, 9, 20), GetHolidays(), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2017, 9, 27)},
            new object[] {new DateTime(2020, 12, 31), GetHolidays(), Enums.SalaryFrequency.DayBeforeLastWorkingDay, 0, new DateTime(2020, 12, 30)}
        };

        public static readonly object[][] LastWorkingDayOfMonthData = {
            new object[] {new DateTime(2017, 6, 8), GetHolidays(), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2017, 6, 30)},
            new object[] {new DateTime(2017, 7, 31), GetHolidays(), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2017, 8, 31)},
            new object[] {new DateTime(2020, 12, 31), GetHolidays(), Enums.SalaryFrequency.LastWorkingDayOfMonth, 0, new DateTime(2021, 1, 29)}
        };

        public static readonly object[][] ArgumentOutOfRangeExceptionData = {
            new object[] {DateTime.Now, Enums.SalaryFrequency.FirstXDay, 0, 5},
            new object[] {DateTime.Now, Enums.SalaryFrequency.FirstXDay, 6, 5},

            new object[] {DateTime.Now, Enums.SalaryFrequency.LastXDay, -1, 5},
            new object[] {DateTime.Now, Enums.SalaryFrequency.LastXDay, 10, 5},

            new object[] {new DateTime(2021, 1, 1), Enums.SalaryFrequency.SpecificDayOfMonth, 0, 31},
            new object[] {new DateTime(2021, 1, 1), Enums.SalaryFrequency.SpecificDayOfMonth, 35, 31},
        };

        private static IEnumerable<Holiday> GetHolidays()
        {
            return new List<Holiday> {
                new Holiday { Day = 1, Month = 1 },
                new Holiday { Day = 2, Month = 4 },
                new Holiday { Day = 4, Month = 4 },
                new Holiday { Day = 5, Month = 4 },
                new Holiday { Day = 1, Month = 5 },
                new Holiday { Day = 8, Month = 5 },
                new Holiday { Day = 5, Month = 7 },
                new Holiday { Day = 6, Month = 7 },
                new Holiday { Day = 28, Month = 9 },
                new Holiday { Day = 28, Month = 10 },
                new Holiday { Day = 17, Month = 11 },
                new Holiday { Day = 24, Month = 12 },
                new Holiday { Day = 25, Month = 12 },
                new Holiday { Day = 26, Month = 12 }
            };
        }
    }
}
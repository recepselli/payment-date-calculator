# PaymentDateCalculator

This task was asked me to complete almost 3 years ago. I had done it with Microsoft Test Framework but in this version, I decided to upgrade and use XUnit framework and FluentAssertions library.

This small application calculates the salary payment date according to given parameters those are, 

SpecificDayOfMonth, // nth day of month.
FirstWorkingDayOfMonth, // first working day of month
DayBeforeLastWorkingDay, // day before last working day of month
LastWorkingDayOfMonth, // last working day of month
FirstXDay, // first x day of month. ie. if day is 2, it means first tuesday of month
LastXDay, // last x day of month. ie. if day is 1, it means last monday of month

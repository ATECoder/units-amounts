### About

[cc.isr.Units.Amounts] is a .Net library for implementing strongly typed units and amounts.

More information is to be found in the original contribution at [Working with units and amounts].

### How to Use

The following code comes from a console application demo program.

```
namespace SampleConsoleApplication
{
    using System;
    using cc.isr.UnitsAmounts.StandardUnits;
    using cc.isr.UnitsAmounts;

    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
        private static void Main(string[] args)
        {
            // Just a sample case:
            // ===================

            // To allow using units by name, we'll register all units declared in the "StandardUnits" assembly:
            UnitManager.RegisterByAssembly(typeof(cc.isr.UnitsAmounts.StandardUnits.SIUnitTypes).Assembly);

            // I'm driving in a car driving at 70 Miles per hour:
            var carSpeed = new Amount(70.0, SpeedUnits.MilePerHour);

            // I throw an apple forward through the window at 4 meter per second:
            var relativeAppleSpeed = new Amount(4.0, "meter/second");

            // So the absolute speed of the apple flowing through the air:
            var absoluteAppleSpeed = carSpeed + relativeAppleSpeed;

            // Display the result in km/h:
            Console.WriteLine("Absolute speed of the apple: {0:#,##0.00 US|kilometer/hour}", absoluteAppleSpeed);

            Console.WriteLine();
            Console.WriteLine("Press ENTER to end.");
            Console.ReadLine();
        }
    }
}
```

### Key Features

* Full typed units and amounts;
* Numerical operations between the typed units;
* Formatting of typed units values;

### Main Types

The main types provided by this library are:

* _Unit_ Defines a physical units such as meter, kilogram, second, newton, etc.
* _Amount_ Consists of a numerical (double precision type) value and a _Unit_.

### Feedback

[cc.isr.Units.Amounts] is released as open source under the MIT license.
Bug reports and contributions are welcome at the [cc.isr.Units.Amounts] repository.

[Working with units and amounts]: https://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts
[cc.isr.Units.Amounts]: https://github.com/atecoder/units-amounts



using System;
using cc.isr.UnitsAmounts;
using cc.isr.UnitsAmounts.StandardUnits;

namespace SampleConsoleApplication;

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
        Console.WriteLine( "Car speed: {0:#,##0.00 US|mile/hour}", carSpeed );
        Console.WriteLine( "Relative apple speed: {0:#,##0.00 US|meter/second}", relativeAppleSpeed );
        Console.WriteLine("Absolute speed of the apple: {0:#,##0.00 US|kilometer/hour}", absoluteAppleSpeed);

        Console.WriteLine();
        Console.WriteLine("Press ENTER to end.");
        Console.ReadLine();
    }
}

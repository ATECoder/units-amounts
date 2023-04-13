namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A time units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class TimeUnits
{
    private static Unit SecondUnit => new( "Second", "s", SIUnitTypes.Time );

    public static Unit Second => new( "Second", "s", SecondUnit );

    public static Unit Millisecond => new( "Millisecond", "ms", 0.001 * Second );

    public static Unit Microsecond => new( "Microsecond", UnitSymbols.MU + "s", 0.000001 * Second );

    public static Unit Nanosecond => new( "Nanosecond", UnitSymbols.Eta + "s", 0.000000001 * Second );

    public static Unit Minute => new( "Minute", "min", 60.0 * Second );

    public static Unit Hour => new( "Hour", "h", 3600.0 * Second );

    public static Unit Day => new( "Day", "d", 24.0 * Hour );
}


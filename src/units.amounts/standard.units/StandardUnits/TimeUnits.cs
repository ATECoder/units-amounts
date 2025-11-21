
namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A time units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class TimeUnits
{
    private static Unit SecondUnit => new( "Second", "s", SIUnitTypes.Time );

    /// <summary>   Gets the second. </summary>
    /// <value> The second. </value>
    public static Unit Second => new( "Second", "s", SecondUnit );

    /// <summary>   Gets the millisecond. </summary>
    /// <value> The millisecond. </value>
    public static Unit Millisecond => new( "Millisecond", "ms", 0.001 * Second );

    /// <summary>   Gets the microsecond. </summary>
    /// <value> The microsecond. </value>
    public static Unit Microsecond => new( "Microsecond", UnitSymbols.MU + "s", 0.000001 * Second );

    /// <summary>   Gets the nanosecond. </summary>
    /// <value> The nanosecond. </value>
    public static Unit Nanosecond => new( "Nanosecond", UnitSymbols.Eta + "s", 0.000000001 * Second );

    /// <summary>   Gets the minute. </summary>
    /// <value> The minute. </value>
    public static Unit Minute => new( "Minute", "min", 60.0 * Second );

    /// <summary>   Gets the hour. </summary>
    /// <value> The hour. </value>
    public static Unit Hour => new( "Hour", "h", 3600.0 * Second );

    /// <summary>   Gets the day. </summary>
    /// <value> The day. </value>
    public static Unit Day => new( "Day", "d", 24.0 * Hour );
}


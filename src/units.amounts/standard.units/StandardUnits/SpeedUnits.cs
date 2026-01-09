namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A speed units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class SpeedUnits
{
    /// <summary>   Gets the meter per second. </summary>
    /// <value> The meter per second. </value>
    public static Unit MeterPerSecond => new( "Meter/Second", "m/s", LengthUnits.Meter / TimeUnits.Second );

    /// <summary>   Gets the kilometer per hour. </summary>
    /// <value> The kilometer per hour. </value>
    public static Unit KilometerPerHour => new( "Kilometer/Hour", "km/h", LengthUnits.Kilometer / TimeUnits.Hour );

    /// <summary>   Gets the mile per hour. </summary>
    /// <value> The mile per hour. </value>
    public static Unit MilePerHour => new( "Mile/Hour", "mi/h", LengthUnits.Mile / TimeUnits.Hour );

    /// <summary>   Gets the knot. </summary>
    /// <value> The knot. </value>
    public static Unit Knot => new( "Knot", "kn", 1.852 * SpeedUnits.KilometerPerHour );
}



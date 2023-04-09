namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A speed units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class SpeedUnits
{
    public static Unit MeterPerSecond => new( "Meter/Second", "m/s", LengthUnits.Meter / TimeUnits.Second );

    public static Unit KilometerPerHour => new( "Kilometer/Hour", "km/h", LengthUnits.Kilometer / TimeUnits.Hour );

    public static Unit MilePerHour => new( "Mile/Hour", "mi/h", LengthUnits.Mile / TimeUnits.Hour );

    public static Unit Knot => new( "Knot", "kn", 1.852 * SpeedUnits.KilometerPerHour );
}



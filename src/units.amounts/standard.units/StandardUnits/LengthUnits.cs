namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A length units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class LengthUnits
{
    private static Unit MeterUnit => new( "Meter", "m", SIUnitTypes.Length );

    /// <summary>   Gets the meter. </summary>
    /// <value> The meter. </value>
    public static Unit Meter => new( "Meter", "m", MeterUnit );

    /// <summary>   Gets the micron. </summary>
    /// <value> The micron. </value>
    public static Unit Micron => new( "Micron", $"{UnitSymbols.MU}m", 0.000001 * Meter );

    /// <summary>   Gets the millimeter. </summary>
    /// <value> The millimeter. </value>
    public static Unit Millimeter => new( "Millimeter", "mm", 0.001 * Meter );

    /// <summary>   Gets the centimeter. </summary>
    /// <value> The centimeter. </value>
    public static Unit Centimeter => new( "Centimeter", "cm", 0.01 * Meter );

    /// <summary>   Gets the decimeter. </summary>
    /// <value> The decimeter. </value>
    public static Unit Decimeter => new( "Decimeter", "dm", 0.1 * Meter );

    /// <summary>   Gets the decameter. </summary>
    /// <value> The decameter. </value>
    public static Unit Decameter => new( "Decameter", "Dm", 10.0 * Meter );

    /// <summary>   Gets the hectometer. </summary>
    /// <value> The hectometer. </value>
    public static Unit Hectometer => new( "Hectometer", "Hm", 100.0 * Meter );

    /// <summary>   Gets the kilometer. </summary>
    /// <value> The kilometer. </value>
    public static Unit Kilometer => new( "Kilometer", "km", 1000.0 * Meter );

    /// <summary>   Gets the inch. </summary>
    /// <value> The inch. </value>
    public static Unit Inch => new( "Inch", "in", 0.0254 * Meter );

    /// <summary>   Gets the foot. </summary>
    /// <value> The foot. </value>
    public static Unit Foot => new( "Foot", "ft", 12.0 * Inch );

    /// <summary>   Gets the yard. </summary>
    /// <value> The yard. </value>
    public static Unit Yard => new( "Yard", "yd", 36.0 * Inch );

    /// <summary>   Gets the mile. </summary>
    /// <value> The mile. </value>
    public static Unit Mile => new( "Mile", "mi", 5280.0 * Foot );

    /// <summary>   Gets the nautical mile. </summary>
    /// <value> The nautical mile. </value>
    public static Unit NauticalMile => new( "Nautical Mile", "nmi", 1852.0 * Meter );

    /// <summary>   Gets the lightyear. </summary>
    /// <value> The lightyear. </value>
    public static Unit Lightyear => new( "Lightyear", "ly", 9460730472580800.0 * Meter );
}


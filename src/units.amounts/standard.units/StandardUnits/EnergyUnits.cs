namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   An energy units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class EnergyUnits
{
    /// <summary>   Gets the joule. </summary>
    /// <value> The joule. </value>
    public static Unit Joule => new( "Joule", "J", LengthUnits.Meter.Power( 2 ) * MassUnits.Kilogram * TimeUnits.Second.Power( -2 ) );

    /// <summary>   Gets the kilojoule. </summary>
    /// <value> The kilojoule. </value>
    public static Unit Kilojoule => new( "Kilojoule", "KJ", 1000.0 * Joule );

    /// <summary>   Gets the megajoule. </summary>
    /// <value> The megajoule. </value>
    public static Unit Megajoule => new( "Megajoule", "MJ", 1000000.0 * Joule );

    /// <summary>   Gets the gigajoule. </summary>
    /// <value> The gigajoule. </value>
    public static Unit Gigajoule => new( "Gigajoule", "GJ", 1000000000.0 * Joule );

    /// <summary>   Gets the watt. </summary>
    /// <value> The watt. </value>
    public static Unit Watt => new( "Watt", "W", Joule / TimeUnits.Second );

    /// <summary>   Gets the kilowatt. </summary>
    /// <value> The kilowatt. </value>
    public static Unit Kilowatt => new( "Kilowatt", "kW", 1000.0 * Watt );

    /// <summary>   Gets the megawatt. </summary>
    /// <value> The megawatt. </value>
    public static Unit Megawatt => new( "Megawatt", "MW", 1000000.0 * Watt );

    /// <summary>   Gets the watt second. </summary>
    /// <value> The watt second. </value>
    public static Unit WattSecond => new( "Watt-Second", "WSec", Watt * TimeUnits.Second );

    /// <summary>   Gets the watt hour. </summary>
    /// <value> The watt hour. </value>
    public static Unit WattHour => new( "Watt-Hour", "Wh", Watt * TimeUnits.Hour );

    /// <summary>   Gets the kilowatt hour. </summary>
    /// <value> The kilowatt hour. </value>
    public static Unit KilowattHour => new( "Kilowatt-Hour", "KWh", 1000.0 * WattHour );

    /// <summary>   Gets the calorie. </summary>
    /// <value> The calorie. </value>
    public static Unit Calorie => new( "Calorie", "cal", 4.1868 * Joule );

    /// <summary>   Gets the kilocalorie. </summary>
    /// <value> The kilocalorie. </value>
    public static Unit Kilocalorie => new( "Kilocalorie", "Kcal", 1000.0 * Calorie );

    /// <summary>   Gets the horsepower. </summary>
    /// <value> The horsepower. </value>
    public static Unit Horsepower => new( "Horsepower", "hp", 0.73549875 * Kilowatt );
}


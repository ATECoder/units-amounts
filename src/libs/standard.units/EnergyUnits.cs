namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   An energy units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class EnergyUnits
{

    public static Unit Joule => new( "Joule", "J", LengthUnits.Meter.Power( 2 ) * MassUnits.Kilogram * TimeUnits.Second.Power( -2 ) );

    public static Unit Kilojoule => new( "Kilojoule", "KJ", 1000.0 * Joule );

    public static Unit Megajoule => new( "Megajoule", "MJ", 1000000.0 * Joule );

    public static Unit Gigajoule => new( "Gigajoule", "GJ", 1000000000.0 * Joule );

    public static Unit Watt => new( "Watt", "W", Joule / TimeUnits.Second );

    public static Unit Kilowatt => new( "Kilowatt", "kW", 1000.0 * Watt );

    public static Unit Megawatt => new( "Megawatt", "MW", 1000000.0 * Watt );

    public static Unit WattSecond => new( "Watt-Second", "WSec", Watt * TimeUnits.Second );

    public static Unit WattHour => new( "Watt-Hour", "Wh", Watt * TimeUnits.Hour );

    public static Unit KilowattHour => new( "Kilowatt-Hour", "KWh", 1000.0 * WattHour );

    public static Unit Calorie => new( "Calorie", "cal", 4.1868 * Joule );

    public static Unit Kilocalorie => new( "Kilocalorie", "Kcal", 1000.0 * Calorie );

    public static Unit Horsepower => new( "Horsepower", "hp", 0.73549875 * Kilowatt );
}


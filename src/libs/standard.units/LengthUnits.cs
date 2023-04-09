namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A length units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class LengthUnits
{
    private static Unit MeterUnit => new( "Meter", "m", SIUnitTypes.Length );

    public static Unit Meter => new( "Meter", "m", MeterUnit );

    public static Unit Micron => new( "Micron", $"{UnitSymbols.MU}m", 0.000001 * Meter );

    public static Unit Millimeter => new( "Millimeter", "mm", 0.001 * Meter );

    public static Unit Centimeter => new( "Centimeter", "cm", 0.01 * Meter );

    public static Unit Decimeter => new( "Decimeter", "dm", 0.1 * Meter );

    public static Unit Decameter => new( "Decameter", "Dm", 10.0 * Meter );

    public static Unit Hectometer => new( "Hectometer", "Hm", 100.0 * Meter );

    public static Unit Kilometer => new( "Kilometer", "km", 1000.0 * Meter );


    public static Unit Inch => new( "Inch", "in", 0.0254 * Meter );

    public static Unit Foot => new( "Foot", "ft", 12.0 * Inch );

    public static Unit Yard => new( "Yard", "yd", 36.0 * Inch );

    public static Unit Mile => new( "Mile", "mi", 5280.0 * Foot );

    public static Unit NauticalMile => new( "Nautical Mile", "nmi", 1852.0 * Meter );


    public static Unit Lightyear => new( "Lightyear", "ly", 9460730472580800.0 * Meter );
}


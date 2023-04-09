namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A pressure units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class PressureUnits
{
    public static Unit Pascal => new( "Pascal", "Pa", ForceUnits.Newton * LengthUnits.Meter.Power( -2 ) );

    public static Unit Hectopascal => new( "Hectopascal", "HPa", 100.0 * Pascal );

    public static Unit Kilopascal => new( "Kilopascal", "KPa", 1000.0 * Pascal );

    public static Unit Bar => new( "Bar", "bar", 100000.0 * Pascal );

    public static Unit Millibar => new( "Millibar", "mbar", 0.001 * Bar );

    public static Unit Atmosphere => new( "Atmosphere", "atm", 101325.0 * Pascal );

    // Pound-force (lbf) per square inch.
    public static Unit PSI => new( "PSI", "psi", 6894.7 * Pascal );

    public static Unit InH2O => new( "Inch H2O", "inH2O", 249.088908333 * Pascal );
}


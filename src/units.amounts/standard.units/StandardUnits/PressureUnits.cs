
namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A pressure units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class PressureUnits
{
    /// <summary>   Gets the pascal. </summary>
    /// <value> The pascal. </value>
    public static Unit Pascal => new( "Pascal", "Pa", ForceUnits.Newton * LengthUnits.Meter.Power( -2 ) );

    /// <summary>   Gets the hectopascal. </summary>
    /// <value> The hectopascal. </value>
    public static Unit Hectopascal => new( "Hectopascal", "HPa", 100.0 * Pascal );

    /// <summary>   Gets the kilopascal. </summary>
    /// <value> The kilopascal. </value>
    public static Unit Kilopascal => new( "Kilopascal", "KPa", 1000.0 * Pascal );

    /// <summary>   Gets the bar. </summary>
    /// <value> The bar. </value>
    public static Unit Bar => new( "Bar", "bar", 100000.0 * Pascal );

    /// <summary>   Gets the millibar. </summary>
    /// <value> The millibar. </value>
    public static Unit Millibar => new( "Millibar", "mbar", 0.001 * Bar );

    /// <summary>   Gets the atmosphere. </summary>
    /// <value> The atmosphere. </value>
    public static Unit Atmosphere => new( "Atmosphere", "atm", 101325.0 * Pascal );

    /// <summary>   Pound-force (lbf) per square inch. </summary>
    /// <value> The psi. </value>
    public static Unit PSI => new( "PSI", "psi", 6894.7 * Pascal );

    /// <summary>   Gets the in h 2 o. </summary>
    /// <value> The in h 2 o. </value>
    public static Unit InH2O => new( "Inch H2O", "inH2O", 249.088908333 * Pascal );
}


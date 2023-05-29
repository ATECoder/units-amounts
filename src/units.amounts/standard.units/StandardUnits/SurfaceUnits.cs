namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A surface units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class SurfaceUnits
{
    /// <summary>   Gets the square meter. </summary>
    /// <value> The square meter. </value>
    public static Unit SquareMeter => new( "Meter" + UnitSymbols.Squared, "m" + UnitSymbols.Squared, LengthUnits.Meter.Power( 2 ) );

    /// <summary>   Gets the are. </summary>
    /// <value> The are. </value>
    public static Unit Are => new( "Are", "are", 100.0 * SquareMeter );

    /// <summary>   Gets the hectare. </summary>
    /// <value> The hectare. </value>
    public static Unit Hectare => new( "Hectare", "ha", 10000.0 * SquareMeter );

    /// <summary>   Gets the square kilometer. </summary>
    /// <value> The square kilometer. </value>
    public static Unit SquareKilometer => new( "Kilometer" + UnitSymbols.Squared, "Km" + UnitSymbols.Squared, LengthUnits.Kilometer.Power( 2 ) );
}


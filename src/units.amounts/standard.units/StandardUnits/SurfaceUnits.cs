namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A surface units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class SurfaceUnits
{
    public static Unit SquareMeter => new( "Meter" + UnitSymbols.Squared, "m" + UnitSymbols.Squared, LengthUnits.Meter.Power( 2 ) );

    public static Unit Are => new( "Are", "are", 100.0 * SquareMeter );

    public static Unit Hectare => new( "Hectare", "ha", 10000.0 * SquareMeter );

    public static Unit SquareKilometer => new( "Kilometer" + UnitSymbols.Squared, "Km" + UnitSymbols.Squared, LengthUnits.Kilometer.Power( 2 ) );
}


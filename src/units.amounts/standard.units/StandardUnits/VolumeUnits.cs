namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A volume units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class VolumeUnits
{
    public static Unit Liter => new( "Liter", "L", LengthUnits.Decimeter.Power( 3 ) );

    public static Unit Milliliter => new( "Milliliter", "mL", 0.001 * Liter );

    public static Unit Centiliter => new( "Centiliter", "cL", 0.01 * Liter );

    public static Unit Deciliter => new( "Deciliter", "dL", 0.1 * Liter );

    public static Unit CubicMeter => new( "Meter" + UnitSymbols.Cubed, "m" + UnitSymbols.Cubed, LengthUnits.Meter.Power( 3 ) );

    public static Unit CubicFoot => new( "Foot" + UnitSymbols.Cubed, "ft" + UnitSymbols.Cubed, LengthUnits.Foot.Power( 3 ) );

    public static Unit MCF => new( "MCF", "MCF", 1000 * LengthUnits.Foot );

    public static Unit MMCF => new( "MMCF", "MMCF", 1000000 * LengthUnits.Foot );
}


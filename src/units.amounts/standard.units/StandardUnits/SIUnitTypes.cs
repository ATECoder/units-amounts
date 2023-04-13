namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A unit types. </summary>
/// <remarks>   2023-04-08.  <para>
/// DH: Change from meter to Meter; Set all unit name first character to upper case.
/// </para>
/// </remarks>
public static class SIUnitTypes
{

    public static UnitType Length => new( "Meter" );

    public static UnitType Mass => new( "Kilogram" );

    public static UnitType Time => new( "Second" );

    public static UnitType ElectricCurrent => new( "Ampere" );

    public static UnitType ThermodynamicTemperature => new( "Kelvin" );

    public static UnitType AmountOfSubstance => new( "Mole" );

    public static UnitType LuminousIntensity => new( "Candela" );

    public static UnitType Count => new( "Z-*" );

    public static UnitType Ratio => new( "Ratio" );

    public static UnitType Bel => new( "Bel" );

    public static UnitType Neper => new( "Neper" );

    public static UnitType Percent => new( "Percent" );

    public static UnitType Hex => new( "0x" );
}


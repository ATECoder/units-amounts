namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A unit types. </summary>
/// <remarks>
/// 2023-04-08.  <para>
/// DH: Change from meter to Meter; Set all unit name first character to upper case.
/// </para>
/// </remarks>
public static class SIUnitTypes
{

    /// <summary>   Gets the length. </summary>
    /// <value> The length. </value>
    public static UnitType Length => new( "Meter" );

    /// <summary>   Gets the mass. </summary>
    /// <value> The mass. </value>
    public static UnitType Mass => new( "Kilogram" );

    /// <summary>   Gets the time. </summary>
    /// <value> The time. </value>
    public static UnitType Time => new( "Second" );

    /// <summary>   Gets the electric current. </summary>
    /// <value> The electric current. </value>
    public static UnitType ElectricCurrent => new( "Ampere" );

    /// <summary>   Gets the thermodynamic temperature. </summary>
    /// <value> The thermodynamic temperature. </value>
    public static UnitType ThermodynamicTemperature => new( "Kelvin" );

    /// <summary>   Gets the amount of substance. </summary>
    /// <value> The amount of substance. </value>
    public static UnitType AmountOfSubstance => new( "Mole" );

    /// <summary>   Gets the luminous intensity. </summary>
    /// <value> The luminous intensity. </value>
    public static UnitType LuminousIntensity => new( "Candela" );

    /// <summary>   Gets the number of.  </summary>
    /// <value> The count. </value>
    public static UnitType Count => new( "Z-*" );

    /// <summary>   Gets the ratio. </summary>
    /// <value> The ratio. </value>
    public static UnitType Ratio => new( "Ratio" );

    /// <summary>   Gets the bel. </summary>
    /// <value> The bel. </value>
    public static UnitType Bel => new( "Bel" );

    /// <summary>   Gets the neper. </summary>
    /// <value> The neper. </value>
    public static UnitType Neper => new( "Neper" );

    /// <summary>   Gets the percent. </summary>
    /// <value> The percent. </value>
    public static UnitType Percent => new( "Percent" );

    /// <summary>   Gets the hexadecimal. </summary>
    /// <value> The hexadecimal. </value>
    public static UnitType Hex => new( "0x" );
}


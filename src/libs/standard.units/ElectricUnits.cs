namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   An electric units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class ElectricUnits
{
    public static Unit Ampere => new( "Ampere", "A", SIUnitTypes.ElectricCurrent );

    public static Unit MilliAmpere => new( "Milliampere", $"m{Ampere.Symbol}", 0.001 * Ampere );

    public static Unit Coulomb => new( "Coulomb", "C", TimeUnits.Second * Ampere );

    public static Unit Volt => new( "Volt", "V", EnergyUnits.Watt / Ampere );

    public static Unit Millivolt => new( "Millivolt", $"m{Volt.Symbol}", 0.001 * Volt );

    public static Unit Microvolt => new( "Microvolt", $"{UnitSymbols.MU}{Volt.Symbol}", 0.000001 * Volt );

    public static Unit Ohm => new( "Ohm", UnitSymbols.Omega, Volt / Ampere );

    public static Unit Kilohm => new( "Kilohm", "K" + UnitSymbols.Omega, 1000 * Ohm );

    public static Unit Megohm => new( "Megohm", "M" + UnitSymbols.Omega, 1e+6 * Ohm );

    public static Unit OhmMeter => new( "Ohm-Meter", $"{UnitSymbols.Omega}{UnitSymbols.DotProduct}m", Ohm * LengthUnits.Meter );

    public static Unit OhmPerSquare => new( "Ohm/sq", $"{UnitSymbols.Omega}/{UnitSymbols.WhiteSquare}", Ohm * LengthUnits.Meter );

    public static Unit Mho => new( "Mho", UnitSymbols.OmegaInverted, Ampere / Volt );

    public static Unit Farad => new( "Farad", "F", Coulomb / Volt );

    public static Unit Henry => new( "Henry", "H", Ohm * TimeUnits.Second );

    public static Unit MicroHenry => new( "MicroHenry", $"{UnitSymbols.MU}H", 0.000001 * Henry );

    public static Unit Seebeck => new( "Seebeck", "V/K", Volt / TemperatureUnits.Kelvin );

    public static Unit MicroSeebeck => new( "MicroSeebeck", $"{UnitSymbols.MU}V/K", 0.000001 * Seebeck );
}


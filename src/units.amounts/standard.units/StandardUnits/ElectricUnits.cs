// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   An electric units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class ElectricUnits
{
    /// <summary>   Gets the ampere. </summary>
    /// <value> The ampere. </value>
    public static Unit Ampere => new( "Ampere", "A", SIUnitTypes.ElectricCurrent );

    /// <summary>   Gets the milli ampere. </summary>
    /// <value> The milli ampere. </value>
    public static Unit MilliAmpere => new( "Milliampere", $"m{Ampere.Symbol}", 0.001 * Ampere );

    /// <summary>   Gets the coulomb. </summary>
    /// <value> The coulomb. </value>
    public static Unit Coulomb => new( "Coulomb", "C", TimeUnits.Second * Ampere );

    /// <summary>   Gets the volt. </summary>
    /// <value> The volt. </value>
    public static Unit Volt => new( "Volt", "V", EnergyUnits.Watt / Ampere );

    /// <summary>   Gets the millivolt. </summary>
    /// <value> The millivolt. </value>
    public static Unit Millivolt => new( "Millivolt", $"m{Volt.Symbol}", 0.001 * Volt );

    /// <summary>   Gets the microvolt. </summary>
    /// <value> The microvolt. </value>
    public static Unit Microvolt => new( "Microvolt", $"{UnitSymbols.MU}{Volt.Symbol}", 0.000001 * Volt );

    /// <summary>   Gets the ohm. </summary>
    /// <value> The ohm. </value>
    public static Unit Ohm => new( "Ohm", UnitSymbols.Omega, Volt / Ampere );

    /// <summary>   Gets the kilohm. </summary>
    /// <value> The kilohm. </value>
    public static Unit Kilohm => new( "Kilohm", "K" + UnitSymbols.Omega, 1000 * Ohm );

    /// <summary>   Gets the megohm. </summary>
    /// <value> The megohm. </value>
    public static Unit Megohm => new( "Megohm", "M" + UnitSymbols.Omega, 1e+6 * Ohm );

    /// <summary>   Gets the ohm meter. </summary>
    /// <value> The ohm meter. </value>
    public static Unit OhmMeter => new( "Ohm-Meter", $"{UnitSymbols.Omega}{UnitSymbols.DotProduct}m", Ohm * LengthUnits.Meter );

    /// <summary>   Gets the ohm per square. </summary>
    /// <value> The ohm per square. </value>
    public static Unit OhmPerSquare => new( "Ohm/sq", $"{UnitSymbols.Omega}/{UnitSymbols.WhiteSquare}", Ohm * LengthUnits.Meter );

    /// <summary>   Gets the mho. </summary>
    /// <value> The mho. </value>
    public static Unit Mho => new( "Mho", UnitSymbols.OmegaInverted, Ampere / Volt );

    /// <summary>   Gets the farad. </summary>
    /// <value> The farad. </value>
    public static Unit Farad => new( "Farad", "F", Coulomb / Volt );

    /// <summary>   Gets the henry. </summary>
    /// <value> The henry. </value>
    public static Unit Henry => new( "Henry", "H", Ohm * TimeUnits.Second );

    /// <summary>   Gets the micro henry. </summary>
    /// <value> The micro henry. </value>
    public static Unit MicroHenry => new( "MicroHenry", $"{UnitSymbols.MU}H", 0.000001 * Henry );

    /// <summary>   Gets the Seebeck. </summary>
    /// <value> The Seebeck. </value>
    public static Unit Seebeck => new( "Seebeck", "V/K", Volt / TemperatureUnits.Kelvin );

    /// <summary>   Gets the micro seebeck. </summary>
    /// <value> The micro seebeck. </value>
    public static Unit MicroSeebeck => new( "MicroSeebeck", $"{UnitSymbols.MU}V/K", 0.000001 * Seebeck );
}


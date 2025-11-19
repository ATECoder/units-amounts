// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A volume units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class VolumeUnits
{
    /// <summary>   Gets the liter. </summary>
    /// <value> The liter. </value>
    public static Unit Liter => new( "Liter", "L", LengthUnits.Decimeter.Power( 3 ) );

    /// <summary>   Gets the milliliter. </summary>
    /// <value> The milliliter. </value>
    public static Unit Milliliter => new( "Milliliter", "mL", 0.001 * Liter );

    /// <summary>   Gets the centiliter. </summary>
    /// <value> The centiliter. </value>
    public static Unit Centiliter => new( "Centiliter", "cL", 0.01 * Liter );

    /// <summary>   Gets the deciliter. </summary>
    /// <value> The deciliter. </value>
    public static Unit Deciliter => new( "Deciliter", "dL", 0.1 * Liter );

    /// <summary>   Gets the cubic meter. </summary>
    /// <value> The cubic meter. </value>
    public static Unit CubicMeter => new( "Meter" + UnitSymbols.Cubed, "m" + UnitSymbols.Cubed, LengthUnits.Meter.Power( 3 ) );

    /// <summary>   Gets the cubic foot. </summary>
    /// <value> The cubic foot. </value>
    public static Unit CubicFoot => new( "Foot" + UnitSymbols.Cubed, "ft" + UnitSymbols.Cubed, LengthUnits.Foot.Power( 3 ) );

    /// <summary>   Gets the MCF. </summary>
    /// <value> The MCF. </value>
    public static Unit MCF => new( "MCF", "MCF", 1000 * LengthUnits.Foot );

    /// <summary>   Gets the MMCF. </summary>
    /// <value> The MMCF. </value>
    public static Unit MMCF => new( "MMCF", "MMCF", 1000000 * LengthUnits.Foot );
}


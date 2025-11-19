// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A force units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class ForceUnits
{
    /// <summary>   Gets the newton. </summary>
    /// <value> The newton. </value>
    public static Unit Newton => new( "Newton", "N", LengthUnits.Meter * MassUnits.Kilogram * TimeUnits.Second.Power( -2 ) );

    /// <summary>   Gets the pound. </summary>
    /// <value> The pound. </value>
    public static Unit Pound => new( "Pound", "lbf", 4.4482216 * ForceUnits.Newton );
}


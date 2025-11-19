// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The luminous intensity units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class LuminousIntensityUnits
{
    /// <summary>   Gets the candela. </summary>
    /// <value> The candela. </value>
    public static Unit Candela => new( "Candela", "cd", SIUnitTypes.LuminousIntensity );
}


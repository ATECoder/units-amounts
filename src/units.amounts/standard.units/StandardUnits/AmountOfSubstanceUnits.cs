// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   An amount of substance units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class AmountOfSubstanceUnits
{
    /// <summary>   Gets the mole. </summary>
    /// <value> The mole. </value>
    public static Unit Mole => new( "Mole", "mole", SIUnitTypes.AmountOfSubstance );
}


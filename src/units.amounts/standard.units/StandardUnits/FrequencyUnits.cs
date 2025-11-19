// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A frequency units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class FrequencyUnits
{
    /// <summary>   Gets the hertz. </summary>
    /// <value> The hertz. </value>
    public static Unit Hertz => new( "Hertz", "Hz", TimeUnits.Second.Power( -1 ) );

    /// <summary>   Gets the kilohertz. </summary>
    /// <value> The kilohertz. </value>
    public static Unit Kilohertz => new( "Kilohertz", "KHz", 1000.0 * Hertz );

    /// <summary>   Gets the megahertz. </summary>
    /// <value> The megahertz. </value>
    public static Unit Megahertz => new( "Megahertz", "MHz", 1000000.0 * Hertz );

    /// <summary>   Gets the gigahertz. </summary>
    /// <value> The gigahertz. </value>
    public static Unit Gigahertz => new( "Gigahertz", "GHz", 1000000000.0 * Hertz );

    /// <summary>   Gets the rpm. </summary>
    /// <value> The rpm. </value>
    public static Unit RPM => new( "Revolutions per Minute", "rpm", TimeUnits.Minute.Power( -1 ) );
}


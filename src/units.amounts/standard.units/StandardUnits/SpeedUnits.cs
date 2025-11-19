// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A speed units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class SpeedUnits
{
    /// <summary>   Gets the meter per second. </summary>
    /// <value> The meter per second. </value>
    public static Unit MeterPerSecond => new( "Meter/Second", "m/s", LengthUnits.Meter / TimeUnits.Second );

    /// <summary>   Gets the kilometer per hour. </summary>
    /// <value> The kilometer per hour. </value>
    public static Unit KilometerPerHour => new( "Kilometer/Hour", "km/h", LengthUnits.Kilometer / TimeUnits.Hour );

    /// <summary>   Gets the mile per hour. </summary>
    /// <value> The mile per hour. </value>
    public static Unit MilePerHour => new( "Mile/Hour", "mi/h", LengthUnits.Mile / TimeUnits.Hour );

    /// <summary>   Gets the knot. </summary>
    /// <value> The knot. </value>
    public static Unit Knot => new( "Knot", "kn", 1.852 * SpeedUnits.KilometerPerHour );
}



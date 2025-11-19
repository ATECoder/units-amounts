// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts;
/// <summary>
/// Attribute to mark classes having static unit fields to be registered by the UnitManager's
/// RegisterUnits method.
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
[AttributeUsage( AttributeTargets.Class )]
public sealed class UnitDefinitionClassAttribute : Attribute
{
}
/// <summary>
/// Attribute to mark classes having static methods that register conversion functions. The
/// UnitConvert class uses this attribute to identify classes with unit conversion methods in its
/// RegisterConversions method.
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
[AttributeUsage( AttributeTargets.Class )]
public sealed class UnitConversionClassAttribute : Attribute
{
}

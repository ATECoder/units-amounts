// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/runtime/blob/f21a2666c577306e437f80fe934d76cdb15072a5/src/libraries/Common/src/Interop/Windows/Shell32/Interop.SHGetKnownFolderPath.cs

namespace cc.isr.UnitsAmounts;

/// <summary>   This interface represents a consumer of a unit, such as an Amount. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public interface IUnitConsumer
{
    /// <summary>   The unit of the consumer. </summary>
    /// <value> The unit. </value>
    public Unit Unit { get; }
}

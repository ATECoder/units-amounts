
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

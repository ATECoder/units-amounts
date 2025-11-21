
namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The mass units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class MassUnits
{
    /// <summary>   Gets the gram. </summary>
    /// <value> The gram. </value>
    public static Unit Gram => new( "Gram", "g", 0.001 * Kilogram );

    /// <summary>   Gets the kilogram. </summary>
    /// <value> The kilogram. </value>
    public static Unit Kilogram => new( "Kilogram", "Kg", SIUnitTypes.Mass );

    /// <summary>   Gets the milligram. </summary>
    /// <value> The milligram. </value>
    public static Unit Milligram => new( "Milligram", "mg", 0.001 * Gram );

    /// <summary>   Gets the ton. </summary>
    /// <value> The ton. </value>
    public static Unit Ton => new( "Ton", "ton", 1000.0 * Kilogram );
}


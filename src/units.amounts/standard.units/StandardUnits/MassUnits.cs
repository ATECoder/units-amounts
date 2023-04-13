namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The mass units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class MassUnits
{
    public static Unit Gram => new( "Gram", "g", 0.001 * Kilogram );

    public static Unit Kilogram => new( "Kilogram", "Kg", SIUnitTypes.Mass );

    public static Unit Milligram => new( "Milligram", "mg", 0.001 * Gram );

    public static Unit Ton => new( "Ton", "ton", 1000.0 * Kilogram );
}


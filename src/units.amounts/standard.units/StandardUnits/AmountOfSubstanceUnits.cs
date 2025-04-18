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


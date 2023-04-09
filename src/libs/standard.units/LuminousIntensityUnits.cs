namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The luminous intensity units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class LuminousIntensityUnits
{
    public static Unit Candela => new( "Candela", "cd", SIUnitTypes.LuminousIntensity );
}


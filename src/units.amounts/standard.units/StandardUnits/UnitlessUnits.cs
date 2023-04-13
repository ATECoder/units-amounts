namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The unitless units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class UnitlessUnits
{
    public static Unit Count => new( "Count", UnitSymbols.InvertedOne, SIUnitTypes.Count );

    public static Unit Bel => new( "Bel", "Bel", SIUnitTypes.Bel );

    public static Unit Decibel => new( "Decibel", "dB", 10 * Bel );

    public static Unit Ratio => new( "Ratio", UnitSymbols.InvertedOne, SIUnitTypes.Ratio );

    public static Unit Percent => new( "Percent", "%", 100 * Ratio );

    public static Unit PartsPerMillion => new( "PartsPerMillion", "ppm", 1000000 * Ratio );

    public static Unit Neper => new( "Neper", "Np", SIUnitTypes.Neper );

    public static Unit Status => new( "Status", "Ox", SIUnitTypes.Hex );

    #region " Conversion functions "
    public static void RegisterConversions()
    {
        // Register conversion functions:

        // Convert Volts to Decibels:
        UnitManager.RegisterConversion( ElectricUnits.Volt, UnitlessUnits.Decibel, delegate ( Amount amount ) {
            return new Amount( 20 * Math.Log10( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Watts to Decibels:
        UnitManager.RegisterConversion( EnergyUnits.Watt, UnitlessUnits.Decibel, delegate ( Amount amount ) {
            return new Amount( 10 * Math.Log10( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Neper to Decibels:
        UnitManager.RegisterConversion( UnitlessUnits.Neper, UnitlessUnits.Decibel, delegate ( Amount amount ) {
            return new Amount( 0.05 * Math.Log( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Decibels to Neper:
        UnitManager.RegisterConversion( UnitlessUnits.Decibel, UnitlessUnits.Neper, delegate ( Amount amount ) {
            return new Amount( 20 * Math.Log10( Math.E ) * amount.Value, UnitlessUnits.Neper );
        }
        );
    }
    #endregion Conversion functions
}

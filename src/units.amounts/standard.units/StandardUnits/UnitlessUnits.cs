
namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   The unitless units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class UnitlessUnits
{
    /// <summary>   Gets the number of.  </summary>
    /// <value> The count. </value>
    public static Unit Count => new( "Count", UnitSymbols.InvertedOne, SIUnitTypes.Count );

    /// <summary>   Gets the bel. </summary>
    /// <value> The bel. </value>
    public static Unit Bel => new( "Bel", "Bel", SIUnitTypes.Bel );

    /// <summary>   Gets the decibel. </summary>
    /// <value> The decibel. </value>
    public static Unit Decibel => new( "Decibel", "dB", 10 * Bel );

    /// <summary>   Gets the ratio. </summary>
    /// <value> The ratio. </value>
    public static Unit Ratio => new( "Ratio", UnitSymbols.InvertedOne, SIUnitTypes.Ratio );

    /// <summary>   Gets the percent. </summary>
    /// <value> The percent. </value>
    public static Unit Percent => new( "Percent", "%", 100 * Ratio );

    /// <summary>   Gets the parts per million. </summary>
    /// <value> The parts per million. </value>
    public static Unit PartsPerMillion => new( "PartsPerMillion", "ppm", 1000000 * Ratio );

    /// <summary>   Gets the neper. </summary>
    /// <value> The neper. </value>
    public static Unit Neper => new( "Neper", "Np", SIUnitTypes.Neper );

    /// <summary>   Gets the status. </summary>
    /// <value> The status. </value>
    public static Unit Status => new( "Status", "Ox", SIUnitTypes.Hex );

    #region " conversion functions "
    /// <summary>   Registers the conversions. </summary>
    /// <remarks>   2023-05-28. </remarks>
    public static void RegisterConversions()
    {
        // Register conversion functions:

        // Convert Volts to Decibels:
        UnitManager.RegisterConversion( ElectricUnits.Volt, UnitlessUnits.Decibel, delegate ( Amount amount )
        {
            return new Amount( 20 * Math.Log10( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Watts to Decibels:
        UnitManager.RegisterConversion( EnergyUnits.Watt, UnitlessUnits.Decibel, delegate ( Amount amount )
        {
            return new Amount( 10 * Math.Log10( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Neper to Decibels:
        UnitManager.RegisterConversion( UnitlessUnits.Neper, UnitlessUnits.Decibel, delegate ( Amount amount )
        {
            return new Amount( 0.05 * Math.Log( amount.Value ), UnitlessUnits.Decibel );
        }
        );

        // Convert Decibels to Neper:
        UnitManager.RegisterConversion( UnitlessUnits.Decibel, UnitlessUnits.Neper, delegate ( Amount amount )
        {
            return new Amount( 20 * Math.Log10( Math.E ) * amount.Value, UnitlessUnits.Neper );
        }
        );
    }
    #endregion Conversion functions
}

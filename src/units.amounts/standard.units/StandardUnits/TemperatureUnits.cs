namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A temperature units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass, UnitConversionClass]
public static class TemperatureUnits
{
    public static Unit Kelvin => new( "Kelvin", "K", SIUnitTypes.ThermodynamicTemperature );

    public static Unit DegreeCelsius => new( "Degree Celsius", UnitSymbols.Degrees + "C", new UnitType( "Celsius Temperature" ) );

    public static Unit DegreeFahrenheit => new( "Degree Fahrenheit", UnitSymbols.Degrees + "F", new UnitType( "Fahrenheit Temperature" ) );

    public static Unit DegreesCelsiusPerSecond => new( "Deg C/Second", UnitSymbols.Degrees + "C/s", TemperatureUnits.DegreeCelsius / TimeUnits.Second );

    public static Unit DegreesCelsiusPerMinute => new( "Deg C/Minute", UnitSymbols.Degrees + "C/m", TemperatureUnits.DegreeCelsius / TimeUnits.Minute );

    #region " conversion functions "

    public static void RegisterConversions()
    {
        // Register conversion functions:

        // Convert Celsius to Fahrenheit:
        UnitManager.RegisterConversion( DegreeCelsius, DegreeFahrenheit, delegate ( Amount amount ) {
            return new Amount( amount.Value * 1.8 + 32.0, DegreeFahrenheit );
        }
        );

        // Convert Fahrenheit to Celsius:
        UnitManager.RegisterConversion( DegreeFahrenheit, DegreeCelsius, delegate ( Amount amount ) {
            return new Amount( (amount.Value - 32.0) / 1.8, DegreeCelsius );
        }
        );

        // Convert Celsius to Kelvin:
        UnitManager.RegisterConversion( DegreeCelsius, Kelvin, delegate ( Amount amount ) {
            return new Amount( amount.Value + 273.15, Kelvin );
        }
        );

        // Convert Kelvin to Celsius:
        UnitManager.RegisterConversion( Kelvin, DegreeCelsius, delegate ( Amount amount ) {
            return new Amount( amount.Value - 273.15, DegreeCelsius );
        }
        );

        // Convert Fahrenheit to Kelvin:
        UnitManager.RegisterConversion( DegreeFahrenheit, Kelvin, delegate ( Amount amount ) {
            return amount.ConvertedTo( DegreeCelsius ).ConvertedTo( Kelvin );
        }
        );

        // Convert Kelvin to Fahrenheit:
        UnitManager.RegisterConversion( Kelvin, DegreeFahrenheit, delegate ( Amount amount ) {
            return amount.ConvertedTo( DegreeCelsius ).ConvertedTo( DegreeFahrenheit );
        }
        );
    }

    #endregion Conversion functions
}


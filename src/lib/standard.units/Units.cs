namespace Arebis.StandardUnits
{
    using System;

    using Arebis.UnitsAmounts;

    public static class UnitSymbols
    {
        // http://unicodelookup.com/#greek
        // http://unicodelookup.com/#math
        public static string MU => $"{Convert.ToChar( 0x3BC )}";
        public static string Eta => $"{Convert.ToChar( 0x3BC )}";
        public static string Omega => $"{Convert.ToChar( 0x3A9 )}";
        public static string Sigma => $"{Convert.ToChar( 0x3C3 )}";
        public static string OmegaInverted => $"{Convert.ToChar( 0x2127 )}";
        public static string Squared => $"{Convert.ToChar( 0x2072 )}";
        public static string Cubed => $"{Convert.ToChar( 0x2073 )}";
        public static string Degrees => $"{Convert.ToChar( 0x2070 )}";
        public static string DotProduct => $"{Convert.ToChar( 0x2219 )}";
        public static string WhiteSquare => $"{Convert.ToChar( 0x25A1 )}";
        public static string InvertedOne => $"{Convert.ToChar( 0x196 )}";
    }

    public static class SIUnitTypes
    {
        // DH: Change from meter to Meter; Set all unit name first character to upper case.

        public static UnitType Length => new( "Meter" );

        public static UnitType Mass => new( "Kilogram" );

        public static UnitType Time => new( "Second" );

        public static UnitType ElectricCurrent => new( "Ampere" );

        public static UnitType ThermodynamicTemperature => new( "Kelvin" );

        public static UnitType AmountOfSubstance => new( "Mole" );

        public static UnitType LuminousIntensity => new( "Candela" );

        public static UnitType Count => new( "Z-*" );

        public static UnitType Ratio => new( "Ratio" );

        public static UnitType Bel => new( "Bel" );

        public static UnitType Neper => new( "Neper" );

        public static UnitType Percent => new( "Percent" );

        public static UnitType Hex => new( "0x" );
    }

    [UnitDefinitionClass]
    public static class LengthUnits
    {
        private static Unit MeterUnit => new( "Meter", "m", SIUnitTypes.Length );

        public static Unit Meter => new( "Meter", "m", MeterUnit );

        public static Unit Micron => new( "Micron", $"{UnitSymbols.MU}m", 0.000001 * Meter );

        public static Unit Millimeter => new( "Millimeter", "mm", 0.001 * Meter );

        public static Unit Centimeter => new( "Centimeter", "cm", 0.01 * Meter );

        public static Unit Decimeter => new( "Decimeter", "dm", 0.1 * Meter );

        public static Unit Decameter => new( "Decameter", "Dm", 10.0 * Meter );

        public static Unit Hectometer => new( "Hectometer", "Hm", 100.0 * Meter );

        public static Unit Kilometer => new( "Kilometer", "km", 1000.0 * Meter );


        public static Unit Inch => new( "Inch", "in", 0.0254 * Meter );

        public static Unit Foot => new( "Foot", "ft", 12.0 * Inch );

        public static Unit Yard => new( "Yard", "yd", 36.0 * Inch );

        public static Unit Mile => new( "Mile", "mi", 5280.0 * Foot );

        public static Unit NauticalMile => new( "Nautical Mile", "nmi", 1852.0 * Meter );


        public static Unit Lightyear => new( "Lightyear", "ly", 9460730472580800.0 * Meter );
    }

    [UnitDefinitionClass]
    public static class SurfaceUnits
    {
        public static Unit SquareMeter => new( "Meter" + UnitSymbols.Squared, "m" + UnitSymbols.Squared, LengthUnits.Meter.Power( 2 ) );

        public static Unit Are => new( "Are", "are", 100.0 * SquareMeter );

        public static Unit Hectare => new( "Hectare", "ha", 10000.0 * SquareMeter );

        public static Unit SquareKilometer => new( "Kilometer" + UnitSymbols.Squared, "Km" + UnitSymbols.Squared, LengthUnits.Kilometer.Power( 2 ) );
    }

    [UnitDefinitionClass]
    public static class VolumeUnits
    {
        public static Unit Liter => new( "Liter", "L", LengthUnits.Decimeter.Power( 3 ) );

        public static Unit Milliliter => new( "Milliliter", "mL", 0.001 * Liter );

        public static Unit Centiliter => new( "Centiliter", "cL", 0.01 * Liter );

        public static Unit Deciliter => new( "Deciliter", "dL", 0.1 * Liter );

        public static Unit CubicMeter => new( "Meter" + UnitSymbols.Cubed, "m" + UnitSymbols.Cubed, LengthUnits.Meter.Power( 3 ) );

        public static Unit CubicFoot => new( "Foot" + UnitSymbols.Cubed, "ft" + UnitSymbols.Cubed, LengthUnits.Foot.Power( 3 ) );

        public static Unit MCF => new( "MCF", "MCF", 1000 * LengthUnits.Foot );

        public static Unit MMCF => new( "MMCF", "MMCF", 1000000 * LengthUnits.Foot );
    }

    [UnitDefinitionClass]
    public static class TimeUnits
    {
        private static Unit SecondUnit => new( "Second", "s", SIUnitTypes.Time );

        public static Unit Second => new( "Second", "s", SecondUnit );

        public static Unit Millisecond => new( "Millisecond", "ms", 0.001 * Second );

        public static Unit Microsecond => new( "Microsecond", UnitSymbols.MU + "s", 0.000001 * Second );

        public static Unit Nanosecond => new( "Nanosecond", UnitSymbols.Eta + "s", 0.000000001 * Second );

        public static Unit Minute => new( "Minute", "min", 60.0 * Second );

        public static Unit Hour => new( "Hour", "h", 3600.0 * Second );

        public static Unit Day => new( "Day", "d", 24.0 * Hour );
    }

    [UnitDefinitionClass]
    public static class SpeedUnits
    {
        public static Unit MeterPerSecond => new( "Meter/Second", "m/s", LengthUnits.Meter / TimeUnits.Second );

        public static Unit KilometerPerHour => new( "Kilometer/Hour", "km/h", LengthUnits.Kilometer / TimeUnits.Hour );

        public static Unit MilePerHour => new( "Mile/Hour", "mi/h", LengthUnits.Mile / TimeUnits.Hour );

        public static Unit Knot => new( "Knot", "kn", 1.852 * SpeedUnits.KilometerPerHour );
    }


    [UnitDefinitionClass]
    public static class FlowUnits
    {
        public static Unit CubicFootPerHour => new( VolumeUnits.CubicFoot.Name + "/" + TimeUnits.Hour.Name,
                                                                VolumeUnits.CubicFoot.Symbol + "/" + TimeUnits.Hour.Symbol,
                                                                VolumeUnits.CubicFoot / TimeUnits.Hour );

        public static Unit MCFPerDay => new( VolumeUnits.MCF.Name + "/" + TimeUnits.Day.Name,
                                                         VolumeUnits.MCF.Symbol + "/" + TimeUnits.Day.Symbol,
                                                         VolumeUnits.MCF / TimeUnits.Day );

        public static Unit MMCFPerDay => new( VolumeUnits.MMCF.Name + "/" + TimeUnits.Day.Name,
                                                     VolumeUnits.MMCF.Symbol + "/" + TimeUnits.Day.Symbol,
                                                     VolumeUnits.MMCF / TimeUnits.Day );
    }

    [UnitDefinitionClass]
    public static class MassUnits
    {
        public static Unit Gram => new( "Gram", "g", 0.001 * Kilogram );

        public static Unit Kilogram => new( "Kilogram", "Kg", SIUnitTypes.Mass );

        public static Unit Milligram => new( "Milligram", "mg", 0.001 * Gram );

        public static Unit Ton => new( "Ton", "ton", 1000.0 * Kilogram );
    }

    [UnitDefinitionClass]
    public static class ForceUnits
    {
        public static Unit Newton => new( "Newton", "N", LengthUnits.Meter * MassUnits.Kilogram * TimeUnits.Second.Power( -2 ) );

        public static Unit Pound => new( "Pound", "lbf", 4.4482216 * ForceUnits.Newton );
    }

    [UnitDefinitionClass]
    public static class ElectricUnits
    {
        public static Unit Ampere => new( "Ampere", "A", SIUnitTypes.ElectricCurrent );

        public static Unit MilliAmpere => new( "Milliampere", $"m{Ampere.Symbol}", 0.001 * Ampere );

        public static Unit Coulomb => new( "Coulomb", "C", TimeUnits.Second * Ampere );

        public static Unit Volt => new( "Volt", "V", EnergyUnits.Watt / Ampere );

        public static Unit Millivolt => new( "Millivolt", $"m{Volt.Symbol}", 0.001 * Volt );

        public static Unit Microvolt => new( "Microvolt", $"{UnitSymbols.MU}{Volt.Symbol}", 0.000001 * Volt );

        public static Unit Ohm => new( "Ohm", UnitSymbols.Omega, Volt / Ampere );

        public static Unit Kilohm => new( "Kilohm", "K" + UnitSymbols.Omega, 1000 * Ohm );

        public static Unit Megohm => new( "Megohm", "M" + UnitSymbols.Omega, 1e+6 * Ohm );

        public static Unit OhmMeter => new( "Ohm-Meter", $"{UnitSymbols.Omega}{UnitSymbols.DotProduct}m", Ohm * LengthUnits.Meter );

        public static Unit OhmPerSquare => new( "Ohm/sq", $"{UnitSymbols.Omega}/{UnitSymbols.WhiteSquare}", Ohm * LengthUnits.Meter );

        public static Unit Mho => new( "Mho", UnitSymbols.OmegaInverted, Ampere / Volt );

        public static Unit Farad => new( "Farad", "F", Coulomb / Volt );

        public static Unit Henry => new( "Henry", "H", Ohm * TimeUnits.Second );

        public static Unit MicroHenry => new( "MicroHenry", $"{UnitSymbols.MU}H", 0.000001 * Henry );

        public static Unit Seebeck => new( "Seebeck", "V/K", Volt / TemperatureUnits.Kelvin );

        public static Unit MicroSeebeck => new( "MicroSeebeck", $"{UnitSymbols.MU}V/K", 0.000001 * Seebeck );
    }

    [UnitDefinitionClass]
    public static class EnergyUnits
    {

        public static Unit Joule => new( "Joule", "J", LengthUnits.Meter.Power( 2 ) * MassUnits.Kilogram * TimeUnits.Second.Power( -2 ) );

        public static Unit Kilojoule => new( "Kilojoule", "KJ", 1000.0 * Joule );

        public static Unit Megajoule => new( "Megajoule", "MJ", 1000000.0 * Joule );

        public static Unit Gigajoule => new( "Gigajoule", "GJ", 1000000000.0 * Joule );

        public static Unit Watt => new( "Watt", "W", Joule / TimeUnits.Second );

        public static Unit Kilowatt => new( "Kilowatt", "kW", 1000.0 * Watt );

        public static Unit Megawatt => new( "Megawatt", "MW", 1000000.0 * Watt );

        public static Unit WattSecond => new( "Watt-Second", "Wsec", Watt * TimeUnits.Second );

        public static Unit WattHour => new( "Watt-Hour", "Wh", Watt * TimeUnits.Hour );

        public static Unit KilowattHour => new( "Kilowatt-Hour", "KWh", 1000.0 * WattHour );

        public static Unit Calorie => new( "Calorie", "cal", 4.1868 * Joule );

        public static Unit Kilocalorie => new( "Kilocalorie", "Kcal", 1000.0 * Calorie );

        public static Unit Horsepower => new( "Horsepower", "hp", 0.73549875 * Kilowatt );
    }

    [UnitDefinitionClass, UnitConversionClass]
    public static class TemperatureUnits
    {
        public static Unit Kelvin => new( "Kelvin", "K", SIUnitTypes.ThermodynamicTemperature );

        public static Unit DegreeCelsius => new( "Degree Celsius", UnitSymbols.Degrees + "C", new UnitType( "Celsius Temperature" ) );

        public static Unit DegreeFahrenheit => new( "Degree Fahrenheit", UnitSymbols.Degrees + "F", new UnitType( "Fahrenheit Temperature" ) );

        public static Unit DegreesCelsiusPerSecond => new( "Deg C/Second", UnitSymbols.Degrees + "C/s", TemperatureUnits.DegreeCelsius / TimeUnits.Second );

        public static Unit DegreesCelsiusPerMinute => new( "Deg C/Minute", UnitSymbols.Degrees + "C/m", TemperatureUnits.DegreeCelsius / TimeUnits.Minute );

        #region Conversion functions

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

    [UnitDefinitionClass]
    public static class PressureUnits
    {
        public static Unit Pascal => new( "Pascal", "Pa", ForceUnits.Newton * LengthUnits.Meter.Power( -2 ) );

        public static Unit Hectopascal => new( "Hectopascal", "HPa", 100.0 * Pascal );

        public static Unit Kilopascal => new( "Kilopascal", "KPa", 1000.0 * Pascal );

        public static Unit Bar => new( "Bar", "bar", 100000.0 * Pascal );

        public static Unit Millibar => new( "Millibar", "mbar", 0.001 * Bar );

        public static Unit Atmosphere => new( "Atmosphere", "atm", 101325.0 * Pascal );

        // Pound-force (lbf) per square inch.
        public static Unit PSI => new( "PSI", "psi", 6894.7 * Pascal );

        public static Unit InH2O => new( "Inch H2O", "inH2O", 249.088908333 * Pascal );
    }

    [UnitDefinitionClass]
    public static class FrequencyUnits
    {
        public static Unit Hertz => new( "Hertz", "Hz", TimeUnits.Second.Power( -1 ) );

        public static Unit Kilohertz => new( "Kilohertz", "KHz", 1000.0 * Hertz );

        public static Unit Megahertz => new( "Megahertz", "MHz", 1000000.0 * Hertz );

        public static Unit Gigahertz => new( "Gigahertz", "GHz", 1000000000.0 * Hertz );

        public static Unit RPM => new( "Revolutions per Minute", "rpm", TimeUnits.Minute.Power( -1 ) );
    }

    [UnitDefinitionClass]
    public static class AmountOfSubstanceUnits
    {
        public static Unit Mole => new( "Mole", "mole", SIUnitTypes.AmountOfSubstance );
    }

    [UnitDefinitionClass]
    public static class LuminousIntensityUnits
    {
        public static Unit Candela => new( "Candela", "cd", SIUnitTypes.LuminousIntensity );
    }

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

        #region Conversion functions
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
}

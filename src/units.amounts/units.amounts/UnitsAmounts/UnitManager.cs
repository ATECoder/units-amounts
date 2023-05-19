using System.Reflection;

namespace cc.isr.UnitsAmounts;

/// <summary>
/// An event handler called whenever unit names cannot be resolved. Provides a last chance to
/// resolve units.
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
/// <param name="sender">   Source of the event. </param>
/// <param name="args">     Resolve event information. </param>
/// <returns>   A Unit. </returns>
public delegate Unit UnitResolveEventHandler( object? sender, ResolveEventArgs args );

/// <summary>   Delegate representing a unidirectional unit conversion function. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
/// <param name="originalAmount">   The amount to be converted. </param>
/// <returns>   The resulting amount. </returns>
public delegate Amount ConversionFunction( Amount originalAmount );

/// <summary>
/// The UnitManager class provides services around unit naming and identification.
/// </summary>
/// <remarks>
/// The UnitManager class contains static methods that access a singleton instance of the class.
/// </remarks>
public sealed class UnitManager
{
    #region " Fields "

    /// <summary>   Holds the named units: </summary>
    private readonly List<Unit> _allUnits = new();

    /// <summary>   Holds the units keyed by type. </summary>
    private readonly Dictionary<UnitType, List<Unit>> _unitsByType = new();

    /// <summary>
    /// Holds the units keyed by unit name (ignored case).
    /// </summary>
    /// <remarks> This dictionary ignores case on the unit name key but not on symbol (e.g., difference between
    /// Mega Ohm and Milli Ohm)
    /// </remarks>
    private readonly Dictionary<string, Unit> _unitsByName = new( StringComparer.OrdinalIgnoreCase );

    /// <summary>
    /// Holds the units keyed by unit symbol.
    /// </summary>
    /// <remarks> This dictionary does not ignore on symbol (e.g., difference between
    /// Mega Ohm and Milli Ohm)
    /// </remarks>
    private readonly Dictionary<string, Unit> _unitsBySymbol = new();

    /// <summary>   Holds the conversion keyed by <see cref="UnitConversionKeySlot"/>. </summary>
    private readonly Dictionary<UnitConversionKeySlot, UnitConversionValueSlot> _conversions = new();

    #endregion Fields

    #region " Public properties "

    /// <summary>   The instance of the currently used UnitManager. </summary>
    /// <value> The instance. </value>
    public static UnitManager Instance { get; set; } = new();

    #endregion Public properties

    #region " Public methods - Registrations "

    /// <summary>
    /// Registers both units and conversions based on the assemblies public types marked with
    /// [UnitDefinitionsClass] and [UnitConversionsClass] attributes.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="assembly"> The assembly. </param>
    public static void RegisterByAssembly( Assembly assembly )
    {
        RegisterUnits( assembly );
        RegisterConversions( assembly );
    }

    /// <summary>   Register a conversion function. </summary>
    /// <remarks>
    /// A unit conversion function is registered to convert from one unit to another. It will however
    /// be applied to convert from any unit of the same family of the fromUnit, to any unit family of
    /// the toUnit. For reverse conversion, a separate function must be registered.
    /// </remarks>
    /// <param name="fromUnit">             The unit from which this conversion function allows
    ///                                     conversion. </param>
    /// <param name="toUnit">               The unit to which this conversion function allows
    ///                                     conversion to. </param>
    /// <param name="conversionFunction">   The unit conversion function. </param>
    public static void RegisterConversion( Unit fromUnit, Unit toUnit, ConversionFunction conversionFunction ) => Instance._conversions[new UnitConversionKeySlot( fromUnit, toUnit )] = new UnitConversionValueSlot( fromUnit, toUnit, conversionFunction );

    /// <summary>
    /// Registers a set of conversion functions by executing all public static void methods of the
    /// given type. The methods are supposed to call the RegisterConversion method to register
    /// individual conversion functions.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="unitConversionsClass"> The unit conversions class. </param>
    public static void RegisterConversions( Type unitConversionsClass )
    {
        if ( unitConversionsClass == null )
        {
            throw new ArgumentNullException( nameof( unitConversionsClass ) );
        }

        var none = Array.Empty<object>();
        foreach ( var method in unitConversionsClass.GetMethods( BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static ) )
        {
            if ( (method.ReturnType == typeof( void )) && (method.GetParameters().Length == 0) )
            {
                _ = method.Invoke( null, none );
            }
        }
    }

    /// <summary>
    /// Registers a set of conversion function by executing all public static void methods of public
    /// types marked with the [UnitConversionsClass] attribute in the given assembly. The methods are
    /// supposed to call the RegisterConversion method to register individual conversion functions.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="assembly"> The assembly. </param>
    public static void RegisterConversions( Assembly assembly )
    {
        if ( assembly == null )
        {
            throw new ArgumentNullException( nameof( assembly ) );
        }

        foreach ( var t in assembly.GetExportedTypes() )
        {
            if ( t.GetCustomAttributes( typeof( UnitConversionClassAttribute ), false ).Length > 0 )
            {
                RegisterConversions( t );
            }
        }
    }

    /// <summary>   Event raised whenever a unit can not be resolved. </summary>
    public event UnitResolveEventHandler? UnitResolve;

    /// <summary>   Registers a unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="unit"> The unit for which to find a registered match. </param>
    public static void RegisterUnit( Unit unit )
    {
        // Check precondition: unit <> null
        if ( unit is null )
        {
            throw new ArgumentNullException( nameof( unit ) );
        }

        // Check if unit already registered:
        foreach ( var u in Instance._allUnits )
        {
            if ( Unit.Equals( u, unit ) )
            {
                return;
            }
        }

        // Register unit in allUnits:
        Instance._allUnits.Add( unit );

        // Register unit in unitsByType:
        if ( Instance._unitsByType.ContainsKey( unit.UnitType ) )
        {
            Instance._unitsByType[unit.UnitType].Add( unit );
        }
        else
        {
            Instance._unitsByType.Add( unit.UnitType, new List<Unit>() );
            Instance._unitsByType[unit.UnitType] = new List<Unit>
            {
                unit
            };
        }
        // Register unit by name and symbol:
        if ( !Instance._unitsByName.ContainsKey( unit.Name ) )
        {
            Instance._unitsByName.Add( unit.Name, unit );
        }

        if ( !Instance._unitsBySymbol.ContainsKey( unit.Symbol ) )
        {
            Instance._unitsBySymbol.Add( unit.Symbol, unit );
        }

        Instance._unitsByName[unit.Name] = unit;
        Instance._unitsBySymbol[unit.Symbol] = unit;
    }

    /// <summary>   Register all public static fields of type Unit of the given class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="unitDefinitionClass">  The unit definition class. </param>
    public static void RegisterUnits( Type unitDefinitionClass )
    {
        if ( unitDefinitionClass == null )
        {
            throw new ArgumentNullException( nameof( unitDefinitionClass ) );
        }

        foreach ( var member in unitDefinitionClass.GetFields( BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static ) )
        {
            if ( member.FieldType == typeof( Unit ) )
            {
                RegisterUnit( ( Unit ) member.GetValue( null ) );
            }
        }
        foreach ( var member in unitDefinitionClass.GetProperties( BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static ) )
        {
            if ( member.PropertyType == typeof( Unit ) )
            {
                RegisterUnit( ( Unit ) member.GetValue( null ) );
            }
        }
    }

    /// <summary>
    /// Registers all public static fields of type Unit of classes marked with the
    /// [UnitDefinitionClass] attribute in the given assembly.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="assembly"> The assembly. </param>
    public static void RegisterUnits( Assembly assembly )
    {
        foreach ( var t in assembly.GetExportedTypes() )
        {
            if ( t.GetCustomAttributes( typeof( UnitDefinitionClassAttribute ), false ).Length > 0 )
            {
                RegisterUnits( t );
            }
        }
    }

    #endregion Public methods - Registrations

    #region " Public methods - Named units "

    /// <summary>
    /// Retrieves the unit based on its name. If the unit is not found, a UnitResolve event is fired
    /// as last chance to resolve the unit. If the unit cannot be resolved, an UnknownUnitException
    /// is raised.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="UnknownUnitException"> Thrown when an Unknown Unit error condition occurs. </exception>
    /// <param name="name"> The name. </param>
    /// <returns>   The unit by name. </returns>
    public static Unit GetUnitByName( string name )
    {
        // Try resolve unit by unitsByName:
        _ = Instance._unitsByName.TryGetValue( name, out var result );

        // Try resolve unit by UnitResolve event:
        if ( result is null )
        {
            if ( Instance.UnitResolve != null )
            {
                foreach ( Delegate handler in Instance.UnitResolve.GetInvocationList() )

                {
                    result = ( ( UnitResolveEventHandler ) handler)( Instance, new ResolveEventArgs( name ) );
                    if ( result is not null )
                    {
                        RegisterUnit( result );
                        break;
                    }
                }
            }
        }

        // Throw exception if unit resolution failed:

        if ( result is null )
        {
            throw new UnknownUnitException( $"No unit found which name is '{name}'.", name );
        }

        // Return result:

        return result;
    }

    /// <summary>
    /// Retrieves the unit based on its symbol. If the unit is not found, an UnknownUnitException is
    /// raised.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="UnknownUnitException"> Thrown when an Unknown Unit error condition occurs. </exception>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   The unit by symbol. </returns>
    public static Unit GetUnitBySymbol( string symbol )
    {
        // Try resolve unit by unitsBySymbol:

        _ = Instance._unitsBySymbol.TryGetValue( symbol, out var result );

        // Throw exception if unit resolution failed:

        if ( result is null )
            throw new UnknownUnitException( $"No unit found with symbol '{symbol}'.", symbol );

        // Return result:

        return result;
    }

    /// <summary>   Returns the unit types for which one or more units are registered. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   The unit types. </returns>
    public static ICollection<UnitType> GetUnitTypes() => Instance._unitsByType.Keys;

    /// <summary>   Returns all registered units. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   The units. </returns>
    public static IList<Unit> GetUnits() => Instance._allUnits;

    /// <summary>   Whether the given unit is already registered to the UnitManager. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="unit"> The unit for which to find a registered match. </param>
    /// <returns>   True if registered, false if not. </returns>
    public static bool IsRegistered( Unit unit ) => Instance._allUnits.Contains( unit );

    /// <summary>   Returns all registered units of the given type. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="unitType"> Type of the unit. </param>
    /// <returns>   The units. </returns>
    public static IList<Unit> GetUnits( UnitType unitType ) => Instance._unitsByType[unitType];

    /// <summary>   Returns a registered unit that matches the given unit. </summary>
    /// <remarks>
    /// If the passed unit is named, the passed unit will be returned without checking if it is
    /// registered.
    /// </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="unit">         The unit for which to find a registered match. </param>
    /// <param name="selfIfNone">   If true, returns the passed unit if no match is found, otherwise
    ///                             return null if no match is found. </param>
    /// <returns>   A Unit. </returns>
    public static Unit? ResolveToNamedUnit( Unit? unit, bool selfIfNone )
    {
        if ( unit is null )
        {
            throw new ArgumentNullException( nameof( unit ) );
        }

        if ( unit.IsNamed )
        {
            return unit;
        }

        var factor = unit.Factor;
        if ( Instance._unitsByType.ContainsKey( unit.UnitType ) )
        {
            foreach ( var m in Instance._unitsByType[unit.UnitType] )
            {
                if ( m.Factor == factor )
                {
                    return m;
                }
            }
        }
        return selfIfNone ? unit : null;
    }

    #endregion Public methods - Named units

    #region " Public methods - Unit conversions "

    /// <summary>   Converts the given amount to the given unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <exception cref="UnitConversionException">  Thrown when a Unit Conversion error condition
    ///                                             occurs. </exception>
    /// <param name="amount">   The amount. </param>
    /// <param name="toUnit">   The unit to which this conversion function allows conversion to. </param>
    /// <returns>   to converted. </returns>
    public static Amount ConvertTo( Amount amount, Unit toUnit )
    {

        if ( amount is null )
        {
            throw new ArgumentNullException( nameof( amount ) );
        }

        if ( toUnit is null )
        {
            throw new ArgumentNullException( nameof( toUnit ) );
        }

        try
        {
            // Performance optimization:
            if ( object.ReferenceEquals( amount.Unit, toUnit ) )
            {
                return amount;
            }

            // Perform conversion:
            if ( amount.Unit.IsCompatibleTo( toUnit ) )

            {
                return new Amount( amount.Value * amount.Unit.Factor / toUnit.Factor, toUnit );
            }
            else
            {
                var expectedSlot = new UnitConversionKeySlot( amount.Unit, toUnit );
                return Instance._conversions[expectedSlot].Convert( amount ).ConvertedTo( toUnit );
            }
        }
        catch ( KeyNotFoundException )
        {
            throw new UnitConversionException( amount.Unit, toUnit );
        }
    }

    #endregion Public methods - Unit conversions

    #region " Private classes to represent slots in conversion dictionary "

    /// <summary>   Key slot in the internal conversions dictionary. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    private class UnitConversionKeySlot
    {
        /// <summary>   Gets the unit types to which or from which to convert. </summary>
        /// <value> The type of to. </value>
        private readonly UnitType _fromType, _toType;

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="from"> Source for the. </param>
        /// <param name="to">   to. </param>
        public UnitConversionKeySlot( Unit from, Unit to )
        {
            this._fromType = from.UnitType;
            this._toType = to.UnitType;
        }

        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="obj">  The object to compare with the current object. </param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public override bool Equals( object obj )
        {
            var other = obj as UnitConversionKeySlot;
            return other is not null && (this._fromType == other._fromType) && (this._toType == other._toType);
        }

        /// <summary>   Serves as the default hash function. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A hash code for the current object. </returns>
        public override int GetHashCode() => ( this._fromType, this._toType ).GetHashCode();

    }

    /// <summary>   Value slot in the internal conversions dictionary. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    private class UnitConversionValueSlot
    {
        /// <summary>   The unit from which to convert. </summary>
        private readonly Unit _from;

        /// <summary>   The unit to which to convert. </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Code Quality", "IDE0052:Remove unread private members", Justification = "<Pending>" )]
        private readonly Unit _to;

        /// <summary>   The conversion function. </summary>
        private readonly ConversionFunction _conversionFunction;

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="from">                 Source for the. </param>
        /// <param name="to">                   to. </param>
        /// <param name="conversionFunction">   The conversion function. </param>
        public UnitConversionValueSlot( Unit from, Unit to, ConversionFunction conversionFunction )
        {
            this._from = from;
            this._to = to;
            this._conversionFunction = conversionFunction;
        }

        /// <summary>   Converts the given amount. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount Convert( Amount amount ) => this._conversionFunction( amount.ConvertedTo( this._from ) );
    }

    #endregion Private classes to represent slots in conversion dictionary	
}

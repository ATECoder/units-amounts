using System.Runtime.Serialization;

namespace cc.isr.UnitsAmounts;

/// <summary>
/// Exception thrown when a unit conversion failed, i.e. because you are converting amounts from
/// one unit into another non-compatible unit.
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public class UnitConversionException : InvalidOperationException
{
    /// <summary>   Default constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    public UnitConversionException() : base() { }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="message">  The message. </param>
    public UnitConversionException( string message )
        : base( message ) { }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="fromUnit"> from unit. </param>
    /// <param name="toUnit">   to unit. </param>
    public UnitConversionException( Unit fromUnit, Unit toUnit )
        : this( $"Failed to convert from unit '{fromUnit.Name}' to unit '{toUnit.Name}'. Units are not compatible and no conversions are defined." ) { }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The information. </param>
    /// <param name="context">  The context. </param>
    protected UnitConversionException( SerializationInfo info, StreamingContext context )
        : base( info, context )
    { }
}

/// <summary>
/// Exception thrown whenever an exception is referenced by name, but no unit with the given name
/// is known (registered to the UnitManager).
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public class UnknownUnitException : Exception
{

    /// <summary>   Default constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    public UnknownUnitException() : base() { }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="message">  The message. </param>
    public UnknownUnitException( string message )
        : base( message ) { }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The information. </param>
    /// <param name="context">  The context. </param>
    protected UnknownUnitException( SerializationInfo info, StreamingContext context )
        : base( info, context )
    { }
}

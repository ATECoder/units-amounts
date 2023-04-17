using System.Runtime.Serialization;

namespace cc.isr.UnitsAmounts;

/// <summary>
/// Exception thrown when a unit conversion failed, i.e. because you are converting amounts from
/// one unit into another non-compatible unit.
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public class UnitConversionException : InvalidOperationException
{

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="fromUnit"> from unit. </param>
    /// <param name="toUnit">   to unit. </param>
    internal UnitConversionException( Unit fromUnit, Unit toUnit )
        : base( $"Failed to convert from unit '{fromUnit.Name}' to unit '{toUnit.Name}'. Units are not compatible and no conversions are defined." )
    {
        this.FromUnit = fromUnit;
        this.ToUnit = toUnit;
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The information. </param>
    /// <param name="context">  The context. </param>
    protected UnitConversionException( SerializationInfo info, StreamingContext context )
        : base( info, context )
    {
        this.FromUnit = ( Unit ) info.GetValue( nameof( this.FromUnit ), typeof( Unit ) );
        this.ToUnit = ( Unit ) info.GetValue( nameof( this.ToUnit ), typeof( Unit ) );
    }

    /// <summary>   Gets or sets from unit. </summary>
    /// <value> from unit. </value>
    public Unit FromUnit { get; set; }
       
    /// <summary>   Gets or sets to unit. </summary>
    /// <value> to unit. </value>
    public Unit ToUnit { get; set; }


    /// <summary>
    /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo">
    /// </see> with information about the exception.
    /// </summary>
    /// <remarks>   2023-04-17. </remarks>
    /// <param name="info">     The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see>
    ///                         that holds the serialized object data about the exception being
    ///                         thrown. </param>
    /// <param name="context">  The <see cref="T:System.Runtime.Serialization.StreamingContext"></see>
    ///                         that contains contextual information about the source or destination. </param>
    public override void GetObjectData( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context )
    {
        if ( info is not null )
        {
            info.AddValue( $"{nameof( UnitConversionException.FromUnit )}", this.FromUnit );
            info.AddValue( $"{nameof( UnitConversionException.ToUnit )}", this.ToUnit );
            base.GetObjectData( info, context );
        }
    }

}


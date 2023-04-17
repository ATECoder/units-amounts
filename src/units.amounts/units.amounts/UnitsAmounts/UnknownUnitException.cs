using System.Runtime.Serialization;

namespace cc.isr.UnitsAmounts;

/// <summary>
/// Exception thrown whenever an exception is referenced by name, but no unit with the given name
/// is known (registered to the UnitManager).
/// </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public class UnknownUnitException : Exception
{

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="message">  The message. </param>
    /// <param name="name">     The name. </param>
    internal UnknownUnitException( string message, string name ) : base( message )
    {
        this.Name = name;
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The information. </param>
    /// <param name="context">  The context. </param>
    protected UnknownUnitException( SerializationInfo info, StreamingContext context )
        : base( info, context )
    {
        this.Name = ( string ) info.GetValue( nameof( this.Name ), typeof( string ) );
    }

    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public String Name { get; set; }

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
            info.AddValue( $"{nameof ( UnknownUnitException.Name )}", this.Name );
            base.GetObjectData( info, context );
        }
    }

}

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
    internal UnknownUnitException( string message, string name ) : base( message ) => this.Name = name;

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The information. </param>
    /// <param name="context"> The <see cref="StreamingContext" />
    /// that contains contextual information about the source or destination.
    /// </param>
#if NET5_0_OR_GREATER
#pragma warning disable CA1041 // Provide ObsoleteAttribute message
    [Obsolete( DiagnosticId = "SYSLIB0051" )] // add this attribute to the serialization ctor
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
#endif
    protected UnknownUnitException( SerializationInfo info, StreamingContext context )
        : base( info, context ) => this.Name = ( string ) info.GetValue( nameof( this.Name ), typeof( string ) );

    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; }

    /// <summary>
    /// When overridden in a derived class, sets the <see cref="SerializationInfo">
    /// </see> with information about the exception.
    /// </summary>
    /// <remarks>   2023-04-17. </remarks>
    /// <param name="info">     The <see cref="SerializationInfo"></see>
    ///                         that holds the serialized object data about the exception being
    ///                         thrown. </param>
    /// <param name="context">  The <see cref="StreamingContext"></see>
    ///                         that contains contextual information about the source or destination. </param>
#if NET5_0_OR_GREATER
#pragma warning disable CA1041 // Provide ObsoleteAttribute message
    [Obsolete( DiagnosticId = "SYSLIB0051" )] // add this attribute to the serialization ctor
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
#endif
    public override void GetObjectData( SerializationInfo info, StreamingContext context )
    {
        if ( info is not null )
        {
            info.AddValue( $"{nameof( UnknownUnitException.Name )}", this.Name );
            base.GetObjectData( info, context );
        }
    }

}

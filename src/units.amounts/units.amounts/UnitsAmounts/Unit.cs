using System.Runtime.Serialization;

namespace cc.isr.UnitsAmounts;

/// <summary>   Unit. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
[Serializable]
public sealed class Unit : IComparable, IComparable<Unit>, IEquatable<Unit>, IFormattable, ISerializable
{
    #region " construction "

    /// <summary>   Default constructor. Required for serialization. </summary>
    public Unit() : this( Unit.None )
    { }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="unit"> The unit. </param>
    public Unit( Unit unit )
        : this( Valid( unit ).Name, Valid( unit ).Symbol, Valid( unit ).Factor, Valid( unit ).UnitType, Valid( unit ).IsNamed )
    { }

    /// <summary>   Validates the given unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="unit"> The unit. </param>
    /// <returns>   A Unit. </returns>
    private static Unit Valid( Unit unit )
    {
        return unit is null ? throw new ArgumentNullException( nameof( unit ) ) : unit;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="name">     Gets the name of the unit. </param>
    /// <param name="symbol">   Gets the symbol of the unit. </param>
    /// <param name="unitType"> Gets the type of the unit. </param>
    public Unit( string name, string symbol, UnitType unitType )
        : this( name, symbol, 1.0, unitType, true )
    { }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="name">     Gets the name of the unit. </param>
    /// <param name="symbol">   Gets the symbol of the unit. </param>
    /// <param name="baseUnit"> The base unit. </param>
    public Unit( string name, string symbol, Unit baseUnit )
        : this( name, symbol, baseUnit.Factor, baseUnit.UnitType, true )
    { }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="name">     Gets the name of the unit. </param>
    /// <param name="symbol">   Gets the symbol of the unit. </param>
    /// <param name="factor">   Gets the factor of the unit. </param>
    /// <param name="unitType"> Gets the type of the unit. </param>
    /// <param name="isNamed">  Whether the unit is named. </param>
    private Unit( string name, string symbol, double factor, UnitType unitType, bool isNamed )
    {
        this.Name = name;
        this.Symbol = symbol;
        this.Factor = factor;
        this.UnitType = unitType;
        this.IsNamed = isNamed;
    }

    /// <summary>   None unit. </summary>
    /// <value> The none. </value>
    public static Unit None { get; } = new Unit( string.Empty, string.Empty, UnitType.None );

    #endregion Constructor methods

    #region " public implementation "

    /// <summary>   Gets the name of the unit. </summary>
    /// <value> The name. </value>
    public string Name { get; }

    /// <summary>   Gets the symbol of the unit. </summary>
    /// <value> The symbol. </value>
    public string Symbol { get; }

    /// <summary>   Gets the factor of the unit. </summary>
    /// <value> The factor. </value>
    public double Factor { get; }

    /// <summary>   Whether the unit is named. </summary>
    /// <value> True if this object is named, false if not. </value>
    public bool IsNamed { get; }

    /// <summary>   Gets the type of the unit. </summary>
    /// <value> The type of the unit. </value>
    public UnitType UnitType { get; }

    /// <summary>
    /// Checks whether the given unit is compatible to this one. Raises an exception if not
    /// compatible.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="UnitConversionException">  Raised when units are not compatible. </exception>
    /// <param name="compatibleUnit">   The compatible unit. </param>
    public void AssertCompatibility( Unit compatibleUnit )
    {
        if ( !this.IsCompatibleTo( compatibleUnit ) )
            throw new UnitConversionException( this, compatibleUnit );
    }

    /// <summary>   Checks whether the passed unit is compatible with this one. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="otherUnit">    The other unit. </param>
    /// <returns>   True if compatible to, false if not. </returns>
    public bool IsCompatibleTo( Unit otherUnit )
    {
        return this.UnitType == (otherUnit ?? Unit.None).UnitType;
    }

    /// <summary>
    /// Returns a unit by raising the present unit to the specified power.
    /// I.e. meter.Power(3) would return a cubic meter unit.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   A Unit. </returns>
    public Unit Power( int value )
    {
        return new( string.Concat( '(', this.Name, '^', value, ')' ),
                 this.Symbol + '^' + value,
                 ( double ) Math.Pow( this.Factor, value ), this.UnitType.Power( value ), false );
    }

    /// <summary>   Tests equality of both objects. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="obj">  The object to compare with the current object. </param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public override bool Equals( object obj )
    {
        return this.Equals( obj as Unit );
    }

    /// <summary>   Tests equality of both objects. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="other">    The unit to compare to this object. </param>
    /// <returns>   <c>true</c> if the objects are considered equal, false if they are not. </returns>
    public bool Equals( Unit? other )
    {
        return other is not null && this.Factor.Equals( other.Factor ) && this.UnitType.Equals( other.UnitType );
    }

    /// <summary>   Tests equality of both objects. </summary>
    /// <remarks>   David, 2022-01-29. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise,
    /// <see langword="false" />.
    /// </returns>
    public static bool Equals( Unit? left, Unit? right )
    {
        return left is not null && right is not null && left.Equals( right );
    }

    /// <summary>   Returns the hash code of this unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   A hash code for the current object. </returns>
    public override int GetHashCode()
    {
#if NETSTANDARD2_1_OR_GREATER
        return HashCode.Combine( this.Factor, this.UnitType );
#else
        return (this.Factor, this.UnitType).GetHashCode();
#endif
    }

    /// <summary>   Returns a string representation of the unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   A <see cref="string" /> that represents the current object. </returns>
    public override string ToString()
    {
        return this.ToString( null, null );
    }

    /// <summary>   Returns a string representation of the unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="format">   The format to use.
    ///
    ///                                                          -or-
    ///
    ///                                                          A null reference
    ///                                                          (<see langword="Nothing" /> in
    ///                                                          Visual Basic) to use the default
    ///                                                          format defined for the type of the
    ///                                                          <see cref="IFormattable" />
    ///                                                          implementation. </param>
    /// <returns>   A <see cref="string" /> that represents this object. </returns>
    public string ToString( string format )
    {
        return this.ToString( format, null );
    }

    /// <summary>   Returns a string representation of the unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="formatProvider">   The provider to use to format the value.
    ///
    ///                                                                  -or-
    ///
    ///                                                                  A null reference
    ///                                                                  (<see langword="Nothing" />
    ///                                                                  in Visual Basic) to obtain
    ///                                                                  the numeric format
    ///                                                                  information from the current
    ///                                                                  locale setting of the
    ///                                                                  operating system. </param>
    /// <returns>   A <see cref="string" /> that represents this object. </returns>
    public string ToString( IFormatProvider formatProvider )
    {
        return this.ToString( null, formatProvider );
    }

    /// <summary>   Returns a string representation of the unit. </summary>
    /// <remarks>   The format string can be either 'UN' (Unit Name) or 'US' (Unit Symbol). </remarks>
    /// <param name="format">           The format to use.
    ///
    ///                                  -or-
    ///
    ///                                  A null reference (<see langword="Nothing" /> in Visual
    ///                                  Basic) to use the default format defined for the type of the
    ///                                  <see cref="IFormattable" /> implementation. </param>
    /// <param name="formatProvider">   The provider to use to format the value.
    ///
    ///                                  -or-
    ///
    ///                                  A null reference (<see langword="Nothing" /> in Visual
    ///                                  Basic) to obtain the numeric format information from the
    ///                                  current locale setting of the operating system. </param>
    /// <returns>   The value of the current instance in the specified format. </returns>
    public string ToString( string? format, IFormatProvider? formatProvider )
    {
        format ??= "US";

        if ( formatProvider is not null )
        {
            // ICustomFormatter formatter = formatProvider.GetFormat(GetType()) as ICustomFormatter;
            if ( formatProvider.GetFormat( this.GetType() ) is ICustomFormatter formatter )
            {
                return formatter.Format( format, this, formatProvider );
            }
        }

        return format switch
        {
            "UN" => this.Name,
            _ => this.Symbol,
        };
    }

    #endregion Public implementation

    #region " operator overloads "

    /// <summary>   Equality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==( Unit? left, Unit? right )
    {
        return Unit.Equals( left, right );
    }

    /// <summary>   Inequality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=( Unit? left, Unit? right )
    {
        return !Unit.Equals( left, right );
    }

    /// <summary>   Multiplies. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   A Unit. </returns>
    public static Unit Multiply( Unit? left, Unit? right )
    {
        left ??= Unit.None;
        right ??= Unit.None;
        return new Unit( string.Concat( '(', left.Name, '*', right.Name, ')' ), left.Symbol + '*' + right.Symbol, left.Factor * right.Factor, left.UnitType * right.UnitType, false );
    }

    /// <summary>   Multiplication operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first value to multiply. </param>
    /// <param name="right">    The second value to multiply. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator *( Unit? left, Unit? right )
    {
        left ??= Unit.None;
        right ??= Unit.None;
        return new Unit( string.Concat( '(', left.Name, '*', right.Name, ')' ), left.Symbol + '*' + right.Symbol, left.Factor * right.Factor, left.UnitType * right.UnitType, false );
    }

    /// <summary>   Multiplication operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first value to multiply. </param>
    /// <param name="right">    The second value to multiply. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator *( Unit? left, double right )
    {
        return right * left;
    }

    /// <summary>   Multiplication operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first value to multiply. </param>
    /// <param name="right">    The second value to multiply. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator *( double left, Unit? right )
    {
        right ??= Unit.None;
        return new Unit( string.Concat( '(', left.ToString(), '*', right.Name, ')' ), left.ToString() + '*' + right.Symbol, left * right.Factor, right.UnitType, false );
    }

    /// <summary>   Division operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The numerator. </param>
    /// <param name="right">    The denominator. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator /( Unit? left, Unit? right )
    {
        left ??= Unit.None;
        right ??= Unit.None;
        return new Unit( string.Concat( '(', left.Name, '/', right.Name, ')' ), left.Symbol + '/' + right.Symbol, left.Factor / right.Factor, left.UnitType / right.UnitType, false );
    }

    /// <summary>   Division operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The numerator. </param>
    /// <param name="right">    The denominator. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator /( double left, Unit? right )
    {
        right ??= Unit.None;
        return new Unit( string.Concat( '(', left.ToString(), '*', right.Name, ')' ), left.ToString() + '*' + right.Symbol, left / right.Factor, right.UnitType.Power( -1 ), false );
    }

    /// <summary>   Division operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The numerator. </param>
    /// <param name="right">    The denominator. </param>
    /// <returns>   The result of the operation. </returns>
    public static Unit operator /( Unit? left, double right )
    {
        left ??= Unit.None;
        return new Unit( string.Concat( '(', left.Name, '/', right.ToString(), ')' ), left.Symbol + '/' + right.ToString(), left.Factor / right, left.UnitType, false );
    }

    #endregion Operator overloads

    #region " icomparable implementation "

    /// <summary>
    /// Compares the passed unit to the current one. Allows sorting units of the same type.
    /// </summary>
    /// <remarks>   Only compatible units can be compared. </remarks>
    /// <param name="obj">  An object to compare with this instance. </param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return value has
    /// these meanings:
    ///
    ///  <list type="table"><listheader><term> Value</term><description>
    ///  Meaning</description></listheader><item><term> Less than zero</term><description> This
    ///  instance precedes <paramref name="obj" /> in the sort
    ///  order.</description></item><item><term> Zero</term><description> This instance occurs in the
    ///  same position in the sort order as <paramref name="obj" />.</description></item><item><term>
    ///  Greater than zero</term><description> This instance follows <paramref name="obj" /> in the
    ///  sort order.</description></item></list>
    /// </returns>
    int IComparable.CompareTo( object obj )
    {
        return (( IComparable<Unit> ) this).CompareTo( ( Unit ) obj );
    }

    /// <summary>
    /// Compares the passed unit to the current one. Allows sorting units of the same type.
    /// </summary>
    /// <remarks>   Only compatible units can be compared. </remarks>
    /// <param name="other">    An object to compare with this instance. </param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return value has
    /// these meanings:
    ///
    ///  <list type="table"><listheader><term> Value</term><description>
    ///  Meaning</description></listheader><item><term> Less than zero</term><description> This
    ///  instance precedes <paramref name="other" /> in the sort
    ///  order.</description></item><item><term> Zero</term><description> This instance occurs in the
    ///  same position in the sort order as
    ///  <paramref name="other" />.</description></item><item><term> Greater than
    ///  zero</term><description> This instance follows <paramref name="other" /> in the sort
    ///  order.</description></item></list>
    /// </returns>
    int IComparable<Unit>.CompareTo( Unit? other )
    {
        other ??= Unit.None;
        this.AssertCompatibility( other );
        return this.Factor < other.Factor ? -1 : this.Factor > other.Factor ? +1 : 0;
    }

    /// <summary>   Less-than comparison operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator <( Unit? left, Unit? right )
    {
        return left is null ? right is not null : left.CompareTo( right ) < 0;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer
    /// that indicates whether the current instance precedes, follows, or occurs in the same position
    /// in the sort order as the other object.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="other">    An object to compare with this instance. </param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return value has
    /// these meanings:
    ///
    ///  <list type="table"><listheader><term> Value</term><description>
    ///  Meaning</description></listheader><item><term> Less than zero</term><description> This
    ///  instance precedes <paramref name="other" /> in the sort
    ///  order.</description></item><item><term> Zero</term><description> This instance occurs in the
    ///  same position in the sort order as
    ///  <paramref name="other" />.</description></item><item><term> Greater than
    ///  zero</term><description> This instance follows <paramref name="other" /> in the sort
    ///  order.</description></item></list>
    /// </returns>
    private int CompareTo( Unit? other )
    {
        return this.CompareTo( other );
    }

    /// <summary>   Less-than-or-equal comparison operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator <=( Unit? left, Unit? right )
    {
        return left is null || left.CompareTo( right ) <= 0;
    }

    /// <summary>   Greater-than comparison operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator >( Unit? left, Unit? right )
    {
        return left is not null && left.CompareTo( right ) > 0;
    }

    /// <summary>   Greater-than-or-equal comparison operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator >=( Unit? left, Unit? right )
    {
        return left is null ? right is null : left.CompareTo( right ) >= 0;
    }

    #endregion

    #region " iserializable members "

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The <see cref="SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  A StreamingContext to process. </param>
    internal Unit( SerializationInfo info, StreamingContext context )
    {
        // Retrieve data from serialization:
        this.Name = info.GetString( nameof( Unit.Name ) );
        this.Symbol = info.GetString( nameof( Unit.Symbol ) );
        this.Factor = Convert.ToDouble( info.GetString( nameof( Unit.Factor ) ) );
        this.IsNamed = Convert.ToBoolean( info.GetString( nameof( Unit.IsNamed ) ) );
        this.UnitType = new UnitType( info, context );
    }

    /// <summary>
    /// Populates a <see cref="SerializationInfo" /> with the data
    /// needed to serialize the target object.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The <see cref="SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  The destination (see
    ///                         <see cref="StreamingContext" />) for
    ///                         this serialization. </param>
    [System.Security.SecurityCritical()]
    void ISerializable.GetObjectData( SerializationInfo info, StreamingContext context )
    {
        this.AddValues( info, context );
    }

    /// <summary>   Adds the values to 'info'. </summary>
    /// <remarks>   David, 2022-01-29. </remarks>
    /// <param name="info">     The <see cref="SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  The destination (see
    ///                         <see cref="StreamingContext" />) for
    ///                         this serialization. </param>
    internal void AddValues( SerializationInfo info, StreamingContext context )
    {
        if ( info is not null )
        {
            info.AddValue( nameof( Unit.Name ), this.Name );
            info.AddValue( nameof( Unit.Symbol ), this.Symbol );
            info.AddValue( nameof( Unit.Factor ), this.Factor );
            info.AddValue( nameof( Unit.IsNamed ), this.IsNamed );
            this.UnitType.AddValues( info, context );
        }
    }

    #endregion
}


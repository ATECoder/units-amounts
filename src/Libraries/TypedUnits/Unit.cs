// file:	TypedUnits\Unit.cs
//
// summary:	Implements the unit class

namespace Arebis.UnitsAmounts
{
    using System;

    /// <summary>   UNit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    public sealed class Unit : IComparable, IComparable<Unit>, IEquatable<Unit>, IFormattable
    {

        #region " CONSTRUCTION "

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unit"> The unit. </param>
        public Unit( Unit unit )
            : this( Valid( unit ).Name, Valid( unit ).Symbol, Valid( unit ).Factor, Valid( unit ).UnitType, Valid( unit ).IsNamed )
        {
        }

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
        {
        }

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="name">     Gets the name of the unit. </param>
        /// <param name="symbol">   Gets the symbol of the unit. </param>
        /// <param name="baseUnit"> The base unit. </param>
        public Unit( string name, string symbol, Unit baseUnit )
            : this( name, symbol, baseUnit.Factor, baseUnit.UnitType, true )
        {
        }

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
        public static Unit None { get; } = new Unit( String.Empty, String.Empty, UnitType.None );

        #endregion Constructor methods

        #region " PUBLIC IMPLEMENTATION "

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
            {
                throw new UnitConversionException( this, compatibleUnit );
            }
        }

        /// <summary>   Checks whether the passed unit is compatible with this one. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="otherUnit">    The other unit. </param>
        /// <returns>   True if compatible to, false if not. </returns>
        public bool IsCompatibleTo( Unit otherUnit ) => this.UnitType == (otherUnit ?? Unit.None).UnitType;

        /// <summary>
        /// Returns a unit by raising the present unit to the specified power.
        /// I.e. meter.Power(3) would return a cubic meter unit.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The value. </param>
        /// <returns>   A Unit. </returns>
        public Unit Power( int value ) => new( String.Concat( '(', this.Name, '^', value, ')' ), this.Symbol + '^' + value, ( double ) Math.Pow( ( double ) this.Factor, ( double ) value ), this.UnitType.Power( value ), false );

        /// <summary>   Tests equality of both objects. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="obj">  The object to compare with the current object. </param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public override bool Equals( object obj ) => this.Equals( obj as Unit );


        /// <summary>   Tests equality of both objects. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="other">    The unit to compare to this object. </param>
        /// <returns>   <c>true</c> if the objects are considered equal, false if they are not. </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0075:Simplify conditional expression", Justification = "<Pending>" )]
        public bool Equals( Unit other )
        {
            return other is null ? false : this.Factor.Equals( other.Factor ) && this.UnitType.Equals( other.UnitType );
        }

        /// <summary>   Returns the hash code of this unit. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A hash code for the current object. </returns>
        public override int GetHashCode() => this.Factor.GetHashCode() ^ this.UnitType.GetHashCode();

        /// <summary>   Returns a string representation of the unit. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A string that represents the current object. </returns>
        public override string ToString() => this.ToString( null, null );

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
        ///                                                          <see cref="T:System.IFormattable" />
        ///                                                          implementation. </param>
        /// <returns>   A string that represents this object. </returns>
        public string ToString( string format ) => this.ToString( format, null );

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
        /// <returns>   A string that represents this object. </returns>
        public string ToString( IFormatProvider formatProvider ) => this.ToString( null, formatProvider );

        /// <summary>   Returns a string representation of the unit. </summary>
        /// <remarks>   The format string can be either 'UN' (Unit Name) or 'US' (Unit Symbol). </remarks>
        /// <param name="format">           The format to use.  
        ///                                 
        ///                                  -or-  
        ///                                 
        ///                                  A null reference (<see langword="Nothing" /> in Visual
        ///                                  Basic) to use the default format defined for the type of the
        ///                                  <see cref="T:System.IFormattable" /> implementation. </param>
        /// <param name="formatProvider">   The provider to use to format the value.  
        ///                                 
        ///                                  -or-  
        ///                                 
        ///                                  A null reference (<see langword="Nothing" /> in Visual
        ///                                  Basic) to obtain the numeric format information from the
        ///                                  current locale setting of the operating system. </param>
        /// <returns>   The value of the current instance in the specified format. </returns>
        public string ToString( string format, IFormatProvider formatProvider )
        {
            if ( format == null )
            {
                format = "US";
            }

            if ( formatProvider != null )
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

        #region " OPERATOR OVERLOADS "

        /// <summary>   Equality operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator ==( Unit left, Unit right ) =>
            // return ((object)left == (object)right) || ((object)left != null && (object)right != null && left.Equals(right));
            object.ReferenceEquals( ( object ) left, ( object ) right ) || (!(left is null) && left.Equals( right ));

        /// <summary>   Inequality operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator !=( Unit left, Unit right ) => (( object ) left != ( object ) right) || left is null || right is null || !left.Equals( right );

        /// <summary>   Multiplies. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   A Unit. </returns>
        public static Unit Multiply( Unit left, Unit right )
        {
            left ??= Unit.None;
            right ??= Unit.None;
            return new Unit( String.Concat( '(', left.Name, '*', right.Name, ')' ), left.Symbol + '*' + right.Symbol, left.Factor * right.Factor, left.UnitType * right.UnitType, false );
        }

        /// <summary>   Multiplication operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first value to multiply. </param>
        /// <param name="right">    The second value to multiply. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator *( Unit left, Unit right )
        {
            left ??= Unit.None;
            right ??= Unit.None;
            return new Unit( String.Concat( '(', left.Name, '*', right.Name, ')' ), left.Symbol + '*' + right.Symbol, left.Factor * right.Factor, left.UnitType * right.UnitType, false );
        }

        /// <summary>   Multiplication operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first value to multiply. </param>
        /// <param name="right">    The second value to multiply. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator *( Unit left, double right ) => right * left;

        /// <summary>   Multiplication operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first value to multiply. </param>
        /// <param name="right">    The second value to multiply. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator *( double left, Unit right )
        {
            right ??= Unit.None;
            return new Unit( String.Concat( '(', left.ToString(), '*', right.Name, ')' ), left.ToString() + '*' + right.Symbol, left * right.Factor, right.UnitType, false );
        }

        /// <summary>   Division operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The numerator. </param>
        /// <param name="right">    The denominator. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator /( Unit left, Unit right )
        {
            left ??= Unit.None;
            right ??= Unit.None;
            return new Unit( String.Concat( '(', left.Name, '/', right.Name, ')' ), left.Symbol + '/' + right.Symbol, left.Factor / right.Factor, left.UnitType / right.UnitType, false );
        }

        /// <summary>   Division operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The numerator. </param>
        /// <param name="right">    The denominator. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator /( double left, Unit right )
        {
            right ??= Unit.None;
            return new Unit( String.Concat( '(', left.ToString(), '*', right.Name, ')' ), left.ToString() + '*' + right.Symbol, left / right.Factor, right.UnitType.Power( -1 ), false );
        }

        /// <summary>   Division operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The numerator. </param>
        /// <param name="right">    The denominator. </param>
        /// <returns>   The result of the operation. </returns>
        public static Unit operator /( Unit left, double right )
        {
            left ??= Unit.None;
            return new Unit( String.Concat( '(', left.Name, '/', right.ToString(), ')' ), left.Symbol + '/' + right.ToString(), left.Factor / right, left.UnitType, false );
        }

        #endregion Operator overloads

        #region " ICOMPARABLE IMPLEMENTATION "

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
        int IComparable.CompareTo( object obj ) => (( IComparable<Unit> ) this).CompareTo( ( Unit ) obj );

        /// <summary>
        /// Compares the passed unit to the current one. Allows sorting units of the same type.
        /// </summary>
        /// <remarks>   Only compatible units can be compared. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
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
        int IComparable<Unit>.CompareTo( Unit other )
        {
            if ( other is null )
            {
                throw new ArgumentNullException( nameof( other ) );
            }

            this.AssertCompatibility( other );
            return this.Factor < other.Factor ? -1 : this.Factor > other.Factor ? +1 : 0;
        }

        /// <summary>   Less-than comparison operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator <( Unit left, Unit right ) => left is null ? right is object : left.CompareTo( right ) < 0;

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
        private int CompareTo( Unit other ) => this.CompareTo( other );

        /// <summary>   Less-than-or-equal comparison operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator <=( Unit left, Unit right ) => left is null || left.CompareTo( right ) <= 0;

        /// <summary>   Greater-than comparison operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator >( Unit left, Unit right ) => left is object && left.CompareTo( right ) > 0;

        /// <summary>   Greater-than-or-equal comparison operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator >=( Unit left, Unit right ) => left is null ? right is null : left.CompareTo( right ) >= 0;

        #endregion 
    }
}


namespace Arebis.UnitsAmounts
{
    using System;
    using System.Linq;

    /// <summary>   Amount. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    public sealed class Amount :
        ICloneable,
        IComparable,
        IComparable<Amount>,
        IConvertible,
        IEquatable<Amount>,
        IFormattable,
        IUnitConsumer
    {

        #region" CONSTRUCTION "

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="value">    The raw value of the amount. </param>
        /// <param name="unit">     The unit of the amount. </param>
        public Amount( double value, Unit unit )
        {
            if ( unit is null )
            {
                throw new ArgumentNullException( nameof( unit ) );
            }

            this.Value = value;
            this.Unit = unit;
        }

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="value">    The amount. </param>
        public Amount( Amount value )
        {
            if ( value is null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            this.Value = value.Value;
            this.Unit = value.Unit;
        }


        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The raw value of the amount. </param>
        /// <param name="unitName"> Name of the unit. </param>
        public Amount( double value, string unitName )
        {
            this.Value = value;
            this.Unit = UnitManager.GetUnitByName( unitName );
        }

        /// <summary>   Zeros. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unit"> The unit of the amount. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Zero( Unit unit ) => new( 0.0, unit );

        /// <summary>   Zeros. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unitName"> Name of the unit. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Zero( string unitName ) => new( 0.0, unitName );

        #endregion Constructor methods

        #region " PUBLIC IMPLEMENTATION "

        /// <summary>   The equality precision. </summary>
        private static int _EqualityPrecision = 8;

        /// <summary>   The precision to which two amounts are considered equal. </summary>
        /// <value> The equality precision. </value>
        public static int EqualityPrecision
        {
            get => Amount._EqualityPrecision;
            set => Amount._EqualityPrecision = value;
        }

        /// <summary>   Gets or sets the raw value of the amount. </summary>
        /// <value> The value. </value>
        public double Value { get; private set; }


        /// <summary>   Gets the unit of the amount. </summary>
        /// <value> The unit. </value>
        public Unit Unit { get; }

        /// <summary>   Returns a unit that matches this amount. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A Unit. </returns>
        public Unit AsUnit() => new( this.Value + "*" + this.Unit.Name, this.Value + "*" + this.Unit.Symbol, this.Value * this.Unit );

        /// <summary>   Returns a clone of the Amount object. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A copy of this object. </returns>
        public object Clone() =>
            // Actually, as Amount is immutable, it can safely return itself:
            this;

        /// <summary>
        /// Returns a matching amount converted to the given unit and rounded up to the given number of
        /// decimals.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unitName"> Name of the unit. </param>
        /// <param name="decimals"> The decimals. </param>
        /// <returns>   An Amount. </returns>
        public Amount ConvertedTo( string unitName, int decimals ) => this.ConvertedTo( UnitManager.GetUnitByName( unitName ), decimals );

        /// <summary>
        /// Returns a matching amount converted to the given unit and rounded up to the given number of
        /// decimals.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unit">     The unit of the amount. </param>
        /// <param name="decimals"> The decimals. </param>
        /// <returns>   An Amount. </returns>
        public Amount ConvertedTo( Unit unit, int decimals ) => new( Math.Round( UnitManager.ConvertTo( this, unit ).Value, decimals ), unit );

        /// <summary>   Returns a matching amount converted to the given unit. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unitName"> Name of the unit. </param>
        /// <returns>   An Amount. </returns>
        public Amount ConvertedTo( string unitName ) => this.ConvertedTo( UnitManager.GetUnitByName( unitName ) );

        /// <summary>   Returns a matching amount converted to the given unit. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unit"> The unit of the amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount ConvertedTo( Unit unit ) =>
            // Let UnitManager perform conversion:
            UnitManager.ConvertTo( this, unit );

        /// <summary>
        /// Splits this amount into integral values of the given units except for the last amount which
        /// is rounded up to the number of decimals given.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="units">    The units. </param>
        /// <param name="decimals"> The decimals. </param>
        /// <returns>   An Amount[]. </returns>
        public Amount[] Split( Unit[] units, int decimals )
        {
            if ( units == null )
            {
                throw new ArgumentNullException( nameof( units ) );
            }

            var amounts = new Amount[units.Length];
            var rest = this;

            // Truncate for all but the last unit:
            for ( var i = 0; i < (units.Length - 1); i++ )
            {
                amounts[i] = ( Amount ) rest.ConvertedTo( units[i] ).MemberwiseClone();
                amounts[i].Value = Math.Truncate( amounts[i].Value );
                rest -= amounts[i];
            }

            // Handle the last unit:
            amounts[units.Length - 1] = rest.ConvertedTo( units[^1], decimals );

            return amounts;
        }


        /// <summary>   Combines the given amounts to an outcome amount of the specified unit. </summary>
        /// <remarks>   David, 2020-03-07. </remarks>
        /// <param name="amounts">  The amounts. </param>
        /// <param name="unit">     The unit of the amount. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Combine( Amount[] amounts, Unit unit )
        {
            Amount result = new( 0, unit );
            foreach ( Amount amnt in amounts )
            {
                result += amnt;
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current
        /// <see cref="T:System.Object" />.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="obj">  The object to compare with the current object. </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="T:System.Object" /> is equal to the current
        /// <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        public override bool Equals( object obj ) => this.Equals( obj as Amount );

        /// <summary>   Tests if this Amount is considered equal to another. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="UnitConversionException">  Thrown when a Unit Conversion error condition
        ///                                             occurs. </exception>
        /// <param name="other">    The amount to compare to this object. </param>
        /// <returns>   <c>true</c> if the objects are considered equal, false if they are not. </returns>
        public bool Equals( Amount other )
        {
            if ( other is null )
            {
                return false;
            }
            // Check value:
            try
            {
                return Math.Round( this.Value, Amount._EqualityPrecision )
                    == Math.Round( other.ConvertedTo( this.Unit ).Value, Amount._EqualityPrecision );
            }
            catch ( UnitConversionException e )
            {
                if ( e == null )
                {
                    throw;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>   Serves as a hash function for a particular type. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A hash code for the current <see cref="T:System.Object" />. </returns>
        public override int GetHashCode() => this.Value.GetHashCode() ^ this.Unit.GetHashCode();

        /// <summary>
        /// Shows the default string representation of the amount. (The default format string is "GG").
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A string that represents this object. </returns>
        public override string ToString() => this.ToString( ( string ) null, ( IFormatProvider ) null );

        /// <summary>
        /// Shows a string representation of the amount, formatted according to the passed format string.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="format">   The format to use.-or- A null reference (Nothing in Visual Basic)
        ///                         to use the default format defined for the type of the
        ///                         <see cref="T:System.IFormattable" />implementation. </param>
        /// <returns>   A string that represents this object. </returns>
        public string ToString( string format ) => this.ToString( format, ( IFormatProvider ) null );

        /// <summary>
        /// Shows the default string representation of the amount using the given format provider.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="formatProvider">   An <see cref="T:System.IFormatProvider" /> interface
        ///                                 implementation that supplies culture-specific formatting
        ///                                 information. </param>
        /// <returns>   A string that represents this object. </returns>
        public string ToString( IFormatProvider formatProvider ) => this.ToString( ( string ) null, formatProvider );

        /// <summary>
        /// Shows a string representation of the amount, formatted according to the passed format string,
        /// using the given format provider.
        /// </summary>
        /// <remarks>
        /// Valid format strings are 'GG', 'GN', 'GS', 'NG', 'NN', 'NS' (where the first letter
        /// represents the value formatting (General, Numeric), and the second letter represents the unit
        /// formatting (General, Name, Symbol)), or a custom number format with 'UG', 'UN' or 'US'
        /// (UnitGeneral, UnitName or UnitSymbol) representing the unit (i.e. "#,##0.00 UL"). The format
        /// string can also contains a '|' followed by a unit to convert to.
        /// </remarks>
        /// <param name="format">           The format to use.-or- A null reference (Nothing in Visual
        ///                                 Basic)
        ///                                 to use the default format defined for the type of the
        ///                                 <see cref="T:System.IFormattable" />
        ///                                 implementation. </param>
        /// <param name="formatProvider">   The provider to use to format the value.-or- A null reference
        ///                                 (Nothing in Visual Basic) to obtain the numeric format
        ///                                 information from the current locale setting of the operating
        ///                                 system. </param>
        /// <returns>   A string that represents this object. </returns>
        public string ToString( string format, IFormatProvider formatProvider )
        {
            if ( format == null )
            {
                format = "GG";
            }

            if ( formatProvider != null )
            {
                if ( formatProvider.GetFormat( this.GetType() ) is ICustomFormatter formatter )
                {
                    return formatter.Format( format, this, formatProvider );
                }
            }

            var formats = format.Split( '|' );
            var amount = this;
            if ( formats.Length >= 2 )
            {
                amount = formats[1] == "?" ? amount.ConvertedTo( UnitManager.ResolveToNamedUnit( amount.Unit, true ) ) : amount.ConvertedTo( formats[1] );
            }

            format = formats[0];
            if ( format.StartsWith( "{", StringComparison.OrdinalIgnoreCase ) )
            {
                return format.Split( ' ' ).Length == 2
                    ? String.Format( formatProvider, format, amount.Value, amount.Unit ).TrimEnd( null )
                    : String.Format( formatProvider, format, amount.Value ).TrimEnd( null );
            }
            else
            {
                switch ( format )
                {
                    case "GG":
                        return String.Format( formatProvider, "{0:G} {1}", amount.Value, amount.Unit ).TrimEnd( null );
                    case "GN":
                        return String.Format( formatProvider, "{0:G} {1:UN}", amount.Value, amount.Unit ).TrimEnd( null );
                    case "GS":
                        return String.Format( formatProvider, "{0:G} {1:US}", amount.Value, amount.Unit ).TrimEnd( null );
                    case "NG":
                        return String.Format( formatProvider, "{0:N} {1}", amount.Value, amount.Unit ).TrimEnd( null );
                    case "NN":
                        return String.Format( formatProvider, "{0:N} {1:UN}", amount.Value, amount.Unit ).TrimEnd( null );
                    case "NS":
                        return String.Format( formatProvider, "{0:N} {1:US}", amount.Value, amount.Unit ).TrimEnd( null );
                    default:
                        formats[0] = formats[0].Replace( "UG", "\"" + amount.Unit.ToString( "", formatProvider ) + "\"" );
                        formats[0] = formats[0].Replace( "UN", "\"" + amount.Unit.ToString( "UN", formatProvider ) + "\"" );
                        formats[0] = formats[0].Replace( "US", "\"" + amount.Unit.ToString( "US", formatProvider ) + "\"" );
                        return amount.Value.ToString( formats[0], formatProvider ).TrimEnd( null );
                }
            }
        }

        /// <summary>
        /// Static convenience ToString method, returns ToString of the amount, or empty string if amount
        /// is null.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <returns>   A string that represents this object. </returns>
        public static string ToString( Amount amount ) => ToString( amount, ( string ) null, ( IFormatProvider ) null );

        /// <summary>
        /// Static convenience ToString method, returns ToString of the amount, or empty string if amount
        /// is null.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <param name="format">   Describes the format to use. </param>
        /// <returns>   A string that represents this object. </returns>
        public static string ToString( Amount amount, string format ) => ToString( amount, format, ( IFormatProvider ) null );

        /// <summary>
        /// Static convenience ToString method, returns ToString of the amount, or empty string if amount
        /// is null.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">           The amount. </param>
        /// <param name="formatProvider">   The format provider. </param>
        /// <returns>   A string that represents this object. </returns>
        public static string ToString( Amount amount, IFormatProvider formatProvider ) => ToString( amount, ( string ) null, formatProvider );

        /// <summary>
        /// Static convenience ToString method, returns ToString of the amount, or empty string if amount
        /// is null.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">           The amount. </param>
        /// <param name="format">           Describes the format to use. </param>
        /// <param name="formatProvider">   The format provider. </param>
        /// <returns>   A string that represents this object. </returns>
        public static string ToString( Amount amount, string format, IFormatProvider formatProvider )
        {
            return amount is null ? String.Empty : amount.ToString( format, formatProvider );
        }

        #endregion Public implementation

        #region " MATHEMATICAL OPERATIONS "

        /// <summary>   Adds this with the amount (= this + amount). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount Add( Amount amount ) => this + amount;

        /// <summary>   Negates this (= -this). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   An Amount. </returns>
        public Amount Negate() => -this;

        /// <summary>   Multiply this with amount (= this * amount). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount Multiply( Amount amount ) => this * amount;

        /// <summary>   Multiply this with value (= this * value). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The raw value of the amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount Multiply( double value ) => this * value;

        /// <summary>   Divides this by amount (= this / amount). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="amount">   The amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount DivideBy( Amount amount ) => this / amount;

        /// <summary>   Divides this by value (= this / value). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The raw value of the amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount DivideBy( double value ) => this / value;

        /// <summary>   Returns 1 over this amount (= 1 / this). </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   An Amount. </returns>
        public Amount Inverse() => 1.0 / this;

        /// <summary>   Raises this amount to the given power. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The raw value of the amount. </param>
        /// <returns>   An Amount. </returns>
        public Amount Power( int value ) => new( Math.Pow( this.Value, value ), this.Unit.Power( value ) );

        #endregion Mathematical operations

        #region " OPERATOR OVERLOADS "

        /// <summary>   Compares two amounts. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator ==( Amount left, Amount right ) => object.ReferenceEquals( ( object ) left, ( object ) right ) || (!(left is null) && left.Equals( right ));

        /// <summary>   Compares two amounts. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator !=( Amount left, Amount right ) => (!object.ReferenceEquals( ( object ) left, ( object ) right )) && (left is null || !left.Equals( right ));

        /// <summary>   Compares two Amount objects to determine their relative ordering. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>
        /// Negative if 'left' is less than 'right', 0 if they are equal, or positive if it is greater.
        /// </returns>
        public static int Compare( Amount left, Amount right )
        {
            if ( (left is null) && (right is null) )
            {
                return 0;
            }

            if ( left is null )
            {
                throw new ArgumentNullException( nameof( left ) );
            }

            if ( right is null )
            {
                throw new ArgumentNullException( nameof( right ) );
            }

            var rightConverted = right.ConvertedTo( left.Unit );
            return left == rightConverted ? 0 : left.Value < rightConverted.Value ? -1 : 1;
        }

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator <( Amount left, Amount right ) => Amount.Compare( left, right ) < 0;

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator <=( Amount left, Amount right ) => Amount.Compare( left, right ) <= 0;

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator >( Amount left, Amount right ) => Amount.Compare( left, right ) > 0;

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator >=( Amount left, Amount right ) => Amount.Compare( left, right ) >= 0;

        /// <summary>   Unary '+' operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator +( Amount right ) => right;

        /// <summary>   Plus. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="right">    The right. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Plus( Amount right ) => right;

        /// <summary>   Plus. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Plus( Amount left, Amount right )
        {
            if ( (left is null) || (right is null) )
            {
                return null;
            }

            left ??= Amount.Zero( (!(right is null)) ? right.Unit : Unit.None );
            right ??= Amount.Zero( left.Unit );
            return new Amount( left.Value + right.ConvertedTo( left.Unit ).Value, left.Unit );
        }

        /// <summary>   Additions two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator +( Amount left, Amount right )
        {
            if ( (left is null) || (right is null) )
            {
                return null;
            }

            left ??= Zero( (!(right is null)) ? right.Unit : Unit.None );
            right ??= Zero( left.Unit );
            return new Amount( left.Value + right.ConvertedTo( left.Unit ).Value, left.Unit );
        }

        /// <summary>   Unary '-' operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator -( Amount right )
        {
            return right is null ? null : right is null ? null : new Amount( -right.Value, right.Unit );
        }

        /// <summary>   Subtracts. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Subtract( Amount left, Amount right )
        {
            return (left is null) || (right is null) ? null : left + (-right);
        }

        /// <summary>   Subtracts two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator -( Amount left, Amount right )
        {
            return (left is null) || (right is null) ? null : left + (-right);
        }

        /// <summary>   Multiplies two amounts. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator *( Amount left, Amount right )
        {
            return (left is null) || (right is null)
                ? null
                : left is null ? null : right is null ? null : new Amount( left.Value * right.Value, left.Unit * right.Unit );
        }

        /// <summary>   Divides. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   An Amount. </returns>
        public static Amount Divide( Amount left, Amount right )
        {
            return (left is null) || (right is null)
                ? null
                : left is null ? null : right is null ? null : new Amount( left.Value / right.Value, left.Unit / right.Unit );
        }

        /// <summary>   Divides two amounts. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator /( Amount left, Amount right ) => Amount.Divide( left, right );

        /// <summary>   Multiplies an amount with a double value. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator *( Amount left, double right )
        {
            return left is null ? null : left is null ? null : new Amount( left.Value * right, left.Unit );
        }

        /// <summary>   Divides an amount by a double value. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator /( Amount left, double right )
        {
            return left is null ? null : left is null ? null : new Amount( left.Value / right, left.Unit );
        }

        /// <summary>   Multiplies a double value with an amount. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator *( double left, Amount right )
        {
            return right is null ? null : right is null ? null : new Amount( left * right.Value, right.Unit );
        }

        /// <summary>   Divides a double value by an amount. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The left. </param>
        /// <param name="right">    The right. </param>
        /// <returns>   The result of the operation. </returns>
        public static Amount operator /( double left, Amount right )
        {
            return right is null ? null : right is null ? null : new Amount( left / right.Value, 1.0 / right.Unit );
        }

        /// <summary>   Casts a double value to an amount expressed in the None unit. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The raw value of the amount. </param>
        /// <returns>   The result of the operation. </returns>
        public static explicit operator Amount( double value ) => new( value, Unit.None );

        /// <summary>   Casts an amount expressed in the None unit to a double. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="InvalidCastException"> Thrown when an object cannot be cast to a required
        ///                                         type. </exception>
        /// <param name="amount">   The amount. </param>
        /// <returns>   The result of the operation. </returns>
        public static explicit operator double?( Amount amount )
        {
            try
            {
                return amount is null ? null : ( double? ) amount.ConvertedTo( Unit.None ).Value;
            }
            catch ( UnitConversionException )
            {
                throw new InvalidCastException( "An amount can only be casted to a numeric type if it is expressed in a None unit." );
            }
        }

        #endregion Operator overloads

        #region " ICONVERTIBLE IMPLEMENTATION "

        /// <summary>   Returns the <see cref="T:System.TypeCode" /> for this instance. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>
        /// The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value
        /// type that implements this interface.
        /// </returns>
        TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

        /// <summary>
        /// Converts the value of this instance to an equivalent Boolean value using the specified
        /// culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   A Boolean value equivalent to the value of this instance. </returns>
        bool IConvertible.ToBoolean( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to boolean." );

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit unsigned integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 8-bit unsigned integer equivalent to the value of this instance. </returns>
        byte IConvertible.ToByte( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to byte." );

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified
        /// culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   A Unicode character equivalent to the value of this instance. </returns>
        char IConvertible.ToChar( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to Char." );

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.DateTime" />
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>
        /// A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.
        /// </returns>
        DateTime IConvertible.ToDateTime( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to Date Time." );

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.Decimal" />
        /// number using the specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>
        /// A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.
        /// </returns>
        decimal IConvertible.ToDecimal( IFormatProvider provider ) => ( decimal ) ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent double-precision floating-point number
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>
        /// A double-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        double IConvertible.ToDouble( IFormatProvider provider ) => ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit signed integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 16-bit signed integer equivalent to the value of this instance. </returns>
        short IConvertible.ToInt16( IFormatProvider provider ) => ( Int16 ) ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 32-bit signed integer equivalent to the value of this instance. </returns>
        int IConvertible.ToInt32( IFormatProvider provider ) => ( Int32 ) ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit signed integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 64-bit signed integer equivalent to the value of this instance. </returns>
        long IConvertible.ToInt64( IFormatProvider provider ) => ( Int64 ) ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit signed integer using the specified
        /// culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 8-bit signed integer equivalent to the value of this instance. </returns>
        sbyte IConvertible.ToSByte( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to signed byte." );

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>
        /// A single-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        float IConvertible.ToSingle( IFormatProvider provider ) => ( float ) ( double ) this;

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.String" />
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>
        /// A <see cref="T:System.String" /> instance equivalent to the value of this instance.
        /// </returns>
        string IConvertible.ToString( IFormatProvider provider ) => this.ToString( provider );

        /// <summary>
        /// Converts the value of this instance to an <see cref="T:System.Object" /> of the specified
        /// <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-
        /// specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <exception cref="InvalidCastException"> Thrown when an object cannot be cast to a required
        ///                                         type. </exception>
        /// <param name="conversionType">   The <see cref="T:System.Type" /> to which the value of this
        ///                                 instance is converted. </param>
        /// <param name="provider">         An <see cref="T:System.IFormatProvider" /> interface
        ///                                 implementation that supplies culture-specific formatting
        ///                                 information. </param>
        /// <returns>
        /// An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" />
        /// whose value is equivalent to the value of this instance.
        /// </returns>
        object IConvertible.ToType( Type conversionType, IFormatProvider provider )
        {
            return conversionType == typeof( Double )
                ? Convert.ToDouble( this )
                : conversionType == typeof( Single )
                    ? Convert.ToSingle( this )
                    : conversionType == typeof( Decimal )
                        ? Convert.ToDecimal( this )
                        : conversionType == typeof( Int16 )
                            ? Convert.ToInt16( this )
                            : conversionType == typeof( Int32 )
                                ? Convert.ToInt32( this )
                                : conversionType == typeof( Int64 )
                                    ? Convert.ToInt64( this )
                                    : ( object ) (conversionType == typeof( String )
                                        ? Convert.ToString( this, provider )
                                        : throw new InvalidCastException( $"An Amount cannot be converted to the requested type {conversionType}." ));
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit unsigned integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 16-bit unsigned integer equivalent to the value of this instance. </returns>
        ushort IConvertible.ToUInt16( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to unsigned Int16." );

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 32-bit unsigned integer equivalent to the value of this instance. </returns>
        uint IConvertible.ToUInt32( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to unsigned Int32." );

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit unsigned integer using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="provider"> An <see cref="T:System.IFormatProvider" /> interface implementation
        ///                         that supplies culture-specific formatting information. </param>
        /// <returns>   An 64-bit unsigned integer equivalent to the value of this instance. </returns>
        ulong IConvertible.ToUInt64( IFormatProvider provider ) => throw new InvalidCastException( "An Amount cannot be converted to unsigned Int64." );

        #endregion IConvertible implementation

        #region " ICOMPARABLE IMPLEMENTATION "

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="obj">  An object to compare with this instance. </param>
        /// <returns>
        /// Negative if this object is less than the other, 0 if they are equal, or positive if this is
        /// greater.
        /// </returns>
        int IComparable.CompareTo( object obj )
        {
            return obj is not Amount other ? +1 : (( IComparable<Amount> ) this).CompareTo( other );
        }

        /// <summary>   Compares two amounts of compatible units. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="other">    Another instance to compare. </param>
        /// <returns>
        /// Negative if this object is less than the other, 0 if they are equal, or positive if this is
        /// greater.
        /// </returns>
        int IComparable<Amount>.CompareTo( Amount other )
        {
            return this < other ? -1 : this > other ? +1 : 0;
        }

        #endregion 
    }
}

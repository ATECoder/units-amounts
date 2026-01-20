using System.Runtime.Serialization;
using System.Text;

namespace cc.isr.UnitsAmounts;

/// <summary>   a unit type. This class cannot be inherited. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
[Serializable]
public sealed class UnitType : ISerializable
{
    /// <summary>   The cached hash code. </summary>
    [NonSerialized]
    private int _cachedHashCode;

    #region " construction "

    /// <summary>   Default constructor. Required for serialization. </summary>
    public UnitType() : this( UnitType.None.ToString() )
    { }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="unitTypeName"> Name of the unit type. </param>
    public UnitType( string unitTypeName )
    {
        int unitIndex = UnitType.GetBaseUnitIndex( unitTypeName );
        this.BaseUnitIndices = new short[unitIndex + 1];
        this.BaseUnitIndices[unitIndex] = 1;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="indicesLength">    Length of the indices. </param>
    private UnitType( int indicesLength ) => this.BaseUnitIndices = new short[indicesLength];

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="baseUnitIndices">  The base unit indices. </param>
    public UnitType( short[] baseUnitIndices ) => this.BaseUnitIndices = ( short[] ) baseUnitIndices.Clone();

    /// <summary>   Type of the none unit. </summary>
    private static readonly UnitType _noneUnitType = new( 0 );

    /// <summary>   Gets the none. </summary>
    /// <value> The none. </value>
    public static UnitType None => UnitType._noneUnitType;

    #endregion

    #region " unit type base units "

    /// <summary>   The base unit indices. </summary>
    /// <remarks> Defines the power of each indexes unit name. </remarks>
    public short[] BaseUnitIndices { get; set; }

    /// <summary>   The base unit type lock. </summary>
    private static readonly ReaderWriterLock _baseUnitTypeLock = new();

    /// <summary>   List of names of the base unit types. </summary>
    private static readonly IList<string> _baseUnitTypeNames = [];

    /// <summary>   Gets base unit name. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="index">    Zero-based index of the. </param>
    /// <returns>   The base unit name. </returns>
    private static string GetBaseUnitName( int index )
    {
        // Lock baseUnitTypeNames:
        _baseUnitTypeLock.AcquireReaderLock( 2000 );

        try
        {
            return _baseUnitTypeNames[index];
        }
        finally
        {
            // Release lock:
            _baseUnitTypeLock.ReleaseReaderLock();
        }
    }

    /// <summary>   Gets the zero-based index of the base unit. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="unitTypeName"> Name of the unit type. </param>
    /// <returns>   The base unit index. </returns>
    private static int GetBaseUnitIndex( string unitTypeName )
    {
        // Verify unitTypeName does not contain pipe char (which is used in serializations):
        if ( unitTypeName.Contains( '|' ) )
        {
            throw new ArgumentException( "The name of a UnitType must not contain the '|' (pipe) character.", nameof( unitTypeName ) );
        }

        // Lock baseUnitTypeNames:
        _baseUnitTypeLock.AcquireReaderLock( 2000 );

        try
        {
            // Retrieve index of unitTypeName:
            int index = _baseUnitTypeNames.IndexOf( unitTypeName );

            // If not found, register unitTypeName:
            if ( index == -1 )
            {
                _ = _baseUnitTypeLock.UpgradeToWriterLock( 2000 );
                index = _baseUnitTypeNames.Count;
                _baseUnitTypeNames.Add( unitTypeName );
            }

            // Return index:
            return index;
        }
        finally
        {
            // Release lock:
            _ = _baseUnitTypeLock.ReleaseLock();
        }
    }

    #endregion

    #region " public implementation "

    /// <summary>   Returns the unit type raised to the specified power. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   An UnitType. </returns>
    public UnitType Power( int value )
    {
        UnitType result = new( this.BaseUnitIndices );
        for ( int i = 0; i < result.BaseUnitIndices.Length; i++ )
        {
            result.BaseUnitIndices[i] = ( short ) (result.BaseUnitIndices[i] * value);
        }

        return result;
    }

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <remarks>   David, 2022-01-29. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    public static bool Equals( UnitType left, UnitType right )
    {
        return left is not null && right is not null && left.Equals( right );
    }

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="obj">  The object to compare with the current object. </param>
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    public override bool Equals( object obj )
    {
        return this.Equals( obj as UnitType );
    }

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="other">    The unit type to compare to this object. </param>
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    public bool Equals( UnitType? other )
    {
        if ( other is null )
        {
            return false;
        }
        // Determine longest and shortest base Unit Index arrays:
        short[] longest, shortest;
        int leftLength = this.BaseUnitIndices.Length;
        int rightLength = other.BaseUnitIndices.Length;
        if ( leftLength > rightLength )
        {
            longest = this.BaseUnitIndices;
            shortest = other.BaseUnitIndices;
        }
        else
        {
            longest = other.BaseUnitIndices;
            shortest = this.BaseUnitIndices;
        }

        // Compare base Unit Indices array content:
        for ( int i = 0; i < shortest.Length; i++ )
        {
            if ( shortest[i] != longest[i] )
            {
                return false;
            }
        }

        for ( int i = shortest.Length; i < longest.Length; i++ )
        {
            if ( longest[i] != 0 )
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>   Serves as the default hash function. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   A hash code for the current object. </returns>
    public override int GetHashCode()
    {
        if ( this._cachedHashCode == 0 )
        {
            int hash = 0;
            for ( int i = 0; i < this.BaseUnitIndices.Length; i++ )
            {
                int factor = i + i + 1;
                hash += factor * factor * this.BaseUnitIndices[i] * this.BaseUnitIndices[i];
            }
            this._cachedHashCode = hash;
        }
        return this._cachedHashCode;
    }

    /// <summary>   Returns a string that represents the current object. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   A <see cref="string" /> that represents the current object. </returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        string sep = "";
        for ( int i = 0; i < this.BaseUnitIndices.Length; i++ )
        {
            if ( this.BaseUnitIndices[i] != 0 )
            {
                _ = sb.Append( sep );
                _ = sb.Append( UnitType.GetBaseUnitName( i ) );
                _ = sb.Append( '^' );
                _ = sb.Append( this.BaseUnitIndices[i] );
                sep = " * ";
            }
        }
        return sb.ToString();
    }

    #endregion

    #region " operator overloads "

    /// <summary>   Multiplication operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first value to multiply. </param>
    /// <param name="right">    The second value to multiply. </param>
    /// <returns>   The result of the operation. </returns>
    public static UnitType operator *( UnitType left, UnitType right )
    {
        UnitType result = new( Math.Max( left.BaseUnitIndices.Length, right.BaseUnitIndices.Length ) );
        left.BaseUnitIndices.CopyTo( result.BaseUnitIndices, 0 );
        for ( int i = 0; i < right.BaseUnitIndices.Length; i++ )
        {
            result.BaseUnitIndices[i] += right.BaseUnitIndices[i];
        }

        return result;
    }

    /// <summary>   Division operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The numerator. </param>
    /// <param name="right">    The denominator. </param>
    /// <returns>   The result of the operation. </returns>
    public static UnitType operator /( UnitType left, UnitType right )
    {
        UnitType result = new( Math.Max( left.BaseUnitIndices.Length, right.BaseUnitIndices.Length ) );
        left.BaseUnitIndices.CopyTo( result.BaseUnitIndices, 0 );
        for ( int i = 0; i < right.BaseUnitIndices.Length; i++ )
        {
            result.BaseUnitIndices[i] -= right.BaseUnitIndices[i];
        }

        return result;
    }

    /// <summary>   Equality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==( UnitType left, UnitType right )
    {
        return UnitType.Equals( left, right );
    }

    /// <summary>   Inequality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=( UnitType left, UnitType right )
    {
        return !UnitType.Equals( left, right );
    }

    #endregion Operator overloads

    #region " iserializable members "

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The <see cref="SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  A StreamingContext to process. </param>
    internal UnitType( SerializationInfo info, StreamingContext context )
    {
        // Retrieve data from serialization:
        int[] baseUnitIndexes = [.. info.GetString( "names" ).Split( ['|'], StringSplitOptions.RemoveEmptyEntries ).Select( UnitType.GetBaseUnitIndex )];
        short[] exponents = [.. info.GetString( "exponents" ).Split( ['|'], StringSplitOptions.RemoveEmptyEntries ).Select( x => Convert.ToInt16( x ) )];

        // Construct instance:
        if ( exponents.Length > 0 )
        {
            this.BaseUnitIndices = new short[baseUnitIndexes.Max() + 1];
            for ( int i = 0; i < exponents.Length; i++ )
            {
                this.BaseUnitIndices[baseUnitIndexes[i]] = exponents[i];
            }
        }
        else
        {
            this.BaseUnitIndices = [];
        }
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0060:Remove unused parameter", Justification = "<Pending>" )]
    internal void AddValues( SerializationInfo info, StreamingContext context )
    {
        if ( info is not null )
        {
            bool first = true;
            StringBuilder unitNames = new( this.BaseUnitIndices.Length * 8 );
            StringBuilder unitExponents = new( this.BaseUnitIndices.Length * 4 );
            for ( int i = 0; i < this.BaseUnitIndices.Length; i++ )
            {
                if ( this.BaseUnitIndices[i] != 0 )
                {
                    if ( !first )
                    {
                        _ = unitNames.Append( '|' );
                    }

                    _ = unitNames.Append( UnitType.GetBaseUnitName( i ) );
                    if ( !first )
                    {
                        _ = unitExponents.Append( '|' );
                    }

                    _ = unitExponents.Append( this.BaseUnitIndices[i] );
                    first = false;
                }
            }
            if ( info is not null )
            {
                info.AddValue( "names", unitNames.ToString() );
                info.AddValue( "exponents", unitExponents.ToString() );
            }
        }
    }

    #endregion
}


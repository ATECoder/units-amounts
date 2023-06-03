using System.Runtime.Serialization;
using System.Text;

namespace cc.isr.UnitsAmounts;

/// <summary>   a unit type. This class cannot be inherited. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
[Serializable]
public sealed class UnitType : ISerializable
{

    /// <summary>   The base unit indices. </summary>
    private readonly sbyte[] _baseUnitIndices;

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
        var unitIndex = GetBaseUnitIndex( unitTypeName );
        this._baseUnitIndices = new sbyte[unitIndex + 1];
        this._baseUnitIndices[unitIndex] = 1;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="indicesLength">    Length of the indices. </param>
    private UnitType( int indicesLength ) => this._baseUnitIndices = new sbyte[indicesLength];

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="baseUnitIndices">  The base unit indices. </param>
    private UnitType( sbyte[] baseUnitIndices ) => this._baseUnitIndices = ( sbyte[] ) baseUnitIndices.Clone();

    /// <summary>   Type of the none unit. </summary>
    private static readonly UnitType _noneUnitType = new( 0 );

    /// <summary>   Gets the none. </summary>
    /// <value> The none. </value>
    public static UnitType None => UnitType._noneUnitType;

    #endregion

    #region " unit type base units "

    /// <summary>   The base unit type lock. </summary>
    private static readonly ReaderWriterLock _baseUnitTypeLock = new();

    /// <summary>   List of names of the base unit types. </summary>
    private static readonly IList<string> _baseUnitTypeNames = new List<string>();

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
            var index = _baseUnitTypeNames.IndexOf( unitTypeName );

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
        var result = new UnitType( this._baseUnitIndices );
        for ( var i = 0; i < result._baseUnitIndices.Length; i++ )
        {
            result._baseUnitIndices[i] = ( sbyte ) (result._baseUnitIndices[i] * value);
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
    public static bool Equals( UnitType left, UnitType right ) => left is object && right is object && left.Equals( right );

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="obj">  The object to compare with the current object. </param>
    /// <returns>
    /// true if the specified object  is equal to the current object; otherwise, false.
    /// </returns>
    public override bool Equals( object obj ) => this.Equals( obj as UnitType );

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
        sbyte[] longest, shortest;
        var leftLength = this._baseUnitIndices.Length;
        var rightLength = other._baseUnitIndices.Length;
        if ( leftLength > rightLength )
        {
            longest = this._baseUnitIndices;
            shortest = other._baseUnitIndices;
        }
        else
        {
            longest = other._baseUnitIndices;
            shortest = this._baseUnitIndices;
        }

        // Compare base Unit Indices array content:
        for ( var i = 0; i < shortest.Length; i++ )
        {
            if ( shortest[i] != longest[i] )
            {
                return false;
            }
        }

        for ( var i = shortest.Length; i < longest.Length; i++ )
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
            var hash = 0;
            for ( var i = 0; i < this._baseUnitIndices.Length; i++ )
            {
                var factor = i + i + 1;
                hash += factor * factor * this._baseUnitIndices[i] * this._baseUnitIndices[i];
            }
            this._cachedHashCode = hash;
        }
        return this._cachedHashCode;
    }

    /// <summary>   Returns a string that represents the current object. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <returns>   A string that represents the current object. </returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        var sep = "";
        for ( var i = 0; i < this._baseUnitIndices.Length; i++ )
        {
            if ( this._baseUnitIndices[i] != 0 )
            {
                _ = sb.Append( sep );
                _ = sb.Append( GetBaseUnitName( i ) );
                _ = sb.Append( '^' );
                _ = sb.Append( this._baseUnitIndices[i] );
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
        var result = new UnitType( Math.Max( left._baseUnitIndices.Length, right._baseUnitIndices.Length ) );
        left._baseUnitIndices.CopyTo( result._baseUnitIndices, 0 );
        for ( var i = 0; i < right._baseUnitIndices.Length; i++ )
        {
            result._baseUnitIndices[i] += right._baseUnitIndices[i];
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
        var result = new UnitType( Math.Max( left._baseUnitIndices.Length, right._baseUnitIndices.Length ) );
        left._baseUnitIndices.CopyTo( result._baseUnitIndices, 0 );
        for ( var i = 0; i < right._baseUnitIndices.Length; i++ )
        {
            result._baseUnitIndices[i] -= right._baseUnitIndices[i];
        }

        return result;
    }

    /// <summary>   Equality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator ==( UnitType left, UnitType right ) => UnitType.Equals( left, right );

    /// <summary>   Inequality operator. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="left">     The first instance to compare. </param>
    /// <param name="right">    The second instance to compare. </param>
    /// <returns>   The result of the operation. </returns>
    public static bool operator !=( UnitType left, UnitType right ) => !UnitType.Equals( left, right );

    #endregion Operator overloads

    #region " iserializable members "

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The <see cref="T:System.Runtime.Serialization.SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  A StreamingContext to process. </param>
    internal UnitType( SerializationInfo info, StreamingContext context )
    {
        // Retrieve data from serialization:
        var baseUnitIndexes = info.GetString( "names" ).Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries )
            .Select( x => UnitType.GetBaseUnitIndex( x ) )
            .ToArray();
        var exponents = info.GetString( "exponents" ).Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries )
            .Select( x => Convert.ToSByte( x ) )
            .ToArray();

        // Construct instance:
        if ( exponents.Length > 0 )
        {
            this._baseUnitIndices = new sbyte[baseUnitIndexes.Max() + 1];
            for ( var i = 0; i < exponents.Length; i++ )
            {
                this._baseUnitIndices[baseUnitIndexes[i]] = exponents[i];
            }
        }
        else
        {
            this._baseUnitIndices = Array.Empty<sbyte>();
        }
    }

    /// <summary>
    /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data
    /// needed to serialize the target object.
    /// </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    /// <param name="info">     The <see cref="T:System.Runtime.Serialization.SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  The destination (see
    ///                         <see cref="T:System.Runtime.Serialization.StreamingContext" />) for
    ///                         this serialization. </param>
    [System.Security.SecurityCritical()]
    void ISerializable.GetObjectData( SerializationInfo info, StreamingContext context )
    {
        this.AddValues( info, context );
    }


    /// <summary>   Adds the values to 'info'. </summary>
    /// <remarks>   David, 2022-01-29. </remarks>
    /// <param name="info">     The <see cref="T:System.Runtime.Serialization.SerializationInfo" />
    ///                         to populate with data. </param>
    /// <param name="context">  The destination (see
    ///                         <see cref="T:System.Runtime.Serialization.StreamingContext" />) for
    ///                         this serialization. </param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0060:Remove unused parameter", Justification = "<Pending>" )]
    internal void AddValues( SerializationInfo info, StreamingContext context )
    {
        if ( info is object )
        {
            var first = true;
            var unitNames = new StringBuilder( this._baseUnitIndices.Length * 8 );
            var unitExponents = new StringBuilder( this._baseUnitIndices.Length * 4 );
            for ( var i = 0; i < this._baseUnitIndices.Length; i++ )
            {
                if ( this._baseUnitIndices[i] != 0 )
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

                    _ = unitExponents.Append( this._baseUnitIndices[i] );
                    first = false;
                }
            }
            if ( info is object )
            {
                info.AddValue( "names", unitNames.ToString() );
                info.AddValue( "exponents", unitExponents.ToString() );
            }
        }
    }


    #endregion
}



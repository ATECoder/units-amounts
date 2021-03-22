// file:	TypedUnits\UnitType.cs
//
// summary:	Implements the unit type class

namespace Arebis.UnitsAmounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;

    /// <summary>   (Serializable) a unit type. This class cannot be inherited. </summary>
    /// <remarks>   David, 2021-03-22. </remarks>
    [Serializable]
    public sealed class UnitType : ISerializable
    {
        #region " BASE UNIT TYPE SUPPORT "

        /// <summary>   The base unit type lock. </summary>
        private static readonly ReaderWriterLock BaseUnitTypeLock = new();
        /// <summary>   List of names of the base unit types. </summary>
        private static readonly IList<string> BaseUnitTypeNames = new List<string>();

        /// <summary>   Gets base unit name. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="index">    Zero-based index of the. </param>
        /// <returns>   The base unit name. </returns>
        private static string GetBaseUnitName( int index )
        {
            // Lock baseUnitTypeNames:
            BaseUnitTypeLock.AcquireReaderLock( 2000 );

            try
            {
                return BaseUnitTypeNames[index];
            }
            finally
            {
                // Release lock:
                BaseUnitTypeLock.ReleaseReaderLock();
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
            BaseUnitTypeLock.AcquireReaderLock( 2000 );

            try
            {
                // Retrieve index of unitTypeName:
                var index = BaseUnitTypeNames.IndexOf( unitTypeName );

                // If not found, register unitTypeName:
                if ( index == -1 )
                {
                    _ = BaseUnitTypeLock.UpgradeToWriterLock( 2000 );
                    index = BaseUnitTypeNames.Count;
                    BaseUnitTypeNames.Add( unitTypeName );
                }

                // Return index:
                return index;
            }
            finally
            {
                // Release lock:
                _ = BaseUnitTypeLock.ReleaseLock();
            }
        }

        #endregion BaseUnitType support

        /// <summary>   The base unit indices. </summary>
        private readonly sbyte[] _BaseUnitIndices;

        /// <summary>   The cached hash code. </summary>
        [NonSerialized]
        private int _CachedHashCode;

        #region " CONSTRUCTION "

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="unitTypeName"> Name of the unit type. </param>
        public UnitType( string unitTypeName )
        {
            var unitIndex = GetBaseUnitIndex( unitTypeName );
            this._BaseUnitIndices = new sbyte[unitIndex + 1];
            this._BaseUnitIndices[unitIndex] = 1;
        }

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="indicesLength">    Length of the indices. </param>
        private UnitType( int indicesLength ) => this._BaseUnitIndices = new sbyte[indicesLength];

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="baseUnitIndices">  The base unit indices. </param>
        private UnitType( sbyte[] baseUnitIndices ) => this._BaseUnitIndices = ( sbyte[] ) baseUnitIndices.Clone();

        /// <summary>   Constructor. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="info"> The <see cref="T:System.Runtime.Serialization.SerializationInfo" />
        ///                     to populate with data. </param>
        /// <param name="c">    A StreamingContext to process. </param>
        private UnitType( SerializationInfo info, StreamingContext c )
        {
            // Retrieve data from serialization:
            var tstoreexp = info.GetString( "indexes" ).Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries )
                .Select( x => Convert.ToSByte( x ) )
                .ToArray();
            var tstoreind = info.GetString( "names" ).Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries )
                .Select( x => UnitType.GetBaseUnitIndex( x ) )
                .ToArray();

            // Construct instance:
            if ( tstoreexp.Length > 0 )
            {
                this._BaseUnitIndices = new sbyte[tstoreind.Max() + 1];
                for ( var i = 0; i < tstoreexp.Length; i++ )
                {
                    this._BaseUnitIndices[tstoreind[i]] = tstoreexp[i];
                }
            }
            else
            {
                this._BaseUnitIndices = Array.Empty<sbyte>();
            }
        }

        /// <summary>   Type of the none unit. </summary>
        private static readonly UnitType NoneUnitType = new( 0 );

        /// <summary>   Gets the none. </summary>
        /// <value> The none. </value>
        public static UnitType None => UnitType.NoneUnitType;

        #endregion

        #region " PUBLIC IMPLEMENTATION "

        /// <summary>   Returns the unit type raised to the specified power. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="value">    The value. </param>
        /// <returns>   An UnitType. </returns>
        public UnitType Power( int value )
        {
            var result = new UnitType( this._BaseUnitIndices );
            for ( var i = 0; i < result._BaseUnitIndices.Length; i++ )
            {
                result._BaseUnitIndices[i] = ( sbyte ) (result._BaseUnitIndices[i] * value);
            }

            return result;
        }

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
        public bool Equals( UnitType other )
        {
            if ( other is null )
            {
                return false;
            }
            // Determine longest and shortest baseUnitUndice arrays:
            sbyte[] longest, shortest;
            var leftlen = this._BaseUnitIndices.Length;
            var rightlen = other._BaseUnitIndices.Length;
            if ( leftlen > rightlen )
            {
                longest = this._BaseUnitIndices;
                shortest = other._BaseUnitIndices;
            }
            else
            {
                longest = other._BaseUnitIndices;
                shortest = this._BaseUnitIndices;
            }

            // Compare baseUnitIndice array content:
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
            if ( this._CachedHashCode == 0 )
            {
                var hash = 0;
                for ( var i = 0; i < this._BaseUnitIndices.Length; i++ )
                {
                    var factor = i + i + 1;
                    hash += factor * factor * this._BaseUnitIndices[i] * this._BaseUnitIndices[i];
                }
                this._CachedHashCode = hash;
            }
            return this._CachedHashCode;
        }

        /// <summary>   Returns a string that represents the current object. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <returns>   A string that represents the current object. </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var sep = "";
            for ( var i = 0; i < this._BaseUnitIndices.Length; i++ )
            {
                if ( this._BaseUnitIndices[i] != 0 )
                {
                    _ = sb.Append( sep );
                    _ = sb.Append( GetBaseUnitName( i ) );
                    _ = sb.Append( '^' );
                    _ = sb.Append( this._BaseUnitIndices[i] );
                    sep = " * ";
                }
            }
            return sb.ToString();
        }

        #endregion 

        #region " OPERATOR OVERLOADS "

        /// <summary>   Multiplication operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first value to multiply. </param>
        /// <param name="right">    The second value to multiply. </param>
        /// <returns>   The result of the operation. </returns>
        public static UnitType operator *( UnitType left, UnitType right )
        {
            var result = new UnitType( Math.Max( left._BaseUnitIndices.Length, right._BaseUnitIndices.Length ) );
            left._BaseUnitIndices.CopyTo( result._BaseUnitIndices, 0 );
            for ( var i = 0; i < right._BaseUnitIndices.Length; i++ )
            {
                result._BaseUnitIndices[i] += right._BaseUnitIndices[i];
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
            var result = new UnitType( Math.Max( left._BaseUnitIndices.Length, right._BaseUnitIndices.Length ) );
            left._BaseUnitIndices.CopyTo( result._BaseUnitIndices, 0 );
            for ( var i = 0; i < right._BaseUnitIndices.Length; i++ )
            {
                result._BaseUnitIndices[i] -= right._BaseUnitIndices[i];
            }

            return result;
        }

        /// <summary>   Equality operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator ==( UnitType left, UnitType right ) =>
            // return ((object)left == (object)right) || ((object)left != null && (object)right != null && left.Equals(right));
            object.ReferenceEquals( ( object ) left, ( object ) right ) || (left is object && left.Equals( right ));

        /// <summary>   Inequality operator. </summary>
        /// <remarks>   David, 2021-03-22. </remarks>
        /// <param name="left">     The first instance to compare. </param>
        /// <param name="right">    The second instance to compare. </param>
        /// <returns>   The result of the operation. </returns>
        public static bool operator !=( UnitType left, UnitType right ) =>
            // return ((object)left != (object)right) || ((object)left == null || (object)right == null || !left.Equals(right));
            (!object.ReferenceEquals( ( object ) left, ( object ) right )) && (left is null || !left.Equals( right ));

        #endregion Operator overloads

        #region ISerializable Members

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
            var first = true;
            var sbn = new StringBuilder( this._BaseUnitIndices.Length * 8 );
            var sbx = new StringBuilder( this._BaseUnitIndices.Length * 4 );
            for ( var i = 0; i < this._BaseUnitIndices.Length; i++ )
            {
                if ( this._BaseUnitIndices[i] != 0 )
                {
                    if ( !first )
                    {
                        _ = sbn.Append( '|' );
                    }

                    _ = sbn.Append( UnitType.GetBaseUnitName( i ) );
                    if ( !first )
                    {
                        _ = sbx.Append( '|' );
                    }

                    _ = sbx.Append( this._BaseUnitIndices[i] );
                    first = false;
                }
            }
            if ( info is object )
            {
                info.AddValue( "names", sbn.ToString() );
                info.AddValue( "indexes", sbx.ToString() );
            }
        }


        #endregion
    }
}



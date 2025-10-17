namespace cc.isr.UnitsAmounts.Tests;

/// <summary>   Contains unit tests for Amounts. </summary>
/// <remarks>   2025-09-06. </remarks>
[TestClass]
public class AmountTests
{
    #region " initialize & cleanup "

    private UnitManager? _defaultUnitManager;
    private static readonly double[] _oneFifthWeekZeroSecondsArray = [1.0, 9.0, 36.0, 0.0];
    private static readonly double[] _oneFifthWeekSixtySecondsArray = [1.0, 9.0, 35.0, 60.0];

    /// <summary>   Sets up this unit test class prior to execution. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestInitialize()]
    public void InitializeBeforeEachTest()
    {
        Console.Write( "Resetting the Unit Manager instance..." );
        this._defaultUnitManager = UnitManager.Instance;
        UnitManager.Instance = new UnitManager();
        UnitManager.RegisterByAssembly( typeof( LengthUnits ).Assembly );
        Console.WriteLine( " done." );
    }

    /// <summary>   Cleans up after this unit test class has executed its tests. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestCleanup()]
    public void CleanupAfterEachTest()
    {
        UnitManager.Instance = this._defaultUnitManager!;
    }

    #endregion Initialize & cleanup

    /// <summary>   (Unit Test Method) tests construction 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Construction01Test()
    {
        Amount a = new( 100, nameof( VolumeUnits.Liter ) );
        Assert.AreEqual( 100.0, a.Value );
        Assert.AreEqual( nameof( VolumeUnits.Liter ), a.Unit.Name );
    }

    /// <summary>   (Unit Test Method) tests addition. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod]
    public void AdditionTest()
    {
        Amount a = new( 3000.0, LengthUnits.Meter );
        Amount? sum = new( 2000.0, LengthUnits.Meter );
        Amount expected = new( 5000.0, LengthUnits.Meter );

        sum += a;

        Console.WriteLine( "Sum = {0}", sum );
        Assert.AreEqual( expected, sum );
    }

    /// <summary>   (Unit Test Method) tests addition derived. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod]
    public void AdditionDerivedTest()
    {
        Amount a = new( 3000.0, LengthUnits.Meter );
        Amount? sum = new( 2.0, LengthUnits.Kilometer );
        Amount expected = new( 5.0, LengthUnits.Kilometer );

        sum += a;

        Console.WriteLine( "Sum = {0}", sum );
        Assert.AreEqual( expected, sum );
    }

    /// <summary>   (Unit Test Method) tests conversion 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Conversion01Test()
    {
        Amount speed = new( 120, LengthUnits.Kilometer / TimeUnits.Hour );
        Amount time = new( 15, TimeUnits.Minute );
        Amount? distance = (speed * time)!.ConvertedTo( LengthUnits.Kilometer, 4 );
        Assert.AreEqual( 30.0, distance.Value );
        Assert.AreEqual( LengthUnits.Kilometer.Name, distance.Unit.Name );
    }

    /// <summary>   (Unit Test Method) tests casting 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Casting01Test()
    {
        Amount a = ( Amount ) 350.0;
        Assert.AreEqual( new Amount( 350.0, Unit.None ), a );

        Amount b = new( 123.0, Unit.None );
        Assert.AreEqual( 123.0, ( double ) b! );

        Amount c = new( 500.0, LengthUnits.Meter / LengthUnits.Kilometer );
        Assert.AreEqual( 0.5, ( double ) c! );

        Assert.AreEqual( "15.3", (( Amount ) 15.3).ToString( CultureInfo.CurrentCulture ).Replace( ",", "." ) );
    }

    /// <summary>   (Unit Test Method) tests percentage 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Percentage01Test()
    {
        Unit percent = new( "percent", "%", 0.01 * Unit.None );

        Amount a = new( 15.0, percent );
        Amount b = new( 300.0, TimeUnits.Minute );

        Assert.AreEqual( "15 %", a.ToString( "0 US", CultureInfo.CurrentCulture ) );
        Assert.AreEqual( 0.15, ( double ) a! );
        Console.WriteLine( a * b );
        Assert.AreEqual( 45.0, (a * b)!.ConvertedTo( TimeUnits.Minute ).Value );
    }

    /// <summary>   (Unit Test Method) tests percentage 02. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Percentage02Test()
    {
        Unit percent = new( "percent", "%", 0.01 * Unit.None );

        Amount a = new( 2.0, LengthUnits.Meter );
        Amount b = new( 17.0, LengthUnits.Centimeter );

        Amount p = (b / a)!.ConvertedTo( percent );

        Assert.AreEqual( "8.50 %", p.ToString( "0.00 US", CultureInfo.InvariantCulture ) );
        Assert.AreEqual( 0.085, ( double ) p! );
    }

    /// <summary>   (Unit Test Method) tests power 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Power01Test()
    {
        Amount a = new( 12.0, LengthUnits.Meter );

        Assert.AreEqual( new Amount( 1.0, Unit.None ), a.Power( 0 ) );

        Assert.AreEqual( new Amount( 12.0, LengthUnits.Meter ), a.Power( 1 ) );
        Assert.AreEqual( new Amount( 144.0, SurfaceUnits.SquareMeter ), a.Power( 2 ) );
        Assert.AreEqual( new Amount( 1728.0, VolumeUnits.CubicMeter ), a.Power( 3 ) );

        Assert.AreEqual( new Amount( 1.0 / 12.0, Unit.None / LengthUnits.Meter ), a.Power( -1 ) );
        Assert.AreEqual( new Amount( 1.0 / 144.0, Unit.None / SurfaceUnits.SquareMeter ), a.Power( -2 ) );
        Assert.AreEqual( new Amount( 1.0 / 1728.0, Unit.None / VolumeUnits.CubicMeter ), a.Power( -3 ) );
    }

    /// <summary>   (Unit Test Method) tests split 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Split01Test()
    {
        Amount a = new( 146.0, TimeUnits.Second );
        Amount[] values = a.Split( [TimeUnits.Hour, TimeUnits.Minute, TimeUnits.Second], 0 );

        string separator = "";
        foreach ( Amount v in values )
        {
            Console.Write( separator );
            Console.Write( v );
            separator = ", ";
        }
        Console.WriteLine();

        Assert.HasCount<Amount>( 3, values );
        Assert.AreEqual( new Amount( 0.0, TimeUnits.Hour ), values[0] );
        Assert.AreEqual( new Amount( 2.0, TimeUnits.Minute ), values[1] );
        Assert.AreEqual( new Amount( 26.0, TimeUnits.Second ), values[2] );
    }

    /// <summary>   (Unit Test Method) tests split 02. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Split02Test()
    {
        Amount a = new( 10.5, LengthUnits.Meter );
        Amount[] values = a.Split( [LengthUnits.Yard, LengthUnits.Foot, LengthUnits.Inch], 1 );

        string separator = "";
        foreach ( Amount v in values )
        {
            Console.Write( separator );
            Console.Write( v );
            separator = ", ";
        }
        Console.WriteLine();

        Assert.HasCount<Amount>( 3, values );
        Assert.AreEqual( new Amount( 11.0, LengthUnits.Yard ), values[0] );
        Assert.AreEqual( new Amount( 1.0, LengthUnits.Foot ), values[1] );
        Assert.AreEqual( new Amount( 5.4, LengthUnits.Inch ), values[2] );
    }

    /// <summary>   (Unit Test Method) tests split 03. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Split03Test()
    {
        Amount a = new( Math.Sqrt( 13 ), LengthUnits.Meter );
        Amount[] values = a.Split( [LengthUnits.Meter, LengthUnits.Decimeter, LengthUnits.Centimeter, LengthUnits.Millimeter], 0 );

        string separator = "";
        foreach ( Amount v in values )
        {
            Console.Write( separator );
            Console.Write( v );
            separator = ", ";
        }
        Console.WriteLine();

        Assert.AreEqual( new Amount( 3.0, LengthUnits.Meter ), values[0] );
        Assert.AreEqual( new Amount( 6.0, LengthUnits.Decimeter ), values[1] );
        Assert.AreEqual( new Amount( 0.0, LengthUnits.Centimeter ), values[2] );
        Assert.AreEqual( new Amount( 6.0, LengthUnits.Millimeter ), values[3] );
    }

    /// <summary>   (Unit Test Method) tests formatting 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Formatting01Test()
    {
        CultureInfo defaultCultureInfo = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            CultureInfo cultureDutchBelgium = CultureInfo.GetCultureInfo( "nl-BE" );
            CultureInfo cultureEnglishUs = CultureInfo.GetCultureInfo( "en-US" );

            Amount a = new( 12.3456789, LengthUnits.Kilometer );
            Amount b = new( 12345.6789, LengthUnits.Meter );
            Amount c = new( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );
            Amount d = new( 25.678, LengthUnits.Meter * LengthUnits.Meter );

            Assert.AreEqual( "12.3456789 km", a.ToString( CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "12,3456789 Kilometer", a.ToString( "GN", cultureDutchBelgium ) );
            Assert.AreEqual( "12.346 km", a.ToString( "0.000 US", cultureEnglishUs ) );
            Assert.AreEqual( "12.346 km", a.ToString( "0.000 US", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "12.346", a.ToString( "0.000", CultureInfo.CurrentCulture ) );
#if NET5_0_OR_GREATER
            Assert.AreEqual( "12,346 km", a.ToString( "NS", cultureDutchBelgium ) );
            Assert.AreEqual( "12.346 km", a.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "12.345,679 m", b.ToString( "NS", cultureDutchBelgium ) );
            Assert.AreEqual( "12,345.679 m", b.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "-0.450 km/h", c.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "-0.450 (Kilometer/Hour)", c.ToString( "NN", cultureEnglishUs ) );
#else
            Assert.AreEqual( "12,35 km", a.ToString( "NS", cultureDutchBelgium ) );
            Assert.AreEqual( "12.35 km", a.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "12.345,68 m", b.ToString( "NS", cultureDutchBelgium ) );
            Assert.AreEqual( "12,345.68 m", b.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "-0.45 km/h", c.ToString( "NS", cultureEnglishUs ) );
            Assert.AreEqual( "-0.45 (Kilometer/Hour)", c.ToString( "NN", cultureEnglishUs ) );
#endif
            Assert.AreEqual( "-0,450 km/h", c.ToString( "0.000 US", cultureDutchBelgium ) );
            Assert.AreEqual( "[0,450] km/h", c.ToString( "0.000 US;[0.000] US", cultureDutchBelgium ) );
            Assert.AreEqual( "12.35 Kilometer", b.ToString( "NN|kilometer", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "12.346 km", b.ToString( "#,##0.000 US|kilometer", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "+12.346 km", b.ToString( "+#,##0.000 US|kilometer", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "12.346 km neg", (-b)!.ToString( "#,##0.000 US pos;#,##0.000 US neg|kilometer", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "25.68 m*m", d.ToString( "NS", CultureInfo.CurrentCulture ) );
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = defaultCultureInfo;
        }
    }

    /// <summary>   (Unit Test Method) tests formatting 08. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Formatting08Test()
    {
        CultureInfo defaultCultureInfo = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Amount a = new( 12.3456789, LengthUnits.Kilometer );
            Assert.AreEqual( "12.346 km", a.ToString( "{0:G5} {1}", CultureInfo.CurrentCulture ) );
            Assert.AreEqual( "12.346", a.ToString( "{0:G5}", CultureInfo.CurrentCulture ) );
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = defaultCultureInfo;
        }
    }

    /// <summary>   (Unit Test Method) tests formatting 02. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Formatting02Test()
    {
        Amount b = new( 1234.5678, LengthUnits.Meter );
        Assert.AreEqual( "", Amount.ToString( null, "#,##0.000 UN", CultureInfo.InvariantCulture ) );
        Assert.AreEqual( "1,234.568 Meter", Amount.ToString( b, "#,##0.000 UN", CultureInfo.InvariantCulture ) );
    }

    /// <summary>   (Unit Test Method) tests formatting 03. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void Formatting03Test()
    {
        Amount d = new( 278.9, LengthUnits.Mile );
        Amount t = new( 2.5, TimeUnits.Hour );

        Amount? s = d / t;

        Assert.AreEqual(
            "Taking 2.5 h to travel 449 km means your speed was 179.54 km/h",
            string.Format( CultureInfo.CurrentCulture,
            "Taking {1:GG|hour} to travel {0:#,##0 US|kilometer} means your speed was {2:#,##0.00 US|kilometer/hour}", d, t, s ) );

        Amount? a = null;

        Assert.AreEqual( "a = ", string.Format( CultureInfo.CurrentCulture, "a = {0:#,##0.0 US}", a ) );
    }

    /// <summary>   (Unit Test Method) tests static formatting. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void StaticFormattingTest()
    {
        Amount a = new( 1234.5678, LengthUnits.Meter );
        Amount? b = null;

        CultureInfo cultureEnglishUs = CultureInfo.GetCultureInfo( "en-US" );
        CultureInfo cultureDutchBelgium = CultureInfo.GetCultureInfo( "nl-BE" );

        Assert.AreEqual( "1234.5678 m", Amount.ToString( a, CultureInfo.CurrentCulture ).Replace( ",", "." ) );
        Assert.AreEqual( "1234.5678 m", Amount.ToString( a, cultureEnglishUs ) );
        Assert.AreEqual( "1234,5678 m", Amount.ToString( a, cultureDutchBelgium ) );
        Assert.AreEqual( "1.234.57 m", Amount.ToString( a, "#,##0.00 US", CultureInfo.CurrentCulture ).Replace( ",", "." ) );
        Assert.AreEqual( "1,234.57 m", Amount.ToString( a, "#,##0.00 US", cultureEnglishUs ) );
        Assert.AreEqual( "1.234,57 m", Amount.ToString( a, "#,##0.00 US", cultureDutchBelgium ) );

        string emptyValue = "";
        Assert.AreEqual( emptyValue, Amount.ToString( b, CultureInfo.CurrentCulture ).Replace( ",", "." ) );
        Assert.AreEqual( emptyValue, Amount.ToString( b, cultureEnglishUs ) );
        Assert.AreEqual( emptyValue, Amount.ToString( b, cultureDutchBelgium ) );
        Assert.AreEqual( emptyValue, Amount.ToString( b, "#,##0.00 US", CultureInfo.CurrentCulture ).Replace( ",", "." ) );
        Assert.AreEqual( emptyValue, Amount.ToString( b, "#,##0.00 US", cultureEnglishUs ) );
        Assert.AreEqual( emptyValue, Amount.ToString( b, "#,##0.00 US", cultureDutchBelgium ) );

        Amount? x = null;
        string s = string.Empty;
        Assert.AreEqual( "", s + Amount.ToString( x, "#,##0.00 US|meter", CultureInfo.CurrentCulture ) );

    }

    /// <summary>   (Unit Test Method) tests null amount is not less than. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void NullAmountIsNotLessThanTest()
    {
        Amount? a = null;
        Amount b = ( Amount ) 100.0;
        _ = Assert.ThrowsExactly<ArgumentNullException>( () => a < b );
    }

    /// <summary>   (Unit Test Method) tests null comparison. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void NullComparisonTest()
    {
        Amount? a = null;
        Amount b = ( Amount ) 100.0;

        int result = (( IComparable ) b).CompareTo( a );

        Assert.IsGreaterThan( 0, result );
    }

    /// <summary>   (Unit Test Method) tests addition with null. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AdditionWithNullTest()
    {
        Amount? a, b, sum;

        // Test both not null:
        a = new Amount( 100.0, LengthUnits.Meter );
        b = new Amount( 25.0, LengthUnits.Meter );
        sum = a + b;
        Assert.AreEqual( new Amount( 125.0, LengthUnits.Meter ), sum );

        // Test right not null:
        a = new Amount( 100.0, LengthUnits.Meter );
        b = null;
        sum = a + b;
        // not consistent with .NET treatment of Nullable:
        // Assert.AreEqual(new Amount(100.0, LengthUnits.Meter), sum);
        Assert.IsNull( sum );

        // Test left not null:
        a = null;
        b = new Amount( 25.0, LengthUnits.Meter );
        sum = a + b;
        // not consistent with .NET treatment of Nullable:
        // Assert.AreEqual(new Amount(25.0, LengthUnits.Meter), sum);
        Assert.IsNull( sum );

        // Test both null:
        a = null;
        b = null;
        sum = a + b;
        Assert.IsNull( sum );
    }

    /// <summary>   (Unit Test Method) tests subtract with null. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void SubtractWithNullTest()
    {
        Amount? a, b, subs;

        // Test both not null:
        a = new Amount( 100.0, LengthUnits.Meter );
        b = new Amount( 25.0, LengthUnits.Meter );
        subs = a - b;
        Assert.AreEqual( new Amount( 75.0, LengthUnits.Meter ), subs );

        // Test right not null:
        a = new Amount( 100.0, LengthUnits.Meter );
        b = null;
        subs = a - b;
        // not consistent with .NET treatment of Nullable:
        // Assert.AreEqual(new Amount(100.0, LengthUnits.Meter), subs);
        Assert.IsNull( subs );

        // Test left not null:
        a = null;
        b = new Amount( 25.0, LengthUnits.Meter );
        subs = a - b;
        // not consistent with .NET treatment of Nullable:
        // Assert.AreEqual(new Amount(-25.0, LengthUnits.Meter), subs);
        Assert.IsNull( subs );

        // Test both null:
        a = null;
        b = null;
        subs = a - b;
        Assert.IsNull( subs );
    }

    /// <summary>   (Unit Test Method) tests rounded comparison. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void RoundedComparisonTest()
    {
        Amount a = new( 0.045, LengthUnits.Meter );
        Amount b = new( 0.0450000000001, LengthUnits.Meter );
        Amount c = new( 0.0450000000002, LengthUnits.Meter );
        Amount d = new( 0.046, LengthUnits.Meter );
        Assert.AreNotEqual( b.Value, a.Value );
        Assert.AreNotEqual( c.Value, b.Value );
        Assert.AreNotEqual( c.Value, a.Value );
        Assert.IsTrue( a == b );
        Assert.IsTrue( b == c );
        Assert.IsTrue( a == c );
        Assert.IsFalse( c == d );
        Assert.IsTrue( a.Equals( b ) );
        Assert.IsTrue( b.Equals( c ) );
        Assert.IsTrue( a.Equals( c ) );
        Assert.IsFalse( c.Equals( d ) );
    }

    /// <summary>   (Unit Test Method) tests comparison 01. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod]
    public void Comparison01Test()
    {
        Amount a = new( -0.00002, EnergyUnits.Horsepower );
        Amount b = new( -0.00002, EnergyUnits.Horsepower );

        Amount ar = a.ConvertedTo( EnergyUnits.Watt );
        Amount br = b.ConvertedTo( EnergyUnits.Watt );

        Assert.IsTrue( a == b );
        Assert.IsFalse( a > b );
        Assert.IsFalse( a < b );
        Assert.IsTrue( ar == br );
        Assert.IsFalse( ar > br );
        Assert.IsFalse( ar < br );
    }

    /// <summary>   (Unit Test Method) tests comparison 02. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod]
    public void Comparison02Test()
    {
        Amount a = new( 120.0, SpeedUnits.KilometerPerHour );
        Amount b = new( 33.3333333330, SpeedUnits.MeterPerSecond );

        Assert.IsTrue( a == b );
        Assert.IsFalse( a < b );
        Assert.IsFalse( a > b );
        Assert.IsTrue( a <= b );
        Assert.IsTrue( a >= b );
        Assert.IsFalse( a != b );
    }

    /// <summary>   (Unit Test Method) tests division by zero. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod]
    public void DivisionByZeroTest()
    {
        Amount d1 = new( 32.0, LengthUnits.Kilometer );
        Amount d2 = new( 0.0, LengthUnits.Kilometer );
        Amount t = new( 0.0, TimeUnits.Hour );

        Amount? s;

        s = d1 / t;

        Assert.IsTrue( double.IsInfinity( s!.Value ) );
        Assert.IsTrue( double.IsPositiveInfinity( s.Value ) );
        Assert.AreEqual( s.Unit, d1.Unit / t.Unit );

        s = d2 / t;

        Assert.IsTrue( double.IsNaN( s!.Value ) );
        Assert.AreEqual( s.Unit, d2.Unit / t.Unit );
    }

    /// <summary>   (Unit Test Method) tests amount compatibility. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AmountCompatibilityTest()
    {
        Amount a = new( 300, LengthUnits.Mile / TimeUnits.Hour.Power( 2 ) );
        Assert.IsTrue( a.Unit.IsCompatibleTo( LengthUnits.Meter / TimeUnits.Second.Power( 2 ) ) );
        Assert.IsTrue( a.Unit.IsCompatibleTo( LengthUnits.Meter * TimeUnits.Second.Power( -2 ) ) );
        Assert.IsFalse( a.Unit.IsCompatibleTo( LengthUnits.Meter / TimeUnits.Second.Power( 1 ) ) );
        Assert.IsFalse( a.Unit.IsCompatibleTo( MassUnits.Gram ) );
        Console.WriteLine( a.Unit.UnitType.ToString() );
    }

    /// <summary>   (Unit Test Method) tests amount split. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AmountSplitTest()
    {
        // One fifth of a week:
        Amount a = new( 1.0 / 5.0, TimeUnits.Day * 7.0 );

        Amount[] result = a.Split( [TimeUnits.Day, TimeUnits.Hour, TimeUnits.Minute, TimeUnits.Second], 3 );

        foreach ( Amount item in result )
            Console.WriteLine( item );

        Assert.HasCount<Amount>( 4, result );
        CollectionAssert.AreEqual( _oneFifthWeekZeroSecondsArray.ToList(), result.Select( x => x.Value ).ToList() );
    }

    /// <summary>   (Unit Test Method) tests amount split 2. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AmountSplit2Test()
    {
        // One fifth of a week:
        Amount a = new( 7.0 / 5.0, TimeUnits.Day );

        Amount[] result = a.Split( [TimeUnits.Day, TimeUnits.Hour, TimeUnits.Minute, TimeUnits.Second], 3 );

        foreach ( Amount item in result )
            Console.WriteLine( item );

        // In this case, the split results in 1 day, 9 hours, 35 minutes and 60 SECONDS!
        // This is due to rounding; it results in ..., 35 minutes and 59.99999 seconds,
        // which once rounded, end up to be 60 seconds...

        Assert.HasCount<Amount>( 4, result );
        CollectionAssert.AreEqual( _oneFifthWeekSixtySecondsArray.ToList(), result.Select( x => x.Value ).ToList() );
    }

    /// <summary>   (Unit Test Method) tests amount split incompatible. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AmountSplitIncompatibleTest()
    {
        // One fifth of a week:
        Amount a = new( 7.0 / 5.0, TimeUnits.Day );
        _ = Assert.ThrowsExactly<UnitConversionException>( () => a.Split( [TimeUnits.Day, TimeUnits.Hour, LengthUnits.Meter, TimeUnits.Second], 3 ) );
    }
}


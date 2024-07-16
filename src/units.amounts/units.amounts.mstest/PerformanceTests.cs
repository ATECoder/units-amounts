namespace cc.isr.UnitsAmounts.MSTest;
/// <summary>
/// Summary description for PerformanceTests
/// </summary>
[TestClass]
public class PerformanceTests
{
    private const double MAX_ACCEPTANCE_VARIANCE = +0.25;
    private const double MIN_ACCEPTANCE_VARIANCE = -2.0;

    #region " initialize & cleanup "

    private UnitManager? _defaultUnitManager;

    [TestInitialize()]
    public void InitializeBeforeEachTest()
    {
        Console.Write( "Resetting the Unit Manager instance..." );
        this._defaultUnitManager = UnitManager.Instance;
        UnitManager.Instance = new UnitManager();
        UnitManager.RegisterByAssembly( typeof( LengthUnits ).Assembly );
        Console.WriteLine( " done." );
    }

    [TestCleanup()]
    public void CleanupAfterEachTest()
    {
        UnitManager.Instance = this._defaultUnitManager!;
    }

    #endregion Initialize & cleanup

    [TestMethod]
    public void AmountAdditionPerformanceTest()
    {
        Amount a = new( 15.0, LengthUnits.Meter );
        Amount sum = new( 0.0, LengthUnits.Meter );

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            sum = (sum + a)!;
        double time = (Environment.TickCount - t) / 1000.0;
        double expectedTime = 0.5 * 0.031;
        double var = (time - expectedTime) / expectedTime;

        Console.WriteLine( "Time to perform 100K additions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 1500000.0, sum.Value );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountDerivedAdditionPerformanceTest()
    {
        Amount sum = new( 0.0, LengthUnits.Kilometer );


        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            sum = (sum + new Amount( 15.0, LengthUnits.Meter ))!;
        double time = (Environment.TickCount - t) / 1000.0;
        double expectedTime = 0.3 * 0.219;
        double var = (time - expectedTime) / expectedTime;

        Console.WriteLine( "Sum = {0}", sum );
        Console.WriteLine( "Time to perform 100K additions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 1500.0, Math.Round( sum.Value, 8 ) );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountSimpleDivisionPerformanceTest()
    {
        Amount a = new( 15.0, LengthUnits.Meter );
        Amount b = new( 3.0, TimeUnits.Second );
        Amount? q = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            q = a / b;
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.4 * 0.11)) / 0.11;

        Console.WriteLine( "Time to perform 100K simple divisions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 5.0, q!.Value );
        Assert.IsTrue( q.Unit.IsCompatibleTo( LengthUnits.Meter / TimeUnits.Second ) );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountComplexDivisionPerformanceTest()
    {
        Amount a = new( 15.0, LengthUnits.Kilometer );
        Amount b = new( 3.0, TimeUnits.Hour );
        Amount? q = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            q = a / b;
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - 0.061) / 0.061;

        Console.WriteLine( "Time to perform 100K complex divisions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 5.0, q!.Value );
        Assert.AreEqual( 5.0, q.ConvertedTo( LengthUnits.Kilometer / TimeUnits.Hour, 8 ).Value );
        Assert.IsTrue( q.Unit.IsCompatibleTo( LengthUnits.Meter / TimeUnits.Second ) );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountScenario01PerformanceTest()
    {
        Amount distance;
        Amount speed;
        Amount? duration = Amount.Zero( TimeUnits.Day );

        long t = Environment.TickCount;
        for ( int n = 1; n <= 10000; n++ )
        {
            distance = new Amount( 50.0, new Unit( $"my{"foot"}", $"my{LengthUnits.Foot.Symbol}", 44.0 * LengthUnits.Centimeter ) );
            speed = new Amount( n, LengthUnits.Kilometer / TimeUnits.Hour );
            duration = duration! + (distance / speed)!;
        }
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.4 * 0.234)) / (0.4 * 0.234);

        duration = duration!.ConvertedTo( TimeUnits.Minute, 1 );

        Console.WriteLine( "Time to perform 10K complex scenario: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 12.9, duration.Value );
        Assert.IsTrue( duration.Unit.IsCompatibleTo( TimeUnits.Second ) );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountSimpleConvertPerformanceTest()
    {
        Amount a = new( 15.0, LengthUnits.Kilometer );
        Amount? b = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            b = a.ConvertedTo( LengthUnits.Meter, 8 );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.45 * 0.172)) / (0.45 * 0.172);

        Console.WriteLine( "Result = {0}", b );
        Console.WriteLine( "Time to perform 100K conversions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 15000, b!.Value );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void AmountComplexConvertPerformanceTest()
    {
        Amount a = new( 15.0, LengthUnits.Kilometer * LengthUnits.Meter / LengthUnits.Millimeter /
                                           (TimeUnits.Hour * TimeUnits.Minute / TimeUnits.Millisecond) );
        Amount? b = null;
        Unit targetUnit = LengthUnits.Meter / TimeUnits.Second;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            b = a.ConvertedTo( targetUnit, 8 );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - 0.0403) / 0.0403;

        Console.WriteLine( "Original = {0}", a );
        Console.WriteLine( "Result = {0}", b );
        Console.WriteLine( "Time to perform 100K conversions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.AreEqual( 0.06944444, b!.Value );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void UnitManagerResolveNamedToNamedTest()
    {
        // Try resolving a named unit:

        Unit u = LengthUnits.Kilometer;
        Unit? v = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            v = UnitManager.ResolveToNamedUnit( u, true );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.4 * 0.015)) / (4 * 0.015);

        Console.WriteLine( "Original = {0}", u.Name );
        Console.WriteLine( "Result = {0}", v!.Name );
        Console.WriteLine( "Time to perform 100K resolutions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.IsTrue( v.IsNamed );
        Assert.AreEqual( "Kilometer", v.Name );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void UnitManagerResolveKnownToNamedTest()
    {
        // Try resolving a known unit:

        Unit u = LengthUnits.Meter / TimeUnits.Second;
        Unit? v = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            v = UnitManager.ResolveToNamedUnit( u, true );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.3 * 0.219)) / 0.219;

        Console.WriteLine( "Original = {0}", u.Name );
        Console.WriteLine( "Result = {0}", v!.Name );
        Console.WriteLine( "Time to perform 100K resolutions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.IsTrue( v.IsNamed );
        Assert.AreEqual( "Meter/Second", v.Name );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void UnitManagerResolveUnknownToNamedTest()
    {
        // Try resolving an unknown unit of a known family:

        Unit u = LengthUnits.NauticalMile / TimeUnits.Minute;
        Unit? v = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            v = UnitManager.ResolveToNamedUnit( u, true );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.3 * 0.297)) / 0.297;

        Console.WriteLine( "Original = {0}", u.Name );
        Console.WriteLine( "Result = {0}", v!.Name );
        Console.WriteLine( "Time to perform 100K resolutions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.IsFalse( v.IsNamed );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }

    [TestMethod]
    public void UnitManagerResolveUnfamiliarToNamedTest()
    {
        // Try resolving a unit of an unknown family:

        Unit u = ElectricUnits.Ohm * ElectricUnits.Ohm;
        Unit? v = null;

        long t = Environment.TickCount;
        for ( int n = 0; n < 100000; n++ )
            v = UnitManager.ResolveToNamedUnit( u, true );
        double time = (Environment.TickCount - t) / 1000.0;
        double var = (time - (0.5 * 0.032)) / 0.032;

        Console.WriteLine( "Original = {0}", u.Name );
        Console.WriteLine( "Result = {0}", v!.Name );
        Console.WriteLine( "Time to perform 100K resolutions: {0} sec.", time );
        Console.WriteLine( "Variation: {0:0}%.", var * 100 );

        Assert.IsFalse( v.IsNamed );

        Assert.IsTrue( var < MAX_ACCEPTANCE_VARIANCE, $"Performance {var} > {MAX_ACCEPTANCE_VARIANCE} lost detected!" );
        if ( var < MIN_ACCEPTANCE_VARIANCE )
            Assert.Inconclusive( $"Performance {var} was much better than expected {MIN_ACCEPTANCE_VARIANCE}." );
    }
}

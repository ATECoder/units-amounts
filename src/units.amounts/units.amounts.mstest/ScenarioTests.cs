namespace cc.isr.UnitsAmounts.MSTest;

[TestClass]
public class ScenarioTests
{
    #region " Initialize & cleanup "

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
    public void Scenario01Test()
    {
        // What is the height of 36 liter water spread over an area of 45cm x 2m ?

        Amount volume = new( 36.0, "liter" );
        Amount? area = new Amount( 45.0, "centimeter" ) * new Amount( 2.0, "meter" );

        Amount? height = volume / area;

        Console.WriteLine( "Volume : {0}", volume );
        Console.WriteLine( "Area : {0}", area );
        Console.WriteLine( "Height : {0}", height );
        Console.WriteLine( "Height : {0}", height!.ConvertedTo( "centimeter", 2 ) );

        Assert.AreEqual( new Amount( 4.0, "centimeter" ), height );
    }

    [TestMethod()]
    public void Scenario02Test()
    {
        // Driving 140 mile in 2 hours, what is the average speed ?

        Amount distance = new( 140, LengthUnits.Mile );
        Amount time = new( 2, TimeUnits.Hour );

        Amount? speed = distance / time;

        Console.WriteLine( "Speed : {0}", speed );

        Assert.AreEqual( new Amount( 70.0, SpeedUnits.MilePerHour ), speed );
    }

    [TestMethod()]
    public void Scenario03Test()
    {
        // Driving 15 min at a speed of 120 km/h, what distance is made ?

        Amount speed = new( 120, LengthUnits.Kilometer / TimeUnits.Hour );
        Amount time = new( 15, TimeUnits.Minute );

        Amount? distance = speed * time;

        Console.WriteLine( "Distance : {0}", distance );
        Console.WriteLine( "Distance : {0}", distance!.ConvertedTo( "kilometer", 4 ) );

        Assert.AreEqual( new Amount( 30, LengthUnits.Kilometer ), distance );
    }

    [TestMethod()]
    public void Scenario04Test()
    {
        // What is the sum of 500 meter and 2 mile ?

        Amount a = new( 500, LengthUnits.Meter );
        Amount b = new( 2, LengthUnits.Mile );

        Amount? sum = a + b;

        Console.WriteLine( "Sum : {0}", sum );
        Console.WriteLine( "Sum : {0}", sum!.ConvertedTo( LengthUnits.Yard, 4 ) );

        Assert.AreEqual( new Amount( 3718.6880, LengthUnits.Meter ), sum );
    }

    [TestMethod]
    public void Scenario05Test()
    {
        // Scenario calculating stop-distance:
        // A car drives at 120 km/h. What distance does the car need to stop
        // at a deceleration of 6m/s² if the driver needs 1 second to react ?

        // Parameters:
        Amount speed = new( 120, LengthUnits.Kilometer / TimeUnits.Hour );
        Amount reactionTime = new( 1, TimeUnits.Second );
        Amount deceleration = new( 6, LengthUnits.Meter / TimeUnits.Second.Power( 2 ) );

        // Formula:
        Amount? distance;
        distance = (speed * reactionTime)! + (speed.Power( 2 )! / (2 * deceleration)!)!;

        Console.WriteLine( "Distance : {0}", distance );
        Console.WriteLine( "Distance : {0}", distance!.ConvertedTo( LengthUnits.Meter, 1 ) );

        // Result:
        Assert.AreEqual( new Amount( 125.9, LengthUnits.Meter ), distance.ConvertedTo( LengthUnits.Meter, 1 ) );
    }

    [TestMethod]
    public void Scenario06Test()
    {
        // A bottle of 50 liter gas compressed at 80 bar.
        // How many m³ does this represent at 1 atmosphere ?

        Amount bottleVolume = new( 50.0, "liter" );
        Amount bottlePressure = new( 80.0, "bar" );
        Amount outerPressure = new( 1.0, "atmosphere" );

        Amount? outerVolume = (bottleVolume * bottlePressure)! / outerPressure;

        Console.WriteLine( "Volume : {0}", outerVolume );
        Console.WriteLine( "Volume : {0}", outerVolume!.ConvertedTo( "Meter" + UnitSymbols.Cubed, 2 ) );

        Assert.AreEqual( new Amount( 3.95, VolumeUnits.CubicMeter ), outerVolume.ConvertedTo( "Meter" + UnitSymbols.Cubed, 2 ) );
    }

    [TestMethod]
    public void Scenario07Test()
    {
        // What is the energetic value of an amount of LNG ?
        Unit powerPerVolume = new( $"{EnergyUnits.KilowattHour.Symbol}/{VolumeUnits.CubicMeter.Symbol}",
                               $"{EnergyUnits.KilowattHour.Symbol}/{VolumeUnits.CubicMeter.Symbol}",
                               EnergyUnits.KilowattHour / VolumeUnits.CubicMeter );
        Amount lng = new( 83.24, VolumeUnits.CubicMeter );
        Amount ghv = new( 6699.0, powerPerVolume );

        Amount? energy = lng * ghv;

        Console.WriteLine( "LNG volume : {0}", lng );
        Console.WriteLine( "GHV : {0}", ghv );
        Console.WriteLine( "Energy : {0}", energy );
        Console.WriteLine( "Energy : {0}", energy!.ConvertedTo( "kilowatt-hour" ) );

        Assert.AreEqual( new Amount( 557625.0, EnergyUnits.KilowattHour ), energy.ConvertedTo( EnergyUnits.KilowattHour, 0 ) );
    }

    [TestMethod]
    public void Scenario08Test()
    {
        // Which one is faster ?

        Amount a = new( 80.0, LengthUnits.Kilometer / TimeUnits.Hour );
        Amount b = new( 40.0, LengthUnits.Meter / TimeUnits.Second );

        Assert.IsTrue( a < b );

        Console.WriteLine( a.ConvertedTo( b.Unit ) );
        Console.WriteLine( b.ConvertedTo( b.Unit ) );
    }
}

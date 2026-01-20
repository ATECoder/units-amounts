namespace cc.isr.UnitsAmounts.Tests;

/// <summary>   (Unit Test Class) a serialization tests. </summary>
/// <remarks>   2025-09-06. </remarks>
[TestClass]
public class NewtonSoftSerializationTests
{
    #region " initialize & cleanup "

    /// <summary>
    /// Gets or sets the test context which provides information about and functionality for the
    /// current test run.
    /// </summary>
    /// <value> The test context. </value>
    public TestContext? TestContext { get; set; }

    private UnitManager? _defaultUnitManager;

    /// <summary>   Sets up this unit test class prior to execution. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestInitialize()]
    public void InitializeBeforeEachTest()
    {
        Console.WriteLine( $"{this.TestContext?.FullyQualifiedTestClassName}: {DateTime.Now} {System.TimeZoneInfo.Local}" );
        Console.WriteLine( $"\tTesting {typeof( cc.isr.UnitsAmounts.Amount ).Assembly.FullName}" );
        Console.WriteLine( $"\tTesting {typeof( cc.isr.UnitsAmounts.StandardUnits.ElectricUnits ).Assembly.FullName}" );

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

    #endregion " Initialize & cleanup "

    #region " Newton Soft Json serializer "

    /// <summary>   Amount should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    /// <exception cref="InvalidDataException"> Thrown when an Invalid Data error condition occurs. </exception>
    /// <param name="amount">   An Amount to process. </param>
    public static void AmountShouldSerialize( Amount amount )
    {
        Console.WriteLine( $"Serializing: {amount}" );
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject( amount );
        Amount deserializedAmount = Newtonsoft.Json.JsonConvert.DeserializeObject<Amount>( jsonData ) ?? throw new InvalidDataException();
        Assert.AreEqual( amount, deserializedAmount );
    }

    /// <summary>   (Unit Test Method) meter should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    [TestMethod()]
    public void MeterShouldSerialize()
    {
        Amount amount = new( 12345.6789, LengthUnits.Meter );
        NewtonSoftSerializationTests.AmountShouldSerialize( amount );
    }

    /// <summary>   (Unit Test Method) kilometer per hour should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    [TestMethod()]
    public void KilometerPerHourShouldSerialize()
    {
        Amount amount = new( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );
        NewtonSoftSerializationTests.AmountShouldSerialize( amount );
    }

    /// <summary>   (Unit Test Method) tests amount JSON serialization. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void KilowattPerCubicMeterShouldSerialize()
    {
        Amount amount = new( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        NewtonSoftSerializationTests.AmountShouldSerialize( amount );
    }

    #endregion " Newton Soft Json serializer "
}


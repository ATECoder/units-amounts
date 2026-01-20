using cc.isr.UnitsAmounts.SerializationExtensions;

namespace cc.isr.UnitsAmounts.Tests;

/// <summary>   (Unit Test Class) a serialization tests. </summary>
/// <remarks>   2025-09-06. </remarks>
[TestClass]
public class JsonSerializationTests
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
        Console.WriteLine( $"\tTesting {typeof( cc.isr.UnitsAmounts.StandardUnits.LengthUnits ).Assembly.FullName}" );
        Console.WriteLine( $"\tTesting {typeof( cc.isr.UnitsAmounts.SerializationExtensions.SerializationExtensionsMethods ).Assembly.FullName}" );

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

    #region " json serializer "

    /// <summary>   Amount should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    /// <param name="amount">   The amount. </param>
    public static void AmountShouldSerialize( Amount amount )
    {
        Console.WriteLine( $"Serializing: {amount}" );
        Assert.AreEqual( amount.Unit.UnitType, amount.Unit.UnitType.Serialize().ToUnitType() );
        Assert.AreEqual( amount.Unit, amount.Unit.Serialize().ToUnit() );
        Assert.AreEqual( amount, amount.Serialize().ToAmount() );
    }

    /// <summary>   (Unit Test Method) meter should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    [TestMethod()]
    public void MeterShouldSerialize()
    {
        Amount amount = new( 12345.6789, LengthUnits.Meter );
        JsonSerializationTests.AmountShouldSerialize( amount );
    }

    /// <summary>   (Unit Test Method) kilometer per hour should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    [TestMethod()]
    public void KilometerPerHourShouldSerialize()
    {
        Amount amount = new( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );
        JsonSerializationTests.AmountShouldSerialize( amount );
    }

    /// <summary>   (Unit Test Method) kilowatt per cubic meter should serialize. </summary>
    /// <remarks>   2026-01-20. </remarks>
    [TestMethod()]
    public void KilowattPerCubicMeterShouldSerialize()
    {
        Amount amount = new( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        JsonSerializationTests.AmountShouldSerialize( amount );
    }

    #endregion " json serializer "
}

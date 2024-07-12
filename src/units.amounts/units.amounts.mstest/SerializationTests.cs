namespace cc.isr.UnitsAmounts.MSTest;

/// <summary>   An amount serialization tests. </summary>
/// <remarks>   2023-04-08. </remarks>
[TestClass]
public class SerializationTests
{
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

    #endregion " Initialize & cleanup "

    #region " json serializer "

    /// <summary>   JSON serialize deserialize. </summary>
    /// <remarks>   2024-06-21. </remarks>
    /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                 invalid. </exception>
    /// <param name="a">    An Amount to process. </param>
    /// <returns>   An Amount. </returns>
    public static Amount JsonSerializeDeserialize( Amount a )
    {
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject( a );
        Amount b = Newtonsoft.Json.JsonConvert.DeserializeObject<Amount>( jsonData )!;
        return b;
    }

    /// <summary>   (Unit Test Method) tests serialize deserialize using JSON. </summary>
    /// <remarks>   2024-06-21. </remarks>
    [TestMethod()]
    public void SerializeDeserializeUsingJsonTest()
    {
        // Make some amounts:
        Amount a1before = new( 12345.6789, LengthUnits.Meter );
        Amount a2before = new( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );

        // Deserialize units:
        Amount a1after = SerializationTests.JsonSerializeDeserialize( a1before );
        Amount a2after = SerializationTests.JsonSerializeDeserialize( a2before );

        Console.WriteLine( "{0} => {1}", a1before, a1after );
        Console.WriteLine( "{0} => {1}", a2before, a2after );

        Assert.AreEqual( a1before, a1after );
        Assert.AreEqual( a2before, a2after );

    }

    [TestMethod()]
    public void AmountJsonSerializationTest()
    {
        Amount a = new( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        Amount b = SerializationTests.JsonSerializeDeserialize( a );

        // Compare:
        Console.WriteLine( a );
        Console.WriteLine( b );
        Assert.AreEqual( a, b );
    }

    #endregion " json serializer "

}

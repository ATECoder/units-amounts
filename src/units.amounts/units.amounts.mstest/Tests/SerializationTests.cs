namespace cc.isr.UnitsAmounts.Tests;

/// <summary>   (Unit Test Class) a serialization tests. </summary>
/// <remarks>   2025-09-06. </remarks>
[TestClass]
public class SerializationTests
{
    #region " initialize & cleanup "

    private UnitManager? _defaultUnitManager;

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

    #endregion " Initialize & cleanup "

    #region " json serializer "

    /// <summary>   JSON serialize deserialize. </summary>
    /// <remarks>   2024-06-21. </remarks>
    /// <exception cref="InvalidDataException"> Thrown when an Invalid Data error condition occurs. </exception>
    /// <param name="a">    An Amount to process. </param>
    /// <returns>   An Amount. </returns>
    ///
    /// ### <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                     invalid. </exception>
    public static Amount JsonSerializeDeserialize( Amount a )
    {
#if true
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject( a );
        return Newtonsoft.Json.JsonConvert.DeserializeObject<Amount>( jsonData ) ?? throw new InvalidDataException();
#else
        // JsonSerializer fails because it does not support ISerializable. While the Amount and Unit classes can
        // be serialized once the relevant constructors are decorated with the JsonConstructor attribute,
        // a JsonConverter<UnitType> is needed to serialize the UnitType class. The Json attributes require adding 
        // the Json package to the Units Amounts project which is not desirable. 
        // Possibly, JsonConverters can be written for the Amount and Unit classes by emitting the ISerializable data.
        // Microsoft logic for dropping support for ISerializable that 'this interface is a legacy mechanism for binary 
        // and XML serialization' is clearly unfortunate.
        System.Text.Json.JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.Default,
            WriteIndented = true
        };
        string jsonData = System.Text.Json.JsonSerializer.Serialize<Amount>( a, options );
        return System.Text.Json.JsonSerializer.Deserialize<Amount>( jsonData, options ) ?? throw new InvalidDataException();
#endif
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
        Amount a1after = JsonSerializeDeserialize( a1before );
        Amount a2after = JsonSerializeDeserialize( a2before );

        Console.WriteLine( "{0} => {1}", a1before, a1after );
        Console.WriteLine( "{0} => {1}", a2before, a2after );

        Assert.AreEqual( a1before, a1after );
        Assert.AreEqual( a2before, a2after );

    }

    /// <summary>   (Unit Test Method) tests amount JSON serialization. </summary>
    /// <remarks>   2025-09-06. </remarks>
    [TestMethod()]
    public void AmountJsonSerializationTest()
    {
        Amount a = new( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        Amount b = JsonSerializeDeserialize( a );

        // Compare:
        Console.WriteLine( a );
        Console.WriteLine( b );
        Assert.AreEqual( a, b );
    }

    #endregion " json serializer "
}

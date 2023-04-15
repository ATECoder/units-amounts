using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace cc.isr.UnitsAmounts.MSTest;

/// <summary>   An amount serialization tests. </summary>
/// <remarks>   2023-04-08. </remarks>
[TestClass()]
public class SerializationTests
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

    #endregion " Initialize & cleanup "

    #region " IFORMATTER "

    /// <summary>   Asserts should serialize. </summary>
    /// <remarks>   David, 2022-01-31. </remarks>
    /// <param name="stream">       The stream. </param>
    /// <param name="formatter">    The formatter. </param>
    private static void AssertShouldSerialize( Stream stream, IFormatter formatter )
    {

        // Make some amounts:
        var a1before = new Amount( 12345.6789, LengthUnits.Meter );
        var a2before = new Amount( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );

#pragma warning disable SYSLIB0011 // Type or member is obsolete
        // Serialize the units:
        formatter.Serialize( stream, a1before );
        formatter.Serialize( stream, a2before );

        // Reset stream:
        _ = stream.Seek( 0, SeekOrigin.Begin );

        // Deserialize units:
        Amount a1after = ( Amount ) formatter.Deserialize( stream );
        Amount a2after = ( Amount ) formatter.Deserialize( stream );
#pragma warning restore SYSLIB0011 // Type or member is obsolete

        stream.Close();
        Console.WriteLine( "{0} => {1}", a1before, a1after );
        Console.WriteLine( "{0} => {1}", a2before, a2after );

        Assert.AreEqual( a1before, a1after );
        Assert.AreEqual( a2before, a2after );

    }

    #endregion " IFORMATTER "

    #region " Binary Formatter "

    /// <summary>   (Unit Test Method) should serialize using binary formatter. </summary>
    /// <remarks>   David, 2022-01-31. </remarks>
    [TestMethod()]
    public void ShouldSerializeUsingBinaryFormatter()
    {
        using var memoryStream = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        SerializationTests.AssertShouldSerialize( memoryStream, formatter );
    }

    [TestMethod()]
    public void AmountBinaryFormatterSerializationTest()
    {
        Amount a = new( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );

        // Serialize instance:
        using MemoryStream stream = new();
        BinaryFormatter formatter = new();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        formatter.Serialize( stream, a );
#pragma warning restore SYSLIB0011 // Type or member is obsolete

        // Deserialize instance:
        stream.Position = 0;
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        Amount b = ( Amount ) formatter.Deserialize( stream );
#pragma warning restore SYSLIB0011 // Type or member is obsolete

        // Compare:
        Console.WriteLine( a );
        Console.WriteLine( b );
        Assert.AreEqual( a, b );
    }

    [TestMethod()]
    public void SerializeDeserialize01Test()
    {
        // Make some amounts:
        Amount a1before = new( 12345.6789, LengthUnits.Meter );
        Amount a2before = new( -0.45, LengthUnits.Kilometer / TimeUnits.Hour );

        using MemoryStream buffer = new();
        // Serialize the units:
        BinaryFormatter f = new();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        f.Serialize( buffer, a1before );
        f.Serialize( buffer, a2before );
#pragma warning restore SYSLIB0011 // Type or member is obsolete

        // Reset stream:
        _ = buffer.Seek( 0, SeekOrigin.Begin );

        // Deserialize units:
        BinaryFormatter g = new();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        Amount a1after = ( Amount ) g.Deserialize( buffer );
        Amount a2after = ( Amount ) g.Deserialize( buffer );
#pragma warning restore SYSLIB0011 // Type or member is obsolete

        Console.WriteLine( "{0} => {1}", a1before, a1after );
        Console.WriteLine( "{0} => {1}", a2before, a2after );

        Assert.AreEqual( a1before, a1after );
        Assert.AreEqual( a2before, a2after );

    }

    #endregion " Binary Formatter "

    #region " Grammophone Formatter "

#if Grammophone
    /// <summary>   (Unit Test Method) should serialize using fast binary formatter. </summary>
    /// <remarks>   David, 2022-01-31. </remarks>
    [TestMethod()]
    public void ShouldSerializeUsingFastBinaryFormatter()
    {
        using var memoryStream = new MemoryStream();
        IFormatter formatter = new Grammophone.Serialization.FastBinaryFormatter();
        AmountSerializationTests.AssertShouldSerialize( memoryStream, formatter );
    }

    /// <summary>   (Unit Test Method) should serialize array. </summary>
    /// <remarks>   David, 2022-01-31. </remarks>
    [TestMethod()]
    public void ShouldSerializeArray()
    {
        // Serialize instance:
        using MemoryStream stream = new();
        var formatter = new Grammophone.Serialization.FastBinaryFormatter();

        Amount[] amounts = new Amount[6];
        amounts[0] = new Amount( 32.5, LengthUnits.NauticalMile );
        amounts[1] = new Amount( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        amounts[2] = 3 * amounts[0];
        amounts[3] = 3 * amounts[1];
        amounts[4] = amounts[1] / amounts[3];
        amounts[5] = new Amount( 42.3, LengthUnits.Meter / TimeUnits.Second.Power( 2 ) );

        formatter.Serialize( stream, amounts );

        // Deserialize instance:
        stream.Position = 0;
        Amount[] ba = ( Amount[] ) formatter.Deserialize( stream );

        // Compare:
        Assert.AreEqual( amounts.Length, ba.Length );
        for ( int i = 0; i < amounts.Length; i++ )
        {
            Console.WriteLine( amounts[i] );
            Console.WriteLine( ba[i] );
            Assert.AreEqual( amounts[i], ba[i] );
        }
    }

#endif

    #endregion " Grammophone Formatter "

    #region " SOAP Formatter "

#if Soap


    [TestMethod()]
    public void AmountSoapFormatterSerializationTest()
    {
        Amount a = new Amount( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );

        // Serialize instance:
        using MemoryStream stream = new MemoryStream();
        SoapFormatter formatter = new SoapFormatter();
        formatter.Serialize( stream, a );

        // Deserialize instance:
        stream.Position = 0;
        Amount b = ( Amount ) formatter.Deserialize( stream );

        // Show serialization (this closes the stream):
        stream.Position = 0;
        Console.WriteLine( stream.ToXmlString() );
        Console.WriteLine();

        // Compare:
        Console.WriteLine( a );
        Console.WriteLine( b );
        Assert.AreEqual( a, b );
    }

#endif

    #endregion " SOAP Formatter  "

    #region " NetDataContractSerializer"

#if NetDataContractSerializer

    [TestMethod()]
    public void AmountNetDataContractSerializerSerializationTest()
    {
        Amount a = new Amount( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );

        // Serialize instance:
        using MemoryStream stream = new MemoryStream();
        NetDataContractSerializer serializer = new NetDataContractSerializer();
        serializer.Serialize( stream, a );

        // Deserialize instance:
        stream.Position = 0;
        Amount b = ( Amount ) serializer.Deserialize( stream );

        // Show serialization (this closes the stream):
        stream.Position = 0;
        Console.WriteLine( stream.ToXmlString() );
        Console.WriteLine();

        // Compare:
        Console.WriteLine( a );
        Console.WriteLine( b );
        Assert.AreEqual( a, b );
    }

    [TestMethod()]
    public void AmountArrayNetDataContractSerializerSerializationTest()
    {
        Amount[] aa = new Amount[6];
        aa[0] = new Amount( 32.5, LengthUnits.NauticalMile );
        aa[1] = new Amount( 3500.12, EnergyUnits.KilowattHour * (365.0 * TimeUnits.Day) / VolumeUnits.CubicMeter );
        aa[2] = 3 * aa[0];
        aa[3] = 3 * aa[1];
        aa[4] = aa[1] / aa[3];
        aa[5] = new Amount( 42.3, LengthUnits.Meter / TimeUnits.Second.Power( 2 ) );

        // Serialize instance:
        using MemoryStream stream = new MemoryStream();
        NetDataContractSerializer serializer = new NetDataContractSerializer();
        serializer.WriteObject( stream, aa );

        // Deserialize instance:
        stream.Position = 0;
        Amount[] ba = ( Amount[] ) serializer.ReadObject( stream );

        // Show serialization (this closes the stream):
        stream.Position = 0;
        Console.WriteLine( stream.ToXmlString() );
        Console.WriteLine();

        // Compare:
        Assert.AreEqual( aa.Length, ba.Length );
        for ( int i = 0; i < aa.Length; i++ )
        {
            Console.WriteLine( aa[i] );
            Console.WriteLine( ba[i] );
            Assert.AreEqual( aa[i], ba[i] );
        }
    }

#endif

    #endregion " NetDataContractSerializer "

}

#if NetDataContractSerializer || Soap
internal static class StreamExtensions
{
    /// <summary>   A Stream extension method that converts a stream to an XML string. </summary>
    /// <remarks>   David, 2020-03-07. </remarks>
    /// <param name="stream">   The stream to act on. </param>
    /// <returns>   Stream as a string. </returns>
    public static string ToXmlString( this Stream stream )
    {
        XmlWriterSettings settings = new() {
            OmitXmlDeclaration = true,
            Indent = true,
        };

        using StreamReader reader = new( stream );
        NameTable nt = new();
        XmlDocument doc = new();
        doc.LoadXml( reader.ReadToEnd() );
        StringBuilder sb = new();
        using ( XmlWriter xmlWriter = XmlWriter.Create( sb, settings ) )
        {
            doc.WriteTo( xmlWriter );
        }
        return sb.ToString();
    }
}
#endif


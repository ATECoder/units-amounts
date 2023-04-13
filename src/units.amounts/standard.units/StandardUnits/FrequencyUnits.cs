namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A frequency units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class FrequencyUnits
{
    public static Unit Hertz => new( "Hertz", "Hz", TimeUnits.Second.Power( -1 ) );

    public static Unit Kilohertz => new( "Kilohertz", "KHz", 1000.0 * Hertz );

    public static Unit Megahertz => new( "Megahertz", "MHz", 1000000.0 * Hertz );

    public static Unit Gigahertz => new( "Gigahertz", "GHz", 1000000000.0 * Hertz );

    public static Unit RPM => new( "Revolutions per Minute", "rpm", TimeUnits.Minute.Power( -1 ) );
}


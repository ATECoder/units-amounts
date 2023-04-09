namespace cc.isr.UnitsAmounts.StandardUnits;

/// <summary>   A flow units. </summary>
/// <remarks>   2023-04-08. </remarks>
[UnitDefinitionClass]
public static class FlowUnits
{
    public static Unit CubicFootPerHour => new( VolumeUnits.CubicFoot.Name + "/" + TimeUnits.Hour.Name,
                                                            VolumeUnits.CubicFoot.Symbol + "/" + TimeUnits.Hour.Symbol,
                                                            VolumeUnits.CubicFoot / TimeUnits.Hour );

    public static Unit MCFPerDay => new( VolumeUnits.MCF.Name + "/" + TimeUnits.Day.Name,
                                                     VolumeUnits.MCF.Symbol + "/" + TimeUnits.Day.Symbol,
                                                     VolumeUnits.MCF / TimeUnits.Day );

    public static Unit MMCFPerDay => new( VolumeUnits.MMCF.Name + "/" + TimeUnits.Day.Name,
                                                 VolumeUnits.MMCF.Symbol + "/" + TimeUnits.Day.Symbol,
                                                 VolumeUnits.MMCF / TimeUnits.Day );
}


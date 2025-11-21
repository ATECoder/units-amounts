
namespace cc.isr.UnitsAmounts;

/// <summary>   This interface represents a consumer of a unit, such as an Amount. </summary>
/// <remarks>   David, 2021-03-22. </remarks>
public interface IUnitConsumer
{
    /// <summary>   The unit of the consumer. </summary>
    /// <value> The unit. </value>
    public Unit Unit { get; }
}

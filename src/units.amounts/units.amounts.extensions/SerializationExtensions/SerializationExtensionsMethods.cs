namespace cc.isr.UnitsAmounts.SerializationExtensions;

/// <summary>   A unit amount serialization extensions. </summary>
/// <remarks>   2026-01-19. </remarks>
public static class SerializationExtensionsMethods
{
    /// <summary>   Serialize <see cref="UnitType"/> to a string. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <param name="unitType"> Type of the unit. </param>
    /// <returns>   A string. </returns>
    public static string Serialize( this UnitType unitType )
    {
        return System.Text.Json.JsonSerializer.Serialize<UnitType>( unitType );
    }

    /// <summary>   Deserialize to a <see cref="UnitType"/>. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <exception cref="InvalidDataException"> Thrown when an Invalid Data error condition occurs. </exception>
    /// <param name="jsonUnitType"> The json Data representing the <see cref="UnitType"/> Type of the JSON unit. </param>
    /// <returns>   A <see cref="UnitType"/>. </returns>
    public static UnitType ToUnitType( this string? jsonUnitType )
    {
#if NET10_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( jsonUnitType );
#else
        if ( jsonUnitType is null )
            throw new ArgumentNullException( nameof( jsonUnitType ) );
#endif
        return System.Text.Json.JsonSerializer.Deserialize<UnitType>( jsonUnitType ) ?? throw new InvalidDataException( jsonUnitType );
    }

    /// <summary>   Serialize <see cref="Unit"/> to a string. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <param name="unit"> The unit. </param>
    /// <returns>   A string. </returns>
    public static string Serialize( this Unit unit )
    {
        return System.Text.Json.JsonSerializer.Serialize<Unit>( unit );
    }

    /// <summary>   Deserialize to a <see cref="Unit"/>. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <exception cref="InvalidDataException"> Thrown when an Invalid Data error condition occurs. </exception>
    /// <param name="jsonUnit"> The json Data representing the <see cref="Unit"/> Type of the JSON unit. </param>
    /// <returns>   A <see cref="Unit"/>. </returns>
    public static Unit ToUnit( this string? jsonUnit )
    {
#if NET10_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( jsonUnit );
#else
        if ( jsonUnit is null )
            throw new ArgumentNullException( nameof( jsonUnit ) );
#endif

        System.Text.Json.JsonElement jsonElement = System.Text.Json.JsonDocument.Parse( jsonUnit ).RootElement;
        return new( jsonElement.GetProperty( nameof( Unit.Name ) ).ToString() ?? throw new InvalidDataException(),
            jsonElement.GetProperty( nameof( Unit.Symbol ) ).ToString() ?? throw new InvalidDataException(),
            Convert.ToDouble( jsonElement.GetProperty( nameof( Unit.Factor ) ).ToString(), System.Globalization.CultureInfo.CurrentCulture ),
            jsonElement.GetProperty( nameof( Unit.UnitType ) ).ToString().ToUnitType(),
            bool.Parse( jsonElement.GetProperty( nameof( Unit.IsNamed ) ).ToString() ?? throw new InvalidDataException() ) );
    }

    /// <summary>   Serialize <see cref="Amount"/> to a string. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <param name="amount"> The Amount. </param>
    /// <returns>   A string. </returns>
    public static string Serialize( this Amount amount )
    {
        return System.Text.Json.JsonSerializer.Serialize<Amount>( amount );
    }

    /// <summary>   Deserialize to a <see cref="Amount"/>. </summary>
    /// <remarks>   2026-01-19. </remarks>
    /// <exception cref="InvalidDataException"> Thrown when an Invalid Data error condition occurs. </exception>
    /// <param name="jsonAmount"> The json Data representing the <see cref="Amount"/> Type of the JSON Amount. </param>
    /// <returns>   A <see cref="Amount"/>. </returns>
    public static Amount ToAmount( this string jsonAmount )
    {
#if NET10_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( jsonAmount );
#else
        if ( jsonAmount is null )
            throw new ArgumentNullException( nameof( jsonAmount ) );
#endif
        System.Text.Json.JsonElement jsonElement = System.Text.Json.JsonDocument.Parse( jsonAmount ).RootElement;
        return new( Convert.ToDouble( jsonElement.GetProperty( nameof( Amount.Value ) ).ToString(), System.Globalization.CultureInfo.CurrentCulture ),
            jsonElement.GetProperty( nameof( Amount.Unit ) ).ToString().ToUnit() );
    }
}

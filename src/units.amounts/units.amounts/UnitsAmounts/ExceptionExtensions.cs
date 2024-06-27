namespace cc.isr.UnitsAmounts.ExceptionExtensions;

/// <summary>
/// Exception methods for adding exception data and building a detailed exception message.
/// </summary>
internal static class ExceptionDataMethods
{

    /// <summary>
    /// Adds the <paramref name="exception"/> data to <paramref name="value"/> exception.
    /// </summary>
    /// <remarks>
    /// For more info on the external exceptions see:
    /// <see href="http://MSDN.Microsoft.com/en-us/library/system.runtime.InteropServices.SEHException.ASPX"/>.
    /// </remarks>
    /// <param name="value">     The value. </param>
    /// <param name="exception"> The exception. </param>
    /// <returns>
    /// <c>true</c> if it <see cref="Exception"/> is not nothing; otherwise <c>false</c>
    /// </returns>
    public static bool AddExceptionData( Exception value, System.Runtime.InteropServices.ExternalException? exception )
    {
        if ( value is not null && exception is not null )
        {
            value.Data.Add( $"{value.Data.Count}-External.Error.Code", $"{exception.ErrorCode}" );
        }

        return exception is not null;
    }

    /// <summary>
    /// Adds the <paramref name="exception"/> data to <paramref name="value"/> exception.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="value">     The value. </param>
    /// <param name="exception"> The exception. </param>
    /// <returns>
    /// <c>true</c> if it <see cref="Exception"/> is not nothing; otherwise <c>false</c>
    /// </returns>
    public static bool AddExceptionData( Exception value, ArgumentOutOfRangeException? exception )
    {
        if ( value is not null && exception is not null )
        {
            value.Data.Add( $"{value.Data.Count}-Name+Value", $"{exception.ParamName}={exception.ActualValue}" );
        }

        return exception is not null;
    }

    /// <summary>
    /// Adds the <paramref name="exception"/> data to <paramref name="value"/> exception.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="value">     The value. </param>
    /// <param name="exception"> The exception. </param>
    /// <returns>
    /// <c>true</c> if it <see cref="Exception"/> is not nothing; otherwise <c>false</c>
    /// </returns>
    public static bool AddExceptionData( Exception value, ArgumentException? exception )
    {
        if ( value is not null && exception is not null )
        {
            value.Data.Add( $"{value.Data.Count}-Name", exception.ParamName );
        }

        return exception is not null;
    }

    /// <summary>
    /// Adds the <paramref name="exception"/> data to <paramref name="value"/> exception.
    /// </summary>
    /// <param name="value">        The value. </param>
    /// <param name="exception">    The exception. </param>
    /// <returns>
    /// <c>true</c> if it <see cref="Exception"/> is not nothing; otherwise <c>false</c>
    /// </returns>
    public static bool AddExceptionData( Exception value, UnitConversionException? exception )
    {
        if ( value is not null && exception is not null )
        {
            value.Data.Add( $"{value.Data.Count}-{nameof( UnitConversionException.FromUnit )}", exception.FromUnit );
            value.Data.Add( $"{value.Data.Count}-{nameof( UnitConversionException.ToUnit )}", exception.ToUnit );
        }

        return exception is not null;
    }

    /// <summary>
    /// Adds the <paramref name="exception"/> data to <paramref name="value"/> exception.
    /// </summary>
    /// <param name="value">        The value. </param>
    /// <param name="exception">    The exception. </param>
    /// <returns>
    /// <c>true</c> if it <see cref="Exception"/> is not nothing; otherwise <c>false</c>
    /// </returns>
    public static bool AddExceptionData( Exception value, UnknownUnitException? exception )
    {
        if ( value is not null && exception is not null )
        {
            value.Data.Add( $"{value.Data.Count}-{nameof( UnknownUnitException.Name )}", exception.Name );
        }

        return exception is not null;
    }


    /// <summary> Adds exception data from the specified exception. </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="exception"> The exception. </param>
    /// <returns> <c>true</c> if exception was added; otherwise <c>false</c> </returns>
    public static bool AddExceptionData( Exception? exception )
    {
        return exception is not null && (AddExceptionData( exception, exception as ArgumentOutOfRangeException ) ||
               AddExceptionData( exception, exception as ArgumentException ) ||
               AddExceptionData( exception, exception as UnitConversionException ) ||
               AddExceptionData( exception, exception as UnknownUnitException ) ||
               AddExceptionData( exception, exception as System.Runtime.InteropServices.ExternalException ));
    }

}

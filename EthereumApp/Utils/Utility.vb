Public Class Utility
    Public Shared Function ConvertHexToDecimal(hexString As String) As Integer
        Dim decValue As Integer = 0
        decValue = CInt("&H" & hexString)
        Return decValue
    End Function

    Public Shared Function ConvertDecimalToHex(number As Integer) As String
        Return $"0x{Hex(number)}"
    End Function

    Public Shared Function FormatLogMessage(actionName As String, eslapTime As String) As String
        Return $"{actionName} ==> {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} \t {eslapTime}ms"
    End Function

End Class

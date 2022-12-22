Imports System.Configuration
Imports RestSharp

Public Class Etherscan : Implements IEtherscan
    Private apiKey As String
    Private baseApiUrl As String
    Private moduleApi As String

    Public Sub New()
        baseApiUrl = ConfigurationManager.AppSettings.Get("BaseApiUrl")
        moduleApi = ConfigurationManager.AppSettings.Get("ModuleApi")
        apiKey = ConfigurationManager.AppSettings.Get("APIKey")
    End Sub

    Private Function GenActionApi(actionName As String, tag As String, Optional index As String = "") As String
        Dim url As String = $"{moduleApi}&apikey={apiKey}&action={actionName}&tag={tag}"
        If Not String.IsNullOrEmpty(index) Then
            url = $"{url}&index={index}"
        End If
        Return url
    End Function


    Public Function getBlockByNumber(blockNumber As String) As RPCModelBlock Implements IEtherscan.getBlockByNumber
        Dim stopWatch As New Stopwatch
        stopWatch.Start()

        Dim result As RPCModelBlock
        Try

            Dim client = New RestClient(baseApiUrl)
            Dim request = New RestRequest(GenActionApi("eth_getBlockByNumber", blockNumber), Method.Get)
            Dim response = client.Execute(request)
            result = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RPCModelBlock)(response.Content)

        Catch
            Console.WriteLine($"Can not found this Block with blockNumber: {blockNumber }")
            result = New RPCModelBlock
        End Try

        Dim logMessage As String = Utility.FormatLogMessage("eth_getBlockByNumber", Stopwatch.ElapsedMilliseconds.ToString())
        Console.WriteLine(logMessage)
        Loggers.TraceLog(logMessage)
        stopWatch.Stop()

        Return result

    End Function

    Public Function getBlockTransactionCountByNumber(blockNumber As String) As RPCModelCounter Implements IEtherscan.getBlockTransactionCountByNumber
        Dim stopWatch As New Stopwatch
        stopWatch.Start()
        Dim result As RPCModelCounter
        Try

            Dim client = New RestClient(baseApiUrl)
            Dim request = New RestRequest(GenActionApi("eth_getBlockTransactionCountByNumber", blockNumber), Method.Get)
            Dim response = client.Execute(request)
            result = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RPCModelCounter)(response.Content)


        Catch
            Console.WriteLine($"Can not found this BlockTransaction with blockNumber: {blockNumber }")
            result = New RPCModelCounter
        End Try

        Dim logMessage As String = Utility.FormatLogMessage("eth_getBlockByNumber", stopWatch.ElapsedMilliseconds.ToString())
        Console.WriteLine(logMessage)
        Loggers.TraceLog(logMessage)
        stopWatch.Stop()

        Return result
    End Function

    Public Function getTransactionByBlockNumberAndIndex(blockNumber As String, index As String) As RPCModelTransaction Implements IEtherscan.getTransactionByBlockNumberAndIndex
        Dim stopWatch As New Stopwatch
        stopWatch.Start()
        Dim result As RPCModelTransaction

        Try

            Dim client = New RestClient(baseApiUrl)
            Dim request = New RestRequest(GenActionApi("eth_getTransactionByBlockNumberAndIndex", blockNumber, index), Method.Get)
            Dim response = client.Execute(request)
            result = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RPCModelTransaction)(response.Content)
        Catch
            Console.WriteLine($"Can not found this Transaction with blockNumber: {blockNumber } and Index: {index}")
            result = New RPCModelTransaction
        End Try

        Dim logMessage As String = Utility.FormatLogMessage("eth_getBlockByNumber", stopWatch.ElapsedMilliseconds.ToString())
        Console.WriteLine(logMessage)
        Loggers.TraceLog(logMessage)
        stopWatch.Stop()

        Return result
    End Function
End Class

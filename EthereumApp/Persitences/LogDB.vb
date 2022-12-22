Imports System.Configuration
Imports System.Data.SqlClient
Imports MySqlConnector

Public Class LogDB : Implements ITransactionLog
    Private connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("ConnString").ConnectionString
    End Sub

    Public Sub SaveBlock(block As Block) Implements ITransactionLog.SaveBlock
        Dim stopWatch As New Stopwatch
        stopWatch.Start()

        Dim conn As New MySqlConnection
        Try
            conn.ConnectionString = connectionString
            conn.Open()

            Using sqlCommand As New MySqlCommand()
                With sqlCommand.CommandText = $"INSERT INTO `blocks` (`blockNumber`, `hash`, `parentHash`, `miner`, `blockReward`, `gasLimit`, `gasUsed`) VALUES ({Utility.ConvertHexToDecimal(block.number)}, '{block.hash}', '{block.parentHash}', '{block.miner}', {0}, {Utility.ConvertHexToDecimal(block.gasLimit)}, {Utility.ConvertHexToDecimal(block.gasUsed)})"
                End With

                sqlCommand.ExecuteNonQuery()

            End Using

            Dim logMessage As String = Utility.FormatLogMessage("SaveTransaction", stopWatch.ElapsedMilliseconds.ToString())
            Console.WriteLine(logMessage)
            Loggers.TraceLog(logMessage)
            stopWatch.Stop()

        Catch
        Finally
            conn.Close()
        End Try

    End Sub

    Public Sub SaveTransaction(tran As Transaction) Implements ITransactionLog.SaveTransaction
        Dim stopWatch As New Stopwatch
        stopWatch.Start()

        Dim conn As New MySqlConnection
        Try
            conn.ConnectionString = connectionString
            conn.Open()

            Using sqlCommand As New MySqlCommand()
                With sqlCommand.CommandText = $"INSERT INTO `transactions`(`blockID`, `hash`, `from`, `to`, `value`, `gas`, `gasPrice`, `transactionIndex`) VALUES ({Utility.ConvertHexToDecimal(tran.blockNumber)},'{tran.hash}','{tran.from}','{tran.to}',{Utility.ConvertHexToDecimal(tran.value)},{Utility.ConvertHexToDecimal(tran.gas)},{Utility.ConvertHexToDecimal(tran.gasPrice)},{Utility.ConvertHexToDecimal(tran.transactionIndex)})"
                End With
                sqlCommand.ExecuteNonQuery()
            End Using

            Dim logMessage As String = Utility.FormatLogMessage("SaveTransaction", stopWatch.ElapsedMilliseconds.ToString())
            Console.WriteLine(logMessage)
            Loggers.TraceLog(logMessage)
            stopWatch.Stop()
        Catch
        Finally
            conn.Close()
        End Try
    End Sub
End Class

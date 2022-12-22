Imports System.Configuration
Imports System.Threading
Imports RestSharp

Module AppStartModule
    Dim transactionLog As ITransactionLog
    Dim etherscanService As IEtherscan
    Dim durationApp As Integer

    Sub Main()
        durationApp = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DurationApp"))
        transactionLog = New LogDB()
        etherscanService = New Etherscan()
        DoProgress()

        'Console.WriteLine(Utility.ConvertDecimalToHex(68943))
        'Console.WriteLine(Utility.ConvertHexToDecimal("0x10d4f"))
        'Console.ReadKey()
    End Sub

    Private Sub DoProgress()

        Dim startBlockId = Convert.ToInt32(ConfigurationManager.AppSettings.Get("StartBlockID"))
        Dim limitBlockId = Convert.ToInt32(ConfigurationManager.AppSettings.Get("LimitBlockID"))
        Dim intBlockId As Integer = startBlockId


        Do While intBlockId <= limitBlockId

            Dim hexBlockId = Utility.ConvertDecimalToHex(intBlockId)

            'get BlockByNumber from Api
            Dim block = etherscanService.getBlockByNumber(hexBlockId)

            If block Is DBNull.Value And block.result Is DBNull.Value Then
                ' save this block into `blocks` table in database
                transactionLog.SaveBlock(block.result)

                'get BlockTransactionCount ByNumber from Api
                Dim transactionCount = etherscanService.getBlockTransactionCountByNumber(hexBlockId)
                If transactionCount Is DBNull.Value And transactionCount.result Is DBNull.Value Then
                    Dim numberTrasaction = Utility.ConvertHexToDecimal(transactionCount.result)
                    If numberTrasaction > 0 Then
                        ' browse all index of transaction list
                        For idx As Integer = 1 To numberTrasaction
                            Dim hexIndex = Utility.ConvertDecimalToHex(idx)
                            'get Transaction ByBlockNumber And Index from Api
                            Dim transactionDetail = etherscanService.getTransactionByBlockNumberAndIndex(hexBlockId, hexIndex)
                            If transactionDetail Is DBNull.Value Then
                                ' save this block into `transactions` table in database
                                transactionLog.SaveTransaction(transactionDetail.result)
                            End If
                        Next
                    End If
                End If

            End If

            Thread.Sleep(durationApp)
            intBlockId += 1
        Loop


    End Sub
End Module

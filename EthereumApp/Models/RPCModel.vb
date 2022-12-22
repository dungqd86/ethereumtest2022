Public Class RPCModelBlock
    Public Property jsonrpc As String
    Public Property id As Integer
    Public Property result As Block
End Class

Public Class RPCModelCounter
    Public Property jsonrpc As String
    Public Property id As Integer
    Public Property result As String 'Hex string
End Class


Public Class RPCModelTransaction
    Public Property jsonrpc As String
    Public Property result As Transaction
    Public Property id As Integer
End Class

'Public Class BlockNumber
'    Public Property accessList() As Object
'    Public Property blockHash As String
'    Public Property blockNumber As String
'    Public Property chainId As String
'    Public Property condition As Object
'    Public Property creates As Object
'    Public Property from As String
'    Public Property gas As String
'    Public Property gasPrice As String
'    Public Property hash As String
'    Public Property input As String
'    Public Property maxFeePerGas As String
'    Public Property maxPriorityFeePerGas As String
'    Public Property nonce As String
'    Public Property publicKey As String
'    Public Property r As String
'    Public Property raw As String
'    Public Property s As String
'    Public Property _to As String
'    Public Property transactionIndex As String
'    Public Property type As String
'    Public Property v As String
'    Public Property value As String
'End Class

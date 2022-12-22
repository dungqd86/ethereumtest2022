Public Interface IEtherscan
    Function getBlockByNumber(blockNumber As String) As RPCModelBlock
    Function getBlockTransactionCountByNumber(blockNumber As String) As RPCModelCounter
    Function getTransactionByBlockNumberAndIndex(blockNumber As String, index As String) As RPCModelTransaction

End Interface

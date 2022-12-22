Public Interface ITransactionLog
    Sub SaveBlock(block As Block)
    Sub SaveTransaction(tran As Transaction)
End Interface

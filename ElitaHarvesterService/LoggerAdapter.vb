Imports Assurant.Elita.WorkerFramework
Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class LoggerAdapter
    Implements ILogger

    Public Sub WriteException(exception As Exception) Implements ILogger.WriteException
        Logger.AddError(exception)
    End Sub

    Public Sub WriteInformation(message As String) Implements ILogger.WriteInformation
        Logger.AddInfo(message)
    End Sub

    Public Sub WriteTrace(message As String) Implements ILogger.WriteTrace
        Logger.AddDebugLog(message)
    End Sub

    Public Sub WriteWarning(message As String) Implements ILogger.WriteWarning
        Logger.AddWarning(message)
    End Sub
End Class

Imports ElitaHarvesterService.Antivirus.ActivateProduct
Imports Assurant.ElitaPlus.BusinessObjectsNew

Public NotInheritable Class ReActivateProductTask
    Inherits AntivirusProductTaskBase

#Region "Constructors"
    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

#Region "Protected Methods"

    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Dim cancelProductTask As CancelProductTask
        cancelProductTask = DirectCast(TaskFactory.CreateTask(Codes.TASK__AV_CANCEL_PRODUCT, Me.PublishedTask, MyBase.MachineName, MyBase.ProcessThreadName), CancelProductTask)
        cancelProductTask.Execute()

        Dim activateProductTask As ActivateProductTask
        activateProductTask = DirectCast(TaskFactory.CreateTask(Codes.TASK__AV_ACTIVATE_PRODUCT, Me.PublishedTask, MyBase.MachineName, MyBase.ProcessThreadName), ActivateProductTask)

        activateProductTask.Execute()

        Logger.AddDebugLogExit()
    End Sub
#End Region




End Class

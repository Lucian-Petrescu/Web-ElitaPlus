Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class TaskFactory
    Public Shared Function CreateTask(ByVal ptask As PublishedTask, ByVal machineName As String, ByVal processThreadName As String) As ITask
        Return CreateTask(ptask.Task.Code, ptask, machineName, processThreadName)
    End Function

    Friend Shared Function CreateTask(ByVal taskCode As String, ByVal ptask As PublishedTask, ByVal machineName As String, ByVal processThreadName As String) As ITask
        Dim task As TaskBase = Nothing
        Select Case taskCode
            Case Codes.TASK_FBL_SLA_MONTR
                task = New FalabellaSLAMonitoringTask(machineName, processThreadName)
            Case Codes.TASK__AV_ACTIVATE_PRODUCT
                task = New ActivateProductTask(machineName, processThreadName)
            Case Codes.TASK__AV_CANCEL_PRODUCT
                task = New CancelProductTask(machineName, processThreadName)
            Case Codes.TASK__AV_UPDATE_PRODUCT
                task = New UpdateProductTask(machineName, processThreadName)
            Case Codes.TASK__AV_RE_ACTIVATE_PRODUCT
                task = New ReActivateProductTask(machineName, processThreadName)
            Case Codes.TASK__VENDOR_INVOICE_LOAD
                task = New InvoiceFileLoadTask(machineName, processThreadName)
            Case Codes.TASK__VENDOR_AUTHORIZATION_LOAD
                task = New VendorAuthorizationFileLoadTask(machineName, processThreadName)
            Case Codes.TASK__REPAIR_LOGISTIC_AUTHORIZATION_LOAD
                'task = New RepairLogisticAuthorizationFileLoadTask(machineName, processThreadName)
            Case Codes.TASK_ACSELX_DEDCOLL_FILE_LOAD
                task = New AcselXToElitaClaimFileLoadTask(machineName, processThreadName)
            Case Codes.TASK_INV_MGMT_FILE_LOAD
                task = New InvMgmtFileLoadTask(machineName, processThreadName)
            Case Codes.TASK_SEND_EMAIL_NOTIFICATION
                task = New SendAMLEmailTask(machineName, processThreadName)
            Case Codes.TASK_NOTIFY_DIGITAL_SERVICE
                task = New NotifyDigitalServiceTask(machineName, processThreadName)
            Case Codes.TASK_SEND_WELCOME_EMAIL
                task = New SendWelcomeEmailTask(machineName, processThreadName)
            Case Codes.TASK_SEND_ENR_REWARD_EMAIL
                task = New SendEnrollRewardTask(machineName, processThreadName)
            Case Codes.TASK_SEND_CANC_REQ_REJ_EMAIL
                task = New SendCancelRequestTask(machineName, processThreadName)
            Case Codes.TASK_SEND_CLM_GIFTCARD_EMAIL
                task = New SendClaimGiftCardEMailTask(machineName, processThreadName)
            Case Codes.TASK_RESEND_WELCOME_EMAIL
                task = New ReSendWelcomeEmailTask(machineName, processThreadName)
            Case Codes.TASK_RESEND_ENR_REWARD_EMAIL
                task = New ReSendEnrollRewardTask(machineName, processThreadName)
            Case Codes.TASK_RESEND_CLM_GIFTCARD_EMAIL
                task = New ReSendClaimGiftCardEMailTask(machineName, processThreadName)
            Case Codes.TaskSendOutboundEmail
                task = New SendOutboundEmailTask(machineName, processThreadName)
            Case Codes.TaskResendOutboundEmail
                task = New ReSendOutboundEmailTask(machineName, processThreadName)
            Case Codes.TASK_APPLECARE_ENROLL_ITEM_UPD
                task = New AppleCareEnrollItemUpdateTask(machineName, processThreadName)
            Case Codes.TASK_ASM_UPDATE_CERT_IMEI
                task = New UpdateMexicoImeiTask(machineName, processThreadName)
            Case Codes.TASK_INVOKE_FALABELLA
                task = New FalabellaIntegration(machineName, processThreadName)
            Case Else
                Throw New InvalidOperationException(String.Format("Task {0} is not supported", ptask.Task.Description))
        End Select

        task.PublishedTask = ptask

        Return task
    End Function
End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/7/2015)  ********************

Public Class AFAMaintainence
    Inherits BusinessObjectBase

#Region "ReconciliationDV"

    Public Class AFAProcessStatusDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DEALER_ID As String = "dealer_id"
        Public Const COL_NAME_PROCESS_TYPE As String = "process_type"
        Public Const COL_NAME_STATUS As String = "status"
        Public Const COL_NAME_COMMENTS As String = "comments"
        Public Const COL_NAME_START_DATE_TIME As String = "start_date_time"
        Public Const COL_NAME_COMPLETION_DATE_TIME As String = "completetion_date_time"


#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region



#Region "Properties"

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFAMaintainenceDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReconciliationDAL.COL_NAME_DEALER_ID, value)
        End Set
    End Property

    Public Property ProcessType As String
        Get
            CheckDeleted()
            If Row(AFAMaintainenceDAL.COL_NAME_PROCESS_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAMaintainenceDAL.COL_NAME_PROCESS_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAMaintainenceDAL.COL_NAME_PROCESS_TYPE, value)
        End Set
    End Property

    Public Property Status As String
        Get
            CheckDeleted()
            If Row(AFAMaintainenceDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAMaintainenceDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAMaintainenceDAL.COL_NAME_STATUS, value)
        End Set
    End Property

    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(AFAMaintainenceDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAMaintainenceDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAMaintainenceDAL.COL_NAME_COMMENTS, value)
        End Set
    End Property

    Public Property StartDateTime As DateTimeType
        Get
            CheckDeleted()
            If Row(AFAMaintainenceDAL.COL_NAME_START_DATE_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AFAMaintainenceDAL.COL_NAME_START_DATE_TIME), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAMaintainenceDAL.COL_NAME_START_DATE_TIME, value)
        End Set
    End Property

    Public Property CompletionDateTime As DateTimeType
        Get
            CheckDeleted()
            If Row(AFAMaintainenceDAL.COL_NAME_COMPLETION_DATE_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AFAMaintainenceDAL.COL_NAME_COMPLETION_DATE_TIME), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAMaintainenceDAL.COL_NAME_COMPLETION_DATE_TIME, value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Shared Function GetProcessStatus(dealerId As Guid, languageId As Guid, firstDayOfMonth As Date, lastDayOfMonth As Date) As AFAProcessStatusDV
        Try

            Dim dal As New AFAMaintainenceDAL
            Return New AFAProcessStatusDV(dal.GetProcessStatus(dealerId, languageId, firstDayOfMonth, lastDayOfMonth).Tables(0))

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ReRunReconciliation(dealerId As Guid, firstDayOfMonth As Date, lastDayOfMonth As Date, userName As String) As Boolean
        Try

            Dim dal As New AFAMaintainenceDAL
            Return dal.ReRunReconciliation(dealerId, firstDayOfMonth, lastDayOfMonth, userName)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ReRunInvoice(dealerId As Guid, firstDayOfMonth As Date, lastDayOfMonth As Date, userName As String) As Boolean
        Try

            Dim dal As New AFAMaintainenceDAL
            Return dal.ReRunInvoice(dealerId, firstDayOfMonth, lastDayOfMonth, userName)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class




'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/10/2017)********************


Public Class CustCommunicationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CUST_COMMUNICATION"
    Public Const TABLE_KEY_NAME As String = "cust_communication_id"

    Public Const COL_NAME_CUST_COMMUNICATION_ID As String = "cust_communication_id"
    Public Const COL_NAME_CUSTOMER_ID As String = "customer_id"
    Public Const COL_NAME_ENTITY_NAME As String = "entity_name"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_DIRECTION As String = "direction"
    Public Const COL_NAME_COMMUNICATION_CHANNEL As String = "communication_channel"
    Public Const COL_NAME_COMMUNICATION_FORMAT As String = "communication_format"
    Public Const COL_NAME_CUST_CONTACT_ID As String = "cust_contact_id"
    Public Const COL_NAME_COMMUNICATION_COMPONENT As String = "communication_component"
    Public Const COL_NAME_COMPONENT_REF_ID As String = "component_ref_id"
    Public Const COL_NAME_COMMUNICATION_STATUS As String = "communication_status"
    Public Const COL_NAME_COMM_RESPONSE_ID As String = "comm_response_id"
    Public Const COL_NAME_COMM_RESPONSE_XML As String = "comm_response_xml"
    Public Const COL_NAME_IS_RETRYABLE As String = "is_retryable"
    Public Const COL_NAME_RETRY_COMPO_REFERENCE As String = "retry_compo_reference"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cust_communication_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



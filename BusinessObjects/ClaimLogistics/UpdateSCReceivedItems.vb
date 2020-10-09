
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectData.Common

Public Class UpdateSCReceivedItems
    Inherits BusinessObjectBase

#Region "Member Variables"

    Private claimStr As String = ""
    Private _serviceCenterId As Guid = Guid.Empty
#End Region

#Region "Constants"

    Public TABLE_NAME As String = "UpdateSCReceivedItems"
    Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Public TABLE_NAME_CLAIM As String = "CLAIM"
    Public TABLE_NAME_CLAIM_NUMBER As String = "CLAIM_NUMBERS"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Private Const SOURCE_COL_PICKLIST_NUMBER As String = "PICK_LIST_NUMBER"
    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_CLAIM_ID = "CLAIM_ID"
    Private Const DELIMITER = ":"
    Public Const DATA_COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"

#End Region

#Region "Constructors"

    Public Sub New(ds As UpdateSCReceivedItemsDs)
        MyBase.New()
        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ds As UpdateSCReceivedItemsDs)
        Try
            If ds.UpdateSCReceivedItems.Count = 0 Then Exit Sub
            With ds.UpdateSCReceivedItems.Item(0)
                PickListNumber = .PICK_LIST_NUMBER
                ServiceCenterCode = .SERVICE_CENTER_CODE

                Dim i As Integer
                For i = 0 To ds.CLAIMS.Count - 1
                    If ds.CLAIMS(i).CLAIM_NUMBER IsNot Nothing AndAlso ds.CLAIMS(i).CLAIM_NUMBER <> "" Then
                        claimStr = claimStr & ds.CLAIMS(i).CLAIM_NUMBER & DELIMITER
                    End If
                Next
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Dim selfThrownException As Boolean = False
        Dim row As DataRow

        Try
            Validate()

            PickupListHeader.UpdatePickListStatus_Received(PickListNumber, ServiceCenterID, claimStr)

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As DALConcurrencyAccessException
            Throw ex
        Catch ex As DataBaseUniqueKeyConstraintViolationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub MapDataSet(ds As UpdateSCReceivedItemsDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ds As UpdateSCReceivedItemsDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub


#End Region


#Region "Properties"

    <ValueMandatory("")> _
    Public Property PickListNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PICKLIST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PICKLIST_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PICKLIST_NUMBER, Value)
        End Set
    End Property


    Public Property ServiceCenterCode As String
        Get
            If Row(DATA_COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SERVICE_CENTER_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceCenterID As Guid
        Get
            If _serviceCenterId.Equals(Guid.Empty) AndAlso ServiceCenterCode IsNot Nothing AndAlso ServiceCenterCode <> "" Then

                Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)

                If dvServiceCenter IsNot Nothing AndAlso dvServiceCenter.Count > 0 Then
                    _serviceCenterId = LookupListNew.GetIdFromCode(dvServiceCenter, ServiceCenterCode)

                    If _serviceCenterId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("UpdateSCReceivedItems Error: ", INVALID_SERVICE_CENTER_CODE)
                    End If
                Else
                    Throw New BOValidationException("UpdateSCReceivedItems Error: ", INVALID_SERVICE_CENTER_CODE)
                End If

            End If

            Return _serviceCenterId
        End Get
    End Property


#End Region

End Class


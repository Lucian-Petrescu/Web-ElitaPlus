Imports System.Text.RegularExpressions

Public Class GetExtendedClaimStatusList
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COMPANY_GROUP_CODE As String = "COMPANY_GROUP_CODE"
    Private Const TABLE_NAME As String = "GetExtendedClaimStatusList"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID = "claim_status_by_group_id"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetExtendedClaimStatusListDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _company_group_id As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetExtendedClaimStatusListDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GetExtendedClaimStatusListDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetExtendedClaimStatusList Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetExtendedClaimStatusListDs)
        Try
            If ds.GetExtendedClaimStatusList.Count = 0 Then Exit Sub
            With ds.GetExtendedClaimStatusList.Item(0)
                If Not .IsCOMPANY_GROUP_CODENull Then Me.CompanyGroupCode = .COMPANY_GROUP_CODE
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetExtendedClaimStatusList Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property CompanyGroupCode() As String
        Get
            If Row(Me.DATA_COL_NAME_COMPANY_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_COMPANY_GROUP_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COMPANY_GROUP_CODE, Value)
        End Set
    End Property


    Private ReadOnly Property CompanyGroupID() As Guid
        Get
            If Me._company_group_id.Equals(Guid.Empty) AndAlso Not Me.CompanyGroupCode Is Nothing AndAlso Me.CompanyGroupCode <> "" Then

                Dim list As DataView = LookupListNew.GetCompanyGroupLookupList()
                If list Is Nothing Then
                    Throw New BOValidationException("Get Extended Claim Status List Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                Me._company_group_id = LookupListNew.GetIdFromCode(list, Me.CompanyGroupCode)
                If _company_group_id.Equals(Guid.Empty) Then
                    Throw New BOValidationException("Get Extended Claim Status List Error: ", Common.ErrorCodes.ERR_INVALID_COMPANY_GROUP)
                End If
                list = Nothing
            Else
                Me._company_group_id = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            End If

            Return Me._company_group_id

        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim objStatusDs As DataSet = ClaimStatusByGroup.LoadListByCompanyGroup(Me.CompanyGroupID)

            If objStatusDs Is Nothing Then
                Throw New BOValidationException("Get Extended Claim Status List Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
            ElseIf objStatusDs.Tables.Count > 0 AndAlso objStatusDs.Tables(0).Rows.Count > 0 Then
                objStatusDs.Tables(0).Columns.Remove(Me.COL_NAME_CLAIM_STATUS_BY_GROUP_ID)
                Return (XMLHelper.FromDatasetToXML(objStatusDs))
            ElseIf objStatusDs.Tables.Count > 0 AndAlso objStatusDs.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("Get Extended Claim Status List Error: ", Common.ErrorCodes.ERR_INVALID_NO_EXTENDED_CLAIM_STATUS_COMPANY_GROUP)
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class


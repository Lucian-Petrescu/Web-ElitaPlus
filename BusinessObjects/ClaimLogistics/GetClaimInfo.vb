Imports System.Text.RegularExpressions

Public Class GetClaimInfo
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "ELITA_CLAIM_NUMBER"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "CUSTOMER_NAME"
    Public Const DATA_COL_NAME_CUSTOMER_PHONE As String = "CUSTOMER_PHONE"
    Public Const DATA_COL_NAME_AUTHORIZATION_NUMBER As String = "BBY_CLAIM_NUMBER"
    Public Const DATA_COL_NAME_INCLUDE_STATUS_HISTORY As String = "INCLUDE_STATUS_HISTORY"
    Private Const TABLE_NAME As String = "GetClaimInfo"
    Private Const DATASET_NAME As String = "GetClaimInfo"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetClaimInfoDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _claimId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetClaimInfoDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Sub Load(ByVal ds As GetClaimInfoDs)
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
            Throw New ElitaPlusException("GetClaimInfo Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetClaimInfoDs)
        Try
            If ds.GetClaimInfo.Count = 0 Then Exit Sub
            With ds.GetClaimInfo.Item(0)
                If Not .IsELITA_CLAIM_NUMBERNull Then
                    Me.ClaimNumber = ds.GetClaimInfo.Item(0).ELITA_CLAIM_NUMBER
                End If

                If Not .IsBBY_CLAIM_NUMBERNull Then
                    Me.AuthorizationNumber = ds.GetClaimInfo.Item(0).BBY_CLAIM_NUMBER
                End If

                If Not .IsCUSTOMER_NAMENull Then
                    Me.CustomerName = ds.GetClaimInfo.Item(0).CUSTOMER_NAME
                End If

                If Not .IsCUSTOMER_PHONENull Then
                    Me.CustomerPhone = ds.GetClaimInfo.Item(0).CUSTOMER_PHONE
                End If

                If Me.ClaimNumber Is Nothing AndAlso Me.CustomerName Is Nothing AndAlso Me.CustomerPhone Is Nothing AndAlso Me.AuthorizationNumber Is Nothing Then
                    Throw New BOValidationException("GetClaimInfo Error: ", Common.ErrorCodes.AT_LEAST_ONE_FIELD_REQUIRED)
                End If

                Me.IncludeStatusHistory = ds.GetClaimInfo.Item(0).INCLUDE_STATUS_HISTORY
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValidStringLength("", Max:=50)> _
    Public Property ClaimNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_CLAIM_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
Public Property CustomerName() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_CUSTOMER_NAME), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
Public Property CustomerPhone() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_CUSTOMER_PHONE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property AuthorizationNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_AUTHORIZATION_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property IncludeStatusHistory() As String
        Get
            If Row(Me.DATA_COL_NAME_INCLUDE_STATUS_HISTORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_INCLUDE_STATUS_HISTORY), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_INCLUDE_STATUS_HISTORY, Value)
        End Set
    End Property

    Private ReadOnly Property ClaimID() As Guid
        Get
            If _claimId.Equals(Guid.Empty) And Not Me.ClaimNumber Is Nothing Then
                Me._claimId = Claim.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, Me.ClaimNumber)

                If Me._claimId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetClaimInfo Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                End If

            End If

            Return Me._claimId
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim dsClaim As DataSet = PickupListHeader.GetClaimInfo(Me.ClaimID, Me.IncludeStatusHistory, Me.CustomerName, Me.CustomerPhone, Me.AuthorizationNumber)

            If dsClaim Is Nothing Or dsClaim.Tables.Count <= 0 Or dsClaim.Tables(0).Rows.Count <> 1 Then
                Throw New BOValidationException("GetClaimInfo Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
            Else
                'Dim oClaim As New Claim(New Guid(CType(dsClaim.Tables(0).Rows(0)(DALObjects.ClaimDAL.COL_NAME_CLAIM_ID), Byte())))
                Dim oClaim As Claim
                oClaim = ClaimFacade.Instance.GetClaim(Of Claim)((New Guid(CType(dsClaim.Tables(0).Rows(0)(DALObjects.ClaimDAL.COL_NAME_CLAIM_ID), Byte()))))
                Dim assurantPay As String = CType(oClaim.AssurantPays, String)
                dsClaim.DataSetName = Me.DATASET_NAME
                Dim excludeTags As ArrayList = New ArrayList()
                excludeTags.Add("/GetClaimInfo/CLAIM/CLAIM_ID")
                excludeTags.Add("/GetClaimInfo/CLAIM/CREATED_DATE")

                If Not Me.IncludeStatusHistory Is Nothing AndAlso Me.IncludeStatusHistory = "Y" Then
                    excludeTags.Add("/GetClaimInfo/CLAIM_STATUS_HISTORY/CLAIM_ID")
                    excludeTags.Add("/GetClaimInfo/CLAIM_STATUS_HISTORY/CLAIM_NUMBER")
                End If

                Return XMLHelper.FromDatasetToXML(dsClaim, excludeTags, True).Replace("|REPLACE_VALUE|", assurantPay)
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class

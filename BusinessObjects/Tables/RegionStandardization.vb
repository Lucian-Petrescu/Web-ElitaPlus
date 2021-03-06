'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/17/2005)  ********************

Public Class RegionStandardization
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RegionStandardizationDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RegionStandardizationDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

    Public Const COL_NAME_DESCRIPTION As String = RegionStandardizationDAL.COL_NAME_DESCRIPTION
    Public Const COL_NAME_REGION As String = RegionStandardizationDAL.COL_NAME_REGION
    Public Const RISK_GROUP As String = "Risk_Group"
    'not being used
    'Public Const COMPANY_ID_COL As String = RiskTypeDAL.COMPANY_ID_COL 
    Public Const DESCRIPTION_COL As String = RiskTypeDAL.DESCRIPTION_COL
    Public Const RISK_TYPE_ID_COL As String = RiskTypeDAL.RISK_TYPE_ID_COL
    Public Const RISK_TYPE_ENGLISH_COL As String = RiskTypeDAL.RISK_TYPE_ENGLISH_COL
    Public Const LANGUAGE_ID_COL As String = RiskTypeDAL.LANGUAGE_ID_COL

    Public Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_REGION_ALIAS"

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(RegionStandardizationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionStandardizationDAL.COL_NAME_REGION_STANDARDIZATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255), ValidUniqueness("")> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(RegionStandardizationDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionStandardizationDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegionStandardizationDAL.COL_NAME_DESCRIPTION, Value.Trim)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If row(RegionStandardizationDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionStandardizationDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegionStandardizationDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If row(RegionStandardizationDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionStandardizationDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegionStandardizationDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RegionStandardizationDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetRegionAliasList(ByVal descriptionMask As String, _
                                               ByVal regionIdForSearch As Guid, _
                                               ByVal countryId As Guid) As DataView
        Try
            Dim dal As New RegionStandardizationDAL
            Dim ds As Dataset

            ds = dal.GetRegionAliasList(descriptionMask, regionIdForSearch, _
                                     countryId)
            Return ds.Tables(RegionStandardizationDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRegionAliasList(ByVal descriptionMask As String, _
                                                ByVal regionIdForSearch As Guid, _
                                                ByVal userCompanies As ArrayList) As DataView
        Try
            Dim dal As New RegionStandardizationDAL
            Dim ds As Dataset

            If userCompanies.Count > 0 Then
                ds = dal.GetRegionAliasList(descriptionMask, regionIdForSearch, Country.GetCountries(userCompanies))
            Else
                Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMPANYID_REQUIRED, GetType(Region), Nothing, "CompanyID", Nothing)
                Throw New BOValidationException(New ValidationError() {err}, "Company ID")
            End If

            Return (ds.Tables(RegionStandardizationDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView
        Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim countryId As Guid = company.BusinessCountryId
        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(RegionStandardizationDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(RegionStandardizationDAL.COL_NAME_REGION) = String.Empty
        row(RegionStandardizationDAL.COL_NAME_REGION_STANDARDIZATION_ID) = id.ToByteArray
        row(RegionStandardizationDAL.COL_NAME_COUNTRY_ID) = countryId


        dt.Rows.Add(row)

        Return (dv)

    End Function

#End Region

#Region "Custom validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidUniqueness
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, MSG_UNIQUE_VIOLATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As RegionStandardization = CType(objectToValidate, RegionStandardization)

            If obj.IsNew Then 'when adding new
                Dim dv As DataView = GetRegionAliasList(obj.Description.Trim, obj.RegionId, obj.CountryId)
                If (dv.Count > 0) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region
#Region "SearchDV"
    Public Class RegionStandardizationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REGION_STANDARDIZATION_ID As String = RegionStandardizationDAL.COL_NAME_REGION_STANDARDIZATION_ID
        Public Const COL_REGION As String = RegionStandardizationDAL.COL_NAME_REGION
        Public Const COL_REGION_ALIAS As String = RegionStandardizationDAL.COL_NAME_DESCRIPTION
        Public Const COL_COUNTRY_ID As String = RegionStandardizationDAL.COL_NAME_COUNTRY_ID
        Public Const COL_COUNTRY_NAME As String = RegionStandardizationDAL.COL_NAME_COUNTRY_NAME
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region
End Class




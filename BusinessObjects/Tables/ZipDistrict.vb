'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/22/2004)  ********************

Public Class ZipDistrict
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const NO_ERROR As Integer = 100
    Public Const COUNTRY_ID_NOT_FOUND As Integer = -2
    Public Const LOCATOR_LENGTH_NOT_FOUND As Integer = -1
    Public Const PARENT_EXIST As Integer = 1
    Public Const ZIP_FORMAT_NOT_VALID As Integer = 2
    Public Const CHILD_EXIST As Integer = 3
    Public Const INSERTING_CHILD_FAILD As Integer = 4
    Public Const DELETING_CHILD_FAILD As Integer = 1
    Public Const DELETING_PARENT_FAILD As Integer = 2
    Public Const ZIP_CODES_MAX_RANGE As Integer = 100
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ZipDistrictDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            '   Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ZipDistrictDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    'Private Sub Initialize()
    '    Me.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
    'End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ZipDistrictDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ZipDistrictDAL.COL_NAME_ZIP_DISTRICT_ID), Byte()))
            End If
        End Get
    End Property

    '<ValueMandatory("")> _
    'Public Property CompanyId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(ZipDistrictDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(ZipDistrictDAL.COL_NAME_COMPANY_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(ZipDistrictDAL.COL_NAME_COMPANY_ID, Value)
    '    End Set
    'End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(ZipDistrictDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ZipDistrictDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ZipDistrictDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property ShortDesc As String
        Get
            CheckDeleted()
            If Row(ZipDistrictDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ZipDistrictDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ZipDistrictDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ZipDistrictDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ZipDistrictDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ZipDistrictDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    'Private _countryId As Guid
    'Private ReadOnly Property CountryId() As Guid
    '    Get
    '        If Me._countryId.Equals(Guid.Empty) Then
    '            Dim company As New company(Me.CompanyId)
    '            Me._countryId = company.BusinessCountryId
    '        End If
    '        Return Me._countryId
    '    End Get
    'End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ZipDistrictDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(searchCode As String, searchDesc As String, _
                                    oCountryId As Guid) As ZipDistrictSearchDV
        Try
            Dim dal As New ZipDistrictDAL
            Dim oCountryIds As ArrayList

            If DALBase.IsNothing(oCountryId) Then
                ' Get All User Countries
                oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            Else
                oCountryIds = New ArrayList
                oCountryIds.Add(oCountryId)
            End If

            Return New ZipDistrictSearchDV(dal.LoadList(oCountryIds, searchCode, searchDesc).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ZipDistrictSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_COUNTRY As String = ZipDistrictDAL.COL_NAME_COUNTRY_ID
        Public Const COL_NAME_COUNTRY_DESC As String = ZipDistrictDAL.COL_NAME_COUNTRY_DESC
        Public Const COL_NAME_DESCRIPTION As String = ZipDistrictDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = ZipDistrictDAL.COL_NAME_SHORT_DESC
        Public Const COL_NAME_ZIP_DISTRICT_ID As String = ZipDistrictDAL.COL_NAME_ZIP_DISTRICT_ID
#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region


#Region "Children Related"
    Public ReadOnly Property ZipCodeChildren As ZipDistrictDetailList
        Get
            Return New ZipDistrictDetailList(Me)
        End Get
    End Property

    Public Function GetZipCodeSelectionView() As ZipCodeSelectionView
        Dim t As DataTable = ZipCodeSelectionView.CreateTable
        Dim detail As ZipDistrictDetail
        For Each detail In ZipCodeChildren
            Dim row As DataRow = t.NewRow
            row(ZipCodeSelectionView.COL_NAME_DETAIL_ID) = detail.Id.ToByteArray
            row(ZipCodeSelectionView.COL_NAME_ZIP_CODE) = detail.ZipCode
            t.Rows.Add(row)
        Next
        Return New ZipCodeSelectionView(t)
    End Function

    Public Class ZipCodeSelectionView
        Inherits DataView
        Public Const COL_NAME_DETAIL_ID As String = "DetailId"
        Public Const COL_NAME_ZIP_CODE As String = "RiskTypeDescription"

        Public Sub New(Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_DETAIL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ZIP_CODE, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetChild(childId As Guid) As ZipDistrictDetail
        Return CType(ZipCodeChildren.GetChild(childId), ZipDistrictDetail)
    End Function

    Public Function GetNewChild() As ZipDistrictDetail
        Dim newZdDetail As ZipDistrictDetail = CType(ZipCodeChildren.GetNewChild, ZipDistrictDetail)
        newZdDetail.ZipDistrictId = Id
        Return newZdDetail
    End Function

    Public Sub AddZipCode(zipCode As String)
        ValidateZipCode(zipCode)
        Dim newzipDistrictDetail As ZipDistrictDetail = GetNewChild
        newzipDistrictDetail.ZipCode = zipCode
        newzipDistrictDetail.Save()
    End Sub

    Public Sub AddZipCodeRangeNew(startZipCode As String, endZipCode As String)
        Dim startZc, endZc As Integer
        Try
            startZc = Integer.Parse(startZipCode)
            endZc = Integer.Parse(endZipCode)
        Catch ex As Exception
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End Try

        If endZc - startZc >= ZIP_CODES_MAX_RANGE Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_MAX_RANGE_ERROR, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        ElseIf endZc - startZc < 0 Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_RANGE, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End If

        Dim zipCode As Integer
        Dim addedList As New ArrayList
        Try
            For zipCode = startZc To endZc
                Dim zc As String = zipCode.ToString
                If zc.Length < startZipCode.Length Then
                    zc = "0000000000000".Substring(0, startZipCode.Length - zc.Length) & zc
                End If
                '''ValidateZipCode(zc)
                Dim newzipDistrictDetail As ZipDistrictDetail = GetNewChild
                newzipDistrictDetail.ZipCode = zc
                '''newzipDistrictDetail.Save()
                addedList.Add(newzipDistrictDetail)
            Next
        Catch ex As Exception
            'rollback and thow an exception
            Dim bo As ZipDistrictDetail
            For Each bo In addedList
                bo.Delete()
                bo.Save()
            Next
            Throw ex
        End Try
    End Sub

    Public Sub AddZipCodeRange(startZipCode As String, endZipCode As String)
        Dim startZc, endZc As Integer
        Try
            startZc = Integer.Parse(startZipCode)
            endZc = Integer.Parse(endZipCode)
        Catch ex As Exception
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End Try

        If endZc - startZc >= ZIP_CODES_MAX_RANGE Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_MAX_RANGE_ERROR, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        ElseIf endZc - startZc < 0 Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_RANGE, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End If

        Dim zipCode As Integer
        Dim addedList As New ArrayList
        Try
            For zipCode = startZc To endZc
                Dim zc As String = zipCode.ToString
                If zc.Length < startZipCode.Length Then
                    zc = "0000000000000".Substring(0, startZipCode.Length - zc.Length) & zc
                End If
                ValidateZipCode(zc)
                Dim newzipDistrictDetail As ZipDistrictDetail = GetNewChild
                newzipDistrictDetail.ZipCode = zc
                newzipDistrictDetail.Save()
                addedList.Add(newzipDistrictDetail)
            Next
        Catch ex As Exception
            'rollback and thow an exception
            Dim bo As ZipDistrictDetail
            For Each bo In addedList
                bo.Delete()
                bo.Save()
            Next
            Throw ex
        End Try
    End Sub

    Public Sub DeleteZipCodes(zcIdList() As String)
        If Not zcIdList Is Nothing AndAlso zcIdList.Length > 0 Then
            Dim zipCodeId As String
            For Each zipCodeId In zcIdList
                Dim zipDistrictDetail As zipDistrictDetail = GetChild(New Guid(zipCodeId))
                zipDistrictDetail.Delete()
                zipDistrictDetail.Save()
            Next
        End If
    End Sub

    Private zipCodesInUseByOtherZipDistrictsDv As DataView
    Private ReadOnly Property ZipCodesInUseByOtherZipDistricts As DataView
        Get
            If zipCodesInUseByOtherZipDistrictsDv Is Nothing Then
                zipCodesInUseByOtherZipDistrictsDv = ZipDistrictDetail.GetZipCodesUsedInOtherZipDistricts(Id)
            End If
            Return zipCodesInUseByOtherZipDistrictsDv
        End Get
    End Property


    public Sub ValidateZipCode(zipCode As String)
        If Not ZipCodeChildren.Find(zipCode) Is Nothing Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_DUPLICATE_ZIP_CODE, GetType(ZipDistrictDetail), Nothing, "ZipCode", zipCode)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End If

        Dim pc As New PostalCodeValidator(CountryId, zipCode)

        Dim objPostalCodeFormatResult As PostalCodeFormatResult
        If zipCode <> "*" Then
            objPostalCodeFormatResult = pc.IsValid()
            If Not objPostalCodeFormatResult.IsValid Then
                Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
                Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
            Else
                Try
                    Dim intPostalCode As String = zipCode
                Catch ex As Exception
                    Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT_AND_INVALID_LOCATOR, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                End Try
            End If
        End If

    End Sub


#End Region

#Region "Extended Functionality: Batch Insert in DB"
    Public Function ZDAndDetail_Batch_Insert() As Integer
        Dim intZipCodeLowValue, intZipCodeHighValue, index As Integer

        Try
            intZipCodeLowValue = Integer.Parse(ZipCodeChildren.Table.Rows(0).Item(ZipDistrictDetailDAL.COL_NAME_ZIP_CODE))
            intZipCodeHighValue = Integer.Parse(ZipCodeChildren.Table.Rows(ZipCodeChildren.Table.Rows.Count - 1).Item(ZipDistrictDetailDAL.COL_NAME_ZIP_CODE))
        Catch ex As Exception
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End Try

        If ZipCodeChildren.Table.Rows.Count - 1 <> (intZipCodeHighValue - intZipCodeLowValue) Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_RANGE, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End If

        If intZipCodeHighValue - intZipCodeLowValue >= ZIP_CODES_MAX_RANGE Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_MAX_RANGE_ERROR, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        ElseIf intZipCodeHighValue - intZipCodeLowValue < 0 Then
            Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_RANGE, GetType(ZipDistrictDetail), Nothing, "ZipCode", Nothing)
            Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
        End If

        Try
            Dim dal As New ZipDistrictDAL
            Dim intResult As Integer = dal.ZDAndDetail_Batch_Insert(Id, CountryId, ShortDesc, Description, intZipCodeLowValue, intZipCodeHighValue)
            Dim err As ValidationError

            Select Case intResult
                Case PARENT_EXIST
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_ADDING_ZIP_DISTRICT, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case ZIP_FORMAT_NOT_VALID
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case CHILD_EXIST
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ZIP_DISTRICT_DUPLICATE_ZIP_CODE, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case INSERTING_CHILD_FAILD
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_ADDING_ZIP_DISTRICT_DETAL, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case COUNTRY_ID_NOT_FOUND
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case LOCATOR_LENGTH_NOT_FOUND
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_ZIP_LOCATOR_LENGTH_NOT_FOUND, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case NO_ERROR
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                    Return NO_ERROR
                Case Else
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_UNKNOWN_ERROR_IN_ZIP_DISTRICT_BATCH_INSERT, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
            End Select
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Function ZDAndDetail_Batch_Delete() As Integer

        Try
            Dim dal As New ZipDistrictDAL
            Dim intResult As Integer = dal.ZDAndDetail_Batch_Delete(Id)
            Dim err As ValidationError

            Select Case intResult
                Case DELETING_CHILD_FAILD
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_DELETING_ZIP_DISTRICT_DETAIL, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case DELETING_PARENT_FAILD
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_DELETING_ZIP_DISTRICT, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
                Case NO_ERROR
                    'Dim objId As Guid = Me.Id
                    'Me.Dataset = New Dataset
                    'Me.Row = Nothing
                    'Me.Load(objId)
                    Return NO_ERROR
                Case Else
                    err = New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_UNKNOWN_ERROR_IN_ZIP_DISTRICT_BATCH_INSERT, GetType(ZipDistrictDetail), Nothing, Nothing, Nothing)
                    Throw New BOValidationException(New ValidationError() {err}, "Zip District", UniqueId)
            End Select
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region
End Class



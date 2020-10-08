'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2007)  ********************

Public Class VSCModel
    Inherits BusinessObjectBase

#Region " Constants"

    Public Const COL_MAKE As String = "make"
    Public Const COL_MODEL As String = "model"
    Public Const COL_ENGINE_VERSION As String = "description"
    Public Const COL_YEAR As String = "model_year"
    Public Const COL_NEW_CLASS_CODE As String = "new_class_code"
    Public Const COL_USED_CLASS_CODE As String = "USED_CLASS_CODE"

    Public Const VAL_NEW As String = VSCModelDAL.VAL_NEW
    Public Const VAL_USED As String = VSCModelDAL.VAL_USED
    Public Const VAL_BOTH As String = "BOTH"
    Public Const COL_NAME_ACTIVE_NEW As String = VSCModelDAL.COL_NAME_ACTIVE_NEW
    Public Const COL_NAME_ACTIVE_USED As String = VSCModelDAL.COL_NAME_ACTIVE_USED

    Public Const VAL_YES As String = "Y"
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
    'Existing BO
    Public Sub New(manufacturer As String, description As String, year As Integer)
        MyBase.New()
        Dataset = New Dataset
        Load(manufacturer, description, year)
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
            Dim dal As New VSCModelDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New VSCModelDAL
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
    Protected Sub Load(manufacturer As String, description As String, year As Integer)
        Try
            Dim dal As New VSCModelDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(manufacturer, description, year, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                'dal.Load(Me.Dataset, manufacturer, description, year)
                Row = FindRow(manufacturer, description, year, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
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
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(VSCModelDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_MODEL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    'REQ-1142 changed the data type
    <ValueMandatory(""), ValidStringLength("", Max:=15), Valid_CarCode("")> _
    Public Property CarCode As String
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCModelDAL.COL_NAME_CAR_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_CAR_CODE, Value)
        End Set
    End Property
    'REQ-1142 
    <ValidStringLength("", Max:=10), Valid_External_Car_Code("")> _
    Public Property ExternalCarCode As String
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_EXTERNAL_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCModelDAL.COL_NAME_EXTERNAL_CAR_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_EXTERNAL_CAR_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCModelDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCModelDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=1111)> _
    Public Property ModelYear As Integer
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_MODEL_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCModelDAL.COL_NAME_MODEL_YEAR), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_MODEL_YEAR, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property NewClassCodeId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_NEW_CLASS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_NEW_CLASS_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_NEW_CLASS_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UsedClassCodeId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_USED_CLASS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_USED_CLASS_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_USED_CLASS_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ActiveNewId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_ACTIVE_NEW_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_ACTIVE_NEW_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_ACTIVE_NEW_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ActiveUsedId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_ACTIVE_USED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_ACTIVE_USED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_ACTIVE_USED_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CoverageLimitId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGgroupId As Guid
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCModelDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0)> _
    Public Property CoverageLimitCode As Integer
        Get
            CheckDeleted()
            If Row(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_CODE), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCModelDAL.COL_NAME_COVERAGE_LIMIT_CODE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCModelDAL
                dal.Update(Row)
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
    Public Sub Copy(original As VSCModel)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing VSC MODEL")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(make As String, _
                                   model As String, _
                                   trim As String, _
                                   year As String, _
                                   coverageSupport As String) As VSCModelSearchDV
        Try

            Dim dal As New VSCModelDAL
            Dim dv As VSCModelSearchDV = New VSCModelSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, _
                                                    make, model, trim, year).Tables(0))

            If Not coverageSupport Is Nothing AndAlso Not coverageSupport.Equals(String.Empty) Then
                If coverageSupport = VAL_NEW Then
                    dv.RowFilter = COL_NAME_ACTIVE_NEW & " ='" & VAL_YES & "'"
                ElseIf coverageSupport = VAL_USED Then
                    dv.RowFilter = COL_NAME_ACTIVE_USED & " ='" & VAL_YES & "'"
                End If
            End If

            Return dv

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getDistinctList(make As String, _
                                   model As String, _
                                   trim As String, _
                                   year As String, _
                                   requestedfield As String, _
                                   coverageSupport As String) As VSCModelSearchDV
        Try

            Dim dal As New VSCModelDAL
            Dim dv As VSCModelSearchDV = New VSCModelSearchDV(dal.LoadDistinctList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, _
                                                    make, model, trim, year, requestedfield).Tables(0))

            If Not coverageSupport Is Nothing AndAlso Not coverageSupport.Equals(String.Empty) Then
                If coverageSupport = VAL_NEW Then
                    dv.RowFilter = COL_NAME_ACTIVE_NEW & " ='" & VAL_YES & "'"
                ElseIf coverageSupport = VAL_USED Then
                    dv.RowFilter = COL_NAME_ACTIVE_USED & " ='" & VAL_YES & "'"
                End If
            End If

            Return dv

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(VSCModelDAL.COL_NAME_MODEL_ID) = id.ToByteArray
        row(VSCModelDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(VSCModelDAL.COL_NAME_CAR_CODE) = String.Empty
        row(VSCModelDAL.COL_NAME_MODEL) = String.Empty
        row(VSCModelDAL.COL_NAME_NEW_CLASS_CODE_ID) = Guid.Empty
        row(VSCModelDAL.COL_NAME_USED_CLASS_CODE_ID) = Guid.Empty
        row(VSCModelDAL.COL_NAME_ACTIVE_NEW_ID) = Guid.Empty
        row(VSCModelDAL.COL_NAME_ACTIVE_USED_ID) = Guid.Empty
        row(VSCModelDAL.COL_NAME_MANUFACTURER_ID) = Guid.Empty

        dt.Rows.Add(row)

        Return (dv)

    End Function

#Region "Web Services Methods"

    Public Function GetModels(DS As VSCGetModelsInputDs) As String
        Try

            Dim sMake As String = ""
            Dim sWSConsumer As String = ""
            Dim XMLData As String
            Dim dv As DataView

            If Not DS.VSCGetModelsInput.Count = 0 Then
                With DS.VSCGetModelsInput.Item(0)
                    sMake = .Make
                    'sWSConsumer = .wsConsumer
                End With
            End If

            Dim companyGroupID As Guid = Authentication.CompanyGroupId

            If Not WSUtility.IsGuid(sMake) Then
                ' if Make is not GUID, get GUID using Make and CompanyID
                Dim ManuGUID As System.Guid
                ManuGUID = WSUtility.GetManufacturerGUID(sMake, companyGroupID)
                If Not WSUtility.IsGuid(ManuGUID) Then 'no such manufacturer for this company group
                    Exit Function
                Else
                    dv = getDistinctList(GuidControl.GuidToHexString(ManuGUID), Nothing, Nothing, Nothing, "M.MODEL", Nothing)
                End If
            Else
                dv = getDistinctList(sMake, Nothing, Nothing, Nothing, "M.MODEL", Nothing)
            End If

            Dim dsRetMsg As New DataSet
            dsRetMsg.DataSetName = "VSCGetModelsResultset"
            dsRetMsg.Tables.Add(dv.Table.Copy)
            XMLData = XMLHelper.FromDatasetToXML(dsRetMsg)
            dsRetMsg.Dispose()

            dv.Dispose()
            DS.Dispose()

            Return XMLData

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Function GetVersions(DS As VSCGetVersionsInputDs) As String
        Try

            Dim sMake As String = ""
            Dim sModel As String = ""
            Dim sWSConsumer As String = ""
            Dim XMLData As String
            Dim dv As DataView

            If Not DS.VSCGetVersionsInput.Count = 0 Then
                With DS.VSCGetVersionsInput.Item(0)
                    sMake = .Make
                    sModel = .Model
                    'sWSConsumer = .wsConsumer
                End With
            End If

            Dim companyGroupID As Guid = Authentication.CompanyGroupId

            If Not WSUtility.IsGuid(sMake) Then
                ' if Make is not GUID, get GUID using Make and CompanyID
                Dim ManuGUID As System.Guid
                ManuGUID = WSUtility.GetManufacturerGUID(sMake, companyGroupID)
                If Not WSUtility.IsGuid(ManuGUID) Then 'no such manufacturer for this company group
                    Exit Function
                Else
                    dv = getList(GuidControl.GuidToHexString(ManuGUID), sModel, "", "", "")
                End If
            Else
                dv = getList(sMake, sModel, "", "", "")
            End If

            'remove unnecessary columns
            dv.Table.Columns.Remove("MAKE")
            dv.Table.Columns.Remove("MODEL")
            dv.Table.Columns.Remove("MODEL_ID")
            dv.Table.Columns.Remove("MODEL_YEAR")
            dv.Table.Columns.Remove("NEW_CLASS_CODE")
            dv.Table.Columns.Remove("USED_CLASS_CODE")
            dv.Table.Columns.Remove("ENGINE_MONTHS_KM_MI")
            dv.Table.Columns.Remove("ACTIVE_NEW")
            dv.Table.Columns.Remove("ACTIVE_USED")

            Dim dsRetMsg As New DataSet
            dsRetMsg.DataSetName = "VSCGetVersionsResultset"
            dsRetMsg.Tables.Add(dv.Table.Copy)
            XMLData = XMLHelper.FromDatasetToXML(dsRetMsg)
            dsRetMsg.Dispose()

            dv.Dispose()
            DS.Dispose()

            Return XMLData

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Function GetYears(DS As VSCGetYearsInputDs) As String
        Try

            Dim sMake As String = ""
            Dim sModel As String = ""
            Dim sVersion As String = ""
            Dim sWSConsumer As String = ""
            Dim XMLData As String
            Dim dv As DataView

            If Not DS.VSCGetYearsInput.Count = 0 Then
                With DS.VSCGetYearsInput.Item(0)
                    sMake = .Make
                    sModel = .Model
                    sVersion = .Version
                    'sWSConsumer = .wsConsumer
                End With
            End If

            Dim companyGroupID As Guid = Authentication.CompanyGroupId

            If Not WSUtility.IsGuid(sMake) Then
                ' if Make is not GUID, get GUID using Make and CompanyID
                Dim ManuGUID As System.Guid
                ManuGUID = WSUtility.GetManufacturerGUID(sMake, companyGroupID)
                If Not WSUtility.IsGuid(ManuGUID) Then 'no such manufacturer for this company group
                    Exit Function
                Else
                    dv = getList(GuidControl.GuidToHexString(ManuGUID), sModel, sVersion, "", "")
                End If
            Else
                dv = getList(sMake, sModel, sVersion, "", "")
            End If

            'remove unnecessary columns
            dv.Table.Columns.Remove("MAKE")
            dv.Table.Columns.Remove("MODEL")
            dv.Table.Columns.Remove("MODEL_ID")
            dv.Table.Columns.Remove("DESCRIPTION")
            dv.Table.Columns.Remove("NEW_CLASS_CODE")
            dv.Table.Columns.Remove("USED_CLASS_CODE")
            dv.Table.Columns.Remove("ENGINE_MONTHS_KM_MI")
            dv.Table.Columns.Remove("ACTIVE_NEW")
            dv.Table.Columns.Remove("ACTIVE_USED")

            Dim dsRetMsg As New DataSet
            dsRetMsg.DataSetName = "VSCGetYearsResultset"
            dsRetMsg.Tables.Add(dv.Table.Copy)
            XMLData = XMLHelper.FromDatasetToXML(dsRetMsg)
            dsRetMsg.Dispose()

            dv.Dispose()
            DS.Dispose()

            Return XMLData

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function GetVSCModels(companyGroupId As Guid, make As String) As DataSet
        Try
            Dim dal As New VSCModelDAL
            Return dal.LoadVSCModels(companyGroupId, make)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetVSCEngineVersions(companyGroupId As Guid, make As String, model As String) As DataSet
        Try
            Dim dal As New VSCModelDAL
            Return dal.LoadVSCEngineVersions(companyGroupId, make, model)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetVSCYears(companyGroupId As Guid, make As String, model As String, engineVersion As String) As DataSet
        Try
            Dim dal As New VSCModelDAL
            Return dal.LoadVSCYears(companyGroupId, make, model, engineVersion)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ValidateExternalCarCode(companyGroupId As Guid, externalCarCode As String, ManufacturerId As Guid, model As String, engineVersion As String) As Boolean
        Dim isValid As Boolean = True
        Dim dal As VSCModelDAL
        Dim ds As DataSet
        Dim count As Integer
        dal = New VSCModelDAL()
        ds = dal.LoadExternalCardCode(companyGroupId, externalCarCode, ManufacturerId, model, engineVersion)
        If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            count = CType(ds.Tables(0).Rows(0)(0).ToString, Integer)
            If count > 0 Then
                isValid = False
            End If
        End If
        Return isValid
    End Function

#End Region

#Region "SearchDV"
    Public Class VSCModelSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_MODEL_ID As String = VSCModelDAL.COL_NAME_MODEL_ID
        Public Const COL_NAME_MAKE As String = VSCModelDAL.COL_NAME_MAKE
        Public Const COL_NAME_DESCRIPTION As String = VSCModelDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_MODEL_YEAR As String = VSCModelDAL.COL_NAME_MODEL_YEAR
        Public Const COL_NAME_NEW_CLASS_CODE As String = VSCModelDAL.COL_NAME_NEW_CLASS_CODE
        Public Const COL_NAME_USED_CLASS_CODE As String = VSCModelDAL.COL_NAME_USED_CLASS_CODE
        Public Const COL_NAME_ACTIVE_NEW As String = VSCModelDAL.COL_NAME_ACTIVE_NEW
        Public Const COL_NAME_ACTIVE_USED As String = VSCModelDAL.COL_NAME_ACTIVE_USED
        Public Const COL_NAME_ENGINE_MONTHS_KM_MI As String = VSCModelDAL.COL_NAME_ENGINE_MONTHS_KM_MI
        Public Const COL_NAME_COMPANY_GROUP_ID As String = VSCModelDAL.COL_NAME_COMPANY_GROUP_ID
        Public Const COL_NAME_COVERAGE_LIMIT_CODE As String = VSCModelDAL.COL_NAME_COVERAGE_LIMIT_CODE

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)

        End Sub

    End Class


#End Region


#End Region

#Region "Custom Validation"
    ''' <summary>
    ''' Validation to check whether carcode start with externalcar code. If not then return error 
    ''' Value of car code and external car code are not in synch.
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Valid_CarCode
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_CAR_CODE_ERROR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As VSCModel = CType(objectToValidate, VSCModel)
            If obj.CarCode Is Nothing Then Return True
            Dim strCarCode As String = obj.CarCode
            If obj.ExternalCarCode Is Nothing Then Return True
            Dim strExternalCarCode As String = obj.ExternalCarCode
            If Not strCarCode.StartsWith(strExternalCarCode) Then
                Return False
            End If
            Return True
        End Function
    End Class

    ''' <summary>
    ''' If provided then it can only be allowed to be used for the same Make, Model and Engine version combination
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Valid_External_Car_Code
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_EXTERNAL_CAR_CODE_ERROR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As VSCModel = CType(objectToValidate, VSCModel)
            If obj.ExternalCarCode Is Nothing Then Return True
            Dim strExternalCarCode As String = obj.ExternalCarCode
            If ValidateExternalCarCode(obj.CompanyGgroupId, obj.ExternalCarCode, obj.ManufacturerId, obj.Model, obj.Description) Then
                Return True
            End If
            Return False
        End Function
    End Class
#End Region

End Class




Public Class CoverageType
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CoverageTypeDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CoverageTypeDAL
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
    Private Sub Initialize()

    End Sub
#End Region
    Private defVal As String = Guid.Empty.ToString

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(CoverageTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageTypeDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageTypeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property MaintainableByUser As String
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_MAINTAINABLE_BY_USER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageTypeDAL.COL_NAME_MAINTAINABLE_BY_USER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_MAINTAINABLE_BY_USER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property DisplayToUser As String
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_DISPLAY_TO_USER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageTypeDAL.COL_NAME_DISPLAY_TO_USER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_DISPLAY_TO_USER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ListId As Guid
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageTypeDAL.COL_NAME_LIST_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DictItemId As Guid
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageTypeDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property ActiveFlag As String
        Get
            CheckDeleted()
            If Row(CoverageTypeDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageTypeDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageTypeDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property

    Public ReadOnly Property AssociatedCoveragesLoss As CoverageLoss.CovLossList
        Get
            Return New CoverageLoss.CovLossList(Me)
        End Get
    End Property

    Public ReadOnly Property AssociatedCoveragesLoss(ByVal coverageLossId As Guid) As CoverageLoss.CovLossList
        Get
            Return New CoverageLoss.CovLossList(Me, coverageLossId)
        End Get
    End Property

    <CheckDefaultValue("Selected Default Value")> _
    Public Property AvailableDefaultValue As String
        Get
            Return defVal
        End Get
        Set
            defVal = value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageTypeDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDefaultValue
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, " Value Is Required : " + fieldDisplayName)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageType = CType(objectToValidate, CoverageType)
            If (obj.AvailableDefaultValue = Guid.Empty.ToString) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region

#Region "Children Related"
    Public Shared Function GetAvailableCausesOfLoss(ByVal coverageTypeId As Guid) As DataView
        Dim dal As New CoverageLossDAL
        Dim ds As DataSet
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        ds = dal.LoadAvailableCausesOfLoss(oCompanyGroupId, oLanguageId, coverageTypeId)
        Return ds.Tables(CoverageLossDAL.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetSelectedCausesOfLoss(ByVal coverageTypeId As Guid) As DataView
        Dim dal As New CoverageLossDAL
        Dim ds As DataSet
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        ds = dal.LoadSelectedCausesOfLoss(oCompanyGroupId, coverageTypeId, oLanguageId)
        Return ds.Tables(CoverageLossDAL.TABLE_NAME).DefaultView
    End Function


    Public Sub AttachCoverageLoss(ByVal selectedCoverageLossGuidStrCollection As ArrayList)
        Dim ctCovLossIdStr As String
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        For Each ctCovLossIdStr In selectedCoverageLossGuidStrCollection
            Dim ctCovLoss As CoverageLoss = New CoverageLoss(New Guid(ctCovLossIdStr), Id)
            If ctCovLoss.Row Is Nothing Then
                ctCovLoss = AssociatedCoveragesLoss.GetNewChild
                ctCovLoss.CauseOfLossId = New Guid(ctCovLossIdStr)
                ctCovLoss.CoverageTypeId = Id
                ctCovLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                ctCovLoss.Active = Nothing
            Else
                ctCovLoss = AssociatedCoveragesLoss(ctCovLoss.Id).GetChild(ctCovLoss.Id)
                ctCovLoss.Active = Nothing
            End If

            ctCovLoss.Save()

        Next
    End Sub

    Public Sub DetachCoverageLoss(ByVal selectedCoverageLossGuidStrCollection As ArrayList)
        Dim ctCovLossIdStr As String
        For Each ctCovLossIdStr In selectedCoverageLossGuidStrCollection
            Dim ctCovLoss As CoverageLoss = AssociatedCoveragesLoss.FindById(New Guid(ctCovLossIdStr))
            ctCovLoss.Active = "N"
            'ctCovLoss.Delete()
            ctCovLoss.Save()
        Next
    End Sub
    'Function GetNotAvailableCoverageLoss(ByVal causeOfLossId As Guid) As CoverageLoss
    '    Dim dal As New CoverageLossDAL
    '    Dim ds As DataSet
    '    Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
    '    Dim CovLoss As CoverageLoss = New CoverageLoss(oCompanyGroupId, causeOfLossId)

    '    Return CovLoss
    'End Function

    Function CreateDataSource(ByVal selectedCoverageLossGuidStrCollection As ArrayList) As ICollection

        ' Create a table to store data for the DropDownList control.
        Dim ctCovLossIdStr As String
        Dim dt As DataTable = New DataTable

        ' Define the columns of the table.
        dt.Columns.Add(New DataColumn(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID, GetType(String)))
        dt.Columns.Add(New DataColumn("description", GetType(String)))

        ' Populate the table with sample values.
        For Each ctCovLossIdStr In selectedCoverageLossGuidStrCollection
            Dim ctCovLoss As CoverageLoss = AssociatedCoveragesLoss.FindById(New Guid(ctCovLossIdStr))
            'dt.Rows.Add(CreateRow(ctCovLossIdStr, , dt))
        Next

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        Dim dv As DataView = New DataView(dt)
        Return dv

    End Function

    Function CreateRow(ByVal Text As String, ByVal Value As String, ByVal dt As DataTable) As DataRow

        ' Create a DataRow using the DataTable defined in the 
        ' CreateDataSource method.
        Dim dr As DataRow = dt.NewRow()

        ' This DataRow contains the ColorTextField and ColorValueField 
        ' fields, as defined in the CreateDataSource method. Set the 
        ' fields with the appropriate value. Remember that column 0 
        ' is defined as ColorTextField, and column 1 is defined as 
        ' ColorValueField.
        dr(0) = Text
        dr(1) = Value

        Return dr

    End Function


#End Region

#Region "DataView Retrieveing Methods"

    Public ReadOnly Property GetCoverageTypeDescription(ByVal coveragetypeID As Guid) As String
        Get
            'Dim moCoverage As New CertItemCoverage
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim coverageDV As DataView
            Dim coverageTypeDesc As String

            coverageDV = LookupListNew.GetCoverageTypeLookupList(langId)
            coverageTypeDesc = LookupListNew.GetDescriptionFromId(coverageDV, coveragetypeID)

            Return coverageTypeDesc
        End Get
    End Property
#End Region
End Class

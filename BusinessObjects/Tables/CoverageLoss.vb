'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2006)  ********************

Public Class CoverageLoss
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    'New BO Load not avialable
    Public Sub New(causeOfLossId As Guid, coverageTypeId As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(causeOfLossId, coverageTypeId)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CoverageLossDAL
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
            Dim dal As New CoverageLossDAL
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

    Protected Sub Load(causeOfLossId As Guid, coverageTypeId As Guid)
        Try
            Dim dal As New CoverageLossDAL
            Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(causeOfLossId, dal.COL_NAME_CAUSE_OF_LOSS_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, oCompanyGroupId, causeOfLossId, coverageTypeId)
                Row = FindRow(causeOfLossId, dal.COL_NAME_CAUSE_OF_LOSS_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            'If Me.Row Is Nothing Then
            '    Throw New DataNotFoundException
            'End If
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

#Region "CONSTANTS"
    Private Const DEFAULT_FLAG As String = "Y"
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(CoverageLossDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COVERAGE_LOSS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CauseOfLossId As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property

    Public Property DefaultFlag As String
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_DEFAULT_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageLossDAL.COL_NAME_DEFAULT_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageLossDAL.COL_NAME_DEFAULT_FLAG, Value)
        End Set
    End Property

    Public Property Active As String
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageLossDAL.COL_NAME_ACTIVE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageLossDAL.COL_NAME_ACTIVE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageLossDAL
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

    Public Function getCoverageLossForSpecialService(splsvcCode As String, DealerCode As String) As DataView
        Dim dal As New CoverageLossDAL
        Try
            Dim dealerid As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALERS, DealerCode)
            Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim ds As DataSet = dal.getCoverageLossforSpecialService(splsvcCode, dealerid, oCompanyGroupId)
            If ds.Tables(CoverageLossDAL.TABLE_NAME).Rows.Count < 1 Then
                Return Nothing
            End If
            Return ds.Tables(CoverageLossDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(coverageTypeId As Guid) As SearchDV
        Try
            Dim dal As New CoverageLossDAL
            'Dim oCompany As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New SearchDV(dal.LoadList(oCompanyGroupId, coverageTypeId, oLanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Shared Function GetCovLossList(parent As CoverageType) As DataTable
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CovLossList)) Then
                Dim dal As New CoverageLossDAL
                dal.LoadList(parent.Id, oCompanyGroupId, parent.Dataset)
                parent.AddChildrenCollection(GetType(CovLossList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(CoverageLossDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Private Shared Function GetCovLoss(parent As CoverageType, coverageLossId As Guid) As DataTable
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CovLossList)) Then
                Dim dal As New CoverageLossDAL
                dal.Load(parent.Dataset, coverageLossId)
                parent.AddChildrenCollection(GetType(CovLossList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(CoverageLossDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCauseOfLossInUse(causeOfLossIds As ArrayList) As Boolean
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim dal As New CoverageLossDAL

        Dim ds As Dataset = dal.GetCauseOfLossInUse(causeOfLossIds, compIds)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Shared Function LoadSelectedCovLossFromCovandCauseOfLoss(CauseOfLossId As Guid, coverageTypeId As Guid) As DataSet
        Dim grpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim dal As New CoverageLossDAL

        Dim ds As DataSet = dal.LoadSelectedCovLossFromCovandCauseOfLoss(CauseOfLossId, coverageTypeId, grpId)

        Return ds
    End Function

    Public Shared Function LoadCauseOfLossByCov(coverageTypeId As Guid) As DataSet
        Dim grpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim language_Id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim dal As New CoverageLossDAL

        Dim ds As DataSet = dal.LoadCauseOfLossByCov(coverageTypeId, grpId, language_Id)

        Return ds
    End Function

    Public Shared Function LoadDefaultCauseOfLossByCov(coverageTypeId As Guid) As DataSet
        Dim grpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim language_Id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim dal As New CoverageLossDAL

        Dim ds As DataSet = dal.LoadDefaultCauseOfLossByCov(coverageTypeId, grpId, language_Id)

        Return ds
    End Function
#End Region

#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = "description"
        Public Const COL_NAME_CODE As String = "code"
        Public Const COL_NAME_COVERAGE_LOSS_ID As String = CoverageLossDAL.COL_NAME_COVERAGE_LOSS_ID
        Public Const COL_NAME_COMPANY_GROUP_ID As String = CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID
        Public Const COL_NAME_COVERAGE_TYPE_ID As String = CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID
        Public Const COL_NAME_CAUSE_OF_LOSS_ID As String = CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID
        Public Const COL_NAME_DEFAULT_FLAG As String = CoverageLossDAL.COL_NAME_DEFAULT_FLAG
        Public Const COL_NAME_ACTIVE As String = CoverageLossDAL.COL_NAME_ACTIVE

#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CoverageLossId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_LOSS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyGroupGd(row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMPANY_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CoverageTypeId(row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CauseOfLossId(row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFlag(row As DataRow) As String
            Get
                Return CType(row(COL_NAME_DEFAULT_FLAG), String)
            End Get
        End Property

        Public Shared ReadOnly Property Active(row As DataRow) As String
            Get
                Return CType(row(COL_NAME_ACTIVE), String)
            End Get
        End Property

    End Class

#End Region

#Region "List Methods"
    Public Class CovLossList
        Inherits BusinessObjectListBase
        Public Sub New(parent As CoverageType)
            MyBase.New(GetCovLossList(parent), GetType(CoverageLoss), parent)
        End Sub
        Public Sub New(parent As CoverageType, coverageLossId As Guid)
            MyBase.New(GetCovLoss(parent, coverageLossId), GetType(CoverageLoss), parent)
        End Sub
        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(CauseOfLossId As Guid) As CoverageLoss
            Dim bo As CoverageLoss
            For Each bo In Me
                If bo.CauseOfLossId.Equals(CauseOfLossId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function FindDefault() As CoverageLoss
            Dim bo As CoverageLoss
            For Each bo In Me
                If bo.DefaultFlag = DEFAULT_FLAG Then
                    Return bo
                End If
            Next
            Return Nothing
        End Function
    End Class

#End Region
End Class




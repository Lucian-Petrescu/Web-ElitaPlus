'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2006)  ********************

Public Class CoverageLoss
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

    'New BO Load not avialable
    Public Sub New(ByVal causeOfLossId As Guid, ByVal coverageTypeId As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(causeOfLossId, coverageTypeId)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CoverageLossDAL
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
            Dim dal As New CoverageLossDAL
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

    Protected Sub Load(ByVal causeOfLossId As Guid, ByVal coverageTypeId As Guid)
        Try
            Dim dal As New CoverageLossDAL
            Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(causeOfLossId, dal.COL_NAME_CAUSE_OF_LOSS_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, oCompanyGroupId, causeOfLossId, coverageTypeId)
                Me.Row = Me.FindRow(causeOfLossId, dal.COL_NAME_CAUSE_OF_LOSS_ID, Me.Dataset.Tables(dal.TABLE_NAME))
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

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CoverageLossDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COVERAGE_LOSS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CauseOfLossId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property

    Public Property DefaultFlag() As String
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_DEFAULT_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageLossDAL.COL_NAME_DEFAULT_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageLossDAL.COL_NAME_DEFAULT_FLAG, Value)
        End Set
    End Property

    Public Property Active() As String
        Get
            CheckDeleted()
            If Row(CoverageLossDAL.COL_NAME_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageLossDAL.COL_NAME_ACTIVE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageLossDAL.COL_NAME_ACTIVE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageLossDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function getCoverageLossForSpecialService(ByVal splsvcCode As String, ByVal DealerCode As String) As DataView
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

    Public Shared Function getList(ByVal coverageTypeId As Guid) As SearchDV
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

    Private Shared Function GetCovLossList(ByVal parent As CoverageType) As DataTable
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


    Private Shared Function GetCovLoss(ByVal parent As CoverageType, ByVal coverageLossId As Guid) As DataTable
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

    Public Function LoadCauseOfLossInUse(ByVal causeOfLossIds As ArrayList) As Boolean
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim dal As New CoverageLossDAL

        Dim ds As Dataset = dal.GetCauseOfLossInUse(causeOfLossIds, compIds)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        Return False

    End Function

    Public Shared Function LoadSelectedCovLossFromCovandCauseOfLoss(ByVal CauseOfLossId As Guid, ByVal coverageTypeId As Guid) As DataSet
        Dim grpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim dal As New CoverageLossDAL

        Dim ds As DataSet = dal.LoadSelectedCovLossFromCovandCauseOfLoss(CauseOfLossId, coverageTypeId, grpId)

        Return ds
    End Function

    Public Shared Function LoadCauseOfLossByCov(ByVal coverageTypeId As Guid) As DataSet
        Dim grpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim language_Id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim dal As New CoverageLossDAL

        Dim ds As DataSet = dal.LoadCauseOfLossByCov(coverageTypeId, grpId, language_Id)

        Return ds
    End Function

    Public Shared Function LoadDefaultCauseOfLossByCov(ByVal coverageTypeId As Guid) As DataSet
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CoverageLossId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_LOSS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyGroupGd(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMPANY_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CoverageTypeId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CauseOfLossId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFlag(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_DEFAULT_FLAG), String)
            End Get
        End Property

        Public Shared ReadOnly Property Active(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_ACTIVE), String)
            End Get
        End Property

    End Class

#End Region

#Region "List Methods"
    Public Class CovLossList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As CoverageType)
            MyBase.New(GetCovLossList(parent), GetType(CoverageLoss), parent)
        End Sub
        Public Sub New(ByVal parent As CoverageType, ByVal coverageLossId As Guid)
            MyBase.New(GetCovLoss(parent, coverageLossId), GetType(CoverageLoss), parent)
        End Sub
        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal CauseOfLossId As Guid) As CoverageLoss
            Dim bo As CoverageLoss
            For Each bo In Me
                If bo.CauseOfLossId.Equals(CauseOfLossId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function FindDefault() As CoverageLoss
            Dim bo As CoverageLoss
            For Each bo In Me
                If bo.DefaultFlag = Codes.YESNO_Y Then
                    Return bo
                End If
            Next
            Return Nothing
        End Function
    End Class

#End Region
End Class




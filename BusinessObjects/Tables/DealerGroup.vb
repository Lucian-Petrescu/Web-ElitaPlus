'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/30/2004)  ********************

Public Class DealerGroup
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

    Protected Sub Load()
        Dim dal As New DealerGroupDAL
        If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
            dal.LoadSchema(Dataset)
        End If
        Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
        Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
        Row = newRow
        SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Initialize()
    End Sub

    Protected Sub Load(ByVal id As Guid)
        'This code was added manually. Begin
        If _isDSCreator Then
            If Not Row Is Nothing Then
                Dataset.Tables(DealerGroupDAL.TABLE_NAME).Rows.Remove(Row)
            End If
        End If
        'This code was added Manually. End
        Row = Nothing
        Dim dal As New DealerGroupDAL
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
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(DealerGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerGroupDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        ''The Set is required for NUnit testing purposes (to test for Duplication)
        'Set(ByVal Value As Guid)
        '    CheckDeleted()
        '    Me.SetValue(DealerGroupDAL.COL_NAME_DEALER_GROUP_ID, Value)
        'End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(DealerGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DealerGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(DealerGroupDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DealerGroupDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
        Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If row(DealerGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AcctingByGroupId As Guid
        Get
            CheckDeleted()
            If Row(DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_ID, Value)
        End Set
    End Property

    Public Property UseClientCodeYNId As Guid
        Get
            CheckDeleted()
            If Row(DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YNID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YNID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YNID, Value)
        End Set
    End Property

    Public Property AutoRejErrTypeId As Guid
        Get
            CheckDeleted()
            If Row(DealerGroupDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerGroupDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID, Value)
        End Set
    End Property

    Public Property BankInfoId As Guid
        Get
            CheckDeleted()
            If Row(DealerGroupDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerGroupDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerGroupDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerGroupDAL
                dal.Update(Row)
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

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        'default value for accting by group
        AcctingByGroupId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal descriptionMask As String, ByVal codeMask As String) As DataView
        Try
            Dim dal As New DealerGroupDAL
            Dim ds As DataSet
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            ds = dal.LoadList(descriptionMask, codeMask, compGroupId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(DealerGroupDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As DealerGroup) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(DealerGroupDAL.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(DealerGroupDAL.COL_NAME_CODE) = bo.Code 'String.Empty
            row(DealerGroupDAL.COL_NAME_DEALER_GROUP_ID) = bo.Id.ToByteArray
            row(DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_ID) = bo.AcctingByGroupId.ToByteArray
            row(DealerGroupDAL.COL_NAME_BANK_INFO_ID) = bo.BankInfoId.ToByteArray
            row(DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_DESC) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, bo.AcctingByGroupId)
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

    Public Shared Function CheckAllDealerObligor(ByVal DealerGrpId As Guid) As Boolean
        Try
            Dim dal As New DealerGroupDAL, dv As DataView, oNoOfRecords As Integer
            Dim blnCheckAllDealerObligor As Boolean = False
            dv = New DataView(dal.CheckAllDealerObligor(DealerGrpId).Tables(0))

            If dv.Count > 1 Then
                blnCheckAllDealerObligor = False
            Else
                blnCheckAllDealerObligor = True
            End If

            Return blnCheckAllDealerObligor
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetUseClientDealerCodeYN(ByVal DealerGrpId As Guid) As String
        Try
            Dim dal As New DealerGroupDAL, dv As DataView
            dv = New DataView(dal.GetUseClientDealerCodeYN(DealerGrpId).Tables(0))

            Dim yesnoVal As String = CType(dv(0)(DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YN), String)
            Return yesnoVal
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"
    Public Class DealerGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DEALER_GROUP_ID As String = DealerGroupDAL.COL_NAME_DEALER_GROUP_ID
        Public Const COL_CODE As String = DealerGroupDAL.COL_NAME_CODE
        Public Const COL_DESCRIPTION As String = DealerGroupDAL.COL_NAME_DESCRIPTION
        Public Const COL_BANK_INFO_ID As String = DealerGroupDAL.COL_NAME_BANK_INFO_ID
        Public Const COL_ACCTING_BY_GROUP_ID As String = DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_ID
        Public Const COL_ACCTING_BY_GROUP_DESC As String = DealerGroupDAL.COL_NAME_ACCTING_BY_GROUP_DESC
        Public Const COL_USE_CLIENT_CODE_YNID As String = DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YNID
        Public Const COL_USE_CLIENT_CODE_YNDESC As String = DealerGroupDAL.COL_NAME_USE_CLIENT_CODE_YNDESC
        Public Const COL_AUTO_REJ_ERR_TYPE_ID As String = DealerGroupDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID
        Public Const COL_AUTO_REJ_ERR_TYPE_DESC As String = DealerGroupDAL.COL_NAME_AUTO_REJ_ERR_TYPE_DESC
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



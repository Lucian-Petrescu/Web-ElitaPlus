'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2015)  ********************

Public Class AfaAcctAmtSrc
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
            Dim dal As New AfaAcctAmtSrcDAL
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
            Dim dal As New AfaAcctAmtSrcDAL
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


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AfaAcctAmtSrcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcDAL.COL_NAME_ACCT_AMT_SRC_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctAmtSrcFieldTypeId() As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcDAL.COL_NAME_ACCT_AMT_SRC_FIELD_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcDAL.COL_NAME_ACCT_AMT_SRC_FIELD_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_ACCT_AMT_SRC_FIELD_TYPE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property EntityByRegion() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property EntityByRegionCoverageType() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION_COVERAGE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION_COVERAGE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            If Not (Value = String.Empty AndAlso Row(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION_COVERAGE_TYPE) Is DBNull.Value) Then
                'don't overwrite the null value and trigger the object dirty
                Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_ENTITY_BY_REGION_COVERAGE_TYPE, Value)
            End If
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property ReconcilWithInvoice() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcDAL.COL_NAME_RECONCIL_WITH_INVOICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcDAL.COL_NAME_RECONCIL_WITH_INVOICE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_RECONCIL_WITH_INVOICE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1), CheckDuplicateCLIPFormula("")> _
    Public Property UseFormulaForClip() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcDAL.COL_NAME_USE_FORMULA_FOR_CLIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcDAL.COL_NAME_USE_FORMULA_FOR_CLIP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcDAL.COL_NAME_USE_FORMULA_FOR_CLIP, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaAcctAmtSrcDAL
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

    Private Function IsDuplicateCLIPFormula() As Boolean
        Dim dal As New AfaAcctAmtSrcDAL
        Dim ds As DataSet
        ds = dal.LoadDuplicateClipFormula(Me.DealerId, Me.AcctAmtSrcFieldTypeId)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Sub getList(ByVal dealerID As Guid, ByRef dvMapped As DataView, ByRef dvNotMapped As DataView)
        Dim dal As New AfaAcctAmtSrcDAL
        Dim ds As DataSet = dal.LoadList(dealerID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        dvMapped = ds.Tables(AfaAcctAmtSrcDAL.TABLE_NAME_LIST_MAPPED).DefaultView
        dvNotMapped = ds.Tables(AfaAcctAmtSrcDAL.TABLE_NAME_LIST_NOT_MAPPED).DefaultView
    End Sub
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateCLIPFormula
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_CLIP_FORNULA As String = "DUPLICATE_CLIP_FORMULA"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_CLIP_FORNULA)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AfaAcctAmtSrc = CType(objectToValidate, AfaAcctAmtSrc)

            If (obj.UseFormulaForClip = "Y" AndAlso obj.IsDuplicateCLIPFormula()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region
End Class
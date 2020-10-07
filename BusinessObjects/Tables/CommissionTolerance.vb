'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/12/2007)  ********************

Public Class CommissionTolerance
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const ONE_ENTRY_PER_MARKUP_ONLY As String = "ONE_ENTRY_PER_MARKUP_ONLY"
#End Region

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
            Dim dal As New CommissionToleranceDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CommissionToleranceDAL
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

    Protected Sub Load(ByVal id As Guid, ByVal commMarkup As Decimal)
        Try
            Dim dal As New CommissionToleranceDAL
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


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CommissionToleranceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionToleranceDAL.COL_NAME_COMMISSION_TOLERANCE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CommissionPeriodId As Guid
        Get
            CheckDeleted()
            If row(CommissionToleranceDAL.COL_NAME_COMMISSION_PERIOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionToleranceDAL.COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionToleranceDAL.COL_NAME_COMMISSION_PERIOD_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0), ValidMarkup("")> _
    Public Property AllowedMarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionToleranceDAL.COL_NAME_ALLOWED_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionToleranceDAL.COL_NAME_ALLOWED_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionToleranceDAL.COL_NAME_ALLOWED_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tolerance As DecimalType
        Get
            CheckDeleted()
            If row(CommissionToleranceDAL.COL_NAME_TOLERANCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CommissionToleranceDAL.COL_NAME_TOLERANCE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionToleranceDAL.COL_NAME_TOLERANCE, Value)
        End Set
    End Property

    Public ReadOnly Property AssociatedAssocComm As AssociateCommissions.AssocCommList
        Get
            Return New AssociateCommissions.AssocCommList(Me, Id)
        End Get
    End Property

    Public Sub AttachAsscComm(ByVal familyDS As DataSet, ByVal NewObject As AssociateCommissions)

        Dim newBO As AssociateCommissions = New AssociateCommissions(familyDS)

        If Not newBO Is Nothing Then
            newBO.CommissionToleranceId = Id
            newBO.CommissionPercent = NewObject.CommissionPercent
            newBO.MarkupPercent = NewObject.MarkupPercent
            newBO.Position = NewObject.Position
            'newBO.Save()
        End If

    End Sub

#End Region

#Region "Public Members"
    Public Sub Copy(ByVal original As CommissionTolerance, ByVal familyDS As DataSet)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Dealer")
        End If
        'Copy myself
        CopyFrom(original)

        'copy the children 
        Dim ocount As Integer = 0

        Dim oAsscComm As AssociateCommissions
        For Each oAsscComm In original.AssociatedAssocComm
            If original.Id.Equals(oAsscComm.CommissionToleranceId) Then
                Dim newAsscComm As New AssociateCommissions
                newAsscComm.CopyFrom(oAsscComm)
                AttachAsscComm(familyDS, newAsscComm)
                ocount += 1
                If ocount = 5 Then Exit For
            End If
        Next

    End Sub
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionToleranceDAL
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

#Region "DataView Retrieveing Methods"


    'Public Shared Function getList(ByVal periodId As Guid) As SearchDV
    '    Try
    '        Dim dal As New CommissionToleranceDAL

    '        Return New SearchDV(dal.LoadList(periodId).Tables(0))
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function


    Public Shared Function getList(ByVal periodId As Guid) As DataView
        Try
            Dim dal As New CommissionToleranceDAL

            Return New DataView(dal.LoadList(periodId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getList(ByVal periodId As Guid, ByVal AllowedMarkup As DecimalType) As DataView
        Try
            Dim dal As New CommissionToleranceDAL

            Return New DataView(dal.LoadList(periodId, AllowedMarkup).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Shared Function GetToleranceList(ByVal parent As CommissionPeriod) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ToleranceList)) Then
                Dim dal As New CommissionToleranceDAL
                dal.LoadList(parent.Id, parent.Dataset)
                parent.AddChildrenCollection(GetType(ToleranceList))
            End If
            Return parent.Dataset.Tables(CommissionToleranceDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_COMMISSION_TOLERANCE_ID As String = CommissionToleranceDAL.COL_NAME_COMMISSION_TOLERANCE_ID
        Public Const COL_NAME_COMMISSION_PERIOD_ID As String = CommissionToleranceDAL.COL_NAME_COMMISSION_PERIOD_ID
        Public Const COL_NAME_ALLOWED_MARKUP_PCT As String = CommissionToleranceDAL.COL_NAME_ALLOWED_MARKUP_PCT
        Public Const COL_NAME_TOLERANCE As String = CommissionToleranceDAL.COL_NAME_TOLERANCE
        Public Const COL_NAME_DEALER_MARKUP_PCT As String = CommissionToleranceDAL.COL_NAME_DEALER_MARKUP_PCT
        Public Const COL_NAME_DEALER_COMM_PCT As String = CommissionToleranceDAL.COL_NAME_DEALER_COMM_PCT

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CommissionPeriodId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_TOLERANCE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CommissionPeriodId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property AllowedMarkupPct(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_ALLOWED_MARKUP_PCT), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property Tolerance(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_TOLERANCE), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property DealerMarkupPct(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_DEALER_MARKUP_PCT), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property DealerCommPct(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_DEALER_COMM_PCT), LongType)
            End Get
        End Property

    End Class

#End Region
#Region "List Methods"
    Public Class ToleranceList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As CommissionPeriod)
            MyBase.New(GetToleranceList(parent), GetType(CommissionTolerance), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal commToleranceId As Guid) As CommissionTolerance
            Dim bo As CommissionTolerance
            For Each bo In Me
                If bo.Id.Equals(commToleranceId) Then Return bo
            Next
            Return Nothing
        End Function

    End Class
#End Region
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidMarkup
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ONE_ENTRY_PER_MARKUP_ONLY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CommissionTolerance = CType(objectToValidate, CommissionTolerance)

            Dim commToleranceView As DataView = obj.getList(obj.CommissionPeriodId, obj.AllowedMarkupPct)
            If commToleranceView.Count > 1 Then
                Return False
            End If
            Return True
            
        End Function
    End Class
#End Region
End Class


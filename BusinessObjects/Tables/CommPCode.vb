'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/1/2010)  ********************
Imports Assurant.ElitaPlus.Common

Public Class CommPCode
    Inherits BusinessObjectBase

#Region "Variables"

    Private moRowExpiration As DataRow

#End Region

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

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CommPCodeDAL
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
            Dim dal As New CommPCodeDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
            If row(CommPCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeDAL.COL_NAME_COMM_P_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If row(CommPCodeDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPCodeDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If row(CommPCodeDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CommPCodeDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPCodeDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If row(CommPCodeDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CommPCodeDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPCodeDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property




#End Region

#Region "Properties-Expiration"

    Public ReadOnly Property MaxExpiration(oData As Object) As Date
        Get
            Dim ds As Dataset
            Dim oExpiration As Date

            Dim oCommPCodeData As CommPCodeData = CType(oData, CommPCodeData)
            Dim dal As New CommPCodeDAL
            ds = dal.LoadExpiration(oCommPCodeData)
            If ds.Tables(CommPCodeDAL.TABLE_NAME).Rows.Count = 0 Then
                oExpiration = GenericConstants.INFINITE_DATE
            Else
                moRowExpiration = ds.Tables(CommPCodeDAL.TABLE_NAME).Rows(0)
                oExpiration = CType(moRowExpiration(CommPCodeDAL.COL_NAME_MAX_EXPIRATION), Date)
            End If
           

            Return oExpiration
        End Get
    End Property

    Public ReadOnly Property ExpirationCount(oData As Object) As Integer
        Get
            Dim ds As Dataset
            Dim nExpiration As Integer

            Dim oCommPCodeData As CommPCodeData = CType(oData, CommPCodeData)
            Dim dal As New CommPCodeDAL
            ds = dal.LoadExpiration(oCommPCodeData)
            If ds.Tables(CommPCodeDAL.TABLE_NAME).Rows.Count = 0 Then
                nExpiration = 0
            Else
                moRowExpiration = ds.Tables(CommPCodeDAL.TABLE_NAME).Rows(0)
                nExpiration = CType(moRowExpiration(CommPCodeDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            End If
           
            Return nExpiration
        End Get
    End Property

#End Region

#Region "Properties-Entity"

    'Public ReadOnly Property AssociatedCommPeriodEntity() As CommPeriodEntity.EntityList
    '    Get
    '        Return New CommPeriodEntity.EntityList(Me)
    '    End Get
    'End Property

#End Region

#Region "Child Entity"

    ' Look for Id in DataSet, If found, used; otherwise create one
    Public Function AddCommPCodeEntity(commPCodeEntityId As Guid) As CommPCodeEntity
        Dim oRow As DataRow
        Dim objCommPCodeEntity As CommPCodeEntity
        Dim oEntityDal As CommPCodeEntityDAL

        If Dataset.Tables.Contains(CommPCodeEntityDAL.TABLE_NAME) = True Then
            oRow = FindRow(commPCodeEntityId, CommPCodeEntityDAL.TABLE_KEY_NAME, Dataset.Tables(CommPCodeEntityDAL.TABLE_NAME))
        End If
        If oRow Is Nothing Then 'it is Not in the dataset, so will bring it from the db
            ' is it new or old ?
            oEntityDal = New CommPCodeEntityDAL
            If oEntityDal.FindRecord(commPCodeEntityId) = True Then
               ' old
                objCommPCodeEntity = New CommPCodeEntity(commPCodeEntityId, Dataset)
            Else
                'new 
                objCommPCodeEntity = New CommPCodeEntity(True, commPCodeEntityId, Dataset)
                objCommPCodeEntity.CommPCodeId = Id
            End If
        Else ' It is in DataSet
            objCommPCodeEntity = New CommPCodeEntity(oRow)
        End If
        Return objCommPCodeEntity
    End Function

    Public Function ClearCommPCodeEntity()
        If (Dataset.Tables.Contains(CommPCodeEntityDAL.TABLE_NAME) = True) Then
            Dataset.Tables.Remove(CommPCodeEntityDAL.TABLE_NAME)
        End If
    End Function

    Public Sub AttachEntities(ByRef totalPComm As Double,  ByRef totalPMarkup As Double)
        Dim oTable As DataTable
        Dim isPComm As Boolean = False
        Dim isPMarkup As Boolean = False

        ClearCommPCodeEntity()
        oTable = CommPCodeEntity.getEntities(Dataset, Id)
        'GetEntityTotals(oTable, totalPComm, isPComm, totalPMarkup, isPMarkup)
    End Sub

    Public Shared Sub GetEntityTotals(oTable As DataTable, ByRef totalPComm As Double, _
                                      ByRef isPComm As Boolean, ByRef totalPMarkup As Double, _
                                      ByRef isPMarkup As Boolean)
        Dim rows As DataRowCollection
        Dim entity As CommPCodeEntity
        Dim isYesNoView As DataView
        Dim isCommFixedCode As String
       
        totalPComm = 0
        totalPMarkup = 0
        isPComm = False
        isPMarkup = False
        rows = oTable.Rows
        isYesNoView = LookupListNew.GetYesNoLookupList(Authentication.LangId)
        For Each row As DataRow In rows
            If row.RowState = DataRowState.Deleted Then Continue For
            entity = Nothing
            entity = New CommPCodeEntity(row)
            isCommFixedCode = LookupListNew.GetCodeFromId(isYesNoView, entity.IsCommFixedId)
            If isCommFixedCode = Codes.YESNO_N Then
                ' Percent Commission
                totalPComm += entity.CommissionAmount.Value
                isPComm = True
            End If

            If isCommFixedCode = Codes.YESNO_N Then
                ' Percent Commission
                totalPMarkup += entity.MarkupAmount.Value
                isPMarkup = True
            End If

            'isMarkupFixedCode = LookupListNew.GetCodeFromId(isYesNoView, entity.IsMarkupFixedId)
            'If isMarkupFixedCode = Codes.YESNO_N Then
            '    ' Percent Markup
            '    totalPMarkup += entity.MarkupAmount.Value
            '    isPMarkup = True
            'End If
        Next

    End Sub

#End Region

#Region "Public Members"

    Public Function Copy(original As CommPCode) As DataView
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Comm Product Code")
        End If
        'Copy myself
        CopyFrom(original)

        'copy the children 
        Dim oDataView As DataView
        Dim oRows As DataRowCollection
        Dim oRow As DataRow
        Dim oEntity, oldEntity As CommPCodeEntity
        Dim oldEntityId As Guid
        'Try
        oDataView = CommPCodeEntity.getList(original.Id)
        oRows = oDataView.Table.Rows
        For Each oRow In oRows
            oEntity = New CommPCodeEntity(Dataset)
            oldEntityId = New Guid(CType(oRow(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ENTITY_ID), Byte()))
            oldEntity = New CommPCodeEntity(oldEntityId, original.Dataset)
            oEntity.CommissionAmount = oldEntity.CommissionAmount
            oEntity.CommPCodeId = Id
            oEntity.EntityId = oldEntity.EntityId
            oEntity.IsCommFixedId = oldEntity.IsCommFixedId            
            oEntity.MarkupAmount = oldEntity.MarkupAmount
            oEntity.PayeeTypeId = oldEntity.PayeeTypeId
            oRow(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ENTITY_ID) = oEntity.Id.ToByteArray
        Next
        Return oDataView
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Dataset.Tables.Contains(CommPCodeEntityDAL.TABLE_NAME) = True Then
                ValidateEntityChildren(Dataset.Tables(CommPCodeEntityDAL.TABLE_NAME))
            End If

            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommPCodeDAL
                UpdateFamily(Dataset)
                '   dal.Update(Me.Row)
                dal.UpdateFamily(Dataset)  'New Code Added Manually                
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

    Public Shared Function getList(oCommPCodeData As CommPCodeData) As CommPCodeSearchDV
        Try
            Dim dal As New CommPCodeDAL
            Return New CommPCodeSearchDV(dal.LoadList(oCommPCodeData).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "CommPCodeSearchDV"
    Public Class CommPCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMM_P_CODE_ID As String = CommPCodeDAL.COL_NAME_COMM_P_CODE_ID
        Public Const COL_COMPANY_CODE As String = CommPCodeDAL.COL_NAME_COMPANY_CODE
        Public Const COL_DEALER_NAME As String = CommPCodeDAL.COL_NAME_DEALER_NAME
        Public Const COL_PRODUCT_CODE As String = CommPCodeDAL.COL_NAME_PRODUCT_CODE
        Public Const COL_EFFECTIVE_DATE As String = CommPCodeDAL.COL_NAME_EFFECTIVE
        Public Const COL_EXPIRATION_DATE As String = CommPCodeDAL.COL_NAME_EXPIRATION

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Validation"

    Public Shared Sub ValidateEntityChildren(oTable As DataTable)
        'Dim rows As DataRowCollection
        Dim entity As CommPCodeEntity
        Dim totalPComm As Double = 0
        Dim totalPMarkup As Double = 0
        'Dim isYesNoView As DataView
        'Dim isCommFixedCode, isMarkupFixedCode As String
        Dim errIndex As Integer = 0
        Dim err1 As ValidationError = Nothing
        Dim err2 As ValidationError = Nothing
        Dim isPComm As Boolean = False
        Dim isPMarkup As Boolean = False

        'rows = oTable.Rows
        'isYesNoView = LookupListNew.GetYesNoLookupList(Authentication.LangId)
        'For Each row As DataRow In rows
        '    If row.RowState = DataRowState.Deleted Then Continue For
        '    entity = Nothing
        '    entity = New CommPCodeEntity(row)
        '    isCommFixedCode = LookupListNew.GetCodeFromId(isYesNoView, entity.IsCommFixedId)
        '    If isCommFixedCode = Codes.YESNO_N Then
        '        ' Percent Commission
        '        totalPComm += entity.CommissionAmount.Value
        '        isPComm = True
        '    End If

        '    isMarkupFixedCode = LookupListNew.GetCodeFromId(isYesNoView, entity.IsMarkupFixedId)
        '    If isMarkupFixedCode = Codes.YESNO_N Then
        '        ' Percent Markup
        '        totalPMarkup += entity.MarkupAmount.Value
        '        isPMarkup = True
        '    End If
        'Next

        GetEntityTotals(oTable, totalPComm, isPComm, totalPMarkup, isPMarkup)
        entity = New CommPCodeEntity
        If ((isPComm = True) AndAlso ((Math.Round(totalPComm, 2) <> 0.0) AndAlso Math.Round(totalPComm, 2) <> 100.0)) Then

            err1 = New ValidationError(ErrorCodes.INVALID_COMM_BREAK_COMM_PCT_ERR, GetType(CommPCodeEntity), _
                                       GetType(CommPCodeEntity), "CommissionAmount", entity.CommissionAmount)
            errIndex += 1
        End If

        If ((isPMarkup = True) AndAlso ((Math.Round(totalPMarkup, 2) <> 0.0) AndAlso Math.Round(totalPMarkup, 2) <> 100.0)) Then
            err2 = New ValidationError(ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR, GetType(CommPCodeEntity), GetType(CommPCodeEntity), "MarkupAmount", entity.MarkupAmount)
            errIndex += 1
        End If
        If errIndex > 0 Then
            Dim errors(errIndex - 1) As ValidationError
            Dim errCurrIndex As Integer = 0
            If err1 IsNot Nothing Then
                errors(errCurrIndex) = err1
                errCurrIndex += 1
            End If
            If err2 IsNot Nothing Then
                errors(errCurrIndex) = err2
                errCurrIndex += 1
            End If
            '   Throw New BOValidationException(errors, CommPCodeEntity.GetType.Name, Me.UniqueId)
            Throw New BOValidationException(errors, "CommPCodeEntity")
        End If
    End Sub

#End Region

End Class



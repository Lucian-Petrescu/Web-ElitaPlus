Public Class RiskTypeTolerance
    Inherits BusinessObjectBase
    #Region "Constant"
    public Const COL_NAME_RISK_TYPE As String = "RISK_TYPE"
    public Const COL_NAME_TOLERANCE_PCT As String = "TOLERANCE_PCT"
    public Const COL_NAME_DEALER As String = "DEALER"

#End Region

#Region "Properties"
    Public ReadOnly Property Id As Guid
        Get
            If row(RiskTypeToleranceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RiskTypeToleranceDAL.COL_NAME_DLR_RK_TYP_TOLERANCE_ID), Byte()))
            End If
        End Get
    End Property

    Public Property RiskTypeToleranceId As Guid
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_DLR_RK_TYP_TOLERANCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RiskTypeToleranceDAL.COL_NAME_DLR_RK_TYP_TOLERANCE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_DLR_RK_TYP_TOLERANCE_ID, Value)
        End Set
    End Property

    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RiskTypeToleranceDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property Dealer As String
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RiskTypeToleranceDAL.COL_NAME_DEALER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_DEALER, Value)
        End Set
    End Property
    
    <ValueMandatory("")>
    Public Property RiskType As String
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_RISK_TYPE, Value)
        End Set
    End Property

  <ValueMandatoryRiskTypeTolerance(""),ValidNumericRange("", Min:=0, Max:=9999.99)>
    Public Property TolerancePct As DecimalType
        Get
            CheckDeleted()
            If Row(RiskTypeToleranceDAL.COL_NAME_TOLERANCE_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RiskTypeToleranceDAL.COL_NAME_TOLERANCE_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RiskTypeToleranceDAL.COL_NAME_TOLERANCE_PCT, Value)
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    public Sub New (ByVal id As Guid, ByVal key As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id,key)
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RiskTypeToleranceDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
           
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RiskTypeToleranceDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then '
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
    Protected Sub Load(ByVal searchid As Guid,ByVal key As Guid)
        Try
            Dim dal As New RiskTypeToleranceDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(key, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 
                dal.Load(Dataset, searchid)
                Row = FindRow(key, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RiskTypeToleranceDAL
                dal.SaveRiskTypeTolerance(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim searchid As Guid = DealerId
                    Dim lookupkey As Guid = RiskTypeToleranceId
                    Dataset = New DataSet
                    Row = Nothing 
                    Load( searchid,lookupkey)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    
    Public Function GetRiskTypeTolerance() As RiskTypeToleranceDV
        Dim RiskTypeToleranceDAL As New RiskTypeToleranceDAL

        If Not (DealerId.Equals(Guid.Empty)) Then
            Return New RiskTypeToleranceDV(RiskTypeToleranceDAL.LoadRiskTypeTolerance(DealerId).Tables(0))
        End If

    End Function

    Public Function ValidateNewRiskTypeTolerance(ByVal DealerInflations As RiskTypeToleranceDV) As Boolean

        Dim dealerInflation() = DealerInflations.ToTable().Select(COL_NAME_RISK_TYPE & "=" & "'" & RiskType & "'")
                               
        If dealerInflation.Length >0 Then
            Return true
        Else 
            Return false
        End If

    End Function

#End Region

#Region "Risk Type Tolerance Search Dataset"
    Public Class RiskTypeToleranceDV
        Inherits DataView

        Public Const COL_DLR_RK_TYP_TOLERANCE_ID As String = "dlr_rk_typ_tolerance_id"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER As String = "dealer"
        Public Const COL_RISK_TYPE As String = "risk_type"
        Public Const COL_TOLERANCE_PCT As String = "tolerance_pct"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Grid Data Related"
    Public Shared Function GetEmptyList(ByVal dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(RiskTypeToleranceDV.COL_DLR_RK_TYP_TOLERANCE_ID) = Guid.NewGuid.ToByteArray
            row(RiskTypeToleranceDV.COL_DEALER_ID) = Guid.NewGuid.ToByteArray
            row.Item(RiskTypeToleranceDV.COL_RISK_TYPE) = String.Empty
            row(RiskTypeToleranceDV.COL_TOLERANCE_PCT) = 0D
            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
       
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As RiskTypeToleranceDV, ByVal NewBO As RiskTypeTolerance)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(RiskTypeToleranceDV.COL_DLR_RK_TYP_TOLERANCE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RiskTypeToleranceDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RiskTypeToleranceDV.COL_DEALER, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RiskTypeToleranceDV.COL_RISK_TYPE, GetType(String))
                dt.Columns.Add(RiskTypeToleranceDV.COL_TOLERANCE_PCT, GetType(String))
               
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(RiskTypeToleranceDV.COL_DLR_RK_TYP_TOLERANCE_ID) = NewBO.Id.ToByteArray
            row(RiskTypeToleranceDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New RiskTypeToleranceDV(dt)
            dv.Sort = COL_NAME_TOLERANCE_PCT & " DESC"

        End If
    End Sub

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryRiskTypeTolerance
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.VALUE_REQUIRED_RISK_TOLERANCE_PERCENTAGE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As RiskTypeTolerance = CType(objectToValidate, RiskTypeTolerance)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
#End Region

End Class


Public Class DealerInflation
    Inherits BusinessObjectBase

#Region "Constant"
    public Const COL_NAME_INFLATION_MONTH As String = "INFLATION_MONTH"
    public Const COL_NAME_INFLATION_YEAR As String = "INFLATION_YEAR"
    public Const COL_NAME_INFLATION_PCT As String = "INFLATION_PCT"

#End Region

#Region "Properties"
    Public ReadOnly Property Id() As Guid
        Get
            If row(DealerInflationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerInflationDAL.COL_NAME_DEALER_INFLATION_ID), Byte()))
            End If
        End Get
    End Property

    Public Property DealerInflationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerInflationDAL.COL_NAME_DEALER_INFLATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerInflationDAL.COL_NAME_DEALER_INFLATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerInflationDAL.COL_NAME_DEALER_INFLATION_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(DealerInflationDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerInflationDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerInflationDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property
    
    <ValueMandatory("")>
    Public Property InflationMonth() As String
        Get
            CheckDeleted()
            If Row(DealerInflationDAL.COL_NAME_INFLATION_MONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerInflationDAL.COL_NAME_INFLATION_MONTH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerInflationDAL.COL_NAME_INFLATION_MONTH, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InflationYear() As string
        Get
            CheckDeleted()
            If Row(DealerInflationDAL.COL_NAME_INFLATION_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerInflationDAL.COL_NAME_INFLATION_YEAR), string)
            End If
        End Get
        Set(ByVal Value As string)
            CheckDeleted()
            Me.SetValue(DealerInflationDAL.COL_NAME_INFLATION_YEAR, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InflationPct() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerInflationDAL.COL_NAME_INFLATION_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerInflationDAL.COL_NAME_INFLATION_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerInflationDAL.COL_NAME_INFLATION_PCT, Value)
        End Set
    End Property

#End Region


#Region "Constructors"

    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    public Sub New (ByVal id As Guid, ByVal key As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id,key)
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DealerInflationDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
           
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New DealerInflationDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then '
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
    Protected Sub Load(ByVal searchid As Guid,ByVal key As Guid)
        Try
            Dim dal As New DealerInflationDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(key, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 
                dal.Load(Me.Dataset, searchid)
                Me.Row = Me.FindRow(key, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
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
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerInflationDAL
                dal.SaveDealerInflation(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim searchid As Guid = Me.DealerId
                    Dim lookupkey As Guid = Me.DealerInflationId
                    Me.Dataset = New DataSet
                    Me.Row = Nothing 
                    Me.Load( searchid,lookupkey)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    
    Public Function GetDealerInflation() As DealerInflationDV
        Dim dealerInflationDAL As New DealerInflationDAL

        If Not (Me.DealerId.Equals(Guid.Empty)) Then
            Return New DealerInflationDV(DealerInflationDAL.LoadDealerInflation(Me.DealerId).Tables(0))
        End If

    End Function

    Public Function ValidateNewDealerInflation(ByVal DealerInflations As DealerInflationDV) As Boolean

        Dim dealerInflation() = DealerInflations.ToTable().Select(COL_NAME_INFLATION_YEAR & "=" & Me.InflationYear &  " And " & COL_NAME_INFLATION_MONTH  &"=" & Me.InflationMonth)
                               
        If dealerInflation.Length >0 Then
            Return true
        Else 
            Return false
        End If

    End Function

#End Region

#Region "Dealer Search Dataset"
    Public Class DealerInflationDV
        Inherits DataView

        Public Const COL_dealer_inflation_id As String = "dealer_inflation_id"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_inflation_month As String = "inflation_month"
        Public Const COL_inflation_year As String = "inflation_year"
        Public Const COL_inflation_pct As String = "inflation_pct"
       
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
            row.Item(DealerInflationDV.COL_dealer_inflation_id) = Guid.NewGuid.ToByteArray
            row(DealerInflationDV.COL_DEALER_ID) = Guid.NewGuid.ToByteArray
            row.Item(DealerInflationDV.COL_inflation_month) = String.Empty
            row.Item(DealerInflationDV.COL_inflation_year) = String.Empty
            row(DealerInflationDV.COL_inflation_pct) = 0D
            dsv.Tables(0).Rows.Add(row)

            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As DealerInflationDV, ByVal NewBO As DealerInflation)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(DealerInflationDV.COL_dealer_inflation_id, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DealerInflationDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DealerInflationDV.COL_inflation_month, GetType(String))
                dt.Columns.Add(DealerInflationDV.COL_inflation_year, GetType(String))
                dt.Columns.Add(DealerInflationDV.COL_inflation_pct, GetType(String))
             

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(DealerInflationDV.COL_dealer_inflation_id) = NewBO.Id.ToByteArray
            row(DealerInflationDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New DealerInflationDV(dt)
        End If
    End Sub

#End Region

End Class

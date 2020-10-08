'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/8/2009)  ********************

Public Class ComunaCode
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

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ComunaCodeDAL
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
            Dim dal As New ComunaCodeDAL
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

    Public Shared Function LoadList(ComunaMask As String, PostalCodeMask As String, RegionMask As Guid) As ComunaCodeSearchDV
        Try
            Dim dal As New ComunaCodeDAL
            Dim UserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Return New ComunaCodeSearchDV(dal.LoadList(ComunaMask, PostalCodeMask, RegionMask, UserId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
 
    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ComunaCodeSearchDV, NewComunaCodeBO As ComunaCode)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewComunaCodeBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ComunaCodeSearchDV.COL_COMUNA_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ComunaCodeSearchDV.COL_COMUNA, GetType(String))
                dt.Columns.Add(ComunaCodeSearchDV.COL_POSTALCODE, GetType(String))
                dt.Columns.Add(ComunaCodeSearchDV.COL_REGION_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ComunaCodeSearchDV.COL_REGION_DESC, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(ComunaCodeSearchDV.COL_COMUNA_CODE_ID) = NewComunaCodeBO.Id.ToByteArray
            row(ComunaCodeSearchDV.COL_COMUNA) = String.Empty
            row(ComunaCodeSearchDV.COL_POSTALCODE) = String.Empty
            row(ComunaCodeSearchDV.COL_REGION_ID) = NewComunaCodeBO.RegionId.ToByteArray
            row(ComunaCodeSearchDV.COL_REGION_DESC) = String.Empty
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ComunaCodeSearchDV(dt)
        End If
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
            If row(ComunaCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ComunaCodeDAL.COL_NAME_COMUNA_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If row(ComunaCodeDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ComunaCodeDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ComunaCodeDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Comuna As String
        Get
            CheckDeleted()
            If row(ComunaCodeDAL.COL_NAME_COMUNA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ComunaCodeDAL.COL_NAME_COMUNA), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ComunaCodeDAL.COL_NAME_COMUNA, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Postalcode As String
        Get
            CheckDeleted()
            If row(ComunaCodeDAL.COL_NAME_POSTALCODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ComunaCodeDAL.COL_NAME_POSTALCODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ComunaCodeDAL.COL_NAME_POSTALCODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ComunaCodeDAL
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

    Public Shared Function LoadList(countryID As Guid) As DataSet
        Try
            Dim dal As New ComunaCodeDAL
            Return dal.LoadList(countryID)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "SearchDV"
    Public Class ComunaCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMUNA_CODE_ID As String = ComunaCodeDAL.COL_NAME_COMUNA_CODE_ID
        Public Const COL_CODE As String = DALBase.COL_NAME_CODE
        Public Const COL_COMUNA As String = ComunaCodeDAL.COL_NAME_COMUNA
        Public Const COL_POSTALCODE As String = ComunaCodeDAL.COL_NAME_POSTALCODE
        Public Const COL_REGION_ID As String = ComunaCodeDAL.COL_NAME_REGION_ID
        Public Const COL_REGION_DESC As String = ComunaCodeDAL.COL_NAME_REGION_DESC

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ComunaCodeSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_COMUNA_CODE_ID) = (New Guid()).ToByteArray
            row(COL_COMUNA) = ""
            row(COL_POSTALCODE) = ""
            row(COL_REGION_ID) = Guid.Empty.ToByteArray
            row(COL_REGION_DESC) = ""
            dt.Rows.Add(row)
            Return New ComunaCodeSearchDV(dt)
        End Function

    End Class
#End Region
End Class



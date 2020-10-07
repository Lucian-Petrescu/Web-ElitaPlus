'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/13/2017)  ********************

Public Class SpUserClaims
    Inherits BusinessObjectBase

#Region "Constants"


    Public Const COL_NAME_SP_USER_CLAIMS_ID As String = "sp_user_claims_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_SP_CLAIM_TYPE_ID As String = "sp_claim_type_id"
    Public Const COL_NAME_SP_CLAIM_VALUE As String = "sp_claim_value"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Const COL_NAME_SP_CLAIM_CODE_DESCRIPTION As String = "sp_claim_code_description"

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
            Dim dal As New SpUserClaimsDAL
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
            Dim dal As New SpUserClaimsDAL
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
            If Row(SpUserClaimsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpUserClaimsDAL.COL_NAME_SP_USER_CLAIMS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property UserId As Guid
        Get
            CheckDeleted()
            If Row(SpUserClaimsDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpUserClaimsDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SpUserClaimsDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property SpClaimTypeId As Guid
        Get
            CheckDeleted()
            If Row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SpUserClaimsDAL.COL_NAME_SP_CLAIM_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=500)>
    Public Property SpClaimValue As String
        Get
            CheckDeleted()
            If Row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_VALUE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SpUserClaimsDAL.COL_NAME_SP_CLAIM_VALUE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(SpUserClaimsDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(SpUserClaimsDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SpUserClaimsDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(SpUserClaimsDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(SpUserClaimsDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SpUserClaimsDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SpUserClaimsDAL
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
    Public Shared Function LoadSpUserClaims(ByVal UserId As Guid, ByVal LanguageId As Guid) As DataView
        Dim dal As New SpUserClaimsDAL
        Dim ds As DataSet
        ds = dal.LoadSpUserClaims(UserId)
        Return ds.Tables("LoadSpUserClaims").DefaultView
    End Function
#End Region
    Public Class SpUserClaimsDV
        Inherits DataView

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property UserId(ByVal row As DataRow) As String
            Get
                Return row(SpUserClaimsDAL.COL_NAME_USER_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property SpClaimTypeID(ByVal row As DataRow) As String
            Get
                Return row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_TYPE_ID).ToString
            End Get
        End Property
        Public Shared ReadOnly Property SpClaimCodeDescription(ByVal row As DataRow) As String
            Get
                Return row(SpClaimTypesDAL.COL_NAME_SP_CLAIM_CODE_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property SpClaimValue(ByVal row As DataRow) As String
            Get
                Return row(SpUserClaimsDAL.COL_NAME_SP_CLAIM_VALUE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row As DataRow) As String
            Get
                Return row(SpUserClaimsDAL.COL_NAME_EFFECTIVE_DATE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row As DataRow) As String
            Get
                Return row(SpUserClaimsDAL.COL_NAME_EXPIRATION_DATE).ToString
            End Get
        End Property

    End Class
End Class



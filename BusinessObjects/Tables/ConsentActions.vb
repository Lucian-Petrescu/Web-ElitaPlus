Imports Common = Assurant.ElitaPlus.Common
Public Class ConsentActions
    Inherits BusinessObjectBase


#Region "Constants"
    Private Const SEARCH_EXCEPTION As String = "SEARCH_CRITERION_001" ' Consent Actions List Search Exception - If no criterion(except companyid) is selected
#End Region


#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Public Sub DeleteAndSave()
        Me.BeginEdit()

        Try
            Me.Delete()
            Me.Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        Catch ex As RowNotInTableException
            ex = Nothing
        Catch ex As Exception
            Me.cancelEdit()
            Throw ex
        End Try
    End Sub
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ConsentActionsDAL
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
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ConsentActionsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConsentActionsDAL.COL_NAME_CONSENT_VALUE_ID), Byte()))
            End If
        End Get
    End Property


#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getConsentActionsList(ByVal ReferenceType As String, ByVal ReferenceValueId As Guid, ByVal ConsentType As String, ByVal ConsentFieldName As String, ByVal languageId As Guid) As ConsentActionsSearchDV
        Try
            Dim dal As New ConsentActionsDAL
            Dim fromdate As Date?
            Dim todate As Date?

            Return New ConsentActionsSearchDV(dal.LoadConsentActionsList(ReferenceType, ReferenceValueId, ConsentType, ConsentFieldName, languageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "ConsentSearchDV"
    Public Class ConsentActionsSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CONSENT_VALUE_ID As String = "consent_value_id"
        Public Const COL_REFERENCE_VALUE As String = "reference_value"
        Public Const COL_REFERENCE_TYPE As String = "reference_type"
        Public Const COL_CONSENT_TYPE As String = "consent_type_xcd"
        Public Const COL_CONSENT_FIELD_NAME As String = "consent_field_name_xcd"
        Public Const COL_CONSENT_FIELD_VALUE As String = "field_value"
        Public Const COL_CONSENT_ACTION As String = "action_xcd"
        Public Const COL_EFFECTIVE As String = "effective"
        Public Const COL_EXPIRATION As String = "expiration"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ConsentActionsDAL
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
#End Region
End Class

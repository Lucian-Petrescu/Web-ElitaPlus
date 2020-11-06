'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/9/2015)  ********************
Imports System.Collections.Generic
Imports System.Linq

Public Class CertExtendedItemFormBO
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DUPLICATE_ELITA_CERT_EXTENDED_ITEM As String = "DUPLICATE_ELITA_CERT_EXTENDED_ITEM"

    Public Const COL_NAME_CERT_EXTENDED_ITEM_COUNT As String = "cert_extended_item_count"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_FIELD_NAME As String = CertExtendedItemFormDAL.COL_NAME_FIELD_NAME
    Public Const COL_NAME_CERT_EXTENDED_ITEM_ID As String = CertExtendedItemFormDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID
    Public Const COL_NAME_DEFAULT_VALUE As String = CertExtendedItemFormDal.COL_NAME_DEFAULT_VALUE
#End Region
#Region "BankNameSearchDV"
    Public Class CertExtendedItemSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_EXT_CONFIG_ID As String = "crt_ext_fields_config_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_FIELD_NAME As String = "field_name"
        'Public Const COL_IN_ENROLLMENT As String = "in_enrollment"
        Public Const COL_DEFAULT_VALUE As String = "default_value"
        Public Const COL_ALLOW_UPDATE As String = "allow_update"
        Public Const COL_ALLOW_DISPLAY As String = "allow_display"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

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
    'Public Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As CertExtendedItemSearchDV

    '    Dim dt As DataTable
    '    dt = dv.Table
    '    Dim newrow As DataRow = dt.NewRow

    '    'newrow(CertExtendedItemFormDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID) = id.ToByteArray
    '    newrow(CertExtendedItemFormDal.COL_NAME_CODE) = String.Empty
    '    newrow(CertExtendedItemFormDal.COL_NAME_DESCRIPTION) = String.Empty
    '    newrow(CertExtendedItemFormDal.COL_NAME_FIELD_NAME) = String.Empty
    '    'newrow(CertExtendedItemFormDal.COL_NAME_IN_ENROLLMENT) = "Y"
    '    newrow(CertExtendedItemFormDal.COL_NAME_DEFAULT_VALUE) = String.Empty
    '    newrow(CertExtendedItemFormDal.COL_NAME_ALLOW_UPDATE) = "Y"
    '    newrow(CertExtendedItemFormDal.COL_NAME_ALLOW_DISPLAY) = "Y"
    '    dt.Rows.Add(newrow)
    '    Me.Row = newrow
    '    Return New CertExtendedItemSearchDV(dt)

    'End Function
    Public Function GetNewDataViewRow(ByVal dv As DataView) As CertExtendedItemSearchDV

        Dim dt As DataTable
        dt = dv.Table
        Dim newrow As DataRow = dt.NewRow

        'newrow(CertExtendedItemFormDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID) = id.ToByteArray
        newrow(CertExtendedItemFormDal.COL_NAME_CODE) = String.Empty
        newrow(CertExtendedItemFormDal.COL_NAME_DESCRIPTION) = String.Empty
        newrow(CertExtendedItemFormDal.COL_NAME_FIELD_NAME) = " "
        'newrow(CertExtendedItemFormDal.COL_NAME_IN_ENROLLMENT) = "Y"
        newrow(CertExtendedItemFormDal.COL_NAME_DEFAULT_VALUE) = String.Empty
        newrow(CertExtendedItemFormDal.COL_NAME_ALLOW_UPDATE) = "Y"
        newrow(CertExtendedItemFormDal.COL_NAME_ALLOW_DISPLAY) = "Y"
        dt.Rows.Add(newrow)
        Me.Row = newrow
        Return New CertExtendedItemSearchDV(dt)

    End Function
    Public Shared Function GetList(ByVal codeMask As String) As CertExtendedItemSearchDV
        Try
            Dim dal As New CertExtendedItemFormDal
            Return New CertExtendedItemSearchDV(dal.LoadList(codeMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Protected Sub Load()
        Try
            Dim dal As New CertExtendedItemFormDal
            If Dataset.Tables.IndexOf(CertExtendedItemFormDal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(CertExtendedItemFormDal.TABLE_NAME).NewRow
            Dataset.Tables(CertExtendedItemFormDal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'SetValue(dal.COL_NAME_FIELD_NAME, " ")
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertExtendedItemFormDal
            If _isDSCreator AndAlso Row IsNot Nothing Then
                Dataset.Tables(CertExtendedItemFormDal.TABLE_NAME).Rows.Remove(Row)
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CertExtendedItemFormDal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, CertExtendedItemFormDal.TABLE_KEY_NAME, Dataset.Tables(CertExtendedItemFormDal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, CertExtendedItemFormDal.TABLE_KEY_NAME, Dataset.Tables(CertExtendedItemFormDal.TABLE_NAME))
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
        'InEnrollment = Codes.YESNO_Y
        AllowUpdate = Codes.YESNO_Y
        FieldName = String.Empty
        DefaultValue = String.Empty
        AllowDisplay = Codes.YESNO_Y
    End Sub
#End Region
    Public Function GetAvailableCompanies() As DataView
        Try
            Dim dv As DataView = BusinessObjectsNew.User.GetUserCompanies(Authentication.CurrentUser.Id)
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Function GetSelectedCompanies(ByVal codeMask As String) As DataView

        Try
            Dim dal As New CertExtendedItemFormDal
            Dim dv As DataView = dal.LoadSelectedCompaniesList(codeMask).Tables(0).DefaultView
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Function GetSelectedDealers(ByVal codeMask As String) As DataView

        Try
            Dim dal As New CertExtendedItemFormDal
            Dim dv As DataView = dal.LoadSelectedDealersList(codeMask).Tables(0).DefaultView
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Function GetAvailableDealers() As DataView
        Try
            Dim dvMain As DataView = BusinessObjectsNew.User.GetUserCompanies(Authentication.CurrentUser.Id)
            Dim dv As DataView
            Dim dsMerge = New DataSet()
            For Each rowView As DataRowView In dvMain
                Dim ds = New DataSet()
                Dim row As DataRow = rowView.Row
                Dim currentCompanyID As Guid = New Guid(CType(row("COMPANY_ID"), Byte()))
                dv = LookupListNew.GetDealerLookupList(currentCompanyID)
                ds.Tables.Add(dv.ToTable())
                dsMerge.Merge(ds)
            Next
            Return dsMerge.Tables(0).DefaultView()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Sub ClearCompanyList(ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedCompanies(codeMask)
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentCompanyID As Guid = New Guid(CType(row("ID"), Byte()))
                Dim dal = New CertExtendedItemFormDal
                dal.DeleteDealerCompanyList(codeMask, "ELP_COMPANY", currentCompanyID)
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub SaveCompanyList(ByVal companylist As ArrayList, ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedCompanies(codeMask)
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentCompanyID As Guid = New Guid(CType(row("ID"), Byte()))
                For Each Str As String In companylist
                    Dim company_id As Guid = New Guid(Str)
                    If currentCompanyID = company_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemFormDal
                    dal.DeleteDealerCompanyList(codeMask, "ELP_COMPANY", currentCompanyID)
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In companylist
                Dim dFound As Boolean = False
                For Each rowView As DataRowView In dv
                    Dim company_id As Guid = New Guid(Str)
                    Dim row As DataRow = rowView.Row
                    Dim currentCompanyID As Guid = New Guid(CType(row("ID"), Byte()))
                    If currentCompanyID = company_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemFormDal
                    Dim company_id As Guid = New Guid(Str)
                    dal.SaveDealerCompanyList(codeMask, "ELP_COMPANY", company_id)
                End If
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub ClearDealerList(ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedDealers(codeMask)
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentDealerID As Guid = New Guid(CType(row("ID"), Byte()))

                Dim dal = New CertExtendedItemFormDal
                dal.DeleteDealerCompanyList(codeMask, "ELP_DEALER", currentDealerID)
                dFound = True
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub SaveDealerList(ByVal dealerlist As ArrayList, ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedDealers(codeMask)
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentDealerID As Guid = New Guid(CType(row("ID"), Byte()))
                For Each Str As String In dealerlist
                    Dim dealer_id As Guid = New Guid(Str)
                    If currentDealerID = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemFormDal
                    dal.DeleteDealerCompanyList(codeMask, "ELP_DEALER", currentDealerID)
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In dealerlist
                Dim dFound As Boolean = False
                For Each rowView As DataRowView In dv
                    Dim dealer_id As Guid = New Guid(Str)
                    Dim row As DataRow = rowView.Row
                    Dim currentDealerID As Guid = New Guid(CType(row("ID"), Byte()))
                    If currentDealerID = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemFormDal
                    Dim dealer_id As Guid = New Guid(Str)
                    dal.SaveDealerCompanyList(codeMask, "ELP_DEALER", dealer_id)
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertExtendedItemFormDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertExtendedItemFormDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID), Byte()))
            End If
        End Get
    End Property
    '<ValueMandatory(""), ValidStringLength("", Max:=30)>
    'Public Property TableName() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertExtendedItemFormDAL.COL_NAME_TABLE_NAME) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertExtendedItemFormDAL.COL_NAME_TABLE_NAME), String)
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertExtendedItemFormDal.COL_NAME_TABLE_NAME, value)
    '    End Set
    'End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property FieldName() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME, value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDal.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDal.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDal.COL_NAME_CODE, value)
        End Set
    End Property
    '<ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDal.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDal.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDal.COL_NAME_DESCRIPTION, value)
        End Set
    End Property

    '<ValueMandatory(""), ValidStringLength("", Max:=3)>
    'Public Property InEnrollment() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT), String)
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT, value)
    '    End Set
    'End Property

    '<ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property DefaultValue() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=3)>
    Public Property AllowUpdate() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDal.COL_NAME_ALLOW_UPDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDal.COL_NAME_ALLOW_UPDATE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDal.COL_NAME_ALLOW_UPDATE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=3)>
    Public Property AllowDisplay() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDal.COL_NAME_ALLOW_DISPLAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDal.COL_NAME_ALLOW_DISPLAY), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDal.COL_NAME_ALLOW_DISPLAY, value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertExtendedItemFormDal
                dal.Update(Me.Row)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicateCertExtendedItem
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_ELITA_CERT_EXTENDED_ITEM)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertExtendedItemFormBO = CType(objectToValidate, CertExtendedItemFormBO)
            For Each dr As DataRow In obj.Row.Table.Rows
                Dim oEa As CertExtendedItemFormBO = New CertExtendedItemFormBO(dr)
                If (oEa.Id = obj.Id) Then
                    Continue For
                End If
            Next

            Return True
        End Function
    End Class

End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/9/2015)  ********************
Imports System.Collections.Generic
Imports System.Linq

Public Class CertExtendedItem
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DUPLICATE_ELITA_CERT_EXTENDED_ITEM As String = "DUPLICATE_ELITA_CERT_EXTENDED_ITEM"

    Public Const COL_NAME_CERT_EXTENDED_ITEM_COUNT As String = "cert_extended_item_count"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_FIELD_NAME As String = CertExtendedItemDal.COL_NAME_FIELD_NAME
    Public Const COL_NAME_CERT_EXTENDED_ITEM_ID As String = CertExtendedItemDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID
    Public Const COL_NAME_DEFAULT_VALUE As String = CertExtendedItemDal.COL_NAME_DEFAULT_VALUE
#End Region
#Region "BankNameSearchDV"
    Public Class CertExtendedItemSearchDv
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_EXT_CONFIG_ID As String = "crt_ext_cd_flds_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_FIELD_NAME As String = "field_name"
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
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDs
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub
    Public Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As CertExtendedItemSearchDv

        Dim dt As DataTable
        dt = dv.Table
        Dim newRow As DataRow = dt.NewRow

        newRow(CertExtendedItemDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID) = id.ToByteArray
        newRow(CertExtendedItemDal.COL_NAME_CODE) = String.Empty
        newRow(CertExtendedItemDal.COL_NAME_DESCRIPTION) = String.Empty
        newRow(CertExtendedItemDal.COL_NAME_FIELD_NAME) = String.Empty
        newRow(CertExtendedItemDal.COL_NAME_DEFAULT_VALUE) = String.Empty
        newRow(CertExtendedItemDal.COL_NAME_ALLOW_UPDATE) = "N"
        newRow(CertExtendedItemDal.COL_NAME_ALLOW_DISPLAY) = "Y"
        dt.Rows.Add(newRow)
        Row = newRow
        Return New CertExtendedItemSearchDv(dt)

    End Function
    Public Shared Function GetList(ByVal codeMask As String) As CertExtendedItemSearchDV
        Try
            Dim dal As New CertExtendedItemDal
            Return New CertExtendedItemSearchDV(dal.LoadList(codeMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Protected Sub Load()
        Try
            Dim dal As New CertExtendedItemDal
            If Dataset.Tables.IndexOf(CertExtendedItemDal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(CertExtendedItemDal.TABLE_NAME).NewRow
            Dataset.Tables(CertExtendedItemDal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertExtendedItemDal
            If _isDSCreator AndAlso Row IsNot Nothing Then
                Dataset.Tables(CertExtendedItemDal.TABLE_NAME).Rows.Remove(Row)
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CertExtendedItemDal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, CertExtendedItemDal.TABLE_KEY_NAME, Dataset.Tables(CertExtendedItemDal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, CertExtendedItemDal.TABLE_KEY_NAME, Dataset.Tables(CertExtendedItemDal.TABLE_NAME))
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
        AllowUpdate = Codes.YESNO_N
        FieldName = String.Empty
        DefaultValue = String.Empty
        AllowDisplay = Codes.YESNO_Y
    End Sub
#End Region
#Region "Dealer Company List"
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
            Dim dal As New CertExtendedItemDal
            Dim dv As DataView = dal.LoadSelectedCompaniesList(codeMask).Tables(0).DefaultView
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Function GetSelectedDealers(ByVal codeMask As String) As DataView

        Try
            Dim dal As New CertExtendedItemDal
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
                Dim currentCompanyId As Guid = New Guid(CType(row("COMPANY_ID"), Byte()))
                dv = LookupListNew.GetDealerLookupList(currentCompanyId)
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
                Dim currentCompanyId As Guid = New Guid(CType(row("ID"), Byte()))
                Dim dal = New CertExtendedItemDal
                dal.DeleteDealerCompanyList(codeMask, "ELP_COMPANY", currentCompanyId)
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub SaveCompanyList(ByVal companylist As ArrayList, ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedCompanies(codeMask)
            Me.SetCreatedAuditInfo()
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentCompanyId As Guid = New Guid(CType(row("ID"), Byte()))
                For Each str As String In companylist
                    Dim companyId As Guid = New Guid(str)
                    If currentCompanyId = companyId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemDal
                    dal.DeleteDealerCompanyList(codeMask, "ELP_COMPANY", currentCompanyID)
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In companylist
                Dim dFound As Boolean = False
                For Each rowView As DataRowView In dv
                    Dim companyId As Guid = New Guid(Str)
                    Dim row As DataRow = rowView.Row
                    Dim currentCompanyId As Guid = New Guid(CType(row("ID"), Byte()))
                    If currentCompanyId = companyId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemDal
                    Dim companyId As Guid = New Guid(Str)
                    dal.SaveDealerCompanyList(codeMask, "ELP_COMPANY", companyId, Me.Row(DALBase.COL_NAME_CREATED_BY).ToString())
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
                Dim row As DataRow = rowView.Row
                Dim currentDealerId As Guid = New Guid(CType(row("ID"), Byte()))

                Dim dal = New CertExtendedItemDal
                dal.DeleteDealerCompanyList(codeMask, "ELP_DEALER", currentDealerId)
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub SaveDealerList(ByVal dealerList As ArrayList, ByVal codeMask As String)
        Try
            Dim dv As DataView
            dv = GetSelectedDealers(codeMask)
            Me.SetCreatedAuditInfo()
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each rowView As DataRowView In dv
                Dim dFound As Boolean = False
                Dim row As DataRow = rowView.Row
                Dim currentDealerId As Guid = New Guid(CType(row("ID"), Byte()))
                For Each Str As String In dealerList
                    Dim dealerId As Guid = New Guid(Str)
                    If currentDealerId = dealerId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemDal
                    dal.DeleteDealerCompanyList(codeMask, "ELP_DEALER", currentDealerID)
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In dealerList
                Dim dFound As Boolean = False
                For Each rowView As DataRowView In dv
                    Dim dealerId As Guid = New Guid(Str)
                    Dim row As DataRow = rowView.Row
                    Dim currentDealerId As Guid = New Guid(CType(row("ID"), Byte()))
                    If currentDealerId = dealerId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim dal = New CertExtendedItemDal
                    Dim dealerId As Guid = New Guid(Str)
                    dal.SaveDealerCompanyList(codeMask, "ELP_DEALER", dealerId, Me.Row(DALBase.COL_NAME_CREATED_BY).ToString())
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Sub SaveDescription(ByVal codeMask As String, ByVal descriptionValue As String)
        Try
            Me.SetModifiedAuditInfo()
            Dim dal = New CertExtendedItemDal
            dal.SaveDescription(codeMask, descriptionValue, Me.Row(DALBase.COL_NAME_MODIFIED_BY).ToString())

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region
#Region "Dealer Company List Validation"
    Public Shared Function GetFieldConfigExist(ByVal codeMask As String, ByVal fieldName As String) As DataView

        Try
            Dim dal As New CertExtendedItemDal
            Dim dv As DataView = dal.FieldConfigExist(codeMask, fieldName).Tables(0).DefaultView
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetDealerCompanyConfigExist(ByVal code As String, ByVal reference As String, ByVal id As Guid) As DataView

        Try
            Dim dal As New CertExtendedItemDal
            Dim dv As DataView = dal.DealerCompanyConfigExist(code, reference, id).Tables(0).DefaultView
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertExtendedItemDal.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertExtendedItemDal.COL_NAME_CRT_EXT_FIELDS_CONFIG_ID), Byte()))
            End If
        End Get
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property FieldName() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_FIELD_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_FIELD_NAME), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_FIELD_NAME, value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_CODE, value)
        End Set
    End Property
    '<ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_DESCRIPTION, value)
        End Set
    End Property
    '<ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property DefaultValue() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_DEFAULT_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_DEFAULT_VALUE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_DEFAULT_VALUE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=3)>
    Public Property AllowUpdate() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_ALLOW_UPDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_ALLOW_UPDATE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_ALLOW_UPDATE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=3)>
    Public Property AllowDisplay() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemDal.COL_NAME_ALLOW_DISPLAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemDal.COL_NAME_ALLOW_DISPLAY), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemDal.COL_NAME_ALLOW_DISPLAY, value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertExtendedItemDal
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
            Dim obj As CertExtendedItem = CType(objectToValidate, CertExtendedItem)
            For Each dr As DataRow In obj.Row.Table.Rows
                Dim oEa As CertExtendedItem = New CertExtendedItem(dr)
                If (oEa.Id = obj.Id) Then
                    Continue For
                End If
            Next

            Return True
        End Function
    End Class

End Class
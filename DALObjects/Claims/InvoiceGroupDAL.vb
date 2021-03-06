﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/12/2013)********************


Public Class InvoiceGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_GROUP"
    Public Const TABLE_KEY_NAME As String = "invoice_group_id"

    Public Const COL_NAME_INVOICE_GROUP_ID As String = "invoice_group_id"
    Public Const COL_NAME_INVOICE_GROUP_NUMBER As String = "invoice_group_number"
    Public Const COL_NAME_INVOICE_GROUP_STATUS_ID As String = "invoice_group_status_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"

    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_MOBILE_NUMBER As String = "work_phone"
    Public Const COL_NAME_DUE_DATE As String = "due_date"
    Public Const COL_NAME_SERVICE_CENTER_NAME As String = "description"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_STATUS_ID As String = "invoice_status_id"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_MEMBERSHIP_NUMBER As String = "membership_number"
    Public Const COL_NAME_CERTIFICATE As String = "cert_number"


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal Invgrpnum As String, ByVal claimnumber As String, _
                                    ByVal oCountryId As Guid, ByVal groupnofrom As String, ByVal mobilenum As String, _
                                   ByVal duedate As String, ByVal svcname As String, ByVal groupnoto As String, _
                                  ByVal invoicenum As String, ByVal Invstatusid As Guid, ByVal Membershipnumber As String, ByVal Certificate As String) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")


            Dim whereClauseConditions As String = ""

            If Not (oCountryId = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "sc." & Me.COL_NAME_COUNTRY_ID & "= " & MiscUtil.GetDbStringFromGuid(oCountryId)
            End If
            'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("sc." & Me.COL_NAME_COUNTRY_ID, oCountryIds, False)
            If Me.FormatSearchMask(Invgrpnum) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "Invgrp." & Me.COL_NAME_INVOICE_GROUP_NUMBER & Invgrpnum
            End If
            If Me.FormatSearchMask(claimnumber) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cl." & Me.COL_NAME_CLAIM_NUMBER & ") " & claimnumber.ToUpper
            End If
            If Me.FormatSearchMask(mobilenum) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(C." & Me.COL_NAME_MOBILE_NUMBER & "),' ') " & mobilenum.ToUpper
            End If
            If Me.FormatSearchMask(svcname) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & "),' ') " & svcname.ToUpper
            End If
            If Me.FormatSearchMask(Membershipnumber) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "NVL(UPPER(C." & Me.COL_NAME_MEMBERSHIP_NUMBER & "),' ') " & Membershipnumber.ToUpper
            End If

            If Me.FormatSearchMask(invoicenum) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(INV." & Me.COL_NAME_INVOICE_NUMBER & ") " & invoicenum.ToUpper
            End If

            If Me.FormatSearchMask(Certificate) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(C." & Me.COL_NAME_CERTIFICATE & ") " & Certificate.ToUpper
            End If

            If Not (groupnofrom = Nothing) Then

                whereClauseConditions &= " AND " & Environment.NewLine & "trunc(Invgrp." & Me.COL_NAME_CREATED_DATE & ") >= '" & groupnofrom & " '"
            End If

            If (Not (groupnoto = Nothing)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "trunc(Invgrp." & Me.COL_NAME_CREATED_DATE & ") <= '" & groupnoto & " '"
            End If

            If Not (duedate = Nothing) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "trunc(INV." & Me.COL_NAME_DUE_DATE & ") = '" & duedate & "'"
            End If
            'whereClauseConditions &= " AND " & Environment.NewLine & "ROWNUM < " & ROW_MAX
            If Not (Invstatusid = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "INV." & Me.COL_NAME_INVOICE_STATUS_ID & "=" & MiscUtil.GetDbStringFromGuid(Invstatusid)

            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If


            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    



#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class




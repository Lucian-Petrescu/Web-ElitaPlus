'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/26/2007)  ********************

Public Class PoliceReport
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
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

    'Existing BO attaching to a BO family
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
    ''Existing or New BO by claimid ???
    Public Sub New(ByVal claimid As Guid, ByVal NewOrExisting As Boolean)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(claimid, NewOrExisting)
    End Sub
    'Existing BO attaching to a BO family by claimid
    Public Sub New(ByVal Claimid As Guid, ByVal familyDS As Dataset, ByVal NewOrExisting As Boolean)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(ClaimId, NewOrExisting)
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PoliceReportDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New PoliceReportDAL
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
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal Claimid As Guid, ByVal NewOrExist As Boolean)
        ' load the police report by claim id which is not the primary key !
        Try
            Dim dal As New PoliceReportDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(Claimid, dal.COL_NAME_CLAIM_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadbyClaimid(Me.Dataset, Claimid)
                Me.Row = Me.FindRow(Claimid, dal.COL_NAME_CLAIM_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If


            If Me.Row Is Nothing Then
                Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
                Me.Row = newRow
            End If

            '' NOTE: There may or may not be a record for that claim id, so need to catch the following 
            '' DataNotFound exception, in the calling form and do NOT throw an error !
            'If Me.Row Is Nothing Then
            '    Throw New DataNotFoundException
            'End If
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(PoliceReportDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PoliceReportDAL.COL_NAME_POLICE_REPORT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property PoliceStationId() As Guid
        Get
            CheckDeleted()
            If row(PoliceReportDAL.COL_NAME_POLICE_STATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PoliceReportDAL.COL_NAME_POLICE_STATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PoliceReportDAL.COL_NAME_POLICE_STATION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(PoliceReportDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PoliceReportDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PoliceReportDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ClaimNumber(ByVal mclaimid As Guid) As String
        Get
            Dim claim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(mclaimid)
            'Me.SetValue(ClaimDAL.COL_NAME_CERTIFICATE_NUMBER, cert.CertNumber)
            Return claim.ClaimNumber
        End Get
    End Property

    Public ReadOnly Property CertificateNumber(ByVal mclaimid As Guid) As String
        Get
            Dim claim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(mclaimid)
            Dim certItemCoverage As New certItemCoverage(claim.CertItemCoverageId)
            Dim certItem As New certItem(certItemCoverage.CertItemId)
            Dim cert As New Certificate(certItem.CertId)
            Return cert.CertNumber
        End Get
    End Property

    Public ReadOnly Property DealerName(ByVal mclaimid As Guid) As String
        Get
            Dim claim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(mclaimid)
            Dim certItemCoverage As New certItemCoverage(claim.CertItemCoverageId)
            Dim certItem As New certItem(certItemCoverage.CertItemId)
            Dim cert As New Certificate(certItem.CertId)
            Dim dlr As New Dealer(cert.DealerId)
            Return dlr.DealerName
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property ReportNumber() As String
        Get
            CheckDeleted()
            If Row(PoliceReportDAL.COL_NAME_REPORT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceReportDAL.COL_NAME_REPORT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceReportDAL.COL_NAME_REPORT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property OfficerName() As String
        Get
            CheckDeleted()
            If Row(PoliceReportDAL.COL_NAME_OFFICER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceReportDAL.COL_NAME_OFFICER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceReportDAL.COL_NAME_OFFICER_NAME, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PoliceReportDAL
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

    Public Shared Sub DeleteNewChildPoliceReport(ByVal parentClaim As Claim)
        Dim row As DataRow
        If parentClaim.Dataset.Tables.IndexOf(PoliceReportDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            'For Each row In parentClaim.Dataset.Tables(PoliceReportDAL.TABLE_NAME).Rows
            For rowIndex = 0 To parentClaim.Dataset.Tables(PoliceReportDAL.TABLE_NAME).Rows.Count - 1
                row = parentClaim.Dataset.Tables(PoliceReportDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim p As PoliceReport = New PoliceReport(row)
                    If parentClaim.Id.Equals(p.ClaimId) And p.IsNew Then
                        p.Delete()
                    End If
                End If
            Next

        End If
    End Sub

    Public Function IsReportNumberInUser(ByRef lstClaimNum As Collections.Generic.List(Of String)) As Boolean
        Dim blnInUser As Boolean = False
        Dim dal As New PoliceReportDAL
        Dim ds As DataSet = dal.GetClaimsByPoliceRptNumber(Me.PoliceStationId, Me.ReportNumber)
        If (Not ds Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            blnInUser = True
            If lstClaimNum Is Nothing Then lstClaimNum = New Collections.Generic.List(Of String)
            Dim dr As DataRow
            For Each dr In ds.Tables(0).Rows
                lstClaimNum.Add(dr("claim_number").ToString())
            Next
        End If
        Return blnInUser
    End Function
    Public Shared Function IsReportNumberInUse(ByRef lstClaimNum As Collections.Generic.List(Of String), ByVal policeReportNumber As String, ByVal policeStationId As Guid) As Boolean
        Dim blnInUser As Boolean = False
        Dim dal As New PoliceReportDAL
        Dim ds As DataSet = dal.GetClaimsByPoliceRptNumber(policeStationId, policeReportNumber)
        If (Not ds Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            blnInUser = True
            If lstClaimNum Is Nothing Then lstClaimNum = New Collections.Generic.List(Of String)
            Dim dr As DataRow
            For Each dr In ds.Tables(0).Rows
                lstClaimNum.Add(dr("claim_number").ToString())
            Next
        End If
        Return blnInUser
    End Function

#End Region

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            If (Not IsEmptyString(Me.ReportNumber)) AndAlso _
                (Not Me.PoliceStationId.Equals(Guid.Empty)) Then
                Return False
            End If
            Return True
        End Get
    End Property

    Private Function IsEmptyString(ByVal value As String)
        Return (value Is Nothing OrElse value.Trim.Length = 0)
    End Function

#Region "DataView Retrieveing Methods"

#End Region

End Class



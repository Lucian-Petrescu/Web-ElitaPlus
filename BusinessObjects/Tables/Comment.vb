'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/3/2005)  ********************

Public Class Comment
    Inherits BusinessObjectBase

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

    'Protected Sub Load()
    '    Try
    '        Dim dal As New CommentDAL
    '        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
    '            dal.LoadSchema(Me.Dataset)
    '        End If
    '        Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
    '        Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
    '        Me.Row = newRow
    '        setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
    '        Initialize()
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

    Protected Sub Load()
        Try
            Dim dal As New CommentDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            If Dataset.Tables(dal.TABLE_NAME).Rows.Count = 1 Then
                Dataset.Tables(dal.TABLE_NAME).Rows(0).Delete()
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CommentDAL
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
    Private _cert As Certificate
    Private _claim As ClaimBase

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CommentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommentDAL.COL_NAME_COMMENT_ID), Byte()))
            End If
        End Get
    End Property

    <MandatryCertOrForgetRequest("")>
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommentDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_CERT_ID, Value)
            'refresh the certifcate object
            _cert = Nothing
        End Set
    End Property

    Public ReadOnly Property Certificate() As Certificate
        Get
            If _cert Is Nothing Then
                If Not CertId.Equals(Guid.Empty) Then
                    _cert = New Certificate(CertId)
                End If
            End If
            Return _cert
        End Get
    End Property


    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommentDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_CLAIM_ID, Value)
            'refresh the claim
            _claim = Nothing
        End Set
    End Property

    Public ReadOnly Property Claim() As ClaimBase
        Get
            If _claim Is Nothing Then
                If Not ClaimId.Equals(Guid.Empty) Then
                    _claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId)
                End If
            End If
            Return _claim
        End Get
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property CallerName() As String
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommentDAL.COL_NAME_CALLER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_CALLER_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CommentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_COMMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommentDAL.COL_NAME_COMMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_COMMENT_TYPE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)>
    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommentDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    <MandatryCertOrForgetRequest("")>
    Public Property ForgotRequestId() As Guid
        Get
            CheckDeleted()
            If Row(CommentDAL.COL_NAME_FORGOT_REQUEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommentDAL.COL_NAME_FORGOT_REQUEST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommentDAL.COL_NAME_FORGOT_REQUEST_ID, Value)
        End Set
    End Property

    Public ReadOnly Property AddedBy() As String
        Get
            Dim userCode As String = CreatedById
            If userCode Is Nothing Then
                userCode = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            End If
            Return LookupListNew.GetDescriptionFromCode(LookupListNew.LK_USERS, userCode)
        End Get
    End Property


    Public ReadOnly Property CertificateNumber() As String
        Get
            If Not Certificate Is Nothing Then
                Return Certificate.CertNumber
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property ClaimNumber() As String
        Get
            If Not Claim Is Nothing Then
                Return Claim.ClaimNumber
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property ClaimStatus() As String
        Get
            If Not Claim Is Nothing Then
                Return Claim.StatusCode
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property Dealer() As String
        Get
            If Not Certificate Is Nothing Then
                Return LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEALERS, Certificate.DealerId)
            Else
                Return Nothing
            End If
        End Get
    End Property


#End Region


#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatryCertOrForgetRequest
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.MSG_CERT_Or_FORGOT_REQUEST_MANDATORY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Comment = CType(objectToValidate, Comment)

            If Not obj.ForgotRequestId = Guid.Empty AndAlso obj.CertId = Guid.Empty Then
                Return True
            ElseIf obj.ForgotRequestId = Guid.Empty AndAlso Not obj.CertId = Guid.Empty Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommentDAL
                UpdateFamily(Dataset)
                dal.UpdateFamily(Dataset)
                If (Not Claim Is Nothing) AndAlso (Me.Claim.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                    CType(Claim, Claim).HandleGVSTransactionCreation(Id, Nothing)
                End If
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
   Public Sub PopulateWithDefaultValues(ByVal certId As Guid, Optional ByVal claimId As Object = Nothing)
        Dim cert As New Certificate(certId)
        Me.CertId = certId
        CallerName = cert.CustomerName
        SetValue(DALBase.COL_NAME_CREATED_BY, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        If Not claimId Is Nothing Then
            Me.ClaimId = CType(claimId, Guid)
        End If
    End Sub

    Public Shared Function GetNewComment(ByVal certId As Guid, Optional ByVal claimId As Object = Nothing) As Comment
        Dim c As New Comment
        c.PopulateWithDefaultValues(certId, claimId)
        Return c
    End Function

    Public Shared Function GetNewComment(ByVal original As Comment) As Comment
        Dim c As New Comment
        c.CopyFrom(original)
        c.SetValue(DALBase.COL_NAME_CREATED_BY, original.CreatedById)
        c.SetValue(DALBase.COL_NAME_CREATED_DATE, original.CreatedDate)
        Return c
    End Function

    Public Shared Function GetLatestComment(ByVal parentClaim As ClaimBase) As Comment
        Dim dal As New CommentDAL
        Dim ds As DataSet = dal.LoadListForClaim(parentClaim.Id)
        If Not ds.Tables(dal.TABLE_NAME) Is Nothing AndAlso ds.Tables(dal.TABLE_NAME).Rows.Count > 0 Then
            Dim c As New Comment(ds.Tables(dal.TABLE_NAME).Rows(0))
            c._isDSCreator = True
            Return c
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetCommentsForClaim(ByVal ClaimId As Guid) As DataSet
        Dim dal As New CommentDAL
        Return dal.LoadListForClaim(ClaimId)
    End Function

    Public Shared Sub DeleteNewChildComment(ByVal parentClaim As ClaimBase)
        Dim row As DataRow
        If parentClaim.Dataset.Tables.IndexOf(CommentDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            'For Each row In parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows
            For rowIndex = 0 To parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Count - 1
                row = parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim c As Comment = New Comment(row)
                    If parentClaim.Id.Equals(c.ClaimId) And c.IsNew Then
                        c.Delete()
                    End If
                End If
            Next

        End If
    End Sub
    Public Sub AddClaimAuthComment()
        _isDSCreator= true
        Save()
    End Sub

#End Region

#Region "Shared Methods"

    Public Shared Sub SetProcessCancellationData(ByVal oCertCancelCommentInfoData As CommentData, _
                                              ByVal oCommentInfo As Comment)
        With oCertCancelCommentInfoData
            .CommentId = oCommentInfo.Id
            .Callername = oCommentInfo.CallerName
            .Comment = oCommentInfo.Comments
            .CommentTypeId = oCommentInfo.CommentTypeId
        End With
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    'Manually added method

    Public Shared Function getList(ByVal certId As Guid) As CommentSearchDV

        Try
            Dim dal As New CommentDAL

            Return New CommentSearchDV(dal.LoadList(certId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getExtList(ByVal claimId As Guid) As ExtCommentSearchDV

        Try
            Dim dal As New CommentDAL

            Return New ExtCommentSearchDV(dal.LoadExtendedList(claimId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class CommentSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMMENT_ID As String = CommentDAL.COL_NAME_COMMENT_ID
        Public Const COL_ADDED_BY As String = CommentDAL.COL_NAME_ADDED_BY
        Public Const COL_CREATED_DATE As String = CommentDAL.COL_NAME_CREATED_DATE
        Public Const COL_CALLER_NAME As String = CommentDAL.COL_NAME_CALLER_NAME
        Public Const COL_COMMENTS As String = CommentDAL.COL_NAME_COMMENTS
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class ExtCommentSearchDV
        Inherits DataView
#Region "ExtConstants"
        Public Const COL_EXT_COMMENT_ID As String = CommentDAL.COL_NAME_EXT_STATUS_ID
        Public Const COL_EXT_ADDED_BY As String = CommentDAL.COL_NAME_ADDED_BY
        Public Const COL_EXT_CREATED_DATE As String = CommentDAL.COL_NAME_CREATED_DATE
        Public Const COL_EXT_CALLER_NAME As String = CommentDAL.COL_NAME_CALLER_NAME
        Public Const COL_EXT_COMMENTS As String = CommentDAL.COL_NAME_COMMENTS
#End Region
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region


#Region "Claim Comment List Selection View"
    Public Class ClaimCommentList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As ClaimBase)
            MyBase.New(LoadTable(parent), GetType(Comment), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, Comment).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As ClaimBase) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimCommentList)) Then
                    Dim dal As New CommentDAL
                    dal.LoadListForClaim(parent.Id, parent.Dataset)
                    parent.AddChildrenCollection(GetType(ClaimCommentList))
                End If
                Return parent.Dataset.Tables(CommentDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

End Class



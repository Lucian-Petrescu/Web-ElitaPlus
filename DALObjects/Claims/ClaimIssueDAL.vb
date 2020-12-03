Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class ClaimIssueDAL
    Inherits EntityIssueDAL
#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

    Public Class PublishTaskClaimData
        Public Property ClaimId As Guid
        Public Property EventTypeLookup As Func(Of Guid, Guid)
    End Class

    Public Const COL_NAME_CREATED_BY = "created_by_name"

#Region "Fraude Methods"
    Public Function ProcessFraudMonitoringIndicatorRule(ByVal claimId As Guid, ByVal certId As Guid, ByVal issueCode As String) As String
        Dim errorCode As String = String.Empty
        Dim storedProcName As String = Me.Config("/SQL/FraudMonitoringIndicatorRule")

        Using connection As OracleConnection = CreateConnection()
            Using command As OracleCommand = CreateCommand(storedProcName, CommandType.StoredProcedure, connection)
                command.AddParameter("pi_claim_id", OracleDbType.Raw, 16, claimId.ToByteArray())
                command.AddParameter("pi_cert_id", OracleDbType.Raw, 16, certId.ToByteArray())
                command.AddParameter("pi_issue_code", OracleDbType.Varchar2, 20, issueCode)
                command.AddParameter("po_error_code", OracleDbType.Varchar2, 50, errorCode, ParameterDirection.Output)
                command.BindByName = True

                Try
                    command.ExecuteNonQuery()
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
                End Try

                If Not command.Parameters("po_error_code").Value.IsNull Then
                    errorCode = command.Parameters("po_error_code").Value.ToString
                End If

                Return errorCode

            End Using
        End Using
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, ByVal publishEventData As PublishedTaskDAL.PublishTaskData(Of PublishTaskClaimData), Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim claimIssueResponseDAL As New ClaimIssueResponseDAL
        Dim claimIsssueStatusDAL As New ClaimIssueStatusDAL

        If Not familyDataset.Tables(Me.TABLE_NAME) Is Nothing Then
            For Each DataRow As DataRow In familyDataset.Tables(Me.TABLE_NAME).Rows
                If (Not String.IsNullOrEmpty(DataRow(Me.COL_NAME_CREATED_BY).ToString())) Then
                    If CType(DataRow(Me.COL_NAME_CREATED_BY), String) = "SYSTEM" Then
                        DataRow(DALBase.COL_NAME_CREATED_BY) = "SYSTEM"
                    End If
                Else
                    DataRow(DALBase.COL_NAME_CREATED_BY) = "SYSTEM"
                End If
            Next
        End If

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            claimIsssueStatusDAL.Update(familyDataset, tr, DataRowState.Deleted)
            claimIssueResponseDAL.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)


            'Second Pass updates additions and changes
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            claimIssueResponseDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If (familyDataset.Tables.Contains(ClaimIssueStatusDAL.TABLE_NAME)) Then
                Dim publishedTaskDal As New PublishedTaskDAL()
                If familyDataset.Tables(ClaimIssueStatusDAL.TABLE_NAME).GetChanges(DataRowState.Added) IsNot Nothing Then
                    For Each dr As DataRow In familyDataset.Tables(ClaimIssueStatusDAL.TABLE_NAME).GetChanges(DataRowState.Added).Rows

                        Dim claimIssueId As Guid = New Guid(DirectCast(dr(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_ID), Byte()))
                        Dim issueId As Guid = Guid.Empty
                        For Each drClaimIssue As DataRow In familyDataset.Tables(ClaimIssueDAL.TABLE_NAME).Rows
                            If (claimIssueId = New Guid(DirectCast(drClaimIssue(ClaimIssueDAL.COL_NAME_ENTITY_ISSUE_ID), Byte()))) Then
                                issueId = New Guid(DirectCast(drClaimIssue(ClaimIssueDAL.COL_NAME_ISSUE_ID), Byte()))
                                Exit For
                            End If
                        Next

                        If (issueId <> Guid.Empty) Then

                            publishedTaskDal.AddEvent(
                             publishEventData.CompanyGroupId,
                             publishEventData.CompanyId,
                             publishEventData.CountryId,
                             publishEventData.DealerId,
                             publishEventData.ProductCode,
                             publishEventData.CoverageTypeId,
                             "ClaimIssue_StatusChange",
                             "ClaimId:" & DALBase.GuidToSQLString(publishEventData.Data.ClaimId),
                             DateTime.UtcNow,
                             publishEventData.Data.EventTypeLookup(New Guid(CType(dr(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_STATUS_C_ID), Byte()))),
                             issueId)

                        End If

                    Next

                    claimIsssueStatusDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
                    If Transaction Is Nothing Then
                        'We are the creator of the transaction we shoul commit it  and close the connection
                        DBHelper.Commit(tr)
                    End If
                End If
            End If

        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
#End Region

End Class

Imports System.Runtime.CompilerServices
''' <summary>
''' Claim Facade is expected to expose Claim System Operations to External System encapsulating the difference between Single Authorization Claim Objects and 
''' Multiple Authorization Claim Objects
''' </summary>
''' <remarks>The class implements Singleton Pattern in order to support future Inheritance Requirements</remarks>
Public NotInheritable Class ClaimFacade

#Region "Fields"
    ''' <summary>
    ''' Thread Syncronization Context for Singleton Object Creation 
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _syncRoot As Object

    ''' <summary>
    ''' Static Instance Variable to store Singleton Instance
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _instance As ClaimFacade
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Private Constructor, restricts creation of new instances of this class by other classes/external callers
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

    ''' <summary>
    ''' Shared Constructor, creates instance object for Thread Syncronization
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub New()
        _syncRoot = New Object()
    End Sub
#End Region

#Region "Properties"
    Public Shared ReadOnly Property Instance As ClaimFacade
        Get
            If (_instance Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_instance Is Nothing) Then
                        _instance = New ClaimFacade()
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property
#End Region

#Region "Methods"

    Public Function GetClaim(Of TClaim As ClaimBase)(claimNumber As String, companyId As Guid, Optional ByVal pDataSet As DataSet = Nothing) As TClaim
        Return GetClaim(Of TClaim)( _
            claimId:=Guid.Empty, _
            claimNumber:=claimNumber, _
            companyId:=companyId, _
            companyCode:=Nothing, _
            dealerCode:=Nothing, _
            dealerId:=Guid.Empty, _
            serialNumber:=Nothing, _
            ds:=pDataSet, _
            mustReload:=False).AsClaim(Of TClaim)()
    End Function

    Public Function GetClaim(Of TClaim As ClaimBase)(claimId As Guid, Optional ByVal pDataSet As DataSet = Nothing, Optional ByVal blnMustReload As Boolean = False) As TClaim
        Return GetClaim(Of TClaim)( _
            claimId:=claimId, _
            claimNumber:=Nothing, _
            companyId:=Guid.Empty, _
            companyCode:=Nothing, _
            dealerCode:=Nothing, _
            dealerId:=Guid.Empty, _
            serialNumber:=Nothing, _
            ds:=pDataSet, _
            mustReload:=blnMustReload).AsClaim(Of TClaim)()
    End Function

    Public Function GetClaimByClaimNumber(Of TClaim As ClaimBase)(companyCode As String, claimNumber As String) As TClaim
        Return GetClaim(Of TClaim)( _
            claimId:=Guid.Empty, _
            claimNumber:=claimNumber, _
            companyId:=Guid.Empty, _
            companyCode:=companyCode, _
            dealerCode:=Nothing, _
            dealerId:=Guid.Empty, _
            serialNumber:=Nothing, _
            ds:=Nothing, _
            mustReload:=False).AsClaim(Of TClaim)()
    End Function

    Public Function GetClaimBySerialNumber(Of TClaim As ClaimBase)(dealerCode As String, serialNumber As String) As TClaim
        Return GetClaim(Of TClaim)( _
            claimId:=Guid.Empty, _
            claimNumber:=Nothing, _
            companyId:=Guid.Empty, _
            companyCode:=Nothing, _
            dealerCode:=dealerCode, _
            dealerId:=Guid.Empty, _
            serialNumber:=serialNumber, _
            ds:=Nothing, _
            mustReload:=False).AsClaim(Of TClaim)()
    End Function

    Public Function GetClaimByDealerCodeandClaimNumber(Of TClaim As ClaimBase)(dealerCode As String, claimNumber As String) As TClaim
        Return GetClaim(Of TClaim)( _
            claimId:=Guid.Empty, _
            claimNumber:=claimNumber, _
            companyId:=Guid.Empty, _
            companyCode:=Nothing, _
            dealerCode:=dealerCode, _
            dealerId:=Guid.Empty, _
            serialNumber:=Nothing, _
            ds:=Nothing, _
            mustReload:=False).AsClaim(Of TClaim)()
    End Function

    ''' <summary>
    ''' Gets Claim Information which includes Claim Type (Single or Muiltiple Auth Claims) and Claim ID
    ''' </summary>
    ''' <param name="claimId">Claim ID of Claim, when not available then send Guid.Enpty</param>
    ''' <param name="claimNumber">Claim Number of Claim, when not available send Nothing</param>
    ''' <param name="companyId">Company ID of Claim, when not available then send Guid.Enpty</param>
    ''' <param name="companyCode">Company Code of Claim, when not available send Nothing</param>
    ''' <param name="dealerCode">Dealer Code of Claim, when not available send Nothing</param>
    ''' <param name="dealerId">Dealer ID of Claim, when not available then send Guid.Enpty</param>
    ''' <param name="serialNumber">Serial Number of Claim, when not available send Nothing</param>
    ''' <param name="ds">Claim Data Set, when not available send Nothing</param>
    ''' <param name="mustReload">This is applicable for only Single Auth Claims. Setting True will force reloading Claim from Database</param>
    ''' <returns>Claim Object casted to Type specifid as generic parameter</returns>
    ''' <remarks>
    ''' The function will perform best when folling value combinations are used 
    ''' 1. ClaimID
    ''' 2. ClaimNumber + CompanyID
    ''' 3. ClaimNumber + Company Code
    ''' 4. ClaimNumber + Dealer ID
    ''' 5. ClaimNumber + Dealer Code
    ''' 6. Serial Number + Dealer ID/Company ID
    ''' 7. Serial Number + Dealer Code/Company Code
    ''' 8. Any other combinations
    ''' </remarks>
    Private Function GetClaim(Of TClaim As ClaimBase)( _
                                  claimId As Guid, _
                                  claimNumber As String, _
                                  companyId As Guid, _
                                  companyCode As String, _
                                  dealerCode As String, _
                                  dealerId As Guid, _
                                  serialNumber As String, _
                                  ds As DataSet,
                                  mustReload As Boolean) As TClaim
        Dim dal As New ClaimDAL
        Try
            Dim claim As ClaimBase
            Dim claimAuthTypeCode As String = String.Empty
            Dim network_id As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            If (Not IsClaimLoaded(claimId, ds)) Then
                Dim claimAuthTypeDetails As ClaimDAL.ClaimAuthTypeDetails = dal.GetClaimInfo(claimId, claimNumber, companyId, companyCode, dealerId, dealerCode, serialNumber, network_id)
                claimAuthTypeCode = LookupListNew.GetCodeFromId(Codes.CLAIM_AUTHORIZATION_TYPE, GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(claimAuthTypeDetails.ClaimAuthTypeId)))
                claimId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(claimAuthTypeDetails.ClaimId))
            Else
                claimAuthTypeCode = GetClaimType(claimId, ds)
            End If

            Dim claimAuthType As ClaimAuthorizationType
            Select Case claimAuthTypeCode
                Case Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE
                    If (ds Is Nothing) Then
                        claim = New Claim(claimId)
                    Else
                        claim = New Claim(claimId, ds, mustReload)
                    End If
                Case Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE
                    If (ds Is Nothing) Then
                        claim = New MultiAuthClaim(claimId)
                    Else
                        claim = New MultiAuthClaim(claimId, ds)
                    End If
                Case Else
                    Throw New DataNotFoundException
            End Select

            Return claim.AsClaim(Of TClaim)()

        Catch ex As StoredProcedureGeneratedException
            Throw New DataNotFoundException
        End Try
    End Function

    <Obsolete("The method is being used for backward compaitibility. Use the function with DealerId for new implementations ")> _
    Public Function CreateClaim(Of TClaim As ClaimBase)(Optional ByVal pDataset As DataSet = Nothing) As TClaim
        Dim claim As ClaimBase = Nothing
        If (pDataset Is Nothing) Then
            claim = New Claim()
        Else
            claim = New Claim(pDataset)
        End If

        claim.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE)
        Return claim.AsClaim(Of TClaim)()
    End Function

    Public Function CreateClaim(Of TClaim As ClaimBase)(dealerId As Guid, Optional ByVal pDataSet As DataSet = Nothing) As TClaim
        Dim claim As ClaimBase = Nothing
        Dim dealer As Dealer = New Dealer(dealerId)
        If (dealer.UseClaimAuthorizationId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)) Then
            If (pDataSet Is Nothing) Then
                claim = New Claim()
            Else
                claim = New Claim(pDataSet)
            End If
            claim.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE)
        Else
            If (pDataSet Is Nothing) Then
                claim = New MultiAuthClaim()
            Else
                claim = New MultiAuthClaim(pDataSet)
            End If
            claim.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE)
        End If

        Return claim.AsClaim(Of TClaim)()

    End Function

    Public Function CreateClaim(Of TClaim As ClaimBase)(originalClaim As Claim, Optional ByVal pDataSet As DataSet = Nothing) As TClaim
        Dim claim As ClaimBase = Nothing
        Select Case originalClaim.ClaimAuthorizationType
            Case ClaimAuthorizationType.Single
                If (pDataSet Is Nothing) Then
                    claim = New Claim()
                Else
                    claim = New Claim(pDataSet)
                End If
                claim.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE)
            Case ClaimAuthorizationType.Multiple
                If (pDataSet Is Nothing) Then
                    claim = New MultiAuthClaim()
                Else
                    claim = New MultiAuthClaim(pDataSet)
                End If
                claim.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE)
            Case ClaimAuthorizationType.None
                Throw New NotSupportedException

        End Select

        Return claim.AsClaim(Of TClaim)()

    End Function

    Private Function IsClaimLoaded(claimId As Guid, dataSet As DataSet) As Boolean
        Dim flag As Boolean = False
        If (Not dataSet Is Nothing) Then
            Dim row As DataRow = FindRow(claimId, dataSet)
            If Not row Is Nothing Then flag = True
        End If
        Return flag
    End Function

    Private Function GetClaimType(claimId As Guid, dataSet As DataSet) As String
        Dim claimAuthTypeCode As String = String.Empty
        Dim row As DataRow = FindRow(claimId, dataSet)
        If (Not row Is Nothing) Then
            Dim claimAuthTypeId As Guid = New Guid(CType(row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID), Byte()))
            claimAuthTypeCode = LookupListNew.GetCodeFromId(Codes.CLAIM_AUTHORIZATION_TYPE, claimAuthTypeId)
        End If
        Return claimAuthTypeCode
    End Function

    Private Function FindRow(claimId As Guid, dataSet As DataSet) As DataRow
        Dim dal As New ClaimDAL
        Dim row As DataRow = Nothing
        If dataSet.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
            row = ClaimBase.FindRow(claimId, dal.TABLE_KEY_NAME, dataSet.Tables(dal.TABLE_NAME))
        End If
        Return row
    End Function

#End Region

End Class

Public Module CLaimExtentions
    <Extension> _
    Public Function AsClaim(Of TClaim As ClaimBase)(oClaim As ClaimBase) As TClaim
        If (GetType(TClaim).IsAssignableFrom(oClaim.GetType())) Then
            Return CType(oClaim, TClaim)
        Else
            Throw New InvalidCastException()
        End If
    End Function
End Module

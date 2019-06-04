Imports System.Runtime.CompilerServices
''' <summary>
''' Claim Facade is expected to expose Claim System Operations to External System encapsulating the difference between Single Authorization Claim Objects and 
''' Multiple Authorization Claim Objects
''' </summary>
''' <remarks>The class implements Singleton Pattern in order to support future Inheritance Requirements</remarks>
Public NotInheritable Class CertificateFacade

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
    Private Shared _instance As CertificateFacade
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
    Public Shared ReadOnly Property Instance As CertificateFacade
        Get
            If (_instance Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_instance Is Nothing) Then
                        _instance = New CertificateFacade()
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property
#End Region

    Public Shared Function GetCertificatebyCertNumber(ByVal CertificateNumber As String, ByVal dealerCode As String, ByVal upgradeFlag As String) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfo(CertificateNumber, dealerCode, Nothing, upgradeFlag, Nothing, Nothing)

    End Function

    Public Shared Function GetCertificatebyAcctNumber(ByVal BillingAccountNumber As String, ByVal dealerCode As String) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfo(Nothing, dealerCode, BillingAccountNumber, Nothing, Nothing, Nothing)

    End Function

    Public Shared Function GetCertificatebyCertNumberAndCompanyCode(ByVal CertificateNumber As String, ByVal CompanyCode As String) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfo(CertificateNumber, Nothing, Nothing, Nothing, CompanyCode, Nothing)

    End Function

    Public Shared Function GetCertificatebySerialNumber(ByVal dealerCode As String, ByVal SerialNumber As String, ByVal upgradeFlag As String) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfoBySerialNumber(dealerCode, SerialNumber, upgradeFlag)

    End Function

    Public Shared Function GetCertAfterUpgrade(ByVal dealerCode As String, ByVal SerialNumber As String, IdentificationNumber As String, UpgradeDate As Date) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfoBySrNrIdentNrUpgrdDate(dealerCode, SerialNumber, IdentificationNumber, UpgradeDate)

    End Function

    Public Shared Function GetCertificatebyMobileNumber(ByVal MobileNumber As String, ByVal dealerCode As String, ByVal upgradeFlag As String) As Guid
        Dim dal As New CertificateDAL

        Return dal.GetCertInfo(Nothing, dealerCode, Nothing, upgradeFlag, Nothing, MobileNumber)

    End Function
End Class

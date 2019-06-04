Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Practices.Unity

Friend Class RuleFactory

    Private Shared ms_instance As RuleFactory
    Private Shared ms_syncRoot As Object = New Object

    Friend Shared ReadOnly Property Current As RuleFactory
        Get
            If ms_instance Is Nothing Then
                SyncLock ms_syncRoot
                    If ms_instance Is Nothing Then
                        ms_instance = New RuleFactory(ApplicationContext.Current.Container.Resolve(Of ICommonManager)())
                    End If
                End SyncLock
            End If
            Return ms_instance
        End Get
    End Property

    Private ReadOnly CommonManager As ICommonManager
    Private ReadOnly CertificateManager As ICertificateManager

    Private Sub New(ByVal pCommonManager As ICommonManager)
        CommonManager = pCommonManager
    End Sub

    Friend Function GetRuleExecutor(ByVal pRule As Rule) As BaseRule
        Select Case pRule.Code.ToUpperInvariant()
            Case RuleCodes.PoliceReportRequired, RuleCodes.TheftDocumentationRules
                Return New PoliceReportRequiredRule(pRule, CommonManager)
            Case RuleCodes.Troubleshooting
                Return New TroubleShootingRule(pRule, CommonManager)
            Case RuleCodes.DeductibleCollection
                Return New DeductibleCollectionRule(pRule, CommonManager)
            Case RuleCodes.ClaimDocumentsRequired
                Return New ClaimDocumentsRequiredRule(pRule, CommonManager)
            Case RuleCodes.Upgrade
                Return New UpgradeRule(pRule, CommonManager)
            Case Else
                Throw New NotSupportedException()
        End Select
    End Function

End Class

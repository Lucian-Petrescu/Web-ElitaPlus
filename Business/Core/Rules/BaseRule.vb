
Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class BaseRule

    Friend ReadOnly Rule As DataEntities.Rule
    Friend ReadOnly CommonManager As ICommonManager

    Protected Sub New(ByVal pRule As DataEntities.Rule, ByVal pCommonManager As CommonManager)
        Me.Rule = pRule
        Me.CommonManager = pCommonManager
    End Sub

End Class

'Friend MustInherit Class BaseRule(Of TType As ISupportsIssues)
'    Inherits BaseRule
'    Friend MustOverride Sub Execute(ByVal pObject As TType)

'    Protected Sub New(ByVal pRule As DataEntities.Rule, ByVal pCommonManager As CommonManager)
'        MyBase.New(pRule, pCommonManager)
'    End Sub

'    Protected Sub ExecuteAction(ByVal pObject As TType)
'        For Each oIssue As DataEntities.Issue In Rule.RuleIssues.Where(Function(ri) ri.IsEffective()).SelectMany(Of Issue)(Function(ris) ris.Issues)
'            pObject.AddIssue(oIssue, Rule)
'        Next
'    End Sub
'End Class

Friend MustInherit Class BaseClaimRule
    Inherits BaseRule
    Friend MustOverride Sub Execute(ByVal pClaim As Claim, pCertificateItemCoverage As CertificateItemCoverage)

    Protected Sub New(ByVal pRule As DataEntities.Rule, ByVal pCommonManager As CommonManager)
        MyBase.New(pRule, pCommonManager)
    End Sub

    Protected Sub ExecuteAction(ByVal pObject As Claim)
        'For Each oIssue As DataEntities.Issue In Rule.RuleIssues.Where(Function(ri) ri.IsEffective()).SelectMany(Of Issue)(Function(ris) ris.Issues)
        '    pObject.AddIssue(oIssue, Rule)
        'Next
        Dim oIssue As DataEntities.Issue = Rule.RuleIssues.Where(Function(ri) ri.IsEffective()).FirstOrDefault.Issues
        pObject.AddIssue(oIssue, Rule)

    End Sub
End Class

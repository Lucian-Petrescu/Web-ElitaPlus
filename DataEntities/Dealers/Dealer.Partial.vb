Imports Assurant.ElitaPlus.DataEntities

Partial Public Class Dealer

    Public Function GetRules() As IList(Of Rule)
        Dim oRules As New List(Of Rule)
        'Return DealerRuleLists.
        '    Where(Function(drl) drl.IsEffective()).
        '    SelectMany(Of Rule)(Function(drls) drls.RuleList.RuleListDetails.
        '    Where(Function(rld) rld.RuleList.IsEffective() AndAlso rld.IsEffective() AndAlso rld.Rule.IsEffective()).
        '    Select(Of Rule)(Function(rlds) rlds.Rule))

        ' Dim trl As RuleList = Me.DealerRuleLists.Where(Function(drl) drl.IsEffective()).SelectMany(Of RuleList)(Function(dlr1) dlr1).FirstOrDefault Then

        For Each srl As DealerRuleList In DealerRuleLists '.Where(Function(r) r.IsEffective).SelectMany(Of RuleList)(Function(t) t.RuleList)
            If (srl.IsEffective) Then

                If (srl.RuleList.IsEffective) Then
                    For Each rld As RuleListDetail In srl.RuleList.RuleListDetails
                        If rld.IsEffective Then
                            If rld.Rule.IsEffective Then
                                oRules.Add(rld.Rule)
                            End If
                        End If
                    Next
                End If
            End If
        Next

        Return oRules


        'Return From s As Rule In trl.RuleListDetails.Where(Function(rld) rld.IsEffective() AndAlso trl.IsEffective()).SelectMany(Of Rule)(Function(srl) srl.Rule)
        'rld1.Where(Function(srld1) srld1.IsEffective).SelectMany(Of Rule)(Function(r) r.Rule)

    End Function

End Class









Imports System.Runtime.CompilerServices

Public Module ExpressionExtensions

    Public Delegate Function GetVariable(variableName As String) As String

    <Extension>
    Public Function Evaluate(expression As BaseExpression, getVariableCallback As GetVariable) As Decimal
        If expression.[GetType]() = GetType(ConstantToken) Then
            Return DirectCast(expression, ConstantToken).Value
        End If

        If expression.[GetType]() = GetType(VariableToken) Then
            Return Decimal.Parse(getVariableCallback(DirectCast(expression, VariableToken).VariableName))
        End If

        If expression.[GetType]() = GetType(SwitchExpression) Then
            Dim sExp As SwitchExpression = DirectCast(expression, SwitchExpression)
            Dim swicthValue As String = getVariableCallback(sExp.VariableName)
            For Each scExp As SwitchCaseExpression In sExp.Case
                If scExp.CaseValue.ToUpperInvariant() = swicthValue.ToUpperInvariant() Then
                    Return scExp.Expression.Evaluate(getVariableCallback)
                End If
            Next

            Return sExp.Otherwise.Evaluate(getVariableCallback)
        End If

        If expression.[GetType]() = GetType(ComplexExpression) Then
            Dim cExp As ComplexExpression = DirectCast(expression, ComplexExpression)

            Select Case cExp.Operation
                Case OperationType.Minimum
                    Return cExp.Expression.[Select](Function(exp) exp.Evaluate(getVariableCallback)).Min()
                Case OperationType.Maximum
                    Return cExp.Expression.[Select](Function(exp) exp.Evaluate(getVariableCallback)).Max()
                Case OperationType.Percentage
                    Return cExp.Expression.Last().Evaluate(getVariableCallback) * cExp.Expression.First().Evaluate(getVariableCallback) / 100
                Case OperationType.Average
                    Return cExp.Expression.[Select](Function(exp) exp.Evaluate(getVariableCallback)).Average()
                Case OperationType.Total
                    Return cExp.Expression.[Select](Function(exp) exp.Evaluate(getVariableCallback)).Sum()
                Case Else
                    Throw New NotSupportedException()
            End Select
        End If

        Throw New NotSupportedException()

    End Function

End Module

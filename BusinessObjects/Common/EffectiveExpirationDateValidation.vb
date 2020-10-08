Imports Assurant.Common.Localization

Public NotInheritable Class EffectiveExpirationDateValidation
    Inherits ValidBaseAttribute
    Implements IValidatorAttribute
    Public Sub New(FieldNamestring As String)
        MyBase.New(FieldNamestring, Messages.INVALID_EXP_DATE)

    End Sub

    Public Overrides Function IsValid(objectToCheck As Object, context As Object) As Boolean

        Dim Iface As IExpirable = DirectCast(context, IExpirable)

        If objectToCheck Is Nothing Then Return True
        Try
            Dim objDate As DateTimeType = DateTime.Parse(CType(objectToCheck, DateTimeType), LocalizationMgr.CurrentFormatProvider)
            If Not objDate Is Nothing And Not Iface.Effective Is Nothing Then
                If Iface.Effective.Value > objDate.Value Then Return False
            End If
        Catch ex As FormatException
            Return False
        End Try

        Return True

    End Function
End Class
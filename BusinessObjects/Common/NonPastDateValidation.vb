Imports Assurant.Common.Localization

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class NonPastDateValidation
    Inherits ValidBaseAttribute
    Implements IValidatorAttribute

    Public Sub New(FieldDisplayName As String)
        MyBase.New(FieldDisplayName, Messages.NO_PAST_DATE)

    End Sub
    Public Overrides Function IsValid(objectToCheck As Object, context As Object) As Boolean

        Dim TodaysDate As DateTime = DateTime.Today

        If objectToCheck Is Nothing Then Return True

        Dim Iface As IExpirable = DirectCast(context, IExpirable)
        Try
            'For New records date must be greater or equal to todays date(with out time)
            If Iface.IsNew Then
                Dim objDate As DateTimeType = DateTime.Parse(CType(objectToCheck, DateTimeType), LocalizationMgr.CurrentFormatProvider)
                If Not objDate Is Nothing Then
                    If objDate.Value.Date < Date.Today.Date Then Return False
                End If
            End If
            'For Old records Expiration date must be greater or equal to todays date(with out time)
            If Iface.IsNew = False And DisplayName = Codes.EXPIRATION Then
                If Iface.Expiration < Date.Today.Date Then Return False
            End If

        Catch ex As FormatException
            Return False
        End Try

        Return True
    End Function
End Class

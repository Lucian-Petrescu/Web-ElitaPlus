Imports Assurant.Common.Localization

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateDuplicateCode
    Inherits ValidBaseAttribute
    Implements IValidatorAttribute
    Public Sub New(ByVal FieldDisplayName As String)
        MyBase.New(FieldDisplayName, Messages.DUPLICATE_CODE_SAME_EFFECTIVE)
    End Sub
    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
        If objectToCheck Is Nothing Then Return True

        'check if the same code and effective date exist if found then its a duplicate
        If Not context.GetType.GetInterface("IExpirable", True) Is Nothing Then
            Dim iface As IExpirable = DirectCast(context, IExpirable)
            If iface.IsNew Then
                Dim ValDupDal As New ValidateDuplicateCodeDAL
                Dim result As DataView = ValDupDal.ValidateDuplicateCode(context.GetType.Name, iface.Effective, iface.Code, iface.ID)
                If result.Count > 0 Then
                    Return False        'duplicate code exists so validation failes
                End If
            End If
            Return True      'if nothing came as result then validation passes also if not a new record then this validation should be skipped
        End If
        Return False
        'if the object does not implement the IExpirable interface then also we return false
    End Function

End Class


<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidIntervalDateAttribute
    Inherits ValidBaseAttribute

#Region "Variables"
    Private moErrMax As String
    Private moErrDelete As String
#End Region

#Region "Constructor"

#End Region
    'Public Sub New(ByVal fieldDisplayName As String)
    '    MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_EFFECTIVE_EXPIRATION_ERR)
    'End Sub

    Public Sub New(fieldDisplayName As String, oErrEffective As String, oErrMax As String, _
    oErrDelete As String)
        MyBase.New(fieldDisplayName, oErrEffective)
        moErrMax = oErrMax
        moErrDelete = oErrDelete
    End Sub

#Region "Validate"

#End Region
    Public Overrides Function IsValid(valueToCheck As Object, oIntervalObject As Object) _
                                                                                                    As Boolean
        Dim bValid As Boolean = True
        Dim oIntervalDate As IValidateIntervalDate = CType(oIntervalObject, IValidateIntervalDate)

        With oIntervalDate
            If .IEffective.Value > .IExpiration.Value Then
                bValid = False
            ElseIf (.IIsNew = True) Then
                ' For inserting
                Dim dtMaxExpiration As Date = .IMaxExpiration.Value
                If (dtMaxExpiration <> GenericConstants.INFINITE_DATE) AndAlso _
                                                (.IEffective.Value <> dtMaxExpiration.AddDays(1)) Then
                    Message = moErrMax
                    bValid = False
                End If
            ElseIf (.IIsDeleted = True) Then
                ' For  deleting
                If .IExpiration.Value <> .IMaxExpiration.Value Then
                    Message = moErrDelete
                    bValid = False
                End If
            End If
        End With
        Return bValid

    End Function


End Class
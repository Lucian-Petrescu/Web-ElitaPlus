Public Class LabelTranslation
    Inherits BaseEntity

    Public Property uiProgCode As String
    Public Property Translation As String
    Public Property LabelId As Guid

    Public Overrides ReadOnly Property Id As Guid
        Get
            Return LabelId
        End Get
    End Property
End Class

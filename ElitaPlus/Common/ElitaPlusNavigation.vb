Public Class ElitaPlusNavigation
    Inherits Navigation

#Region "Constructors"
    Public Sub New()
        MyBase.New()
        MyBase.Parse(ConfigReader.GetNode(Me.GetType, "/NAVIGATION"))
    End Sub
#End Region
End Class

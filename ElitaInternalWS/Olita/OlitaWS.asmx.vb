Imports System.Web.Services.Protocols
Imports Microsoft.Web.Services3
Imports Microsoft.Web.Services3.Security.Tokens
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects


Namespace Olita

    <System.Web.Services.WebService(Namespace:="http://tempuri.org/ElitaInternalWS/Olita/OlitaWS")> _
    Public Class OlitaWS
        Inherits ElitaWebService

#Region " Web Services Designer Generated Code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Web Services Designer.
            InitializeComponent()

            'Add your own initialization code after the InitializeComponent() call

        End Sub

        'Required by the Web Services Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Web Services Designer
        'It can be modified using the Web Services Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
        End Sub

        Protected Overloads Overrides Sub Dispose(disposing As Boolean)
            'CODEGEN: This procedure is required by the Web Services Designer
            'Do not modify it using the code editor.
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region


    End Class

End Namespace
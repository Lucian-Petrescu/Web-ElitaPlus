<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.HarvesterServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.HarvesterServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'HarvesterServiceProcessInstaller
        '
        Me.HarvesterServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.HarvesterServiceProcessInstaller.Password = Nothing
        Me.HarvesterServiceProcessInstaller.Username = Nothing
        '
        'HarvesterServiceInstaller
        '
        Me.HarvesterServiceInstaller.Description = "Elita Event Harvester Service"
        Me.HarvesterServiceInstaller.DisplayName = "Elita Harvester Service"
        Me.HarvesterServiceInstaller.ServiceName = "ElitaHarvesterService"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.HarvesterServiceProcessInstaller, Me.HarvesterServiceInstaller})

    End Sub
    Private WithEvents HarvesterServiceInstaller As System.ServiceProcess.ServiceInstaller
    Private WithEvents HarvesterServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller

End Class

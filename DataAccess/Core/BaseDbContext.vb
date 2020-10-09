Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Objects
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class BaseDbContext
    Inherits DbContext


    Private ReadOnly ObjectContext As ObjectContext

    Private Const CONNECTION_STRING_TEMPLATE As String = "metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider={1};provider connection string=""{2}"""
    Public Sub New(ByVal modelNamespace As String)
        MyBase.New(String.Format(CONNECTION_STRING_TEMPLATE, modelNamespace, GetProviderString(), GetConnectionString()))

        Initialize()

        ObjectContext = DirectCast(Me, IObjectContextAdapter).ObjectContext

        AddHandler ObjectContext.SavingChanges, AddressOf UpdateAuditInformation


    End Sub

    Private Sub UpdateAuditInformation(ByVal sender As Object, ByVal e As EventArgs)

        Dim objectStateEnties As IEnumerable(Of ObjectStateEntry)
        objectStateEnties = ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added Or EntityState.Modified)

        If ((objectStateEnties IsNot Nothing) AndAlso (objectStateEnties.Any)) Then

            Dim networkId As String = System.Threading.Thread.CurrentPrincipal.GetNetworkId()

            For Each ose As ObjectStateEntry In objectStateEnties

                If (GetType(IRecordCreateModifyInfo).IsAssignableFrom(ose.Entity.GetType())) Then
                    Dim entity As IRecordCreateModifyInfo = DirectCast(ose.Entity, IRecordCreateModifyInfo)
                    If (ose.State = EntityState.Added) Then
                        entity.CreatedDate = DateTime.Now
                        entity.CreatedBy = networkId
                    End If
                    If (ose.State = EntityState.Modified) Then
                        entity.ModifiedDate = DateTime.Now
                        entity.ModifiedBy = networkId
                    End If
                End If
            Next
        End If
    End Sub

    Private Shared Function GetConnectionString() As String
        ''Return "data source=ElitaPlus_DEV;password=elidev;persist security info=True;user id=elita"
        ''Return "DATA SOURCE=atl0loran113.atl0.assurant.com:1522/v4d4elp;PASSWORD=elidev;USER ID=ELITA"
        ''Return "Validate Connection=true;User ID=elita;Password=elidev;Data Source=ElitaPlus_Dev"
        Return Common.AppConfig.DataBase.ConnectionString()
    End Function

    Private Shared Function GetProviderString() As String
        Return "Oracle.ManagedDataAccess.Client"
    End Function

    Private Sub Initialize()
        Configuration.ProxyCreationEnabled = False
        Configuration.ValidateOnSaveEnabled = False
    End Sub
End Class

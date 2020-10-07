Imports System.Linq.Expressions
Imports System.Data.Entity
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Threading
Imports Assurant.ElitaPlus.Security

Public MustInherit Class Repository(Of TEntity As {BaseEntity, IRecordCreateModifyInfo}, TContext As BaseDbContext)
    Implements IRepository(Of TEntity), IDisposable

    Private m_context As Lazy(Of TContext)
    Private m_dbSet As DbSet(Of TEntity)

    Public Sub New(context As Lazy(Of TContext))
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        m_context = context
    End Sub

    Protected ReadOnly Property Context() As TContext
        Get
            Return m_context.Value
        End Get
    End Property

    Private ReadOnly Property DbSet() As DbSet(Of TEntity)
        Get
            If m_dbSet Is Nothing Then
                m_dbSet = Context.[Set](Of TEntity)()
            End If
            Return m_dbSet
        End Get
    End Property

    Public Function [Get](
                         Optional filter As Expression(Of Func(Of TEntity, Boolean)) = Nothing,
                         Optional orderBy As Func(Of IQueryable(Of TEntity), IOrderedQueryable(Of TEntity)) = Nothing,
                         Optional includeProperties As String = "") As IEnumerable(Of TEntity) Implements IRepository(Of TEntity).Get
        Dim query As IQueryable(Of TEntity) = Context.[Set](Of TEntity)()

        If filter IsNot Nothing Then
            query = query.Where(filter)
        End If

        For Each includeProperty As String In includeProperties.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
            query = query.Include(includeProperty)
        Next

        If orderBy IsNot Nothing Then
            Return orderBy(query).ToList()
        Else
            Return query.ToList()
        End If
    End Function

    Public Function GetById(id As Guid) As TEntity Implements IRepository(Of TEntity).GetById
        Dim entity As TEntity
        Using New TransactionScope(TransactionScopeOption.Suppress)
            entity = DbSet.Find(id)
        End Using

        Return entity
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Sub Dispose(disposing As Boolean)
        If disposing Then
            Context.Dispose()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Sub Delete(id As Guid) Implements IRepository(Of TEntity).Delete
        Try
            Dim entityToDelete As TEntity = DbSet.Find(id)
            Delete(entityToDelete)
        Catch ex As DbUpdateException
            Throw TryCast(ex.InnerException, DataException)
        End Try
    End Sub

    Protected Sub Delete(entity As TEntity)
        If Context.Entry(entity).State = EntityState.Detached Then
            DbSet.Attach(entity)
        End If
        DbSet.Remove(entity)

        Save()
    End Sub

    ''' <summary>
    ''' Saves entity to context and updates database.
    ''' </summary>
    ''' <param name="entity">Entity to be saved (Inserted/Updated)</param>
    ''' <param name="stageChanges">When true changes are applied to datastore immediately, when false changes are applied when overload of Save method with no parameters is called</param>
    Public Sub Save(entity As TEntity, Optional stageChanges As Boolean = False) Implements IRepository(Of TEntity).Save
        Try
            Context.Configuration.ValidateOnSaveEnabled = False

            Dim dbEntity = GetById(entity.Id)

            If dbEntity Is Nothing Then
                Insert(entity)
            Else
                Update(entity)
            End If

            If Not stageChanges Then
                Save()
            End If
        Catch ex As DbUpdateException
            Throw TryCast(ex.InnerException, DataException)
        End Try
    End Sub

    Private Sub SetCreateModifyInfo(record As IRecordCreateModifyInfo, state As EntityState)
        If state = EntityState.Added Then
            record.CreatedBy = Thread.CurrentPrincipal.GetNetworkId()
            record.CreatedDate = DateTime.Now
            record.ModifiedBy = Nothing
            record.ModifiedDate = Nothing
        End If

        If state = EntityState.Modified Then
            record.ModifiedBy = Thread.CurrentPrincipal.GetNetworkId()
            record.ModifiedDate = DateTime.Now
        End If
    End Sub

    Protected Sub Insert(entity As TEntity)
        SetCreateModifyInfo(entity, EntityState.Added)
        DbSet.Add(entity)
    End Sub

    Protected Sub Update(Of TEntityType As {BaseEntity, IRecordCreateModifyInfo})(entity As TEntityType)
        Dim dbEntity = Context.[Set](Of TEntityType)().Find(entity.Id)

        entity.CreatedBy = dbEntity.CreatedBy
        entity.CreatedDate = dbEntity.CreatedDate
        SetCreateModifyInfo(entity, EntityState.Modified)

        Context.Entry(dbEntity).CurrentValues.SetValues(entity)
    End Sub

    Protected Sub Update(Of TChildEntity As {BaseEntity, IRecordCreateModifyInfo})(incomingEntities As ICollection(Of TChildEntity), databaseEntities As ICollection(Of TChildEntity))
        Dim notDeletedIds As New List(Of Guid)()

        For Each item As TChildEntity In incomingEntities
            If databaseEntities.[Select](Function(x) x.Id).Contains(item.Id) Then
                ' Modify
                Update(Of TChildEntity)(item)
            Else
                ' Add
                SetCreateModifyInfo(item, EntityState.Added)
                Context.[Set](Of TChildEntity)().Add(item)
            End If

            notDeletedIds.Add(item.Id)
        Next

        Dim childEntities = databaseEntities.Where(Function(x) Not notDeletedIds.Contains(x.Id)).ToArray()

        For i As Integer = childEntities.Count() - 1 To 0 Step -1

            Context.[Set](Of TChildEntity)().Remove(childEntities(i))
        Next

    End Sub

    Protected Overridable Sub Update(entity As TEntity)
        Update(Of TEntity)(entity)
    End Sub

    ''' <summary>
    ''' Commits Staged changes to Data Source
    ''' </summary>
    Public Sub Save() Implements IRepository(Of TEntity).Save
        Context.SaveChanges()
    End Sub
End Class
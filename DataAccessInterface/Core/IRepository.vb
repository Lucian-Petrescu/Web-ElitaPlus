Imports System.Linq.Expressions
Imports Assurant.ElitaPlus.DataEntities

''' <summary>
''' Defines a method to implement basic Repository
''' </summary>
''' <typeparam name="TEntity">The type of the Entity being implemented by this Repository</typeparam>
Public Interface IRepository(Of TEntity As BaseEntity)
    Inherits IDisposable
    ''' <summary>
    ''' Gets <see cref="IEnumerable"/> of Entites from Data Source.
    ''' </summary>
    ''' <param name="filter">Linq Expression for Filtering</param>
    ''' <param name="orderBy">Sort Expression</param>
    ''' <param name="includeProperties">List of Properties for Eager Loading seperated by comma</param>
    ''' <returns><see cref="IEnumerable"/> of Entities</returns>
    Function [Get](
                  Optional filter As Expression(Of Func(Of TEntity, Boolean)) = Nothing,
                  Optional orderBy As Func(Of IQueryable(Of TEntity), IOrderedQueryable(Of TEntity)) = Nothing,
                  Optional includeProperties As String = "") As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Gets Single Entity by Primary Key
    ''' </summary>
    ''' <param name="id">Primary Key of Entity</param>
    ''' <returns>Instance of Entity when found, null otherwise</returns>
    Function GetById(id As Guid) As TEntity

    ''' <summary>
    ''' Marks Entity represented by ID for deletion from Data Source
    ''' </summary>
    ''' <param name="id">Primary Key of Entity</param>
    Sub Delete(id As Guid)

    ''' <summary>
    ''' Saves entity to context and updates database.
    ''' </summary>
    ''' <param name="entity">Entity to be saved (Inserted/Updated)</param>
    ''' <param name="stageChanges">When true changes are applied to datastore immediately, when false changes are applied when overload of Save method with no parameters is called</param>
    Sub Save(entity As TEntity, Optional stageChanges As Boolean = False)

    ''' <summary>
    ''' Commits Staged changes to Data Source
    ''' </summary>
    Sub Save()

End Interface

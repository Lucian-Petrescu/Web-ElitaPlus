Imports System.Collections.Generic
Public Interface IFileLoadHeaderWork
    ReadOnly Property Id As Guid
    Property Received As LongType
    Property Counted As LongType
    Property Validated As LongType
    Property Rejected As LongType
    Property Bypassed As LongType
    Property Loaded As LongType
    ReadOnly Property Children As IEnumerable(Of IFileLoadReconWork)
    Property FileName As String
    ReadOnly Property FamilyDataSet As DataSet
    Sub Save()
End Interface

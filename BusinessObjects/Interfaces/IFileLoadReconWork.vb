Public Interface IFileLoadReconWork
    ReadOnly Property Id As Guid
    ReadOnly Property ParentId As Guid
    Property Loaded As String
    Property RecordType As String
    Property RejectCode As String
    Property RejectReason As String
    ReadOnly Property FamilyDataSet As DataSet
    Sub Save()
End Interface

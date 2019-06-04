#Region "IsContactChildrenList"

Public Class IsContactChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(VendorContact), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, VendorContact).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsContactChildrenList)) Then
                Dim dal As New VendorContactDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IsContactChildrenList))
            End If
            Return parent.Dataset.Tables(VendorContactDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

Public Class IsContactInfoChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As VendorContact)
        MyBase.New(LoadTable(parent), GetType(ContactInfo), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ContactInfo).Id.Equals(CType(Parent, VendorContact).ContactInfoId)
    End Function

    Private Shared Function LoadTable(ByVal parent As VendorContact) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsContactInfoChildrenList)) Then
                Dim dal As New ContactInfoDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IsContactInfoChildrenList))
            End If
            Return parent.Dataset.Tables(ContactInfoDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

Public Class IsAddressChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ContactInfo)
        MyBase.New(LoadTable(parent), GetType(Address), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, Address).Id.Equals(CType(Parent, ContactInfo).AddressId)
    End Function

    Private Shared Function LoadTable(ByVal parent As ContactInfo) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsAddressChildrenList)) Then
                Dim dal As New AddressDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IsAddressChildrenList))
            End If
            Return parent.Dataset.Tables(AddressDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class


#End Region

#Region "IsQuantityChildrenList"

Public Class IsQuantityChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(VendorQuantity), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, VendorQuantity).ReferenceId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsQuantityChildrenList)) Then
                Dim dal As New VendorQuantityDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IsQuantityChildrenList))
            End If
            Return parent.Dataset.Tables(VendorQuantityDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#End Region


#Region "IsScheduleChildrenList"

Public Class IsScheduleChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServiceSchedule), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceSchedule).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsScheduleChildrenList)) Then
                Dim dal As New ServiceScheduleDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IsScheduleChildrenList))
            End If
            Return parent.Dataset.Tables(ServiceScheduleDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

Public Class IsScheduleTableChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceSchedule)
        MyBase.New(LoadTable(parent), GetType(schedule), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, Schedule).Id.Equals(CType(Parent, ServiceSchedule).ScheduleId)
    End Function

    Private Shared Function LoadTable(ByVal parent As ServiceSchedule) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsScheduleChildrenList)) Then
                Dim dal As New ScheduleDAL
                dal.LoadList(parent.Dataset, parent.ServiceCenterId)
                parent.AddChildrenCollection(GetType(IsScheduleChildrenList))
            End If
            Return parent.Dataset.Tables(ScheduleDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

Public Class IsScheduleDetailChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As Schedule, ByVal objParent As ServiceSchedule)
        MyBase.New(LoadTable(parent, objParent), GetType(ScheduleDetail), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ScheduleDetail).ScheduleId.Equals(CType(Parent, Schedule).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As Schedule, ByVal objParent As ServiceSchedule) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IsScheduleDetailChildrenList)) Then
                Dim dal As New ScheduleDetailDAL
                dal.LoadList(parent.Dataset, parent.Id, objParent.Id)
                parent.AddChildrenCollection(GetType(IsScheduleDetailChildrenList))
            End If
            Return parent.Dataset.Tables(ScheduleDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#End Region


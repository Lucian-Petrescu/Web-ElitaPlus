'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/27/2004)  ********************

Public Class ServiceGroupRiskType
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub


    'Exiting or not BO using parent keys attaching to a BO family
    Public Sub New(serviceGroupId As Guid, riskTypeId As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(serviceGroupId, riskTypeId)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New ServiceGroupRiskTypeDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ServiceGroupRiskTypeDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Protected Sub Load(serviceGroupId As Guid, riskTypeId As Guid)
        'try to find it thru a sequential search
        Dim row As DataRow
        For Each row In Dataset.Tables(ServiceGroupDAL.TABLE_NAME).Rows
            Dim obj As New ServiceGroupRiskType(row)
            If obj.ServiceGroupId.Equals(serviceGroupId) AndAlso obj.RiskTypeId.Equals(riskTypeId) Then
                Me.Row = row
            End If
        Next
        If Me.Row Is Nothing Then
            Me.Row = Dataset.Tables(ServiceGroupDAL.TABLE_NAME).NewRow
            Dataset.Tables(ServiceGroupDAL.TABLE_NAME).Rows.Add(Me.Row)
        End If
        Dim ht As New Hashtable
    End Sub

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ServiceGroupRiskTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupRiskTypeDAL.COL_NAME_SERVICE_GROUP_RISK_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceGroupId As Guid
        Get
            CheckDeleted()
            If Row(ServiceGroupRiskTypeDAL.COL_NAME_SERVICE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupRiskTypeDAL.COL_NAME_SERVICE_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceGroupRiskTypeDAL.COL_NAME_SERVICE_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_ID, Value)
            'Set RiskType Description
            Dim dv As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim riskTypeDesc As String = LookupListNew.GetDescriptionFromId(dv, Value)
            SetValue(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_DESCRIPTION, riskTypeDesc)
        End Set
    End Property


    Public ReadOnly Property RiskTypeDescription As String
        Get
            CheckDeleted()
            If Row(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceGroupRiskTypeDAL.COL_NAME_RISK_TYPE_DESCRIPTION), String)
            End If
        End Get
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceGroupRiskTypeDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property



#End Region

#Region "Children Related"

    Public ReadOnly Property SgRtManufacturerChildren As sgRtManufacturerList
        Get
            Return New SgRtManufacturerList(Me)
        End Get
    End Property

    Public Sub UpdateManufaturers(selectedManufacturerGuidStrCollection As Hashtable)
        If selectedManufacturerGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As SgRtManufacturer
            For Each bo In SgRtManufacturerChildren
                If Not selectedManufacturerGuidStrCollection.Contains(bo.ManufacturerId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedManufacturerGuidStrCollection
                If SgRtManufacturerChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim sgrtMan As SgRtManufacturer = SgRtManufacturerChildren.GetNewChild()
                    sgrtMan.ManufacturerId = New Guid(entry.Key.ToString)
                    sgrtMan.ServiceGroupRiskTypeId = Id
                    sgrtMan.Save()
                End If
            Next
        End If
    End Sub

    Public Sub AttachManufaturers(selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRtManIdStr As String
        For Each sgRtManIdStr In selectedManufacturerGuidStrCollection
            Dim sgRtMan As SgRtManufacturer = SgRtManufacturerChildren.GetNewChild
            sgRtMan.ManufacturerId = New Guid(sgRtManIdStr)
            sgRtMan.ServiceGroupRiskTypeId = Id
            sgRtMan.Save()
        Next
    End Sub

    Public Sub DetachManufaturers(selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRtManIdStr As String
        For Each sgRtManIdStr In selectedManufacturerGuidStrCollection
            Dim sgRtMan As SgRtManufacturer = SgRtManufacturerChildren.Find(New Guid(sgRtManIdStr))
            sgRtMan.Delete()
            sgRtMan.Save()
        Next
    End Sub

    Public Function GetAvailableManufacturers() As DataView
        Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetManufacturersLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedManufacturers() As DataView
        Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetManufacturersLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetManufacturersLookupListSelectedSequenceFilter(dv As DataView, isFilterInclusive As Boolean) As String
        Dim sgRtMan As SgRtManufacturer
        Dim inClause As String = "(-1"
        For Each sgRtMan In SgRtManufacturerChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, sgRtMan.ManufacturerId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter
    End Function

    Public Function IsAssociatedForAnyManufacturer() As Boolean
        Return SgRtManufacturerChildren.Count = 0
    End Function

#End Region


#Region "DataView Retrieveing Methods"

#End Region

End Class




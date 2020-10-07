'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/8/2008)  ********************

Public Class DeniedClaims
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
            Dim dal As New DeniedClaimsDAL
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
            Dim dal As New DeniedClaimsDAL
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
            
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
#Region "Children Related"

   

    Public Sub AttachDeniedReason(selectedServiceCenterGuidStrCollection As ArrayList)

        Dim denResonIdStr As String
        Dim pInfo As Reflection.PropertyInfo
        Dim count As Integer

        For Each denResonIdStr In selectedServiceCenterGuidStrCollection
            count = count + 1
            If Not String.IsNullOrEmpty(denResonIdStr) Then
                pInfo = [GetType].GetProperty("DeniedReason" & count & "Id")
                pInfo.SetValue(Me, New Guid(denResonIdStr), Nothing)
            End If
        Next

    End Sub
    Public Shared Function GetRelatedLetters(scClaimResonId As Guid, scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        ds = cpDAL.LoadLetterList(scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetAvailableDRs(scClaimResonId As Guid, scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadAvailableDRs(ds, scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetSelectedDRs(scClaimResonId As Guid, scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadSelectedDRs(ds, scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetAuthorizeApprover(scClaimResonId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadAuthorizedApprover(ds, scClaimResonId)
        Return ds
    End Function
    Public Sub DetachDeniedReason(selectedServiceCenterGuidStrCollection As ArrayList)
        Dim routeSrvIdStr As String
        For Each routeSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim routeSrvBO As ServiceCenter = New ServiceCenter(New Guid(routeSrvIdStr), Dataset)
            routeSrvBO.RouteId = Guid.Empty
            routeSrvBO.Save()
        Next
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
            If Row(DeniedClaimsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_CLAIMS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property CustomerName As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CITY, Value)
        End Set
    End Property



    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property



    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=480)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DeniedReason1Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem1 As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem2 As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem3 As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ApproverId As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_APPROVER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_APPROVER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_APPROVER_ID, Value)
        End Set
    End Property



    Public Property DeniedReason2Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID, Value)
        End Set
    End Property



    Public Property DeniedReason3Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID, Value)
        End Set
    End Property



    Public Property DeniedReason4Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID, Value)
        End Set
    End Property



    Public Property DeniedReason5Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID, Value)
        End Set
    End Property



    Public Property DeniedReason6Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID, Value)
        End Set
    End Property



    Public Property DeniedReason7Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID, Value)
        End Set
    End Property



    Public Property DeniedReason8Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID, Value)
        End Set
    End Property



    Public Property DeniedReason9Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID, Value)
        End Set
    End Property



    Public Property DeniedReason10Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID, Value)
        End Set
    End Property



    Public Property DeniedReason11Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID, Value)
        End Set
    End Property



    Public Property DeniedReason12Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID, Value)
        End Set
    End Property



    Public Property DeniedReason13Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID, Value)
        End Set
    End Property



    Public Property DeniedReason14Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID, Value)
        End Set
    End Property



    Public Property DeniedReason15Id As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DeniedClaimsDAL

                'dal.Update(Me.Row)
                dal.Update(Dataset)

                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

   

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class




'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/8/2008)  ********************

Public Class DeniedClaims
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DeniedClaimsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New DeniedClaimsDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
#Region "Children Related"

   

    Public Sub AttachDeniedReason(ByVal selectedServiceCenterGuidStrCollection As ArrayList)

        Dim denResonIdStr As String
        Dim pInfo As Reflection.PropertyInfo
        Dim count As Integer

        For Each denResonIdStr In selectedServiceCenterGuidStrCollection
            count = count + 1
            If Not String.IsNullOrEmpty(denResonIdStr) Then
                pInfo = Me.GetType.GetProperty("DeniedReason" & count & "Id")
                pInfo.SetValue(Me, New Guid(denResonIdStr), Nothing)
            End If
        Next

    End Sub
    Public Shared Function GetRelatedLetters(ByVal scClaimResonId As Guid, ByVal scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        ds = cpDAL.LoadLetterList(scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetAvailableDRs(ByVal scClaimResonId As Guid, ByVal scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadAvailableDRs(ds, scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetSelectedDRs(ByVal scClaimResonId As Guid, ByVal scLangID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadSelectedDRs(ds, scClaimResonId, scLangID)
        Return ds
    End Function
    Public Shared Function GetAuthorizeApprover(ByVal scClaimResonId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As DeniedClaimsDAL = New DeniedClaimsDAL
        cpDAL.LoadAuthorizedApprover(ds, scClaimResonId)
        Return ds
    End Function
    Public Sub DetachDeniedReason(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim routeSrvIdStr As String
        For Each routeSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim routeSrvBO As ServiceCenter = New ServiceCenter(New Guid(routeSrvIdStr), Me.Dataset)
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(DeniedClaimsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_CLAIMS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CITY, Value)
        End Set
    End Property



    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property



    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=480)> _
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DeniedReason1Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem1() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem2() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property ConditionProblem3() As String
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_CONDITION_PROBLEM_3, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ApproverId() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_APPROVER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_APPROVER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_APPROVER_ID, Value)
        End Set
    End Property



    Public Property DeniedReason2Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON2_ID, Value)
        End Set
    End Property



    Public Property DeniedReason3Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON3_ID, Value)
        End Set
    End Property



    Public Property DeniedReason4Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON4_ID, Value)
        End Set
    End Property



    Public Property DeniedReason5Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON5_ID, Value)
        End Set
    End Property



    Public Property DeniedReason6Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON6_ID, Value)
        End Set
    End Property



    Public Property DeniedReason7Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON7_ID, Value)
        End Set
    End Property



    Public Property DeniedReason8Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON8_ID, Value)
        End Set
    End Property



    Public Property DeniedReason9Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON9_ID, Value)
        End Set
    End Property



    Public Property DeniedReason10Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON10_ID, Value)
        End Set
    End Property



    Public Property DeniedReason11Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON11_ID, Value)
        End Set
    End Property



    Public Property DeniedReason12Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON12_ID, Value)
        End Set
    End Property



    Public Property DeniedReason13Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON13_ID, Value)
        End Set
    End Property



    Public Property DeniedReason14Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON14_ID, Value)
        End Set
    End Property



    Public Property DeniedReason15Id() As Guid
        Get
            CheckDeleted()
            If Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DeniedClaimsDAL.COL_NAME_DENIED_REASON15_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DeniedClaimsDAL

                'dal.Update(Me.Row)
                dal.Update(Me.Dataset)

                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
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




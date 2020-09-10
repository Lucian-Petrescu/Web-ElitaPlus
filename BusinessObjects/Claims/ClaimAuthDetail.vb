'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/23/2007)  ********************

Public Class ClaimAuthDetail
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid, Optional ByVal blnLoadByClaimID As Boolean = False)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id, blnLoadByClaimID)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
        Me.Initialize()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal blnLoadByClaimID As Boolean = False, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id, blnLoadByClaimID, blnMustReload)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(blnMustReload)
        Me.Initialize()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load(Optional ByVal blnMustReload As Boolean = False)
        Try
            Dim dal As New ClaimAuthDetailDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Me.Row = Nothing
            If blnMustReload AndAlso Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Dim keyColName As String = dal.TABLE_KEY_NAME
                Me.Row = Me.FindRow(Id, keyColName, Me.Dataset.Tables(dal.TABLE_NAME))
                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid, ByVal blnLoadByClaimID As Boolean, Optional ByVal blnMustReload As Boolean = False)
        Try
            Dim dal As New ClaimAuthDetailDAL
            Dim keyColName As String = dal.TABLE_KEY_NAME

            If blnLoadByClaimID Then keyColName = dal.COL_NAME_CLAIM_ID

            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If

            Me.Row = Nothing
            If blnMustReload AndAlso Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, keyColName, Me.Dataset.Tables(dal.TABLE_NAME))
                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                Me.Row = Me.FindRow(id, keyColName, Me.Dataset.Tables(dal.TABLE_NAME))
            End If

            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id, blnLoadByClaimID)
                Me.Row = Me.FindRow(id, keyColName, Me.Dataset.Tables(dal.TABLE_NAME))
            End If

            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"

    Private _claimId As Guid

    'Initialization code for new objects
    Private Sub Initialize()
        Me.LaborAmount = 0
        Me.PartAmount = 0
        Me.PartAmount = 0
        Me.ServiceCharge = 0
        Me.TripAmount = 0
        Me.OtherAmount = 0

    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ClaimAuthDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthDetailDAL.COL_NAME_CLAIM_AUTH_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthDetailDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthDetailDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property LaborAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_LABOR_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_LABOR_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property PartAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_PART_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property ServiceCharge() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_SERVICE_CHARGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_SERVICE_CHARGE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_SERVICE_CHARGE, Value)
        End Set
    End Property


    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property TripAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_TRIP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property ShippingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatoryConditionally(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property OtherAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_OTHER_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property OtherExplanation() As String
        Get
            CheckDeleted()
            If row(ClaimAuthDetailDAL.COL_NAME_OTHER_EXPLANATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthDetailDAL.COL_NAME_OTHER_EXPLANATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_OTHER_EXPLANATION, Value)
        End Set
    End Property

    Public Property TempClaimId() As Guid
        Get
            Return _claimId
        End Get
        Set(ByVal Value As Guid)
            _claimId = Value
        End Set
    End Property

    Public Property DispositionAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_DISPOSITION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_DISPOSITION_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_DISPOSITION_AMOUNT, Value)
        End Set
    End Property


    Public Property DiagnosticsAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthDetailDAL.COL_NAME_DIAGNOSTICS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_DIAGNOSTICS_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_DIAGNOSTICS_AMOUNT, Value)
        End Set
    End Property

    Public Property TotalTaxAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimAuthDetailDAL.COL_NAME_TOTAL_TAX_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimAuthDetailDAL.COL_NAME_TOTAL_TAX_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimAuthDetailDAL.COL_NAME_TOTAL_TAX_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimAuthDetailDAL.COL_NAME_TOTAL_TAX_AMOUNT, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    'Public Overrides ReadOnly Property IsDirty() As Boolean
    '    Get
    '        Return MyBase.IsFamilyDirty()
    '    End Get
    'End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            Me.UpdateClaimAuthorizationAmount()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimAuthDetailDAL
                dal.UpdateFamily(Me.Dataset)
                ' Trigger extended status
                Dim claim As ClaimBase = AddClaim(Me.ClaimId)
                If ((Not claim.LatestClaimStatus Is Nothing) AndAlso (claim.ClaimStatusesCount > 0)) Then
                    If claim.LatestClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(Codes.CLAIM_EXTENDED_STATUS__BUDGET_APPROVED) Then
                        With claim
                            PublishedTask.AddEvent(companyGroupId:=.Company.CompanyGroupId, _
                                                   companyId:=.CompanyId, _
                                                   countryId:=.Company.CountryId, _
                                                   dealerId:=.Dealer.Id, _
                                                   productCode:=.Certificate.ProductCode, _
                                                   coverageTypeId:=.CertificateItemCoverage.CoverageTypeId, _
                                                   sender:="Claim Auth Details", _
                                                   arguments:="ClaimId:" & DALBase.GuidToSQLString(Me.ClaimId), _
                                                   eventDate:=DateTime.UtcNow, _
                                                   eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ALL_ISSUES_RESOLVED_BUDGET_APPROVED), _
                                                   eventArgumentId:=Nothing)
                        End With
                    End If
                End If
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId, False)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function AddTransactionLogHeader(ByVal tranLogHeaderId As Guid) As TransactionLogHeader
        Dim objTranLogHeader As TransactionLogHeader

        If Not tranLogHeaderId.Equals(Guid.Empty) Then
            objTranLogHeader = New TransactionLogHeader(tranLogHeaderId, Me.Dataset)
        Else
            objTranLogHeader = New TransactionLogHeader(Me.Dataset)
        End If

        Return objTranLogHeader
    End Function

    'Public Shared Sub DeleteNewChildClaimAuthDetail(ByVal parentClaim As Claim)
    '    Dim row As DataRow
    '    If parentClaim.Dataset.Tables.IndexOf(ClaimAuthDetailDAL.TABLE_NAME) >= 0 Then
    '        Dim rowIndex As Integer
    '        For rowIndex = 0 To parentClaim.Dataset.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows.Count - 1
    '            row = parentClaim.Dataset.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows.Item(rowIndex)
    '            If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
    '                Dim cad As ClaimAuthDetail = New ClaimAuthDetail(row)
    '                If cad.ClaimId.Equals(parentClaim.Id) And cad.IsNew Then
    '                    cad.Delete()
    '                End If
    '            End If
    '        Next

    '    End If
    'End Sub

#End Region

#Region "Private methods"
    Private Sub UpdateClaimAuthorizationAmount()
        Dim objClaim As ClaimBase
        Dim subTotal As Decimal = 0
        Dim tax As Decimal = 0
        Dim Total As Decimal
        Dim oCompany As Company = Nothing


        If Not Me.IsDeleted Then
            objClaim = Me.AddClaim(Me.ClaimId)
            If Not LaborAmount Is Nothing Then subTotal += LaborAmount.Value
            If Not PartAmount Is Nothing Then subTotal += PartAmount.Value
            If Not ServiceCharge Is Nothing Then subTotal += ServiceCharge.Value
            If Not TripAmount Is Nothing Then subTotal += TripAmount.Value
            If Not OtherAmount Is Nothing Then subTotal += OtherAmount.Value
            If Not ShippingAmount Is Nothing Then subTotal += ShippingAmount.Value
            If Not DispositionAmount Is Nothing Then subTotal += DispositionAmount.Value
            If Not DiagnosticsAmount Is Nothing Then subTotal += DiagnosticsAmount.Value
            If Not TotalTaxAmount Is Nothing Then tax += TotalTaxAmount.Value
        Else
            objClaim = Me.AddClaim(Me.TempClaimId)
        End If

        oCompany = New Company(objClaim.CompanyId)
        If Not oCompany Is Nothing And oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
            Total = subTotal + tax
            DirectCast(objClaim, Claim).AuthorizedAmount = Total
        End If

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Children"
    Public Function AddPartsInfo(ByVal partInfoID As Guid) As PartsInfo
        If partInfoID.Equals(Guid.Empty) Then
            Dim objPartsInfo As New PartsInfo(Me.Dataset)
            Return objPartsInfo
        Else
            Dim objPartsInfo As New PartsInfo(partInfoID, Me.Dataset)
            Return objPartsInfo
        End If
    End Function

    Public Function AddClaimStatus(ByVal claimStatusID As Guid) As ClaimStatus
        If claimStatusID.Equals(Guid.Empty) Then
            Dim objClaimStatus As New ClaimStatus(Me.Dataset)
            Return objClaimStatus
        Else
            Dim objClaimStatus As New ClaimStatus(claimStatusID, Me.Dataset)
            Return objClaimStatus
        End If
    End Function
    Private Function AddClaim(ByVal claimID As Guid) As ClaimBase
        Dim objClaim As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimID, Me.Dataset)
        Return objClaim
    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.AT_LEAST_ONE_AMOUNT_IS_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthDetail = CType(objectToValidate, ClaimAuthDetail)
            Dim oClaim As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(obj.ClaimId, obj.Dataset)
            If (oClaim.Company.AuthDetailRqrdId <> LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, Codes.AUTHORIZATION_DETAIL__REQUIRED)) Then
                Return True
            End If

            If Not obj.PartAmount Is Nothing AndAlso obj.PartAmount.Value > 0 Then
                Return True
            End If

            If Not obj.LaborAmount Is Nothing AndAlso obj.LaborAmount.Value > 0 Then
                Return True
            End If

            If Not obj.ServiceCharge Is Nothing AndAlso obj.ServiceCharge.Value > 0 Then
                Return True
            End If

            If Not obj.TripAmount Is Nothing AndAlso obj.TripAmount.Value > 0 Then
                Return True
            End If

            If Not obj.ShippingAmount Is Nothing AndAlso obj.ShippingAmount.Value > 0 Then
                Return True
            End If

            If Not obj.OtherAmount Is Nothing AndAlso obj.OtherAmount.Value > 0 Then
                Return True
            End If

            'If (obj.PartAmount Is Nothing AndAlso obj.LaborAmount Is Nothing AndAlso obj.ServiceCharge Is Nothing AndAlso _
            'obj.TripAmount Is Nothing AndAlso obj.OtherAmount Is Nothing) Then
            '    Return False
            'ElseIf ((Not (obj.PartAmount Is Nothing) AndAlso obj.PartAmount.Value = 0) AndAlso _
            '    (Not (obj.LaborAmount Is Nothing) AndAlso obj.LaborAmount.Value = 0) AndAlso _
            '    (Not (obj.ServiceCharge Is Nothing) AndAlso obj.ServiceCharge.Value = 0) AndAlso _
            '    (Not (obj.TripAmount Is Nothing) AndAlso obj.TripAmount.Value = 0) AndAlso _
            '    (Not (obj.OtherAmount Is Nothing) AndAlso obj.OtherAmount.Value = 0)) Then
            '    Return False
            'End If

            Return False

        End Function
    End Class
#End Region
End Class



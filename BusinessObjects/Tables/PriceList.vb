'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/31/2012)  ********************
Imports System.Reflection

Public Class PriceList
    Inherits BusinessObjectBase
    Implements IExpirable

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
            Dim dal As New PriceListDAL
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
            Dim dal As New PriceListDAL
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
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        Me.Effective = Date.Now
        Me.Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub
#End Region

#Region "Properties"

    Public Overrides ReadOnly Property IsNew() As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

    'Key Property
    <ValidateDuplicatePriceListDetail(""), ValidateOverlappingDates(""), ValidateOverlappingPrices("")> _
    Public ReadOnly Property Id() As Guid Implements IExpirable.ID
        Get
            If Row(PriceListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDAL.COL_NAME_PRICE_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10), ValidateDuplicateCode("")> _
    Public Property Code() As String Implements IExpirable.Code
        Get
            CheckDeleted()
            If Row(PriceListDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(PriceListDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If row(PriceListDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PriceListDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property
    
    <ValueMandatory("")> _
    Public Property DefaultCurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDAL.COL_NAME_DEFAULT_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDAL.COL_NAME_DEFAULT_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(value As Guid)
            Me.SetValue(PriceListDAL.COL_NAME_DEFAULT_CURRENCY_ID, value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ManageInventoryId() As Guid
        Get
            CheckDeleted()
            If row(PriceListDAL.COL_NAME_MANAGE_INVENTORY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PriceListDAL.COL_NAME_MANAGE_INVENTORY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_MANAGE_INVENTORY_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective() As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(PriceListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Date.Now
            Else
                Return New DateTimeType(CType(Row(PriceListDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration() As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(PriceListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(PriceListDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(PriceListDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


    'Following is a dummy property just implemented to handle interface constraint
    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set(ByVal value As Guid)
            'do nothing
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try

            MyBase.Save()
            Dim dal As New PriceListDAL
            dal.UpdateFamily(Me.Dataset)
            'Reload the Data
            If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
                'Reload the Data from the DB
                Dim objId As Guid = Me.Id
                Me.Dataset = New DataSet
                Me.Row = Nothing
                Me.Load(objId)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function GetList(ByVal code As String, _
                                   ByVal description As String, _
                                   ByVal serviceType As Guid, _
                                   ByVal countryList As String, _
                                   ByVal serviceCenter As String, _
                                   ByVal activeOn As DateType) As PriceListSearchDV


        Try

            'If ( _
            '   (activeOn Is Nothing) AndAlso _
            '   (code = Nothing Or code = String.Empty) AndAlso _
            '   (description = Nothing Or description = String.Empty) AndAlso _
            '   (serviceType = Nothing Or serviceType.Equals(Guid.Empty)) AndAlso _
            '   (country = Nothing Or country.Equals(Guid.Empty)) AndAlso _
            '   (serviceCenter = Nothing Or serviceCenter = String.Empty) _
            '   ) Then

            '    Dim errors() As ValidationError = _
            '        {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, _
            '                             GetType(PriceList), Nothing, "Search", Nothing)}
            '    Throw New BOValidationException(errors, GetType(PriceList).FullName)

            'End If


            Return New PriceListSearchDV((New PriceListDAL).LoadList(code, _
                                                                     description, _
                                                                     serviceType, _
                                                                     countryList, _
                                                                     serviceCenter, _
                                                                     activeOn, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As PriceList)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing List")
        End If
        MyBase.CopyFrom(original)
        Me.Effective = Date.Now
        Me.Expiration = New Date(2499, 12, 31, 23, 59, 59)

        ''''copy the children for cloning
        For Each detail As PriceListDetail In original.PriceListDetailChildren
            Dim newDetail As PriceListDetail = Me.PriceListDetailChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.PriceListId = Me.Id
            newDetail.Effective = Me.Effective
            newDetail.Expiration = Me.Expiration
            newDetail.Save()
        Next
        ' ''''cleared the service center children as it was storing the count of the parent service center
        'Me.ServiceCenterChildren.Table.Rows.Clear()
        'For Each detail As ServiceCenter In original.ServiceCenterChildren
        '    Dim newDetail As ServiceCenter = Me.ServiceCenterChildren.GetNewChild
        '    newDetail.Copy(detail)
        '    newDetail.PriceListCode = Me.Code
        '    newDetail.Save()
        'Next

    End Sub

    Public Function IsPriceListAssignedtoServiceCenter() As Boolean
        Try
            Return (New ServiceCenterDAL).IsPriceListAssignedtoServiceIssue(Me.Code.ToUpper())
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

    Function GetAvailableServiceCenters() As DataView
        Try
            Dim serviceCenterDAL As New ServiceCenterDAL
            Dim oCountryIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            Return serviceCenterDAL.GetAvailableServiceCenters(oCountryIds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Function PopulateVendorList(ByVal vendorlist As ArrayList) As String
        Try
            Dim returnString As String = String.Empty
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each serviceCenter As ServiceCenter In Me.ServiceCenterChildren
                Dim found As Boolean = False
                For Each cid As String In vendorlist
                    Dim vendor_id As Guid = New Guid(cid)
                    If serviceCenter.Id = vendor_id Then
                        found = True : Exit For
                    End If
                Next
                If Not found Then
                    returnString = serviceCenter.Description
                    Return returnString
                    'serviceCenter.BeginEdit()
                    'serviceCenter.PriceListCode = String.Empty
                    'serviceCenter.EndEdit()
                    'serviceCenter.Save()
                End If
            Next

            For Each cid As String In vendorlist  ' new list
                Dim found As Boolean = False
                Dim vendor_id As Guid = New Guid(cid)
                Dim notFoundVendorId As Guid = New Guid()
                For Each serviceCenter As ServiceCenter In Me.ServiceCenterChildren
                    If vendor_id = serviceCenter.Id Then
                        notFoundVendorId = serviceCenter.Id
                        found = True : Exit For
                    End If
                Next
                If Not found Then
                    ' ADD IT TO THE ORGINAL SERVICECENTER CHILDREN
                    ' GET THE BO FOR THIS SERVICE CENTER
                    Dim newServiceCenter As ServiceCenter = Me.GetServiceCenterChild(New Guid(cid))
                    newServiceCenter.BeginEdit()
                    newServiceCenter.PriceListCode = Me.Code
                    newServiceCenter.EndEdit()
                    newServiceCenter.Save()
                End If
            Next
            Return returnString
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Price List Search Dataview"
    Public Class PriceListSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PRICE_LIST_ID As String = PriceListDAL.COL_NAME_PRICE_LIST_ID
        Public Const COL_NAME_COUNTRY_ID As String = PriceListDAL.COL_NAME_COUNTRY_ID
        Public Const COL_NAME_CODE As String = PriceListDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = PriceListDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_EFFECTIVE As String = PriceListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = PriceListDAL.COL_NAME_EXPIRATION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PriceListId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PRICE_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CountryId(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_COUNTRY_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EFFECTIVE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EXPIRATION).ToString
            End Get
        End Property

    End Class

#End Region

#Region "Price List Detail View"

#End Region

#Region "Expiring Logic"

    Public Function ExpireOverLappingList() As Boolean
        Try
            Dim overlap As New OverlapValidationVisitorDAL
            Dim ds As New DataSet
            ds = overlap.LoadList(Me.Id, Me.GetType.Name, Me.Code, Me.Effective, Me.Expiration, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dtrow As DataRow In ds.Tables(0).Rows
                    Dim pId As Guid = New Guid(CType(dtrow(PriceListDAL.COL_NAME_PRICE_LIST_ID), Byte()))
                    Dim ExpPlist As New PriceList(pId, Me.Dataset)
                    If Me.Effective.Value < ExpPlist.Expiration.Value Then
                        'Expire overlapping list 1 second before current question
                        ExpPlist.Accept(New ExpirationVisitor(Me.Effective))
                    End If
                Next
                Return True         'expired successfully
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Visitor"
    ''' <summary>
    ''' Accept member of IElement interface
    ''' </summary>
    ''' <param name="Visitor"></param>
    ''' <returns>Returns True if Overlapping Records are found</returns>
    ''' <remarks></remarks>
    Public Function Accept(ByRef visitor As IVisitor) As Boolean Implements IExpirable.Accept
        Return visitor.Visit(Me)
    End Function

#End Region

#Region "Validations"
    Public NotInheritable Class ValidateDuplicatePriceListDetail
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DUPLICATE_PRICE_LIST_DETAILS_FOR_PRICE_LIST)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceList = CType(objectToValidate, PriceList)
            'check for duplicate records.
            For Each oPriceListDetail As PriceListDetail In obj.PriceListDetailChildren.Where(Function(i) i.IsNew)
                If (obj.PriceListDetailChildren.Where(Function(i) _
                                                         oPriceListDetail.ServiceClassId = i.ServiceClassId AndAlso _
                                                         oPriceListDetail.ServiceTypeId = i.ServiceTypeId AndAlso _
                                                         oPriceListDetail.ServiceLevelId = i.ServiceLevelId AndAlso _
                                                         oPriceListDetail.RiskTypeId = i.RiskTypeId AndAlso _
                                                         oPriceListDetail.EquipmentClassId = i.EquipmentClassId AndAlso _
                                                         oPriceListDetail.EquipmentId = i.EquipmentId AndAlso _
                                                         oPriceListDetail.ConditionId = i.ConditionId AndAlso _
                                                         oPriceListDetail.PartId = i.PartId AndAlso _
                                                         oPriceListDetail.MakeId = i.MakeId AndAlso _
                                                         oPriceListDetail.ManufacturerOriginCode = i.ManufacturerOriginCode AndAlso _
                                                         oPriceListDetail.PriceBandRangeFrom = i.PriceBandRangeFrom AndAlso _
                                                         oPriceListDetail.PriceBandRangeTo = i.PriceBandRangeTo AndAlso _
                                                         oPriceListDetail.Effective.Value.ToShortDateString() = i.Effective.Value.ToShortDateString() AndAlso _
                                                         oPriceListDetail.Expiration.Value.ToShortDateString() = i.Expiration.Value.ToShortDateString() AndAlso _
                                                         i.IsNew).Count() > 1) Then

                    Return False
                End If

            Next
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidateOverlappingDates
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DATE_OVERLAPPING_IN_PRICE_LIST_DETAIL)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceList = CType(objectToValidate, PriceList)
            For Each oPriceListDetail As PriceListDetail In obj.PriceListDetailChildren
                ''''check for overlapping dates
                If (obj.PriceListDetailChildren.Where(Function(i) _
                                                         oPriceListDetail.ServiceClassId = i.ServiceClassId AndAlso _
                                                         oPriceListDetail.ServiceTypeId = i.ServiceTypeId AndAlso _
                                                         oPriceListDetail.ServiceLevelId = i.ServiceLevelId AndAlso _
                                                         oPriceListDetail.RiskTypeId = i.RiskTypeId AndAlso _
                                                         oPriceListDetail.EquipmentClassId = i.EquipmentClassId AndAlso _
                                                         oPriceListDetail.EquipmentId = i.EquipmentId AndAlso _
                                                         oPriceListDetail.ConditionId = i.ConditionId AndAlso _
                                                         oPriceListDetail.PartId = i.PartId AndAlso _
                                                         oPriceListDetail.MakeId = i.MakeId AndAlso _
                                                         oPriceListDetail.ManufacturerOriginCode = i.ManufacturerOriginCode AndAlso _
                                                         oPriceListDetail.PriceBandRangeFrom.Value <= i.PriceBandRangeTo.Value AndAlso _
                                                         i.PriceBandRangeFrom.Value <= oPriceListDetail.PriceBandRangeTo.Value AndAlso _
                                                         DateTime.Compare(oPriceListDetail.Effective.Value, i.Expiration.Value) <= 0 AndAlso _
                                                         DateTime.Compare(i.Effective.Value, oPriceListDetail.Expiration.Value) <= 0 AndAlso _
                                                         Not oPriceListDetail.Id.Equals(i.Id)).Count > 0) Then

                    Return False
                End If

            Next
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidateOverlappingPrices
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_PRICE_OVERLAPPING_IN_PRICE_LIST_DETAIL)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceList = CType(objectToValidate, PriceList)
            For Each oPriceListDetail As PriceListDetail In obj.PriceListDetailChildren
                ''''check for overlapping prices
                If (obj.PriceListDetailChildren.Where(Function(i) _
                                                         oPriceListDetail.ServiceClassId = i.ServiceClassId AndAlso _
                                                         oPriceListDetail.ServiceTypeId = i.ServiceTypeId AndAlso _
                                                         oPriceListDetail.ServiceLevelId = i.ServiceLevelId AndAlso _
                                                         oPriceListDetail.RiskTypeId = i.RiskTypeId AndAlso _
                                                         oPriceListDetail.EquipmentClassId = i.EquipmentClassId AndAlso _
                                                         oPriceListDetail.EquipmentId = i.EquipmentId AndAlso _
                                                         oPriceListDetail.ConditionId = i.ConditionId AndAlso _
                                                         oPriceListDetail.PartId = i.PartId AndAlso _
                                                         oPriceListDetail.MakeId = i.MakeId AndAlso _
                                                         oPriceListDetail.ManufacturerOriginCode = i.ManufacturerOriginCode AndAlso _
                                                         oPriceListDetail.PriceBandRangeFrom.Value <= i.PriceBandRangeTo.Value AndAlso _
                                                         i.PriceBandRangeFrom.Value <= oPriceListDetail.PriceBandRangeTo.Value AndAlso _
                                                         DateTime.Compare(oPriceListDetail.Effective.Value, i.Expiration.Value) <= 0 AndAlso _
                                                         DateTime.Compare(i.Effective.Value, oPriceListDetail.Expiration.Value) <= 0 AndAlso _
                                                         Not oPriceListDetail.Id.Equals(i.Id)).Count > 0) Then

                    Return False
                End If

            Next
            Return True
        End Function
    End Class
#End Region


#Region "Price List Service Center Views"
    Public ReadOnly Property ServiceCenterChildren() As ServiceCenter.ServiceCenterDetailView
        Get
            Return New ServiceCenter.ServiceCenterDetailView(Me)
        End Get
    End Property

    Public Function GetServiceCenterSelectionView() As ServiceCenterSelectionView
        Dim t As DataTable = ServiceCenterSelectionView.CreateTable
        For Each detail As ServiceCenter In Me.ServiceCenterChildren
            'If t.Rows.Count > 0 Then
            '    Dim foundRows() As DataRow
            '    t.DefaultView.RowFilter = "(service_center_id =  '" & GuidControl.GuidToHexString(detail.Id) & "')"
            '    t.DefaultView.RowFilter = "service_center_id = '" + GuidControl.GuidToHexString(detail.Id) + "'"
            '    If t.DefaultView.Count > 0 Then
            '        t.Rows.Remove(t.DefaultView(0).Row)
            '    End If

            'End If
            Dim row As DataRow = t.NewRow
            row(ServiceCenterSelectionView.COL_NAME_SERVICE_CENTER_DESC) = detail.Description.ToString()
            row(ServiceCenterSelectionView.COL_NAME_SERVICE_CENTER_ID) = detail.Id.ToByteArray
            row(ServiceCenterSelectionView.COL_NAME_PRICE_LIST_CODE) = detail.PriceListCode.ToString()

            t.Rows.Add(row)

        Next
        Return New ServiceCenterSelectionView(t)
    End Function

    Public Class ServiceCenterSelectionView
        Inherits DataView
        Public Const COL_NAME_SERVICE_CENTER_ID As String = ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_NAME_SERVICE_CENTER_DESC As String = ServiceCenterDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_PRICE_LIST_CODE As String = ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_SERVICE_CENTER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CENTER_DESC, GetType(String))
            t.Columns.Add(COL_NAME_PRICE_LIST_CODE, GetType(String))
            Return t
        End Function
    End Class
#End Region

    Public Function GetServiceCenterChild(ByVal childId As Guid) As ServiceCenter
        Return CType(Me.ServiceCenterChildren.GetChild(childId), ServiceCenter)
    End Function

    Public Function GetNewServiceCenterChild() As ServiceCenter
        'Dim newsvc As ServiceCenter = New ServiceCenter(New Guid)
        Dim newServiceCenter As ServiceCenter = CType(Me.ServiceCenterChildren.GetNewChild, ServiceCenter)
        With newServiceCenter
            .PriceListCode = Me.Code
            .Description = Me.Description
        End With
        Return newServiceCenter
    End Function

    Public Function GetNewVendorQuantityChild() As VendorQuantity
        Return New VendorQuantity(Me.Dataset)
    End Function

    Public Function GetNewVendorQuantityChild(id As Guid) As VendorQuantity
        Return New VendorQuantity(id, Me.Dataset)
    End Function

#Region "Price List - Price List Detail Views"
    Public ReadOnly Property PriceListDetailChildren() As PriceListDetail.PriceListDetailChildern
        Get
            Return New PriceListDetail.PriceListDetailChildern(Me)
        End Get
    End Property

    Public Function GetPriceListSelectionView() As PriceListDetailSelectionView
        Dim t As DataTable = PriceListDetailSelectionView.CreateTable
        Dim detail As PriceListDetail
        For Each detail In Me.PriceListDetailChildren
            Dim row As DataRow = t.NewRow

            row(PriceListDetailSelectionView.COL_NAME_PRICE_LIST_DETAIL_ID) = detail.Id.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_PRICE_LIST_ID) = detail.PriceListId.ToByteArray
            If Not detail.Expiration Is Nothing Then
                'row(PriceListDetailSelectionView.COL_NAME_EXPIRATION) = String.Empty
                'Else
                row(PriceListDetailSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.Value
            End If
            row(PriceListDetailSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString()
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_CLASS_ID) = detail.ServiceClassId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_CLASS_CODE) = detail.Row("service_class_code")
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_LEVEL_ID) = detail.ServiceTypeId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_LEVEL_CODE) = detail.Row("service_Level_code")
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_TYPE_ID) = detail.ServiceTypeId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_SERVICE_TYPE_CODE) = detail.Row("service_type_code")
            row(PriceListDetailSelectionView.COL_NAME_RISK_TYPE_ID) = detail.RiskTypeId.ToByteArray
            If detail.RiskTypeId.Equals(Guid.Empty) Then
                row(PriceListDetailSelectionView.COL_NAME_RISK_TYPE_CODE) = detail.Risk_Type.ToString()
            Else
                row(PriceListDetailSelectionView.COL_NAME_RISK_TYPE_CODE) = detail.Row("risk_type_code")
            End If
            row(PriceListDetailSelectionView.COL_NAME_EQUIPMENT_ID) = detail.EquipmentId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_EQUIPMENT_CODE) = detail.Row("equipment_code")
            row(PriceListDetailSelectionView.COL_NAME_MAKE_ID) = detail.MakeId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_MAKE) = detail.Row("make")
            row(PriceListDetailSelectionView.COL_NAME_MODEL_ID) = detail.EquipmentId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_MODEL) = detail.Row("model")

            row(PriceListDetailSelectionView.COL_NAME_PART_ID) = detail.PartId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_PART_CODE) = detail.PartCode?.ToString()
            row(PriceListDetailSelectionView.COL_NAME_PART_DESC) = detail.PartDescription?.ToString()
            row(PriceListDetailSelectionView.COL_NAME_MANUFACTURER_ORIGIN) = detail.ManufacturerOriginCode?.ToString()
            row(PriceListDetailSelectionView.COL_NAME_MANUFACTURER_ORIGIN_DESC) = detail.ManufacturerOriginDescription?.ToString()

            row(PriceListDetailSelectionView.COL_NAME_CONDITION_ID) = detail.ConditionId.ToByteArray
            row(PriceListDetailSelectionView.COL_NAME_CONDITION_TYPE_CODE) = detail.Row("condition_type_code")
            If detail.VendorSku Is Nothing Then
                row(PriceListDetailSelectionView.COL_NAME_VENDOR_SKU) = String.Empty
            Else
                row(PriceListDetailSelectionView.COL_NAME_VENDOR_SKU) = detail.VendorSku.ToString()
            End If
            If detail.VendorSkuDescription Is Nothing Then
                row(PriceListDetailSelectionView.COL_NAME_VENDOR_SKU_DESCRIPTION) = String.Empty
            Else
                row(PriceListDetailSelectionView.COL_NAME_VENDOR_SKU_DESCRIPTION) = detail.VendorSkuDescription.ToString()
            End If
            If detail.Price Is Nothing Then
                row(PriceListDetailSelectionView.COL_NAME_PRICE) = 0
            Else
                row(PriceListDetailSelectionView.COL_NAME_PRICE) = detail.Price.Value
            End If
            row(PriceListDetailSelectionView.COL_NAME_VENDOR_QUANTITY) = IIf(detail.Row("vendor_quantity").Equals(DBNull.Value), 0, detail.Row("Vendor_quantity"))
            If detail.PriceBandRangeFrom Is Nothing Then
                row(PriceListDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = ""
            Else
                row(PriceListDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = detail.PriceBandRangeFrom.Value
            End If
            If detail.PriceBandRangeTo Is Nothing Then
                row(PriceListDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = ""
            Else
                row(PriceListDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = detail.PriceBandRangeTo.Value
            End If
            row(PriceListDetailSelectionView.COL_PRICE_WITH_SYMBOL) = detail.Row("price_with_symbol")
            row(PriceListDetailSelectionView.COL_PRICE_LOW_RANGE_WITH_SYMBOL) = detail.Row("price_low_range_with_symbol")
            row(PriceListDetailSelectionView.COL_PRICE_HIGH_RANGE_WITH_SYMBOL) = detail.Row("price_high_range_with_symbol")

            'US 255424
            row(PriceListDetailSelectionView.COL_NAME_PARENT_CONDITION_ID) = detail.Parent_ConditionId.ToByteArray()
            row(PriceListDetailSelectionView.COL_NAME_PARENT_CONDITION_TYPE_CODE) = detail.Parent_ConditionTypeCode?.ToString()
            row(PriceListDetailSelectionView.COL_NAME_PARENT_EQUIPMENT_ID) = detail.Parent_EquipmentId.ToByteArray()
            row(PriceListDetailSelectionView.COL_NAME_PARENT_MAKE) = detail.Parent_Make?.ToString()
            row(PriceListDetailSelectionView.COL_NAME_PARENT_MAKE_ID) = detail.Parent_MakeId.ToByteArray()
            row(PriceListDetailSelectionView.COL_NAME_PARENT_MODEL) = detail.Parent_Model?.ToString()

            t.Rows.Add(row)
        Next
        Return New PriceListDetailSelectionView(t)

    End Function

    Public Class PriceListDetailSelectionView
        Inherits DataView
        Public Const COL_NAME_PRICE_LIST_DETAIL_ID As String = PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_ID
        Public Const COL_NAME_PRICE_LIST_ID As String = PriceListDetailDAL.COL_NAME_PRICE_LIST_ID
        Public Const COL_NAME_EFFECTIVE As String = PriceListDetailDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = PriceListDetailDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_SERVICE_CLASS_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID
        Public Const COL_NAME_SERVICE_CLASS_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_CLASS_CODE
        Public Const COL_NAME_SERVICE_TYPE_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID
        Public Const COL_NAME_SERVICE_TYPE_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE
        Public Const COL_NAME_SERVICE_LEVEL_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_ID
        Public Const COL_NAME_SERVICE_LEVEL_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_CODE
        Public Const COL_NAME_RISK_TYPE_ID As String = PriceListDetailDAL.COL_NAME_RISK_TYPE_ID
        Public Const COL_NAME_RISK_TYPE_CODE As String = PriceListDetailDAL.COL_NAME_RISK_TYPE_CODE
        Public Const COL_NAME_EQUIPMENT_ID As String = PriceListDetailDAL.COL_NAME_EQUIPMENT_ID
        Public Const COL_NAME_EQUIPMENT_CODE As String = PriceListDetailDAL.COL_NAME_EQUIPMENT_CODE
        Public Const COL_NAME_MAKE_ID As String = PriceListDetailDAL.COL_NAME_MAKE_ID
        Public Const COL_NAME_MAKE As String = PriceListDetailDAL.COL_NAME_MAKE
        Public Const COL_NAME_MODEL_ID As String = PriceListDetailDAL.COL_NAME_MODEL_ID
        Public Const COL_NAME_MODEL As String = PriceListDetailDAL.COL_NAME_MODEL

        Public Const COL_NAME_PART_ID As String = PriceListDetailDAL.COL_NAME_PART_ID
        Public Const COL_NAME_PART_CODE As String = PriceListDetailDAL.COL_NAME_PART_CODE
        Public Const COL_NAME_PART_DESC As String = PriceListDetailDAL.COL_NAME_PART_DESC
        Public Const COL_NAME_MANUFACTURER_ORIGIN As String = PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN
        Public Const COL_NAME_MANUFACTURER_ORIGIN_DESC As String = PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN_DESC
        'Public Const COL_NAME_STOCK_ITEM_TYPE As String = PriceListDetailDAL.COL_NAME_STOCK_ITEM_TYPE

        Public Const COL_NAME_CONDITION_ID As String = PriceListDetailDAL.COL_NAME_CONDITION_ID
        Public Const COL_NAME_CONDITION_TYPE_CODE As String = PriceListDetailDAL.COL_NAME_CONDITION_TYPE_CODE
        Public Const COL_NAME_VENDOR_SKU As String = PriceListDetailDAL.COL_NAME_VENDOR_SKU
        Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION
        Public Const COL_NAME_PRICE As String = PriceListDetailDAL.COL_NAME_PRICE
        Public Const COL_NAME_VENDOR_QUANTITY As String = PriceListDetailDAL.COL_NAME_VENDOR_QUANTITY
        Public Const PRICE_BAND_RANGE_FROM_COL_NAME As String = PriceListDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM
        Public Const PRICE_BAND_RANGE_TO_COL_NAME As String = PriceListDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO
        Public Const COL_PRICE_WITH_SYMBOL As String = PriceListDetailDAL.COL_NAME_CURRENCY_SYMBOL
        Public Const COL_PRICE_LOW_RANGE_WITH_SYMBOL As String = PriceListDetailDAL.COL_NAME_PRICE_LOW_RANGE_WITH_SYMBOL
        Public Const COL_PRICE_HIGH_RANGE_WITH_SYMBOL As String = PriceListDetailDAL.COL_NAME_PRICE_HIGH_RANGE_WITH_SYMBOL

        'US 255424
        Public Const COL_NAME_PARENT_EQUIPMENT_ID As String = PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID
        Public Const COL_NAME_PARENT_EQUIPMENT_CODE As String = PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_CODE
        Public Const COL_NAME_PARENT_CONDITION_ID As String = PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID
        Public Const COL_NAME_PARENT_CONDITION_TYPE As String = PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE
        Public Const COL_NAME_PARENT_CONDITION_TYPE_CODE As String = PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE
        Public Const COL_NAME_PARENT_MAKE_ID As String = PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID
        Public Const COL_NAME_PARENT_MAKE As String = PriceListDetailDAL.COL_NAME_PARENT_MAKE
        Public Const COL_NAME_PARENT_MODEL_ID As String = PriceListDetailDAL.COL_NAME_PARENT_MODEL_ID
        Public Const COL_NAME_PARENT_MODEL As String = PriceListDetailDAL.COL_NAME_PARENT_MODEL

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_PRICE_LIST_DETAIL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PRICE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(DateTime))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(DateTime))
            t.Columns.Add(COL_NAME_SERVICE_CLASS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CLASS_CODE, GetType(String))
            t.Columns.Add(COL_NAME_SERVICE_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_TYPE_CODE, GetType(String))
            t.Columns.Add(COL_NAME_SERVICE_LEVEL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_LEVEL_CODE, GetType(String))
            t.Columns.Add(COL_NAME_RISK_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RISK_TYPE_CODE, GetType(String))
            t.Columns.Add(COL_NAME_EQUIPMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EQUIPMENT_CODE, GetType(String))
            t.Columns.Add(COL_NAME_MAKE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_MAKE, GetType(String))
            t.Columns.Add(COL_NAME_MODEL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_MODEL, GetType(String))

            t.Columns.Add(COL_NAME_PART_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PART_CODE, GetType(String))
            t.Columns.Add(COL_NAME_PART_DESC, GetType(String))
            t.Columns.Add(COL_NAME_MANUFACTURER_ORIGIN , GetType(String))
            t.Columns.Add(COL_NAME_MANUFACTURER_ORIGIN_DESC, GetType(String))
            't.Columns.Add(COL_NAME_STOCK_ITEM_TYPE, GetType(String))

            t.Columns.Add(COL_NAME_CONDITION_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CONDITION_TYPE_CODE, GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU, GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_PRICE, GetType(Decimal))
            t.Columns.Add(COL_NAME_VENDOR_QUANTITY, GetType(Int32))
            t.Columns.Add(PRICE_BAND_RANGE_FROM_COL_NAME, GetType(String))
            t.Columns.Add(PRICE_BAND_RANGE_TO_COL_NAME, GetType(String))
            t.Columns.Add(COL_PRICE_WITH_SYMBOL, GetType(String))
            t.Columns.Add(COL_PRICE_LOW_RANGE_WITH_SYMBOL, GetType(String))
            t.Columns.Add(COL_PRICE_HIGH_RANGE_WITH_SYMBOL, GetType(String))

            'US 255424
            t.Columns.Add(COL_NAME_PARENT_EQUIPMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PARENT_EQUIPMENT_CODE, GetType(String))
            t.Columns.Add(COL_NAME_PARENT_MAKE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PARENT_MAKE, GetType(String))
            t.Columns.Add(COL_NAME_PARENT_MODEL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PARENT_MODEL, GetType(String))
            t.Columns.Add(COL_NAME_PARENT_CONDITION_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PARENT_CONDITION_TYPE_CODE, GetType(String))
            Return t
        End Function
    End Class
#End Region

    Public Function GetPriceListDetailChild(ByVal childId As Guid) As PriceListDetail
        Return CType(Me.PriceListDetailChildren.GetChild(childId), PriceListDetail)
    End Function

    Public Function GetNewPriceListDetailChild() As PriceListDetail
        Dim newPriceListDetail As PriceListDetail = CType(Me.PriceListDetailChildren.GetNewChild, PriceListDetail)

        With newPriceListDetail
            .PriceListId = Me.Id
        End With
        Return newPriceListDetail
    End Function
End Class

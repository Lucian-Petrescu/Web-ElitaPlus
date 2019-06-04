'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/29/2004)  ********************
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl

Public Class ServiceOrder
    Inherits BusinessObjectBase

#Region "Constructors"
    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Public Sub New(ByVal claimBO As ClaimBase, Optional ByVal newServiceOrder As Boolean = False)
        ' MyBase.New(False)
        '   Me.Dataset = claimBO.Dataset
        MyBase.New()
        Me.Dataset = New DataSet
        If newServiceOrder Then
            'just attach the schema 
            'Me.Dataset = New Dataset
            Me.PurgeServiceOrder(claimBO)
            Me.Load()
        Else
            'Me.Dataset = claimBO.Dataset
            Me.Load(claimBO)
        End If
    End Sub

    ' when a new claim is opened or a claim is changed , a new service order is created.
    ' the service order is not saved untill the calling claim BO commits.
    ' if the claim BO doesnt commit/save and wants to re-create a service order ,
    ' it calls the service order new(claim) again. To make sure the service order(s) created
    ' earlier doesnt get saved, we mark them as deleted. Thus, when the claim BO commits/saves,
    ' service order(s) marked for deletion will be deleted and the active one will be saved.
    Protected Sub PurgeServiceOrder(ByVal claimBO As ClaimBase)
        Try
            Dim dal As New ServiceOrderDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                'For Each SORow As DataRow In Me.Dataset.Tables(dal.TABLE_NAME).Rows
                Dim rowIndex As Integer
                For rowIndex = 0 To Me.Dataset.Tables(dal.TABLE_NAME).Rows.Count - 1
                    Row = Me.Dataset.Tables(dal.TABLE_NAME).Rows.Item(rowIndex)
                    If Not (Row.RowState = DataRowState.Deleted) Or (Row.RowState = DataRowState.Detached) Then
                        Dim serviceOrderBO As ServiceOrder = New ServiceOrder(Row)
                        If claimBO.Id.Equals(serviceOrderBO.ClaimId) And serviceOrderBO.IsNew Then
                            serviceOrderBO.Delete()
                        End If
                    End If
                Next
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ServiceOrderDAL
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
            Dim dal As New ServiceOrderDAL
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

    Protected Sub Load(ByVal claimBO As ClaimBase)
        Try
            Dim dal As New ServiceOrderDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(claimBO.Id, dal.COL_NAME_CLAIM_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadLatest(Me.Dataset, claimBO.Id)
                Me.Row = Me.FindRow(claimBO.Id, dal.COL_NAME_CLAIM_ID, Me.Dataset.Tables(dal.TABLE_NAME))
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
    End Sub

    Private _claimStatusBO As ClaimStatus
    Private Const EXTENSION_XSLT As String = ".xslt"
    Private Const ASSEMBLY_OBJECT As String = "Assurant.ElitaPlus.BusinessObjectsNew."

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ServiceOrderDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceOrderDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceOrderDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceOrderDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceOrderDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceOrderDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceOrderDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    '08/24/2006 - ALR - Removed Mandatory property
    '                   Modified property to return the byte array from the database if it exists
    Public ReadOnly Property ServiceOrderImage() As Byte()
        Get
            CheckDeleted()
            If Not ServiceOrderImageId.Equals(Guid.Empty) Then
                Return GetServiceOrderImage(ServiceOrderImageId)
            Else
                Return Nothing
            End If
        End Get
    End Property

    '08/24/2006 - ALR - Added property for the changing of serviceorder images.  Points to ELP_Service_order_image
    Public Property ServiceOrderImageId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_ID, Value)
        End Set
    End Property

    '08/24/2006 - ALR - Added property for the changing of serviceorder images.  
    '                   Stores string XML representation of the dataset used to create the service order.
    Public Property ServiceOrderImageData() As String
        Get
            CheckDeleted()
            If Row(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_DATA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_DATA), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServiceOrderDAL.COL_NAME_SERVICE_ORDER_IMAGE_DATA, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceOrderDAL
                If Not Me.Dataset.Tables(ClaimStatusDAL.TABLE_NAME) Is Nothing AndAlso Me.Dataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                    dal.UpdateFamily(Me.Dataset)
                ElseIf Not (claimAuthorizationId.Equals(Guid.Empty)) Then
                    dal.UpdateFamily(Me.Dataset)
                Else
                    dal.UpdateWithParam(Me.Row)
                End If

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

    Public Function AddExtendedClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            _claimStatusBO = New ClaimStatus(claimStatusId, Me.Dataset)
        Else
            _claimStatusBO = New ClaimStatus(Me.Dataset)
        End If

        Return _claimStatusBO
    End Function


    ''' <summary>
    ''' Gets HTML Service Order Report
    ''' </summary>
    ''' <returns>HTML Service Order Report as <see cref="String"/></returns>
    Public Function GetReportHtmlData() As String
        Dim claimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ClaimId)
        Dim methodOfRepair As String = claimBO.MethodOfRepairCode
        Dim companyCode As String = claimBO.Company.Code
        'TODO -- FIX THIS!!!!!  REMOVE HARD_CODING IN CODE-BEHIND!
        ' Exception by Company
        Select Case claimBO.Company.Code
            Case Codes.COMPANY__TBR
                Dim dealerCode As String = claimBO.DealerCode
                ' Exception by Dealer of a Company
                Select Case dealerCode
                    Case Codes.DEALER__DUDR
                        companyCode = Codes.COMPANY__ABR
                End Select
        End Select

        Return GetReportHtml(Me.ServiceOrderImageData, claimBO.ClaimActivityCode, companyCode, methodOfRepair)
    End Function

    ''' <summary>
    ''' Gets HTML Service Order based on Service order Data, Activity Code, Company Code and Method of Repair
    ''' </summary>
    ''' <param name="pServiceOrderXml">Service Order XML Data</param>
    ''' <param name="pActivityCode">Activity Code of Claim</param>
    ''' <param name="pCompanyCode">Company Code of Claim</param>
    ''' <param name="pMethodOfRepair">Method of Repair of Claim</param>
    ''' <returns>HTML Service Order</returns>
    Private Shared Function GetReportHtml(ByVal pServiceOrderXml As String, ByVal pActivityCode As String, ByVal pCompanyCode As String, Optional ByVal pMethodOfRepair As String = "") As String

        Dim xsltString As String = GetReportXslt(pActivityCode, pCompanyCode, pMethodOfRepair)

        Dim xsltProcessor As New XslCompiledTransform()

        Dim settings As New XmlReaderSettings()
        settings.ProhibitDtd = False

        Using sr As New StringReader(xsltString)
            Using xr As XmlReader = XmlReader.Create(sr, settings)
                xsltProcessor.Load(xr)
            End Using
        End Using

        Dim resultXML As String

        Using sr As New StringReader(pServiceOrderXml)
            Using xr As XmlReader = XmlReader.Create(sr)
                Using sw As New StringWriter()
                    xsltProcessor.Transform(xr, Nothing, sw)
                    resultXML = sw.ToString()
                End Using
            End Using
        End Using

        Return resultXML
    End Function


    ''' <summary>
    ''' Gets Service Order XSLT Resource based on Activity Code, Company Code and Repair Code
    ''' </summary>
    ''' <param name="pActivityCode">Activity Code of Claim</param>
    ''' <param name="pCompanyCode">Company Code of Claim</param>
    ''' <param name="pMethodOfRepair">Method of Repair of Claim</param>
    ''' <remarks>When Resource does not exists for combination of Activity Code, Company Code and Repair Code then Report is returned based on Activity Code and Company Code</remarks>
    ''' <returns>XSLT String read from resource</returns>
    ''' <exception cref="ElitaPlusException">When resource is not found</exception>
    Private Shared Function GetReportXslt(ByVal pActivityCode As String, ByVal pCompanyCode As String, Optional ByVal pMethodOfRepair As String = "") As String

        Dim reportType As String
        Select Case pActivityCode
            Case Codes.CLAIM_ACTIVITY__REWORK
                reportType = Codes.SERVICE_WARRANTY
            Case Codes.CLAIM_ACTIVITY__REPLACED, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT
                reportType = Codes.REPLACEMENT_ORDER
            Case Else
                reportType = Codes.SERVICE_ORDER
        End Select

        Dim resourceName As String
        resourceName = ASSEMBLY_OBJECT & reportType & "_" & pCompanyCode & EXTENSION_XSLT
        If Not String.IsNullOrWhiteSpace(pMethodOfRepair) Then
            resourceName = resourceName & "_" & pMethodOfRepair
        End If

        Dim resourceStream As Stream
        Try
            resourceStream = GetType(ServiceOrder).Assembly.GetManifestResourceStream(resourceName)
            If resourceStream Is Nothing Then
                resourceName = ASSEMBLY_OBJECT & reportType & "_" & pCompanyCode & EXTENSION_XSLT
                resourceStream = GetType(ServiceOrder).Assembly.GetManifestResourceStream(resourceName)
            End If

            If (resourceStream Is Nothing) Then
                Throw New ElitaPlusException(String.Format("Resource {0} not found", resourceName), Nothing)
            End If

            Dim xsltString As String
            Using reader As StreamReader = New StreamReader(resourceStream)
                xsltString = reader.ReadToEnd()
            End Using

            Return xsltString

        Finally
            If (Not resourceStream Is Nothing) Then
                resourceStream.Dispose()
            End If
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Shared Methods"
    Public Shared Function GetLatestServiceOrderID(ByVal claimID As Guid, Optional claimAuthID As Guid = Nothing) As Guid
        Dim dal As ServiceOrderDAL = New ServiceOrderDAL
        Return dal.GetLatestID(claimID, claimAuthID)
    End Function
    Public Shared Function GetSericeOrderEmailContent(ByVal companyid As Guid) As String
        Dim dal As ServiceOrderDAL = New ServiceOrderDAL
        Return dal.GetSericeOrderEmailContent(companyid)
    End Function


    '08/24/2006 - ALR - Added method to retrieve the serviceorderimage from the DB  
    Public Shared Function GetServiceOrderImage(ByVal ServiceOrderImageId As Guid) As Byte()
        Dim dal As New ServiceOrderDAL
        Return dal.LoadImage(ServiceOrderImageId)
    End Function

    Public Shared Function GenerateServiceOrder(ByVal claimBO As ClaimBase, Optional tr As IDbTransaction = Nothing, Optional claimAuthId As Guid = Nothing) As ServiceOrder

        Try



            Dim soHandlerBO As ServiceOrderReportHandler = New ServiceOrderReportHandler(claimBO, claimAuthId)
            Dim ds As DataSet = soHandlerBO.SODataSet

            Dim oServiceOrder = New ServiceOrder(claimBO, True)
            oServiceOrder.ClaimId = claimBO.Id

            If Not (claimAuthId.Equals(Guid.Empty)) Then
                oServiceOrder.ClaimAuthorizationId = claimAuthId
            End If

            oServiceOrder.ServiceOrderImageData = ds.GetXml

            'If 1st Service Order
            If GetLatestServiceOrderID(claimBO.Id).Equals(Guid.Empty) Then
                'create a New claim extended status "Work Order Opened"
                Dim newClaimStatusByGroupId As Guid = Guid.Empty
                newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(DALObjects.ClaimStatusDAL.WORK_ORDER_OPENED)
                If Not newClaimStatusByGroupId.Equals(Guid.Empty) Then
                    Dim objClaimStatusBO As ClaimStatus = oServiceOrder.AddExtendedClaimStatus(Guid.Empty)
                    objClaimStatusBO.ClaimId = claimBO.Id
                    objClaimStatusBO.ClaimStatusByGroupId = newClaimStatusByGroupId
                    objClaimStatusBO.StatusDate = DateTime.Now
                End If
            End If

            oServiceOrder.Save()

            Return oServiceOrder
        Catch ex As Exception
            If (tr IsNot Nothing) Then
                DBHelper.RollBack(tr)
            End If
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Function
#End Region

End Class



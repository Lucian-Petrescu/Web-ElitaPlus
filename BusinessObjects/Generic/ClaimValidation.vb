Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Linq
Imports System.Xml.Linq

Public Class ClaimValidation
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ClaimValidation"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const YESNO As String = "YESNO"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ClaimValidationDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyClaimValidation = ds
    End Sub

    Public Sub New(ByVal ds As ClaimValidationDs, ByVal xml As String)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyClaimValidation = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyClaimValidation As ClaimValidationDs
    Dim _transactionId As String
    Dim _functionTypeCode As String
    Dim _functionTypeId As Guid = Guid.Empty
    Dim claimBO As Claim
    Dim claimFamilyBO As Claim

    Private Sub MapDataSet(ByVal ds As ClaimValidationDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As ClaimValidationDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ClaimValidation Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ClaimValidationDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                Me.TransactionId = .TRANSACTION_ID
                Me.FunctionTypeCode = .FUNCTION_TYPE
            End With

            ' Now only support claim update validation
            ' For claim insert, open for future implementation
            'If Not (Me.FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM) Then
            '    Throw New StoredProcedureGeneratedException("ClaimValidation Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            '    'Else
            '    '    Me.FunctionTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.FunctionTypeCode)

            '    '    If Me.FunctionTypeId.Equals(Guid.Empty) Then
            '    '        Throw New StoredProcedureGeneratedException("ClaimValidation Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            '    '    End If
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property TransactionId() As String
        Get
            Return _transactionId
        End Get
        Set(ByVal Value As String)
            _transactionId = Value
        End Set
    End Property

    Public Property FunctionTypeCode() As String
        Get
            Return _functionTypeCode
        End Get
        Set(ByVal Value As String)
            _functionTypeCode = Value
        End Set
    End Property

    'Public Property FunctionTypeId() As Guid
    '    Get
    '        Return _functionTypeId
    '    End Get
    '    Set(ByVal Value As Guid)
    '        _functionTypeId = Value
    '    End Set
    'End Property

#End Region

#Region "Public Members"

    Public Function CheckDBNull(ByVal obj As Object) As Object
        If DBNull.Value.Equals(obj) Then
            If obj.GetType Is GetType(DecimalType) Then
                Return 0
            Else
                Return Nothing
            End If
        Else
            Return obj
        End If
    End Function

    Private Sub ComposeResult(ByRef outputXml As XElement, ByVal parentItemNumber As String, ByVal itemNumber As String, ByVal claimNumber As String, Optional ByVal errorCode As String = "", Optional ByVal propertyName As String = "")
        Dim result As String = ""
        Dim errXml As XElement
        Dim userInfo As String
        userInfo = "User:" & ElitaPlusIdentity.Current.ActiveUser.NetworkId & "; Date:" & Date.Now.ToString("s") & TimeZoneInfo.Local.ToString.Substring(4, 6)

        If errorCode Is Nothing Or errorCode = "" Then
            result = "OK"
        Else
            If Not propertyName Is Nothing AndAlso propertyName <> "" Then
                propertyName = propertyName & ": "
            End If

            result = "ERROR"
            errXml = <ERROR>
                         <CODE><%= errorCode %></CODE>
                         <MESSAGE><%= propertyName %><%= TranslationBase.TranslateLabelOrMessage(errorCode) %></MESSAGE>
                         <ERROR_INFO><%= userInfo %></ERROR_INFO>
                     </ERROR>
        End If

        Dim tranDataRecXml As XElement = <TRANSACTION_DATA_RECORD>
                                             <PARENT_ITEM_NUMBER>
                                                 <%= parentItemNumber %>
                                             </PARENT_ITEM_NUMBER>
                                             <ITEM_NUMBER>
                                                 <%= itemNumber %>
                                             </ITEM_NUMBER>
                                             <CLAIM_NUMBER>
                                                 <%= claimNumber %>
                                             </CLAIM_NUMBER>
                                             <RESULT>
                                                 <%= result %>
                                             </RESULT>
                                         </TRANSACTION_DATA_RECORD>


        If Not errXml Is Nothing AndAlso errXml.HasElements Then
            tranDataRecXml.Add(errXml)
        End If

        outputXml.Add(tranDataRecXml)
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Try

            Dim claimCount As Integer = 0
            Dim statusCount As Integer = 0
            Dim partCount As Integer = 0
            Dim followupCount As Integer = 0
            Dim transFamilyBO As TransactionLogHeader = Nothing
            Dim myClaimBO As Claim
            Dim tempBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            Dim claimHasError As Boolean = False

            Dim outputXml As XElement = <ClaimValidation>
                                            <TRANSACTION_HEADER>
                                                <TRANSACTION_ID><%= Me.TransactionId %></TRANSACTION_ID>
                                                <FUNCTION_TYPE><%= Me.FunctionTypeCode %></FUNCTION_TYPE>
                                            </TRANSACTION_HEADER>
                                        </ClaimValidation>


            ' Now only support claim update validation
            ' For claim insert, open for future implementation
            If Not (Me.FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM) Then
                Dim errorInfo As String = "User:" & ElitaPlusIdentity.Current.ActiveUser.NetworkId & "; Date:" & Date.Now.ToString("s") & TimeZoneInfo.Local.ToString.Substring(4, 6)
                outputXml = <Error>
                                <Code><%= Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE %></Code>
                                <Message><%= TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE) %></Message>
                                <ErrorInfo><%= errorInfo %></ErrorInfo>
                            </Error>
            Else
                While claimCount < dsMyClaimValidation.TRANSACTION_DATA_RECORD.Count
                    claimHasError = False
                    Dim drRec As DataRow = dsMyClaimValidation.TRANSACTION_DATA_RECORD.Rows(claimCount)
                    Dim claimNumber As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CLAIM_NUMBERColumn)), String)
                    Dim parentItemNumber As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PARENT_ITEM_NUMBERColumn)), String)
                    Dim methodOfRepairCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.METHOD_OF_REPAIR_CODEColumn)), String)
                    claimCount = claimCount + 1

                    ' **** validate if the given claim number exist
                    If claimNumber Is Nothing Then
                        ComposeResult(outputXml, parentItemNumber, parentItemNumber, claimNumber, Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                        claimHasError = True
                        Continue While
                    End If

                    Dim claimID As Guid = tempBO.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, claimNumber)
                    If (claimID.Equals(Guid.Empty)) Then
                        'compose the response xml and go for the next claim validation
                        ComposeResult(outputXml, parentItemNumber, parentItemNumber, claimNumber, Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                        claimHasError = True
                        Continue While
                    End If

                    Dim methodOfRepairId As Guid = Guid.Empty
                    If methodOfRepairCode IsNot Nothing Then
                        methodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, methodOfRepairCode)
                        If methodOfRepairId.Equals(Guid.Empty) Then
                            ComposeResult(outputXml, parentItemNumber, parentItemNumber, claimNumber, Common.ErrorCodes.INVALID_METHOD_OF_REPAIR)
                            claimHasError = True
                            Continue While
                        End If
                    End If

                    myClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(claimID)

                    Dim svcCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.SERVICE_CENTER_CODEColumn)), String)
                    Dim authNumber As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.AUTHORIZATION_NUMBERColumn)), String)
                    Dim serviceType As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.SERVICE_TYPEColumn)), String)
                    Dim prodCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PRODUCT_CODEColumn)), String)
                    Dim serialNumber As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.SERIAL_NUMBERColumn)), String)
                    Dim customerName As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CUSTOMER_NAMEColumn)), String)
                    Dim identificationNumber As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.IDENTIFICATION_NUMBERColumn)), String)
                    Dim customerAddr1 As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CUSTOMER_ADDRESS1Column)), String)
                    Dim customerAddr2 As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CUSTOMER_ADDRESS2Column)), String)
                    Dim comuna As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.COMUNAColumn)), String)
                    Dim regionCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.REGION_CODEColumn)), String)
                    Dim phone1 As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PHONE1Column)), String)
                    Dim phone2 As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PHONE2Column)), String)
                    Dim email As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.EMAILColumn)), String)
                    Dim claimCreatedDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CLAIM_CREATED_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CLAIM_CREATED_DATEColumn)), Date), DateType))
                    Dim prodPurchaseInvoice As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PRODUCT_PURCHASE_INVOICEColumn)), String)
                    Dim retailer As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.RETAILERColumn)), String)
                    Dim prodSalesDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PRODUCT_SALES_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PRODUCT_SALES_DATEColumn)), Date), DateType))
                    Dim prodSalesPrice As Decimal = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PRODUCT_SALES_PRICEColumn)), Decimal)
                    Dim problemDescription As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PROBLEM_DESCRIPTIONColumn)), String)
                    Dim techReport As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.TECHNICAL_REPORTColumn)), String)
                    Dim claimActivityCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.CLAIM_ACTIVITY_CODEColumn)), String)
                    Dim repairDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.REPAIR_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.REPAIR_DATEColumn)), Date), DateType))
                    Dim laborAmount As Decimal = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.LABOR_AMOUNTColumn)), Decimal)
                    Dim tripAmount As Decimal = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.TRIP_AMOUNTColumn)), Decimal)
                    Dim shipping As Decimal = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.SHIPPINGColumn)), Decimal)
                    Dim pickupDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PICKUP_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.PICKUP_DATEColumn)), Date), DateType))
                    Dim inHomeVisitDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.IN_HOME_VISIT_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.IN_HOME_VISIT_DATEColumn)), Date), DateType))
                    Dim visitDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.VISIT_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.VISIT_DATEColumn)), Date), DateType))
                    Dim defectReasonCode As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.DEFECT_REASON_CODEColumn)), String)
                    Dim expectedRepairDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.EXPECTED_REPAIR_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.EXPECTED_REPAIR_DATEColumn)), Date), DateType))
                    Dim eTicket As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.E_TICKETColumn)), String)
                    Dim AWB As String = CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.AWBColumn)), String)
                    Dim collectDate As DateType = If(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.COLLECT_DATEColumn)) Is Nothing, Nothing, CType(CType(CheckDBNull(drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.COLLECT_DATEColumn)), Date), DateType))

                    ' claim information
                    If authNumber IsNot Nothing Then
                        myClaimBO.AuthorizationNumber = authNumber
                    End If
                    If repairDate IsNot Nothing Then
                        myClaimBO.RepairDate = repairDate
                    End If
                    If pickupDate IsNot Nothing Then
                        myClaimBO.PickUpDate = pickupDate
                    End If
                    If visitDate IsNot Nothing Then
                        myClaimBO.VisitDate = visitDate
                    End If
                    If Not methodOfRepairId.Equals(Guid.Empty) Then
                        myClaimBO.MethodOfRepairId = methodOfRepairId
                    End If

                    myClaimBO.ProblemDescription = problemDescription

                    ' claim authorized detail
                    If laborAmount <> 0 Or tripAmount <> 0 Or shipping <> 0 Then
                        Dim authDetailBO As ClaimAuthDetail
                        Try
                            authDetailBO = myClaimBO.AddClaimAuthDetail(myClaimBO.Id, True, True)
                        Catch ex As DataNotFoundException
                            authDetailBO = myClaimBO.AddClaimAuthDetail(Guid.Empty)
                        End Try

                        authDetailBO.ClaimId = myClaimBO.Id
                        If laborAmount <> 0 Then
                            authDetailBO.LaborAmount = laborAmount
                        End If
                        If tripAmount <> 0 Then
                            authDetailBO.TripAmount = tripAmount
                        End If
                        If shipping <> 0 Then
                            authDetailBO.ShippingAmount = shipping
                        End If
                    End If

                    Dim claimErrors As ValidationError() = myClaimBO.ValidationErrors()
                    If claimErrors.Length > 0 Then
                        ComposeResult(outputXml, parentItemNumber, parentItemNumber, claimNumber, CType(claimErrors.GetValue(0), Assurant.Common.Validation.ValidationError).Message, CType(claimErrors.GetValue(0), Assurant.Common.Validation.ValidationError).PropertyName)
                        claimHasError = True
                    End If


                    '*** validate status information...
                    Dim drStatus As DataRow() = dsMyClaimValidation.EXTENDED_CLAIM_STATUS.Select(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.TRANSACTION_DATA_RECORD_IdColumn.ColumnName _
                                                                                               & "=" & drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.TRANSACTION_DATA_RECORD_IdColumn.ColumnName))

                    If Not drStatus Is Nothing Then
                        While statusCount < drStatus.Length
                            Dim statusParentItemNumber As String = CType(CheckDBNull(drStatus(statusCount)(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.PARENT_ITEM_NUMBERColumn)), String)
                            Dim statusItemNumber As String = CType(CheckDBNull(drStatus(statusCount)(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.ITEM_NUMBERColumn)), String)
                            Dim statusCode As String = CType(CheckDBNull(drStatus(statusCount)(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.EXTENDED_CLAIM_STATUS_CODEColumn)), String)
                            Dim statusDate As DateType = CType(CType(CheckDBNull(drStatus(statusCount)(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.EXTENDED_CLAIM_STATUS_DATEColumn)), Date), DateType)
                            Dim statusComment As String = CType(CheckDBNull(drStatus(statusCount)(dsMyClaimValidation.EXTENDED_CLAIM_STATUS.EXTENDED_CLAIM_STATUS_COMMENTColumn)), String)
                            Dim claimStatusByGroupId As Guid = Guid.Empty
                            statusCount = statusCount + 1

                            If Not statusCode Is Nothing AndAlso statusCode = DALObjects.ClaimStatusDAL.BUDGET_APPROVED Then
                                ComposeResult(outputXml, parentItemNumber, statusItemNumber, claimNumber, Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_CODE)
                                claimHasError = True
                                Exit While
                            Else
                                claimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(statusCode)

                                If claimStatusByGroupId.Equals(Guid.Empty) Then
                                    ComposeResult(outputXml, parentItemNumber, statusItemNumber, claimNumber, Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_CODE)
                                    claimHasError = True
                                    Exit While
                                Else
                                    Dim statusBO As ClaimStatus = New ClaimStatus
                                    If statusDate IsNot Nothing Then
                                        statusBO.StatusDate = statusDate.Value
                                    End If
                                    If statusComment IsNot Nothing Then
                                        statusBO.Comments = statusComment
                                    End If
                                    statusBO.ClaimId = myClaimBO.Id
                                    statusBO.ClaimStatusByGroupId = claimStatusByGroupId

                                    Dim statusErrors As ValidationError() = statusBO.ValidationErrors()
                                    If statusErrors.Length > 0 Then
                                        ComposeResult(outputXml, parentItemNumber, statusItemNumber, claimNumber, CType(statusErrors.GetValue(0), Assurant.Common.Validation.ValidationError).Message, CType(statusErrors.GetValue(0), Assurant.Common.Validation.ValidationError).PropertyName)
                                        claimHasError = True
                                        Exit While
                                    End If

                                End If
                            End If

                        End While
                    End If

     
                    '*** validate part information...
                    Dim drPart As DataRow() = dsMyClaimValidation.PARTS_LIST.Select(dsMyClaimValidation.PARTS_LIST.TRANSACTION_DATA_RECORD_IdColumn.ColumnName _
                                                                               & "=" & drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.TRANSACTION_DATA_RECORD_IdColumn.ColumnName))

                    If Not drPart Is Nothing Then
                        While partCount < drPart.Length
                            Dim partParentItemNumber As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.PARENT_ITEM_NUMBERColumn)), String)
                            Dim partItemNumber As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.ITEM_NUMBERColumn)), String)
                            Dim partCode As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.PART_CODEColumn)), String)
                            Dim partDefect As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.PART_DEFECTColumn)), String)
                            Dim partSolution As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.PART_SOLUTIONColumn)), String)
                            Dim partInStock As String = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.IN_STOCKColumn)), String)
                            Dim partCost As Decimal = CType(CheckDBNull(drPart(partCount)(dsMyClaimValidation.PARTS_LIST.PART_COSTColumn)), Decimal)
                            partCount = partCount + 1

                            If Not PartsDescription.IsValidCode(partCode) Then
                                ComposeResult(outputXml, parentItemNumber, partItemNumber, claimNumber, Common.ErrorCodes.INVALID_PART_CODE)
                                claimHasError = True
                                Exit While
                            Else
                                Dim dv As PartsInfo.PartsInfoDV = PartsInfo.getSelectedList(myClaimBO.Id)
                                dv.RowFilter = PartsInfo.PartsInfoDV.COL_NAME_CODE & "='" & partCode & "'"

                                If dv.Count >= 1 Then
                                    ' same parts more than one; do nothing for now.
                                ElseIf dv.Count = 0 Then
                                    ' add new part
                                    Dim partsInfoBO As PartsInfo = New PartsInfo
                                    partsInfoBO.ClaimId = myClaimBO.Id
                                    partsInfoBO.PartsDescriptionId = PartsDescription.GetPartDescriptionByCode(partCode)
                                    If partCost <> 0 Then
                                        partsInfoBO.Cost = partCost
                                    End If
                                    partsInfoBO.InStockID = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), partInStock)

                                    Dim partsInfoErrors As ValidationError() = partsInfoBO.ValidationErrors()
                                    If partsInfoErrors.Length > 0 Then
                                        ComposeResult(outputXml, parentItemNumber, partItemNumber, claimNumber, CType(partsInfoErrors.GetValue(0), Assurant.Common.Validation.ValidationError).Message, CType(partsInfoErrors.GetValue(0), Assurant.Common.Validation.ValidationError).PropertyName)
                                        claimHasError = True
                                        Exit While
                                    End If
                                Else
                                    ComposeResult(outputXml, parentItemNumber, partItemNumber, claimNumber, Common.ErrorCodes.INVALID_PART_CODE)
                                    claimHasError = True
                                    Exit While
                                End If

                            End If
                        End While
                    End If


                    '*** validate followup information...
                    Dim drFollowUp As DataRow() = dsMyClaimValidation.FOLLOWUP.Select(dsMyClaimValidation.FOLLOWUP.TRANSACTION_DATA_RECORD_IdColumn.ColumnName _
                                                                               & "=" & drRec(dsMyClaimValidation.TRANSACTION_DATA_RECORD.TRANSACTION_DATA_RECORD_IdColumn.ColumnName))

                    If Not drFollowUp Is Nothing Then
                        While followupCount < drFollowUp.Length
                            Dim followUpParentItemNumber As String = CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.PARENT_ITEM_NUMBERColumn)), String)
                            Dim followUpItemNumber As String = CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.ITEM_NUMBERColumn)), String)
                            Dim followUpCreatedDate As DateType = CType(CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.CREATE_DATEColumn)), Date), DateType)
                            Dim followUpCommentTypeCode As String = CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.COMMENT_TYPE_CODEColumn)), String)
                            Dim followUpComments As String = CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.COMMENTSColumn)), String)
                            Dim followUpCallerName As String = CType(CheckDBNull(drFollowUp(followupCount)(dsMyClaimValidation.FOLLOWUP.CALLER_NAMEColumn)), String)
                            followupCount = followupCount + 1

                            ' validate followup information...
                            Dim commentTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), followUpCommentTypeCode)
                            If commentTypeId.Equals(Guid.Empty) Then
                                ComposeResult(outputXml, parentItemNumber, followUpItemNumber, claimNumber, Common.ErrorCodes.INVALID_COMMENT_TYPE_CODE)
                                claimHasError = True
                                Exit While
                            Else
                                Dim commentBO As Comment = Comment.GetNewComment(myClaimBO.CertificateId, myClaimBO.Id)
                                If followUpCallerName IsNot Nothing Then
                                    commentBO.CallerName = followUpCallerName
                                End If
                                If followUpComments IsNot Nothing Then
                                    commentBO.Comments = followUpComments
                                End If

                                commentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), followUpCommentTypeCode)
                                Dim commentErrors As ValidationError() = commentBO.ValidationErrors()

                                If commentErrors.Length > 0 Then
                                    ComposeResult(outputXml, parentItemNumber, followUpItemNumber, claimNumber, CType(commentErrors.GetValue(0), Assurant.Common.Validation.ValidationError).Message, CType(commentErrors.GetValue(0), Assurant.Common.Validation.ValidationError).PropertyName)
                                    claimHasError = True
                                    Exit While
                                End If
                            End If
                        End While
                    End If

                    If claimHasError = False Then
                        ComposeResult(outputXml, parentItemNumber, parentItemNumber, claimNumber, "")
                    End If

                End While

            End If

            Return XML_VERSION_AND_ENCODING & outputXml.ToString

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class

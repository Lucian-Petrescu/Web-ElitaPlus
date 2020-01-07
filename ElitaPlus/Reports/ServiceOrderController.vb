Imports System.IO
Imports Assurant.ElitaPlus.BusinessObjectsNew.Codes
Imports System.Reflection
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Microsoft.VisualBasic
Imports System.Diagnostics.Debug
Imports System.Web.HttpServerUtility
Imports System.Collections.Generic

Public Class ServiceOrderController
    Private oServiceOrder As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder
    Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

    Public Sub New()
        oServiceOrder = Nothing
    End Sub

    '08/23/2006 - ALR - Stripped out all of the claim and report processing data as all we have to
    '           do in this step is to create the dataset that forms the service order.
    Public Function GenerateServiceOrder(ByVal claimBO As ClaimBase, Optional claimAuthId As Guid = Nothing) As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder

        'Dim soHandlerBO As ServiceOrderReportHandler = New ServiceOrderReportHandler(claimBO, claimAuthId)
        'Dim ds As DataSet = soHandlerBO.SODataSet

        'oServiceOrder = New Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder(claimBO, True)
        'oServiceOrder.ClaimId = claimBO.Id

        'If Not (claimAuthId.Equals(Guid.Empty)) Then
        '    oServiceOrder.ClaimAuthorizationId = claimAuthId
        'End If

        'oServiceOrder.ServiceOrderImageData = ds.GetXml

        'If Me.IsFirstServiceOrder(claimBO.Id) Then
        '    'create a New claim extended status "Work Order Opened"
        '    Dim newClaimStatusByGroupId As Guid = Guid.Empty
        '    newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(DALObjects.ClaimStatusDAL.WORK_ORDER_OPENED)
        '    If Not newClaimStatusByGroupId.Equals(Guid.Empty) Then
        '        Dim objClaimStatusBO As ClaimStatus = oServiceOrder.AddExtendedClaimStatus(Guid.Empty)
        '        objClaimStatusBO.ClaimId = claimBO.Id
        '        objClaimStatusBO.ClaimStatusByGroupId = newClaimStatusByGroupId
        '        objClaimStatusBO.StatusDate = DateTime.Now
        '    End If
        'End If

        'oServiceOrder.Save()
        'Return oServiceOrder

        'US 224089 - Moving this logic down to BO
        oServiceOrder = ServiceOrder.GenerateServiceOrder(claimBO, Nothing, claimAuthId)

        Return oServiceOrder
    End Function

    '08/28/2006 - ALR - Added method to get the required report name if we are using an xml transformation
    '                   rather than creating the PDF report
    Public Function GenerateReportName(ByVal claimId As Guid, ByVal claimAuthorizationId As Guid) As String

        Dim claimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
        Dim sActivityCode As String
        Dim oCompany As New Company(claimBO.CompanyId)
        Dim companyId As Guid = oCompany.Id
        Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
        Dim rprCode As String = claimBO.MethodOfRepairCode

        Dim claimAuthorizationBO As ClaimAuthorization
        Dim serviceOrderType As String

        If Not (claimAuthorizationId.Equals(Guid.Empty)) Then
            claimAuthorizationBO = New ClaimAuthorization(claimAuthorizationId)
            serviceOrderType = claimAuthorizationBO.ServiceOrderType
        End If

        'TODO -- FIX THIS!!!!!  REMOVE HARD_CODING IN CODE-BEHIND!
        ' Exception by Company
        Select Case compCode
            Case Codes.COMPANY__TBR
                Dim dealerCode As String = claimBO.DealerCode
                ' Exception by Dealer of a Company
                Select Case dealerCode
                    Case Codes.DEALER__DUDR
                        compCode = Codes.COMPANY__ABR
                End Select
        End Select

        sActivityCode = claimBO.ClaimActivityCode

        Return GenerateReportName(sActivityCode, compCode, rprCode, serviceOrderType)

    End Function

    Private Function GenerateReportName(ByVal sActivityCode As String,
                                        ByVal compCode As String,
                                        Optional ByVal RepairCode As String = "",
                                        Optional ByVal ServiceOrderType As String = Nothing) As String

        Dim strReportType, strReport As String

        If Not String.IsNullOrEmpty(ServiceOrderType) Then
            Select Case ServiceOrderType
                Case CLAIM_SERVICE_ORDER_TYPE_REPLACEMENT
                    strReportType = REPLACEMENT_ORDER
                Case Else
                    strReportType = SERVICE_ORDER
            End Select
        Else
            Select Case sActivityCode
                Case CLAIM_ACTIVITY__REWORK
                    strReportType = SERVICE_WARRANTY
                Case CLAIM_ACTIVITY__REPLACED, CLAIM_ACTIVITY__PENDING_REPLACEMENT
                    strReportType = REPLACEMENT_ORDER
                Case Else
                    strReportType = SERVICE_ORDER
            End Select

        End If

        strReport = strReportType & "_" & compCode
        If RepairCode.Trim.Length > 0 Then
            strReport = strReport & "_" & RepairCode
        End If

        If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReport) & ".xslt") Then
            strReport = strReportType & "_" & compCode
        End If

        Return strReport
    End Function

    Private Function RemoveInvalidChar(ByVal filename As String) As String
        Dim index As Integer
        For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
        Next
        Return filename

    End Function

    Public Function IsFirstServiceOrder(ByVal claimId As Guid) As Boolean
        Dim serviceOrderID As Guid = ServiceOrder.GetLatestServiceOrderID(claimId)
        If serviceOrderID.Equals(Guid.Empty) Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


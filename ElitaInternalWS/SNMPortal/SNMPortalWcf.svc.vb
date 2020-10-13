Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.Common

' NOTE: If you change the class name "SNMPortalWcf" here, you must also update the reference to "SNMPortalWcf" in Web.config and in the associated .svc file.

Namespace SNMPortal

    <ServiceBehavior(Namespace:="http://elita.assurant.com/SNMPortalNamespace")> _
    Public Class SNMPortalWcf
        Inherits ElitaWcf
        Implements SNMPortal.ISNMPortalWcf

#Region "Operations"
        Public Function Hello(name As String) As String Implements ISNMPortalWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements ISNMPortalWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, _
                        group As String) As String Implements ISNMPortalWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements ISNMPortalWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "SNMPORTALWS")
            Return sRet
        End Function

        Public Function GetPriceList(priceListDetailsSearch As PriceListSearchDC) _
            As System.Collections.Generic.List(Of PriceListDC) Implements ISNMPortalWcf.GetPriceList

            Dim dsPriceList As DataSet
            Dim row As DataRow
            Dim retPriceList As New List(Of PriceListDC)()

            Try
                Try
                    ElitaService.VerifyToken(True, priceListDetailsSearch.Token)
                Catch ex As Exception
                    Throw New BOValidationException("GetPriceList Error: Invalid Token ", ErrorCodes.WS_ERR_INVALID_TOKEN)
                End Try

                Dim oSearch As New PriceListSearch

                If Not priceListDetailsSearch.Claim_Number = Nothing AndAlso Not priceListDetailsSearch.Company_Code = Nothing _
                    AndAlso Not priceListDetailsSearch.Service_Center_Code = Nothing AndAlso (Not priceListDetailsSearch.Equipment_Class_Code = Nothing OrElse Not priceListDetailsSearch.Risk_Type_Code = Nothing) Then

                    Throw New BOValidationException("GetPriceList Error: Search must be based on Claim or Service Center", ErrorCodes.WS_PRICELIST_INVALID_INPUT)
                End If

                If (Not priceListDetailsSearch.Claim_Number = Nothing AndAlso Not priceListDetailsSearch.Company_Code = Nothing) Then

                    Dim oSearchByClaim As New PriceListSearchByClaim(priceListDetailsSearch)
                    oSearch.action = oSearchByClaim
                    dsPriceList = oSearch.GetPriceList(priceListDetailsSearch)

                ElseIf (Not priceListDetailsSearch.Service_Center_Code = Nothing AndAlso Not priceListDetailsSearch.Risk_Type_Code = Nothing) _
                    OrElse (Not priceListDetailsSearch.Service_Center_Code = Nothing AndAlso Not priceListDetailsSearch.Equipment_Class_Code = Nothing) Then

                    Dim oSearchByServiceCenter As New PriceListSearchByServiceCenter(priceListDetailsSearch)
                    oSearch.action = oSearchByServiceCenter
                    dsPriceList = oSearch.GetPriceList(priceListDetailsSearch)
                Else
                    Throw New BOValidationException("GetPriceList Error: Must Provide Service Center Code with Risk Type Code/Equipment Class Code or Claim Number with Company Code", ErrorCodes.WS_PRICELIST_INVALID_INPUT)
                End If

                If (dsPriceList.Tables(0).Rows.Count = 0) Then
                    Throw New BOValidationException("GetPriceList Error: No Data Found ", ErrorCodes.BO_DATA_NOT_FOUND)
                Else
                    For Each row In dsPriceList.Tables(0).Rows
                        Dim pi As New PriceListDC
                        With pi
                            .Service_Center_Code = row("SERVICE_CENTER_CODE").ToString()
                            .Risk_Type_Code = row("RISK_TYPE_CODE").ToString()
                            .Method_Of_Repair_Code = row("METHOD_OF_REPAIR_CODE").ToString()
                            .Service_Class_Code = row("SERVICE_CLASS_CODE").ToString()
                            .Service_Class_Translation = row("SERVICE_CLASS_TRANSLATION").ToString()
                            .Service_Type_Code = row("SERVICE_TYPE_CODE").ToString()
                            .Service_Type_Translation = row("SERVICE_TYPE_TRANSLATION").ToString()
                            .Service_Level_Code = row("SERVICE_LEVEL_CODE").ToString()
                            .Service_Level_Translation = row("SERVICE_LEVEL_TRANSLATION").ToString()
                            .Low_Price = row("LOW_PRICE")
                            .High_Price = row("HIGH_PRICE")
                            .Price = row("PRICE")
                        End With
                        retPriceList.Add(pi)
                    Next
                End If
                Return retPriceList
            Catch ex As BOValidationException

                Dim fault As New SNMPortalFaultDC
                fault.FaultReason = ex.Message
                Throw New FaultException(Of SNMPortalFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR), New FaultCode(ex.Code))

            Catch ex As Exception
                Dim fault As New SNMPortalFaultDC()
                fault.FaultReason = ex.Message
                Throw New FaultException(Of SNMPortalFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR), New FaultCode("FATAL_ERROR"))
            End Try

        End Function

        Public Function GetPreInvoice(preInvoiceDetailsSearch As PreInvoiceSearchDC) _
            As System.Collections.Generic.List(Of PreInvoiceDC) Implements ISNMPortalWcf.GetPreInvoice

            Dim dsPreInvoice As DataSet
            Dim row As DataRow
            Dim objCompVal As Boolean

            Dim retPreInvoice As New List(Of PreInvoiceDC)()
            Try
                Try
                    ElitaService.VerifyToken(True, preInvoiceDetailsSearch.Token)
                Catch ex As Exception
                    Throw New BOValidationException("GetPreInvoice Error: Invalid Token ", ErrorCodes.WS_ERR_INVALID_TOKEN)
                End Try

                If preInvoiceDetailsSearch.Company_Code = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must Provide Company Code ", ErrorCodes.WS_INVALID_COMPANY_CODE)
                End If

                If preInvoiceDetailsSearch.Service_Center_Code = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must Provide Service Center Code ", ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If

                If Not PreInvoice.ValidateServiceCenterCode(preInvoiceDetailsSearch.Service_Center_Code, PreInvoice.GetCountryId(PreInvoice.GetCompanyId(preInvoiceDetailsSearch.Company_Code))) Then
                    Throw New BOValidationException("GetPreInvoice Error: Invalid Service Center Code ", ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If

                If preInvoiceDetailsSearch.SC_PreInvoice_Date_From = Nothing AndAlso Not preInvoiceDetailsSearch.SC_PreInvoice_Date_To = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must provide SC_PreInvoice_Date_From", ErrorCodes.WS_PREINVOICE_DATEFROM_NOT_FOUND)
                End If
                If preInvoiceDetailsSearch.SC_PreInvoice_Date_To = Nothing AndAlso Not preInvoiceDetailsSearch.SC_PreInvoice_Date_From = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must provide SC_PreInvoice_Date_To", ErrorCodes.WS_PREINVOICE_DATETO_NOT_FOUND)
                End If

                dsPreInvoice = PreInvoice.GetPreInvoiceBAL(preInvoiceDetailsSearch.Company_Code, preInvoiceDetailsSearch.Service_Center_Code, preInvoiceDetailsSearch.SC_PreInvoice_Date_From, preInvoiceDetailsSearch.SC_PreInvoice_Date_To)

                If dsPreInvoice.Tables(0).Rows.Count <= 0 Then
                    Throw New BOValidationException("GetPreInvoice Error: No Data Found ", ErrorCodes.BO_DATA_NOT_FOUND)
                Else
                    For Each row In dsPreInvoice.Tables(0).Rows
                        Dim pi As New PreInvoiceDC
                        With pi
                            .Batch_Number = row("BATCH_NUMBER")
                            .PreInvoice_Date = row("PREINVOICE_DATE")
                        End With
                        retPreInvoice.Add(pi)
                    Next
                End If

                Return retPreInvoice
            Catch ex As BOValidationException

                Dim fault As New SNMPortalFaultDC
                fault.FaultReason = ex.Message
                Throw New FaultException(Of SNMPortalFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR), New FaultCode(ex.Code))

            Catch ex As Exception
                Dim fault As New SNMPortalFaultDC()
                fault.FaultReason = ex.Message
                Throw New FaultException(Of SNMPortalFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR), New FaultCode("FATAL_ERROR"))
            End Try
        End Function

#End Region

    End Class

End Namespace

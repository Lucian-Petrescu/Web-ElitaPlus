Imports System.ServiceModel
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "CustomerRegistration" in code, svc and config file together.

Namespace CustomerReg

    <ServiceBehavior(Namespace:="http://elita.assurant.com/CustomerReg")>
    Public Class CustomerRegistration
        Inherits ElitaWcf
        Implements CustomerReg.ICustomerRegistration


        Public Function Hello(name As String) As String Implements ICustomerRegistration.Hello
            Try
                Dim sRet As String

                sRet = MyBase.Hello(name)
                Return sRet
            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function Login() As String Implements ICustomerRegistration.Login
            Try
                Dim sRet As String

                sRet = MyBase.Login()
                Return sRet
            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function LoginBody(networkID As String, password As String, group As String) As String Implements ICustomerRegistration.LoginBody
            Try
                Dim sRet As String

                sRet = MyBase.LoginBody(networkID, password, group)
                Return sRet
            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function FindRegistration(customerRegItemSearch As CustRegItemSearchDC) As CustRegistrationDC Implements ICustomerRegistration.FindRegistration
            Try
                Dim dsCustReg As DataSet
                Dim row As DataRow
                Dim retCustReg As New CustRegistrationDC
                Dim countryId As Guid
                Dim dealerId As Guid

                ElitaService.VerifyToken(True, customerRegItemSearch.Token)

                countryId = CustRegistration.GetCountryID(customerRegItemSearch.CountryCode)
                dealerId = CustRegistration.GetDealerID(customerRegItemSearch.DealerCode)

                dsCustReg = CustRegistration.GetRegistration(customerRegItemSearch.EmailID, dealerId)

                For Each row In dsCustReg.Tables(0).Rows
                    With retCustReg
                        .DealerCode = row("DEALER").ToString()
                        .EmailID = row("EMAIL").ToString()
                        .TaxID = row("TAX_ID").ToString()
                        .FirstName = row("FIRST_NAME").ToString()
                        .LastName = row("LAST_NAME").ToString()
                        .Address1 = row("ADDRESS1").ToString()
                        .Address2 = row("ADDRESS2").ToString()
                        .City = row("CITY").ToString()
                        .State = row("REGION_DESC").ToString()
                        .PostalCode = row("POSTAL_CODE").ToString()
                        .Phone = row("CELL_PHONE").ToString()
                        .CountryCode = row("COUNTRY_CODE").ToString()
                    End With
                Next

                Return retCustReg

            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)
                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try

        End Function

        Public Function CreateRegistration(customerRegistration As CustRegistrationDC) As String Implements ICustomerRegistration.CreateRegistration
            Try
                Dim returnMessage As String

                ElitaService.VerifyToken(True, customerRegistration.Token)

                Dim custRegistrationBO As New CustRegistration

                returnMessage = custRegistrationBO.CreateRegistrationElements(customerRegistration)

                Return returnMessage
            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC()

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)

                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function UpdateRegistration(customerRegistration As CustRegistrationDC) As String Implements ICustomerRegistration.UpdateRegistration
            Try
                Dim returnMessage As String
                Dim id As Guid

                ElitaService.VerifyToken(True, customerRegistration.Token)

                id = CustRegistration.GetRegistration(customerRegistration.EmailID, customerRegistration.DealerCode)

                Dim custRegBO As New CustRegistration(id)

                returnMessage = custRegBO.UpdateRegistrationElements(customerRegistration)

                Return returnMessage
            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC()

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)

                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function FindItem(customerRegItemSearch As CustRegItemSearchDC) As System.Collections.Generic.List(Of CustItemDC) Implements ICustomerRegistration.FindItem
            Try
                Dim dsCustItem As DataSet
                Dim row As DataRow
                Dim retCustItem As New List(Of CustItemDC)()
                Dim dealerId As Guid

                ElitaService.VerifyToken(True, customerRegItemSearch.Token)

                dealerId = CustRegistration.GetDealerID(customerRegItemSearch.DealerCode)
                dsCustItem = CustItem.GetItemFromEmail(customerRegItemSearch.EmailID, dealerId)

                For Each row In dsCustItem.Tables(0).Rows
                    Dim ci As New CustItemDC
                    With ci
                        .RegistrationDate = row("REGISTRATION_DATE")
                        .IMEINumber = row("IMEI_NUMBER").ToString()
                        .Make = row("MAKE").ToString()
                        .Model = row("MODEL").ToString()
                        .ItemName = row("ITEM_NAME").ToString()
                        .RegistrationStatus = row("REGISTRATION_STATUS").ToString()
                        .Coverage = row("COVERAGE").ToString()
                        .OrderReferenceNumber = row("ORDER_REF_NUM").ToString()
                        .ProductKey = row("PRODUCT_KEY").ToString()
                    End With
                    retCustItem.Add(ci)
                Next

                Return retCustItem

            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)
                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function CreateItem(customerItem As CustItemDC) As String Implements ICustomerRegistration.CreateItem
            Try
                Dim returnMessage As String
                Dim registrationId As Guid

                ElitaService.VerifyToken(True, customerItem.Token)

                registrationId = CustRegistration.GetRegistration(customerItem.EmailID, customerItem.DealerCode)

                Dim custRegBO As New CustRegistration(registrationId)

                returnMessage = CustItem.CreateItemElements(customerItem, custRegBO)

                Return returnMessage
            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC()

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)

                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function DeleteItem(customerItemDelete As CustItemDeleteActivateDC) As System.Collections.Generic.List(Of CustItemDC) Implements ICustomerRegistration.DeleteItem
            Try
                Dim dsCustItem As DataSet
                Dim row As DataRow
                Dim retCustItem As New List(Of CustItemDC)()
                Dim dealerId As Guid
                Dim registrationId As Guid
                Dim registrationItemId As Guid

                ElitaService.VerifyToken(True, customerItemDelete.Token)

                dealerId = CustRegistration.GetDealerID(customerItemDelete.DealerCode)

                registrationId = CustRegistration.GetRegistration(customerItemDelete.EmailID, customerItemDelete.DealerCode)

                registrationItemId = CustItem.GetItemByRegistrationAndIMEI(registrationId, customerItemDelete.IMEINumber)

                Dim custItemBO As New CustItem(registrationItemId)
                CustItem.DeleteItem(custItemBO)

                'custItemBO.Delete()
                'custItemBO.Save()

                dsCustItem = CustItem.GetItemFromEmail(customerItemDelete.EmailID, dealerId)

                For Each row In dsCustItem.Tables(0).Rows
                    Dim ci As New CustItemDC
                    With ci
                        .RegistrationDate = row("REGISTRATION_DATE")
                        .IMEINumber = row("IMEI_NUMBER").ToString()
                        .Make = row("MAKE").ToString()
                        .Model = row("MODEL").ToString()
                        .ItemName = row("ITEM_NAME").ToString()
                        .RegistrationStatus = row("REGISTRATION_STATUS").ToString()
                        .Coverage = row("COVERAGE").ToString()
                        .OrderReferenceNumber = row("ORDER_REF_NUM").ToString()
                        .ProductKey = row("PRODUCT_KEY").ToString()
                    End With
                    retCustItem.Add(ci)
                Next

                Return retCustItem

            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)
                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function ActivateItem(customerItemActivate As CustItemDeleteActivateDC) As String Implements ICustomerRegistration.ActivateItem
            Try
                Dim returnMessage As String = String.Empty
                Dim registrationId As Guid = Guid.Empty
                Dim registrationItemId As Guid
                Dim dealerId As Guid = Guid.Empty

                ElitaService.VerifyToken(True, customerItemActivate.Token)

                'dealerId = CustRegistration.GetDealerID(customerItemActivate.DealerCode)

                registrationId = CustRegistration.GetRegistration(customerItemActivate.EmailID, customerItemActivate.DealerCode)
                registrationItemId = CustItem.GetItemByRegistrationAndIMEI(registrationId, customerItemActivate.IMEINumber)

                Dim custRegBO As New CustRegistration(registrationId)

                Dim objCustItemBo As New CustItem(registrationItemId)

                returnMessage = objCustItemBo.ActivateItem(customerItemActivate, custRegBO, objCustItemBo)

                Return returnMessage

            Catch ex As BOValidationException
                Dim fault As New CustServiceFaultDC()

                Dim err As Validation.ValidationError
                Dim validationExc As BOValidationException = ex
                Dim errStrList(validationExc.ValidationErrorList.Length - 1) As String
                Dim i As Integer = 0
                Dim preText As String = ""
                For Each err In validationExc.ValidationErrorList
                    Dim errMsg As String = err.Message.Substring(err.Message.IndexOf("|") + 1)
                    errStrList(i) = ""

                    'Check to see if there is any prefix text on the message
                    If err.Message.IndexOf("|") > 0 Then
                        preText = err.Message.Substring(0, err.Message.IndexOf("|"))
                    End If

                    errStrList(i) &= err.PropertyName & ":" & preText & TranslationBase.TranslateLabelOrMessage(errMsg)
                    fault.FaultDetail = errStrList(i)
                    i += 1
                Next
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))

            Catch ex As Exception
                Dim fault As New CustServiceFaultDC()
                fault.FaultDetail = ex.Message
                Throw New FaultException(Of CustServiceFaultDC)(fault, New FaultReason(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

    End Class

End Namespace
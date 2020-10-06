Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Web.Hosting
Imports Assurant.ElitaPlus.Common

' NOTE: If you change the class name "UtilityWcf" here, you must also update the reference to "UtilityWcf" in Web.config and in the associated .svc file.
Namespace Utilities

    <ServiceBehavior(Namespace:="http://elita.assurant.com/utilitiesNamespace")> _
    Public Class UtilityWcf
        Inherits ElitaWcf
        Implements Utilities.IUtilityWcf

#Region " Constants"

        Private Const END_OF_LINE As String = "^"
        Private Const END_OF_FIELD As String = "|"
        Private Const WS_CONSUMER_CLIENT As String = "CLIENT"
        Private Const WS_CONSUMER_SERVER As String = "SERVER"

#End Region

#Region " Private Methods"

        Private Function CompactData(dw As DataView) As String

            Dim result As String = String.Empty

            Try

                Dim i As Integer
                Dim row As DataRowView
                Dim colNum As Integer = dw.Table.Columns.Count

                Dim IEnum As IEnumerator = dw.GetEnumerator
                While IEnum.MoveNext

                    row = CType(IEnum.Current, DataRowView)
                    For i = 0 To colNum - 1
                        result &= Convert.ToString(row(i))
                        If (i < colNum - 1) Then result &= END_OF_FIELD
                    Next

                    result &= END_OF_LINE

                End While

            Catch ex As Exception

            End Try

            Return result

        End Function

#End Region

#Region "Operations"

        Public Function Hello(name As String) As String Implements IUtilityWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IUtilityWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, group As String) _
                                            As String Implements IUtilityWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements IUtilityWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "UtilityWS")
            Return sRet
        End Function


        Public Function GetVSCMakes(token As String, wsConsumer As String) As String _
                                            Implements IUtilityWcf.GetVSCMakes
            ElitaService.VerifyToken(True, token)
            Dim objCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
            Dim companyGroupID As Guid = objCompanyGroup.Id
            Dim dv As DataView = LookupListNew.GetVSCMakeLookupList(companyGroupID)
            If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
                Return CompactData(dv)
            Else
                Dim ds As New DataSet
                ds.Tables.Add(dv.Table.Copy)
                Return (XMLHelper.FromDatasetToXML(ds))
            End If

        End Function

        Public Function GetVSCModels(token As String, wsConsumer As String, _
                                    make As String) As String Implements IUtilityWcf.GetVSCModels
            ElitaService.VerifyToken(True, token)
            Dim dv As DataView = LookupListNew.GetVSCModelsLookupList(make)
            If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
                Return CompactData(dv)
            Else
                Dim ds As New DataSet
                ds.Tables.Add(dv.Table.Copy)
                Return (XMLHelper.FromDatasetToXML(ds))
            End If

        End Function

        Public Function GetVSCVersions(token As String, wsConsumer As String, _
                                       model As String, make As String) As String _
                                       Implements IUtilityWcf.GetVSCVersions
            ElitaService.VerifyToken(True, token)
            Dim dv As DataView = LookupListNew.GetVSCTrimLookupList(model, make)
            If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
                Return CompactData(dv)
            Else
                Dim ds As New DataSet
                ds.Tables.Add(dv.Table.Copy)
                Return (XMLHelper.FromDatasetToXML(ds))
            End If

        End Function

        Public Function GetVSCYears(token As String, wsConsumer As String, _
                                    trim As String, model As String, make As String) _
                                    As String Implements IUtilityWcf.GetVSCYears
            ElitaService.VerifyToken(True, token)
            Dim dv As DataView = LookupListNew.GetVSCYearsLookupList(trim, model, make)
            If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
                Return CompactData(dv)
            Else
                Dim ds As New DataSet
                ds.Tables.Add(dv.Table.Copy)
                Return (XMLHelper.FromDatasetToXML(ds))
            End If


        End Function

#End Region

    End Class

End Namespace

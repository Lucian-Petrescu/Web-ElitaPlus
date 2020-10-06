Imports System.Web.Services
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

<System.Web.Services.WebService(Namespace:="http://tempuri.org/ElitaInternalWS/Utilities/VSCUtility")> _
Public Class VSCUtility
    Inherits ElitaWebService 'System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

    Protected Overloads Overrides Sub Dispose(disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

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

    <WebMethod(EnableSession:=True)> _
    Public Function GetVSCMakes(token As String, wsConsumer As String) As String
        ElitaService.VerifyToken(False, token)
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

    <WebMethod(EnableSession:=True)> _
    Public Function GetVSCModels(token As String, wsConsumer As String, make As String) As String
        ElitaService.VerifyToken(False, token)
        Dim dv As DataView = LookupListNew.GetVSCModelsLookupList(make)
        If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
            Return CompactData(dv)
        Else
            Dim ds As New DataSet
            ds.Tables.Add(dv.Table.Copy)
            Return (XMLHelper.FromDatasetToXML(ds))
        End If

    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetVSCVersions(token As String, wsConsumer As String, model As String, make As String) As String
        ElitaService.VerifyToken(False, token)
        model = Server.UrlDecode(model)
        Dim dv As DataView = LookupListNew.GetVSCTrimLookupList(model, make)
        If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
            Return CompactData(dv)
        Else
            Dim ds As New DataSet
            ds.Tables.Add(dv.Table.Copy)
            Return (XMLHelper.FromDatasetToXML(ds))
        End If

    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetVSCYears(token As String, wsConsumer As String, trim As String, model As String, make As String) As String
        ElitaService.VerifyToken(False, token)
        model = Server.UrlDecode(model)
        trim = Server.UrlDecode(trim)

        Dim dv As DataView = LookupListNew.GetVSCYearsLookupList(trim, model, make)
        If wsConsumer.ToUpper.Equals(WS_CONSUMER_CLIENT) Then
            Return CompactData(dv)
        Else
            Dim ds As New DataSet
            ds.Tables.Add(dv.Table.Copy)
            Return (XMLHelper.FromDatasetToXML(ds))
        End If


    End Function

End Class

Partial Class ajaxTest
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
     
        ErrControllerMaster.Clear_Hide()

    End Sub

#Region "Ajax ToolKit Testing"


#Region "AUTOCOMPLETE TEXT BOX"

    <System.Web.Services.WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Shared Function GetCompletionList(prefixText As String, count As Integer) As String()

        Dim dv As DataView = LookupListNew.GetDealerLookupList(CType(ElitaPlusIdentity.Current.ActiveUser.Companies.Item(0), Guid))
        Dim arrDealers As New ArrayList

        For i As Integer = 0 To dv.Count - 1
            If dv(i).Item("DESCRIPTION").ToString.ToUpper.StartsWith(prefixText.ToUpper) Then
                'arrDealers.Add(dv(i).Item("DESCRIPTION"))
                arrDealers.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dv(i).Item("DESCRIPTION").ToString, GuidControl.ByteArrayToGuid(dv(i).Item("Id")).ToString))
            End If
        Next

        Return CType(arrDealers.ToArray(GetType(String)), String())
    End Function

#End Region

#Region "Make/model Cascading Dropdowns"


    <System.Web.Services.WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Shared Function FillMakes(knownCategoryValues As String, category As String) As AjaxControlToolkit.CascadingDropDownNameValue()


        Dim dw As DataView
        If Not knownCategoryValues = "" Then
            Return Nothing
        End If

        dw = LookupListNew.GetVSCMakeLookupList(Authentication.CurrentUser.CompanyGroup.Id)
        Dim values As New ArrayList

        For Each dvr As DataRowView In dw
            values.Add(New AjaxControlToolkit.CascadingDropDownNameValue(dvr.Item("DESCRIPTION").ToString, dvr.Item("MANUFACTURER_ID").ToString))
        Next

        Return CType(values.ToArray(GetType(AjaxControlToolkit.CascadingDropDownNameValue)), AjaxControlToolkit.CascadingDropDownNameValue())


    End Function

    <System.Web.Services.WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Shared Function getModels(knownCategoryValues As String, category As String) As AjaxControlToolkit.CascadingDropDownNameValue()

        Dim dw As DataView
        Dim kv As System.Collections.Specialized.StringDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

        If Not kv.ContainsKey("MAKE") Then
            Return Nothing
        End If

        dw = LookupListNew.GetVSCModelsLookupList(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(kv("MAKE"))))
        Dim values As New ArrayList

        For Each dvr As DataRowView In dw
            values.Add(New AjaxControlToolkit.CascadingDropDownNameValue(dvr.Item("DESCRIPTION").ToString, dvr.Item("CODE").ToString))
        Next

        Return CType(values.ToArray(GetType(AjaxControlToolkit.CascadingDropDownNameValue)), AjaxControlToolkit.CascadingDropDownNameValue())

    End Function

    <System.Web.Services.WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Shared Function getTrims(knownCategoryValues As String, category As String) As AjaxControlToolkit.CascadingDropDownNameValue()

        Dim dw As DataView
        Dim kv As System.Collections.Specialized.StringDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

        If Not kv.ContainsKey("MODEL") Then
            Return Nothing
        End If

        dw = LookupListNew.GetVSCTrimLookupList(kv("MODEL"), kv("MAKE"))
        Dim values As New ArrayList

        For Each dvr As DataRowView In dw
            values.Add(New AjaxControlToolkit.CascadingDropDownNameValue(dvr.Item("DESCRIPTION").ToString, dvr.Item("CODE").ToString))
        Next

        Return CType(values.ToArray(GetType(AjaxControlToolkit.CascadingDropDownNameValue)), AjaxControlToolkit.CascadingDropDownNameValue())
    End Function

    <System.Web.Services.WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Shared Function getYears(knownCategoryValues As String, category As String) As AjaxControlToolkit.CascadingDropDownNameValue()

        Dim dw As DataView
        Dim kv As System.Collections.Specialized.StringDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

        If Not kv.ContainsKey("TRIM") Then
            Return Nothing
        End If

        dw = LookupListNew.GetVSCYearsLookupList(kv("TRIM"), kv("MODEL"), kv("MAKE"))

        Dim values As New ArrayList

        For Each dvr As DataRowView In dw
            values.Add(New AjaxControlToolkit.CascadingDropDownNameValue(dvr.Item("DESCRIPTION").ToString, dvr.Item("CODE").ToString))
        Next

        Return CType(values.ToArray(GetType(AjaxControlToolkit.CascadingDropDownNameValue)), AjaxControlToolkit.CascadingDropDownNameValue())
    End Function
#End Region
#End Region

    Private Sub btnAdd200_Click(sender As Object, e As System.EventArgs) Handles btnAdd200.Click

        Dim i As Integer = ddlDummy.Items.Count
        Dim j As Integer = ddlDummy.Items.Count + 199

        For x As Integer = i To j
            ddlDummy.Items.Add("Dummy Item " + x.ToString)
        Next
    End Sub

    Private Sub btnAdd1000_Click(sender As Object, e As System.EventArgs) Handles btnAdd1000.Click

        Dim i As Integer = ddlDummy.Items.Count
        Dim j As Integer = ddlDummy.Items.Count + 999

        For x As Integer = i To j
            ddlDummy.Items.Add("Dummy Item " + x.ToString)
        Next
    End Sub

    Private Sub btnAdd10000_Click(sender As Object, e As System.EventArgs) Handles btnAdd10000.Click

        Dim i As Integer = ddlDummy.Items.Count
        Dim j As Integer = ddlDummy.Items.Count + 9999

        For x As Integer = i To j
            ddlDummy.Items.Add("Dummy Item " + x.ToString)
        Next
    End Sub
End Class

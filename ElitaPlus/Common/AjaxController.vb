Public Class AjaxController


#Region "Ajax Binding"

    Public Shared Function BindCascadingDropToDataView(Data As DataView, SelectItem As Guid, _
            Optional ByVal TextColumnName As String = "DESCRIPTION", _
            Optional ByVal GuidValueColumnName As String = "ID", _
            Optional ByVal AddNothingSelected As Boolean = True _
            ) As AjaxControlToolkit.CascadingDropDownNameValue()
        Dim i As Integer
        Dim values As New ArrayList
        Dim ajaxItem As AjaxControlToolkit.CascadingDropDownNameValue
        Dim res As AjaxControlToolkit.CascadingDropDownNameValue()
        Dim isDefault As Boolean
        Dim id As String

        If AddNothingSelected Then
            isDefault = False
            id = Guid.Empty.ToString
            If SelectItem.ToString = id Then
                isDefault = True
            End If
            ajaxItem = New AjaxControlToolkit.CascadingDropDownNameValue("", id, isDefault)
            values.Add(ajaxItem)
        End If
        If Data IsNot Nothing Then
            For i = 0 To Data.Count - 1
                isDefault = False
                id = New Guid(CType(Data(i)(GuidValueColumnName), Byte())).ToString
                If SelectItem.ToString = id Then
                    isDefault = True
                End If
                ajaxItem = New AjaxControlToolkit.CascadingDropDownNameValue( _
                    Data(i)(TextColumnName).ToString, id, isDefault)
                values.Add(ajaxItem)
            Next
        End If
        res = CType(values.ToArray(GetType(AjaxControlToolkit.CascadingDropDownNameValue)), AjaxControlToolkit.CascadingDropDownNameValue())
        Return res
    End Function

    'Public Sub SetSelectedItem(ByVal cascArray As AjaxControlToolkit.CascadingDropDownNameValue(), ByVal SelectItem As Guid)
    '    Dim item As ListItem = lstControl.SelectedItem
    '    If Not item Is Nothing Then item.Selected = False
    '    lstControl.Items.FindByValue(SelectItem.ToString).Selected = True

    '    cascArray[0].
    'End Sub

    Public Shared Function BindAutoComplete(prefixText As String, dv As DataView) As String()
        Dim arrDealers As New ArrayList

        If prefixText = "*" Then
            ' All 
            For i As Integer = 0 To dv.Count - 1
                arrDealers.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dv(i).Item("DESCRIPTION").ToString, GuidControl.ByteArrayToGuid(dv(i).Item("Id")).ToString))
            Next
        Else
            For i As Integer = 0 To dv.Count - 1
                If dv(i).Item("DESCRIPTION").ToString.ToUpper.StartsWith(prefixText.ToUpper) Then
                    arrDealers.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dv(i).Item("DESCRIPTION").ToString, GuidControl.ByteArrayToGuid(dv(i).Item("Id")).ToString))
                End If
            Next
            arrDealers.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND), Guid.Empty.ToString))
        End If


        Return CType(arrDealers.ToArray(GetType(String)), String())
    End Function


    Public Shared Function BindAutoComplete_SI(prefixText As String, dv As DataView) As String()
        Dim arrReports As New ArrayList

        If prefixText = "*" Then
            ' All 
            For i As Integer = 0 To dv.Count - 1
                arrReports.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dv(i).Item("REP_NAME").ToString, dv(i).Item("REP_ID").ToString))
            Next
        Else
            For i As Integer = 0 To dv.Count - 1
                If dv(i).Item("REP_NAME").ToString.ToUpper.StartsWith(prefixText.ToUpper) Then
                    arrReports.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dv(i).Item("REP_NAME").ToString, dv(i).Item("REP_ID").ToString))
                End If
            Next
        End If


        Return CType(arrReports.ToArray(GetType(String)), String())
    End Function
#End Region
    
#Region "Populate"

    Public Shared Function IsAutoCompleteEmpty(oTextBox As TextBox, oInp As HtmlInputHidden) As Boolean
        Return (oTextBox.Text Is String.Empty) OrElse (oTextBox.Text <> oInp.Value)
    End Function

    Public Shared Sub PopulateBOAutoComplete(oTextBox As TextBox, oInpDesc As HtmlInputHidden, _
                oInpId As HtmlInputHidden, bo As Object, propertyName As String)
        If IsAutoCompleteEmpty(oTextBox, oInpDesc) = True Then
            oInpId.Value = Guid.Empty.ToString
        End If

        ElitaPlusPage.PopulateBOProperty(bo, propertyName, New Guid(oInpId.Value))
    End Sub

#End Region

End Class

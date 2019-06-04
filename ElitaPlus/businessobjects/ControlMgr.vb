Public Class ControlMgr



#Region "CONSTANTS"

    Const COMPANY_FORM_EXCLUSIONS As String = "CompanyFormExclusions"
    Const VIEWONLY As String = "V"
    Const EDIT As String = "E"
    Public Const CONTROL_TABSTRIP As String = "tsHoriz"
    Public Const TABITEM_PREFIX As String = "tab_"
#End Region



#Region "CONSTRUCTOR"

    Public Sub New()
        'MyBase.DataPortalServerURL = ""
        'MyBase.DataServiceAssemblyName = "Assurant.ElitaPlus.DataServices"
        'MyBase.DataServiceClassName = "Assurant.ElitaPlus.DataServices.GenericExecute"
    End Sub

#End Region



#Region "ENUMERATIONS"

    Public Enum enumPermissionType
        VIEWONLY = 1
        EDIT = 2
        NONE = 3
    End Enum

    Public Enum SourceEvent
        onchange
    End Enum

    Public Enum TargetAction
        Visible
        InVisible
    End Enum

#End Region

#Region "Main"

    'Public Function GetControlAuthorization(ByVal sPageName As String, ByVal sNetworkID As String) As DataSet

    '    Dim oGenericBOCommand As New GenericExecuteStoredProc
    '    Dim oStoredProcData As New StoredProcData
    '    Dim oDBFactory As New DBFactory
    '    Dim oDB As New DbStruct
    '    Dim oDS As DataSet


    '    Dim oNetworkID1 As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter
    '    Dim oNetworkID2 As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter
    '    Dim oPageName As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter

    '    Dim SQL As String = oDB.GetSQL("SECURITY/GET_CONTROL_AUTHORIZATION")


    '    With oNetworkID1
    '        .ParameterName = "NETWORK_ID1"
    '        .Value = sNetworkID.ToUpper
    '    End With

    '    With oPageName
    '        .ParameterName = "FORM_CODE"
    '        .Value = sPageName.ToUpper
    '    End With

    '    With oNetworkID2
    '        .ParameterName = "NETWORK_ID2"
    '        .Value = sNetworkID.ToUpper
    '    End With

    '    With oStoredProcData
    '        .CommandText = SQL
    '        .CommandType = CommandType.Text
    '        .Params.Add(oNetworkID1)
    '        .Params.Add(oPageName)
    '        .Params.Add(oNetworkID2)
    '    End With

    '    oDS = oGenericBOCommand.ExecuteCommand(oStoredProcData)

    '    Return oDS


    'End Function

#End Region

#Region "Enable"

    Public Shared Sub DisableTabPanel(ByVal oRootControl As Control, ByVal sControlName As String, Optional ByVal blnEnabled As Boolean = False)
        Dim oTabStrip As Microsoft.Web.UI.WebControls.TabStrip
        Dim sTabItem As String
        Dim oTabItem As Microsoft.Web.UI.WebControls.TabItem

        oTabStrip = CType(oRootControl.FindControl(CONTROL_TABSTRIP), Microsoft.Web.UI.WebControls.TabStrip)
        If Not oTabStrip Is Nothing Then
            oTabStrip.SelectedIndex = -1

            '  If (sControlName.ToUpper().IndexOf(TABITEM_PREFIX.ToUpper()) < 0) Then
            'sTabItem = TABITEM_PREFIX & sControlName
            ' Else
            sTabItem = sControlName
            ' End If

            For Each oTabItem In oTabStrip.Items
                If oTabItem.ID = sTabItem Then
                    ' Found the TabItem
                    oTabItem.Enabled = blnEnabled
                    Exit For
                End If
            Next
        End If

    End Sub

    Public Shared Sub SetEnableTabStrip(ByVal oPage As ElitaPlusPage,
                                        ByVal oTabItem As Microsoft.Web.UI.WebControls.TabItem,
                                        ByVal isEnabled As Boolean)
        Dim sControlName As String = oTabItem.ID.Replace(TABITEM_PREFIX, String.Empty)

        If oPage.CanSetControlEnabled(sControlName) = True Then
            oTabItem.Enabled = isEnabled
        End If

    End Sub

    Public Shared Sub SetEnableTabID(ByVal oPage As ElitaPlusPage,
                                       ByVal oTabItem As Microsoft.Web.UI.WebControls.TabItem,
                                       ByVal isEnabled As Boolean)
        Dim sControlName As String = oTabItem.ID

        If oPage.CanSetControlEnabled(sControlName) = True Then
            oTabItem.Enabled = isEnabled
        End If

    End Sub

    Public Shared Sub SetEnableControl(ByVal oPage As ElitaPlusPage,
                                        ByVal oWebControl As WebControl,
                                        ByVal isEnabled As Boolean)
        If isEnabled = False Then 'always allow control to be disabled
            oWebControl.Enabled = isEnabled
        Else
            'check authorization when enable a control
            If oPage.CanEnableControl(oWebControl) = True Then
                oWebControl.Enabled = isEnabled
            End If
        End If
    End Sub

    Private Shared Sub EnableAGridControl(ByVal oControl As Control, ByVal enable As Boolean)
        Dim oWebControl As WebControl

        If TypeOf oControl Is Button Then Return
        If TypeOf oControl Is WebControl Then
            oWebControl = CType(oControl, WebControl)
            oWebControl.Enabled = enable
        End If
    End Sub

    ' This method enables or disables all the columns  for all the rows on an Editable DataGrid 
    Private Overloads Shared Sub EnableAllGridControls(ByVal grid As DataGrid, ByVal enable As Boolean)
        Dim row, column As Integer
        Dim oControl As Control

        For row = 0 To (grid.Items.Count - 1)
            For column = 0 To (grid.Items(row).Cells.Count - 1)
                oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column)
                EnableAGridControl(oControl, enable)
                If grid.Items(row).Cells(column).Controls.Count = ElitaPlusSearchPage.CELL_NEXT_TEMPLATE_CONTROL_SIZE Then
                    ' Image Button
                    oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column, True)
                    EnableAGridControl(oControl, enable)
                End If
            Next
        Next
    End Sub

    ' This method enables or disables all the columns  for all the rows on an Editable DataGrid 
    Private Overloads Shared Sub EnableAllGridControls(ByVal grid As GridView, ByVal enable As Boolean)
        Dim row, column As Integer
        Dim oControl As Control

        For row = 0 To (grid.Rows.Count - 1)
            For column = 0 To (grid.Rows(row).Cells.Count - 1)
                oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column)
                EnableAGridControl(oControl, enable)
                If grid.Rows(row).Cells(column).Controls.Count = ElitaPlusSearchPage.CELL_NEXT_TEMPLATE_CONTROL_SIZE Then
                    ' Image Button
                    oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column, True)
                    EnableAGridControl(oControl, enable)
                End If
            Next
        Next
    End Sub

    Public Overloads Shared Sub DisableAllGridControlsIfNotEditAuth(ByVal oPage As ElitaPlusPage, ByVal grid As DataGrid)
        If oPage.CanUpdateForm = False Then
            EnableAllGridControls(grid, False)
        End If

    End Sub

    Public Overloads Shared Sub DisableAllGridControlsIfNotEditAuth(ByVal oPage As ElitaPlusPage, ByVal grid As GridView)
        If oPage.CanUpdateForm = False Then
            EnableAllGridControls(grid, False)
        End If

    End Sub

    Public Overloads Shared Sub DisableAllGridControlsIfNotEditAuth(ByVal oPage As ElitaPlusPage, ByVal grid As GridView, ByVal disable As Boolean)
        If oPage.CanUpdateForm = False Or disable = True Then
            EnableAllGridControls(grid, False)
        End If

    End Sub

    Public Overloads Shared Sub DisableEditDeleteGridIfNotEditAuth(ByVal oPage As ElitaPlusPage, ByVal grid As DataGrid)
        If oPage.CanUpdateForm = False Then
            ElitaPlusSearchPage.SetGridControls(grid, False)
        End If

    End Sub

    Public Overloads Shared Sub DisableEditDeleteGridIfNotEditAuth(ByVal oPage As ElitaPlusPage, ByVal grid As GridView)
        If oPage.CanUpdateForm = False Then
            ElitaPlusSearchPage.SetGridControls(grid, False)
        End If

    End Sub
#End Region

#Region "Visible"

    Public Shared Sub SetVisibleControl(ByVal oPage As ElitaPlusPage,
                                        ByVal oControl As Control, ByVal isVisible As Boolean)
        If isVisible = False Then 'always allow a control to be hidden
            oControl.Visible = isVisible
        Else
            'check the authorization when show a control
            Dim sControlName As String = oControl.ID
            If (oPage.CanSetControlVisible(sControlName) = True) Then
                oControl.Visible = isVisible
            End If
        End If

    End Sub

    Public Shared Sub SetVisibleForControlFamily(ByVal oPage As ElitaPlusPage,
                                                    ByVal controlFamilyRoot As Control, ByVal isVisible As Boolean,
                                                    Optional ByVal includeRootControl As Boolean = False)
        Dim oWebcontrol As WebControl

        If (includeRootControl) Then
            If controlFamilyRoot.GetType.IsSubclassOf(GetType(WebControl)) Then
                oWebcontrol = CType(controlFamilyRoot, WebControl)
                SetVisibleControl(oPage, oWebcontrol, isVisible)
            End If
        End If
        Dim childControl As Control
        For Each childControl In controlFamilyRoot.Controls
            SetVisibleForControlFamily(oPage, childControl, isVisible, True)
        Next
    End Sub

#End Region

#Region "Client"

#Region "Visible"

    Private Shared Function SetClientVisibleControl(ByVal oPage As ElitaPlusPage, ByVal oControl As Control,
                                        ByVal isApply As String) As String
        Dim sJavaScript As String
        Dim sControlName As String = oControl.ID

        'If (oPage.CanUpdateControl(sControlName) = True) Then
        If (oPage.CanSetControlVisible(sControlName) = True) Then
            sControlName = sControlName.Replace("$", "_")
            sJavaScript = "obj = document.getElementById('" & sControlName & "');" & Environment.NewLine
            sJavaScript &= "if (obj != null) { obj.style.display=" & isApply & "; }" & Environment.NewLine
        End If

        Return sJavaScript
    End Function

    Private Shared Function SetClientVisibleFamily(ByVal oPage As ElitaPlusPage, ByVal controlFamilyRoot As Control,
                ByVal isApply As String, Optional ByVal includeRootControl As Boolean = False) As String
        Dim sJavaScript As String
        Dim childControl As Control
        Dim oWebcontrol As WebControl

        If (includeRootControl) Then
            If controlFamilyRoot.GetType.IsSubclassOf(GetType(WebControl)) Then
                oWebcontrol = CType(controlFamilyRoot, WebControl)
                sJavaScript = SetClientVisibleControl(oPage, oWebcontrol, isApply)
            End If
        End If
        For Each childControl In controlFamilyRoot.Controls
            sJavaScript &= SetClientVisibleFamily(oPage, childControl, isApply, True)
        Next
        Return sJavaScript
    End Function

    Public Shared Function AddMoreClientVisibleIds(ByVal moreIds As ArrayList,
                ByVal isApply As String) As String
        Dim sJavaScript, sId As String
        sJavaScript = String.Empty
        For Each sId In moreIds
            sJavaScript &= "obj = document.getElementById('" & sId & "');" & Environment.NewLine
            sJavaScript &= "if (obj != null) { obj.style.display=" & isApply & "; }" & Environment.NewLine
        Next

        Return sJavaScript
    End Function

    Private Shared Function SetClientVisibleAction(ByVal oPage As ElitaPlusPage, ByVal targetCtrl As Control,
                        ByVal appplyTrue As String, ByVal appyFalse As String,
                        ByVal moreIds As ArrayList) As String
        Dim sJavaScript As String

        sJavaScript = "var sDisplay; " & Environment.NewLine
        sJavaScript &= "if ( apply == true ) { " & Environment.NewLine
        sJavaScript &= appplyTrue
        sJavaScript &= "} " & Environment.NewLine
        sJavaScript &= "else { " & Environment.NewLine
        sJavaScript &= appyFalse
        sJavaScript &= "} " & Environment.NewLine
        ' Set The action for each control
        sJavaScript &= "(function(fDisplay) { " & Environment.NewLine
        sJavaScript &= "var obj; " & Environment.NewLine
        sJavaScript &= SetClientVisibleFamily(oPage, targetCtrl, "fDisplay")
        sJavaScript &= AddMoreClientVisibleIds(moreIds, "fDisplay")
        sJavaScript &= " })(sDisplay); " & Environment.NewLine
        Return sJavaScript
    End Function

#End Region

#Region "Main"

    ' If apply = True Then installs targetCtrl Action  Else  installs targetCtrl  No Action
    Private Shared Function SetClientAction(ByVal oPage As ElitaPlusPage, ByVal targetCtrl As Control,
                                ByVal action As TargetAction, ByVal moreIds As ArrayList) As String
        Dim sJavaScript As String
        '  Dim diplay As String

        Select Case action
            Case TargetAction.Visible
                sJavaScript = SetClientVisibleAction(oPage, targetCtrl,
                        "sDisplay = ''", "sDisplay = 'none'", moreIds)

            Case TargetAction.InVisible
                sJavaScript = SetClientVisibleAction(oPage, targetCtrl,
                        "sDisplay = 'none'", "sDisplay = ''", moreIds)
        End Select

        Return sJavaScript
    End Function

    ' if the event happens in the sourceCtrl then the registerName will be executed
    ' scriptContent Should return true if the ClientAction will be applied in the targetCtrl
    '   Otherwise the Not ClientAction will be applied in the targetCtrl
    Public Shared Sub ClientRegister(ByVal oPage As ElitaPlusPage, ByVal sourceCtrl As WebControl, ByVal sourceEvnt As SourceEvent,
                ByVal registerName As String, ByVal scriptContent As String,
                ByVal targetCtrl As Control, ByVal action As TargetAction, ByVal moreIds As ArrayList)
        Dim sJavaScript As String

        ' Installs sourceCtrl event
        sourceCtrl.Attributes.Add(SourceEvent.GetName(GetType(SourceEvent), sourceEvnt), registerName & "();")
        ' registerName
        sJavaScript = "<SCRIPT language='JavaScript' >" & Environment.NewLine
        sJavaScript &= "function " & registerName & "()" & Environment.NewLine
        sJavaScript &= "{ " & Environment.NewLine

        ' scriptContent Should return true if the ClientAction will be applied in the targetCtrl
        sJavaScript &= "    var apply = (function() { " & scriptContent & Environment.NewLine
        sJavaScript &= " })(); " & Environment.NewLine
        '  sJavaScript &= " alert('Apply = ' + apply); " & Environment.NewLine
        ' sJavaScript &= " debugger; " & Environment.NewLine

        ' SetClientAction
        sJavaScript &= SetClientAction(oPage, targetCtrl, action, moreIds)
        sJavaScript &= "} " & Environment.NewLine
        ' Invoke the Registered function
        sJavaScript &= registerName & "();" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        oPage.RegisterStartupScript(registerName, sJavaScript)
    End Sub

#End Region

#End Region

End Class

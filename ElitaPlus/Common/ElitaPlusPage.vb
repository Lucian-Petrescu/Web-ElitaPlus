Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration


Public Class ElitaPlusPage
    Inherits NavPage

#Region "Page Return Type"
    Public Class PageReturnType(Of T)
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public EditingBo As T
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal currentEditingBo As T, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = currentEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Constants"
    Public Const PRINCIPAL_SESSION_KEY As String = "PRINCIPAL_SESSION_KEY"
    Public Const SESSION_TRANSLATION As String = "TRANSLATION"

    Public Const DATE_FORMAT As String = "dd-MMM-yyyy"
    Public Const DATE_TIME_FORMAT As String = "dd-MMM-yyyy HH:mm:ss"
    Public Const DATE_TIME_FORMAT_12 As String = "dd-MMM-yyyy hh:mm:ss tt"
    Public Const DECIMAL_FORMAT As String = "N"



    Public Const PERCENT_FORMAT As String = "N4"

    Public Const CONTROL_WRITE_MARK As String = "_WRITE"
    Public Const CONTROL_TAB_MARK As String = "TabPanel"


    Private Const APP_HOME_URL_SESSION_KEY As String = "APP_HOME_URL_SESSION_KEY"

    Private Const TAB_HOME_URL_SESSION_KEY As String = "TAB_HOME_URL_SESSION_KEY"
    Public Const CONTROL_TAB_ITEMS_MARK As String = "tab_"
    Public Const CONTENT_PLACE_HOLDER As String = "BodyPlaceHolder"


    'Private oDSControlsNotAuthorized As DataSet


#End Region

#Region "Constants-DataGrid"
    Public Const NO_ITEM_SELECTED_INDEX As Integer = -1
    Public Const NO_PAGE_INDEX As Integer = 0
    Public Const BLANK_ITEM_SELECTED As Integer = 0
#End Region

#Region "MEMBERS"

#Region "VARIABLES"
    Private mPagePermissionType As FormAuthorization.enumPermissionType
    Private mDSControlAuthList As DataSet
#End Region

#Region "CONSTANTS"

    Private Const INTERNAL_PAGE_NAME As String = "Form1"
    '  Const DEFAULT_NETWORK_ID As String = "TSTUSR"
    Const DEFAULT_NETWORK_ID As String = "GA0166"
    Const DEFAULT_MACHINE_NAME As String = "127.0.0.1"

    Const PAGEBUTTON As String = "System.Web.UI.WebControls.Button"
    Const PAGELABEL As String = "System.Web.UI.WebControls.Label"
    Const PAGELINKBUTTON As String = "System.Web.UI.WebControls.LinkButton"
    Const PAGEDATAGRID As String = "System.Web.UI.WebControls.DataGrid"
    Protected Const CONFIRM_MESSAGE_OK As String = "Ok"
    Protected Const CONFIRM_MESSAGE_CANCEL As String = "Cancel"

    Public Const NOTHING_SELECTED_TEXT As String = "--Nothing Selected--"

    Public Const MSG_TYPE_INFO As String = "0"
    Public Const MSG_TYPE_CONFIRM As String = "1"
    Public Const MSG_TYPE_ALERT As String = "2"

    Public Const MSG_BTN_OK As String = "1"
    Public Const MSG_BTN_CANCEL As String = "2"
    Public Const MSG_BTN_OK_CANCEL As String = "3"
    Public Const MSG_BTN_YES_NO As String = "4"
    Public Const MSG_BTN_YES_NO_CANCEL As String = "5"

    Public Const MSG_VALUE_CANCEL As String = "0"
    Public Const MSG_VALUE_OK As String = "1"
    Public Const MSG_VALUE_YES As String = "2"
    Public Const MSG_VALUE_NO As String = "3"


#End Region

#Region "PROPERTIES"



    Private moTranslationProcess As TranslationProcess

    Protected Property PagePermissionType() As FormAuthorization.enumPermissionType
        Get
            If mPagePermissionType = 0 Then
                Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)
                mPagePermissionType = FormAuthorization.GetPermissions(sPageName)
            End If
            Return mPagePermissionType
        End Get
        Set(ByVal Value As FormAuthorization.enumPermissionType)
            mPagePermissionType = Value
        End Set
    End Property

    Protected ReadOnly Property ControlAuthorization() As DataSet
        Get
            If mDSControlAuthList Is Nothing Then
                Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)
                mDSControlAuthList = FormAuthorization.GetControlAuthorization(sPageName, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
            Return mDSControlAuthList
        End Get
    End Property

#End Region

#End Region

#Region "Public Enums"
    Public Enum DetailPageCommand
        Back
        BackOnErr
        Save
        Undo
        Delete
        New_
        NewAndCopy
        OK
        Cancel
        Accept
        Nothing_
        Redirect_
        GridPageSize
        GridColSort
        Expire
        ViewHistory
    End Enum
#End Region

#Region "Constructors For Page State"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal State As Object)
        MyBase.New(State)
    End Sub

    Public Sub New(ByVal useExistingState As Boolean)
        MyBase.New(useExistingState)
    End Sub

#End Region

#Region "SUBROUTINES AND FUNCTIONS"

    '-------------------------------------
    'Name:Print
    'Purpose:Print the page using the built in browser print function
    '-------------------------------------
    Public Sub Print()
        Dim messageContent As String
        messageContent = ("<script language=JavaScript> window.print() </script>")
        Me.Page.RegisterStartupScript("windowsprintcommand", messageContent)
    End Sub

    Public Function GetTranslationProcessReference() As TranslationProcess

        If moTranslationProcess Is Nothing Then

            moTranslationProcess = New TranslationProcess

        End If

        Return moTranslationProcess

    End Function


    '-------------------------------------
    'Name:TranslatePage
    'Purpose:Translate the dropdowns, grids and single value controls on a form.
    'Input Values:Form name and current language.
    'Uses:Translation Process
    '------------------------------------- 
    Public Sub TranslatePage(ByVal oTranslationProcess As TranslationProcess)

        Dim oControl As Control = GetRootOfPage()
        'pass the form which is the main container control and the language id.
        oTranslationProcess.TranslateThePage(oControl, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    End Sub

    '-------------------------------------
    'Name:GetAuthorization
    'Purpose:Check that the user has permission to view or edit the page.
    ' if not, redirect the user to the message page with a not authorized message popup.
    'Input Values:application user and the current tab.
    'Uses:FormAuthorizatonMgr
    '-------------------------------------
    Protected Sub ApplyAuthorization()

        Dim oRootControl As Control = GetRootOfPage()
        Dim sMessage As String
        Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf("/") + 1))
        'Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)

        mPagePermissionType = FormAuthorization.GetPermissions(sPageName)
        If mPagePermissionType = FormAuthorization.enumPermissionType.NONE Then

            'redirect to the message page if they are not authorized.
            sMessage = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.AUTHORIZATION_DENIED, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'Response.Redirect(ELPWebConstants.MESSAGE_PAGE_WITH_ERROR_INFO & sMessage)
            Throw New GUIException(sMessage, ELPWebConstants.AUTHORIZATION_DENIED)

        ElseIf mPagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then

            'disable all the controls within the page.
            '{vcp} assuming that at the page level, everything is allowed (_WRITE is not set)
            DisableControlGroup(oRootControl, False)

        End If

    End Sub

    Public Function CanUpdateForm() As Boolean
        Dim bCanUpdate As Boolean = False
        Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)
        Dim mPagePermissionType As FormAuthorization.enumPermissionType =
                FormAuthorization.GetPermissions(sPageName)
        If mPagePermissionType = FormAuthorization.enumPermissionType.EDIT Then
            bCanUpdate = True
        End If

        Return bCanUpdate
    End Function

    Public Function CanSetControlEnabled(ByVal sControlName As String) As Boolean
        Dim bCanUpdate As Boolean = True
        Dim oRow As DataRow
        Dim sDbControlName As String

        'Can't enabled for view only form
        If PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then
            bCanUpdate = False
        End If

        For Each oRow In ControlAuthorization.Tables(0).Rows
            'remove the underscore which is part of .net's hidden control collection.
            sDbControlName = oRow.Item("CONTROL_NAME").ToString.Remove(0, 1)
            ' Convert to UserControl Format
            '  sDbControlName = sDbControlName.Replace("._", ":")     2003
            sDbControlName = sDbControlName.Replace("._", "$")
            If sControlName = sDbControlName Then
                Dim sControlPermission As String = oRow.Item("PERMISSION_TYPE").ToString.Trim
                If PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY AndAlso sControlPermission = "E" Then
                    bCanUpdate = True
                ElseIf PagePermissionType = FormAuthorization.enumPermissionType.EDIT AndAlso sControlPermission <> "E" Then
                    bCanUpdate = False
                End If
                Exit For
            End If
        Next

        Return bCanUpdate
    End Function

    Public Function CanSetControlVisible(ByVal sControlName As String) As Boolean
        Dim bCanUpdate As Boolean = True
        Dim oRow As DataRow
        Dim sDbControlName As String

        For Each oRow In ControlAuthorization.Tables(0).Rows
            'remove the underscore which is part of .net's hidden control collection.
            sDbControlName = oRow.Item("CONTROL_NAME").ToString.Remove(0, 1)
            sDbControlName = sDbControlName.Replace("._", "$")
            If sControlName = sDbControlName AndAlso oRow.Item("PERMISSION_TYPE").ToString.Trim = "I" Then
                ' Can not Update Control
                bCanUpdate = False
                Exit For
            End If
        Next
        Return bCanUpdate
    End Function

    Public Function CanEnableControl(ByVal oWebControl As WebControl) As Boolean
        Dim bCanEnable As Boolean = True
        Dim sWebControlName As String = oWebControl.ID

        'Added OrElse PagePermissionType = FormAuthorization.enumPermissionType.EDIT
        'as if a page had both ViewOnly and Edit properties "CanSetControlEnabled"
        'was not getting called and the the ViewOnly property set for a DropDown at 
        'Control level was not working
        If (PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY OrElse
            PagePermissionType = FormAuthorization.enumPermissionType.EDIT) Then
            'check the authorization config if not panel/label
            If Not ((TypeOf oWebControl Is Panel) AndAlso (TypeOf oWebControl Is Label)) Then
                bCanEnable = CanSetControlEnabled(oWebControl.ID)
            End If
        End If
        Return bCanEnable
    End Function

    '-------------------------------------
    'Name:DisableUnauthorizedControls
    'Purpose:
    '1.Get a dataset of unauthorized controls.
    '2.Get the root container which is the page.
    '3. Loop thru the controls within the page and disable them by type and name.
    'Input Values:sNetworkID -user's id.
    '-------------------------------------

    Private Sub DisableUnauthorizedControls(ByVal sNetworkID As String)

        Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)

        'If oDSControlsNotAuthorized Is Nothing Then
        '    'oDSControlsNotAuthorized = FormAuthorization.GetControlAuthorization(sPageName, sNetworkID)
        '    oDSControlsNotAuthorized = Me.ControlAuthorization
        'End If

        If ControlAuthorization.Tables(0).Rows.Count = 0 Then Exit Sub

        Dim oControl As Control

        Dim oRootControl As Control = GetRootOfPage()
        Dim oRow As DataRow
        Dim sControlName As String, sPermissionType As String, blnEnabled As Boolean
        Dim sType As Type

        Dim nStartIndex As Integer
        Dim nTabNum As Integer

        For Each oRow In ControlAuthorization.Tables(0).Rows

            sPermissionType = oRow("PERMISSION_TYPE").ToString.Trim
            'remove the underscore which is part of .net's hidden control collection.
            '  sControlName = oRow.Item("CONTROL_NAME").ToString.Replace("_", String.Empty)
            sControlName = oRow.Item("CONTROL_NAME").ToString.Remove(0, 1)
            ' Convert to UserControl Format
            sControlName = sControlName.Replace("._", ":")

            'get the type of the control from the control collection.
            oControl = oRootControl.FindControl(sControlName)
            'Check if the control was found.  If not, it likely is within a content container of a 
            ' Master Page.  Loop though the child controls (works for more than just master pages)
            If oControl Is Nothing Then
                oControl = FindNestedControl(sControlName, oRootControl)
            End If

            'if the control is found 
            If Not oControl Is Nothing Then
                'DisableSingleControl(oControl)
                SetSingleControlStatusByPermissionTyppe(oControl, sPermissionType)
            End If
            ' To disable tab controls DEF -3297
            nStartIndex = sControlName.LastIndexOf(CONTROL_TAB_ITEMS_MARK)
            If nStartIndex >= 0 Then
                If sPermissionType = "I" Then
                    blnEnabled = False
                Else
                    blnEnabled = True
                End If
                Dim content As ContentPlaceHolder
                content = CType(Master.FindControl(CONTENT_PLACE_HOLDER), ContentPlaceHolder)
                'ControlMgr.DisableTabPanel(oRootControl, sControlName, blnEnabled)
                ControlMgr.DisableTabPanel(content, sControlName, blnEnabled)
            End If



        Next
    End Sub



    'ALR 02/23/2010 - Commented out to add new control containers for AJAX
    'Private Function FindNestedControl(ByVal ctlName As String, ByVal parentCtl As Control) As Control

    '    Dim ctl As Control

    '    For Each ctl In parentCtl.Controls
    '        If Not ctl.FindControl(ctlName) Is Nothing Then
    '            Return ctl.FindControl(ctlName)
    '        Else
    '            ctl = FindNestedChildControls(ctlName, ctl)
    '            If Not ctl Is Nothing Then
    '                Exit For
    '            End If
    '        End If
    '    Next

    '    Return ctl

    'End Function

    'Private Function FindNestedChildControls(ByVal ctlName As String, ByVal parentCtl As Control) As Control

    '    Dim ctl As Control

    '    For Each ctl In parentCtl.Controls
    '        If Not ctl.FindControl(ctlName) Is Nothing Then
    '            Return ctl.FindControl(ctlName)
    '        Else
    '            Return FindNestedChildControls(ctlName, ctl)
    '        End If
    '    Next

    'End Function

    Private Function FindNestedControl(ByVal ctlName As String, ByVal parentCtl As Control) As Control

        For Each ctrl As Control In parentCtl.Controls
            'Return if found
            If Not ctrl.FindControl(ctlName) Is Nothing Then Return ctrl.FindControl(ctlName)

            For Each currentControl As Control In ctrl.Controls
                'Return if found
                If Not currentControl.FindControl(ctlName) Is Nothing Then Return currentControl.FindControl(ctlName)

                If currentControl.GetType.Name = "TabContainer" Then
                    For Each currentContainer As Control In currentControl.Controls
                        'Return if found
                        If Not currentContainer.FindControl(ctlName) Is Nothing Then Return currentContainer.FindControl(ctlName)

                        If currentContainer.GetType.Name = "TabPanel" Then
                            For Each currentCtrl As Control In currentContainer.Controls
                                'Return if found
                                If Not currentCtrl.FindControl(ctlName) Is Nothing Then Return currentCtrl.FindControl(ctlName)

                                If currentCtrl.GetType.Name = "Control" Then
                                    For Each ctrltomodify As Control In currentCtrl.Controls
                                        'Return if found
                                        If Not ctrltomodify.FindControl(ctlName) Is Nothing Then Return ctrltomodify.FindControl(ctlName)

                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            'Not found, so check children
            ctrl = FindNestedChildControls(ctlName, ctrl)
            If Not ctrl Is Nothing Then
                Return ctrl
            End If
        Next

        'Last case - no control exists
        Return Nothing

    End Function

    Private Function FindNestedChildControls(ByVal ctlName As String, ByVal parentCtl As Control) As Control

        For Each ctrl As Control In parentCtl.Controls
            'Return if found
            If Not ctrl.FindControl(ctlName) Is Nothing Then Return ctrl.FindControl(ctlName)

            For Each currentControl As Control In ctrl.Controls
                'Return if found
                If Not currentControl.FindControl(ctlName) Is Nothing Then Return currentControl.FindControl(ctlName)

                If currentControl.GetType.Name = "TabContainer" Then
                    For Each currentContainer As Control In currentControl.Controls
                        'Return if found
                        If Not currentContainer.FindControl(ctlName) Is Nothing Then Return currentContainer.FindControl(ctlName)

                        If currentContainer.GetType.Name = "TabPanel" Then
                            For Each currentCtrl As Control In currentContainer.Controls
                                'Return if found
                                If Not currentCtrl.FindControl(ctlName) Is Nothing Then Return currentCtrl.FindControl(ctlName)

                                If currentCtrl.GetType.Name = "Control" Then
                                    For Each ctrltomodify As Control In currentCtrl.Controls
                                        'Return if found
                                        If Not ctrltomodify.FindControl(ctlName) Is Nothing Then Return ctrltomodify.FindControl(ctlName)

                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            'Not found, so check children
            ctrl = FindNestedChildControls(ctlName, ctrl)
            If Not ctrl Is Nothing Then
                Return ctrl
            End If
        Next

        'Last case - no control exists
        Return Nothing

    End Function
    '-------------------------------------
    'Name:GetRootOfPage
    'Purpose:helper functio to get the collection of controls from a page.
    '-------------------------------------
    Private Function GetRootOfPage() As Control

        If Page.FindControl(INTERNAL_PAGE_NAME) Is Nothing Then
            If Not Page.Master Is Nothing Then
                If Page.Master.FindControl(INTERNAL_PAGE_NAME) Is Nothing Then
                    Return Page.Master.Master.FindControl(INTERNAL_PAGE_NAME)
                Else
                    Return Page.Master.FindControl(INTERNAL_PAGE_NAME)
                End If
            End If
        Else
            Return Page.FindControl(INTERNAL_PAGE_NAME)
        End If

    End Function

    '-------------------------------------
    'Name:DisableControlGroup
    'Purpose:recursively loop thru the control collection and disable any controls found.
    'Input Values:any control can serve as a starting point. but most likely to start at page object.
    'Uses:DisableSingleControl
    '-------------------------------------
    Private Sub DisableControlGroup(ByVal oCurrentControl As Control, ByVal bWriteMarked As Boolean)

        Dim oControl As Control


        For Each oControl In oCurrentControl.Controls

            If oControl.ID <> String.Empty Then

                Me.DisableIfWriteControl(oControl, bWriteMarked)

            End If

            If oControl.HasControls Then

                ' if the value of bWriteMarked passed in the call (parent's value) is  true
                ' means that the parent is disabled, so propagate that value to all subsequent
                ' children

                Dim bCurrentWriteMarked As Boolean = True

                ' the only time the control is not considered "write marked" is when 
                ' its parent is not(indicated by passed value) and the control itself
                ' is not marked
                Try

                    If ((Not bWriteMarked) And ((oControl.ID Is Nothing) OrElse (Not oControl.ID.ToUpper.EndsWith(CONTROL_WRITE_MARK)))) Then
                        bCurrentWriteMarked = False
                    End If
                Catch ex As Exception
                    Throw ex
                End Try

                DisableControlGroup(oControl, bCurrentWriteMarked)

            End If

        Next

    End Sub

    '-------------------------------------
    'Name:SetSingleControlStatusByPermissionTyppe
    'Author: YX
    'Purpose:Set a single control depending on it's type name and permission type
    'Input Values:oCurrentControl
    '-------------------------------------
    Private Sub SetSingleControlStatusByPermissionTyppe(ByVal oCurrentControl As Control, ByVal strPermissionType As String)
        Dim oWebcontrol As WebControl
        Dim oUserControl As UserControl
        Try
            If oCurrentControl.GetType.IsSubclassOf(GetType(WebControl)) Then
                oWebcontrol = CType(oCurrentControl, WebControl)
                Select Case strPermissionType
                    Case "I"
                        oWebcontrol.Visible = False
                    Case "V"
                        oWebcontrol.Visible = True
                        'Logic added for AJAX Tab panel to disable child controls.
                        If oWebcontrol.GetType.Equals(GetType(AjaxControlToolkit.TabPanel)) Then
                            If oWebcontrol.HasControls Then
                                EnableDisableControlGroup(oWebcontrol, False)
                            End If
                        Else
                            oWebcontrol.Enabled = False
                        End If
                    Case "E"
                        oWebcontrol.Visible = True
                        oWebcontrol.Enabled = True
                End Select
            ElseIf oCurrentControl.GetType.IsSubclassOf(GetType(UserControl)) Then
                oUserControl = CType(oCurrentControl, UserControl)
                Select Case strPermissionType
                    Case "I"
                        oUserControl.Visible = False
                    Case "V"
                        oUserControl.Visible = (oUserControl.Visible And True)
                        EnableDisableControlGroup(oUserControl, False)
                    Case "E"
                        oUserControl.Visible = (oUserControl.Visible And True)
                        EnableDisableControlGroup(oUserControl, True)
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    '-------------------------------------
    'Name:EnableDisableControlGroup
    'Author: YX
    'Purpose:recursively loop thru the control collection and enable/disable any controls found.
    'Input Values:any control can serve as a starting point.
    'Uses:EnableDisableControlGroup
    '-------------------------------------
    Private Sub EnableDisableControlGroup(ByVal oCurrentControl As Control, ByVal blnEnable As Boolean)
        Try
            Dim oControl As Control
            For Each oControl In oCurrentControl.Controls
                If oControl.ID <> String.Empty Then
                    EnableDisableSingleWebControl(oControl, blnEnable)
                End If

                If oControl.HasControls Then
                    EnableDisableControlGroup(oControl, blnEnable)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    '-------------------------------------
    'Name:EnableDisableSingleWebControl
    'Author: YX
    'Purpose:Enable/Disable a single webControl
    'Input Values:oCurrentControl
    '-------------------------------------
    Private Sub EnableDisableSingleWebControl(ByVal oCurrentControl As Control, ByVal blnEnable As Boolean)
        Try
            Dim oWebcontrol As WebControl
            If oCurrentControl.GetType.IsSubclassOf(GetType(WebControl)) Then
                oWebcontrol = CType(oCurrentControl, WebControl)
                If Not oWebcontrol.GetType.ToString.Equals("System.Web.UI.WebControls.Label") _
                   AndAlso Not oWebcontrol.GetType.ToString.Equals("System.Web.UI.WebControls.Panel") Then
                    oWebcontrol.Enabled = blnEnable
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    '-------------------------------------
    'Name:DisableSingleControl
    'Purpose:Disable a single control depending on it's type name.
    'Input Values:oCurrentControl
    '-------------------------------------
    Private Sub DisableSingleControl(ByVal oCurrentControl As Control)

        Dim oWebcontrol As WebControl
        Dim oUserControl As UserControl

        If oCurrentControl.GetType.IsSubclassOf(GetType(WebControl)) Then
            oWebcontrol = CType(oCurrentControl, WebControl)
            oWebcontrol.Visible = False
        ElseIf oCurrentControl.GetType.IsSubclassOf(GetType(UserControl)) Then
            oUserControl = CType(oCurrentControl, UserControl)
            oUserControl.Visible = False
        End If

    End Sub

    '-------------------------------------
    'Name:DisableSingleWriteControl
    'Purpose:Disable a single control depending on it's type name. 
    'For buttons. Disables only the buttons with CommandName = 'WRITE'
    'Input Values:oCurrentControl
    '-------------------------------------
    Private Sub DisableIfWriteControl(ByVal oCurrentControl As Control, ByVal bWriteMarked As Boolean)

        Dim oWebcontrol As WebControl

        If oCurrentControl.GetType.IsSubclassOf(GetType(WebControl)) Then
            oWebcontrol = CType(oCurrentControl, WebControl)

            If Not oWebcontrol.GetType.ToString.Equals("System.Web.UI.WebControls.Label") AndAlso Not oWebcontrol.GetType.ToString.Equals("System.Web.UI.WebControls.Panel") Then
                If (oWebcontrol.ID.ToUpper.EndsWith(CONTROL_WRITE_MARK) Or (bWriteMarked)) Then
                    oWebcontrol.Enabled = False
                End If
            End If

        End If

    End Sub
    Private Sub DisableIfWriteControl(ByVal oCurrentControl As Control)

        Dim oWebcontrol As WebControl

        If oCurrentControl.GetType.IsSubclassOf(GetType(WebControl)) Then
            oWebcontrol = CType(oCurrentControl, WebControl)
            If oWebcontrol.ID.ToUpper.EndsWith(CONTROL_WRITE_MARK) Then
                oWebcontrol.Enabled = False
            End If
        End If

    End Sub

    Public Sub ApplyAllowNavigation()
        Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)
        If ((sPageName.ToUpper).StartsWith("NAVIGATION_") = False) AndAlso (sPageName.ToUpper <> "DEFAULT") _
           AndAlso (sPageName.ToUpper <> "HOMEFORM") AndAlso (sPageName.ToUpper <> "MAINPAGE") _
           AndAlso (sPageName.ToUpper <> "LOGINFORM") AndAlso (sPageName.ToUpper <> "ERRORFORM") Then
            If (sPageName.ToUpper = "NOTIFICATIONLISTFORM" AndAlso CType(Session("PageCalledFrom"), String) = "MAINPAGE") Then
                Me.MenuEnabled = False
            Else
                Me.MenuEnabled = FormAuthorization.AllowNavigation(sPageName)
            End If

        Else
            Me.MenuEnabled = True
        End If
    End Sub



#End Region

#Region "Page Events: Page_load"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Added check to see if a scriptmanager exists on the page.  If it does, do not render the WAIT
        '   script as it disables ajax scripting
        'Dim _scriptMan As ScriptManager = ScriptManager.GetCurrent(Page)
        'If _scriptMan Is Nothing Then
        '    AddResubmitBlock()
        'End If

        AddResubmitBlock()


        If (Me.GetType.Name.ToLower <> GetType(ErrorForm).Name.ToLower & "_aspx") AndAlso
           (Not Me.NavController Is Nothing AndAlso Not MyBase.useExistingState) Then
            Me.NavController.Navigate(Me)
        End If

        If Not Page.IsPostBack Then
            ApplyAllowNavigation()
        End If
        'This is to avoid looping
        If ((Me.GetType.Name.ToLower = GetType(LoginForm).Name.ToLower & "_aspx") Or
            (Me.GetType.Name.ToLower = GetType(ErrorForm).Name.ToLower & "_aspx") Or
            (Me.GetType.Name.ToLower = "default".ToLower & "_aspx")) Then
            Return 'Nothin to do
        End If


        ''We are not checking permissions for the Main Page which is just a container
        'If ((Me.GetType.Name.ToLower = GetType(MainPage).Name.ToLower & "_aspx")) Then
        '    '    Return 'Nothin to do
        'End If

        If (Me.GetType.BaseType.FullName = "Assurant.ElitaPlus.ElitaPlusWebApp.MainPage") Then
            Return 'Nothin to do
        End If



        'Get the authorization for the form and then translate the page.

        Dim oTranslationProcess As TranslationProcess = GetTranslationProcessReference()

        If Not Page.IsPostBack Then

            'We are not doing any security check for accessing the Calendar form page
            If Not (Me.GetType.Name.ToLower = GetType(CalendarForm).Name.ToLower & "_aspx") Then
                ApplyAuthorization()
                DisableUnauthorizedControls(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
            NavigationHistory.AddPageToHistory(Request.Path)
            TranslatePage(oTranslationProcess)
            ResizeButton(Me.Form)
        End If

        'Error Cleaning when starting
        Me._errCollection.Clear()
    End Sub

    Private Sub ResizeButton(ByVal containerCtl As Control)
        Dim ctl As Control, btn As Button, btnWidth As Integer
        Try
            For Each ctl In containerCtl.Controls
                If TypeOf ctl Is Button Then
                    btn = CType(ctl, Button)
                    'only resize those button with backgroud images
                    If Not (btn.Style.Item("background-image") = Nothing) Then
                        btnWidth = CInt(btn.Text.Length * 5.5 + 45)
                        If btn.Width.Value < btnWidth Then
                            btn.Style.Add("text-align", "left")
                            btn.Style.Add("padding-left", "18px")
                            btnWidth = CInt(btn.Text.Length * 5.8 + 18)
                            If btn.Width.Value < btnWidth Then
                                btn.Width = Unit.Pixel(btnWidth)
                            End If
                        End If
                    End If
                End If
                If ctl.Controls.Count > 0 Then
                    ResizeButton(ctl)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Security: Login Logic"

    'Public ReadOnly Property ApplicationHost() As String
    '    Get
    '        Return HttpContext.Current.Request.Url.Host
    '    End Get
    'End Property


    'Public Shared Function GetHttpProtocol() As String
    '    Dim sProtocol As String = "//"



    '    Return sProtocol

    'End Function

    'Private Function GetMachineDomain() As String
    '    Dim urlHost As String = Me.ApplicationHost.ToUpper
    '    Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(urlHost)
    '    Dim connHost As String = hostInfo.HostName.ToUpper
    '    Dim connPrefix, connMiddle As String
    '    Dim startIndex, subLength As Integer

    '    connPrefix = Nothing
    '    connMiddle = "NONE"
    '    startIndex = 0
    '    subLength = connHost.IndexOf(".", startIndex)
    '    If subLength > 0 Then
    '        connPrefix = connHost.Substring(startIndex, subLength)
    '        startIndex += subLength + 1
    '        subLength = connHost.IndexOf(".", startIndex) - startIndex
    '        If subLength > 0 Then
    '            connMiddle = connHost.Substring(startIndex, subLength)
    '            startIndex += subLength + 1
    '            subLength = connHost.IndexOf(".", startIndex) - startIndex
    '        End If
    '    End If

    '    Return connMiddle
    'End Function

    'Private Function GetMachineDomain() As String
    '    Dim sMachineDomain As String

    '    sMachineDomain = AppConfig.Config.GetSettingValue( _
    '                 AppConfig.MANUAL_PREFIX & "DOMAIN")
    '    If sMachineDomain = String.Empty Then
    '        ' Dynamic
    '        If AppConfig.CurrentEnvironment <> AppConfig.DEFAULT_ENVIRONMENT Then
    '            '  No Development
    '            sMachineDomain = Environment.MachineName.ToUpper.Substring(0, 4)
    '        Else
    '            ' Development
    '            sMachineDomain = AppConfig.DEFAULT_MACHINE_DOMAIN
    '        End If
    '    End If
    '    Return sMachineDomain
    'End Function

    'Private Function GetHubfromMachineDomain() As String
    '    Dim connType, connPrefix As String
    '    connPrefix = Nothing
    '    connType = Nothing

    '    If AppConfig.CurrentEnvironment <> AppConfig.DEFAULT_ENVIRONMENT Then
    '        '  No Development
    '        connPrefix = GetMachineDomain()
    '        '  Obtain Hub from custom.config based on connPrefix
    '        connType = AppConfig.Config.GetExternalValue(AppConfig.SERVER_PREFIX & connPrefix)
    '    End If

    '    Return connType
    'End Function

    ' It returns like EU, SA the HubRegion. NO means Development, No Region
    'Private Function GetConnectionType() As String
    '    Dim connHost As String = Me.ApplicationHost.ToUpper
    '    Dim connType, connPrefix, connMiddle, connSuffix As String
    '    Dim startIndex, subLength As Integer

    '    Session("ELITAPLUS_HOSTNAME") = "=" & connHost & "="

    '    connPrefix = Nothing
    '    connMiddle = Nothing
    '    connSuffix = Nothing
    '    connType = AppConfig.Config.GetSettingValue( _
    '                 AppConfig.MANUAL_PREFIX & "HUB")
    '    If connType = String.Empty Then
    '        ' Dynamic
    '        startIndex = 0
    '        subLength = connHost.IndexOf(".", startIndex)
    '        If subLength > 0 Then
    '            connPrefix = connHost.Substring(startIndex, subLength)
    '            startIndex += subLength + 1
    '            subLength = connHost.IndexOf(".", startIndex) - startIndex
    '            If subLength > 0 Then
    '                connMiddle = connHost.Substring(startIndex, subLength)
    '                startIndex += subLength + 1
    '                subLength = connHost.IndexOf(".", startIndex) - startIndex
    '                If subLength > 0 Then
    '                    connSuffix = connHost.Substring(startIndex)
    '                End If
    '            End If
    '        End If
    '        If ((Not connPrefix Is Nothing) AndAlso (Not connMiddle Is Nothing) AndAlso _
    '             (Not connSuffix Is Nothing) AndAlso _
    '             ((connPrefix = "ELITAPLUS") OrElse (connPrefix = "ELITA") OrElse (connPrefix = "W1") OrElse _
    '              (connPrefix = "ELITAPLUS-MODL") OrElse (connPrefix = "M1") OrElse _
    '              (connPrefix = "ELITAPLUS-TEST")) AndAlso _
    '             (connSuffix = "ASSURANT.COM")) Then
    '            ' It follows the rules
    '            connType = connMiddle
    '        Else
    '            ' May be it is a physical web server
    '            connType = GetHubfromMachineDomain()
    '            If connType = String.Empty Then
    '                ' Development or Default hub region
    '                connType = AppConfig.HubRegion
    '            End If

    '        End If
    '    End If
    '    Return connType
    'End Function

    'Private Sub StartLog()
    '    Dim isDebug As String = ConfigurationSettings.AppSettings("DEBUG_LOG")
    '    If isDebug Is Nothing Then
    '        isDebug = "FALSE"
    '    End If
    '    isDebug = isDebug.ToUpper
    '    If isDebug = "TRUE" Then
    '        AppConfig.SetModeLog(AppConfig.DB_LOG, True)
    '    Else
    '        AppConfig.SetModeLog(AppConfig.DB_LOG, False)
    '    End If
    'End Sub

    'Private Sub CreatePrincipal(ByVal networkID As String)
    '    Dim connType As String = GetConnectionType()
    '    Dim machineDomain As String = GetMachineDomain()
    '    Dim oServers As Servers
    '    Dim sHostname As String = CType(Session("ELITAPLUS_HOSTNAME"), String)

    '    'Now set the Principal
    '    Dim identity As New ElitaPlusIdentity("ASSURNET")
    '    With identity
    '        .HttpProtocol = GetHttpProtocol()
    '        .ConnectionType = connType
    '    End With

    '    Dim principal As New ElitaPlusPrincipal(identity)
    '    Session(PRINCIPAL_SESSION_KEY) = principal
    '    System.Threading.Thread.CurrentPrincipal = DirectCast(Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY), ElitaPlusPrincipal)
    '    StartLog()
    '    ElitaPlusPage.Debug(ELPWebConstants.ELITA_PLUS_VERSION & ", " & _
    '        sHostname & ", Region =" & connType & ", Prefix =" & machineDomain & ", " & _
    '        AppConfig.DataBase.Server)
    '    ' First Access to DB that is not for Log
    '    oServers = New Servers(connType, machineDomain)

    '    With identity
    '        .FtpHostname = oServers.FtpHostname
    '        .CeSdk = oServers.CrystalSdk
    '        .CeViewer = oServers.CrystalViewer
    '        .CeDrSdk = AppConfig.CE_NO_DR
    '        .CeDrViewer = AppConfig.CE_NO_DR

    '        .CreateUser(networkID)
    '    End With
    '    '    identity.CreateUser(networkID)
    '    'StartLog()
    '    'ElitaPlusPage.Debug(ELPWebConstants.ELITA_PLUS_VERSION & ", " & _
    '    '    sHostname & ", " & _
    '    '    AppConfig.DataBase.Server)
    'End Sub

    Protected Function PopulateUserSession(ByVal networkID As String, ByVal machineName As String) As Boolean
        Dim principal As ElitaPlusPrincipal
        Dim oAuthentication As New Authentication

        '-------------------------------------
        'Name:PopulateUserSession
        'Purpose:pass the networkname to the business object to load the 
        ' user's company and other info.
        'Input Values:
        'Uses:
        '-------------------------------------

        '  CreatePrincipal(networkID)
        principal = oAuthentication.CreatePrincipal(networkID)
        Session(PRINCIPAL_SESSION_KEY) = principal
        ' Dim CFG_MAIN_PAGE_NAME As String = ConfigurationMgr.ConfigValue(ELPWebConstants.MAIN_PAGE_NAME)
        Dim CFG_MAIN_PAGE_NAME As String = ELPWebConstants.MAIN_PAGE_NAME
        Dim CFG_AUTH_FAILED As String 'TODO Check This with Mark ' = ConfigurationMgr.ConfigValue("AUTHENTICATION_FAILED")
        Dim sLoginMessage As String
        '  Dim oLogin_Mgr As New LoginMgr
        Dim bResult As Boolean
        Dim nEnglishLanguageID As Guid


        If (ElitaPlusIdentity.Current.IsValidUser = True) Then

            'now check if it's a good login.
            'bResult = oLogin_Mgr.Login(networkID, Request.UserHostAddress, sLoginMessage, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'If bResult = True Then
            '    'save the elita user object into session.


            'Else
            '    'if the loginmgr returns false then send to message page.
            '    ' and append the message .
            '    'this means the login mgr failed.
            '    'sLoginMessage was loaded by the login mgr.
            '    Response.Redirect(ELPWebConstants.MESSAGE_PAGE_WITH_ERROR_INFO & sLoginMessage)

            'End If
        Else
            'if the applicationmgr returns not valid user then send to message page
            ' and set the language to english because it is unknown due to the login failure
            '   Dim translator As New TranslationProcess
            sLoginMessage = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.UI_INVALID_LOGIN_ERR_MSG, TranslationBase.Get_EnglishLanguageID)
            Response.Redirect(ErrorForm.PAGE_NAME & "?Message=" & sLoginMessage & "&")
        End If

        ''Now set the Principal
        'Dim identity As New ElitaPlusIdentity(networkID, "ASSURNET", GetHttpProtocol(), connectionType)
        'Dim principal As New ElitaPlusPrincipal(identity)
        'Session(PRINCIPAL_SESSION_KEY) = principal
    End Function
#End Region

#Region "Common Dropdown binding and GUID handling Helper Functions "
    Public Shared Sub BindListControlToArray(ByVal lstControl As ListControl, ByVal Data As ListItem(),
                                             Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal AddSelectSelected As Boolean = False,
                                             Optional ByVal nothingValue As String = "", Optional ByVal selectValue As String = "")
        Dim i As Integer
        lstControl.Items.Clear()
        If AddNothingSelected Then
            lstControl.Items.Add(New ListItem("", nothingValue))
        End If
        If AddSelectSelected Then
            lstControl.Items.Add(New ListItem("Select", selectValue))
        End If
        If Not Data Is Nothing Then
            For i = 0 To Data.Count - 1
                lstControl.Items.Add(Data(i))
            Next
        End If
    End Sub

    Public Shared Sub BindListControlToDataView(ByVal lstControl As ListControl, ByVal Data As DataView,
                                                Optional ByVal TextColumnName As String = "DESCRIPTION", Optional ByVal GuidValueColumnName As String = "ID",
                                                Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal SortByTextColumn As Boolean = True,
                                                Optional ByVal AddSelectSelected As Boolean = False,
                                                Optional ByVal AddOtherSelected As Boolean = False)
        Dim i As Integer
        lstControl.Items.Clear()
        If AddNothingSelected Then
            lstControl.Items.Add(New ListItem("", Guid.Empty.ToString))
        End If
        If AddSelectSelected Then
            lstControl.Items.Add(New ListItem("Select", Guid.Empty.ToString))
        End If
        If AddOtherSelected Then
            lstControl.Items.Add(New ListItem("Other", Guid.Empty.ToString))
        End If
        If Not Data Is Nothing Then
            If SortByTextColumn Then
                Data.Table.Locale = CultureInfo.CurrentCulture
                Data.Sort = TextColumnName
            End If
            For i = 0 To Data.Count - 1
                lstControl.Items.Add(New ListItem(Data(i)(TextColumnName).ToString, New Guid(CType(Data(i)(GuidValueColumnName), Byte())).ToString))
            Next
        End If
    End Sub
    Public Shared Sub BindCodeNameToListControl(ByVal lstControl As ListControl, ByVal Data As DataView,
                                                Optional ByVal TextColumnName1 As String = "DESCRIPTION", Optional ByVal TextColumnName2 As String = "CODE", Optional ByVal GuidValueColumnName As String = "ID",
                                                Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal SortByTextColumn As Boolean = True,
                                                Optional ByVal AddSelectSelected As Boolean = False)
        Dim i As Integer
        lstControl.Items.Clear()
        If AddNothingSelected Then
            lstControl.Items.Add(New ListItem("", Guid.Empty.ToString))
        End If
        If AddSelectSelected Then
            lstControl.Items.Add(New ListItem("Select", Guid.Empty.ToString))
        End If
        If Not Data Is Nothing Then
            If SortByTextColumn Then
                Data.Table.Locale = CultureInfo.CurrentCulture
                Data.Sort = TextColumnName1
            End If
            For i = 0 To Data.Count - 1
                lstControl.Items.Add(New ListItem(Data(i)(TextColumnName1).ToString + " (" + Data(i)(TextColumnName2).ToString + ")", New Guid(CType(Data(i)(GuidValueColumnName), Byte())).ToString))
            Next
        End If
    End Sub

    Public Shared Sub BindCodeToListControl(ByVal lstControl As ListControl, ByVal Data As DataView,
                                            Optional ByVal TextColumnName1 As String = "DESCRIPTION", Optional ByVal TextColumnName2 As String = "CODE",
                                            Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal SortByTextColumn As Boolean = True,
                                            Optional ByVal AddSelectSelected As Boolean = False)
        Dim i As Integer
        lstControl.Items.Clear()
        If AddNothingSelected Then
            lstControl.Items.Add(New ListItem("", String.Empty))
        End If
        If AddSelectSelected Then
            lstControl.Items.Add(New ListItem("Select", String.Empty))
        End If
        If Not Data Is Nothing Then
            If SortByTextColumn Then
                Data.Table.Locale = CultureInfo.CurrentCulture
                Data.Sort = TextColumnName1
            End If
            For i = 0 To Data.Count - 1
                lstControl.Items.Add(New ListItem(Data(i)(TextColumnName1).ToString, Data(i)(TextColumnName2).ToString))
            Next
        End If
    End Sub

    Public Shared Sub BindListTextToDataView(ByVal lstControl As ListControl, ByVal Data As DataView,
                                             Optional ByVal TextColumnName As String = "DESCRIPTION", Optional ByVal ValueColumnName As String = "ID",
                                             Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal SortByTextColumn As Boolean = True)
        Dim i As Integer
        lstControl.Items.Clear()
        If AddNothingSelected Then
            lstControl.Items.Add(New ListItem("", "0"))
        End If

        If Not Data Is Nothing Then
            If SortByTextColumn Then
                Data.Table.Locale = CultureInfo.CurrentCulture
                Data.Sort = TextColumnName
            End If
            For i = 0 To Data.Count - 1
                lstControl.Items.Add(New ListItem(Data(i)(TextColumnName).ToString, Data(i)(ValueColumnName).ToString))
            Next
        End If

    End Sub

    Public Sub SetSelectedItemByText(ByVal lstControl As ListControl, ByVal SelectItem As String)
        Dim item As ListItem = lstControl.SelectedItem
        If Not item Is Nothing Then item.Selected = False
        Try
            lstControl.Items.FindByText(SelectItem.ToString).Selected = True
            lstControl.Style.Remove("background")
        Catch ex As Exception
            lstControl.Style.Add("background", "red")
            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE) & ": ControlID : " & lstControl.ClientID & " for Text : " & SelectItem, ex)
        End Try
    End Sub

    Public Sub SetSelectedItem(ByVal lstControl As ListControl, ByVal SelectItem As String)
        Dim item As ListItem = lstControl.SelectedItem
        If Not item Is Nothing Then item.Selected = False
        Try
            lstControl.Items.FindByValue(SelectItem.ToString).Selected = True
            lstControl.Style.Remove("background")
        Catch ex As Exception
            lstControl.Style.Add("background", "red")
            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE) & ": ControlID : " & lstControl.ClientID & " for String : " & SelectItem, ex)
        End Try
    End Sub

    Public Shared Sub SetSelectedItem(ByVal lstControl As ListControl, ByVal SelectItem As Guid)
        Dim item As ListItem = lstControl.SelectedItem
        If Not item Is Nothing Then item.Selected = False
        Try
            lstControl.Items.FindByValue(SelectItem.ToString).Selected = True
            lstControl.Style.Remove("background")
        Catch ex As Exception
            lstControl.Style.Add("background", "red")
            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE) & ": ControlID : " & lstControl.ClientID & " for value : " & SelectItem.ToString(), ex)
        End Try
    End Sub

    Public Shared Sub BindSelectItem(ByVal sValue As String, ByVal oList As ListControl)
        Dim oItem As ListItem

        oList.DataTextField = "Description"
        oList.DataValueField = "ID"
        oList.DataBind()
        If sValue Is Nothing Then
            If oList.Items.Count > 0 Then
                oList.Items(0).Selected = True      ' Selects the first one
            End If
        Else
            ' Selects the value from the  form
            For Each oItem In oList.Items
                If oItem.Value = sValue Then
                    oItem.Selected = True
                    Exit For
                Else
                    oItem.Selected = False
                End If
            Next
        End If
        If oList.Items.Count > 0 AndAlso oList.Items(0).Text = NOTHING_SELECTED_TEXT Then
            oList.Items(0).Text = ""
        End If

    End Sub

    Public Shared Sub BindSelectItemByText(ByVal sText As String, ByVal oList As ListControl)
        Dim oItem As ListItem

        oList.DataTextField = "Description"
        oList.DataValueField = "ID"
        oList.DataBind()
        If sText Is Nothing Then
            If oList.Items.Count > 0 Then
                ' Selects the first one
                oList.Items(0).Selected = True
            End If
        Else
            ' Selects the Text from the list
            For Each oItem In oList.Items
                If oItem.Text Like "*" + sText Then
                    oItem.Selected = True
                    Exit For
                Else
                    oItem.Selected = False
                End If
            Next
        End If
        If oList.Items.Count > 0 AndAlso oList.Items(0).Text = NOTHING_SELECTED_TEXT Then
            oList.Items(0).Text = ""
        End If

    End Sub

    Public Shared Sub ClearList(ByVal aList As ListControl)
        Dim nCount As Integer = aList.Items.Count
        Dim nIndex As Integer

        For nIndex = 0 To nCount - 1
            aList.Items.RemoveAt(0)
        Next
    End Sub

    Public Shared Function GetSelectedDescription(ByVal lstControl As ListControl) As String
        If lstControl.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
            Return String.Empty
        Else
            Return lstControl.SelectedItem.Text
        End If
    End Function

    Public Shared Function GetSelectedValue(ByVal lstControl As ListControl) As String
        If lstControl.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
            Return String.Empty
        Else
            Return lstControl.SelectedItem.Value
        End If
    End Function

    Public Shared Function GetSelectedItem(ByVal lstControl As ListControl) As Guid
        If lstControl.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
            Return Guid.Empty
        Else
            Return New Guid(lstControl.SelectedItem.Value)
        End If
    End Function

    Public Shared Function GetGuidStringFromByteArray(ByVal value As Byte()) As String
        Return New Guid(value).ToString
    End Function

    Public Shared Function GetGuidStringFromByteArray(ByVal value As Object, ByVal IgnoreNull As String) As String

        Try
            Return New Guid(CType(value, Byte())).ToString
        Catch ex As Exception
            If Not IgnoreNull Is Nothing AndAlso IgnoreNull.Equals("true") Then
                Return Guid.Empty.ToString
            Else
                Throw ex
            End If
        End Try

    End Function

    Public Shared Function GetGuidFromString(ByVal value As String) As Guid
        Return New Guid(value)
    End Function

    Public Shared Function RemoveLastPercent(ByVal source As String) As String
        Dim sTarget As String = source
        If source.EndsWith("%") Then
            sTarget = source.Remove(source.Length - 1, 1)
        End If
        Return sTarget
    End Function

    ' *************************************************************************** '
    '   Sub GetSelectedList: Get list of selected items in a list control
    ' *************************************************************************** '
    Protected Function GetSelectedList(ByVal control As ListControl) As String()

        Dim strItems As New System.Collections.ArrayList

        If Not control Is Nothing Then

            Dim item As ListItem

            For Each item In control.Items
                If item.Selected Then
                    strItems.Add(item.Value)
                End If
            Next
        End If

        If strItems.Count = 0 Then
            Return Nothing
        Else
            Dim s(strItems.Count - 1) As String
            strItems.CopyTo(s)
            Return s
        End If

    End Function

    ' *************************************************************************** '
    '   Sub GetListValues: Get list of all items in a list control
    ' *************************************************************************** '
    Protected Function GetListValues(ByVal control As ListControl) As String()

        Dim strItems As New System.Collections.ArrayList

        If Not control Is Nothing Then

            Dim item As ListItem

            For Each item In control.Items
                strItems.Add(item.Value)
            Next
        End If

        If strItems.Count = 0 Then
            Return Nothing
        Else
            Dim s(strItems.Count - 1) As String
            strItems.CopyTo(s)
            Return s
        End If

    End Function

#End Region

#Region "Common Popup Dialogs And Poppup Messages Related Helper Functions"

    ' *************************************************************************** '
    '   Sub AddCalendar: Add a calendar popup to a control's onclick event
    ' *************************************************************************** '
    Public Sub AddCalendar(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "", Optional ByVal setDateTime As String = "N", Optional ByVal disablePreviousDates As String = "N")
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        'Dim url As String = "http://" & ServerName & AppPath & "/Common/CalendarForm.aspx"
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm.aspx"
        Dim textId As String = textbox.UniqueID.Replace(":", "_").Replace("$", "_")

        control.Attributes.Add("onclick", "javascript:return openCalendar('" & textId & "', '" & caller & "', '" & url & "', '" & setDateTime & "', '" & disablePreviousDates & "')")
    End Sub

    'Public Sub AddCalendar(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "", Optional ByVal setDateTime As String = "N")
    '    Dim AppPath As String = Request.ApplicationPath
    '    Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
    '    'Dim url As String = "http://" & ServerName & AppPath & "/Common/CalendarForm.aspx"
    '    Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm.aspx"
    '    Dim textId As String = textbox.UniqueID.Replace(":", "_").Replace("$", "_")

    '    control.Attributes.Add("onclick", "javascript:return openCalendar('" & textId & "', '" & caller & "', '" & url & "', '" & setDateTime & "')")
    'End Sub

    Public Sub AddCalendarwithTime(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "", Optional ByVal setDateTime As String = "N")
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarWithtimeForm.aspx"
        Dim textId As String = textbox.UniqueID.Replace(":", "_").Replace("$", "_")

        control.Attributes.Add("onclick", "javascript:return openCalendarwithtime('" & textId & "', '" & caller & "', '" & url & "', '" & setDateTime & "')")
    End Sub

    Public Sub AddCalendar_New(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "", Optional ByVal setDateTime As String = "N",
                               Optional ByVal disablePreviousDates As String = "N")
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        'Dim url As String = "http://" & ServerName & AppPath & "/Common/CalendarForm.aspx"
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm_New.aspx"
        Dim textId As String = textbox.UniqueID.Replace(":", "_").Replace("$", "_")

        control.Attributes.Add("onclick", "javascript:return openCalendar_New('" & textId & "', '" & caller & "', '" & url & "', '" & setDateTime & "', '" & disablePreviousDates & "')")
    End Sub

    Public Shared Sub SharedAddCalendarNew(ByVal control As System.Web.UI.WebControls.WebControl,
                                           ByVal textBox As System.Web.UI.WebControls.WebControl,
                                           Optional ByVal caller As String = "",
                                           Optional ByVal setDateTime As String = "N",
                                           Optional ByVal disablePreviousDates As String = "N")
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm_New.aspx"
        Dim textId As String = textBox.UniqueID.Replace(":", "_").Replace("$", "_")

        control.Attributes.Add("onclick", $"javascript:return openCalendar_New('{textId}', '{caller}', '{url}', '{setDateTime}', '{disablePreviousDates}')")
    End Sub

    Public Shared Sub AddCalendarNewWithEnableDates(ByVal control As System.Web.UI.WebControls.WebControl,
                                                    ByVal textBox As System.Web.UI.WebControls.WebControl,
                                                    Optional ByVal caller As String = "",
                                                    Optional ByVal setDateTime As String = "N",
                                                    Optional ByVal enabledDates As List(Of Date) = Nothing)
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm_New.aspx"
        Dim textId As String = textBox.UniqueID.Replace(":", "_").Replace("$", "_")
        Dim strEnabledDates As String = String.Empty
        If Not enabledDates Is Nothing AndAlso enabledDates.Count > 0 Then
            enabledDates.ForEach(Sub(d) strEnabledDates = $"{strEnabledDates}{d.ToString("yyyyMMdd")}|")
        End If
        control.Attributes.Add("onclick", $"javascript:return openCalendar_New_enableDates('{textId}', '{caller}', '{url}', '{setDateTime}', '{strEnabledDates}')")
    End Sub

    Public Shared Sub AddCalendarNewWithDisableBeforeDate(ByVal control As System.Web.UI.WebControls.WebControl,
                                                          ByVal textBox As System.Web.UI.WebControls.WebControl,
                                                          Optional ByVal caller As String = "",
                                                          Optional ByVal setDateTime As String = "N",
                                                          Optional ByVal disableBeforeDate As Date = Nothing)
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm_New.aspx"
        Dim textId As String = textBox.UniqueID.Replace(":", "_").Replace("$", "_")
        Dim strDisabledBeforeDate As String = String.Empty
        If disableBeforeDate = Nothing Then
            strDisabledBeforeDate = DateTime.Now.ToString("yyyyMMdd")
        Else
            strDisabledBeforeDate = disableBeforeDate.ToString("yyyyMMdd")
        End If
        control.Attributes.Add("onclick", $"javascript:return openCalendar_New_disableBeforeDate('{textId}', '{caller}', '{url}', '{setDateTime}', '{strDisabledBeforeDate}')")
    End Sub

    Public Sub AddCalendarwithTime_New(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "", Optional ByVal setDateTime As String = "N")
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarWithtimeForm_New.aspx"
        Dim textId As String = textbox.UniqueID.Replace(":", "_").Replace("$", "_")

        control.Attributes.Add("onclick", "javascript:return openCalendarwithtime_New('" & textId & "', '" & caller & "', '" & url & "', '" & setDateTime & "')")
    End Sub

    Public Sub AddConfirmation(ByVal control As System.Web.UI.WebControls.WebControl, ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)

        Dim sJavaScript As String = "javascript:return showMessage('" & translatedMsg & "', '" & "ConfirmationWindow" & "', '" & Me.MSG_BTN_YES_NO & "', '" & Me.MSG_TYPE_CONFIRM & "', '" & "null" & "');"

        control.Attributes.Add("onclick", sJavaScript)
    End Sub

    Public Sub AddConfirmationAndDisplayProgressBar(ByVal control As System.Web.UI.WebControls.WebControl, ByVal strConfirmationMsg As String, ByVal strProgressMsg As String, Optional ByVal translate As Boolean = True)

        DisplayProgressBarOnClick(CType(control, Button), strProgressMsg, translate)

        Dim translatedMsg As String = strConfirmationMsg
        If translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strConfirmationMsg)

        Dim sJavaScript As String = "if (showMessage('" & translatedMsg & "', '" & "ConfirmationWindow" & "', '" & Me.MSG_BTN_YES_NO & "', '" & Me.MSG_TYPE_CONFIRM & "', '" & "null" & "')){" + control.Attributes("onclick") + " }else{return false;}"

        control.Attributes("onclick") = sJavaScript

    End Sub

    Public Sub AddConfirmationAndDisplayNewProgressBar(ByVal control As System.Web.UI.WebControls.WebControl, ByVal strConfirmationMsg As String, ByVal strProgressMsg As String, Optional ByVal translate As Boolean = True)

        DisplayNewProgressBarOnClick(CType(control, Button), strProgressMsg, translate)

        Dim translatedMsg As String = strConfirmationMsg
        If translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strConfirmationMsg)

        Dim sJavaScript As String = "if (showMessage('" & translatedMsg & "', '" & "ConfirmationWindow" & "', '" & Me.MSG_BTN_YES_NO & "', '" & Me.MSG_TYPE_CONFIRM & "', '" & "null" & "')){" + control.Attributes("onclick") + " }else{return false;}"

        control.Attributes("onclick") = sJavaScript

    End Sub



    Public Sub AddInfoMsg(ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        'Session(SESSION_TRANSLATION) = translatedMsg
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        'sJavaScript &= "window.alert('" & translatedMsg & "');" & Environment.NewLine

        sJavaScript &= "try{resizeForm();}catch(e){} showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "');" & Environment.NewLine

        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub


    Public Sub AddConfirmMsg(ByVal strMsg As String, ByVal ReturnResponseIn As HtmlInputHidden, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        'Session(SESSION_TRANSLATION) = translatedMsg
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "try{resizeForm();}catch(e){} ConfirmAndSubmit('" & translatedMsg & "','" & ReturnResponseIn.ClientID & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

    Public Sub AddInfoMsgWithSubmit(ByVal strMsg As String, ByVal ReturnResponseIn As HtmlInputHidden, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        'Session(SESSION_TRANSLATION) = translatedMsg
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "try{resizeForm();}catch(e){} AlertAndSubmit('" & translatedMsg & "','" & ReturnResponseIn.ClientID & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

    Public Sub AddInfoMsgWithDelay(ByVal strMsg As String, Optional ByVal Translate As Boolean = True, Optional ByVal DelayMillSeconds As Integer = 500)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "try{resizeForm();}catch(e){} var t=setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "')""," & DelayMillSeconds.ToString & ");" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

    Public Sub AcctSettingTypeSubmit(ByVal strMsg As String, ByVal ReturnResponseIn As HtmlInputHidden, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "try{resizeForm();}catch(e){} AcctSettingTypeSubmit('" & translatedMsg & "','" & ReturnResponseIn.ClientID & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

    Public Sub AddControlMsg(ByVal control As System.Web.UI.WebControls.WebControl, ByVal strMsg As String, ByVal title As String, ByVal buttons As String, ByVal type As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        control.Attributes.Add("onclick", "javascript:return showMessage('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "', 'null');")
    End Sub
    Public Function GetTranslation(ByVal strUIPROGCODE As String, Optional ByVal Translate As Boolean = True) As String
        Dim translatedMsg As String = strUIPROGCODE
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strUIPROGCODE)
        Return translatedMsg
    End Function
    Public Sub DisplayMessage(ByVal strMsg As String, ByVal title As String, ByVal buttons As String, ByVal type As String, Optional ByVal ReturnResponseIn As HtmlInputHidden = Nothing, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String

        'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = translatedMsg
        ' Session(SESSION_TRANSLATION) = translatedMsg

        Dim id As String = "null"
        If Not ReturnResponseIn Is Nothing Then
            id = ReturnResponseIn.ClientID
        End If

        If Me.IsNewUI Then

            Me.SetDisplayMessageControls(translatedMsg, buttons, type, id)
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "revealModalAfterLoaded('modalPage');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine

            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)

        Else
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "try{resizeForm();}catch(e){} showMessageAfterLoaded('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "', '" & id & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine

            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)

        End If


        Dim isNewUI As Boolean = Me.IsNewUI

    End Sub

    Public Sub SetDisplayMessageControls(ByVal strMsg As String, ByVal buttons As String, ByVal type As String, Optional ByVal ReturnResponseId As String = Nothing)

        Dim imgMsgIcon As HtmlImage
        Dim tdBtnArea As HtmlTableCell
        Dim tdModalMessage As HtmlTableCell
        Dim modalTitleText As Label

        imgMsgIcon = CType(Me.Master.FindControl("imgMsgIcon"), HtmlImage)
        tdBtnArea = CType(Me.Master.FindControl("tdBtnArea"), HtmlTableCell)
        tdModalMessage = CType(Me.Master.FindControl("tdModalMessage"), HtmlTableCell)
        modalTitleText = CType(Me.Master.FindControl("modalTitleText"), Label)

        If type = "0" Then
            imgMsgIcon.Src = "~/Navigation/images/infoIcon.gif"
        ElseIf type = "1" Then
            imgMsgIcon.Src = "~/Navigation/images/questionIcon.gif"
        ElseIf type = "2" Then
            imgMsgIcon.Src = "~/Navigation/images/warningIcon.gif"
        End If
        Dim translatedMsg As String

        tdModalMessage.InnerHtml = strMsg

        If buttons = "1" Then
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("INFO")
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=OK value=OK onClick=evaluate(1,'{0}'); class='primaryBtn floatR'>", ReturnResponseId)
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("INFO")
        ElseIf buttons = "2" Then
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=Cancel value=Cancel onClick=evaluate(0,'{0}'); class='primaryBtn floatR'>", ReturnResponseId)
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
        ElseIf buttons = "3" Then
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("INFO")
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=OK value=OK onClick=evaluate(1,'{0});' class='primaryBtn floatR'><input id='button1' type=button name=Cancel value=Cancel onClick=evaluate(0,'{0}'); class='popWindowCancelbtn floatR'>", ReturnResponseId)
        ElseIf buttons = "4" Then
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=YES value=Yes onClick=evaluate(2,'{0}'); class='primaryBtn floatR' ><input id='button2' type=button name=NO value=No  onClick=evaluate(3,'{0}'); class='popWindowAltbtn floatR'>", ReturnResponseId)
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
        ElseIf buttons = "5" Then
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type='button' name='YES' value='Yes' class='primaryBtn floatR' onClick=evaluate(2,'{0}'); /><input id='button2' type='button' name='NO' value='No' class='popWindowAltbtn floatR' onClick=evaluate(3,'{0}'); ><input id='button3' type='button' name='Cancel' value='Cancel' class='popWindowCancelbtn floatR' onClick=evaluate(0,'{0}');  >", ReturnResponseId)
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
        ElseIf buttons = "6" Then
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=DEALER value=Dealer  onClick=evaluate(1,'{0}'); class='primaryBtn floatR'><input id='button2' type=button name=SERVICE_CENTER value='Service Center'  onClick='evaluate(2,'{0}'); class='altBtn floatR'>", ReturnResponseId)
        ElseIf buttons = "7" Then
            modalTitleText.Text = TranslationBase.TranslateLabelOrMessage("CONFIRM")
            tdBtnArea.InnerHtml = String.Format("<input id='button1' type=button name=YES value=Yes onClick=evaluate(2,'{0}'); class='primaryBtn floatR'>", ReturnResponseId)
        End If

    End Sub

    Public Sub DisplayMessageWithDelay(ByVal strMsg As String, ByVal title As String, ByVal buttons As String, ByVal type As String, Optional ByVal ReturnResponseIn As HtmlInputHidden = Nothing, Optional ByVal Translate As Boolean = True, Optional ByVal DelayMillSeconds As Integer = 500)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        Dim id As String = "null"
        If Not ReturnResponseIn Is Nothing Then
            id = ReturnResponseIn.ClientID
        End If
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "try{resizeForm();}catch(e){} var t=setTimeout(""showMessage('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "', '" & id & "')"", " & DelayMillSeconds.ToString & ");" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

    Public Sub DisplayMessageWithSubmit(ByVal strMsg As String, ByVal title As String, ByVal buttons As String, ByVal type As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String

        If Me.IsNewUI Then

            Me.SetDisplayMessageControls(translatedMsg, buttons, type, Nothing)
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "revealModal('modalPage');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine

            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)

        Else

            'Session(SESSION_TRANSLATION) = translatedMsg

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "try{resizeForm();}catch(e){} showMessageWithSubmit('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
        End If
    End Sub

    Public Sub InstallDisplayProgressBar()
        Dim lit As New System.Web.UI.WebControls.Literal
        'lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 300px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 117px"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm.aspx"" frameBorder=""no"" scrolling=""no""></IFRAME>"
        lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 300px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 142px"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm.aspx"" frameBorder=""no"" scrolling=""no""></IFRAME>"
        lit.ID = "__ProgressBarInternalFrame__"
        If Not PageForm Is Nothing AndAlso PageForm.FindControl(lit.ID) Is Nothing Then
            PageForm.Controls.Add(lit)
        End If
    End Sub

    Public Sub InstallDisplayNewProgressBar()
        Dim lit As New System.Web.UI.WebControls.Literal
        lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 400px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 142px"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm_New.aspx"" frameBorder=""0"" scrolling=""no""></IFRAME>"
        lit.ID = "__ProgressBarInternalFrame__"
        If Not PageForm Is Nothing AndAlso PageForm.FindControl(lit.ID) Is Nothing Then
            PageForm.Controls.Add(lit)
        End If
    End Sub

    Public Sub InstallReportViewer()
        Dim lit As New System.Web.UI.WebControls.Literal
        lit.Text = "<IFRAME id=""reportViewerFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 0px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 0px"" name=""reportViewerFrame"" marginWidth=""0"" src="""" frameBorder=""no"" scrolling=""no""></IFRAME>"
        'lit.Text = "<IFRAME id=""reportViewerFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 100%; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 100%"" name=""progressFrame"" marginWidth=""0"" src="""" frameBorder=""no"" scrolling=""no""></IFRAME>"
        lit.ID = "__ReportViewerFrame__"

        If Not PageForm Is Nothing AndAlso PageForm.FindControl(lit.ID) Is Nothing Then
            PageForm.Controls.Add(lit)
        End If

    End Sub
    Public Sub InstallProgressBar()
        Dim lit As New System.Web.UI.WebControls.Literal
        lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 450px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 168px"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm.aspx"" frameBorder=""no"" scrolling=""no""></IFRAME>"
        ' lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 100%; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 100%"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm.aspx"" frameBorder=""no"" scrolling=""no""></IFRAME>"
        lit.ID = "__ProgressBarInternalFrame__"

        If Not PageForm Is Nothing AndAlso PageForm.FindControl(lit.ID) Is Nothing Then
            PageForm.Controls.Add(lit)
        End If
        InstallReportViewer()
    End Sub

    Public Sub InstallDisplayNewReportProgressBar()
        Dim lit As New System.Web.UI.WebControls.Literal
        lit.Text = "<IFRAME id=""progressFrame"" style=""DISPLAY: none; Z-INDEX: 300; LEFT: 170px; OVERFLOW: hidden; WIDTH: 450px; PADDING-TOP: 0px; POSITION: absolute; TOP: 300px; HEIGHT: 162px"" name=""progressFrame"" marginWidth=""0"" src=""../Common/MessageProgressForm_New.aspx"" frameBorder=""0"" scrolling=""no""></IFRAME>"
        lit.ID = "__ProgressBarInternalFrame__"
        If Not PageForm Is Nothing AndAlso PageForm.FindControl(lit.ID) Is Nothing Then
            PageForm.Controls.Add(lit)
        End If
        InstallReportViewer()

    End Sub

    Public Sub HideHtmlElement(ByVal elementId As String)
        Dim sJavaScript As String

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "document.all.item('" & elementId & "').style.display = 'none';"
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("HideElement", sJavaScript)
    End Sub

    Public Sub ShowHtmlElement(ByVal elementId As String)
        Dim sJavaScript As String

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "document.all.item('" & elementId & "').style.display = '';"
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("showElement", sJavaScript)
    End Sub

    Public Sub DisplayProgressBarOnClick(ByVal onClickBtn As Button, ByVal msg As String, Optional ByVal translate As Boolean = True)
        If translate Then
            msg = TranslationBase.TranslateLabelOrMessage(msg)
        End If
        onClickBtn.Attributes.Add("onclick", "showProgressFrame('" & msg & "', '../Common/MessageProgressForm.aspx');")
        InstallDisplayProgressBar()
    End Sub

    Public Sub DisplayProgressBarOnClick(ByVal ctl As WebControl, ByVal msg As String, Optional ByVal translate As Boolean = True)
        If translate Then
            msg = TranslationBase.TranslateLabelOrMessage(msg)
        End If
        ctl.Attributes.Add("onclick", "showProgressFrame('" & msg & "', '../Common/MessageProgressForm.aspx');")
        InstallDisplayProgressBar()
    End Sub

    Public Sub DisplayNewProgressBarOnClick(ByVal onClickBtn As Button, ByVal msg As String, Optional ByVal translate As Boolean = True)
        If translate Then
            msg = TranslationBase.TranslateLabelOrMessage(msg)
        End If
        onClickBtn.Attributes.Add("onclick", "showNewProgressFrame('" & msg & "', '../Common/MessageProgressForm_New.aspx');")
        InstallDisplayNewProgressBar()
    End Sub

    Sub AddResubmitBlock()
        If Not PageForm Is Nothing Then
            PageForm.Attributes.Add("onsubmit", "return(! IsSubmitting());")
            Dim lit As New System.Web.UI.WebControls.Literal, strbuilder As New System.Text.StringBuilder
            strbuilder.Append("<script>")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("var _isSubmitting = false;")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("function IsSubmitting(){")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("var result = false;")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("if ((event) && (event.srcElement.id == '")
            strbuilder.Append(PageForm.ClientID)
            strbuilder.Append("')){")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("if (!_isSubmitting){")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("_isSubmitting = true;")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("document.body.style.cursor = 'wait';")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("}else{")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("result = true;}}")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("return result;}")
            strbuilder.Append(Environment.NewLine)
            strbuilder.Append("</script>")
            strbuilder.Append(Environment.NewLine)

            lit.Text = strbuilder.ToString
            lit.ID = "__ResubmitBlock__"
            PageForm.Controls.Add(lit)
        End If

    End Sub

    Protected Overloads Sub SetFocus(ByVal control As WebControl)
        Dim sJavaScript As String
        Dim controlId As String = control.UniqueID 'control.UniqueID.Replace(":", "_")
        sJavaScript = "<script language=""javascript"" > " & Environment.NewLine
        sJavaScript &= "try{" & Environment.NewLine
        sJavaScript &= "  SetFocusToElement(" & "'" & controlId & "')" & Environment.NewLine
        sJavaScript &= "   }catch(e){}" & Environment.NewLine
        sJavaScript &= "</script>" & Environment.NewLine
        Me.RegisterStartupScript("SetFocusScript_" & controlId, sJavaScript)
    End Sub

    Protected Sub SetFocusOnFirstEditableControl()
        Dim sJavaScript As String
        sJavaScript = "<script language=""javascript"" > " & Environment.NewLine
        sJavaScript &= "try{" & Environment.NewLine
        sJavaScript &= "SetFocusToFirstEditableElement()" & Environment.NewLine
        sJavaScript &= "   }catch(e){}" & Environment.NewLine
        sJavaScript &= "</script>" & Environment.NewLine
        Me.RegisterStartupScript("SetFocusScript", sJavaScript)
    End Sub


    ' *************************************************************************** '
    '   Sub WindowOpen: Displays a message with a list of validation errors
    ' *************************************************************************** '
    Protected Sub WindowOpen(ByVal url As String, ByVal name As String)
        Dim sJavaScript As String

        '   AddHiddenForWindowOpen()
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "var newWindow;" & Environment.NewLine
        sJavaScript &= "newWindow = windowOpen('" & url & "','" & name & "');" & Environment.NewLine
        sJavaScript &= "newWindow.document.title = '" & name & "';" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("MaximizedWindowOpen", sJavaScript)
    End Sub

    ' *************************************************************************** '
    '   Sub WindowOpen: Displays a message with a list of validation errors
    ' *************************************************************************** '
    Protected Sub WindowOpen(ByVal url As String)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "openSupportWindow('" & url & "','');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("MaximizedWindowOpen", sJavaScript)
    End Sub
    'to be renamed...
    Protected Sub myWindowOpen(ByVal url As String)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "mywindowOpen('" & url & "','');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("MaximizedWindowOpen", sJavaScript)
    End Sub

    Protected Shared Sub OpenPopup(ByVal url As String, ByVal callingPage As Page)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "mywindowOpen('" & url & "','');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        callingPage.RegisterStartupScript("MaximizedWindowOpen", sJavaScript)
    End Sub

    Public Sub HandleGridMessages(ByVal count As Int32, Optional ByVal checkForCount As Boolean = False)
        If (Me.IsNewUI) Then
            If count > 1000 AndAlso checkForCount Then
                'Me.DisplayMessage("Only the first 1000 records are shown. Please modify your search criteria.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.MasterPage.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, True)
            ElseIf count = 0 Then
                Me.MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            If count > 1000 AndAlso checkForCount Then
                'Me.DisplayMessage("Only the first 1000 records are shown. Please modify your search criteria.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.DisplayMessage(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            ElseIf count = 0 Then
                'Me.DisplayMessage("No records were found.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.DisplayMessage(Message.MSG_NO_RECORDS_FOUND, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        End If
    End Sub

    Public Sub SetProgressTimeOutScript(Optional ByVal baseController As String = "")
        Dim sJavaScript As String
        'Dim viewername As String
        'viewername = Reports.ReportCeBase.RptViewer.GetName(GetType(Reports.ReportCeBase.RptViewer), Reports.ReportCeBase.RptViewer.WINDOWOPEN)
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        '  sJavaScript &= "_isSubmitting = true;" & Environment.NewLine
        'sJavaScript &= "SetProgressTimeOut('" & viewername & "' ,'" & baseController & "');" & Environment.NewLine

        sJavaScript &= "SetProgressTimeOut('" & baseController & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("SetProgressTimeOutScript", sJavaScript)
    End Sub

    Public Sub InstallReportCe()

        Dim sJavaScript As String

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "EnableReportCe();" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("EnableReportCe", sJavaScript)
    End Sub

    Protected Sub CloseProgressBarParent(ByVal statusMsg As String, ByVal viewerName As String,
                                         ByVal errorMsg As String, ByVal rptAction As String,
                                         ByVal rptFtp As String)
        Dim sJavaScript As String
        Dim transMsg As String = TranslationBase.TranslateLabelOrMessage(statusMsg)

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.parent.CloseProgressBarParent('" & transMsg & "','" & viewerName & "','" & errorMsg & "','" & rptAction & "','" & rptFtp & "');" & Environment.NewLine
        If viewerName = Reports.ReportCeBase.RptViewer.GetName(GetType(Reports.ReportCeBase.RptViewer), Reports.ReportCeBase.RptViewer.WINDOWOPEN) Then
            ' Window Open
            sJavaScript &= "buttonClick();" & Environment.NewLine
        End If
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("CloseProgressBarParent", sJavaScript)
    End Sub
    Protected Sub CloseReportTimerControl(ByVal closetimer As String)
        Dim sJavaScript As String

        '   AddHiddenForWindowOpen()
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.parent.SetCloseTimerVariable('" & closetimer & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("CloseReportTimerControl", sJavaScript)
    End Sub

#End Region

#Region "Common Culture Related Helper Function"
    Public Shared Function GetDateFormattedStringNullable(ByVal value As Nullable(Of Date)) As String
        If (value.HasValue) Then
            Return GetDateFormattedString(value.Value)
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetLongDateFormattedStringNullable(ByVal value As Nullable(Of Date)) As String
        If (value.HasValue) Then
            Return GetLongDateFormattedString(value.Value)
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetLongDate12FormattedStringNullable(ByVal value As Nullable(Of Date)) As String
        If (value.HasValue) Then
            Return GetLongDate12FormattedString(value.Value)
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetDateFormattedString(ByVal value As Date) As String
        Dim LanguageCode = ElitaPlusIdentity.Current.ActiveUser.LanguageCode
        Dim formattedValue = CommonConfigManager.Current.LanguageManager.Get(LanguageCode).GetAwaiter().GetResult()?.FormatDate(value)
        Return formattedValue.ToString()
    End Function

    Public Shared Function GetLongDateFormattedString(ByVal value As Date) As String
        Dim LanguageCode = ElitaPlusIdentity.Current.ActiveUser.LanguageCode
        Dim formattedDate = CommonConfigManager.Current.LanguageManager.Get(LanguageCode).GetAwaiter().GetResult()?.FormatDate(value)
        Dim FormattedTime = CommonConfigManager.Current.LanguageManager.Get(LanguageCode).GetAwaiter().GetResult()?.FormatTime(value)
        Dim LongDateTimeValue = formattedDate + " " + FormattedTime
        Dim i As String = LongDateTimeValue.IndexOf(" ")
        If i <> -1 Then
            LongDateTimeValue = LongDateTimeValue.Substring(0, i + 9)
        End If
        Return LongDateTimeValue.ToString()
    End Function

    Public Shared Function GetLongDateFormattedStringWithFormat(ByVal value As Date, ByVal Format As String) As String
        'Return value.ToString(DATE_FORMAT, LocalizationMgr.CurrentCulture)
        Return value.ToString(Format, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    Public Shared Function GetLongDateFormattedStringWithFormat(ByVal value As Date?, ByVal format As String) As String
        'Return value.ToString(DATE_FORMAT, LocalizationMgr.CurrentCulture)
        If Not value.HasValue Then
            Return String.Empty
        End If

        Return GetLongDateFormattedStringWithFormat(CType(value.Value, Date), format)
    End Function

    Public Shared Function GetLongDate12FormattedString(ByVal value As Date) As String
        Dim LanguageCode = ElitaPlusIdentity.Current.ActiveUser.LanguageCode
        Dim formattedDate = CommonConfigManager.Current.LanguageManager.Get(LanguageCode).GetAwaiter().GetResult()?.FormatDate(value)
        Dim FormattedTime = CommonConfigManager.Current.LanguageManager.Get(LanguageCode).GetAwaiter().GetResult()?.FormatTime(value)
        Dim FormattedValue = formattedDate + " " + FormattedTime
        Return FormattedValue.ToString()
    End Function

    Public Shared Function GetAmountFormattedString(ByVal value As Decimal, Optional ByVal format As String = Nothing) As String
        If format Is Nothing Then format = DECIMAL_FORMAT
        Return value.ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    Public Shared Function GetAmountFormattedDoubleString(ByVal value As String, Optional ByVal format As String = Nothing) As String

        If format Is Nothing Then format = DECIMAL_FORMAT

        Return Convert.ToDouble(value).ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)

    End Function
    Public Shared Function GetAmountFormattedToString(ByVal value As String) As String
        Return Convert.ToString(value, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    'REQ-5773
    Public Shared Function GetAmountFormattedPercentString(ByVal value As Decimal, Optional ByVal format As String = Nothing) As String

        If format Is Nothing Then format = PERCENT_FORMAT
        Return value.ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function


    Public Shared Function GetAmountFormattedToVariableString(ByVal value As Decimal, Optional ByVal format As String = Nothing) As String
        Dim v As String = value.ToString()
        If (v.Contains(".")) Then
            Dim arr() As String = v.Split(CChar("."))
            Dim len As Integer = arr(1).Length
            format = "N0" & len.ToString()
        ElseIf (v.Contains(",")) Then
            Dim arr() As String = v.Split(CChar(","))
            Dim len As Integer = arr(1).Length
            format = "N0" & len.ToString()
        Else
            format = "N02"
        End If
        If format Is Nothing Then format = "N05"
        Return Convert.ToDecimal(value).ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)

    End Function
    Public Function IsPointFormat() As Byte
        Dim isPoint As Byte = 1
        Dim sFormat As String
        Dim sValue As String = "8.8"
        Dim dValue As String

        dValue = sValue.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        sFormat = GetAmountFormattedDoubleString(dValue)
        If sFormat.IndexOf(".") = -1 Then
            isPoint = 0
        End If

        Return isPoint

    End Function

    Public Sub SetDecimalSeparatorSymbol()
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "var IsPointFormat = '" & IsPointFormat() & "';" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.Page.RegisterStartupScript("Test", sJavaScript)
    End Sub

    Public Shared Function GetDecimalSeperator(ByVal culturecode As String) As String
        Dim decimalsep As String
        decimalsep = CultureInfo.CreateSpecificCulture(culturecode).NumberFormat.CurrencyDecimalSeparator
        Return decimalsep
    End Function

    Public Shared Function GetGroupSeperator(ByVal culturecode As String) As String
        Dim groupsep As String
        groupsep = CultureInfo.CreateSpecificCulture(culturecode).NumberFormat.CurrencyGroupSeparator
        Return groupsep
    End Function



#End Region

#Region " Business Object Routines "

    ' *************************************************************************** '
    '   Sub PopulateBOProperty: Overloaded routine to set the BO property for all
    '                           lookup lists
    ' *************************************************************************** '
    Public Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal comboBox As DropDownList,
                                  Optional ByVal isGuidValue As Boolean = True, Optional ByVal isStringValue As Boolean = False)

        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)

            If (isGuidValue = True) Then
                Dim value As Guid = GetSelectedItem(comboBox)
                piPropertyInfo.SetValue(bo, value, Nothing)
            ElseIf (isStringValue = True) Then
                Dim value As String = GetSelectedValue(comboBox)
                If Not (String.IsNullOrWhiteSpace(piPropertyInfo.GetValue(bo)) And String.IsNullOrWhiteSpace(value)) Then
                    piPropertyInfo.SetValue(bo, value, Nothing)
                End If
            Else
                Dim text As String = GetSelectedDescription(comboBox)
                piPropertyInfo.SetValue(bo, text, Nothing)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ' *********************************************************************************** '
    '   Sub PopulateBOProperty: Overloaded routine to set the BO property for a checkbox
    ' *********************************************************************************** '
    Protected Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal chkBox As CheckBox)

        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)

            Dim value As Boolean = chkBox.Checked

            piPropertyInfo.SetValue(bo, value, Nothing)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal tBox As TextBox)
        Dim lbl As Label
        Dim oColumn As DataGridColumn
        Dim sUniqueId As String
        Try
            Me.PopulateBOProperty(bo, propertyName, tBox.Text)
        Catch ex As Exception
            Try
                sUniqueId = GetValueFromPropertyName(bo, "UniqueId")
                ' Verifies whether  is a Detail Control
                lbl = Me.FindBoPropAssociatedLabel(bo.GetType, propertyName, sUniqueId)
                '  lbl = Me.FindBoPropAssociatedLabel(bo, propertyName)
                If Not lbl Is Nothing Then
                    lbl.ForeColor = Color.Red
                    Throw New PopulateBOPropException(propertyName, tBox, lbl, ex)
                Else

                    If TypeOf (Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(bo.GetType, propertyName, sUniqueId))) Is TemplateField Then
                        Dim grdColTmplFld As TemplateField = Me.FindBoPropAssociatedGridHeaderTmplFld(bo.GetType, propertyName, sUniqueId)
                        If Not grdColTmplFld Is Nothing Then
                            grdColTmplFld.HeaderStyle.ForeColor = Color.Red
                            Throw New PopulateBOPropException(propertyName, tBox, oColumn, ex)
                        End If
                    ElseIf TypeOf (Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(bo.GetType, propertyName, sUniqueId))) Is DataGridColumn Then
                        Dim assocGridColumn As DataGridColumn = Me.FindBoPropAssociatedGridHeader(bo.GetType, propertyName, sUniqueId)
                        If Not assocGridColumn Is Nothing Then
                            assocGridColumn.HeaderStyle.ForeColor = Color.Red
                            Throw New PopulateBOPropException(propertyName, tBox, oColumn, ex)
                        End If
                    End If
                End If

            Catch ex1 As Exception
                Me.ErrCollection.Add(ex1)
            End Try
        End Try
    End Sub

    ' *************************************************************************** '
    '   Sub PopulateBOProperty: Overloaded routine to set the BO property for all
    '                           lookup lists
    ' *************************************************************************** '
    Public Sub PopulateBundlesBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal tBox As TextBox)
        Dim lbl As Label
        Dim oColumn As DataControlField
        Dim sUniqueId As String
        Try
            Me.PopulateBOProperty(bo, propertyName, tBox.Text)
        Catch ex As Exception
            Try
                sUniqueId = GetValueFromPropertyName(bo, "UniqueId")
                ' Verifies whether  is a Detail Control
                lbl = Me.FindBoPropAssociatedLabel(bo.GetType, propertyName, sUniqueId)
                '  lbl = Me.FindBoPropAssociatedLabel(bo, propertyName)
                If Not lbl Is Nothing Then
                    lbl.ForeColor = Color.Red
                    Throw New PopulateBOPropException(propertyName, tBox, lbl, ex)
                Else
                    ' Verifies whether  is a Grid Control
                    '  oColumn = Me.FindBoPropAssociatedGridHeader(bo.GetType, propertyName)
                    oColumn = Me.FindBoPropAssociatedGridViewHeader(bo.GetType, propertyName, sUniqueId)
                    If Not oColumn Is Nothing Then
                        oColumn.HeaderStyle.ForeColor = Color.Red
                        '      errStrList(i) &= RemoveDecoration(assocGridColumn.HeaderText) & ":"
                        Throw New PopulateBOPropException(propertyName, tBox, oColumn, ex)
                    End If
                End If

            Catch ex1 As Exception
                Me.ErrCollection.Add(ex1)
            End Try
        End Try
    End Sub


    Public Function GetValueFromPropertyName(ByVal bo As Object, ByVal propertyName As String) As String
        Dim sPropertyValue As String
        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)
            sPropertyValue = CType(piPropertyInfo.GetValue(bo, Nothing), String)
            Return sPropertyValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetGuidValueFromPropertyName(ByVal bo As Object, ByVal propertyName As String) As Guid
        Dim oPropertyValue As Guid
        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)
            oPropertyValue = CType(piPropertyInfo.GetValue(bo, Nothing), Guid)
            Return oPropertyValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub PopulateControlFromPropertyName(ByVal bo As Object, ByVal oControl As Control, ByVal propertyName As String, Optional ByVal format As String = Nothing)
        Dim sPropertyValue As String
        Dim oPropertyValue As Guid
        Dim lbl As Label
        Dim oColumn As DataGridColumn
        Dim sUniqueId As String
        Try
            If TypeOf oControl Is Label Or TypeOf oControl Is TableCell _
               Or TypeOf oControl Is TextBox Or TypeOf oControl Is CheckBox Then
                sPropertyValue = GetValueFromPropertyName(bo, propertyName)
                PopulateControlFromBOProperty(oControl, sPropertyValue, format)
            ElseIf TypeOf oControl Is DropDownList Or TypeOf oControl Is ListBox Then
                oPropertyValue = GetGuidValueFromPropertyName(bo, propertyName)
                PopulateControlFromBOProperty(oControl, oPropertyValue, format)
            Else
                Throw New GUIException("Control Not Supported", "Control Not Supported")
            End If

        Catch ex As Exception
            Try
                sUniqueId = GetValueFromPropertyName(bo, "UniqueId")
                ' Verifies whether  is a Detail Control
                lbl = Me.FindBoPropAssociatedLabel(bo.GetType, propertyName, sUniqueId)
                If Not lbl Is Nothing Then
                    lbl.ForeColor = Color.Red
                    Throw New PopulateBOPropException(propertyName, oControl, lbl, ex)
                Else
                    ' Verifies whether  is a Grid Control
                    oColumn = Me.FindBoPropAssociatedGridHeader(bo.GetType, propertyName, sUniqueId)
                    If Not oColumn Is Nothing Then
                        oColumn.HeaderStyle.ForeColor = Color.Red
                        Throw New PopulateBOPropException(propertyName, oControl, oColumn, ex)
                    End If
                End If

            Catch ex1 As Exception
                Me.ErrCollection.Add(ex1)
            End Try
        End Try
    End Sub
    ' *************************************************************************** '
    '   Sub PopulateBOProperty: Overloaded routine to set the BO properties for all
    '                           form controls except lookup lists
    ' *************************************************************************** '
    Protected Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal value As String)

        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)
            'an array of types with 1 entry, the String Type
            Dim types() As Type = {GetType(String)}
            'set miMethodInfo to the 'Parse' method which takes a string as a parameter
            Dim miMethodInfo As System.Reflection.MethodInfo
            If (piPropertyInfo.PropertyType.IsGenericType) Then
                miMethodInfo = piPropertyInfo.PropertyType.GetGenericArguments()(0).GetMethod("Parse", types)
            Else
                miMethodInfo = piPropertyInfo.PropertyType.GetMethod("Parse", types)
            End If


            If value Is Nothing OrElse value.Trim.Length = 0 Then
                piPropertyInfo.SetValue(bo, Nothing, Nothing)
            ElseIf piPropertyInfo.PropertyType Is GetType(String) Then
                piPropertyInfo.SetValue(bo, value, Nothing)
            Else
                'Call the parse method on the property class to get the value for the property
                Dim strChkDateFormat As String = String.Empty
                Dim dt As DateTime

                If (miMethodInfo.ReturnType Is GetType(Assurant.Common.Types.DateType) OrElse miMethodInfo.ReturnType Is GetType(Date) _
                    OrElse miMethodInfo.ReturnType Is GetType(Assurant.Common.Types.DateTimeType) OrElse miMethodInfo.ReturnType Is GetType(DateTime)) Then
                    If (value.Length > DATE_TIME_FORMAT.Length) Then ' There are some places where value is set as DateType but page passes value as dd-MMM-yyyy 00:00:00.
                        strChkDateFormat = DATE_TIME_FORMAT_12
                    ElseIf (value.Length > DATE_FORMAT.Length) Then
                        strChkDateFormat = DATE_TIME_FORMAT
                    Else
                        strChkDateFormat = DATE_FORMAT
                    End If
                End If

                If (strChkDateFormat.Length > 0) Then
                    DateTime.TryParseExact(value, strChkDateFormat, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt)
                    If (dt <> DateTime.MinValue) Then
                        value = dt.ToString()
                    End If
                End If

                Dim args() As Object = {value}
                Dim newValue As Object = miMethodInfo.Invoke(bo, args)
                piPropertyInfo.SetValue(bo, newValue, Nothing)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal value As Boolean)

        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)
            'an array of types with 1 entry, the boolean Type
            Dim types() As Type = {GetType(Boolean)}
            'set miMethodInfo to the 'Parse' method which takes a boolean as a parameter
            Dim miMethodInfo As System.Reflection.MethodInfo = piPropertyInfo.PropertyType.GetMethod("Parse", types)

            If piPropertyInfo.PropertyType Is GetType(Boolean) Then
                piPropertyInfo.SetValue(bo, value, Nothing)
            Else
                'Call the parse method on the property class to get the value for the property
                Dim args() As Object = {value}
                Dim newValue As Object = miMethodInfo.Invoke(bo, args)
                piPropertyInfo.SetValue(bo, newValue, Nothing)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub PopulateBOProperty(ByVal bo As Object, ByVal propertyName As String, ByVal value As Guid)

        Try
            Dim piPropertyInfo As System.Reflection.PropertyInfo = bo.GetType.GetProperty(propertyName)
            'an array of types with 1 entry, the guid Type
            Dim types() As Type = {GetType(Guid)}
            'set miMethodInfo to the 'Parse' method which takes a guid as a parameter
            Dim miMethodInfo As System.Reflection.MethodInfo = piPropertyInfo.PropertyType.GetMethod("Parse", types)

            If (CType(value, Guid).Equals(Guid.Empty)) Then
                piPropertyInfo.SetValue(bo, Nothing, Nothing)
            ElseIf piPropertyInfo.PropertyType Is GetType(Guid) Then
                piPropertyInfo.SetValue(bo, value, Nothing)
            Else
                'Call the parse method on the property class to get the value for the property
                Dim args() As Object = {value}
                Dim newValue As Object = miMethodInfo.Invoke(bo, args)
                piPropertyInfo.SetValue(bo, newValue, Nothing)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetLabelsError(ByVal bo As Object, ByVal brokenRules() As String)
        Dim lbl As Label
        Dim propertyName, sUniqueId As String
        Dim i As Integer

        For i = 0 To brokenRules.Length - 1
            Dim parsedString() As String = brokenRules(i).Split(":"c)
            propertyName = parsedString(0)
            sUniqueId = GetValueFromPropertyName(bo, "UniqueId")
            lbl = Me.FindBoPropAssociatedLabel(bo.GetType, propertyName, sUniqueId)
            'lbl = Me.FindBoPropAssociatedLabel(bo, propertyName)
            If Not lbl Is Nothing Then
                lbl.ForeColor = Color.Red
            End If
        Next
    End Sub

    Public Shared Sub SetLabelError(ByVal lbl As Label)
        lbl.ForeColor = Color.Red
    End Sub

    Public Shared Sub ClearLabelError(ByVal lbl As Label)
        lbl.ForeColor = Color.Empty
    End Sub

    Private Function GetFromattedText(ByVal propertyValue As Object, Optional ByVal format As String = Nothing) As String
        Dim formattedValue As String

        If propertyValue Is Nothing Then
            formattedValue = String.Empty
        Else
            formattedValue = propertyValue.ToString.ToString(LocalizationMgr.CurrentFormatProvider)
            If (propertyValue.GetType Is GetType(DateType)) Then
                formattedValue = Me.GetDateFormattedString(CType(propertyValue, DateType).Value)
            ElseIf (propertyValue.GetType Is GetType(DateTimeType) And format IsNot Nothing) Then
                formattedValue = Me.GetLongDateFormattedStringWithFormat(CType(propertyValue, DateTimeType).Value, format)
            ElseIf (propertyValue.GetType Is GetType(DateTimeType)) Then
                formattedValue = Me.GetLongDateFormattedString(CType(propertyValue, DateTimeType).Value)
            ElseIf (propertyValue.GetType Is GetType(DateTime)) Then
                formattedValue = Me.GetLongDateFormattedString(CType(propertyValue, DateTime))
            ElseIf (propertyValue.GetType Is GetType(Date)) Then
                formattedValue = Me.GetDateFormattedString(CType(propertyValue, Date))
            ElseIf (propertyValue.GetType Is GetType(DecimalType)) Then
                formattedValue = Me.GetAmountFormattedString(CType(propertyValue, DecimalType).Value, format)
            ElseIf (propertyValue.GetType Is GetType(Decimal) OrElse propertyValue.GetType Is GetType(Double) OrElse propertyValue.GetType Is GetType(Single)) Then
                formattedValue = Me.GetAmountFormattedString(CType(propertyValue, Decimal), format)
            ElseIf (propertyValue.GetType Is GetType(Byte())) Then
                formattedValue = Me.GetGuidStringFromByteArray(CType(propertyValue, Byte()))
            End If

        End If
        Return formattedValue
    End Function


    Private Function GetFromattedCheckBox(ByVal propertyValue As Object, Optional ByVal format As String = Nothing) As Boolean
        Dim formattedValue As Boolean

        If propertyValue Is Nothing Then
            formattedValue = False
        Else
            formattedValue = Boolean.Parse(propertyValue.ToString)
        End If
        Return formattedValue
    End Function

    ' *************************************************************************** '
    '   Sub PopulateBOProperty: Routine to set the GUI control contents 
    '                           from the BO propety values
    ' *************************************************************************** '
    Public Sub PopulateControlFromBOProperty(ByVal oControl As Control, ByVal propertyValue As Object, Optional ByVal format As String = Nothing)
        Dim oLabel As Label
        Dim oTableCell As TableCell
        Dim oTextBox As TextBox
        Dim oCheckBox As CheckBox
        Dim oWeb As WebControl
        Dim oLinkButton As LinkButton

        Try
            ' Label
            If TypeOf oControl Is Label Then
                oLabel = CType(oControl, Label)
                oLabel.Text = GetFromattedText(propertyValue, format)
                ' TableCell
            ElseIf TypeOf oControl Is TableCell Then
                oTableCell = CType(oControl, TableCell)
                oTableCell.Text = GetFromattedText(propertyValue, format)
                'TextBox
            ElseIf (TypeOf oControl Is TextBox) Then
                oTextBox = CType(oControl, TextBox)
                oTextBox.Text = GetFromattedText(propertyValue, format)
                'CheckBox
            ElseIf TypeOf oControl Is CheckBox Then
                oCheckBox = CType(oControl, CheckBox)
                oCheckBox.Checked = GetFromattedCheckBox(propertyValue, format)
            ElseIf TypeOf oControl Is DropDownList Or TypeOf oControl Is ListBox Then
                Me.SetSelectedItem(CType(oControl, ListControl), CType(propertyValue, Guid))
            ElseIf (TypeOf oControl Is LinkButton) Then
                oLinkButton = CType(oControl, LinkButton)
                oLinkButton.Text = GetFromattedText(propertyValue, format)
            End If

        Catch ex As Exception
            Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE, ex)
        End Try

    End Sub

#End Region

#Region "Translation Functions"


    Public ReadOnly Property MissingTranslationsCount() As Integer
        Get

            Dim oTranslationProcess As TranslationProcess = GetTranslationProcessReference()
            Return oTranslationProcess.TranslationMissingCount

        End Get

    End Property

    Public Function ShowMissingTranslations(ByVal oErrorController As ErrorController) As String
        ShowMissingTranslations(DirectCast(oErrorController, IErrorController))
    End Function

    Public Function ShowMissingTranslations(ByVal oErrorController As IMessageController) As String
        ShowMissingTranslations(DirectCast(oErrorController, IErrorController))
    End Function

    Private Function ShowMissingTranslations(ByVal oErrorController As IErrorController) As String

        Dim sb As New StringBuilder
        Dim sText As String
        Dim bnotSemiColon As Boolean = False

        If Me.MissingTranslationsCount > 0 Then
            For Each sText In Me.MissingTranslations
                If sText.Trim <> ":" Then
                    bnotSemiColon = True
                    oErrorController.AddError(sText & ": is not Translated", False)
                End If
            Next
            If bnotSemiColon Then
                oErrorController.Show()
            End If
        End If


    End Function

    Public ReadOnly Property MissingTranslations() As ArrayList
        Get

            Dim oTranslationProcess As TranslationProcess = GetTranslationProcessReference()
            Return oTranslationProcess.TranslationMissingList()

        End Get

    End Property





    'Public Function TranslateLabelOrMessage(ByVal UIProgCode As String) As String
    '    Return TranslateLabelOrMessage(UIProgCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    'End Function

    'Public Function TranslateLabelOrMessage(ByVal UIProgCode As String, ByVal LangId As Guid) As String
    '    Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
    '    Dim oTranslationItem As New TranslationItem
    '    Dim Coll As New TranslationItemArray
    '    With oTranslationItem
    '        .TextToTranslate = UIProgCode.ToUpper
    '    End With
    '    Coll.Add(oTranslationItem)
    '    TransProcObj.TranslateList(Coll, LangId)
    '    Return oTranslationItem.Translation
    'End Function

    'Public Function TranslateLabelOrMessageList(ByVal UIProgCodes() As String) As String()
    '    Return TranslateLabelOrMessageList(UIProgCodes, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    'End Function

    'Public Function TranslateLabelOrMessageList(ByVal UIProgCodes() As String, ByVal LangId As Guid) As String()
    '    Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
    '    Dim Coll As New TranslationItemArray
    '    Dim Code As String
    '    For Each Code In UIProgCodes
    '        Dim oTranslationItem As New TranslationItem
    '        oTranslationItem.TextToTranslate = Code.ToUpper
    '        Coll.Add(oTranslationItem)
    '    Next
    '    TransProcObj.TranslateList(Coll, LangId)
    '    Dim Result(Coll.Count - 1) As String
    '    Dim i As Integer = 0
    '    Dim item As TranslationItem
    '    For Each item In Coll
    '        Result(i) = item.Translation
    '        i += 1
    '    Next
    '    Return Result
    'End Function

    Public Function TranslateDropdownItems(ByVal DropdownCode As String) As DataView
        Return TranslateDropdownItems(DropdownCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    End Function

    Public Function TranslateDropdownItems(ByVal DropdownCode As String, ByVal LangId As Guid) As DataView
        Return LookupListNew.DropdownLookupList(DropdownCode, LangId)
    End Function

    Public Sub TranslateControl(ByVal Control As WebControl)
        TranslateControl(Control, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    End Sub

    Public Sub TranslateControl(ByVal Control As WebControl, ByVal LangId As Guid)
        Dim ControlType As Type = Control.GetType
        Dim fInfo As System.Reflection.FieldInfo = ControlType.GetField("Text", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public)
        If Not fInfo Is Nothing Then
            Dim originalValue As String = CType(fInfo.GetValue(Control), String)
            If Not originalValue Is Nothing Then
                Dim newValue As String = TranslationBase.TranslateLabelOrMessage(originalValue, LangId)
                fInfo.SetValue(Control, newValue)
            End If
        End If
    End Sub

    Public Overloads Sub TranslateGridHeader(ByVal oGrid As System.Web.UI.WebControls.DataGrid)
        Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        TransProcObj.TranslateGridHeader(oGrid)
    End Sub

    Public Overloads Sub TranslateGridHeader(ByVal oGrid As System.Web.UI.WebControls.GridView)
        Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        TransProcObj.TranslateGridHeader(oGrid)
    End Sub

    Public Overloads Sub TranslateGridControls(ByVal oGrid As System.Web.UI.WebControls.DataGrid)
        Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        TransProcObj.TranslateControlsInGrid(oGrid)
    End Sub

    Public Overloads Sub TranslateGridControls(ByVal oGrid As System.Web.UI.WebControls.GridView)
        Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        TransProcObj.TranslateControlsInGrid(oGrid)
    End Sub

    Public Function TranslateBrokenRules(ByVal BrokenRules() As String, ByVal LangId As Guid) As String()
        Dim Result(BrokenRules.Length - 1) As String
        Dim i As Integer
        For i = 0 To BrokenRules.Length - 1
            Dim ParsedString() As String = BrokenRules(i).Split(":"c)
            Dim PropName As String = TranslationBase.TranslateLabelOrMessage(ParsedString(0), LangId)
            Dim Msg As String = TranslationBase.TranslateLabelOrMessage(ParsedString(1), LangId)
            Result(i) = PropName & ":" & Msg
        Next
        Return Result
    End Function

    Public Function TranslateBrokenRules(ByVal BrokenRules() As String) As String()
        Return TranslateBrokenRules(BrokenRules, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    End Function

    Public Sub TranslateControlsUnder(ByVal rootControl As System.Web.UI.Control)
        'pass the form which is the main container control and the language id.
        Me.GetTranslationProcessReference.TranslateThePage(rootControl, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    End Sub

#End Region

#Region "Error Handling"
    Private _errCollection As ArrayList = New ArrayList
    Private boPropToLabelBinding As Hashtable = New Hashtable
    Private boPropToGridHeaderBinding As Hashtable = New Hashtable

    Protected _onErr As Boolean = False

    Public ReadOnly Property ErrCollection() As ArrayList
        Get
            Return Me._errCollection
        End Get
    End Property


    Public ReadOnly Property OnErr() As Boolean
        Get
            Return Me._onErr
        End Get
    End Property


    'ARF Commented out on 9/8/2004. To use the more global error handler in Global.asax
    'Protected Overridable Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
    '    Dim oStArray As Array
    '    Dim sFrom, sFirstStack, sMessage As String
    '    Dim nPos As Integer
    '    Dim Ex As Exception = Server.GetLastError().GetBaseException
    '    Dim logEx As Exception = Ex
    '    If Not Ex.GetType.IsSubclassOf(GetType(ElitaPlusException)) Then
    '        Try
    '            Throw New UnHandledException(Ex)
    '        Catch ex1 As UnHandledException
    '            logEx = ex1
    '        End Try
    '    End If
    '    Log(logEx)
    '    oStArray = Microsoft.VisualBasic.Split(Ex.StackTrace, Environment.NewLine)
    '    sFirstStack = oStArray.GetValue(0).ToString
    '    nPos = sFirstStack.LastIndexOf(Path.DirectorySeparatorChar)
    '    sFrom = sFirstStack.Substring(nPos + 1)
    '    sMessage = (Ex.Message.Replace(":", " ") & " From " & sFrom.Replace(":", " ")).Replace(Environment.NewLine, " ")

    '    Response.Redirect(ErrorForm.PAGE_NAME & "?Message=" & sMessage & "&")
    'End Sub

    Protected Function GetFormattedAndTranslatedErrorsFromErrCollection() As String()
        If Me._errCollection.Count = 0 Then Return Nothing
        Dim exc As Exception
        Dim errStrList(Me._errCollection.Count - 1) As String
        Dim i As Integer = 0
        For Each exc In Me._errCollection
            errStrList(i) = ""
            If exc.GetType Is GetType(PopulateBOPropException) Then
                Dim popBoPropExc As PopulateBOPropException = CType(exc, PopulateBOPropException)
                With popBoPropExc
                    If Not .LabelControl Is Nothing Then
                        errStrList(i) &= Me.RemoveDecoration(.LabelControl.Text) & ":"
                    ElseIf Not .GridColumnControl Is Nothing Then
                        errStrList(i) &= Me.RemoveDecoration(.GridColumnControl.HeaderText) & ":"
                    Else
                        errStrList(i) &= .BoPropName & ":"
                    End If
                End With
                errStrList(i) &= TranslationBase.TranslateLabelOrMessage(exc.Message)
            ElseIf exc.GetType Is GetType(GUIException) Then
                Dim ex As ElitaPlusException = CType(exc, ElitaPlusException)
                errStrList(i) &= TranslationBase.TranslateLabelOrMessage(exc.Message) & ":" & ex.Code
            End If
            i += 1
        Next
        Return errStrList
    End Function

    Protected Function GetFormattedAndTranslatedErrorsFromBOValidationExc(ByVal validationExc As BOValidationException) As String()
        Dim err As Assurant.Common.Validation.ValidationError
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
            'Check if there is a label bound to the BO property
            ' Dim assocLbl As Label = FindBoPropAssociatedLabel(err.BusinessObjectType, err.PropertyName)
            Dim assocLbl As Label = FindBoPropAssociatedLabel(err.BusinessObjectType, err.PropertyName,
                                                              validationExc.UniqueId)
            If Not assocLbl Is Nothing Then
                assocLbl.ForeColor = Color.Red
                errStrList(i) &= RemoveDecoration(assocLbl.Text) & ":"
            End If
            'Check if there is a Grid Header bound to the BO property
            If TypeOf (Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(err.BusinessObjectType, err.PropertyName, validationExc.UniqueId))) Is TemplateField Then
                Dim grdColTmplFld As TemplateField = Me.FindBoPropAssociatedGridHeaderTmplFld(err.BusinessObjectType, err.PropertyName,
                                                                                              validationExc.UniqueId)
                If Not grdColTmplFld Is Nothing Then
                    grdColTmplFld.HeaderStyle.ForeColor = Color.Red
                    errStrList(i) &= RemoveDecoration(grdColTmplFld.HeaderText) & ":"
                End If
            ElseIf TypeOf (Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(err.BusinessObjectType, err.PropertyName, validationExc.UniqueId))) Is DataGridColumn Then
                Dim assocGridColumn As DataGridColumn = Me.FindBoPropAssociatedGridHeader(err.BusinessObjectType, err.PropertyName,
                                                                                          validationExc.UniqueId)
                If Not assocGridColumn Is Nothing Then
                    assocGridColumn.HeaderStyle.ForeColor = Color.Red
                    errStrList(i) &= RemoveDecoration(assocGridColumn.HeaderText) & ":"
                End If
            End If

            errStrList(i) &= preText & TranslationBase.TranslateLabelOrMessage(errMsg)
            i += 1
        Next
        Return errStrList
    End Function

    'Private Function GetUniqueKeyName(ByVal bo As Object, ByVal propName As String) As String
    '    Dim oldBo As ElitaPlusBusinessObject
    '    Dim newBo As BusinessObjectBase
    '    Dim keyName, sUniqueId, boName As String

    '    If TypeOf bo Is BusinessObjectBase Then
    '        ' New Framework
    '        newBo = CType(bo, BusinessObjectBase)
    '        sUniqueId = newBo.UniqueId.ToString
    '        boName = newBo.GetType.Name
    '    Else
    '        ' Old Framework
    '        oldBo = CType(bo, ElitaPlusBusinessObject)
    '        sUniqueId = oldBo.UniqueId.ToString
    '        boName = oldBo.GetType.Name
    '    End If
    '    keyName = (boName & "." & propName).ToUpper & "." & sUniqueId

    '    Return keyName
    'End Function

    ' New Framework
    Private Function GetUniqueKeyName(ByVal bo As IBusinessObjectBase, ByVal propName As String,
                                      ByVal uniqueId As String) As String
        Dim keyName As String

        keyName = (bo.GetType.Name & "." & propName).ToUpper & "." & uniqueId
        Return keyName
    End Function

    ' Old Framework
    'Private Function GetUniqueKeyName(ByVal bo As ElitaPlusBusinessObject, ByVal propName As String, _
    'ByVal uniqueId As String) As String
    '    Dim keyName As String

    '    keyName = (bo.GetType.Name & "." & propName).ToUpper & "." & uniqueId
    '    Return keyName
    'End Function

    'Type
    Private Function GetUniqueKeyName(ByVal boType As Type, ByVal propName As String,
                                      ByVal uniqueId As String) As String
        Dim keyName As String

        keyName = (boType.Name & "." & propName).ToUpper & "." & uniqueId
        Return keyName
    End Function

    Public Sub BindBOPropertyToLabel(ByVal bo As IBusinessObjectBase, ByVal propName As String, ByVal lbl As Label)
        Try
            If Not bo Is Nothing Then
                Me.boPropToLabelBinding.Item(GetUniqueKeyName(bo, propName, bo.UniqueId)) = lbl
            End If
        Catch ex As Exception
        End Try
    End Sub

    ' Old Framework
    'Public Sub BindBOPropertyToLabel(ByVal bo As ElitaPlusBusinessObject, ByVal propName As String, ByVal lbl As Label)
    '    Try
    '        'Me.boPropToLabelBinding.Item((bo.GetType.Name & "." & propName).ToUpper) = lbl
    '        Me.boPropToLabelBinding.Item(GetUniqueKeyName(bo, propName, bo.UniqueId)) = lbl
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Overloads Sub BindBOPropertyToGridHeader(ByVal bo As IBusinessObjectBase, ByVal propName As String, ByVal gridHeader As DataGridColumn)
        Try
            'Me.boPropToGridHeaderBinding.Item((bo.GetType.Name & "." & propName).ToUpper) = gridHeader
            Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(bo, propName, bo.UniqueId)) = gridHeader
        Catch ex As Exception
        End Try
    End Sub

    Public Overloads Sub BindBOPropertyToGridHeader(ByVal bo As IBusinessObjectBase, ByVal propName As String, ByVal gridHeader As DataControlField)
        Try
            'Me.boPropToGridHeaderBinding.Item((bo.GetType.Name & "." & propName).ToUpper) = gridHeader
            Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(bo, propName, bo.UniqueId)) = gridHeader
        Catch ex As Exception
        End Try
    End Sub

    'Protected Function FindBoPropAssociatedLabel(ByVal boType As Type, ByVal propName As String) As Label
    '    '   Return CType(Me.boPropToLabelBinding.Item((boType.Name & "." & propName).ToUpper), Label)
    '    Return CType(Me.boPropToLabelBinding.Item((boType.Name & "." & propName).ToUpper), Label)
    'End Function

    Protected Function FindBoPropAssociatedLabel(ByVal boType As Type, ByVal propName As String,
                                                 ByVal sUniqueId As String) As Label
        Dim oLabel As Label
        '   Return CType(Me.boPropToLabelBinding.Item((boType.Name & "." & propName).ToUpper), Label)
        oLabel = CType(Me.boPropToLabelBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), Label)
        If oLabel Is Nothing Then
            oLabel = CType(Me.boPropToLabelBinding.Item(GetUniqueKeyName(boType, propName, "")), Label)
        End If
        '  Return CType(Me.boPropToLabelBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), Label)
        Return oLabel
    End Function

    'Protected Function FindBoPropAssociatedGridHeader(ByVal boType As Type, ByVal propName As String) As DataGridColumn
    '    Return CType(Me.boPropToGridHeaderBinding.Item((boType.Name & "." & propName).ToUpper), DataGridColumn)
    'End Function

    Protected Overloads Function FindBoPropAssociatedGridHeader(ByVal boType As Type, ByVal propName As String,
                                                                ByVal sUniqueId As String) As DataGridColumn
        Dim oCol As DataGridColumn

        oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), DataGridColumn)
        If oCol Is Nothing Then
            oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, "")), DataGridColumn)
        End If

        '  Return CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), DataGridColumn)
        Return oCol
    End Function

    Protected Overloads Function FindBoPropAssociatedGridHeaderTmplFld(ByVal boType As Type, ByVal propName As String,
                                                                       ByVal sUniqueId As String) As TemplateField
        Dim oCol As TemplateField

        oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), TemplateField)
        If oCol Is Nothing Then
            oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, "")), TemplateField)
        End If

        Return oCol
    End Function

    Protected Overloads Function FindBoPropAssociatedGridViewHeader(ByVal boType As Type, ByVal propName As String,
                                                                    ByVal sUniqueId As String) As DataControlField
        Dim oCol As DataControlField

        oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), DataControlField)
        If oCol Is Nothing Then
            oCol = CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, "")), DataControlField)
        End If

        '  Return CType(Me.boPropToGridHeaderBinding.Item(GetUniqueKeyName(boType, propName, sUniqueId)), DataControlField)
        Return oCol
    End Function

    <Obsolete("Replace the call with ClearGridViewHeadersAndLabelsErrorSign method")>
    Public Sub ClearGridHeadersAndLabelsErrSign()
        Dim lbl As Label
        For Each lbl In Me.boPropToLabelBinding.Values
            lbl.ForeColor = Color.Empty
        Next
        Dim gridCol As DataGridColumn
        For Each gridCol In Me.boPropToGridHeaderBinding.Values
            gridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub

    <Obsolete("Replace the call with ClearGridViewHeadersAndLabelsErrorSign method")>
    Public Sub ClearGridViewHeadersAndLabelsErrSign()
        Dim lbl As Label
        For Each lbl In Me.boPropToLabelBinding.Values
            lbl.ForeColor = Color.Empty
        Next
        Dim gridCol As DataControlField
        For Each gridCol In Me.boPropToGridHeaderBinding.Values
            gridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub

    ''' <summary>
    ''' Clears Labels and Grid View Header Styles for Error.
    ''' </summary>
    ''' <remarks>This function works with both <see cref="DataGrid"/> and <see cref="GridView"/></remarks>
    Public Sub ClearGridViewHeadersAndLabelsErrorSign()
        Dim lbl As Label
        For Each lbl In Me.boPropToLabelBinding.Values
            lbl.ForeColor = Color.Empty
        Next
        Dim obj As Object
        For Each obj In Me.boPropToGridHeaderBinding.Values
            If (GetType(DataControlField).IsAssignableFrom(obj.GetType())) Then
                DirectCast(obj, DataControlField).HeaderStyle.ForeColor = Color.Empty
            ElseIf (GetType(DataGridColumn).IsAssignableFrom(obj.GetType())) Then
                DirectCast(obj, DataGridColumn).HeaderStyle.ForeColor = Color.Empty
            End If
        Next
    End Sub

    Public Sub ClearLabelErrSign(ByVal oLabel As Label)
        oLabel.ForeColor = Color.Empty
    End Sub

    Public Overridable Sub HandleErrors(ByVal exc As Exception, ByVal ErrorCtrl As ErrorController, Optional ByVal Translate As Boolean = True)
        HandleErrors(exc, DirectCast(ErrorCtrl, IErrorController), Translate)
    End Sub

    Public Overridable Sub HandleErrors(ByVal exc As Exception, ByVal ErrorCtrl As IMessageController, Optional ByVal Translate As Boolean = True)
        HandleErrors(exc, DirectCast(ErrorCtrl, IErrorController), Translate)
    End Sub

    Private Sub HandleErrors(ByVal exc As Exception, ByVal ErrorCtrl As IErrorController, Optional ByVal Translate As Boolean = True)
        If exc.GetType Is GetType(Threading.ThreadAbortException) OrElse
           (Not exc.InnerException Is Nothing AndAlso exc.InnerException.GetType Is GetType(Threading.ThreadAbortException)) Then
            Return
        End If

        If (exc.GetType() Is GetType(UnauthorizedException) OrElse exc.GetType() Is GetType(ServiceException)) Then
            Throw exc
        End If

        Dim errMsg As String
        _onErr = True
        Dim ex As ElitaPlusException

        If Not exc.GetType.IsSubclassOf(GetType(ElitaPlusException)) Then
            If Not exc.GetType.Name.Equals("FaultException") Then
                Try
                    Throw New UnHandledException(exc)
                Catch ex1 As UnHandledException
                    ex = ex1
                End Try
            End If
        Else
            ex = CType(exc, ElitaPlusException)
        End If

        If exc.GetType.Name.Equals("FaultException") Then
            ErrorCtrl.AddErrorAndShow(exc.Message, False)
        ElseIf ex.GetType Is GetType(PopulateBOErrorException) Then
            Dim populateErrList() As String = Me.GetFormattedAndTranslatedErrorsFromErrCollection
            ErrorCtrl.AddErrorAndShow(populateErrList, False)
        ElseIf ex.GetType Is GetType(BOValidationException) Then
            Dim boValidationErrList() As String = Me.GetFormattedAndTranslatedErrorsFromBOValidationExc(CType(ex, BOValidationException))
            ErrorCtrl.AddErrorAndShow(boValidationErrList, False)
        Else
            ErrorCtrl.AddErrorAndShow(ex.Code, Translate)
            If ex.GetType Is GetType(UnHandledException) AndAlso EnvironmentContext.Current.Environment = Environments.Development Then
                ErrorCtrl.AddErrorAndShow(ex.ToString, False)
            End If
        End If

        Me.Log(ex)
    End Sub

    Function GetOriginalExceptionMessage(ByVal exc As Exception) As String
        Dim curExc As Exception = exc
        While Not curExc.InnerException Is Nothing
            curExc = curExc.InnerException
        End While
        Return curExc.Message
    End Function

    'This method assumes you have called "BindBOPropertyToLabel" or BindBOPropertyToGridHeader      
    Public Sub AddLabelDecorations(ByVal bo As Object)
        Dim key As String
        Dim objType As Type = bo.GetType
        Dim attributes As Object()
        attributes = objType.GetCustomAttributes(GetType(ValidatorTypeDescriptorAttribute), True)


        For Each key In boPropToLabelBinding.Keys()
            If key.StartsWith(bo.GetType.Name.ToUpper & ".") Then
                Dim firstDot As Integer = key.IndexOf(".")
                Dim lastDot As Integer = key.LastIndexOf(".")
                Dim propLenght As Integer = lastDot - firstDot - 1
                '  Dim propName As String = key.Substring(key.IndexOf(".") + 1)
                Dim propName As String = key.Substring(firstDot + 1, propLenght)
                Dim lbl As Label = CType(boPropToLabelBinding.Item(key), Label)
                Dim pInfo As PropertyInfo = objType.GetProperty(propName, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)
                Dim mandatoryAttr As Object()
                Dim isMandatory As Boolean = False

                If Not pInfo Is Nothing Then
                    mandatoryAttr = pInfo.GetCustomAttributes(GetType(ValueMandatoryAttribute), True)
                    If Not mandatoryAttr Is Nothing AndAlso mandatoryAttr.Length > 0 Then
                        isMandatory = True
                    Else
                        'if no mandatory attributes, check the custom attributes to see if any are required.
                        mandatoryAttr = pInfo.GetCustomAttributes(GetType(ValidBaseAttribute), True)
                        For Each att As Object In mandatoryAttr
                            If Not att.GetType.Equals(GetType(ValidStringLengthAttribute)) AndAlso
                               Not att.GetType.Equals(GetType(ValidDateRangeAttribute)) AndAlso
                               Not att.GetType.Equals(GetType(ValidNumericRangeAttribute)) AndAlso
                               Not att.GetType.Equals(GetType(ValidIntervalDateAttribute)) AndAlso
                               Not att.GetType.Equals(GetType(ValidateDecimalNumberAttribute)) AndAlso
                               CType(att, ValidBaseAttribute).isMandatory(propName, bo) Then
                                isMandatory = True
                                Exit For
                            End If
                        Next
                    End If

                    If (Not isMandatory) Then

                        For i As Integer = 0 To attributes.Length - 1
                            Dim _validatorTypeDescriptorAttribute As ValidatorTypeDescriptorAttribute
                            Dim fields As FieldInfo()
                            Dim properties As PropertyInfo()
                            _validatorTypeDescriptorAttribute = CType(attributes(i), ValidatorTypeDescriptorAttribute)

                            '//validate the fields
                            fields = _validatorTypeDescriptorAttribute.DescriptorType.GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

                            For Each currField As FieldInfo In fields
                                If (currField.Name.ToUpper() = propName) Then
                                    mandatoryAttr = currField.GetCustomAttributes(GetType(ValueMandatoryAttribute), True)
                                    If Not mandatoryAttr Is Nothing AndAlso mandatoryAttr.Length > 0 Then
                                        isMandatory = True
                                    Else
                                        'if no mandatory attributes, check the custom attributes to see if any are required.
                                        mandatoryAttr = currField.GetCustomAttributes(GetType(ValidBaseAttribute), True)
                                        For Each att As Object In mandatoryAttr
                                            If Not att.GetType.Equals(GetType(ValidStringLengthAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidDateRangeAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidNumericRangeAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidIntervalDateAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidateDecimalNumberAttribute)) AndAlso
                                               CType(att, ValidBaseAttribute).isMandatory(propName, bo) Then
                                                isMandatory = True
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    Exit For
                                End If
                            Next

                            If (isMandatory) Then Exit For

                            '//validate the properties
                            properties = _validatorTypeDescriptorAttribute.DescriptorType.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

                            For Each currProperty As PropertyInfo In properties
                                If (currProperty.Name.ToUpper() = propName) Then
                                    mandatoryAttr = currProperty.GetCustomAttributes(GetType(ValueMandatoryAttribute), True)
                                    If Not mandatoryAttr Is Nothing AndAlso mandatoryAttr.Length > 0 Then
                                        isMandatory = True
                                    Else
                                        'if no mandatory attributes, check the custom attributes to see if any are required.
                                        mandatoryAttr = currProperty.GetCustomAttributes(GetType(ValidBaseAttribute), True)
                                        For Each att As Object In mandatoryAttr
                                            If Not att.GetType.Equals(GetType(ValidStringLengthAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidDateRangeAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidNumericRangeAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidIntervalDateAttribute)) AndAlso
                                               Not att.GetType.Equals(GetType(ValidateDecimalNumberAttribute)) AndAlso
                                               CType(att, ValidBaseAttribute).isMandatory(propName, bo) Then
                                                isMandatory = True
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If

                If isMandatory Then
                    'Add the Required Symbol in front of the Label 
                    If (Me.IsNewUI) Then
                        If Not lbl.Text.StartsWith("<span class=""mandatory"">*</span> ") Then
                            lbl.Text = "<span class=""mandatory"">*</span> " & lbl.Text
                        End If
                    Else
                        If Not lbl.Text.StartsWith("* ") Then
                            lbl.Text = "* " & lbl.Text
                        End If
                    End If

                End If
                If Not lbl.Text.EndsWith(":") Then
                    lbl.Text = lbl.Text & ":"
                End If
            End If
        Next
    End Sub

    'Protected Function RemoveDecoration(ByVal originalText As String) As String
    '    Dim retValue As String = originalText
    '    If retValue.StartsWith("* ") Then
    '        retValue = retValue.Substring(retValue.IndexOf("*") + 2)
    '    End If
    '    If retValue.EndsWith(":") Then
    '        retValue = retValue.Substring(0, retValue.Length - 1)
    '    End If
    '    If retValue.IndexOf(ElitaPlusSearchPage.UP_ARROW) >= 0 Then
    '        retValue = retValue.Substring(0, retValue.IndexOf(ElitaPlusSearchPage.UP_ARROW))
    '    ElseIf retValue.IndexOf(ElitaPlusSearchPage.DOWN_ARROW) >= 0 Then
    '        retValue = retValue.Substring(0, retValue.IndexOf(ElitaPlusSearchPage.DOWN_ARROW))
    '    End If
    '    Return retValue
    'End Function

    Protected Function RemoveDecoration(ByVal originalText As String) As String
        Dim retValue As String = originalText
        If retValue.StartsWith("&nbsp;") Then
            retValue = retValue.Replace("&nbsp;", "")
        End If
        If retValue.StartsWith("* ") Then
            retValue = retValue.Substring(retValue.IndexOf("*") + 2)
        End If
        If retValue.EndsWith(":") Then
            retValue = retValue.Substring(0, retValue.Length - 1)
        End If

        If retValue.IndexOf(ElitaPlusSearchPage.UP_ARROW) >= 0 Then
            retValue = retValue.Substring(0, retValue.IndexOf(ElitaPlusSearchPage.UP_ARROW))
        ElseIf retValue.IndexOf(ElitaPlusSearchPage.DOWN_ARROW) >= 0 Then
            retValue = retValue.Substring(0, retValue.IndexOf(ElitaPlusSearchPage.DOWN_ARROW))
        End If
        Return retValue
    End Function



#End Region

#Region "Master Page"
    Public ReadOnly Property MasterPage As MasterBase
        Get
            If (Me.Master Is Nothing) Then
                Return Nothing
            Else
                If (Me.Master.GetType().BaseType.BaseType.Equals(GetType(MasterBase))) Then
                    Return CType(Me.Master, MasterBase)
                Else
                    Return Nothing
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property IsNewUI() As Boolean
        Get
            If (Me.MasterPage Is Nothing) Then
                Return False
            Else
                If (Me.Master.GetType().BaseType.BaseType.Equals(GetType(MasterBase))) Then
                    If Not Me.MasterErrController Is Nothing Then Return True Else Return False
                    ' Return True
                ElseIf (Me.Master.GetType().BaseType.FullName = GetType(ElitaBase).FullName) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Get
    End Property
#End Region


#Region "Loging"
    Public Shared Sub Log(ByVal msg As String)
        Try
            'Dim APPLICATION_LOG_NAME As String = ConfigurationMgr.ConfigValue("LogCategoryName") 'Web
            'Dim APPLICATION_NAME As String = ConfigurationMgr.ConfigValue("LogAppName") ' ElitaPlus
            'Dim EventLog1 As New System.Diagnostics.EventLog(APPLICATION_NAME, ".", APPLICATION_LOG_NAME)
            'EventLog1.WriteEntry(Msg)
            AppConfig.Log(msg)
        Catch ex1 As Exception
        End Try
    End Sub

    Public Shared Sub Log(ByVal ex As Exception)
        Try
            'Dim APPLICATION_LOG_NAME As String = ConfigurationMgr.ConfigValue("LogCategoryName") 'Web
            'Dim APPLICATION_NAME As String = ConfigurationMgr.ConfigValue("LogAppName") ' ElitaPlus
            'Dim EventLog1 As New System.Diagnostics.EventLog(APPLICATION_NAME, ".", APPLICATION_LOG_NAME)
            'EventLog1.WriteEntry(Msg)
            AppConfig.Log(ex)
        Catch ex1 As Exception
        End Try
    End Sub

    'Public Shared Sub Debug(ByVal msg As String)
    '    Try
    '        'Dim APPLICATION_LOG_NAME As String = ConfigurationMgr.ConfigValue("LogCategoryName") 'Web
    '        'Dim APPLICATION_NAME As String = ConfigurationMgr.ConfigValue("LogAppName") ' ElitaPlus
    '        'Dim EventLog1 As New System.Diagnostics.EventLog(APPLICATION_NAME, ".", APPLICATION_LOG_NAME)
    '        'EventLog1.WriteEntry(Msg)
    '        AppConfig.DebugLog(msg)
    '    Catch ex1 As Exception
    '    End Try
    'End Sub

    Public Shared Shadows Sub Trace(ByVal page As ElitaPlusPage, ByVal msg As String)
        Dim id As String = page.Session.SessionID
        Dim pageName As String = page.ToString
        AppConfig.DebugMessage.Trace(id, pageName, msg & "@ SCount = " & page.Navigator.NavStackCount.ToString)
    End Sub

#End Region

#Region "Menu Navigation"

    Public WriteOnly Property MenuEnabled() As Boolean
        'Get
        '    Dim CurrMenuState As ELPWebConstants.enumMenu_State = ELPWebConstants.enumMenu_State.View_Page_Mode
        '    'load or retrieve the menustate from the session.
        '    If Not HttpContext.Current.Session(ELPWebConstants.MENUSTATE) Is Nothing Then
        '        CurrMenuState = CType(HttpContext.Current.Session(ELPWebConstants.MENUSTATE), ELPWebConstants.enumMenu_State)
        '    End If
        '    Return CurrMenuState = ELPWebConstants.enumMenu_State.View_Page_Mode
        'End Get
        Set(ByVal Value As Boolean)

            'Dim updateHeader As Boolean = False
            'Dim CurrMenuState As ELPWebConstants.enumMenu_State
            'If Not Session(ELPWebConstants.MENUSTATE) Is Nothing Then
            '    CurrMenuState = CType(HttpContext.Current.Session(ELPWebConstants.MENUSTATE), ELPWebConstants.enumMenu_State)
            'End If

            'If Value Then

            '    If CurrMenuState <> ELPWebConstants.enumMenu_State.View_Page_Mode Then
            '        updateHeader = True
            '    End If

            '    Session(ELPWebConstants.MENUSTATE) = ELPWebConstants.enumMenu_State.View_Page_Mode
            'Else

            '    If CurrMenuState <> ELPWebConstants.enumMenu_State.Editing_Page_Mode Then
            '        updateHeader = True
            '    End If

            '    Session(ELPWebConstants.MENUSTATE) = ELPWebConstants.enumMenu_State.Editing_Page_Mode
            'End If
            'register the javascript code to load the page
            Dim sPageName As String = Path.GetFileNameWithoutExtension(Request.Url.ToString)
            ' the service order display is a popup page and it wont have
            ' any navigation bars. so excluce the script.
            If (sPageName.ToUpper <> "SERVICEORDERDISPLAY") Then

                'ALR - Commented out to just pass javascript to new menu
                'Dim sJavaScript As String
                'sJavaScript = "<SCRIPT>" & Environment.NewLine
                'sJavaScript &= "try{" & Environment.NewLine

                ''sJavaScript &= "window.parent.frames(""Navigation_Header"").location.href = window.parent.frames(""Navigation_Header"").location.href " & ";" & Environment.NewLine
                ''sJavaScript &= "window.parent.frames(""Navigation_Side"").location.href = window.parent.frames(""Navigation_Side"").location.href " & ";" & Environment.NewLine

                '' Chequear porque es falso (tarea mañana) 
                'If updateHeader Then
                '    sJavaScript &= "window.parent.document.frames[""Navigation_Header""].location.href = window.parent.document.frames[""Navigation_Header""].location.href " & ";" & Environment.NewLine
                '    sJavaScript &= "window.parent.document.frames[""Navigation_Side""].location.href = window.parent.document.frames[""Navigation_Side""].location.href " & ";" & Environment.NewLine
                'End If

                'sJavaScript &= "   }catch(e){}" & Environment.NewLine
                'sJavaScript &= "</SCRIPT>" & Environment.NewLine
                'Me.RegisterStartupScript("HeadersOnOff", sJavaScript)

                Dim sJavaScript As String
                sJavaScript = String.Format("<SCRIPT>try{{parent.enabled={0};parent.ToggleMenus();}}catch(e){{}}</SCRIPT>", Value.ToString.ToLower)
                Me.RegisterStartupScript("MENU_ENABLED", sJavaScript)

            End If
        End Set
    End Property

    Public Sub ReturnToAppHomePage()
        Me.Redirect(Me.AppHomeUrl)
    End Sub

    Public Sub ReturnToTabHomePage()
        Me.Redirect(Me.TabHomeUrl)
    End Sub

    Public Sub ReloadHeader()
        Dim sJavascript As String
        sJavascript = "<SCRIPT>try{parent.enabled = false;parent.ToggleMenus();parent.refreshMenus();}catch(e){}</SCRIPT>"
        Me.RegisterStartupScript("REFRESHMENU", sJavascript)
    End Sub

#End Region

#Region "Control-Management"

    Public Shared Sub SetTextBackColor(ByVal oTextBox As TextBox, ByVal bDisable As Boolean, Optional ByVal isNewUI As Boolean = False)
        If (Not isNewUI) Then
            If bDisable Then
                oTextBox.BackColor = Color.White
                oTextBox.CssClass = "FLATTEXTBOX"
            Else
                oTextBox.CssClass = ""
            End If
        End If
    End Sub

    Public Shared Sub EnableDisableControls_old(ByVal oParentControl As Control, ByVal bDisable As Boolean)

        Dim controlCurrent As Control
        Dim sTemp As String

        '  oParentControl.Enabled = enableButtons
        For Each controlCurrent In oParentControl.Controls

            sTemp = controlCurrent.GetType.BaseType.ToString

            Select Case sTemp
                Case "System.Web.UI.WebControls.WebControl"

                    If controlCurrent.GetType.ToString = "System.Web.UI.WebControls.Image" Or controlCurrent.GetType.ToString = "System.Web.UI.WebControls.Label" Or controlCurrent.GetType.ToString = "System.Web.UI.WebControls.Button" Then
                        ' do nothing
                    ElseIf controlCurrent.GetType.ToString = "System.Web.UI.WebControls.TextBox" Then
                        Dim txtBox As TextBox = CType(controlCurrent, System.Web.UI.WebControls.TextBox)
                        txtBox.ReadOnly = bDisable
                        SetTextBackColor(txtBox, bDisable)
                    End If

                Case "System.Web.UI.WebControls.ListControl"
                    CType(controlCurrent, System.Web.UI.WebControls.ListControl).Enabled = Not bDisable
                Case "System.Web.UI.WebControls.Image"
                    CType(controlCurrent, System.Web.UI.WebControls.Image).Enabled = Not bDisable
            End Select

        Next

    End Sub

    Public Sub EnableDisableControls(ByVal oParentControl As Control, ByVal bDisable As Boolean)

        Dim controlCurrent As Control
        Dim sTemp As String

        If oParentControl Is Nothing Then Return
        sTemp = oParentControl.GetType.BaseType.ToString
        Select Case sTemp
            Case "System.Web.UI.WebControls.WebControl"

                If oParentControl.GetType.ToString = "System.Web.UI.WebControls.Image" Or
                   oParentControl.GetType.ToString = "System.Web.UI.WebControls.Label" Or
                   oParentControl.GetType.ToString = "System.Web.UI.WebControls.Button" Then
                    ' do nothing
                ElseIf oParentControl.GetType.ToString = "System.Web.UI.WebControls.TextBox" Then
                    Dim txtBox As TextBox = CType(oParentControl, System.Web.UI.WebControls.TextBox)
                    txtBox.ReadOnly = bDisable
                    SetTextBackColor(txtBox, bDisable, Me.IsNewUI)
                End If

            Case "System.Web.UI.WebControls.ListControl"
                ControlMgr.SetEnableControl(Me, CType(oParentControl, System.Web.UI.WebControls.ListControl), Not bDisable)
            Case "System.Web.UI.WebControls.Image"
                ControlMgr.SetEnableControl(Me, CType(oParentControl, System.Web.UI.WebControls.Image), Not bDisable)
        End Select
        For Each controlCurrent In oParentControl.Controls
            EnableDisableControls(controlCurrent, bDisable)
        Next

    End Sub

    Public Shared Sub SetDefaultButton(ByVal oControl As WebControl, ByVal defaultButton As Button)
        'oControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)")
        oControl.Attributes.Add("onkeydown", "fnTrapKD(document.getElementById('" + defaultButton.ClientID + "'),event)")
    End Sub


    Public Sub SetEnabledForControlFamily(ByVal controlFamilyRoot As Control, ByVal enabled As Boolean, Optional ByVal includeRootControl As Boolean = False)
        If (includeRootControl) Then
            ChangeEnabledProperty(controlFamilyRoot, enabled)
        End If
        Dim childControl As Control
        For Each childControl In controlFamilyRoot.Controls
            SetEnabledForControlFamily(childControl, enabled, True)
        Next

    End Sub


    Protected Sub ChangeEnabledProperty(ByVal ctrl As Control, ByVal enabled As Boolean)
        Try
            If ((ctrl.GetType) Is GetType(WebControls.TextBox)) Then
                If enabled = False Then 'change to readonly always allowed
                    CType(ctrl, WebControls.TextBox).ReadOnly = Not (enabled)
                    If Not Me.IsNewUI Then
                        CType(ctrl, WebControls.TextBox).CssClass = "FLATTEXTBOX"
                    End If
                Else
                    If CanEnableControl(CType(ctrl, WebControl)) Then ' check whether change is allowed
                        CType(ctrl, WebControls.TextBox).ReadOnly = Not (enabled)
                        If Not Me.IsNewUI Then
                            CType(ctrl, WebControls.TextBox).CssClass = ""
                        End If
                    End If
                End If
            ElseIf ((ctrl.GetType) Is GetType(WebControls.Label)) Then
                'NOTHING TO DO FOR NOW. MAYBE CHANGE THE COLOR LATER
            Else
                Dim pinfo As PropertyInfo = ctrl.GetType.GetProperty("Enabled", BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)
                If Not pinfo Is Nothing Then
                    '   pinfo.SetValue(ctrl, enabled, Nothing)
                    If ctrl.GetType.IsSubclassOf(GetType(WebControl)) Then
                        ControlMgr.SetEnableControl(Me, CType(ctrl, WebControl), enabled)
                    Else
                        pinfo.SetValue(ctrl, enabled, Nothing)
                    End If

                End If
                'Change the color for a DropdownList control
                If Not Me.IsNewUI Then
                    If ((ctrl.GetType) Is GetType(WebControls.DropDownList)) Then

                        If (enabled) Then
                            'Replaced with correct cast line to prevent exception
                            CType(ctrl, WebControls.DropDownList).CssClass = "FLATTEXTBOX"
                        Else
                            'Replaced with correct cast line to prevent exception
                            CType(ctrl, WebControls.DropDownList).CssClass = ""
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub AddControlWatcher(ByVal controlToWatch As System.Web.UI.WebControls.WebControl, ByVal controlToWatchOnValue As String, ByVal controlToChange As System.Web.UI.WebControls.WebControl, ByVal controlToChangeNewValue As String, Optional ByVal conditional As Boolean = False)
        If conditional Then
            controlToWatch.Attributes.Add("onchange", "(if document.all('" & controlToChange.UniqueID & "').value == '" & controlToWatchOnValue & "'){document.all('" & controlToChange.UniqueID & "').value=" & controlToChangeNewValue & "};")
        Else
            controlToWatch.Attributes.Add("onchange", "document.all('" & controlToChange.UniqueID & "').value=" & controlToChangeNewValue & ";")
        End If
    End Sub

    Public Sub AddControlWatcher(ByVal controlToWatch As System.Web.UI.WebControls.WebControl, ByVal controlToWatchOnValue As String, ByVal controlToChange As System.Web.UI.WebControls.WebControl, ByVal controlToChangeIsDisabled As Boolean, ByVal controlToChangeIsVisible As Boolean, Optional ByVal conditional As Boolean = False)

        If conditional Then
            controlToWatch.Attributes.Add("onchange", "debugger; (if document.all('" & controlToChange.UniqueID & "').value == '" & controlToWatchOnValue & "'){document.all('" & controlToChange.UniqueID & "').disabled=" & controlToChangeIsDisabled & ";document.all('" & controlToChange.UniqueID & "').style.visibility=" & controlToChangeIsVisible & ";")
        Else
            controlToWatch.Attributes.Add("onchange", "debugger; document.all('" & controlToChange.UniqueID & "').disabled=" & controlToChangeIsDisabled & ";document.all('" & controlToChange.UniqueID & "').style.visibility=" & controlToChangeIsVisible & ";")
        End If
    End Sub

    Public Sub ChangeEnabledControlProperty(ByVal ctrl As Control, ByVal enabled As Boolean)
        Me.ChangeEnabledProperty(ctrl, enabled)
    End Sub


#End Region

#Region "Session Info Properties"
    Public Property AppHomeUrl() As String
        Get
            If Not DirectCast(Me, IComponent).Site Is Nothing AndAlso DirectCast(Me, IComponent).Site.DesignMode Then
                Return Nothing
            End If
            Return Me.Session(Me.APP_HOME_URL_SESSION_KEY).ToString
        End Get
        Set(ByVal Value As String)
            Me.Session(Me.APP_HOME_URL_SESSION_KEY) = Value
        End Set
    End Property

    Public Property TabHomeUrl() As String
        Get
            'If Not DirectCast(Me, IComponent).Site Is Nothing AndAlso DirectCast(Me, IComponent).Site.DesignMode Then
            '    Return Nothing
            'End If
            'Return Me.Session(Me.TAB_HOME_URL_SESSION_KEY).ToString

            'ALR - Commented out the tab return as we are no longer using tabs.  Return the AppHomeURL
            Return AppHomeUrl
        End Get
        Set(ByVal Value As String)
            Me.Session(Me.TAB_HOME_URL_SESSION_KEY) = Value
        End Set
    End Property
#End Region


#Region "Navigation Controller"
    Public Const SESSION_KEY_CURRENT_FLOW_CTRL As String = "SESSION_KEY_CURRENT_FLOW_CTRL"
    Public Property NavController() As INavigationController
        Get
            Return CType(Session(Me.SESSION_KEY_CURRENT_FLOW_CTRL), INavigationController)
        End Get
        Set(ByVal Value As INavigationController)
            Session(Me.SESSION_KEY_CURRENT_FLOW_CTRL) = Value
        End Set
    End Property

#End Region

#Region "Stack Controller"

#End Region


#Region "Master Page Methods / Properties"

    Public ReadOnly Property ErrControllerMaster() As ErrorController
        Get
            Dim errCtl As ErrorController
            Try
                If Not Page.Master Is Nothing Then
                    errCtl = CType(Me.Master, MasterBase).ErrController
                End If
                Return errCtl
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property TheReportCeInputControl() As Reports.ReportCeInputControl
        Get
            Dim rptCtl As Reports.ReportCeInputControl
            Try
                If Not Page.Master Is Nothing Then
                    rptCtl = CType(Me.Master, MasterBase).ReportCeInputControl
                End If
                Return rptCtl
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property TheReportExtractInputControl() As Reports.ReportExtractInputControl
        Get
            Dim rptCtl As Reports.ReportExtractInputControl
            Try
                If Not Page.Master Is Nothing Then
                    rptCtl = CType(Me.Master, MasterBase).ReportExtractInputControl
                End If
                Return rptCtl
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property MasterErrController() As MessageController
        Get
            Dim errCtl As MessageController
            Try
                If Not Page.Master Is Nothing Then
                    errCtl = CType(CType(Me.Master, MasterBase).MessageController, MessageController)
                End If
                Return errCtl
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Function SetFormTitle(ByVal TextValue As String, Optional ByVal Translate As Boolean = True) As String
        Try
            If Not Page.Master Is Nothing Then
                If Translate Then
                    CType(Me.Master, MasterBase).PageTitle = TranslationBase.TranslateLabelOrMessage(TextValue)
                Else
                    CType(Me.Master, MasterBase).PageTitle = TextValue
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function SetFormTab(ByVal TextValue As String, Optional ByVal Translate As Boolean = True) As String
        Try
            If Not Page.Master Is Nothing Then
                If Translate Then
                    CType(Me.Master, MasterBase).PageTab = TranslationBase.TranslateLabelOrMessage(TextValue)
                Else
                    CType(Me.Master, MasterBase).PageTab = TextValue
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    Public ReadOnly Property PageForm(Optional ByVal formName As String = "form1") As HtmlForm
        Get
            Dim frm As HtmlForm
            If Not Me.FindControl(formName) Is Nothing Then
                frm = CType(Me.FindControl(formName), HtmlForm)
            ElseIf Not Me.Master Is Nothing Then
                frm = CType(Me.Master, MasterBase).PageForm(formName)
            End If
            Return frm
        End Get
    End Property

#End Region
    <System.Web.Services.WebMethod()>
    Public Shared Function GetListOfComunas(ByVal regionID As Guid) As List(Of String)

        Dim arrItems As List(Of String) = New List(Of String)
        Try
            Dim dv As DataView = LookupListNew.GetRegionAndComunaList(regionID)

            Dim iCount As Integer

            For iCount = 0 To dv.Table.Rows.Count
                arrItems.Add(dv.Table.Rows(iCount)(0).ToString())
            Next iCount
        Catch ex As Exception

        End Try

        Return arrItems

    End Function

#Region "Convert List to Datatable"
    Public Function ConvertToDataTable(Of T)(list As IList(Of T)) As DataTable
        Dim entityType As Type = GetType(T)
        Dim table As New DataTable()
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)
        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function
#End Region

End Class

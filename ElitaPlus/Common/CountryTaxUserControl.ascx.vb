Imports System.Globalization
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class CountryTaxUserControl
    Inherits System.Web.UI.UserControl

#Region "Member Variables"

    Private mDescription As String
    Private mPercent As Object
    Private mComputeMethodID As Guid
    Private mPercentFlagID As Guid
    Private mIsRegion As Boolean
    Private mComputeMethodDescription As String


#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private mPercentCode As String
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region " Constants "

    Private Const PERCENT_CODE As String = "P"
    Private Const REGION_CODE As String = "S"
    Private Const TAX_PERCENT_FLAG As String = "PERCT"
    Private Const INVOICE_CODE_ As String = "I"
    Private Const TAX_COMPUTE_METHOD As String = "TCOMP"
    Private Const Desimal_Format As String = "N4"
    Private Const COUNTRY_TAX_FORM As String = "COUNTRY_TAX_FORM"
    Private Const NOTHING_SELECTED As Int16 = -1
#End Region

#Region "Properties"

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

    End Sub

    Public Sub LoadProperties(Optional strComputeMethodCodeToFilter As String = "")

        'Dim oComputeMethodList As DataView = LookupListNew.DropdownLookupList(TAX_COMPUTE_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.dlstTaxComputeMethod, oComputeMethodList)

        'Dim oPercentFlagList As DataView = LookupListNew.DropdownLookupList(TAX_PERCENT_FLAG, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.dlstTaxPercentFlag, oPercentFlagList)

        Dim ComputeMethodList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="TCOMP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Me.dlstTaxComputeMethod.Populate(ComputeMethodList,
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })
        If Not strComputeMethodCodeToFilter.Equals(String.Empty) Then
            Dim oComputeMethodToFilterId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_COMPUTE_METHOD, strComputeMethodCodeToFilter)
            For x As Integer = Me.dlstTaxComputeMethod.Items.Count - 1 To 0 Step -1
                Dim item As ListItem = Me.dlstTaxComputeMethod.Items(x)
                If item.Value.Equals(oComputeMethodToFilterId.ToString) = True Then
                    Me.dlstTaxComputeMethod.Items.RemoveAt(x)
                End If
            Next
        End If

        Dim PercentList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="PERCT",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Me.dlstTaxPercentFlag.Populate(PercentList,
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })
        LoadText()

    End Sub

    Private Sub LoadText()

        UseRegion = True
        txtTaxDescription.Text = Description

        CType(Me.Page, ElitaPlusPage).SetSelectedItem(dlstTaxComputeMethod, ComputeMethodID)
        CType(Me.Page, ElitaPlusPage).SetSelectedItem(dlstTaxPercentFlag, PercentFlagID)

        If Not UseState() Then
            Me.txtTaxPercent.Text = CType(Percent, Double).ToString(Desimal_Format, CultureInfo.CurrentCulture)
            UseRegion = False
            Me.lblPercent.ForeColor = Color.Black
        End If

        If UseManually() Then
            Dim dlbZeroes As Double = 0.0
            Me.txtTaxPercent.Text = dlbZeroes.ToString(Desimal_Format, CultureInfo.CurrentCulture)
            dlstTaxPercentFlag.SelectedIndex = NOTHING_SELECTED
        End If


    End Sub

    Public Sub SaveProperties()
        UseRegion = UseState()
        Description = Me.txtTaxDescription.Text
        ComputeMethodID = LoadMethodID()
        ComputeMethodDescription = LoadMethodDescription()
        PercentFlagID = LoadPercentFlagID()
        Percent = CheckForNumericNull()
    End Sub


    Private Sub dlstTaxComputeMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlstTaxComputeMethod.SelectedIndexChanged
        Dim dlbZeroes As Double = 0.0

        If UseManually() Then
            Me.txtTaxPercent.Text = dlbZeroes.ToString(Desimal_Format, CultureInfo.CurrentCulture)
            dlstTaxPercentFlag.SelectedIndex = NOTHING_SELECTED
        End If

    End Sub

    Private Sub dlstTaxPercentFlag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlstTaxPercentFlag.SelectedIndexChanged

        Dim dlbZeroes As Double = 0.0
        Percent = Me.txtTaxPercent.Text
        UseRegion = True

        If Not UseState() Then
            UseRegion = False
            If CType(Percent, String) = "" Then
                Me.txtTaxPercent.Text = dlbZeroes.ToString(Desimal_Format, CultureInfo.CurrentCulture)
            End If
        End If

        Try
            Dim parentPage As CountryTaxEdit = CType(Me.Page, CountryTaxEdit)
            parentPage.TestForRegions(UseRegion)
        Catch ex As Exception
        End Try
    End Sub

#Region "PRIVATE FUNCTIONS"

    Private Function LoadMethodID() As Guid

        Return CType(Me.Page, ElitaPlusPage).GetSelectedItem(Me.dlstTaxComputeMethod)

    End Function

    Private Function LoadMethodDescription() As String

        Return CType(Me.Page, ElitaPlusPage).GetSelectedDescription(Me.dlstTaxComputeMethod)

    End Function


    Private Function LoadPercentFlagID() As Guid

        Return CType(Me.Page, ElitaPlusPage).GetSelectedItem(Me.dlstTaxPercentFlag)

    End Function

    Private Function CheckForNumericNull() As String


        Dim dlbPercent As Double

        If UseRegion Then
            dlbPercent = 0.0
        Else
            Try
                dlbPercent = CType(Me.txtTaxPercent.Text, Double)
                Me.lblPercent.ForeColor = Color.Black
            Catch ex As Exception
                CType(Me.Page, ElitaPlusPage).SetLabelError(Me.lblPercent)
                Throw New GUIException("PERCENT_MUST_BE_NUMERIC", "PERCENT_MUST_BE_NUMERIC")
                Return Me.txtTaxPercent.Text
            End Try
        End If


        Return dlbPercent.ToString(Desimal_Format, CultureInfo.CurrentCulture)

    End Function

    Private Function UseState() As Boolean

        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_PERCENT_FLAG, _
            ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(dlstTaxPercentFlag.SelectedValue.ToString)) = PERCENT_CODE Then
            ControlMgr.SetVisibleControl(Page, lblPercent, True)
            ControlMgr.SetVisibleControl(Page, txtTaxPercent, True)
            Return False
        ElseIf LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_PERCENT_FLAG, _
            ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(dlstTaxPercentFlag.SelectedValue.ToString)) = REGION_CODE Then
            ControlMgr.SetVisibleControl(Page, lblPercent, True)
            ControlMgr.SetVisibleControl(Page, lblPercent, False)
            ControlMgr.SetVisibleControl(Page, txtTaxPercent, False)
            Return True
        End If
        Return Nothing
    End Function


    Private Function UseManually() As Boolean

        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_COMPUTE_METHOD, _
            ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(dlstTaxComputeMethod.SelectedValue.ToString)) <> INVOICE_CODE_ Then
            ControlMgr.SetVisibleControl(Page, lblTaxPercent, True)
            ControlMgr.SetVisibleControl(Page, dlstTaxPercentFlag, True)
            ControlMgr.SetVisibleControl(Page, lblPercent, True)
            ControlMgr.SetVisibleControl(Page, txtTaxPercent, True)
            Return False
        Else
            'ElseIf LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_COMPUTE_METHOD, _
            '    ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(dlstTaxComputeMethod.SelectedValue.ToString)) = INVOICE_CODE_ Then
            ControlMgr.SetVisibleControl(Page, lblTaxPercent, False)
            ControlMgr.SetVisibleControl(Page, dlstTaxPercentFlag, False)
            ControlMgr.SetVisibleControl(Page, lblPercent, False)
            ControlMgr.SetVisibleControl(Page, txtTaxPercent, False)
            Return True
        End If
        Return Nothing
    End Function


#End Region

#Region "PROPERTIES"

    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal Value As String)
            mDescription = Value
        End Set
    End Property
    Public Property ComputeMethodDescription() As String
        Get
            Return mComputeMethodDescription
        End Get
        Set(ByVal Value As String)
            mComputeMethodDescription = Value
        End Set
    End Property


    Public Property Percent() As Object
        Get

            Return mPercent
        End Get
        Set(ByVal Value As Object)
            mPercent = Value
        End Set
    End Property

    Public Property ComputeMethodID() As Guid
        Get
            Return mComputeMethodID
        End Get
        Set(ByVal Value As Guid)
            mComputeMethodID = Value
        End Set
    End Property

    Public Property PercentFlagID() As Guid
        Get
            Return mPercentFlagID
        End Get
        Set(ByVal Value As Guid)
            mPercentFlagID = Value
        End Set
    End Property

    Public Property UseRegion() As Boolean
        Get
            Return mIsRegion
        End Get
        Set(ByVal Value As Boolean)
            mIsRegion = Value
        End Set
    End Property

    Public ReadOnly Property isRegion() As Boolean
        Get
            Return UseState()
        End Get

    End Property

#End Region

End Class

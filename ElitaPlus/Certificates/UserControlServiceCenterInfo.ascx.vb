Partial Class UserControlServiceCenterInfo
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "State"
    Class MyState
        Public ServiceCenterBo As ServiceCenter
        Public ErrControllerId As String
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Private ReadOnly Property ErrCtrl() As ErrorController
        Get
            If Not Me.State.ErrControllerId Is Nothing Then
                Return CType(Me.Page.FindControl(Me.State.ErrControllerId), ErrorController)
            End If
        End Get
    End Property
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Me.IsPostBack Then
            Me.Page.SetEnabledForControlFamily(Me, False)
        End If
    End Sub

#Region "Controlling Logic"
    Public Sub Bind(ByVal servCenterBo As ServiceCenter, ByVal containerErrorController As ErrorController)
        With State
            .ErrControllerId = containerErrorController.ID
            .ServiceCenterBo = servCenterBo
        End With
        PopulateControlFromBo()
    End Sub

    Private Sub PopulateControlFromBo()
        If Not Me.State.ServiceCenterBo Is Nothing Then
            With Me.State.ServiceCenterBo
                Me.Page.PopulateControlFromBOProperty(Me.TextboxSCCode, .Code)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxContactName, .ContactName)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxPhone1, .Phone1)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxPhone2, .Phone2)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxFax, .Fax)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxEmail, .Email)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxCcEmail, .CcEmail)
                Me.Page.PopulateControlFromBOProperty(Me.CheckBoxDefaultToEmail, .DefaultToEmailFlag)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxBusinessHours, .BusinessHours)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxComment, .Comments)

                If Not .OriginalDealerId.Equals(Guid.Empty) Then
                    Me.Page.PopulateControlFromBOProperty(Me.txtOriginalDealer, New Dealer(.OriginalDealerId).DealerName)
                End If

                If Not .Address Is Nothing Then
                    Me.Page.PopulateControlFromBOProperty(Me.TextboxAddress, .Address.Address1)
                    Me.Page.PopulateControlFromBOProperty(Me.TextboxAddress2, .Address.Address2)
                    Me.Page.PopulateControlFromBOProperty(Me.TextboxCity, .Address.City)
                    If Not .Address.RegionId.Equals(Guid.Empty) Then
                        Dim oRegion As New Assurant.ElitaPlus.BusinessObjectsNew.Region(.Address.RegionId)
                        Me.Page.PopulateControlFromBOProperty(Me.TextboxStateProvince, oRegion.Description)
                    End If
                    Me.Page.PopulateControlFromBOProperty(Me.TextboxZip, .Address.PostalCode)
                End If
                Me.Page.PopulateControlFromBOProperty(Me.CheckBoxShipping, .Shipping)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxPROCESSING_FEE, .ProcessingFee)

            End With
        End If
    End Sub
#End Region



End Class

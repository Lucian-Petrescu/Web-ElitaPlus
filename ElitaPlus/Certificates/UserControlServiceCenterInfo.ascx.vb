Partial Class UserControlServiceCenterInfo
    Inherits System.Web.UI.UserControl

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

#Region "State"
    Class MyState
        Public ServiceCenterBo As ServiceCenter
        Public ErrControllerId As String
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Private ReadOnly Property ErrCtrl() As ErrorController
        Get
            If State.ErrControllerId IsNot Nothing Then
                Return CType(Page.FindControl(State.ErrControllerId), ErrorController)
            End If
        End Get
    End Property
#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Page.SetEnabledForControlFamily(Me, False)
        End If
    End Sub

#Region "Controlling Logic"
    Public Sub Bind(servCenterBo As ServiceCenter, containerErrorController As ErrorController)
        With State
            .ErrControllerId = containerErrorController.ID
            .ServiceCenterBo = servCenterBo
        End With
        PopulateControlFromBo()
    End Sub

    Private Sub PopulateControlFromBo()
        If State.ServiceCenterBo IsNot Nothing Then
            With State.ServiceCenterBo
                Page.PopulateControlFromBOProperty(TextboxSCCode, .Code)
                Page.PopulateControlFromBOProperty(TextboxDescription, .Description)
                Page.PopulateControlFromBOProperty(TextboxContactName, .ContactName)
                Page.PopulateControlFromBOProperty(TextboxPhone1, .Phone1)
                Page.PopulateControlFromBOProperty(TextboxPhone2, .Phone2)
                Page.PopulateControlFromBOProperty(TextboxFax, .Fax)
                Page.PopulateControlFromBOProperty(TextboxEmail, .Email)
                Page.PopulateControlFromBOProperty(TextboxCcEmail, .CcEmail)
                Page.PopulateControlFromBOProperty(CheckBoxDefaultToEmail, .DefaultToEmailFlag)
                Page.PopulateControlFromBOProperty(TextboxBusinessHours, .BusinessHours)
                Page.PopulateControlFromBOProperty(TextboxComment, .Comments)

                If Not .OriginalDealerId.Equals(Guid.Empty) Then
                    Page.PopulateControlFromBOProperty(txtOriginalDealer, New Dealer(.OriginalDealerId).DealerName)
                End If

                If .Address IsNot Nothing Then
                    Page.PopulateControlFromBOProperty(TextboxAddress, .Address.Address1)
                    Page.PopulateControlFromBOProperty(TextboxAddress2, .Address.Address2)
                    Page.PopulateControlFromBOProperty(TextboxCity, .Address.City)
                    If Not .Address.RegionId.Equals(Guid.Empty) Then
                        Dim oRegion As New Assurant.ElitaPlus.BusinessObjectsNew.Region(.Address.RegionId)
                        Page.PopulateControlFromBOProperty(TextboxStateProvince, oRegion.Description)
                    End If
                    Page.PopulateControlFromBOProperty(TextboxZip, .Address.PostalCode)
                End If
                Page.PopulateControlFromBOProperty(CheckBoxShipping, .Shipping)
                Page.PopulateControlFromBOProperty(TextboxPROCESSING_FEE, .ProcessingFee)

            End With
        End If
    End Sub
#End Region



End Class

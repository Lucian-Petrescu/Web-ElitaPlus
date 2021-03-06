
Namespace Claims

    Partial Class ClaimItemForm
        Inherits ElitaPlusPage

#Region "Page State"

#Region "Parameters"

        Public Class Parameters
            Public moClaimId As Guid

            Public Sub New(ByVal oClaimId As Guid)
                moClaimId = oClaimId
            End Sub

        End Class

#End Region
#Region "MyState"

        Class MyState
            Public Claim As ClaimBase
            Public ReplacementItemsListDv As DataView
            Public ReplacementItemsStatusDv As DataView

            Public ReadOnly Property ClaimBO As ClaimBase
                Get
                    If (Me.Claim Is Nothing) Then
                        If (Me.moParams.moClaimId <> Guid.Empty) Then
                            Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.moParams.moClaimId)
                        End If
                    End If
                    Return Me.Claim
                End Get
            End Property
            Public moParams As Parameters
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            Try
                Me.State.moParams = CType(Me.CallingParameters, Parameters)
                If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.moClaimId.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
                PopulateFormFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "ClaimItemForm.aspx"
        Private Const GridViewReplacementItemsStatus As String = "GridViewReplacementItemsStatus"
#End Region

#Region "Handlers"


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

#Region "Handlers-Init"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTitle & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("Item")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claim") & " " & TranslationBase.TranslateLabelOrMessage("Replacement") & " " & "Item"


            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claims")
                    UpdateBreadCrum()
                    Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                    Me.SetStateProperties()
                    Translate()
                    PopulateReplacementItemsGrid()

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            ' Claim Detail
            Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                GoBack()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#End Region

#Region "Populate"

        Private Sub PopulateFormFromBo()
            Try

                moClaimInfoController.InitController(Me.State.ClaimBO)
                With Me.State.ClaimBO
                    moClaimNumber.Text = .ClaimNumber
                    EnableDisableControls(moClaimNumber, True)
                    moDealer.Text = .DealerName
                    EnableDisableControls(moDealer, True)
                    If (.ClaimedEquipment Is Nothing) Then
                        moManufacturerText.Text = Manufacturer.GetDescription(.CertificateItem.ManufacturerId)
                        moModelText.Text = .CertificateItem.Model
                        moSerialNumberText.Text = .CertificateItem.SerialNumber
                        moIMEINumberText.Text = .CertificateItem.IMEINumber
                    Else
                        moManufacturerText.Text = Manufacturer.GetDescription(.ClaimEquipmentChildren(0).ManufacturerId)
                        moModelText.Text = .ClaimEquipmentChildren(0).Model
                        moSerialNumberText.Text = .ClaimEquipmentChildren(0).SerialNumber
                        moIMEINumberText.Text = .ClaimEquipmentChildren(0).IMEINumber
                        moDeviceTypeText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEVICE, .ClaimEquipmentChildren.Where(Function(ce) ce.ClaimId = .Id).First.DeviceTypeId)
                    End If

                    EnableDisableControls(moManufacturerText, True)
                    EnableDisableControls(moModelText, True)
                    EnableDisableControls(moSerialNumberText, True)
                    EnableDisableControls(moIMEINumberText, True)

                    If Not Me.State.ClaimBO.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                        Me.moSerialNumberIMEILabel.Visible = False
                        Me.moSerialNumberLabel.Visible = True
                        Me.moIMEINumberLabel.Visible = True
                        Me.moIMEINumberText.Visible = True
                    Else
                        Me.moSerialNumberLabel.Visible = False
                        Me.moSerialNumberIMEILabel.Visible = True
                        Me.moIMEINumberLabel.Visible = False
                        Me.moIMEINumberText.Visible = False
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region


#Region "RepacementItem"



        Public Sub Translate()
            Me.TranslateGridHeader(GridViewReplacementItems)

        End Sub
        Private Sub PopulateReplacementItemsGrid()
            Try

                LoadReplacementItems()

                If Not State.ReplacementItemsListDv Is Nothing AndAlso State.ReplacementItemsListDv.Count > 0 Then
                    GridViewReplacementItems.DataSource = State.ReplacementItemsListDv
                    GridViewReplacementItems.DataBind()

                Else
                    lblCdRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub LoadReplacementItems()
            State.ReplacementItemsListDv = State.ClaimBO.GetReplacementItem(State.ClaimBO.Id)
        End Sub


        Protected Sub GridViewReplacementItems_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewReplacementItems.RowDataBound
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If e.Row.RowType = DataControlRowType.DataRow Then
                State.ReplacementItemsStatusDv = State.ClaimBO.GetReplacementItemStatus(State.ClaimBO.Id, GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow("claim_equipment_id"), Byte()))))
                Dim gridViewReplacementStatus As GridView = CType(e.Row.FindControl(GridViewReplacementItemsStatus), GridView)
                Me.TranslateGridHeader(gridViewReplacementStatus)
                gridViewReplacementStatus.DataSource = State.ReplacementItemsStatusDv
                gridViewReplacementStatus.DataBind()


            End If
        End Sub
#End Region

    End Class

End Namespace
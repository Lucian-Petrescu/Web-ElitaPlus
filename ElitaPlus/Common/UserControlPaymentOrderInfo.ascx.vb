Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Public Class UserControlPaymentOrderInfo
    Inherits System.Web.UI.UserControl

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not Me.State.myPmtOrderInfoBo Is Nothing Then
                BindBoPropertiesToLabels()
                'Page.AddLabelDecorations(Me.State.myPmtOrderInfoBo)                                               
                'EnableControlsBasedOnUserCountry()                
                '  Else
                '    EnableControlsBasedOnUserCountry()
                ' End If
            End If
        End If
    End Sub
#End Region

#Region "State"

    Class MyState
        Public myPmtOrderInfoBo As PaymentOrderInfo
        Public IsNewObjDirty As Boolean = False
        Public transferType As String
        Public Function IsBODirty() As Boolean
            If myPmtOrderInfoBo Is Nothing Then
                Return False
            Else
                If myPmtOrderInfoBo.IsNew Then
                    Return IsNewObjDirty
                Else
                    Return myPmtOrderInfoBo.IsDirty
                End If
            End If
        End Function
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

#End Region

#Region "PopulateFields"

    Private Sub PopulateBankNameDropdown()
        '    Dim obank As New BankName
        Dim BankDV As DataView = New DataView(BankName.LoadBankNameByCountry(Me.State.myPmtOrderInfoBo.CountryID))
        Page.BindListControlToDataView(moBankName, BankDV, "DESCRIPTION", "ID", True)
    End Sub

    Private Sub LoadCountryList(Optional ByVal nothingSelcted As Boolean = True)

        Dim oCountryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
        moCountryDrop_WRITE.Populate(oCountryList, New PopulateOptions() With
                                        {
                                        .AddBlankItem = nothingSelcted
                                        })


    End Sub

    Private Sub PopulateControlFromBo()
        If Not Me.State.myPmtOrderInfoBo Is Nothing Then

            With Me.State.myPmtOrderInfoBo
                If Not .CountryID.Equals(Guid.Empty) Then
                    LoadCountryList(False)
                Else
                    LoadCountryList()
                End If
                Me.Page.PopulateControlFromBOProperty(Me.textboxBankID, .Bank_Id)
                If Me.txtBankName.Visible Then
                    Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                ElseIf moBankName.Visible Then
                    If Not .BankName Is Nothing Then Me.Page.SetSelectedItemByText(Me.moBankName, .BankName)
                Else
                    If moBankName.Items.Count > 0 Then
                        Me.Page.SetSelectedItemByText(Me.moBankName, .BankName)
                    Else
                        Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                    End If
                End If

                Me.Page.SetSelectedItem(Me.moCountryDrop_WRITE, .CountryID)
                Me.Page.PopulateControlFromBOProperty(Me.txtNameAccount, .Account_Name)
            End With
        End If
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType, LTemp As Long
        If Not Me.State.myPmtOrderInfoBo Is Nothing Then
            With Me.State.myPmtOrderInfoBo
                Me.BindBoPropertiesToLabels()

                If .IsNew AndAlso .Bank_Id <> textboxBankID.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myPmtOrderInfoBo, "Bank_Id", textboxBankID)

                If .IsNew AndAlso .Account_Name <> txtNameAccount.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myPmtOrderInfoBo, "Account_Name", txtNameAccount)

                guidTemp = .CountryID
                Me.Page.PopulateBOProperty(Me.State.myPmtOrderInfoBo, "CountryID", moCountryDrop_WRITE)
                If .IsNew AndAlso .CountryID <> guidTemp Then State.IsNewObjDirty = True

                If txtBankName.Visible = True Then
                    If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                    Me.Page.PopulateBOProperty(Me.State.myPmtOrderInfoBo, "BankName", txtBankName)
                Else
                    'guidTemp = .AccountTypeId
                    Me.Page.PopulateBOProperty(Me.State.myPmtOrderInfoBo, "BankName", moBankName, False)
                    If .IsNew AndAlso .BankName <> moBankName.SelectedItem.Text Then State.IsNewObjDirty = True
                End If

                If blnValidate AndAlso .IsDirty Then
                    .Validate()
                    If Me.Page.ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End If

                If Not blnExcludeSave Then .Save()
            End With
        End If
    End Sub

#End Region

#Region "PopulateBO"

    Protected Sub BindBoPropertiesToLabels()

        Me.Page.BindBOPropertyToLabel(Me.State.myPmtOrderInfoBo, "Bank_Id", labelBankID)
        Me.Page.BindBOPropertyToLabel(Me.State.myPmtOrderInfoBo, "BankName", lblBankName)

        ' Move to PaymentOrderInfo BO 02/08/2008
        Me.Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub Bind(ByVal PmtOrderInfoBo As PaymentOrderInfo)
        With State
            .myPmtOrderInfoBo = PmtOrderInfoBo
        End With
        Me.textboxBankID.Text = String.Empty

        EnableControlsBasedOnUserCountry()
        Me.PopulateControlFromBo()

    End Sub

    Public Sub GetBankIdForBankName(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBankName.SelectedIndexChanged
        If Not Me.Page.GetSelectedItem(moBankName).Equals(Guid.Empty) Then
            Dim boBankName As BankName
            boBankName = New BankName(Me.Page.GetSelectedItem(moBankName))
            textboxBankID.Text = boBankName.Code
        Else
            textboxBankID.Text = String.Empty
        End If
    End Sub

#End Region

#Region "EnableDisableFields"

    Private Sub DisplayBankID()
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, True)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankNameDP()
        ControlMgr.SetVisibleControl(Me.Page, moBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, True)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub HideBankName()
        ControlMgr.SetVisibleControl(Me.Page, moBankName, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, False)
    End Sub

    Private Sub DisplayBankNametxt()
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, moBankName, False)
        moBankName.SelectedIndex = -1
    End Sub

    Public Sub DisableAllFields()
        ControlMgr.SetEnableControl(Me.Page, txtBankName, False)
        ControlMgr.SetEnableControl(Me.Page, moBankName, False)
        ControlMgr.SetEnableControl(Me.Page, textboxBankID, False)
    End Sub

    Public Sub EnableControlsBasedOnUserCountry()

        If Not Me.State.myPmtOrderInfoBo Is Nothing Then

            Dim ocountry As New Country(Me.State.myPmtOrderInfoBo.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, ocountry.UseBankListId) = Codes.YESNO_Y Then
                DisplayBankNameDP()
                DisplayBankID()
                PopulateBankNameDropdown()
                ControlMgr.SetEnableControl(Me.Page, textboxBankID, False)
            Else
                DisplayBankNametxt()
                ControlMgr.SetEnableControl(Me.Page, textboxBankID, True)
            End If
            SetTheRequiredFields()
        Else
            DisplayBankNametxt()
            DisplayBankID()
        End If

    End Sub

    Public Sub EnableDisableControls()
        EnableControlsBasedOnUserCountry()
    End Sub

    Public Sub SetTheRequiredFields()

        If labelBankID.Text.IndexOf("*") <> 0 Then Me.labelBankID.Text = "* " & Me.labelBankID.Text
        If lblBankName.Text.IndexOf("*") <> 0 Then Me.lblBankName.Text = "* " & Me.lblBankName.Text
        If lblNameonAccount.Text.IndexOf("*") <> 0 Then Me.lblNameonAccount.Text = "* " & Me.lblNameonAccount.Text
        If lblCountryOfBank.Text.IndexOf("*") <> 0 Then Me.lblCountryOfBank.Text = "* " & Me.lblCountryOfBank.Text
    End Sub

#End Region

End Class

Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Public Class UserControlPaymentOrderInfo
    Inherits System.Web.UI.UserControl

#Region "Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If State.myPmtOrderInfoBo IsNot Nothing Then
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

#End Region

#Region "PopulateFields"

    Private Sub PopulateBankNameDropdown()
        '    Dim obank As New BankName
        Dim BankDV As DataView = New DataView(BankName.LoadBankNameByCountry(State.myPmtOrderInfoBo.CountryID))
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
        If State.myPmtOrderInfoBo IsNot Nothing Then

            With State.myPmtOrderInfoBo
                If Not .CountryID.Equals(Guid.Empty) Then
                    LoadCountryList(False)
                Else
                    LoadCountryList()
                End If
                Page.PopulateControlFromBOProperty(textboxBankID, .Bank_Id)
                If txtBankName.Visible Then
                    Page.PopulateControlFromBOProperty(txtBankName, .BankName)
                ElseIf moBankName.Visible Then
                    If .BankName IsNot Nothing Then Page.SetSelectedItemByText(moBankName, .BankName)
                Else
                    If moBankName.Items.Count > 0 Then
                        Page.SetSelectedItemByText(moBankName, .BankName)
                    Else
                        Page.PopulateControlFromBOProperty(txtBankName, .BankName)
                    End If
                End If

                Page.SetSelectedItem(moCountryDrop_WRITE, .CountryID)
                Page.PopulateControlFromBOProperty(txtNameAccount, .Account_Name)
            End With
        End If
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType, LTemp As Long
        If State.myPmtOrderInfoBo IsNot Nothing Then
            With State.myPmtOrderInfoBo
                BindBoPropertiesToLabels()

                If .IsNew AndAlso .Bank_Id <> textboxBankID.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myPmtOrderInfoBo, "Bank_Id", textboxBankID)

                If .IsNew AndAlso .Account_Name <> txtNameAccount.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myPmtOrderInfoBo, "Account_Name", txtNameAccount)

                guidTemp = .CountryID
                Page.PopulateBOProperty(State.myPmtOrderInfoBo, "CountryID", moCountryDrop_WRITE)
                If .IsNew AndAlso .CountryID <> guidTemp Then State.IsNewObjDirty = True

                If txtBankName.Visible = True Then
                    If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                    Page.PopulateBOProperty(State.myPmtOrderInfoBo, "BankName", txtBankName)
                Else
                    'guidTemp = .AccountTypeId
                    Page.PopulateBOProperty(State.myPmtOrderInfoBo, "BankName", moBankName, False)
                    If .IsNew AndAlso .BankName <> moBankName.SelectedItem.Text Then State.IsNewObjDirty = True
                End If

                If blnValidate AndAlso .IsDirty Then
                    .Validate()
                    If Page.ErrCollection.Count > 0 Then
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

        Page.BindBOPropertyToLabel(State.myPmtOrderInfoBo, "Bank_Id", labelBankID)
        Page.BindBOPropertyToLabel(State.myPmtOrderInfoBo, "BankName", lblBankName)

        ' Move to PaymentOrderInfo BO 02/08/2008
        Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub Bind(PmtOrderInfoBo As PaymentOrderInfo)
        With State
            .myPmtOrderInfoBo = PmtOrderInfoBo
        End With
        textboxBankID.Text = String.Empty

        EnableControlsBasedOnUserCountry()
        PopulateControlFromBo()

    End Sub

    Public Sub GetBankIdForBankName(sender As Object, e As System.EventArgs) Handles moBankName.SelectedIndexChanged
        If Not Page.GetSelectedItem(moBankName).Equals(Guid.Empty) Then
            Dim boBankName As BankName
            boBankName = New BankName(Page.GetSelectedItem(moBankName))
            textboxBankID.Text = boBankName.Code
        Else
            textboxBankID.Text = String.Empty
        End If
    End Sub

#End Region

#Region "EnableDisableFields"

    Private Sub DisplayBankID()
        ControlMgr.SetVisibleControl(Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Page, textboxBankID, True)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Page, textboxBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankNameDP()
        ControlMgr.SetVisibleControl(Page, moBankName, True)
        ControlMgr.SetVisibleControl(Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Page, textboxBankID, True)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub HideBankName()
        ControlMgr.SetVisibleControl(Page, moBankName, False)
        ControlMgr.SetVisibleControl(Page, txtBankName, False)
    End Sub

    Private Sub DisplayBankNametxt()
        ControlMgr.SetVisibleControl(Page, txtBankName, True)
        ControlMgr.SetVisibleControl(Page, moBankName, False)
        moBankName.SelectedIndex = -1
    End Sub

    Public Sub DisableAllFields()
        ControlMgr.SetEnableControl(Page, txtBankName, False)
        ControlMgr.SetEnableControl(Page, moBankName, False)
        ControlMgr.SetEnableControl(Page, textboxBankID, False)
    End Sub

    Public Sub EnableControlsBasedOnUserCountry()

        If State.myPmtOrderInfoBo IsNot Nothing Then

            Dim ocountry As New Country(State.myPmtOrderInfoBo.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, ocountry.UseBankListId) = Codes.YESNO_Y Then
                DisplayBankNameDP()
                DisplayBankID()
                PopulateBankNameDropdown()
                ControlMgr.SetEnableControl(Page, textboxBankID, False)
            Else
                DisplayBankNametxt()
                ControlMgr.SetEnableControl(Page, textboxBankID, True)
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

        If labelBankID.Text.IndexOf("*") <> 0 Then labelBankID.Text = "* " & labelBankID.Text
        If lblBankName.Text.IndexOf("*") <> 0 Then lblBankName.Text = "* " & lblBankName.Text
        If lblNameonAccount.Text.IndexOf("*") <> 0 Then lblNameonAccount.Text = "* " & lblNameonAccount.Text
        If lblCountryOfBank.Text.IndexOf("*") <> 0 Then lblCountryOfBank.Text = "* " & lblCountryOfBank.Text
    End Sub

#End Region

End Class

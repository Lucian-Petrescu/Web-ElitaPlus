Imports System.Net
Imports System.IO
Imports Assurant.ElitaPlus.ElitaPlusWebApp.UtilityService

Public Class ClaimDeviceInformationController
    Inherits System.Web.UI.UserControl

#Region "Page State"

#End Region

#Region "Private Types"

    Private Class MyState

        Public MyBO As AttributeValue = Nothing
        Public IsNew As Boolean = False

    End Class

#End Region

#Region "properties"
    Public ClaimBO As ClaimBase
    Public thisPage As ElitaPlusPage
    Private ReadOnly Property State() As MyState
        Get
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Public Property ShowDeviceEditImg() As Boolean
        Get
            Return showGridEditImg
        End Get
        Set(ByVal Value As Boolean)
            showGridEditImg = Value
        End Set
    End Property

#End Region

#Region "Constants"
    Private showGridEditImg As Boolean = False
    Private Const UtiliutyEndPointName = "CustomBinding_IUtilityWcf"
    Private Const DATA_TEXT_FIELD_NAME_MANUFACTURER = "MANUFACTURER"
    Private Const DATA_TEXT_FIELD_NAME_MODEL = "MODEL"
    Private Const GRID_CTRL_COMMAND_NAME_EDIT As String = "Edit"
    Private Const GRID_COL_NAME_EQUIPMENT_TYPE As String = "EquipmentType"
    Private Const GRID_CTRL_NAME_LBL_EQUIPMENT_TYPE As String = "lblEquipmentType"
    Private Const GRID_CTRL_NAME_IMG_EDIT = "ImgBtnEdit"
    Private Const GRID_CTRL_NAME_DDL_MAKE = "ddlMake"
    Private Const GRID_CTRL_NAME_DDL_MODEL = "ddlModel"
    Private Const GRID_CTRL_NAME_TXT_MAKE = "txtMake"
    Private Const GRID_CTRL_NAME_TXT_MODEL = "txtModel"
    Private Const GRID_CTRL_NAME_TXT_IMEI = "txtImeiNumber"
    Private Const GRID_CTRL_NAME_TXT_SERIALNUM = "txtSerialNumber"
    Private Const GRID_CTRL_NAME_TXT_COLOR = "txtColor"
    Private Const GRID_CTRL_NAME_TXT_CAPACITY = "txtCapacity"
    Private Const GRID_CTRL_NAME_LBL_ID = "lblID"
    Private Const EDIT_COMMAND_NAME As String = "EditRecord"
    Private Const SAVE_COMMAND_NAME As String = "SaveRecord"
    Private Const CANCEL_COMMAND_NAME As String = "CancelRecord"
    Private Const DATA_TEXT_CLAIM_EQUIP_TYPE As String ="CLAIM_EQUIP_TYPE"
    Private Const DATA_TEXT_CLAIM_EQUIP_TYPE_CLAIMED As String ="CLAIM_EQUIP_TYPE-C"
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.Page.TranslateGridHeader(GvClaimDeviceInformation)
            PopulateGridViewData()
        End If
    End Sub

#End Region

#Region "GridView Events"

    Public Sub RowCommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try

            If (e.CommandName = SAVE_COMMAND_NAME) Then

                If Not PopulateBOFromGrid(CType(sender, GridView)) Then
                    Exit Sub
                End If
                Me.ClaimBO.ClaimedEquipment.EndEdit()
                Me.ClaimBO.ClaimedEquipment.SaveClaimDeviceInfo()
                GvClaimDeviceInformation.EditIndex = -1
                PopulateGridViewData()
            End If

        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GvClaimDeviceInformation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvClaimDeviceInformation.RowDataBound
        Try
            Dim sEquipTypeClaimed As String = LookupListNew.GetDescriptionFromExtCode(DATA_TEXT_CLAIM_EQUIP_TYPE, Authentication.LangId, DATA_TEXT_CLAIM_EQUIP_TYPE_CLAIMED)
            'im dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If Not e.Row.DataItem Is Nothing And e.Row.RowType = DataControlRowType.DataRow Then
                If CType(e.Row.FindControl(GRID_CTRL_NAME_LBL_EQUIPMENT_TYPE), Label).Text = sEquipTypeClaimed Then
                    If Not e.Row.FindControl(GRID_CTRL_NAME_IMG_EDIT) Is Nothing Then
                        CType(e.Row.FindControl(GRID_CTRL_NAME_IMG_EDIT), ImageButton).Visible = ShowDeviceEditImg
                    End If
                End If
            End If

            If e.Row.RowType = DataControlRowType.DataRow AndAlso GvClaimDeviceInformation.EditIndex = e.Row.RowIndex Then
                If CType(e.Row.FindControl(GRID_CTRL_NAME_LBL_EQUIPMENT_TYPE), Label).Text = sEquipTypeClaimed Then

                    'Dim lblDealerCode As Label = CType(e.Row.FindControl(GRID_CTRL_NAME_LBL_DEALER_CODE), Label)
                    Dim ddlMake As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_DDL_MAKE), DropDownList)
                    Dim txtMake As TextBox = CType(e.Row.FindControl(GRID_CTRL_NAME_TXT_MAKE), TextBox)
                    Dim ddlModel As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_DDL_MODEL), DropDownList)
                    Dim txtModel As TextBox = CType(e.Row.FindControl(GRID_CTRL_NAME_TXT_MODEL), TextBox)

                    If Not String.IsNullOrEmpty(Me.ClaimBO.Dealer.Dealer) Then
                        Dim makesAndModels = GetMakesAndModels(Me.ClaimBO.Dealer.Dealer)
                        If (makesAndModels IsNot Nothing) Then
                            Dim ds As New DataSet()
                            ds.ReadXml(New StringReader(makesAndModels))
                            If (ds.Tables.Count > 1) Then
                                If ds.Tables(0).Columns.Contains("Manufacturer") = True Then
                                    ddlMake.DataSource = ds.Tables(0).DefaultView.ToTable(True, "Manufacturer")
                                    ddlMake.DataTextField = DATA_TEXT_FIELD_NAME_MANUFACTURER
                                    ddlMake.DataValueField = DATA_TEXT_FIELD_NAME_MANUFACTURER
                                    ddlMake.DataBind()
                                End If
                                If ds.Tables(0).Columns.Contains("Model") = True Then
                                    ddlModel.DataSource = ds.Tables(0).DefaultView.ToTable(True, "Model")
                                    ddlModel.DataTextField = DATA_TEXT_FIELD_NAME_MODEL
                                    ddlModel.DataValueField = DATA_TEXT_FIELD_NAME_MODEL
                                    ddlModel.DataBind()
                                End If
                            End If
                        End If

                        If ddlMake.Items.Count > 0 Then
                            If ddlMake.Items.FindByText(txtMake.Text) IsNot Nothing Then
                                ddlMake.SelectedValue = txtMake.Text
                                txtMake.Visible = False
                            Else
                                ddlMake.Items.Clear()
                                ddlMake.Visible = False
                                txtMake.Visible = True
                            End If
                        Else
                            ddlMake.Visible = False
                            txtMake.Visible = True
                        End If

                        If ddlModel.Items.Count > 0 Then
                            If ddlModel.Items.FindByText(txtModel.Text) IsNot Nothing Then
                                ddlModel.SelectedValue = txtModel.Text
                                txtModel.Visible = False
                            Else
                                ddlModel.Visible = False
                                ddlModel.Items.Clear()
                                txtModel.Visible = True
                            End If
                        Else
                            ddlModel.Visible = False
                            txtModel.Visible = True
                        End If

                    Else
                        ddlMake.Visible = False
                        txtMake.Visible = True
                        ddlModel.Visible = False
                        txtModel.Visible = True
                    End If

                End If
            End If
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub GvClaimDeviceInformation_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GvClaimDeviceInformation.RowEditing
        Try
            Me.GvClaimDeviceInformation.EditIndex = e.NewEditIndex
            PopulateGridViewData()
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.GvClaimDeviceInformation.EditIndex = -1
            PopulateGridViewData()
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"
    Private Sub PopulateGridViewData()
        Try
            If Not Me.ClaimBO Is Nothing Then
                Me.GvClaimDeviceInformation.DataSource = Me.ClaimBO.ClaimedEnrolledEquipments()
                Me.GvClaimDeviceInformation.DataBind()
            End If
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Shared Function GetMakesAndModels(dealer As String) As Object
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__UTILTY_SERVICE), False)
        Dim client = New UtilityWcfClient(UtiliutyEndPointName, oWebPasswd.Url)
        Dim token = client.LoginBody(oWebPasswd.UserId, oWebPasswd.Password, Codes.SERVICE_TYPE_GROUP_NAME)

        Dim makesAndModels As Object
        If (Not String.IsNullOrEmpty(token)) Then
            Dim requestXmlData = "<GetMakesAndModelsDs><GetMakesAndModels><DealerCode>" + dealer + "</DealerCode></GetMakesAndModels></GetMakesAndModelsDs>"
            makesAndModels = client.ProcessRequest(token, "GetMakesAndModels", requestXmlData.ToString())
        End If

        Return makesAndModels

    End Function

    Private Function PopulateBOFromGrid(ByVal pGridView As GridView) As Boolean
        Dim row As GridViewRow = pGridView.Rows(pGridView.EditIndex)
        Dim blnUpdateSuccess As Boolean = True
        With Me.ClaimBO
            If Not .ClaimedEquipment Is Nothing Then
                Dim lblId As Label = CType(row.FindControl(GRID_CTRL_NAME_LBL_ID), Label)
                Dim _ClaimEquipment As ClaimEquipment = Me.ClaimBO.ClaimEquipmentChildren.GetChild(Guid.Parse(lblId.Text))
                Dim ddlMake As DropDownList = CType(row.FindControl(GRID_CTRL_NAME_DDL_MAKE), DropDownList)
                Dim txtMake As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_MAKE), TextBox)
                Dim ddlModel As DropDownList = CType(row.FindControl(GRID_CTRL_NAME_DDL_MODEL), DropDownList)
                Dim txtModel As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_MODEL), TextBox)
                Dim txtImei As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_IMEI), TextBox)
                Dim txtSerialNumber As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_SERIALNUM), TextBox)
                Dim txtColor As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_COLOR), TextBox)
                Dim txtCapacity As TextBox = CType(row.FindControl(GRID_CTRL_NAME_TXT_CAPACITY), TextBox)
                Dim makeName As String

                .ClaimedEquipment = _ClaimEquipment

                If ddlMake.Items.Count > 0 Then
                    makeName = ddlMake.SelectedItem.Text
                Else
                    makeName = txtMake.Text
                End If

                Dim makeId As Guid = LookupListNew.GetIdFromDescription(LookupListNew.GetManufacturerLookupList(Authentication.CompanyGroupId), makeName)
                If makeId.Equals(Guid.Empty) Then
                    Me.Page.MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.BO_ERROR_NO_MANUFACTURER_FOUND, True)
                    blnUpdateSuccess = False
                    Return blnUpdateSuccess
                Else
                    Me.Page.MasterPage.MessageController.Clear()
                    .ClaimedEquipment.ManufacturerId = makeId
                End If

                If ddlModel.Items.Count > 0 Then
                    .ClaimedEquipment.Model = ddlModel.SelectedItem.Text
                Else
                    .ClaimedEquipment.Model = txtModel.Text
                End If
                .ClaimedEquipment.IMEINumber = txtImei.Text
                .ClaimedEquipment.SerialNumber = txtSerialNumber.Text
                .ClaimedEquipment.Color = txtColor.Text
                .ClaimedEquipment.Memory = txtCapacity.Text

            End If
        End With
        Return blnUpdateSuccess
    End Function

#End Region



End Class
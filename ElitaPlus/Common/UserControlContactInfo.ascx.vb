Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class UserControlContactInfo
    Inherits UserControl


#Region "Constants"
    Public Const SALUTATION As String = "SalutationId"
    Public Const ADDRESS_TYPE As String = "AddressTypeId"
    Public Const NAME As String = "Name"
    Public Const HOME_PHONE As String = "HomePhone"
    Public Const EMAIL As String = "Email"
    Public Const WORK_PHONE As String = "WorkPhone"
    Public Const CELL_PHONE As String = "CellPhone"
#End Region

#Region "Properties"

    Public Property MyGenBO() As BusinessObjectBase
        Get
            Return CType(Page.StateSession.Item(UniqueID), BusinessObjectBase)
        End Get
        Set(Value As BusinessObjectBase)
            Page.StateSession.Item(UniqueID) = Value
        End Set
    End Property

    Private _ContactInfoBO As ContactInfo
    Public Property ContactInfoBO() As ContactInfo
        Get
            Return _ContactInfoBO
        End Get
        Set(Value As ContactInfo)
            _ContactInfoBO = Value
        End Set
    End Property


    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

#Region "Constants"
    Public Const NO As String = "N"
    Public Const YES As String = "Y"
    Public Const CODE As String = "Code"
    Public Const FIRST_ROW As Integer = 0
#End Region

#Region "Attributes"

    Dim isSalutation As Boolean

#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        EnableDisableFields()
    End Sub

#End Region

#Region "Load"

    '' REQ-784
    Public Sub NewClaimBind(oBusObj As BusinessObjectBase)
        'ContactInfoBO = oContactInfo
        MyGenBO = oBusObj
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        PopulateDropdowns()
    End Sub

    ''REQ-784
    Protected Sub BindBoPropertiesToLabels()
        Page.BindBOPropertyToLabel(MyGenBO, SALUTATION, moSalutationLabel)
        Page.BindBOPropertyToLabel(MyGenBO, ADDRESS_TYPE, Label1)
        Page.BindBOPropertyToLabel(MyGenBO, NAME, moContactNameLabel)
        Page.BindBOPropertyToLabel(MyGenBO, HOME_PHONE, moHomePhoneLabel)
        Page.BindBOPropertyToLabel(MyGenBO, EMAIL, moEmailAddressLabel)
        Page.BindBOPropertyToLabel(MyGenBO, WORK_PHONE, moWorkPhoneLabel)
        Page.BindBOPropertyToLabel(MyGenBO, CELL_PHONE, moCellPhoneLabel)
    End Sub


    ''REQ-784
    Protected Sub PopulateDropdowns()
        Try
            'Dim oSalutationList As DataView = LookupListNew.DropdownLookupList("SLTN", Authentication.LangId, True)
            'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.cboSalutationId, oSalutationList)

            'Dim oAddressTypeList As DataView = LookupListNew.DropdownLookupList("ATYPE", Authentication.LangId, True)
            'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.moAddressTypeDrop_WRITE, oAddressTypeList)

            Dim SalutationList As ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SLTN",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboSalutationId.Populate(SalutationList,
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })

            Dim AddressTypeList As ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ATYPE",
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            moAddressTypeDrop_WRITE.Populate(AddressTypeList,
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })
        Catch ex As Exception

        End Try
    End Sub
    '' REQ-784
    Public Sub Bind(oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        PopulateDropdowns()
        PopulateControlFromBO()
    End Sub
    'Req 784
    Public Sub PopulateControlFromBO()
        If MyGenBO IsNot Nothing Then
            With MyGenBO

                Page.PopulateControlFromPropertyName(MyGenBO, moAddressTypeDrop_WRITE, "AddressTypeId")
                Page.PopulateControlFromPropertyName(MyGenBO, cboSalutationId, "SalutationId")
                Page.PopulateControlFromPropertyName(MyGenBO, moContactNameText, "Name")
                Page.PopulateControlFromPropertyName(MyGenBO, moHomePhoneText, "HomePhone")
                Page.PopulateControlFromPropertyName(MyGenBO, moEmailAddressText, "Email")
                Page.PopulateControlFromPropertyName(MyGenBO, moWorkPhoneText, "WorkPhone")
                Page.PopulateControlFromPropertyName(MyGenBO, moCellPhoneText, "CellPhone")
            End With
        End If
    End Sub
    ''REQ-784
    Public Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False)

        '' Populate child control inside the parent user control
        moAddressController.PopulateBOFromControl(True)

        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If

        If MyGenBO IsNot Nothing AndAlso Not MyGenBO.IsDeleted Then
            With MyGenBO
                Page.PopulateBOProperty(MyGenBO, "SalutationId", cboSalutationId)
                Page.PopulateBOProperty(MyGenBO, "AddressTypeId", moAddressTypeDrop_WRITE)
                Page.PopulateBOProperty(MyGenBO, "Name", moContactNameText)
                Page.PopulateBOProperty(MyGenBO, "HomePhone", moHomePhoneText)
                Page.PopulateBOProperty(MyGenBO, "Email", moEmailAddressText)
                Page.PopulateBOProperty(MyGenBO, "WorkPhone", moWorkPhoneText)
                Page.PopulateBOProperty(MyGenBO, "CellPhone", moCellPhoneText)
                Page.PopulateBOProperty(MyGenBO, "AddressId", CType(MyGenBO, ContactInfo).AddressId)


            End With
        End If
    End Sub


#End Region

#Region "Controlling Logic"

    ''REQ-784
    Protected Sub EnableDisableFields()
        Try

            '' Always Visible False
            ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moSalutationText, False)
            'ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moSalutationLabel, False)
            ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moAddressTypeText, False)
            ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moAddressController.FindControl("moCountryText"), False)
            ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moAddressController.FindControl("moRegionText"), False)

            'isSalutation = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N"))

            'If Not MyGenBO Is Nothing Then
            '    isSalutation = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, MyGenBO.SalutationId)
            'End If

            'If Not isSalutation Then
            '    ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moSalutationLabel, True)
            '    ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.cboSalutationId, True)
            'Else

            'End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub EnableDisablecontrol(bvalue As Boolean)

        cboSalutationId.Enabled = True
        moAddressTypeDrop_WRITE.Enabled = True
        moEmailAddressText.ReadOnly = bvalue
        moCellPhoneText.ReadOnly = bvalue
        moContactNameText.ReadOnly = bvalue
        moWorkPhoneText.ReadOnly = bvalue
        moHomePhoneText.ReadOnly = bvalue
        ' ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moCountryDrop_WRITE"), True)
        ' ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moRegionDrop_WRITE"), True)
        ' ControlMgr.SetEnableControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moCountryDrop_WRITE"), True)
        ' ControlMgr.SetEnableControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moRegionDrop_WRITE"), True)


    End Sub
    Private Function GetYesNo(LanguageId As Guid, oId As Guid) As Boolean
        Dim oYesList As DataView = LookupListNew.GetListItemId(oId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
        Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
        If oYesNo = YES Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class

































































































































































































































































































































































































































































































































































































































































































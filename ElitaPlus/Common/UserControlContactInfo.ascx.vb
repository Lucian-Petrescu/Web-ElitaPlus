Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class UserControlContactInfo
    Inherits System.Web.UI.UserControl


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
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), BusinessObjectBase)
        End Get
        Set(ByVal Value As BusinessObjectBase)
            Me.Page.StateSession.Item(Me.UniqueID) = Value
        End Set
    End Property

    Private _ContactInfoBO As ContactInfo
    Public Property ContactInfoBO() As ContactInfo
        Get
            Return _ContactInfoBO
        End Get
        Set(ByVal Value As ContactInfo)
            Me._ContactInfoBO = Value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.EnableDisableFields()
    End Sub

#End Region

#Region "Load"

    '' REQ-784
    Public Sub NewClaimBind(ByVal oBusObj As BusinessObjectBase)
        'ContactInfoBO = oContactInfo
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(MyGenBO)
        End If
        Me.PopulateDropdowns()
    End Sub

    ''REQ-784
    Protected Sub BindBoPropertiesToLabels()
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, SALUTATION, Me.moSalutationLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, ADDRESS_TYPE, Me.Label1)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, NAME, Me.moContactNameLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, HOME_PHONE, Me.moHomePhoneLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, EMAIL, Me.moEmailAddressLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, WORK_PHONE, Me.moWorkPhoneLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, CELL_PHONE, Me.moCellPhoneLabel)
    End Sub


    ''REQ-784
    Protected Sub PopulateDropdowns()
        Try
            'Dim oSalutationList As DataView = LookupListNew.DropdownLookupList("SLTN", Authentication.LangId, True)
            'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.cboSalutationId, oSalutationList)

            'Dim oAddressTypeList As DataView = LookupListNew.DropdownLookupList("ATYPE", Authentication.LangId, True)
            'CType(Me.Page, ElitaPlusPage).BindListControlToDataView(Me.moAddressTypeDrop_WRITE, oAddressTypeList)

            Dim SalutationList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SLTN",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Me.cboSalutationId.Populate(SalutationList,
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })

            Dim AddressTypeList As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ATYPE",
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Me.moAddressTypeDrop_WRITE.Populate(AddressTypeList,
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })
        Catch ex As Exception

        End Try
    End Sub
    '' REQ-784
    Public Sub Bind(ByVal oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        PopulateDropdowns()
        PopulateControlFromBO()
    End Sub
    'Req 784
    Public Sub PopulateControlFromBO()
        If Not Me.MyGenBO Is Nothing Then
            With Me.MyGenBO

                Me.Page.PopulateControlFromPropertyName(MyGenBO, moAddressTypeDrop_WRITE, "AddressTypeId")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, cboSalutationId, "SalutationId")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moContactNameText, "Name")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moHomePhoneText, "HomePhone")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moEmailAddressText, "Email")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moWorkPhoneText, "WorkPhone")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moCellPhoneText, "CellPhone")
            End With
        End If
    End Sub
    ''REQ-784
    Public Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False)

        '' Populate child control inside the parent user control
        moAddressController.PopulateBOFromControl(True)

        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If

        If Not Me.MyGenBO Is Nothing AndAlso Not MyGenBO.IsDeleted Then
            With Me.MyGenBO
                Me.Page.PopulateBOProperty(Me.MyGenBO, "SalutationId", cboSalutationId)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "AddressTypeId", moAddressTypeDrop_WRITE)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "Name", moContactNameText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "HomePhone", moHomePhoneText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "Email", moEmailAddressText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "WorkPhone", moWorkPhoneText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "CellPhone", moCellPhoneText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "AddressId", CType(Me.MyGenBO, ContactInfo).AddressId)


            End With
        End If
    End Sub


#End Region

#Region "Controlling Logic"

    ''REQ-784
    Protected Sub EnableDisableFields()
        Try

            '' Always Visible False
            ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moSalutationText, False)
            'ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moSalutationLabel, False)
            ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moAddressTypeText, False)
            ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moCountryText"), False)
            ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), moAddressController.FindControl("moRegionText"), False)

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
    Public Sub EnableDisablecontrol(ByVal bvalue As Boolean)

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
    Private Function GetYesNo(ByVal LanguageId As Guid, ByVal oId As Guid) As Boolean
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

































































































































































































































































































































































































































































































































































































































































































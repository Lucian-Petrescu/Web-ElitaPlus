
'*  $log$
'*  First Line
'*    tt
'*  First Line
'*    test 5
'*  First Line
'*    test 4
'*    test3
'*    test2
'  hola amigos 2 3 4
'*
'* $Id: CertificateListFormAJAX.aspx.vb, 129148+1 2014/04/14 18:05:01 co0799 $
'*
'* $Header: ?/International/Source/Elita+/ElitaPlus/test/CertificateListFormAJAX.aspx.vb, 129148+1 2014/04/14 18:05:01 co0799 $
'*
'* $Source: ?/International/Source/Elita+/ElitaPlus/test/CertificateListFormAJAX.aspx.vb $
'*
'* $Author: co0799 $
'*
'* $Date: 2014/04/14 18:05:01 $
'*
'* $RCSFile: CertificateListFormAJAX.aspx.vb $
'*
'* $PrevRevision: 129148 $
'*
'* $Revision: 129148+1 $
'*
'*


Imports elp = Assurant.ElitaPlus.BusinessObjects.Common
Imports elpTrans = Assurant.ElitaPlus.BusinessObjects.Translation
Imports Assurant.Common.Localization
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Certificates

    Partial Public Class CertificateListFormAJAX
        Inherits ElitaPlusSearchPage

        Protected WithEvents progressFrame As System.Web.UI.HtmlControls.HtmlGenericControl

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CERTIFICATE_ID_IDX As Integer = 1
        Public Const GRID_COL_CERTIFICATE_IDX As Integer = 2
        Public Const GRID_COL_STATUS_CODE_IDX As Integer = 3
        Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 4
        Public Const GRID_COL_ADDRESS_IDX As Integer = 5
        Public Const GRID_COL_ZIP_IDX As Integer = 6
        Public Const GRID_COL_TAXID_IDX As Integer = 7
        Public Const GRID_COL_DEALER_IDX As Integer = 8
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 9

        Public Const GRID_TOTAL_COLUMNS As Integer = 10

        Public Const MAX_LIMIT As Integer = 1000

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public certificateNumber As String
            Public customerName As String
            Public address As String
            Public zip As String
            Public taxId As String
            Public dealerId As Guid
            Public dealerName As String = String.Empty
            Public selectedSortById As Guid = Guid.Empty
            Public searchDV As Certificate.CertificateSearchDV = Nothing
            Public searchClick As Boolean = False
            Public certificatesFoundMSG As String

            'Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            'Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
            Public selectedCertificateId As Guid = Guid.Empty

            Public IsGridVisible As Boolean = False

            Sub New()
                'SortColumns(GRID_COL_CERTIFICATE_IDX) = Certificate.CertificateSearchDV.COL_CERTIFICATE_NUMBER
                'SortColumns(GRID_COL_CUSTOMER_NAME_IDX) = Certificate.CertificateSearchDV.COL_CUSTOMER_NAME
                'SortColumns(GRID_COL_ADDRESS_IDX) = Certificate.CertificateSearchDV.COL_ADDRESS
                'SortColumns(GRID_COL_ZIP_IDX) = Certificate.CertificateSearchDV.COL_ZIP
                'SortColumns(GRID_COL_TAXID_IDX) = Certificate.CertificateSearchDV.COL_TAX_ID
                'SortColumns(GRID_COL_DEALER_IDX) = Certificate.CertificateSearchDV.COL_DEALER

                'IsSortDesc(GRID_COL_CERTIFICATE_IDX) = False
                'IsSortDesc(GRID_COL_CUSTOMER_NAME_IDX) = False
                'IsSortDesc(GRID_COL_ADDRESS_IDX) = False
                'IsSortDesc(GRID_COL_ZIP_IDX) = False
                'IsSortDesc(GRID_COL_TAXID_IDX) = False
                'IsSortDesc(GRID_COL_DEALER_IDX) = False
            End Sub

            Public ReadOnly Property CurrentSortExpresion() As String
                Get
                    'Dim s As String
                    'Dim i As Integer
                    'Dim sortExp As String = ""
                    'For i = 0 To Me.SortColumns.Length - 1
                    '    If Not Me.SortColumns(i) Is Nothing Then
                    '        sortExp &= Me.SortColumns(i)
                    '        If Me.IsSortDesc(i) Then sortExp &= " DESC"
                    '        sortExp &= ","
                    '    End If
                    'Next
                    'Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            'Public Sub ToggleSort(ByVal gridColIndex As Integer)
            '    IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
            'End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                'Me.State.searchDV = Nothing
                Dim retObj As CertificateForm.ReturnType = CType(ReturnPar, CertificateForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedCertificateId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.ScriptManager1.Services(0).Path = "http://" + Environment.MachineName + "/ElitaInternalWS/Utilities/UtilityWS.asmx"
            lblTK.Value = Authentication.CreateWSToken(Authentication.CurrentUser.NetworkId)
            Me.ErrorCtrl.Attributes.Add("style", "display: none")
            cboPageSize.Attributes.Add("onChange", "GetList('GetCertificateList');")
            'Me.trPageSize.Visible = False

            Try
                If Not Me.IsPostBack Then
                    Me.PopulateDealerDropdown(Me.moDealerDrop)
                    Me.PopulateSortByDropDown()
                    Me.GetStateProperties()
                    SetFocus(Me.moCertificateText)
                Else
                    Me.SetStateProperties()
                End If

                InstallDisplayProgressBar()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Try
                Me.State.certificateNumber = Me.moCertificateText.Text.ToUpper
                Me.State.customerName = Me.moCustomerNameText.Text.ToUpper
                Me.State.address = Me.moAddressText.Text.ToUpper
                Me.State.zip = Me.moZipText.Text.ToUpper
                Me.State.taxId = Me.moTaxIdText.Text.ToUpper
                Me.State.dealerId = Me.GetSelectedItem(moDealerDrop)
                'Me.State.dealerName = LookupListNew.GetDescriptionFromId("DEALERS", Me.State.dealerId)
                Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)
                '  Me.State.dealerName = Me.GetSelectedDescription(Me.moDealerDrop)
                Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub GetStateProperties()
            Try
                Me.moCertificateText.Text = Me.State.certificateNumber
                Me.moCustomerNameText.Text = Me.State.customerName
                Me.moAddressText.Text = Me.State.address
                Me.moZipText.Text = Me.State.zip
                Me.moTaxIdText.Text = Me.State.taxId
                Me.SetSelectedItem(Me.moDealerDrop, Me.State.dealerId)
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(ByVal dealerDropDownList As DropDownList)
            Try
                'Me.BindListControlToDataView(dealerDropDownList,
                '              LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)


                Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)

                For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.DealerListByCompany,
                                                        context:=New ListContext() With
                                                        {
                                                          .CompanyId = CompanyId
                                                        })
                    If Dealers.Count > 0 Then
                        If Not DealerList Is Nothing Then
                            DealerList.AddRange(Dealers)
                        Else
                            DealerList = Dealers.Clone()
                        End If
                    End If
                Next

                dealerDropDownList.Populate(DealerList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                Me.SetSelectedItem(dealerDropDownList, Me.State.dealerId)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Sub PopulateSortByDropDown()
            Try
                'Me.BindListControlToDataView(Me.cboSortBy, LookupListNew.GetCertificateSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)

                Dim sortBy As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="CSDRP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                Me.cboSortBy.Populate(sortBy.ToArray(), New PopulateOptions())
                Dim defaultSelectedCodeId As Guid = (From sb In sortBy
                                                     Where sb.Code = "CUSTOMER_NAME"
                                                     Select sb.ListItemId).FirstOrDefault()

                If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                    Me.State.selectedSortById = defaultSelectedCodeId
                Else
                    Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


    End Class
End Namespace

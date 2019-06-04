Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables

    Partial Public Class RegistrationLetterSearchForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        ' Private Const PRODUCTCODE_LIST_FORM001 As String = "PRODUCTCODE_LIST_FORM001" ' Maintain Product Code List Exception
        ' Public Const URL As String = "Interfaces/InvoiceControlListForm.aspx"
        Public Const PAGETITLE As String = "REGISTRATION_LETTER"
        Public Const PAGETAB As String = "TABLES"

        Private Const LABEL_DEALER As String = "DEALER"

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_REGISTRATION_LETTER_ID As Integer = 1
        Public Const GRID_COL_DEALER_CODE As Integer = 2
        Public Const GRID_COL_DEALER_NAME As Integer = 3
        Public Const GRID_COL_NUMBER_OF_DAYS As Integer = 4
        Public Const GRID_TOTAL_COLUMNS As Integer = 5

        Public Const GRID_CTRL_REGISTRATION_LETTER_ID As String = "lblLetterID"

#End Region

#Region "Variables"
        Protected WithEvents multipleDropControl As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl
        Private IsReturningFromChild As Boolean = False
#End Region

#Region "Properties"

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState
            Public searchDV As RegistrationLetter.RegistrationLetterSearchDV = Nothing
            Public SortExpression As String = RegistrationLetter.RegistrationLetterSearchDV.COL_DEALER_CODE


            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public SearchDataView As RegistrationLetter.RegistrationLetterSearchDV
            Public moRegistrationLetterId As Guid = Guid.Empty


            Private moDealerId As Guid = Guid.Empty


#Region "State-Properties"

            'Public Property ProductCodeMask() As String
            '    Get
            '        Return msProductCode
            '    End Get
            '    Set(ByVal Value As String)
            '        msProductCode = Value
            '    End Set
            'End Property

            Public Property DealerId() As Guid
                Get
                    Return moDealerId
                End Get
                Set(ByVal Value As Guid)
                    moDealerId = Value
                End Set
            End Property

            'Public Property ProductCodeId() As Guid
            '    Get
            '        Return moDealerId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moDealerId = Value
            '    End Set
            'End Property

            'Public Property RiskGroupId() As Guid
            '    Get
            '        Return moRiskGroupId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moRiskGroupId = Value
            '    End Set
            'End Property

            'Public Property PageIndex() As Integer
            '    Get
            '        Return mnPageIndex
            '    End Get
            '    Set(ByVal Value As Integer)
            '        mnPageIndex = Value
            '    End Set
            'End Property

            'Public Property PageSize() As Integer
            '    Get
            '        Return mnPageSize
            '    End Get
            '    Set(ByVal Value As Integer)
            '        mnPageSize = Value
            '    End Set
            'End Property

            'Public Property PageSort() As String
            '    Get
            '        Return msPageSort
            '    End Get
            '    Set(ByVal Value As String)
            '        msPageSort = Value
            '    End Set
            'End Property

            'Public Property SearchDataView() As ProductCode.ProductCodeSearchDV
            '    Get
            '        Return moSearchDataView
            '    End Get
            '    Set(ByVal Value As ProductCode.ProductCodeSearchDV)
            '        moSearchDataView = Value
            '    End Set
            'End Property
#End Region

            Sub New()
            End Sub

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property


#Region "Page Return"
        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moRegistrationLetterId = retObj.moRegistrationLetterId
                        Case Else
                            Me.State.moRegistrationLetterId = Guid.Empty
                    End Select

                    Grid.PageIndex = Me.State.PageIndex
                    Grid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    PopulateDealer()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moRegistrationLetterId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oRegistrationLetterId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moRegistrationLetterId = oRegistrationLetterId
                Me.BoChanged = boChanged
            End Sub


        End Class

#End Region

#End Region

#Region "Handlers"

#Region "Hanlers-Init"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    Me.SortDirection = Me.State.SortExpression
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    TranslateGridHeader(Grid)

                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                    Else
                        ' It is returning from detail
                        Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
                        'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
                    End If

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

 
        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try                
                Me.State.PageIndex = Grid.PageIndex
                Me.State.moRegistrationLetterId = Guid.Empty
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_REGISTRATION_LETTER_ID).FindControl(Me.GRID_CTRL_REGISTRATION_LETTER_ID), Label)
                    Me.State.moRegistrationLetterId = New Guid(lblCtrl.Text)
                    SetSession()
                    Me.callPage(RegistrationLetterForm.URL, Me.State.moRegistrationLetterId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
                
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSearch.Click
            Try
                Grid.PageIndex = Me.NO_PAGE_INDEX
                State.moRegistrationLetterId = Guid.Empty
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
            Try
                Me.State.moRegistrationLetterId = Guid.Empty
                SetSession()
                Me.callPage(RegistrationLetterForm.URL, Me.State.moRegistrationLetterId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub PopulateDealer()
            Try

                Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                TheDealerControl.NothingSelected = True
                TheDealerControl.BindData(oDataView)
                TheDealerControl.AutoPostBackDD = False
                TheDealerControl.NothingSelected = True
                TheDealerControl.SelectedGuid = Me.State.DealerId

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

 
        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = RegistrationLetter.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, _
                                        TheDealerControl.SelectedGuid)
                End If
                Me.State.searchDV.Sort = Me.SortDirection
                
                Session("recCount") = Me.State.searchDV.Count

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moRegistrationLetterId, Me.Grid, Me.State.PageIndex)
                
                Me.State.PageIndex = Me.Grid.PageIndex

                If (Me.State.searchDV.Count = 0) Then
                    Grid.DataSource = RegistrationLetter.getEmptyList(Me.State.searchDV)
                    Grid.DataBind()
                    Grid.Rows(0).Visible = False

                    Me.State.searchDV = Nothing
                Else
                    Me.Grid.Enabled = True
                    Me.Grid.DataSource = Me.State.searchDV
                    HighLightSortColumn(Grid, Me.SortDirection)
                    Me.Grid.DataBind()
                End If

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

                If Not Me.State.searchDV Is Nothing Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = 0 & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            Me.State.moRegistrationLetterId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .DealerId = TheDealerControl.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
                '.SearchDataView = Me.State.searchDV
            End With
        End Sub

#End Region


        
    End Class

End Namespace
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
                Set(Value As Guid)
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
        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moRegistrationLetterId = retObj.moRegistrationLetterId
                        Case Else
                            State.moRegistrationLetterId = Guid.Empty
                    End Select

                    Grid.PageIndex = State.PageIndex
                    Grid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    PopulateDealer()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moRegistrationLetterId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oRegistrationLetterId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moRegistrationLetterId = oRegistrationLetterId
                Me.BoChanged = boChanged
            End Sub


        End Class

#End Region

#End Region

#Region "Handlers"

#Region "Hanlers-Init"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then
                    SortDirection = State.SortExpression
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    TranslateGridHeader(Grid)

                    If Not IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                    Else
                        ' It is returning from detail
                        PopulateGrid(POPULATE_ACTION_SAVE)
                        'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
                    End If

                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

 
        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try                
                State.PageIndex = Grid.PageIndex
                State.moRegistrationLetterId = Guid.Empty
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_REGISTRATION_LETTER_ID).FindControl(GRID_CTRL_REGISTRATION_LETTER_ID), Label)
                    State.moRegistrationLetterId = New Guid(lblCtrl.Text)
                    SetSession()
                    callPage(RegistrationLetterForm.URL, State.moRegistrationLetterId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
                
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(sender As Object, e As EventArgs) Handles moBtnSearch.Click
            Try
                Grid.PageIndex = NO_PAGE_INDEX
                State.moRegistrationLetterId = Guid.Empty
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(sender As Object, e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                State.moRegistrationLetterId = Guid.Empty
                SetSession()
                callPage(RegistrationLetterForm.URL, State.moRegistrationLetterId)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
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
                TheDealerControl.SelectedGuid = State.DealerId

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

 
        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = RegistrationLetter.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, _
                                        TheDealerControl.SelectedGuid)
                End If
                State.searchDV.Sort = SortDirection
                
                Session("recCount") = State.searchDV.Count

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.moRegistrationLetterId, Grid, State.PageIndex)
                
                State.PageIndex = Grid.PageIndex

                If (State.searchDV.Count = 0) Then
                    Grid.DataSource = RegistrationLetter.getEmptyList(State.searchDV)
                    Grid.DataBind()
                    Grid.Rows(0).Visible = False

                    State.searchDV = Nothing
                Else
                    Grid.Enabled = True
                    Grid.DataSource = State.searchDV
                    HighLightSortColumn(Grid, SortDirection)
                    Grid.DataBind()
                End If

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                If State.searchDV IsNot Nothing Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = 0 & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            State.moRegistrationLetterId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .DealerId = TheDealerControl.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                '.SearchDataView = Me.State.searchDV
            End With
        End Sub

#End Region


        
    End Class

End Namespace
Public Class UserControlCallerInfo
    Inherits UserControl
#Region "Constant"
    'Control Name
    Private Const ImageButtonRadio As String = "EditButton_WRITE"
    Private Const TextBoxFirstName As String = "TextboxFirstName"
    Private Const TextBoxLastName As String = "TextboxLastName"
    Private Const TextBoxWorkPhone As String = "TextboxWorkPhone"
    Private Const TextBoxEmail As String = "TextboxEmail"
    Private Const DropDownlistRelationship As String = "ddlRelationship"
    'Grid columns sequence
    Private Const GridViewCallerColFirstName As Integer = 1
    Private Const GridViewCallerColLastName As Integer = 2
    Private Const GridViewCallerColWorkPhone As Integer = 3
    Private Const GridViewCallerColEmail As Integer = 4
    
    'Table columns sequence
    Private Const CallerTableColFirstName As Integer = 0
    Private Const CallerTableColLastName As Integer = 1
    Private Const CallerTableColWorkPhone As Integer = 2
    Private Const CallerTableColEmail As Integer = 3
#End Region
#Region "Property"
    Public Property FirstName() As String
    Public Property LastName() As String
    Public Property WorkPhoneNumber() As String
    Public Property Email() As String
    Public Property RelationshipCode() As String
    Public Property RelationshipDesc() As String
    Public Property ItemSelected() As Boolean

#End Region

#Region "Variables"
    Public Event SelectedIndexChanged1(aSrc As UserControlCallerInfo)

    Public Delegate Sub SelectedIndexChanged(aSrc As UserControlCallerInfo)
    Public Event GridSelectionHandler  As SelectedIndexChanged
    
#End Region
    
#Region "Control State"
    Private Class MyState
        Public CallersDataTable As DataTable

        Public Sub New()
        End Sub
    End Class
    Private ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property
#End Region
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    
#Region "Caller View - Other Public Sub"
    Public Sub PopulateGridViewCaller(certId As Guid,  Optional ByVal caseId As Guid = nothing, Optional ByVal IsAuthenticated As Boolean = True)
        Page.TranslateGridHeader(GridViewCaller)
        Dim emptyDataRow As DataRow

        If  IsAuthenticated = True
            If (Not certId.Equals(Guid.Empty)) Then
                State.callersDataTable = Certificate.getCallerListForCert(certId)            
            Else
                State.callersDataTable = Certificate.getCallerListForCase(caseId)  
            End If
        Else
            State.callersDataTable = Certificate.getCallerListForCert(Guid.Empty)
        End If

        emptyDataRow = State.callersDataTable.NewRow()
        State.callersDataTable.Rows.InsertAt(emptyDataRow, 0)
        GridViewCaller.EditIndex = 0
        GridViewCaller.SelectedIndex = 0
        ItemSelected = True
        BindData()


    End Sub

    Public Sub DisableGridSelection()
        
        GridViewCaller.EditIndex = -1
        GridViewCaller.SelectedIndex = -1
        ItemSelected = False
        BindData()

    End Sub

    Public Sub PopulateGridViewPrevCaller(certId As Guid, Optional ByVal caseId As Guid = nothing, Optional ByVal IsAuthenticated As Boolean = True)
        Page.TranslateGridHeader(GridViewCaller)
        Dim emptyDataRow As DataRow        
        State.callersDataTable = Certificate.getCallerListForCert(Guid.Empty)
                 
        'Session("PrevCallerFirstName") = "c"
        'Session("PrevCallerLastName") = "k"
        'Session("PrevCallerRelationshipCode") = "Self"
        'Session("PrevCallerWorkPhoneNumber") = "1234"
        'Session("PrevCallerEmail") = "rc.k@y.com"
       
        emptyDataRow = State.callersDataTable.NewRow()
        emptyDataRow("FIRST_NAME")= Session("PrevCallerFirstName")
        emptyDataRow("LAST_NAME") = Session("PrevCallerLastName")
        emptyDataRow("RELATIONSHIP") = Session("PrevCallerRelationshipCode")
        emptyDataRow("WORK_PHONE") = Session("PrevCallerWorkPhoneNumber")
        emptyDataRow("EMAIL") = Session("PrevCallerEmail")

        State.callersDataTable.Rows.InsertAt(emptyDataRow, 0)
        GridViewCaller.EditIndex = -1
        GridViewCaller.SelectedIndex = -1
        ItemSelected = False
        BindData()
    End Sub

    Public Sub GetCallerInformation()
        For i As Integer = 0 To GridViewCaller.Rows.Count - 1
            If i = GridViewCaller.SelectedIndex Then
                FirstName = DirectCast(GridViewCaller.Rows(i).FindControl(TextBoxFirstName), TextBox).Text
                LastName = DirectCast(GridViewCaller.Rows(i).FindControl(TextBoxLastName), TextBox).Text
                RelationshipCode = DirectCast(GridViewCaller.Rows(i).FindControl(DropDownlistRelationship), DropDownList).SelectedValue
                RelationshipDesc = DirectCast(GridViewCaller.Rows(i).FindControl(DropDownlistRelationship), DropDownList).SelectedItem.Text
                WorkPhoneNumber = DirectCast(GridViewCaller.Rows(i).FindControl(TextBoxWorkPhone), TextBox).Text
                Email = DirectCast(GridViewCaller.Rows(i).FindControl(TextBoxEmail), TextBox).Text
                Exit Sub
            End If
        Next
    End Sub
#End Region
#Region "Caller View - Other Private Sub/Function"
    Private Sub BindData()
        If (State.CallersDataTable IsNot Nothing) Then
            GridViewCaller.DataSource = State.CallersDataTable
            GridViewCaller.DataBind()
        End If
    End Sub
    Private Sub GridViewCaller_RowDataBound(sender As System.Object, e As GridViewRowEventArgs) Handles GridViewCaller.RowDataBound
        Try
            Dim rowType As DataControlRowType = e.Row.RowType
            If rowType = DataControlRowType.DataRow Then
                Dim editButtonWrite As ImageButton = DirectCast(e.Row.FindControl(ImageButtonRadio), ImageButton)
                Dim rowState As DataControlRowState = e.Row.RowState
                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then
                    editButtonWrite.ImageUrl = "~/Navigation/images/icons/radioButtonOn.png"
                    FillRelationshipDropDownList(e.Row)
                Else
                    editButtonWrite.ImageUrl = "~/Navigation/images/icons/radioButtonOff.png"
                End If
            End If
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewCaller_RowCommand(source As Object, e As GridViewCommandEventArgs) Handles GridViewCaller.RowCommand
        Try
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                Dim nIndex As Integer 
                nIndex = CInt(e.CommandArgument)
                GridViewCaller.EditIndex = nIndex
                GridViewCaller.SelectedIndex = nIndex

                BindData()
                Page.SetSelectedGridText(GridViewCaller, GridViewCallerColFirstName, State.callersDataTable.Rows(GridViewCaller.SelectedIndex).Item(CallerTableColFirstName).ToString)
                Page.SetSelectedGridText(GridViewCaller, GridViewCallerColLastName, State.callersDataTable.Rows(GridViewCaller.SelectedIndex).Item(CallerTableColLastName).ToString)
                Page.SetSelectedGridText(GridViewCaller, GridViewCallerColWorkPhone, State.callersDataTable.Rows(GridViewCaller.SelectedIndex).Item(CallerTableColWorkPhone).ToString)
                Page.SetSelectedGridText(GridViewCaller, GridViewCallerColEmail, State.callersDataTable.Rows(GridViewCaller.SelectedIndex).Item(CallerTableColEmail).ToString)

                ItemSelected = True
                RaiseEvent GridSelectionHandler(Me)
                'RaiseEvent SelectedIndexChanged(Me)

            End If
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub FillRelationshipDropDownList(dtRow As GridViewRow)
        Dim ddlRel As DropDownList = DirectCast(dtRow.FindControl(DropDownlistRelationship), DropDownList)

        If ddlRel IsNot Nothing Then
            Dim relList As ListItem() = (From llItem As DataRow In LookupListNew.GetRelationshipList(Authentication.CurrentUser.LanguageId).ToTable().AsEnumerable()
                    Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).Distinct().ToArray()

            ElitaPlusPage.BindListControlToArray(ddlRel, relList, False)
            ElitaPlusPage.BindSelectItemByText(State.callersDataTable.Rows(GridViewCaller.SelectedIndex).Item(4).ToString, ddlRel)
        End If
    End Sub
#End Region
End Class
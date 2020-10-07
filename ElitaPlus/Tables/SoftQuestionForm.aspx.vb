'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/16/2004)  ********************


Partial Class SoftQuestionForm
    Inherits ElitaPlusPage



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            txtSoftQuestion.Text = CType(Session("SoftQuestion"), String)
            Dim strLevel As String = CType(Session("Level"), String)
            If strLevel = "0" Then
                LoadSoftQuestionGroup(True)
            Else
                LoadSoftQuestionGroup(False)
            End If
        End If
        EnableDisableValues()
    End Sub

    Private Sub LoadSoftQuestionGroup(bLoadAvailGroups As Boolean)
        Dim i As Integer
        Dim softQuestionData As DataView
        If bLoadAvailGroups Then
            softQuestionData = SoftQuestion.getAvailableSoftQuestionGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            softQuestionData = SoftQuestionGroup.getList("", ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        End If

        cboSoftQuestionGroup.Items.Clear()
        For i = 0 To softQuestionData.Count - 1
            Dim tempGUID As Guid = New Guid(CType(softQuestionData(i)(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_GROUP_ID), Byte()))
            cboSoftQuestionGroup.Items.Add(New ListItem(softQuestionData(i)(SoftQuestion.SoftQuestionDV.COL_NAME_DESCRIPTION).ToString, tempGUID.ToString()))
        Next
        Dim softQuestionGrpID As String = ""
        If Session("SoftQuestionGroupID") IsNot Nothing Then
            softQuestionGrpID = CType(Session("SoftQuestionGroupID"), String)
            Dim selItem As ListItem = cboSoftQuestionGroup.Items.FindByValue(softQuestionGrpID)
            cboSoftQuestionGroup.SelectedIndex = cboSoftQuestionGroup.Items.IndexOf(selItem)
        Else
            cboSoftQuestionGroup.SelectedIndex = 0
        End If

    End Sub

    Private Sub EnableDisableValues()
        Dim strLevel As String = ""
        If Session("Level") IsNot Nothing Then
            strLevel = CType(Session("Level"), String)
        End If

        cboSoftQuestionGroup.Enabled = (strLevel = "0")
        If (cboSoftQuestionGroup.Enabled) Then
            cboSoftQuestionGroup.BackColor = Color.White
        Else
            cboSoftQuestionGroup.BackColor = Color.WhiteSmoke
        End If

        txtSoftQuestion.Enabled = (strLevel <> "0")
        If (txtSoftQuestion.Enabled) Then
            txtSoftQuestion.BackColor = Color.White
        Else
            txtSoftQuestion.BackColor = Color.WhiteSmoke
        End If

        RequiredFieldValidator1.Enabled = (strLevel <> "0")
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, (cboSoftQuestionGroup.SelectedValue.Trim().Length > 0))
    End Sub

#End Region

#Region "Button Clicks"


    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim strSoftQuestion As String = txtSoftQuestion.Text
            'Dim softQuestionGroupID As String = CType(Session("SoftQuestionGroupID"), String)
            Dim softQuestionGroupID As String = cboSoftQuestionGroup.SelectedValue
            Dim softQuestionID As String = CType(Session("SoftQuestionID"), String)
            Dim parentID As String = CType(Session("ParentID"), String)
            Dim childOrder As Integer = CType(Session("ChildOrder"), Integer)

            Dim strAction As String = CType(Session("SoftQuestionAction"), String)
            Dim strLevel As String = CType(Session("Level"), String)
            If strAction = "Add" Then

                Dim softQuestionBO As SoftQuestion = New SoftQuestion
                With softQuestionBO
                    .Description = strSoftQuestion
                    If strLevel = "0" Then
                        .ParentId = Nothing
                    Else
                        .ParentId = New Guid(parentID)
                    End If
                    .SoftQuestionGroupId = New Guid(softQuestionGroupID)
                    .ChildOrder = New LongType(childOrder)

                    .Save()
                End With

            Else
                Dim softQuestionBO As SoftQuestion = New SoftQuestion(New Guid(softQuestionID))
                With softQuestionBO
                    .Description = strSoftQuestion
                    '.ParentId = New Guid(softQuestionGroupID)
                    '.ChildOrder = New LongType(childOrder)
                    If softQuestionBO.IsDirty Then
                        .Save()
                    End If
                End With

            End If
        Catch ex As Exception
            Throw ex
        End Try

        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.SaveSoftQuestion();" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("SaveSoftQuestion", sJavaScript)

    End Sub


#End Region

End Class




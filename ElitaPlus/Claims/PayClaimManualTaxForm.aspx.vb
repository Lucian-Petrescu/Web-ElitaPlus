Public Class PayClaimManualTaxForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "PayClaimManualTaxForm.aspx"
    Public Const PAGETITLE As String = "TAXES"
    Public Const PAGETAB As String = "CLAIM"

    Private Const GRID_COL_DESCRIPTION As Integer = 0
    Private Const GRID_COL_AMOUNT As Integer = 1

    Private Const GRID_CTRL_NAME_DESCRIPTION As String = "lblDesc"
    Private Const GRID_CTRL_NAME_AMOUNT As String = "txtAmount"

#End Region

#Region "Page Calling Parameters and Return Type"
    Public Class ManualTaxDetail
        Private _Description As String
        Private _Position As Integer
        Private _Amount As Decimal
        Public Sub New(ByVal strDesc As String, ByVal intPos As Integer, ByVal dblAmount As Decimal)
            _Description = strDesc
            _Position = intPos
            _Amount = dblAmount
        End Sub
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal Value As String)
                _Description = Value
            End Set
        End Property

        Public Property Amount() As Decimal
            Get
                Return _Amount
            End Get
            Set(ByVal Value As Decimal)
                _Amount = Value
            End Set
        End Property

        Public Property Position() As Integer
            Get
                Return _Position
            End Get
            Set(ByVal Value As Integer)
                If Value > 0 And Value < 7 Then _Position = Value
            End Set
        End Property
    End Class

    Public Class Parameters
        Public ManualTaxList As Collections.Generic.List(Of ManualTaxDetail)
    End Class

#End Region

#Region "Page State"
    Class MyState        
        Public ManualTaxDetials As Collections.Generic.List(Of ManualTaxDetail)
        Public TaxAmtByDesc As Collections.Generic.Dictionary(Of String, Decimal)
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Me.Form.DefaultButton = btnSave_WRITE.UniqueID
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                populateGrid()
                TranslateGridHeader(Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                State.ManualTaxDetials = CType(Me.CallingParameters, Parameters).ManualTaxList
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub populateGrid()
        Grid.DataSource = State.ManualTaxDetials
        Grid.DataBind()
    End Sub

    Private Sub DoSave(Optional ByVal blnComingFromBack As Boolean = False)
        Dim ErrMsg As New Collections.Generic.List(Of String)
        If PopulateBOFromForm(ErrMsg) Then
            For Each mtd As ManualTaxDetail In State.ManualTaxDetials
                mtd.Amount = State.TaxAmtByDesc(mtd.Description)
            Next
        Else
            Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
        End If
    End Sub

    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim strDesc As String, decAmt As Decimal, strTemp As String
        State.TaxAmtByDesc = New Collections.Generic.Dictionary(Of String, Decimal)

        For Each gvr As GridViewRow In Grid.Rows
            strDesc = String.Empty
            decAmt = 0D
            strDesc = CType(gvr.Cells(GRID_COL_DESCRIPTION).FindControl(GRID_CTRL_NAME_DESCRIPTION), Label).Text
            strTemp = CType(gvr.Cells(GRID_COL_AMOUNT).FindControl(GRID_CTRL_NAME_AMOUNT), TextBox).Text.Trim

            If Decimal.TryParse(strTemp, decAmt) Then
                State.TaxAmtByDesc(strDesc) = Decimal.Parse(Decimal.Round(decAmt, 2).ToString("N2"))            
            Else
                blnSuccess = False
                errMsg.Add(strDesc & " " & TranslationBase.TranslateLabelOrMessage("AMOUNT") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR))
            End If
        Next
        Return blnSuccess
    End Function
#End Region

#Region "Button click handlers"
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Dim parm As New Parameters
            parm.ManualTaxList = State.ManualTaxDetials
            ReturnToCallingPage(parm)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCancel_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel_WRITE.Click
        Try
            populateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.DoSave()
            populateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

    
    
End Class
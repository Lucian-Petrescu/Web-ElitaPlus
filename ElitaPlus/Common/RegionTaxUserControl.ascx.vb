Imports System.Globalization
Partial Class RegionTaxUserControl
    Inherits System.Web.UI.UserControl
#Region "Member Variables"

    Private mDescription As String
    Private mPercent As String
    Private mNonTaxable As String
    Private mMiniMumTax As String
    Private mGLAccount As String


#End Region

#Region " Constants "

    Private Const Decimal_Format As String = "N"

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        End Sub

    Public Sub LoadText(oRegiontaxdetail As RegionTaxDetail, Optional ByVal enableEditing As Boolean = True)
        Dim ZeroDecimal As Decimal = 0
        With oRegiontaxdetail
            txtTaxDescription.Text = .Description()
            If .Percent Is Nothing Then
                txtTaxPercent.Text = ZeroDecimal.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            Else
                txtTaxPercent.Text = .Percent.Value.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            End If

            If .NonTaxable Is Nothing Then
                txtNonTaxable.Text = ZeroDecimal.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            Else
                txtNonTaxable.Text = .NonTaxable.Value.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            End If

            If .MinimumTax Is Nothing Then
                txtMinimumTax.Text = ZeroDecimal.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            Else
                txtMinimumTax.Text = .MinimumTax.Value.ToString(Decimal_Format, CultureInfo.CurrentCulture)
            End If

            If .GlAccountNumber Is Nothing Then
                txtGLAccount.Text = ""
            Else
                txtGLAccount.Text = .GlAccountNumber
            End If
        End With
        
        If enableEditing Then
            txtTaxPercent.Enabled = True
            txtNonTaxable.Enabled = True
            txtMinimumTax.Enabled = True
            txtGLAccount.Enabled = True
            'reset the label status
            Dim dValue As Decimal
            If Decimal.TryParse(txtTaxPercent.Text, dValue) Then lblPercent.ForeColor = Color.Black
            If Decimal.TryParse(txtNonTaxable.Text, dValue) Then lblNontaxable.ForeColor = Color.Black
            If Decimal.TryParse(txtMinimumTax.Text, dValue) Then lblMinTax.ForeColor = Color.Black
        End If
    End Sub

    Public Function VerifyInput(ByRef errmsg() As String) As Boolean

        Dim blnSuccess As Boolean, blnHasErr As Boolean = False
        Dim dTest As Decimal
        Dim parentPage As RegionTaxes = CType(Page, RegionTaxes)

        blnSuccess = Decimal.TryParse(txtTaxPercent.Text, dTest)
        If blnSuccess Then
            lblPercent.ForeColor = Color.Black
        Else
            blnHasErr = True
            Array.Resize(errmsg, errmsg.Length + 1)
            errmsg(errmsg.Length - 1) = "PERCENT_MUST_BE_NUMERIC"
            parentPage.SetLabelError(lblPercent)
        End If

        blnSuccess = Decimal.TryParse(txtNonTaxable.Text, dTest)
        If blnSuccess Then
            lblNontaxable.ForeColor = Color.Black
        Else
            blnHasErr = True
            Array.Resize(errmsg, errmsg.Length + 1)
            errmsg(errmsg.Length - 1) = "GUI_INVALID_NUMBER"
            parentPage.SetLabelError(lblNontaxable)
        End If

        blnSuccess = Decimal.TryParse(txtMinimumTax.Text, dTest)
        If blnSuccess Then
            lblMinTax.ForeColor = Color.Black
        Else
            blnHasErr = True
            Array.Resize(errmsg, errmsg.Length + 1)
            errmsg(errmsg.Length - 1) = "GUI_INVALID_NUMBER"
            parentPage.SetLabelError(lblMinTax)
        End If

        Return (Not blnHasErr)
    End Function

#Region "PROPERTIES"

    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(Value As String)
            mDescription = Value
        End Set
    End Property

    Public Property Percent() As String
        Get
            Return txtTaxPercent.Text.Trim()
        End Get
        Set(Value As String)
            txtTaxPercent.Text = Value
        End Set
    End Property

    Public Property Nontaxable() As String
        Get
            Return txtNonTaxable.Text.Trim()
        End Get
        Set(Value As String)
            txtNonTaxable.Text = Value
        End Set
    End Property

    Public Property MinimumTax() As String
        Get
            Return txtMinimumTax.Text.Trim()
        End Get
        Set(Value As String)
            txtMinimumTax.Text = Value
        End Set
    End Property

    Public Property GLAccount() As String
        Get
            Return txtGLAccount.Text.Trim()
        End Get
        Set(Value As String)
            txtGLAccount.Text = Value
        End Set
    End Property

    Public ReadOnly Property IsEnabledForEditing() As Boolean
        Get
            Return txtTaxPercent.Enabled
        End Get
    End Property
#End Region
End Class

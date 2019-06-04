Public Class UserControlCaseHeader
    Inherits Web.UI.UserControl
#Region "Constants"
    Public Const NoData As String = " - "

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub
    Private Sub ResetAllValue()
        LabelCustomerNameValue.Text = NoData
        LabelCallerNameValue.Text = NoData
        LabelCompanyValue.Text = NoData
        LabelDealerNameValue.Text = NoData
        LabelCaseNumberValue.Text = NoData
        LabelCaseOpenDateValue.Text = NoData
        LabelCasePurposeValue.Text = NoData
        LabelCaseStatusValue.Text = NoData
        LabelCaseCloseDateValue.Text = NoData
        LabelCaseCloseValue.Text = NoData
        LabelCertificateNumberValue.Text = NoData
        LabelClaimNumberValue.Text = NoData
    End Sub
    Public Property CustomerName() As String
        Get
            Return LabelCustomerNameValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCustomerNameValue.Text = value
            Else
                LabelCustomerNameValue.Text = NoData
            End If
        End Set
    End Property
    Public Property CallerName() As String
        Get
            Return LabelCallerNameValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCallerNameValue.Text = value
            Else
                LabelCallerNameValue.Text = NoData
            End If
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return LabelCompanyValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCompanyValue.Text = value
            Else
                LabelCompanyValue.Text = NoData
            End If

        End Set
    End Property
    Public Property DealerName() As String
        Get
            Return LabelDealerNameValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelDealerNameValue.Text = value
            Else
                LabelDealerNameValue.Text = NoData
            End If

        End Set
    End Property
    Public Property CaseNumber() As String
        Get
            Return LabelCaseNumberValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCaseNumberValue.Text = value
            Else
                LabelCaseNumberValue.Text = NoData
            End If
        End Set
    End Property
    Public Property CaseOpenDate() As String
        Get
            Return LabelCaseOpenDateValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCaseOpenDateValue.Text = value
            Else
                LabelCaseOpenDateValue.Text = NoData
            End If
        End Set
    End Property
    Public Property CasePurpose() As String
        Get
            Return LabelCasePurposeValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCasePurposeValue.Text = value
            Else
                LabelCasePurposeValue.Text = NoData
            End If
        End Set
    End Property

    Public Property CaseStatus() As String
        Get
            Return LabelCaseStatusValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCaseStatusValue.Text = value
            Else
                LabelCaseStatusValue.Text = NoData
            End If
        End Set
    End Property
    Public WriteOnly Property SetCaseStatusCssClass() As String
        Set(ByVal value As String)
            If value <> String.Empty Then
                CaseStatusTD.Attributes.Item("Class") = value
            End If
        End Set
    End Property
    Public Property CaseCloseDate() As String
        Get
            Return LabelCaseCloseDateValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCaseCloseDateValue.Text = value
            Else
                LabelCaseCloseDateValue.Text = NoData
            End If
        End Set
    End Property
    Public Property CaseCloseReason() As String
        Get
            Return LabelCaseCloseValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCaseCloseValue.Text = value
            Else
                LabelCaseCloseValue.Text = NoData
            End If
        End Set
    End Property

    Public Property CertificateNumber() As String
        Get
            Return LabelCertificateNumberValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelCertificateNumberValue.Text = value
            Else
                LabelCertificateNumberValue.Text = NoData
            End If
        End Set
    End Property
    Public Property ClaimNumber() As String
        Get
            Return LabelClaimNumberValue.Text.ToString
        End Get
        Set(ByVal value As String)
            If value <> String.Empty Then
                LabelClaimNumberValue.Text = value
            Else
                LabelClaimNumberValue.Text = NoData
            End If
        End Set
    End Property
End Class
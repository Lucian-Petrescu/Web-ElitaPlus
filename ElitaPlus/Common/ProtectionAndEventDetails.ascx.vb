'------------------------------------------------------------------------------------------------------
'Purpose: 	Header Information in Wizard
'Author:  	Prayag Ganoje
'Date:    	06/19/2012
'Modification History: REQ-860
'------------------------------------------------------------------------------------------------------


Public Class ProtectionAndEventDetails
    Inherits UserControl

    Private NO_DATA As String = " - "

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

#End Region

#Region "Properties"

    Public Property CustomerName() As String
        Get
            Return CustomerNameText.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                CustomerNameText.Text = value
            Else
                CustomerNameText.Text = NO_DATA
            End If
        End Set
    End Property

    Public Property EnrolledMake() As String
        Get
            Return EnrolledMakeText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                EnrolledMakeText.Text = value
            Else
                EnrolledMakeText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property ClaimNumber() As String
        Get
            Return ClaimNumberText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                ClaimNumberText.Text = value
            Else
                ClaimNumberText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property DealerName() As String
        Get
            Return DealerNameText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                DealerNameText.Text = value
            Else
                DealerNameText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property EnrolledModel() As String
        Get
            Return EnrolledModeText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                EnrolledModeText.Text = value
            Else
                EnrolledModeText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property ClaimStatus() As String
        Get
            Return ClaimStatusText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                ClaimStatusText.Text = value
            Else
                ClaimStatusText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property CallerName() As String
        Get
            Return CallerNameText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                CallerNameText.Text = value
            Else
                CallerNameText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property ClaimedMake() As String
        Get
            Return ClaimedMakeText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                ClaimedMakeText.Text = value
            Else
                ClaimedMakeText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property DateOfLoss() As String
        Get
            Return DateOfLossText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                DateOfLossText.Text = value
            Else
                DateOfLossText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property ProtectionStatus() As String
        Get
            Return ProtectionStatusText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                ProtectionStatusText.Text = value
            Else
                ProtectionStatusText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property ClaimedModel() As String
        Get
            Return ClaimedModelText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                ClaimedModelText.Text = value
            Else
                ClaimedModelText.Text = NO_DATA
            End If

        End Set
    End Property

    Public Property TypeOfLoss() As String
        Get
            Return TypeOfLossText.Text.ToString
        End Get
        Set(value As String)
            If value <> String.Empty Then
                TypeOfLossText.Text = value
            Else
                TypeOfLossText.Text = NO_DATA
            End If

        End Set
    End Property
    Public Property ShowCustomerAddress() As Boolean
        Get
            Return custAddress.Visible
        End Get
        Set(value As Boolean)
            custAddress.Visible = value
        End Set
    End Property
    Public Property CustomerAddress() As String
        Get
            If ShowCustomerAddress Then
                Return CustomerAddressText.Text.ToString
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            If ShowCustomerAddress Then
                If value <> String.Empty Then
                    CustomerAddressText.Text = value
                Else
                    CustomerAddressText.Text = NO_DATA
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property ProtectionStatusCss() As String
        Set(value As String)
            Dim initialClass As String = "bor padRight"
            ProtectionStatusTD.Attributes.Item("Class") = initialClass & " " & value
        End Set
    End Property

    Public WriteOnly Property ClaimStatusCss() As String
        Set(value As String)
            Dim initialClass As String = "padRight"
            ClaimStatusTD.Attributes.Item("Class") = initialClass & " " & value
        End Set
    End Property

#End Region


End Class
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization

Namespace Claims

    Public Class LetterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = DeniedClaimsDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_DENIED_REASON1_ID As String = DeniedClaimsDAL.COL_NAME_DENIED_REASON1_ID
#End Region

#Region "Constructors"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
#End Region

    End Class

    Partial Class ClaimDeniedInformationForm
        Inherits ElitaPlusPage

        Public Const PAGETITLE As String = "INFORMATION_VERIFICATION"
        Public Const PAGETAB As String = "CLAIMS"

#Region "Page State"


        Protected Shadows ReadOnly Property State() As MyState
            Get
                'Return CType(MyBase.State, MyState)
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                Else
                    If Me.NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(Me.NavController.State, MyState)
                        'Me.StartNavControl()
                        Me.NavController.State = s
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property


#Region "Internal State Management"

#End Region
#Region "Parameters"

        Public Class Parameters
            Public moClaimbo As ClaimBase
            Public moClaimId As Guid
            Public moCertId As Guid


            Public Sub New(ByVal oClaimbo As ClaimBase, ByVal oClaimId As Guid, ByVal oCertId As Guid)
                moClaimbo = oClaimbo
                moClaimId = oClaimId
                moCertId = oCertId
            End Sub

        End Class

#End Region

#Region "MyState"

        Class MyState
            Public moParams As Parameters
            Public searchDV As Claims.LetterSearchDV


            Public Sub New()

            End Sub
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub



        Private Sub SetStateProperties()
            Try
                'Me.State.moParams = CType(Me.CallingParameters, Parameters)
                Me.State.moParams = CType(Me.NavController.ParametersPassed, Parameters)
                If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.moClaimId.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
                PopulateFormFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Constants"
#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Claim
            Public BoChanged As Boolean = False
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Claim, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.BoChanged = boChanged
            End Sub
            Public Sub New(ByVal LastOp As DetailPageCommand)
                Me.LastOperation = LastOp
            End Sub
        End Class
#End Region

        Public Const URL As String = "ClaimDeniedInformationForm.aspx"
        Protected WithEvents ErrorCtrl As ErrorController



#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents moErrorController As ErrorController
        ''NOTE: The following placeholder declaration is required by the Web Form Designer.
        ''Do not delete or move it.
        'Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Me.SetStateProperties()
            Try
                Me.ErrControllerMaster.Clear_Hide()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.ShowMissingTranslations(Me.ErrControllerMaster)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            ' Claim Detail
            Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack_WRITE.Click
            Try
                GoBack()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Public Shared Function getLetterList(ByVal claimLetterId As Guid) As LetterSearchDV

            Try
                Dim dal As New DeniedClaimsDAL
                Return New LetterSearchDV(dal.LoadLetterList(claimLetterId, Authentication.LangId).Tables(0))
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

        End Function
        Private Sub btnNext_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext_WRITE.Click

            Me.State.searchDV = getLetterList(Me.State.moParams.moClaimId)
            Try
                With Me.State.moParams
                    If Me.State.searchDV.Count = 0 Then
                        Me.NavController.Navigate(Me, "denied_claims_next", New Claims.DeniedClaimsForm.Parameters(.moClaimbo, .moClaimId, moClaimNumberText.Text, moCertificateText.Text, .moClaimbo.Certificate.Id, True, Me.State.searchDV))
                    Else
                        Me.NavController.Navigate(Me, "claims_denied_letter_list_next", New ClaimDeniedLetterListForm.Parameters(.moClaimbo, Me.State.searchDV, .moClaimId, moClaimNumberText.Text, moCertificateText.Text, .moClaimbo.Certificate.Id))
                    End If
                End With

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try


        End Sub
#End Region

#End Region

#Region "Populate"

        Private Sub PopulateClaimPart()

            With Me.State.moParams.moClaimbo
                moClaimNumberText.Text = .ClaimNumber
                EnableDisableControls(moClaimNumberText, True)
                moDealerText.Text = .Dealer.DealerName
                EnableDisableControls(moDealerText, True)
                moCertificateText.Text = .Certificate.CertNumber
                EnableDisableControls(moCertificateText, True)
                moMakeText.Text = Manufacturer.GetDescription(.CertificateItem.ManufacturerId)

                EnableDisableControls(moMakeText, True)
                moCustomerNameText.Text = .Certificate.CustomerName
                EnableDisableControls(moCustomerNameText, True)

                EnableDisableControls(moAddressText, True)
                EnableDisableControls(moAddressText2, True)
                EnableDisableControls(moCityText, True)
                EnableDisableControls(moStateText, True)
                EnableDisableControls(moZipText, True)

                moAddressText.Text = String.Empty
                moAddressText2.Text = String.Empty
                moCityText.Text = String.Empty
                moStateText.Text = String.Empty
                moZipText.Text = String.Empty

                If (.Certificate.AddressId <> Guid.Empty) Then
                    Dim oAddress As New Address(.Certificate.AddressId)

                    moAddressText.Text = oAddress.Address1
                    moAddressText2.Text = oAddress.Address2
                    moCityText.Text = oAddress.City

                    moZipText.Text = oAddress.PostalCode

                    If (oAddress.RegionId <> Guid.Empty) Then
                        Dim oRegion As New BusinessObjectsNew.Region(oAddress.RegionId)
                        moStateText.Text = oRegion.Description
                    End If
                End If

                moModelText.Text = .CertificateItem.Model
                EnableDisableControls(moModelText, True)
                moProductCodeText.Text = .Certificate.ProductCode
                EnableDisableControls(moProductCodeText, True)

            End With

        End Sub




        Private Sub PopulateFormFromBo()
            Try
                PopulateClaimPart()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private Sub moClaimNumberText_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles moClaimNumberText.Init

        End Sub
    End Class

End Namespace

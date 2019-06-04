Imports System.Xml
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Public Class WSUtility
    Inherits BusinessObjectBase

#Region "Public Variables & Constants"

    Public Const WS_CONSUMER_CLIENT As String = "CLIENT"
    Public Const WS_CONSUMER_SERVER As String = "SERVER"

#End Region

#Region "Private Variables & Constants"

    Private sErrorMsg As String = ""
    Private bHasError As Boolean

    Private Const END_OF_LINE As String = "^"
    Private Const END_OF_FIELD As String = "|"

#End Region

    Public Sub New()
        MyBase.New()

    End Sub

    Public Sub New(ByVal DS As DataSet)
        MyBase.New()
        Me.Dataset = DS

    End Sub

#Region "Properties"
    Public ReadOnly Property HasError() As Boolean
        Get
            Return bHasError
        End Get
    End Property

    Public ReadOnly Property ErrorMsg() As String
        Get
            Return sErrorMsg
        End Get
    End Property

#End Region

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Public Shared Function CompactData(ByVal dw As DataView) As String

        Try

            Dim result As String = String.Empty

            Dim i As Integer
            Dim row As DataRowView
            Dim colNum As Integer = dw.Table.Columns.Count

            Dim IEnum As IEnumerator = dw.GetEnumerator
            While IEnum.MoveNext

                row = CType(IEnum.Current, DataRowView)
                For i = 0 To colNum - 1
                    result &= Convert.ToString(row(i))
                    If (i < colNum - 1) Then result &= END_OF_FIELD
                Next

                result &= END_OF_LINE

            End While

            Return result

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try

    End Function

    Public Shared Function GetManufacturerGUID(ByVal sManufacturer As String, ByVal companyGroupID As Guid) As System.Guid
        Try

            Dim oManu As New Manufacturer(sManufacturer.ToUpper, companyGroupID)
            Dim sManuID As System.Guid = oManu.Id

            Return sManuID

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function GetDealerCode(ByVal DealerID As Guid) As String
        Try

            Dim oDealer As New Dealer(DealerID)
            Dim sDealerCode As String = oDealer.Dealer
            oDealer = Nothing

            Return sDealerCode

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function GetDealerCode(ByVal sDealerName As String, ByVal companyGroupID As Guid) As String
        Try

            Dim oDealer As New Dealer(companyGroupID, sDealerName.ToUpper)
            Dim sDealerCode As String = oDealer.Dealer
            oDealer = Nothing

            Return sDealerCode

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function GetDealerID(ByVal companyGroupID As Guid, ByVal sDealer As String) As System.Guid
        Try

            Dim oDealer As New Dealer(companyGroupID, sDealer.ToUpper)
            Dim sDealerID As System.Guid = oDealer.Id
            oDealer = Nothing

            Return sDealerID

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function IsGuid(ByRef Value As Object) As Boolean
        Try
            Dim tmp As New Guid(Value.ToString) ' If bad Guid then this will raise an error
            IsGuid = True ' Value is a Guid
        Catch ex As Exception
            IsGuid = False ' Error raised, not a Guid
        End Try
    End Function

    Public Shared Function FormatErrorStringArray(ByVal Err() As String) As String
        Dim FormattedError As String = ""
        Dim i As Integer
        For i = 0 To Err.Length - 1
            FormattedError = Err(i) & " :: "
        Next

        Return FormattedError

    End Function

    Public Shared Function FormatAndTranslateErrorsFromBOValidationExc(ByVal validationExc As BOValidationException, Optional ByVal Translate As Boolean = True) As String
        Dim err As Assurant.Common.Validation.ValidationError
        Dim errStrList As String
        For Each err In validationExc.ValidationErrorList
            If Not errStrList Is Nothing Then errStrList &= " :: "
            If Translate Then
                errStrList &= TranslationBase.TranslateLabelOrMessage(err.Message)
            Else
                errStrList &= err.Message
            End If
        Next

        Return errStrList

    End Function

End Class

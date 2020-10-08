#Region "PostalCodeFormatStructure"

Public Structure PostalCodeFormatResult
    Private _valid As Boolean
    Private _pCode As String
    Private _locStart As Integer
    Private _locLength As Integer
    Private _reformattedPCode As String
    Private _errMsg As String
    Private _comuna As String


    Public Property LocatorStart As Integer
        Get
            Return _locStart
        End Get
        Set
            _locStart = Value
        End Set
    End Property

    Public Property LocatorLength As Integer
        Get
            Return _locLength
        End Get
        Set
            _locLength = Value
        End Set
    End Property
    Public Property ComunaEnabled As String
        Get
            Return _comuna
        End Get
        Set
            _comuna = Value
        End Set
    End Property

    Public Property PostalCode As String
        Get
            Return _pCode
        End Get
        Set
            _pCode = Value
        End Set
    End Property

    Public Property ReformattedPostalCode As String
        Get
            Return _reformattedPCode
        End Get
        Set
            _reformattedPCode = Value
        End Set
    End Property

    Public Property IsValid As Boolean
        Get
            Return _valid
        End Get
        Set
            _valid = Value
        End Set
    End Property

    Public Property ErrorMessage As String
        Get
            Return _errMsg
        End Get
        Set
            _errMsg = Value
        End Set
    End Property

End Structure

#End Region

Public Class PostalCodeValidator

    Private couID As Guid
    Private postCode As String
    Private reformatInp As Boolean

    Public Sub New(countryID As Guid, postalCode As String, Optional ByVal reformatInput As Boolean = False)
        couID = countryID
        postCode = postalCode
        reformatInp = reformatInput
    End Sub

#Region "Properties"

    Public ReadOnly Property CountryID As Guid
        Get
            Return couID
        End Get
    End Property

    Public ReadOnly Property PostalCode As String
        Get
            Return postCode
        End Get
    End Property

    Public ReadOnly Property ReformatInput As Boolean
        Get
            Return reformatInp
        End Get
    End Property
    Public Function IsValid() As PostalCodeFormatResult

        Return CountryPostalCodeFormat.IsValidFormat(CountryID, PostalCode, ReformatInput)

    End Function

#End Region

End Class

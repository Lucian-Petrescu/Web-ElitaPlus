Public Class ProductDetail
    Public Property CategoryNamePath() As String
        Get
            Return m_CategoryNamePath
        End Get
        Set
            m_CategoryNamePath = Value
        End Set
    End Property
    Private m_CategoryNamePath As String
    Public Property CategoryId() As String
        Get
            Return m_CategoryId
        End Get
        Set
            m_CategoryId = Value
        End Set
    End Property
    Private m_CategoryId As String

    Public Property Mpid() As String
        Get
            Return m_Mpid
        End Get
        Set
            m_Mpid = Value
        End Set
    End Property
    Private m_Mpid As String

    Public Property CategoryName() As String
        Get
            Return m_CategoryName
        End Get
        Set
            m_CategoryName = Value
        End Set
    End Property
    Private m_CategoryName As String

    Public Upcs As List(Of String)

    Public Property BrandName() As String
        Get
            Return m_BrandName
        End Get
        Set
            m_BrandName = Value
        End Set
    End Property
    Private m_BrandName As String

    Public Property MinSalePrice() As String
        Get
            Return m_MinSalePrice
        End Get
        Set
            m_MinSalePrice = Value
        End Set
    End Property
    Private m_MinSalePrice As String

    Public Property BrandId() As String
        Get
            Return m_BrandId
        End Get
        Set
            m_BrandId = Value
        End Set
    End Property
    Private m_BrandId As String

    Public Property CategoryIdPath() As String
        Get
            Return m_CategoryIdPath
        End Get
        Set
            m_CategoryIdPath = Value
        End Set
    End Property
    Private m_CategoryIdPath As String

    Public Mpns As List(Of String)

    Public Property CountryCode() As String
        Get
            Return m_CountryCode
        End Get
        Set
            m_CountryCode = Value
        End Set
    End Property
    Private m_CountryCode As String

    Public Property Currency() As String
        Get
            Return m_Currency
        End Get
        Set
            m_Currency = Value
        End Set
    End Property
    Private m_Currency As String

    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set
            m_Title = Value
        End Set
    End Property
    Private m_Title As String

    Public Property LastRecordedAt() As String
        Get
            Return m_LastRecordedAt
        End Get
        Set
            m_LastRecordedAt = Value
        End Set
    End Property
    Private m_LastRecordedAt As String

    Public Property ImageUrl() As String
        Get
            Return m_ImageUrl
        End Get
        Set
            m_ImageUrl = Value
        End Set
    End Property
    Private m_ImageUrl As String

    Public Property MaxSalePrice() As String
        Get
            Return m_MaxSalePrice
        End Get
        Set
            m_MaxSalePrice = Value
        End Set
    End Property
    Private m_MaxSalePrice As String

    Public Property OffersCount() As String
        Get
            Return m_OffersCount
        End Get
        Set
            m_OffersCount = Value
        End Set
    End Property
    Private m_OffersCount As String

    Public Property StoresCount() As String
        Get
            Return m_StoresCount
        End Get
        Set
            m_StoresCount = Value
        End Set
    End Property
    Private m_StoresCount As String

End Class

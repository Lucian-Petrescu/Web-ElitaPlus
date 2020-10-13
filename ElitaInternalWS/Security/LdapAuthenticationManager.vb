Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Globalization
Imports System.Text
Imports System.Collections.Generic
Imports System.Security.Cryptography

Namespace Security

    Friend NotInheritable Class LdapAuthenticationManager

#Region "Singleton Implementation"

        Private Shared _instance As LdapAuthenticationManager
        Private Shared _syncRoot As New Object

        Friend Shared ReadOnly Property Current As LdapAuthenticationManager
            Get
                If (_instance Is Nothing) Then
                    SyncLock (_syncRoot)
                        If (_instance Is Nothing) Then
                            _instance = New LdapAuthenticationManager()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property

#End Region

#Region "Fields"
        Private _cacheStore As Cache(Of String, CacheData)
#End Region

#Region "Constructor"

        Private Sub New()
            _cacheStore = New Cache(Of String, CacheData)(New TimeSpan(0, 0, 5, 0, 0)) '' 5 Mins Cache
        End Sub

#End Region

#Region "Methods"

        Friend Function GetPrincipal(userName As String) As ElitaPlusPrincipal
            Try
                Dim cacheItem As CacheData
                cacheItem = _cacheStore.GetData(userName)

                ' Cache Not Found
                If (cacheItem Is Nothing) Then Return Nothing

                ElitaService.VerifyToken(True, cacheItem.Token)

                Return DirectCast(Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal)

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Validates LDAP User, Creates Token and Caches Token
        ''' </summary>
        ''' <param name="userName">If user is part of Services Group then only User Name otherwise in format {GroupName}\{UserName}</param>
        ''' <param name="password">LDAP Password for User</param>
        ''' <returns>True when Authentication is Successful, false otherwise</returns>
        ''' <remarks>The class uses combination of Cache and LDAP to Authentication</remarks>
        Friend Function ValidateLdapUser(userName As String, password As String) As Boolean
            Try
            '' Validate User Name against Cache
            Dim data As CacheData = _cacheStore.GetData(userName.ToUpperInvariant())
            If (data IsNot Nothing) Then
                If (data.ValidatePassword(password)) Then Return True
                _cacheStore.Remove(userName)
            End If

            Dim networkId As String
            Dim groupName As String

            If (userName.Contains("\")) Then
                networkId = userName.Split("\".ToCharArray())(1)
                groupName = userName.Split("\".ToCharArray())(0)
            Else
                networkId = userName
                groupName = "Services"
            End If

            Dim complexUserName As String = String.Format("{0}{2}{0}{2}{1}", networkId, groupName, AppConfig.FIELD_SEPARATOR)
            Dim token As String
            Try
                token = ElitaService.VerifyLogin(True, complexUserName, password)
            Catch ex As Exception
                Return False
            End Try

                _cacheStore.Add(networkId.ToUpperInvariant(), New CacheData(token, password))

            Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

#Region "Cache Data"
        Private Class CacheData
            Private _token As String
            Private _password As String

#Region "Password Hashing"

            ''' <summary>
            ''' Stores Salt to be used for Hashing
            ''' </summary>
            ''' <remarks></remarks>
            Private Shared ReadOnly salt As Byte()

            ''' <summary>
            ''' Hash Algorithm used for generating Hash value of Passwords
            ''' </summary>
            ''' <remarks></remarks>
            Private Shared ReadOnly hashAlgorithm As HashAlgorithm

            ''' <summary>
            ''' Size of Salt for Hashing Password in Bytes
            ''' </summary>
            ''' <remarks></remarks>
            Private Const saltValueSize As Integer = 16

            ''' <summary>
            ''' Generated Random Salt String
            ''' </summary>
            ''' <returns>Random Salt String</returns>
            Private Shared Function GenerateSaltValue() As Byte()
                ' Create a random number object seeded from the value
                ' of the last random seed value. This is done
                ' interlocked because it is a static value and we want
                ' it to roll forward safely.

                Dim random As New Random(CInt(DateTime.Now.Ticks And Integer.MaxValue))

                If random IsNot Nothing Then
                    ' Create an array of random values.

                    Dim saltValue As Byte() = New Byte(SaltValueSize - 1) {}

                    random.NextBytes(saltValue)

                    Return saltValue
                End If

                Return Nothing
            End Function

            ''' <summary>
            ''' Creates Hash Sting from input clear string
            ''' </summary>
            ''' <param name="inputString">String to be Hashed</param>
            ''' <returns>Hash of Input String</returns>
            Private Shared Function HashPassword(inputString As String) As String
                Dim encoding As New UnicodeEncoding()

                If inputString IsNot Nothing AndAlso hashAlgorithm IsNot Nothing AndAlso encoding IsNot Nothing Then

                    Dim valueToHash As Byte() = New Byte(SaltValueSize + (encoding.GetByteCount(inputString) - 1)) {}
                    Dim binaryPassword As Byte() = encoding.GetBytes(inputString)

                    ' Copy the salt value and the password to the hash buffer.

                    salt.CopyTo(valueToHash, 0)
                    binaryPassword.CopyTo(valueToHash, SaltValueSize)

                    Dim hashValue As Byte() = hashAlgorithm.ComputeHash(valueToHash)

                    ' The hashed password is the salt plus the hash value (as a string).

                    Dim hashedPassword As String = Nothing

                    For Each hexdigit As Byte In hashValue
                        hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat)
                    Next

                    ' Return the hashed password as a string.

                    Return hashedPassword
                End If

                Return Nothing
            End Function

            Shared Sub New()
                hashAlgorithm = New SHA1Managed()
                salt = GenerateSaltValue()
            End Sub

#End Region

            Friend Sub New(token As String, password As String)
                _token = token
                _password = HashPassword(password)
            End Sub

            Friend ReadOnly Property Token As String
                Get
                    Return _token
                End Get
            End Property

            Friend Function ValidatePassword(password As String) As Boolean
                Return (_password = HashPassword(password))
            End Function

        End Class
#End Region

#Region "Cache"
        Private Class Cache(Of TKey, TData)

            Private _store As Dictionary(Of TKey, CacheItem(Of TData))
            Private ReadOnly _duration As TimeSpan
            Private _syncRoot As Object

            Public Sub New(duration As TimeSpan)
                _duration = duration
                _store = New Dictionary(Of TKey, CacheItem(Of TData))
                _syncRoot = New Object
            End Sub

            Public Function GetData(key As TKey) As TData
                SyncLock (_syncRoot)
                    If (Not _store.ContainsKey(key)) Then Return Nothing
                    Dim item As CacheItem(Of TData) = _store(key)
                    If (item.CacheDateTime.Add(_duration) < DateTime.Now) Then
                        _store.Remove(key)
                        Return Nothing
                    End If
                    Return item.Data
                End SyncLock
            End Function

            Public Sub Remove(key As TKey)
                SyncLock (_syncRoot)
                    If (Not _store.ContainsKey(key)) Then Return
                    _store.Remove(key)
                End SyncLock
            End Sub

            Public Sub Add(key As TKey, data As TData)
                SyncLock (_syncRoot)
                    If (_store.ContainsKey(key)) Then
                        Dim item As CacheItem(Of TData) = _store(key)
                        _store.Remove(key)
                    End If
                    _store.Add(key, New CacheItem(Of TData)(data))
                End SyncLock
            End Sub

#Region "Cache Item"
            ''' <summary>
            ''' Represents Item in Cache
            ''' </summary>
            ''' <remarks></remarks>
            Private Class CacheItem(Of TType)

                Private ReadOnly _data As TType
                Private ReadOnly _cacheDateTime As DateTime

                Friend Sub New(data As TType)
                    _data = data
                    _cacheDateTime = DateTime.Now
                End Sub

                ''' <summary>
                ''' Gets Cache Item Data
                ''' </summary>
                ''' <value></value>
                ''' <returns></returns>
                ''' <remarks></remarks>
                Friend ReadOnly Property Data As TType
                    Get
                        Return _data
                    End Get
                End Property

                ''' <summary>
                ''' Gets Date and Time when Cache Item was created
                ''' </summary>
                ''' <value></value>
                ''' <returns></returns>
                ''' <remarks></remarks>
                Friend ReadOnly Property CacheDateTime As DateTime
                    Get
                        Return _cacheDateTime
                    End Get
                End Property

            End Class
#End Region

        End Class
#End Region




    End Class
End Namespace
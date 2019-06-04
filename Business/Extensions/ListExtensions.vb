Imports System.Runtime.CompilerServices
Imports System.Text
Imports Assurant.ElitaPlus.DataEntities

Public Module ListExtensions

    ''' <summary>
    ''' Gets Description (Translation based on Language Code passed as parameter) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <param name="pLanguageCode">Language Code to be used for Translation. Use Constrants <see cref="Assurant.ElitaPlus.DataEntities.LanguageCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager, pListCode or pLanguageCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                                 ByVal pValue As Nullable(Of Guid),
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String,
                                 ByVal pLanguageCode As String) As String
        If Not pValue.HasValue Then
            Return String.Empty
        Else
            Return pValue.Value.ToDescription(pCommonManager, pListCode, pLanguageCode)
        End If
    End Function

    ''' <summary>
    ''' Gets Description (Translation based on Language Code passed as parameter) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <param name="pLanguageCode">Language Code to be used for Translation. Use Constrants <see cref="Assurant.ElitaPlus.DataEntities.LanguageCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager, pListCode or pLanguageCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                                 ByVal pValue As Guid,
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String,
                                 ByVal pLanguageCode As String) As String

        If (pValue = Guid.Empty) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pLanguageCode)) Then
            Throw New ArgumentNullException("pLanguageCode")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode, pLanguageCode).Where(Function(ieli) ieli.ListItemId = pValue).FirstOrDefault()
        If (eli.Description Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Description.ToUpperInvariant()
    End Function

    ''' <summary>
    ''' Gets Description (Translation based on Language Code passed as parameter) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager, pListCode or pLanguageCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                                 ByVal pValue As Nullable(Of Guid),
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String) As String
        If Not pValue.HasValue Then
            Return String.Empty
        Else
            Return pValue.Value.ToDescription(pCommonManager, pListCode)
        End If
    End Function

    ''' <summary>
    ''' Gets Description (Translation based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                                 ByVal pValue As Guid,
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String) As String

        If (pValue = Guid.Empty) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode).Where(Function(ieli) ieli.ListItemId = pValue).FirstOrDefault()
        If (eli.Description Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Description.ToUpperInvariant()

    End Function


    ''' <summary>
    ''' Gets Description (Translation based on Language Code passed as parameter) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Code for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <param name="pLanguageCode">Language Code to be used for Translation. Use Constrants <see cref="Assurant.ElitaPlus.DataEntities.LanguageCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Code and Language</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager, pListCode  is Null or Blank String</exception>
    ''' <remarks>When pValue is Null or Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                             ByVal pValue As String,
                             ByVal pCommonManager As ICommonManager,
                             ByVal pListCode As String,
                             ByVal pLanguageCode As String) As String

        If (String.IsNullOrWhiteSpace(pValue)) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pLanguageCode)) Then
            Throw New ArgumentNullException("pLanguageCode")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode, pLanguageCode).Where(Function(ieli) ieli.Code = pValue).FirstOrDefault()
        If (eli.Description Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Description.ToUpperInvariant()
    End Function

    ''' <summary>
    ''' Gets Description (Translation based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Code for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Code and Langauge received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Null or Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToDescription(
                         ByVal pValue As String,
                         ByVal pCommonManager As ICommonManager,
                         ByVal pListCode As String) As String

        If (String.IsNullOrWhiteSpace(pValue)) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode).Where(Function(ieli) ieli.Code = pValue).FirstOrDefault()
        If (eli.Description Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Description.ToUpperInvariant()

    End Function

    ''' <summary>
    ''' Gets Code (Code based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToCode(ByVal pValue As Nullable(Of Guid),
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String,
                                 ByVal pLanguageCode As String) As String
        If (Not pValue.HasValue) Then
            Return String.Empty
        Else
            Return pValue.Value.ToCode(pCommonManager, pListCode, pLanguageCode)
        End If


    End Function
    ''' <summary>
    ''' Gets Code (Code based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>

    <Extension()>
    Public Function ToCode(
                                 ByVal pValue As Guid,
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String,
                                 ByVal pLanguageCode As String) As String

        If (pValue = Guid.Empty) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pLanguageCode)) Then
            Throw New ArgumentNullException("pLanguageCode")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode, pLanguageCode).Where(Function(ieli) ieli.ListItemId = pValue).FirstOrDefault()
        If (eli.Code Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Code.ToUpperInvariant()
    End Function

    ''' <summary>
    ''' Gets Code (Code based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToCode(ByVal pValue As Nullable(Of Guid),
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String) As String
        If (Not pValue.HasValue) Then
            Return String.Empty
        Else
            Return pValue.Value.ToCode(pCommonManager, pListCode)
        End If


    End Function

    ''' <summary>
    ''' Gets Code (Code based on Language Code received in Claims) from Elita Lists
    ''' </summary>
    ''' <param name="pValue">List Item Id for which Translation/Description is required</param>
    ''' <param name="pCommonManager">Instance of <see cref="ICommonManager"/> used to access Elita Lists</param>
    ''' <param name="pListCode">List Code to which List Item Code belongs. Use Contsants <see cref="Assurant.ElitaPlus.DataEntities.ListCodes"/></param>
    ''' <returns>Description/Translation based on List Code, List Item Id and Language received in Claims</returns>
    ''' <exception cref="ArgumentNullException">When pCommonManager or pListCode is Null or Blank String</exception>
    ''' <remarks>When pValue is Empty or Item is not Found returns Empty String</remarks>
    <Extension()>
    Public Function ToCode(
                                 ByVal pValue As Guid,
                                 ByVal pCommonManager As ICommonManager,
                                 ByVal pListCode As String) As String

        If (pValue = Guid.Empty) Then
            Return String.Empty
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Dim eli As ElitaListItem = pCommonManager.GetListItems(pListCode).Where(Function(ieli) ieli.ListItemId = pValue).FirstOrDefault()
        If (eli.Code Is Nothing) Then
            Return String.Empty
        End If

        Return eli.Code.ToUpperInvariant()

    End Function
    <Extension>
    Public Function ToGuid(ByVal pListItemCode As String,
                           ByVal pListCode As String,
                          ByVal pCommonManager As ICommonManager) As Guid

        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListItemCode")
        End If
        If (String.IsNullOrWhiteSpace(pListCode)) Then
            Throw New ArgumentNullException("pListCode")
        End If

        Return pCommonManager.GetListItems(pListCode).Where(Function(ieli) ieli.Code = pListItemCode).FirstOrDefault.ListItemId

    End Function

    Public Function ToSQLString(ByVal pValue As Nullable(Of Guid)) As String

        If (pValue = Guid.Empty) Then
            Return String.Empty
        End If

        Return pValue.Value.ToString()
    End Function
    <Extension>
    Public Function ToSQLString(ByVal pValue As Guid) As String

        Dim byteArray As Byte() = pValue.ToByteArray
        Dim i As Integer
        Dim result As New StringBuilder("")
        For i = 0 To byteArray.Length - 1
            Dim hexStr As String = byteArray(i).ToString("X")
            If hexStr.Length < 2 Then
                hexStr = "0" & hexStr
            End If
            result.Append(hexStr)
        Next

        Return result.ToString()
    End Function


End Module

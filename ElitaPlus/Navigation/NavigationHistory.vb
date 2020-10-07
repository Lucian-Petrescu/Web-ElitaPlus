Imports System.IO
Imports System.Web
Imports System.Diagnostics
Imports Assurant.AssurNet.Services



Public Class NavigationHistory


#Region "CONSTANTS"
    Private Const PAGE_HISTORY As String = "PAGEHISTORY"
#End Region



#Region "FUNCTIONS"

    Private Shared Function PageHistory() As Stack
        Dim oStack As Stack


        oStack = DirectCast(HttpContext.Current.Session(PAGE_HISTORY), Stack)

        If oStack Is Nothing Then
            'if it's empty then create a new stack.
            oStack = New Stack()
            'make sure the home page is loaded in as the first page.
            If oStack.Count = 0 Then
                oStack.Push(ELPWebConstants.CFG_PATH_TO_HOME_PAGE)
            End If
        End If

        Return oStack

    End Function


    Private Shared Sub SavePageHistory(oStack As Stack)
        'Save the stack into memory using the session.
        HttpContext.Current.Session(PAGE_HISTORY) = oStack
    End Sub


    Public Shared Function ViewPagesFromHistory() As ArrayList
        'return the values in the stack as an array.
        Dim oStack As Stack
        Dim oArrayList As New ArrayList(oStack.ToArray)

        Return oArrayList

    End Function


    Public Shared Sub AddPageToHistory(sPageName As String)

        Dim sLastPageInHistory As String
        Dim sNextPageToAddToHistory As String

        Dim oStack As Stack
        'get the current page history collection.
        oStack = PageHistory()


        'retrieve the item on top of the stack if there is anything there.
        sLastPageInHistory = Path.GetFileName(oStack.Peek.ToString).ToUpper
        sNextPageToAddToHistory = Path.GetFileName(sPageName).ToUpper

        'add the item into the stack if it is not on top of the stack.
        If sLastPageInHistory <> sNextPageToAddToHistory Then
            oStack.Push(sPageName)
        End If

        'save the stack into memory
        SavePageHistory(oStack)

    End Sub


    Public Shared Sub ClearHistory()
        Dim oStack As Stack
        ' Dim CFG_HOME_PAGE As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
        Dim CFG_HOME_PAGE As String = ELPWebConstants.HOME_PAGE


        'get the current page history collection.
        oStack = PageHistory()
        oStack.Clear()

        'put the home page back into the stack.
        oStack.Push(CFG_HOME_PAGE)

        'save the stack into memory
        SavePageHistory(oStack)

    End Sub


    Public Shared Function LastPageByNumber(nCount As Int16) As String

        'return the top item in the stack and save it into memory.
        Dim oStack As Stack
        Dim sLastPage As String
        Dim nCountOfPagesToPop As Int16

        'get the current page history collection.
        oStack = PageHistory()


        For nCountOfPagesToPop = 0 To nCount

            If oStack.Count > 0 Then
                'pop twice since the current page the users sees is always sitting on top of the stack.
                sLastPage = oStack.Pop().ToString
            End If

            'if the home page is the last page in the stack push it back into the stack.
            KeepHomePageInHistory(sLastPage, oStack)

        Next

        'save the stack into memory
        SavePageHistory(oStack)

        Return sLastPage


    End Function


    Private Shared Function KeepHomePageInHistory(sLastPage As String, oStack As Stack) As Stack

        '  Dim CFG_HOME_PAGE As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
        Dim CFG_HOME_PAGE As String = ELPWebConstants.HOME_PAGE

        If Path.GetFileName(sLastPage).ToUpper = CFG_HOME_PAGE.ToUpper And oStack.Count = 0 Then
            oStack.Push(ELPWebConstants.CFG_PATH_TO_HOME_PAGE)

            'set the home page as the last page
            sLastPage = ELPWebConstants.CFG_PATH_TO_HOME_PAGE

            HttpContext.Current.Session(ELPWebConstants.SELECTED_TAB) = HttpContext.Current.Session(ELPWebConstants.HOME_PAGE_TAB_ID)

        End If

        Return oStack

    End Function



    Public Shared Function LastPage() As String

        'return the top item in the stack and save it into memory.
        Dim oStack As Stack
        Dim sLastPage As String
        ' Dim CFG_HOME_PAGE As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
        Dim CFG_HOME_PAGE As String = ELPWebConstants.HOME_PAGE

        'get the current page history collection.
        oStack = PageHistory()

        sLastPage = oStack.Pop().ToString

        If oStack.Count > 0 Then
            'pop twice since the current page the users sees is always sitting on top of the stack.
            sLastPage = oStack.Pop().ToString
        End If

        'if the home page is the last page in the stack push it back into the stack.
        If Path.GetFileName(sLastPage).ToUpper = CFG_HOME_PAGE.ToUpper And oStack.Count = 0 Then
            oStack.Push(ELPWebConstants.CFG_PATH_TO_HOME_PAGE)

            'set the home page as the last page
            sLastPage = ELPWebConstants.CFG_PATH_TO_HOME_PAGE

            HttpContext.Current.Session(ELPWebConstants.SELECTED_TAB) = HttpContext.Current.Session(ELPWebConstants.HOME_PAGE_TAB_ID)

        End If

        'save the stack into memory
        SavePageHistory(oStack)

        Return sLastPage & "?nextAction=return"

    End Function



#End Region



End Class

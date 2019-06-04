Namespace Generic

    Partial  Class FormatToPrint
        Inherits System.Web.UI.UserControl

#Region "Constants"

        Protected Const EXCEPTION_TEXT As String = "FormatToPrint can not access Data Source -- "
        Protected Const BTN_STYLE As String = "height:20px;width:90px; BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
        Protected Const PRINT_BTN_STYLE As String = "align:left; height:20px;width:90px; BACKGROUND-IMAGE: url(../Navigation/images/icons/reports_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
        Const PRINT_BTN_TEXT As String = "PRINT"
#End Region

#Region "Attributes"

        Private moAllDataView As DataView
        Private msTitle As String = " "
        Private msBtnToolTip As String = " "
        Private msBtnRedirect, msBtnText As String
        Private moPage As ElitaPlusPage
        Private moSubTitleTable As DataTable
        Private mnPageSize As Integer
        Private mbIsButton As Boolean
#End Region

#Region "Handlers"



#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
        End Sub

#End Region

#Region "Set Data"

        Public Sub SetData(ByVal aDataView As DataView, ByVal oPage As ElitaPlusPage)
            moAllDataView = aDataView
            moPage = oPage
        End Sub

#End Region

#Region "Create-Tables"

        Private Sub SetPageSize()
            Dim nCount As Integer = moAllDataView.Count
            Dim oRow As DataRow
            Dim oColumn As DataColumn
            Dim oRowWidth As Integer = 0

            mnPageSize = 30
            If nCount > 0 Then
                oRow = moAllDataView.Table.Rows(0)
                For Each oColumn In moAllDataView.Table.Columns
                    oRowWidth += (oRow(oColumn).ToString).Length
                Next
                If oRowWidth > 50 Then
                    mnPageSize = 15
                End If
            End If
        End Sub

        Public Sub CreateTables()
            Dim oDataGrid As DataGrid
            Dim nCount As Integer = moAllDataView.Count
            Dim nIndex As Integer
            Dim oTable As HtmlTable = New HtmlTable

            PopulateReportHeader(oTable)
            moPlaceHolder.Controls.Add(oTable)

            SetPageSize()
            For nIndex = 0 To nCount - 1 Step mnPageSize
                oDataGrid = New DataGrid
                If nIndex > 0 Then oDataGrid.Attributes.Add("style", "page-break-before: always;")
                PopulateNextTable(oDataGrid, nIndex)
                moPlaceHolder.Controls.Add(oDataGrid)
            Next
        End Sub

        Public Sub CreateTables(ByVal title As String, ByVal btnToolTip As String, ByVal btnRedirect As String, ByVal btnText As String)
            msTitle = title
            msBtnToolTip = btnToolTip
            msBtnText = btnText
            msBtnRedirect = btnRedirect
            mbIsButton = True
            CreateTables()
        End Sub

        Public Sub CreateTables(ByVal title As String, ByVal oSubTitleTable As DataTable)
            msTitle = title
            moSubTitleTable = oSubTitleTable
            mbIsButton = False
            CreateTables()
        End Sub
#End Region

#Region "Populate"

        ' Empty Row
        Private Sub PopulateEmpty(ByVal oTable As HtmlTable)
            Dim oRow As HtmlTableRow
            Dim oCell As HtmlTableCell

            oRow = New HtmlTableRow
            oCell = New HtmlTableCell
            oCell.Height = "20px"
            oRow.Cells.Add(oCell)
            oTable.Rows.Add(oRow)
        End Sub

        'Private Sub PopulateBtn(ByVal oBtn As HtmlButton)
        '    oBtn.InnerText = msBtnText
        '    oBtn.Attributes.Add("title", msBtnToolTip)
        '    oBtn.Attributes.Add("style", BTN_STYLE)
        '    oBtn.Attributes.Add("onclick", "javascript:return window.location.replace( '" & msBtnRedirect & "' )")
        'End Sub

        ' If mbIsButton = True Then
        ''Button
        '        PopulateBtn(oBtn)
        '        oRow = New HtmlTableRow
        '        oCell = New HtmlTableCell
        '        oCell.Controls.Add(oBtn)
        '        oRow.Cells.Add(oCell)
        '        oTable.Rows.Add(oRow)
        '    End If

        ' User Button like BACK
        'PopulateBtn(oTable)     ' User Button like BACK
        '    PopulatePrintBtn(oTable) 'Print Button

        Private Sub PopulateBtn(ByVal oRow As HtmlTableRow)
            If mbIsButton = False Then Return
            Dim oBtn As HtmlButton = New HtmlButton
            Dim oCell As HtmlTableCell

            oBtn.InnerText = msBtnText
            oBtn.Attributes.Add("title", msBtnToolTip)
            oBtn.Attributes.Add("style", BTN_STYLE)
            oBtn.Attributes.Add("onclick", "javascript:return window.location.replace( '" & msBtnRedirect & "' )")
            oCell = New HtmlTableCell
            oCell.Controls.Add(oBtn)
            oRow.Cells.Add(oCell)
        End Sub

        ' Print Button
        Private Sub PopulatePrintBtn(ByVal oRow As HtmlTableRow)
            Dim oBtn As HtmlButton = New HtmlButton
            Dim oCell As HtmlTableCell

            oBtn.InnerText = TranslationBase.TranslateLabelOrMessage(PRINT_BTN_TEXT)
            ' oBtn.Attributes.Add("title", msBtnToolTip)
            oBtn.Attributes.Add("style", PRINT_BTN_STYLE)
            oBtn.Attributes.Add("onclick", "javascript:return window.print()")
            oCell = New HtmlTableCell
            oCell.Controls.Add(oBtn)
            oRow.Cells.Add(oCell)
        End Sub

        ' Buttons
        Private Sub PopulateBtns(ByVal oTable As HtmlTable)
            Dim oRow As HtmlTableRow

            oRow = New HtmlTableRow
            PopulateBtn(oRow)     ' User Button like BACK
            PopulatePrintBtn(oRow) 'Print Button
            oTable.Rows.Add(oRow)
        End Sub

        Private Sub AddSubTitleCell(ByVal oRow As HtmlTableRow, ByVal sText As String)
            Dim oCell As HtmlTableCell

            oCell = New HtmlTableCell
            oCell.Height = "20px"
            oCell.Attributes.Add("style", "font-weight:bold;")
            oCell.InnerText = sText
            oRow.Cells.Add(oCell)
        End Sub

        Private Sub PopulateSubTitle(ByVal oTable As HtmlTable)
            Dim oDataRow As DataRow
            Dim oColumn As DataColumn
            Dim oRow As HtmlTableRow

            For Each oDataRow In moSubTitleTable.Rows
                oRow = New HtmlTableRow
                For Each oColumn In moSubTitleTable.Columns
                    AddSubTitleCell(oRow, oDataRow(oColumn).ToString)
                Next
                oTable.Rows.Add(oRow)
            Next
        End Sub

        Private Sub PopulateReportHeader(ByVal oTable As HtmlTable)
            Dim oRow As HtmlTableRow
            Dim oCell As HtmlTableCell

            PopulateEmpty(oTable) ' Empty
            PopulateBtns(oTable)    ' Buttons
            ' Title
            oRow = New HtmlTableRow
            oCell = New HtmlTableCell
            oCell.Width = "150px"
            oRow.Cells.Add(oCell)

            oCell = New HtmlTableCell
            oCell.Height = "40px"
            oCell.Attributes.Add("style", "font-size:25px; font-weight:bold;")
            oCell.InnerText = msTitle
            oRow.Cells.Add(oCell)
            oTable.Rows.Add(oRow)

            If Not moSubTitleTable Is Nothing Then PopulateSubTitle(oTable) ' SubTitle

            'If mbIsButton = True Then
            '    'Button
            '    PopulateBtn(oBtn)
            '    oRow = New HtmlTableRow
            '    oCell = New HtmlTableCell
            '    oCell.Controls.Add(oBtn)
            '    oRow.Cells.Add(oCell)
            '    oTable.Rows.Add(oRow)
            'End If

            'PopulateBtn(oTable)     ' User Button like BACK
            ' PopulatePrintBtn(oTable) 'Print Button

            PopulateEmpty(oTable) ' Empty
            'oRow = New HtmlTableRow
            'oCell = New HtmlTableCell
            'oCell.Height = "20px"
            'oRow.Cells.Add(oCell)
            'oTable.Rows.Add(oRow)

        End Sub

        Private Sub PopulateHeaders(ByVal oHeaders As DataColumnCollection, ByVal oTable As DataTable)
            Dim oHeader As DataColumn
            Dim oTranslationItem As TranslationItem
            Dim oAryTranslations As New ArrayList

            For Each oHeader In oHeaders
                oTranslationItem = New TranslationItem
                oTranslationItem.TextToTranslate = oHeader.ToString
                oAryTranslations.Add(oTranslationItem)
            Next
            TranslationBase.TranslateMessageList(oAryTranslations)

            For Each oTranslationItem In oAryTranslations
                oTable.Columns.Add(oTranslationItem.Translation)
            Next
        End Sub

        Private Sub PopulateNextTable(ByVal aDataGrid As DataGrid, ByVal start As Integer)
            Try
                Dim oRow As DataRow
                Dim oTable As DataTable = New DataTable
                Dim oRows As DataRowCollection = moAllDataView.Table.Rows
                Dim oNewRow As DataRow
                Dim oHeaders As DataColumnCollection = moAllDataView.Table.Columns
                Dim nIndex As Integer
                Dim nStop As Integer

                If (start + mnPageSize) < moAllDataView.Count Then
                    nStop = start + mnPageSize - 1
                Else
                    nStop = moAllDataView.Count - 1
                End If

                ' Styles
                With aDataGrid.HeaderStyle
                    .Font.Bold = True
                    .BackColor = Color.Tan
                End With
                With aDataGrid.AlternatingItemStyle
                    .BackColor = Color.PaleGoldenrod
                End With
                With aDataGrid.PagerStyle
                    .BackColor = Color.Tan
                    .Mode = PagerMode.NumericPages
                End With

                ' Headers
                PopulateHeaders(oHeaders, oTable)

                ' Body
                For nIndex = start To nStop
                    oNewRow = oTable.NewRow
                    oNewRow.ItemArray = oRows(nIndex).ItemArray
                    oTable.Rows.Add(oNewRow)
                Next

                aDataGrid.DataSource = oTable
                aDataGrid.DataBind()

            Catch ex As Exception
                Throw New DataNotFoundException(EXCEPTION_TEXT & ex.Message)
            End Try
        End Sub
#End Region

    End Class

End Namespace

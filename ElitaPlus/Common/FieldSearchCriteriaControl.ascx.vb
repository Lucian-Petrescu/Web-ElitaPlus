Imports System.Threading
Imports System.Text
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Globalization

<DefaultProperty("Value"), ToolboxData("<{0}:FieldSearchCriteriaControl runat=""server""><{0}:FieldSearchCriteriaControl>")> _
Public Class FieldSearchCriteriaControl
    Inherits System.Web.UI.UserControl

    ' Future Enhancements
    ' * Specify Default Search Type in Markup
    ' * Specify Available Search Type Values Subset in Markup

    ' * Specify Custom Error Messages in Markup

#Region "Enums"
    Public Enum DataTypeEnum
        None
        [Date]
        [DateTime]
        NumberLong
        NumberDouble
        NumberString
    End Enum
#End Region

#Region "Shared Methods/Fields/Constructors"
    Private Shared ReadOnly _javaScript As String

    Shared Sub New()
        Dim javaScriptBuilder As New StringBuilder
        javaScriptBuilder.Append("<script language=""JavaScript"" type=""text/javascript"">function ShowHideControls(e,t,n,r,i){if($(e).val()==""BT""){if(i){$(t).fadeIn(""slow"");$(n).fadeIn(""slow"");$(r).fadeIn(""slow"")}else{$(t).show();$(n).show();$(r).show()}}else{if(i){$(t).fadeOut(""slow"");$(n).fadeOut(""slow"");$(r).fadeOut(""slow"")}else{$(t).hide();$(n).hide();$(r).hide()}}}</script>")
        _javaScript = javaScriptBuilder.ToString()
        '<script language="javascript" type="text/javascript">
        '    function ShowHideControls(searchTypeClientId, andLabelClientId, criteria2ClientId, calenderClientId, fade) {
        '        if ($(searchTypeClientId).val() == 'BT') {
        '            if (fade) {
        '                $(andLabelClientId).fadeIn('slow');
        '                $(criteria2ClientId).fadeIn('slow');
        '                $(calenderClientId).fadeIn('slow');
        '            }
        '            else {
        '                $(andLabelClientId).show();
        '                $(criteria2ClientId).show();
        '                $(calenderClientId).show();
        '            }
        '        }
        '        else {
        '            if (fade) {
        '                $(andLabelClientId).fadeOut('slow');
        '                $(criteria2ClientId).fadeOut('slow');
        '                $(calenderClientId).fadeOut('slow');
        '            }
        '            else {
        '                $(andLabelClientId).hide();
        '                $(criteria2ClientId).hide();
        '                $(calenderClientId).hide();
        '            }
        '        }
        '    }
        '</script>
    End Sub

    Private Shared Function GetSearchType(ByVal value As String) As SearchTypeEnum
        Select Case value
            Case Codes.SEARCH_TYPE__EQUALS
                Return SearchTypeEnum.Equals
            Case Codes.SEARCH_TYPE__LESS_THAN_DATE, Codes.SEARCH_TYPE__LESS_THAN
                Return SearchTypeEnum.LessThan
            Case Codes.SEARCH_TYPE__GREATER_THAN_DATE, Codes.SEARCH_TYPE__GREATER_THAN
                Return SearchTypeEnum.GreaterThan
            Case Codes.SEARCH_TYPE__BETWEEN
                Return SearchTypeEnum.Between
            Case Codes.SEARCH_TYPE__LIKE
                Return SearchTypeEnum.Like
            Case Else
                Return SearchTypeEnum.Equals
        End Select
    End Function

    Private Shared Function GetSearchTypeCode(ByVal value As SearchTypeEnum, ByVal dataType As DataTypeEnum) As String
        Select Case value
            Case SearchTypeEnum.Equals
                Return Codes.SEARCH_TYPE__EQUALS
            Case SearchTypeEnum.LessThan
                If (dataType = DataTypeEnum.Date OrElse dataType = DataTypeEnum.DateTime) Then
                    Return Codes.SEARCH_TYPE__LESS_THAN_DATE
                Else
                    Return Codes.SEARCH_TYPE__LESS_THAN
                End If
            Case SearchTypeEnum.GreaterThan
                If (dataType = DataTypeEnum.Date OrElse dataType = DataTypeEnum.DateTime) Then
                    Return Codes.SEARCH_TYPE__GREATER_THAN_DATE
                Else
                    Return Codes.SEARCH_TYPE__GREATER_THAN
                End If
            Case SearchTypeEnum.Between
                Return Codes.SEARCH_TYPE__BETWEEN
            Case SearchTypeEnum.Like
                Return Codes.SEARCH_TYPE__LIKE
            Case Else
                Return String.Empty
        End Select
    End Function
#End Region

#Region "Fields/Constants"
    Private Const CLIENT_SCRIPT_KEY As String = "FieldSearchCriteriaClientScript"
    Private _dataType As New DataTypeEnum
#End Region

#Region "Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            If (Me.DataType = DataTypeEnum.Date) Then
                Me.ParentPage.AddCalendar_New(Me.imgCalender1, Me.moSearchCriteria1)
                Me.ParentPage.AddCalendar_New(Me.imgCalender2, Me.moSearchCriteria2)
            ElseIf (Me.DataType = DataTypeEnum.DateTime) Then
                Me.ParentPage.AddCalendarwithTime_New(Me.imgCalender1, Me.moSearchCriteria1)
                Me.ParentPage.AddCalendarwithTime_New(Me.imgCalender2, Me.moSearchCriteria2)
            End If

            If (Me.DataType = DataTypeEnum.Date OrElse Me.DataType = DataTypeEnum.DateTime) Then
                moSearchCriteria1.Style.Add("width", "135px")
                moSearchCriteria2.Style.Add("width", "135px")
            End If
        End If
        ElitaPlusPage.ClearLabelError(Me.moSearchLabel)
        '' Register Client Side Script
        If (Not Me.ParentPage.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), CLIENT_SCRIPT_KEY)) Then
            Me.ParentPage.ClientScript.RegisterClientScriptBlock(Me.GetType(), CLIENT_SCRIPT_KEY, _javaScript)
        End If
    End Sub
#End Region

#Region "Properties"
    Private ReadOnly Property ParentPage As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    <Category("Behavior"), Description("Gets/Sets the data type of Search Value."),
        MergableProperty(False), Bindable(False)>
    Public Property DataType As FieldSearchCriteriaControl.DataTypeEnum
        Get
            Return _dataType
        End Get
        Set(ByVal value As FieldSearchCriteriaControl.DataTypeEnum)
            If (value = DataTypeEnum.None) Then Throw New ArgumentException("None is not valid value for DataType", "DataType")
            If (_dataType <> value) Then
                _dataType = value
                ProcessDataTypeUpdate()
            End If

        End Set
    End Property

    <Category("Behavior"), Description("Gets/Sets the type of Search like Equals, Before, After etc."),
        MergableProperty(False), Bindable(False)>
    Public Property SearchType As SearchTypeEnum
        Get
            Return FieldSearchCriteriaControl.GetSearchType(Me.ParentPage.GetSelectedValue(moSearchType))
        End Get
        Set(ByVal value As SearchTypeEnum)
            If (Not Me.ParentPage.GetSelectedValue(moSearchType).Equals(FieldSearchCriteriaControl.GetSearchTypeCode(value, Me.DataType))) Then
                Me.ParentPage.SetSelectedItem(moSearchType, FieldSearchCriteriaControl.GetSearchTypeCode(value, Me.DataType))
            End If
        End Set
    End Property

    <Category("Appearance"), Description("Sets UI Prog Code of Search Label"),
        MergableProperty(False), Bindable(False)>
    Public Property Text As String
        Get
            Return Me.moSearchLabel.Text
        End Get
        Set(ByVal value As String)
            Me.moSearchLabel.Text = value
        End Set
    End Property

    Private ReadOnly Property FromValueControl As TextBox
        Get
            Return moSearchCriteria1
        End Get
    End Property

    Public Property FromValue As String
        Get
            Return FromValueControl.Text
        End Get
        Set(ByVal value As String)
            FromValueControl.Text = value
        End Set
    End Property

    Public Property ToValue As String
        Get
            If (SearchType = SearchTypeEnum.Between) Then
                Return moSearchCriteria2.Text
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            If (SearchType = SearchTypeEnum.Between) Then
                moSearchCriteria2.Text = value
            Else
                If (value Is Nothing OrElse value.Trim() = String.Empty) Then
                    moSearchCriteria2.Text = String.Empty
                Else
                    Throw New InvalidOperationException("ToValue property can not be set when SerachType is not 'Between'")
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Validates Values entered by User
    ''' </summary>
    ''' <value></value>
    ''' <returns>True when the values are validated successfullly, false if there are errors.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsValid As Boolean
        Get
            Return Validate(False)
        End Get
    End Property

    ''' <summary>
    ''' Checks if the values are entered in User Control or it is blank.
    ''' </summary>
    ''' <value></value>
    ''' <returns>True when search value is not entered, False othewise</returns>
    ''' <remarks>Checks if FromValue is specified. If SearchType is Between then ToValue is also checked.</remarks>
    Public ReadOnly Property IsEmpty As Boolean
        Get
            If (Not Me.FromValue Is Nothing AndAlso Me.FromValue.Trim() <> String.Empty) Then
                Return False
            End If
            If (Me.SearchType = SearchTypeEnum.Between) Then
                If (Not Me.ToValue Is Nothing AndAlso Me.ToValue.Trim() <> String.Empty) Then
                    Return False
                End If
            End If
            Return True
        End Get
    End Property

    Public Property Value As SearchCriteriaType
        Get
            If (Not IsValid) Then
                Throw New Exception()
            End If
            Select Case Me.DataType
                Case DataTypeEnum.NumberString
                    Return New SearchCriteriaStringType() With {.SearchType = Me.SearchType, .FromValue = Me.FromValue, .ToValue = Me.ToValue}
                Case DataTypeEnum.Date
                    Return New SearchCriteriaStructType(Of DateTime)(SearchDataType.Date) With {.SearchType = Me.SearchType, .FromValue = GetDateValue(Me.FromValue), .ToValue = GetDateValue(Me.ToValue)}
                Case DataTypeEnum.DateTime
                    Return New SearchCriteriaStructType(Of DateTime)(SearchDataType.DateTime) With {.SearchType = Me.SearchType, .FromValue = GetDateValue(Me.FromValue), .ToValue = GetDateValue(Me.ToValue)}
                Case DataTypeEnum.NumberLong
                    Return New SearchCriteriaStructType(Of Long)(SearchDataType.Number) With {.SearchType = Me.SearchType, .FromValue = GetLongValue(Me.FromValue), .ToValue = GetLongValue(Me.ToValue)}
                Case DataTypeEnum.NumberDouble
                    Return New SearchCriteriaStructType(Of Double)(SearchDataType.Amount) With {.SearchType = Me.SearchType, .FromValue = GetDoubleValue(Me.FromValue), .ToValue = GetDoubleValue(Me.ToValue)}
                Case Else
                    Throw New NotSupportedException()
            End Select
        End Get
        Set(ByVal value As SearchCriteriaType)
            Me.SearchType = value.SearchType
            Select Case Me.DataType
                Case DataTypeEnum.NumberString
                    With DirectCast(value, SearchCriteriaStringType)
                        Me.FromValue = .FromValue
                        If (Me.SearchType = SearchTypeEnum.Between) Then Me.ToValue = .ToValue
                    End With
                Case DataTypeEnum.Date
                    With DirectCast(value, SearchCriteriaStructType(Of Date))
                        If (.FromValue.HasValue) Then
                            Me.FromValue = .FromValue.Value.ToString(Me.ParentPage.DATE_FORMAT)
                        End If
                        If (Me.SearchType = SearchTypeEnum.Between) Then
                            If (.ToValue.HasValue) Then
                                Me.ToValue = .ToValue.Value.ToString(Me.ParentPage.DATE_FORMAT)
                            End If
                        End If
                    End With
                Case DataTypeEnum.DateTime
                    With DirectCast(value, SearchCriteriaStructType(Of Date))
                        If (.FromValue.HasValue) Then
                            Me.FromValue = .FromValue.Value.ToString(Me.ParentPage.DATE_TIME_FORMAT_12)
                        End If
                        If (Me.SearchType = SearchTypeEnum.Between) Then
                            If (.ToValue.HasValue) Then
                                Me.ToValue = .ToValue.Value.ToString(Me.ParentPage.DATE_TIME_FORMAT_12)
                            End If
                        End If
                    End With
                Case DataTypeEnum.NumberLong
                    With DirectCast(value, SearchCriteriaStructType(Of Long))
                        If (.FromValue.HasValue) Then
                            Me.FromValue = .FromValue.Value.ToString()
                        End If
                        If (Me.SearchType = SearchTypeEnum.Between) Then
                            If (.ToValue.HasValue) Then
                                Me.ToValue = .ToValue.Value.ToString()
                            End If
                        End If
                    End With
                Case DataTypeEnum.NumberDouble
                    With DirectCast(value, SearchCriteriaStructType(Of Double))
                        If (.FromValue.HasValue) Then
                            Me.FromValue = Me.ParentPage.GetAmountFormattedString(Convert.ToDecimal(.FromValue.Value))
                        End If
                        If (Me.SearchType = SearchTypeEnum.Between) Then
                            If (.ToValue.HasValue) Then
                                Me.ToValue = Me.ParentPage.GetAmountFormattedString(Convert.ToDecimal(.ToValue.Value))
                            End If
                        End If
                    End With
                Case Else
                    Throw New NotSupportedException()
            End Select

        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Clears the content of Search Box
    ''' </summary>
    ''' <remarks>Does not change Search Type</remarks>
    Public Sub Clear()
        Me.FromValue = String.Empty
        Me.ToValue = String.Empty
    End Sub

    ''' <summary>
    ''' Validates Values entered by User and display them in Page Message Controller as Errors. Also, the search labels change the color to Red.
    ''' </summary>
    ''' <returns>True when the values are validated successfullly, false if there are errors.</returns>
    ''' <remarks></remarks>
    Public Function Validate() As Boolean
        Return Validate(True)
    End Function

    ''' <summary>
    ''' Validates Values entered by User. Optionally depending on updateUILabel parameter, display them in Page Message Controller as Errors and change the color of search labels to Red.
    ''' </summary>
    ''' <param name="updateUILabels"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Validate(ByVal updateUILabels As Boolean) As Boolean
        Dim returnValue As Boolean = True
        Dim firstParameterSpecified As Boolean = False
        Dim secondParameterSpecified As Boolean = False
        ' Check if First Parameter is specified
        If (Not (moSearchCriteria1.Text Is Nothing OrElse moSearchCriteria1.Text.Trim() = String.Empty)) Then
            firstParameterSpecified = True
        End If

        ' Check if Second Parameter is specified
        If (Me.SearchType = SearchType.Between) AndAlso (Not (moSearchCriteria2.Text Is Nothing OrElse moSearchCriteria2.Text.Trim() = String.Empty)) Then
            secondParameterSpecified = True
        End If

        ' Check if SearchType is Between then either both parameters are specified else none is specified
        If (Me.SearchType = SearchType.Between) Then
            returnValue = (firstParameterSpecified = secondParameterSpecified)
            If (updateUILabels AndAlso Not returnValue) Then
                Me.ParentPage.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE_FORMAT)
                ElitaPlusPage.SetLabelError(Me.moSearchLabel)
            End If
        End If

        ' Validate Data Type of First Value
        If (returnValue AndAlso firstParameterSpecified) Then
            returnValue = IsValidDataType(moSearchCriteria1.Text)
            If (updateUILabels AndAlso Not returnValue) Then
                Me.ParentPage.MasterPage.MessageController.AddError(String.Format("{0} : {1}", moSearchLabel.Text, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE_FORMAT)), False)
                ElitaPlusPage.SetLabelError(Me.moSearchLabel)
            End If
        End If

        ' Validate Data Type of Second Value
        If (returnValue AndAlso secondParameterSpecified) Then
            returnValue = IsValidDataType(moSearchCriteria2.Text)
            If (updateUILabels AndAlso Not returnValue) Then
                Me.ParentPage.MasterPage.MessageController.AddError(String.Format("{0} : {1}", moSearchLabel.Text, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE_FORMAT)), False)
                ElitaPlusPage.SetLabelError(Me.moSearchLabel)
            End If
        End If

        Return returnValue
    End Function

    Private Sub ProcessDataTypeUpdate()
        With moSearchType.Items
            .Clear()
            .Add(New ListItem("Equals", Codes.SEARCH_TYPE__EQUALS))
            If (Me.DataType = DataTypeEnum.NumberString) Then
                .Add(New ListItem("Like", Codes.SEARCH_TYPE__LIKE))
            End If
            If (Me.DataType = DataTypeEnum.Date OrElse Me.DataType = DataTypeEnum.DateTime) Then
                .Add(New ListItem("Before", Codes.SEARCH_TYPE__LESS_THAN_DATE))
                .Add(New ListItem("After", Codes.SEARCH_TYPE__GREATER_THAN_DATE))
            Else
                .Add(New ListItem("Less Than", Codes.SEARCH_TYPE__LESS_THAN))
                .Add(New ListItem("Greater Than", Codes.SEARCH_TYPE__GREATER_THAN))
            End If
            .Add(New ListItem("Between", Codes.SEARCH_TYPE__BETWEEN))
        End With
        If (Me.DataType = DataTypeEnum.Date OrElse Me.DataType = DataTypeEnum.DateTime) Then
            imgCalender1.Visible = True
            imgCalender2.Visible = True
        Else
            imgCalender1.Visible = False
            imgCalender2.Visible = False
        End If
    End Sub

    Private Function IsValidDataType(ByVal value As String) As Boolean
        Select Case Me.DataType
            Case DataTypeEnum.Date, DataTypeEnum.DateTime
                Dim dateValue As Date
                value = DateHelper.GetDateValue(value)
                Return Date.TryParse(value, Thread.CurrentThread.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, dateValue)
            Case DataTypeEnum.NumberLong
                Dim longValue As Long
                Return Long.TryParse(value, NumberStyles.AllowLeadingSign Or NumberStyles.AllowLeadingWhite Or NumberStyles.AllowThousands Or NumberStyles.AllowTrailingSign Or NumberStyles.AllowTrailingWhite, Thread.CurrentThread.CurrentCulture, longValue)
            Case DataTypeEnum.NumberDouble
                Dim doubleValue As Double
                Return Double.TryParse(value, NumberStyles.AllowLeadingSign Or NumberStyles.AllowLeadingWhite Or NumberStyles.AllowThousands Or NumberStyles.AllowTrailingSign Or NumberStyles.AllowTrailingWhite Or NumberStyles.AllowDecimalPoint, Thread.CurrentThread.CurrentCulture, doubleValue)
            Case Else
                Return True
        End Select
    End Function

    Private Function GetDoubleValue(ByVal value As String) As Nullable(Of Double)
        If (value Is Nothing OrElse value.Trim() = String.Empty) Then Return Nothing
        If (Me.DataType = DataTypeEnum.NumberDouble) Then
            Dim doubleValue As Double
            If (Double.TryParse(value, NumberStyles.AllowLeadingSign Or NumberStyles.AllowLeadingWhite Or NumberStyles.AllowThousands Or NumberStyles.AllowTrailingSign Or NumberStyles.AllowTrailingWhite Or NumberStyles.AllowDecimalPoint, Thread.CurrentThread.CurrentCulture, doubleValue)) Then
                Return doubleValue
            Else
                Return Nothing
            End If
        Else
            Throw New InvalidOperationException()
        End If
    End Function

    Private Function GetLongValue(ByVal value As String) As Nullable(Of Long)
        If (value Is Nothing OrElse value.Trim() = String.Empty) Then Return Nothing
        If (Me.DataType = DataTypeEnum.NumberLong) Then
            Dim longValue As Long
            If (Long.TryParse(value, NumberStyles.AllowLeadingSign Or NumberStyles.AllowLeadingWhite Or NumberStyles.AllowThousands Or NumberStyles.AllowTrailingSign Or NumberStyles.AllowTrailingWhite, Thread.CurrentThread.CurrentCulture, longValue)) Then
                Return longValue
            Else
                Return Nothing
            End If
        Else
            Throw New InvalidOperationException()
        End If
    End Function

    Private Function GetDateValue(ByVal value As String) As Nullable(Of DateTime)
        If (value Is Nothing OrElse value.Trim() = String.Empty) Then Return Nothing
        If (Me.DataType = DataTypeEnum.Date OrElse Me.DataType = DataTypeEnum.DateTime) Then
            Dim dateValue As Date
            If (Date.TryParse(value, Thread.CurrentThread.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, dateValue)) Then
                '' Remove any time portion accidently added
                If (Me.DataType = DataTypeEnum.Date) Then
                    dateValue = New DateTime(dateValue.Year, dateValue.Month, dateValue.Day)
                End If
                Return dateValue
            Else
                Return Nothing
            End If
        Else
            Throw New InvalidOperationException()
        End If
    End Function
#End Region

End Class
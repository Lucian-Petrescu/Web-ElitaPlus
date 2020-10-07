Imports System.Threading
Imports System.Reflection

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=True)> _
Public Class DateCompareValidatorAttribute
    Inherits ValidBaseAttribute

    Public Enum CompareType
        Equal
        NotEqual
        GreaterThan
        LessThan
        GreaterThanOrEqual
        LessThanOrEqual
    End Enum

    Public Enum DefaultType
        Today
        Now
        MinDate
        MaxDate
        UtcToday
        UtcNow
    End Enum

    Public Enum CompareToPropertyType
        [Property]
        [Nothing]
    End Enum

    Public Sub New(ByVal fieldDisplayName As String, ByVal message As String, ByVal pCompareToPropertyName As String, ByVal pComparisionType As CompareType)
        MyBase.New(fieldDisplayName, message)
        CompareToPropertyName = pCompareToPropertyName
        ComparisionType = pComparisionType
        DefaultCompareToValue = DefaultType.MaxDate
        DefaultCompareValue = DefaultType.MinDate
        CheckWhenNew = False
        CompareToType = CompareToPropertyType.Property
    End Sub

    Private _comparisionType As CompareType
    Private _compareToPropertyName As String
    Private _defaultCompareValue As DefaultType
    Private _defaultCompareToValue As DefaultType
    Private _checkWhenNew As Boolean
    Private _compareToType As CompareToPropertyType

    ''' <summary>
    ''' Defines Default Value to consider when the CompareToValue is nothing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefaultCompareToValue() As DefaultType
        Get
            Return _defaultCompareToValue
        End Get
        Set(ByVal value As DefaultType)
            _defaultCompareToValue = value
        End Set
    End Property

    ''' <summary>
    ''' Defines Default Value to consider when the CompareValue is nothing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefaultCompareValue() As DefaultType
        Get
            Return _defaultCompareValue
        End Get
        Set(ByVal value As DefaultType)
            _defaultCompareValue = value
        End Set
    End Property

    ''' <summary>
    ''' Defines Comparision Operator E.g. Equal, Not Equal, Greater Than etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ComparisionType() As CompareType
        Get
            Return _comparisionType
        End Get
        Set(ByVal value As CompareType)
            _comparisionType = value
        End Set
    End Property

    ''' <summary>
    ''' Name of the property who's value is to be compared. The property should be of one of the Types <see cref="DateTime" />, <see cref="Date" />, <see cref="DateTimeType" />, <see cref="DateType" />, <see cref="Nullable(Of DateTime)" /> and <see cref="Nullable(Of Date)" />
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Default Value = CompareToPropertyType.Property. When CompareToType = CompareToPropertyType.Property then this field is Mandatory.</remarks>
    Public Property CompareToPropertyName() As String
        Get
            Return _compareToPropertyName
        End Get
        Set(ByVal value As String)
            _compareToPropertyName = value
        End Set
    End Property

    ''' <summary>
    ''' Setting the value to True will execute the validation only when Object.IsNew = True; Otherwise will pass the validation without checking the values.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Default Value = False. When set to True, the object should have IsNew Property returning <see cref="Boolean" /> Value. </remarks>
    Public Property CheckWhenNew() As Boolean
        Get
            Return _checkWhenNew
        End Get
        Set(ByVal value As Boolean)
            _checkWhenNew = value
        End Set
    End Property

    ''' <summary>
    ''' Defines what is type of CompareToPropertyName. When CompareToPropertyType.Property then the value in CompareToPropertyName is Name of Property of Class;
    ''' when CompareToPropertyType.Nothing then value of CompareTo is derived using DefaultCompareToValue property.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CompareToType() As CompareToPropertyType
        Get
            Return _compareToType
        End Get
        Set(ByVal value As CompareToPropertyType)
            _compareToType = value
        End Set
    End Property

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
        Dim value As DateTime
        Dim valueToCompare As DateTime
        Dim propInfo As PropertyInfo
        Dim propValue As Object

        If (CheckWhenNew) Then
            propInfo = context.GetType().GetProperty("IsNew")

            If (propInfo Is Nothing) Then
                Throw New InvalidOperationException(String.Format("Propety with Name {0} not found in Object of Type {1}", "IsNew", context.GetType().FullName))
            End If
            If Not (DirectCast(propInfo.GetValue(context, Nothing), Boolean)) Then
                Return True
            End If
        End If

        If (Me.CompareToType = CompareToPropertyType.Property) Then
            If (CompareToPropertyName Is Nothing OrElse CompareToPropertyName.Trim().Length = 0) Then
                Throw New InvalidOperationException("DateCompareValidatorAttribute::CompareToPropertyName can not be Blank")
            End If
            propInfo = context.GetType().GetProperty(CompareToPropertyName)
            If (propInfo Is Nothing) Then
                Throw New InvalidOperationException(String.Format("Propety with Name {0} not found in Object of Type {1}", CompareToPropertyName, context.GetType().FullName))
            End If
            propValue = propInfo.GetValue(context, Nothing)
        Else
            propValue = Nothing
        End If
        valueToCompare = GetDate(propValue, DefaultCompareToValue)
        value = GetDate(objectToCheck, DefaultCompareValue)
        Select Case ComparisionType
            Case CompareType.Equal
                Return valueToCompare.Equals(value)
            Case CompareType.NotEqual
                Return Not valueToCompare.Equals(value)
            Case CompareType.GreaterThan
                Return value > valueToCompare
            Case CompareType.LessThan
                Return value < valueToCompare
            Case CompareType.GreaterThanOrEqual
                Return value >= valueToCompare
            Case CompareType.LessThanOrEqual
                Return value <= valueToCompare
        End Select
    End Function

    Private Function GetDate(ByVal value As Object, ByVal pDefaultType As DefaultType) As DateTime
        Dim returnValue As DateTime
        If (value Is Nothing) Then
            returnValue = GetDefaultDate(pDefaultType)
        ElseIf (TypeOf (value) Is DateTime) Then ' Date and DateTime are one and the same, so we do not have to handle the conditions
            returnValue = DirectCast(value, Date)
        ElseIf (TypeOf (value) Is String) Then
            returnValue = DateHelper.GetDateValue(value.ToString())
        ElseIf (TypeOf (value) Is DateType) Then
            Dim oDate As DateType
            oDate = DirectCast(value, DateType)
            returnValue = oDate.Value
        ElseIf (TypeOf (value) Is DateTimeType) Then
            Dim oDate As DateTimeType
            oDate = DirectCast(value, DateTimeType)
            returnValue = oDate.Value
        ElseIf (TypeOf (value) Is Nullable(Of DateTime)) Then
            Dim oDate As Nullable(Of DateTime)
            oDate = DirectCast(value, Nullable(Of DateTime))
            If (oDate.HasValue) Then
                returnValue = oDate.Value
            Else
                returnValue = GetDefaultDate(pDefaultType)
            End If
        End If
        ' Rounding off to Seconds accuracy, removing Miliseconds part
        Return New DateTime(returnValue.Year, returnValue.Month, returnValue.Day, returnValue.Hour, returnValue.Minute, returnValue.Second)
    End Function

    Private Function GetDefaultDate(ByVal pDefaultType As DefaultType) As DateTime
        Select Case pDefaultType
            Case DefaultType.Today
                Return DateTime.Today
            Case DefaultType.Now
                Return DateTime.Now
            Case DefaultType.MinDate
                Return DateTime.MinValue
            Case DefaultType.MaxDate
                Return DateTime.MaxValue
            Case DefaultType.UtcToday
                Return DateTime.UtcNow.Date
            Case DefaultType.UtcNow
                Return DateTime.UtcNow
        End Select
    End Function

End Class

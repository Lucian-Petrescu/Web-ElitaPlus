﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=4.0.30319.33440.
'

'''<remarks/>
<XmlInclude(GetType(ConstantToken)),
 XmlInclude(GetType(ComplexExpression)),
 XmlInclude(GetType(VariableToken)),
 XmlInclude(GetType(SwitchExpression)),
 GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd"),
 XmlRoot("Expression", [Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd", IsNullable:=False)>
Partial Public MustInherit Class BaseExpression
End Class

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Partial Public Class SwitchCaseExpression

    Private caseValueField As String

    Private expressionField As BaseExpression

    '''<remarks/>
    Public Property CaseValue() As String
        Get
            Return Me.caseValueField
        End Get
        Set
            Me.caseValueField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Expression() As BaseExpression
        Get
            Return Me.expressionField
        End Get
        Set
            Me.expressionField = Value
        End Set
    End Property
End Class

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Partial Public Class ConstantToken
    Inherits BaseExpression

    Private valueField As Decimal

    '''<remarks/>
    Public Property Value() As Decimal
        Get
            Return Me.valueField
        End Get
        Set
            Me.valueField = Value
        End Set
    End Property
End Class

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Partial Public Class ComplexExpression
    Inherits BaseExpression

    Private operationField As OperationType

    Private expressionField() As BaseExpression

    '''<remarks/>
    Public Property Operation() As OperationType
        Get
            Return Me.operationField
        End Get
        Set
            Me.operationField = Value
        End Set
    End Property

    '''<remarks/>
    <XmlElement("Expression")>
    Public Property Expression() As BaseExpression()
        Get
            Return Me.expressionField
        End Get
        Set
            Me.expressionField = Value
        End Set
    End Property
End Class

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Public Enum OperationType

    '''<remarks/>
    Minimum

    '''<remarks/>
    Maximum

    '''<remarks/>
    Percentage

    '''<remarks/>
    Average

    '''<remarks/>
    Total
End Enum

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Partial Public Class VariableToken
    Inherits BaseExpression

    Private variableNameField As String

    '''<remarks/>
    Public Property VariableName() As String
        Get
            Return Me.variableNameField
        End Get
        Set
            Me.variableNameField = Value
        End Set
    End Property
End Class

'''<remarks/>
<GeneratedCode("xsd", "4.0.30319.33440"),
 Serializable(),
 DebuggerStepThrough(),
 DesignerCategory("code"),
 XmlType([Namespace]:="http://Assurant.com/Elita/2016/Expression.xsd")>
Partial Public Class SwitchExpression
    Inherits BaseExpression

    Private variableNameField As String

    Private caseField() As SwitchCaseExpression

    Private otherwiseField As BaseExpression

    '''<remarks/>
    Public Property VariableName() As String
        Get
            Return Me.variableNameField
        End Get
        Set
            Me.variableNameField = Value
        End Set
    End Property

    '''<remarks/>
    <XmlElement("Case")>
    Public Property [Case]() As SwitchCaseExpression()
        Get
            Return Me.caseField
        End Get
        Set
            Me.caseField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Otherwise() As BaseExpression
        Get
            Return Me.otherwiseField
        End Get
        Set
            Me.otherwiseField = Value
        End Set
    End Property
End Class

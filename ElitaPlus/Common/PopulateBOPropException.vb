Imports Assurant.ElitaPlus.Common
Imports System.Runtime.Serialization

<Serializable()> Public Class PopulateBOPropException
    Inherits ElitaPlusException

#Region "Private Members"

    Private _propName As String
    Private _oCtrl As Control
    Private _lblCtrl As Label
    Private _gridColumn As DataGridColumn
    Private _gridViewField As DataControlField

#End Region

#Region "Constructors"
    Public Sub New(BoPropName As String, oControl As Control, lbl As Label, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        _propName = BoPropName
        _oCtrl = oControl
        _lblCtrl = lbl
    End Sub

    Public Sub New(BoPropName As String, oControl As Control, gridColumn As DataGridColumn, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        _propName = BoPropName
        _oCtrl = oControl
        _gridColumn = gridColumn
    End Sub

    Public Sub New(BoPropName As String, oControl As Control, gridColumn As DataControlField, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        _propName = BoPropName
        _oCtrl = oControl
        _gridViewField = gridColumn
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region

    

#Region "Properties"
    Public ReadOnly Property BoPropName() As String
        Get
            Return _propName
        End Get
    End Property

    Public ReadOnly Property Control() As Control
        Get
            Return _oCtrl
        End Get
    End Property

    Public ReadOnly Property LabelControl() As Label
        Get
            Return _lblCtrl
        End Get
    End Property

    Public ReadOnly Property GridColumnControl() As DataGridColumn
        Get
            Return _gridColumn
        End Get
    End Property
#End Region


End Class

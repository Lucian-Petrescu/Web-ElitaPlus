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
    Public Sub New(ByVal BoPropName As String, ByVal oControl As Control, ByVal lbl As Label, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        Me._propName = BoPropName
        Me._oCtrl = oControl
        Me._lblCtrl = lbl
    End Sub

    Public Sub New(ByVal BoPropName As String, ByVal oControl As Control, ByVal gridColumn As DataGridColumn, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        Me._propName = BoPropName
        Me._oCtrl = oControl
        Me._gridColumn = gridColumn
    End Sub

    Public Sub New(ByVal BoPropName As String, ByVal oControl As Control, ByVal gridColumn As DataControlField, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Error Populating as business property", ErrorCodes.POPULATE_PROP_ERR, innerExc)
        Me._propName = BoPropName
        Me._oCtrl = oControl
        Me._gridViewField = gridColumn
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region

    

#Region "Properties"
    Public ReadOnly Property BoPropName() As String
        Get
            Return Me._propName
        End Get
    End Property

    Public ReadOnly Property Control() As Control
        Get
            Return Me._oCtrl
        End Get
    End Property

    Public ReadOnly Property LabelControl() As Label
        Get
            Return Me._lblCtrl
        End Get
    End Property

    Public ReadOnly Property GridColumnControl() As DataGridColumn
        Get
            Return Me._gridColumn
        End Get
    End Property
#End Region


End Class

Imports System.Text.RegularExpressions

Public Class GVSUpdateSVC
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME As String = "TRANSACTION"
    Private Const TABLE_NAME_TRANSACTION_HEADER As String = "TRANSACTION_HEADER"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "FUNCTION_TYPE"
#End Region

#Region "Variables"
    Private msInputXml, msFunctionToProcess As String
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GVSUpdateSVCDs, ByVal xml As String, _
                   ByVal functionToProcess As String)
        MyBase.New()
        InputXml = Xml
        FuncToProc = functionToProcess
    End Sub

#End Region

#Region "Private Members"

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"
    Private Property InputXml() As String
        Get
            Return msInputXml
        End Get
        Set(ByVal value As String)
            msInputXml = value
        End Set
    End Property

    Private Property FuncToProc() As String
        Get
            Return msFunctionToProcess
        End Get
        Set(ByVal value As String)
            msFunctionToProcess = value
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim xmlOut As String

        Try
            xmlOut = GVSService.SendToGvs(InputXml, FuncToProc)
            Return xmlOut
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Extended Properties"

    Public Property TransactionId() As String
        Get
            If Row(DATA_COL_NAME_TRANSACTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_TRANSACTION_ID), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_TRANSACTION_ID, Value)
        End Set
    End Property

    Public Property FunctionType() As String
        Get
            If Row(DATA_COL_NAME_FUNCTION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_FUNCTION_TYPE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_FUNCTION_TYPE, Value)
        End Set
    End Property

#End Region

End Class

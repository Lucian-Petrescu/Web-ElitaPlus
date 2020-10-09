Imports System.Text.RegularExpressions

Public Class AcselXQuoteCancelCert
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "FUNCTION_TYPE"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "TRANSACTION"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
#End Region

#Region "Variables"
    Private msInputXml, msFunctionToProcess As String
#End Region

#Region "Constructors"

    Public Sub New(ds As AcselXQuoteCancelCertDs, xml As String, _
                   functionToProcess As String)
        MyBase.New()
        InputXml = xml
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

    Private Property InputXml As String
        Get
            Return msInputXml
        End Get
        Set
            msInputXml = value
        End Set
    End Property

    Private Property FuncToProc As String
        Get
            Return msFunctionToProcess
        End Get
        Set
            msFunctionToProcess = value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim xmlOut As String

        Try
            xmlOut = AcselXService.SendToAcselX(InputXml, FuncToProc)
            Return xmlOut
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class




'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/3/2005)  ********************

Public Class CertificateDataProtection
    Inherits BusinessObjectBase

#Region "Save Data for Data Protection"
    Public Shared Sub SaveNewForm(ByRef intErrCode As Integer, ByRef strErrMsg As String, ByVal certId As Guid,
                           ByVal restricred As String, ByVal comment As String,
                           ByVal requestId As String)
        Dim dal As New CertificateDataProtectionDAL
        dal.SaveForm(intErrCode, strErrMsg, certId, restricred, comment, requestId)
    End Sub
#End Region


#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal certId As Guid) As DataProtectionSearchDV
        Try
            Dim dal As New CertificateDataProtectionDAL
            Return New DataProtectionSearchDV(dal.LoadList(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class DataProtectionSearchDV
        Inherits DataView
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region




End Class



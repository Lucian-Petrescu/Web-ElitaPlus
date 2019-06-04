Imports System.Runtime.Serialization

    <Serializable()> Public Class GUIException
        Inherits ElitaPlusException

    Public Sub New(ByVal message As String, ByVal Code As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, Code, innerException)
        Me.Type = ErrorTypes.ERROR_GUI
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

#Region "Validation"

    Public Shared Sub ValidateDate(ByVal lbl As Label, ByVal sDate As String, Optional ByVal Err_Mess As String = Nothing)
        Dim dt As Date
        dt = DateHelper.GetDateValue(sDate)
        ' If Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(sDate)) Then
        If (dt = Date.MinValue) Then
            If Not lbl Is Nothing Then
                ElitaPlusPage.SetLabelError(lbl)
            End If
            If Err_Mess Is Nothing Then
                Throw New GUIException(Assurant.ElitaPlus.ElitaPlusWebApp.Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            Else
                Throw New GUIException(Assurant.ElitaPlus.ElitaPlusWebApp.Message.MSG_BEGIN_END_DATE, Err_Mess)
            End If
        End If
    End Sub

#End Region
End Class

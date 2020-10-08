Public Class VscTableProcessed
    Inherits BusinessObjectBase

#Region "StoreProcedures Control"

    Public Shared Sub ProcessFileRecords(oVscTableProcessedData As VscTableProcessedData)
        ' Dim ds As DataSet
        Try
            Dim dal As New VscTableProcessedDAL
            oVscTableProcessedData.companyGroupCode = Authentication.CompanyGroupCode
            oVscTableProcessedData.interfaceStatus_id = _
                        InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VSC_PROCESS)
            dal.ProcessFileRecords(oVscTableProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
        ' Return ds
    End Sub

    'Public Shared Function ProcessFileRecords(ByVal oVscTableProcessedData As VscTableProcessedData) As DataSet
    '    Dim ds As DataSet
    '    Dim dsArr As DataSet()
    '    Dim count As Integer = 0
    '    Try
    '        Dim dal As New VscTableProcessedDAL
    '        oVscTableProcessedData.companyGroupCode = Authentication.CompanyGroupCode
    '        dsArr = BusinessObjectBase.SplitDatasetForXML(oVscTableProcessedData.allDs, Nothing)
    '        For Each inputDs As DataSet In dsArr
    '            oVscTableProcessedData.xmlData = XMLHelper.FromDatasetToXML(inputDs)
    '            count += 1
    '            If count < dsArr.Length Then
    '                oVscTableProcessedData.isLastRecord = "False"
    '            Else
    '                oVscTableProcessedData.isLastRecord = "True"
    '            End If

    '            ds = dal.ProcessFileRecords(oVscTableProcessedData)
    '        Next
    '        '    ds = dal.ProcessFileRecords(oVscTableProcessedData)

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    '    Return ds
    'End Function

    'Public Shared Sub ProcessFileRecords(ByVal oVscTableProcessedData As VscTableProcessedData)
    '    Try
    '        Dim dal As New VscTableProcessedDAL

    '        oVscTableProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
    '        dal.ProcessFileRecords(oVscTableProcessedData)

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Sub

#End Region

End Class

Imports RMEncryption
Imports Assurant.AssurNet.Services
Imports System.Xml

Public Class Services

#Region "Constants"



    
#End Region


#Region "WebServicesNames"
    Public Class WebServicesNames

        Public Const WEB_SERVICE_NODE_NAME As String = "WEB_SERVICE"

        Public Const ATTRIBUTE_NAME = "name"
        Public Const SETTING_TAG As String = "REQUEST"

        Public Const WEB_SERVICES_DATA_SET_NAME = "WebServicesDataSet"
        Public Const WEB_SERVICES_FUNCTIONS_DATA_SET_NAME = "WebServiceFunctionsDataSet"
        Public Const WEB_SERVICES_TABLE_NAME = "WebServicesTable"
        Public Const WEB_SERVICES_FUNCTIONS_TABLE_NAME = "WebServiceFunctionsTable"
        Public Const COL_ID = "id"
        Public Const COL_CODE = "code"
        Public Const COL_DESCRIPTION = "description"

        Public Shared Function GetWebServiceNames() As DataSet
            Dim servicePath As String

            Dim parentNode As XmlNode = ConfigReader.ConfigDocument(GetType(Services))
            'Dim webServiceNodeName As String = "WEB_SERVICE"
            'Dim attributeName As String = "name"

            Dim ds As New DataSet(WEB_SERVICES_DATA_SET_NAME)
            Dim objWebServicesTable As DataTable = New DataTable(WEB_SERVICES_TABLE_NAME)
            ' Define the columns of the table.
            objWebServicesTable.Columns.Add(New DataColumn(COL_ID, GetType(String)))
            objWebServicesTable.Columns.Add(New DataColumn(COL_CODE, GetType(String)))
            objWebServicesTable.Columns.Add(New DataColumn(COL_DESCRIPTION, GetType(String)))


            Dim outerList As XmlNodeList = parentNode.ChildNodes(1).SelectNodes(WEB_SERVICE_NODE_NAME)

            If outerList Is Nothing Then
                Throw New ConfigAccessException("Services path not found : " & GetType(Services).ToString)
            End If

            Dim node As XmlNode
            For Each node In outerList
                Dim row As DataRow = objWebServicesTable.NewRow
                row(COL_ID) = Guid.Empty.ToString
                row(COL_CODE) = node.Attributes(ATTRIBUTE_NAME).Value.ToUpper
                row(COL_DESCRIPTION) = node.Attributes(ATTRIBUTE_NAME).Value.ToUpper
                objWebServicesTable.Rows.Add(row)
            Next

            ds.Tables.Add(objWebServicesTable)
            Return ds

        End Function

        Public Shared Function GetWebServiceFunctionsNames(strWebServiceName As String) As DataSet
            Dim servicePath As String

            Dim parentNode As XmlNode = ConfigReader.ConfigDocument(GetType(Services))

            Dim ds As New DataSet(WEB_SERVICES_FUNCTIONS_DATA_SET_NAME)
            Dim objWebServicesTable As DataTable = New DataTable(WEB_SERVICES_FUNCTIONS_TABLE_NAME)
            ' Define the columns of the table.
            objWebServicesTable.Columns.Add(New DataColumn(COL_ID, GetType(String)))
            objWebServicesTable.Columns.Add(New DataColumn(COL_CODE, GetType(String)))
            objWebServicesTable.Columns.Add(New DataColumn(COL_DESCRIPTION, GetType(String)))


            Dim outerList As XmlNodeList = parentNode.ChildNodes(1).SelectNodes(WEB_SERVICE_NODE_NAME)

            If outerList Is Nothing Then
                Throw New ConfigAccessException("Services path not found : " & GetType(Services).ToString)
            End If

            Dim node As XmlNode
            For Each node In outerList
                If node.Attributes(ATTRIBUTE_NAME).Value.ToUpper = strWebServiceName.ToUpper Then
                    'If GetAttribute(node, webServiceName).ToUpper = attributeValue.ToUpper Then
                    Dim list As XmlNodeList = node.SelectNodes(SETTING_TAG)
                    Dim innerNode As XmlNode
                    For Each innerNode In list
                        If Not XMLHelper.GetAttribute(innerNode, ATTRIBUTE_NAME).ToUpper.Contains("GVS") Then
                            Dim row As DataRow = objWebServicesTable.NewRow
                            row(COL_ID) = Guid.Empty.ToString
                            row(COL_CODE) = XMLHelper.GetAttribute(innerNode, ATTRIBUTE_NAME).ToUpper
                            row(COL_DESCRIPTION) = XMLHelper.GetAttribute(innerNode, ATTRIBUTE_NAME).ToUpper
                            objWebServicesTable.Rows.Add(row)
                        End If
                    Next
                End If
                
            Next

            ds.Tables.Add(objWebServicesTable)
            Return ds




        End Function


    End Class
#End Region



End Class

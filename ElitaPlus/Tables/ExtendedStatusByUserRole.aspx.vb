Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Script.Services
Imports System.Xml
Imports System.Xml.Linq
Imports System.Text
Imports System.IO

Public Class ExtendedStatusByUserRole
    ' Inherits System.Web.UI.Page
    Inherits ElitaPlusSearchPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.ClearLabelsErrSign()
        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()

                Me.MasterPage.UsePageTabTitleInBreadCrum = False

                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EXTENDED_STATUS_BY_USER_ROLE")

                UpdateBreadCrum()
                'GetGrantData()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Public Sub ClearLabelsErrSign()
        Try
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()

        If (Not Me.Page Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("EXTENDED_STATUS_BY_USER_ROLE")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EXTENDED_STATUS_BY_USER_ROLE")
        End If

    End Sub

#Region "Button event handlers"
    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
    '    Try
    '        'Me.SetStateProperties()
    '        Me.State.PageIndex = 0
    '        'Me.State.selectedCertificateId = Guid.Empty
    '        Me.State.IsGridVisible = True
    '        ' Me.State.searchClick = True
    '        Me.State.searchDV = Nothing
    '        'Me.PopulateGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region
    'Generate xml data with companies, roles, extended status and grants

    <System.Web.Services.WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Xml)>
    Public Shared Function GetGrantData() As XmlDocument

        Dim listItems As List(Of ListItemDTO)
        Dim sw As System.IO.StringWriter
        Dim xEleCompany As XElement

        listItems = GetZAxisList("Company")
        xEleCompany = New XElement("Companies", _
                    From item In listItems _
                    Select New XElement("Company", _
                    New XElement("Id", item.Id), _
                    New XElement("Code", item.Name), _
                    New XElement("Description", item.Description), _
                    New XElement("Enabled", item.Enabled)))



        listItems = GetZAxisList("Role")
        Dim xEleRlole = New XElement("Roles", _
                    From item In listItems _
                    Select New XElement("Role", _
                    New XElement("Id", item.Id), _
                    New XElement("Code", item.Name), _
                    New XElement("Description", item.Description), _
                    New XElement("Enabled", item.Enabled)))


        listItems = GetZAxisList("ExtendedStatus")
        Dim xEleExtStatus = New XElement("ExtendedStatuses", _
                    From item In listItems _
                    Select New XElement("ExtendedStatus", _
                    New XElement("Id", item.Id), _
                    New XElement("Code", item.Name), _
                    New XElement("Description", item.Description), _
                    New XElement("Enabled", item.Enabled)))

        Dim dv As DataView = ClaimStatusByUserRole.GetData()
        'Session("ExtendedStatusByUserRole_Grants") = dv
        Dim grantItems As New List(Of GrantItemDTO)
        For index As Integer = 0 To dv.Count - 1
            Dim companyId As String = (New Guid(CType(dv.Item(index).Item("company_id"), Byte()))).ToString()
            Dim roleId As String = (New Guid(CType(dv.Item(index).Item("role_id"), Byte()))).ToString()
            Dim extendedStatusId As String = (New Guid(CType(dv.Item(index).Item("claim_status_by_group_id"), Byte()))).ToString()
            Dim Id As String = (New Guid(CType(dv.Item(index).Item("claim_status_by_role_id"), Byte()))).ToString()

            grantItems.Add(New GrantItemDTO With {.CompanyId = companyId, .RoleId = roleId, .ExtendedStatusId = extendedStatusId, .Id = Id})
        Next

        Dim xEleGrant = New XElement("Grants", _
                    From item In grantItems _
                    Select New XElement("Grant", _
                    New XElement("CompanyId", item.CompanyId), _
                    New XElement("RoleId", item.RoleId), _
                    New XElement("ExtendedStatusId", item.ExtendedStatusId), _
                    New XElement("Id", item.Id)))


        Dim xmlRoot As New XDocument(New XProcessingInstruction("xml-stylesheet", "type='text/xsl' href='CrossTab.xslt'"), New XElement("RoleCompanyStatus", xEleCompany, xEleRlole, xEleExtStatus, xEleGrant))
        xmlRoot.Declaration = New XDeclaration("1.0", "utf-8", Nothing)

        Dim xmlDoc As New XmlDocument
        Using xreader As XmlReader = xmlRoot.CreateReader()
            xmlDoc.Load(xreader)
        End Using
        Return xmlDoc
    End Function

    'load the third dropdown (zaxis) dropdown with data
    <System.Web.Services.WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetZAxisList(ByVal zAxisName As String) As List(Of ListItemDTO)
        Dim dv As DataView
        Dim dvdescription As DataView

        Dim listItems As List(Of ListItemDTO)
        If zAxisName = "Company" Then
            dv = LookupListNew.GetUserCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id)

        ElseIf zAxisName = "Role" Then

            dv = LookupListNew.GetRolesLookupList()


        ElseIf zAxisName = "ExtendedStatus" Then
            dv = LookupListNew.GetExtendedStatusByGroupUserRoleLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End If

        listItems = GetList(dv)
        Return listItems
    End Function

    'save the grants
    'xyAxisValue e.g BRZ-IHQ,ARG-IHQ
    <System.Web.Services.WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveGrants(ByVal xAxisName As String, ByVal yAxisName As String, ByVal zAxisName As String, ByVal xyAxisValue As String, ByVal zAxisValue As String) As Object
        Try
            Dim xmlDoc As XmlDocument = GetGrantData()
            'Dim fileName As String = HttpContext.Current.Server.MapPath("Data.xml")
            'Dim xmlDoc As New XmlDocument()
            'xmlDoc.Load(fileName)

            Dim grantsNode As XmlNodeList = xmlDoc.SelectNodes("RoleCompanyStatus/Grants/Grant[" & zAxisName & "Id = '" & zAxisValue & "']")

            Dim newGrants() As String = xyAxisValue.Split(New Char() {","})
            Dim newGrantList As New List(Of String)


            If Not String.IsNullOrEmpty(xyAxisValue) Then
                For Each grant As String In newGrants

                    Dim axisValues As New Dictionary(Of String, String)
                    Dim grantInfo() As String = grant.Split(New Char() {"-"})

                    Dim xAxisNode As XmlNode = xmlDoc.SelectSingleNode(GetNodePath(xAxisName) & "[Code='" & grantInfo(0) & "']/Id")
                    Dim xAxisId As String = xAxisNode.InnerText
                    Dim yAxisNode As XmlNode = xmlDoc.SelectSingleNode(GetNodePath(yAxisName) & "[Code='" & grantInfo(1) & "']/Id")
                    Dim yAxisId As String = yAxisNode.InnerText
                    'Dim zAxisNode As XmlNode = xmlDoc.SelectSingleNode(GetNodePath(zAxisName) & "[Id='" & zAxisValue & "']/Id")
                    'Dim zAxisId As String = zAxisNode.InnerText
                    Dim zAxisId As String = zAxisValue

                    axisValues.Add(xAxisName, xAxisId)
                    axisValues.Add(yAxisName, yAxisId)
                    axisValues.Add(zAxisName, zAxisId)
                    newGrantList.Add(axisValues.Item("Company") & "-" & axisValues.Item("Role") & "-" & axisValues.Item("ExtendedStatus"))


                    'check grant already exist
                    Dim grantExist As XmlNode = xmlDoc.SelectSingleNode("RoleCompanyStatus/Grants/Grant[CompanyId = '" & axisValues.Item("Company") & "' and RoleId = '" &
    axisValues.Item("Role") & "' and ExtendedStatusId = '" & axisValues.Item("ExtendedStatus") & "']")
                    'new grant
                    If grantExist Is Nothing Then
           

                        Dim bo As New ClaimStatusByUserRole()
                        bo.CompanyId = New Guid(axisValues.Item("Company"))
                        bo.RoleId = New Guid(axisValues.Item("Role"))
                        bo.ClaimStatusByGroupId = New Guid(axisValues.Item("ExtendedStatus"))
                        bo.Save()

                    End If
                Next
            End If


            'Delete missing grants
            For i As Integer = 0 To grantsNode.Count - 1
                Dim grant As XmlNode = grantsNode.Item(i)
                Dim grantText As String = grant.SelectSingleNode("CompanyId").InnerText & "-" & grant.SelectSingleNode("RoleId").InnerText & "-" & grant.SelectSingleNode("ExtendedStatusId").InnerText
                If Not newGrantList.Contains(grantText) Then
                    'grantsNode.RemoveChild(grant)
                    Dim id As New Guid(grant.SelectSingleNode("Id").InnerText)

                    Dim bo As New ClaimStatusByUserRole(id)
                    bo.Delete()
                    bo.Save()
                End If
            Next

            xmlDoc = Nothing
            Return New With {.Status = Message.SAVE_RECORD_CONFIRMATION, .Message = TranslationBase.TranslateLabelOrMessage(Message.SAVE_RECORD_CONFIRMATION)}
        Catch ex As Exception
            Return New With {.Status = Message.MSG_RECORD_NOT_SAVED, .Message = TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORD_NOT_SAVED)}
        End Try
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function GetTranslatedText(ByVal messageCode As String) As String
        Return TranslationBase.TranslateLabelOrMessage(messageCode)
    End Function

    Protected Shared Function GetNodePath(ByVal nodeName As String) As String
        Select Case nodeName
            Case "Company"
                Return "/RoleCompanyStatus/Companies/Company"
            Case "Role"
                Return "/RoleCompanyStatus/Roles/Role"
            Case "ExtendedStatus"
                Return "/RoleCompanyStatus/ExtendedStatuses/ExtendedStatus"
        End Select
        Return ""
    End Function

    Protected Shared Function GetList(ByVal dv As DataView) As List(Of ListItemDTO)
        Dim listItems As New List(Of ListItemDTO)
        For index As Integer = 0 To dv.Count - 1
            Dim id As String = (New Guid(CType(dv.Item(index).Item("id"), Byte()))).ToString()
            Dim enabled As String = If(dv.Table.Columns.Contains("enabled"), dv.Item(index).Item("enabled").ToString(), "Y")
            listItems.Add(New ListItemDTO With {.Id = id, .Name = dv.Item(index).Item("code"), .Description = dv.Item(index).Item("description"), .Enabled = enabled})
        Next
        Return listItems
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim xAxisName As String = GetNodeName(moHorizontalValues.SelectedValue)
        Dim yAxisName As String = GetNodeName(moVerticalValues.SelectedValue)
        Dim zAxisName As String

    End Sub

    Protected Function GetNodeName(ByVal Name As String)
        Select Case Name
            Case "1"
                GetNodeName = "Company"
            Case "2"
                GetNodeName = "Role"
            Case "3"
                GetNodeName = "ExtendedStatus"
        End Select
        Return GetNodeName
    End Function

    Protected Function GetZAxisName(ByVal xAxisName As String, ByVal yAxisName As String)

    End Function

End Class

Public Class ListItemDTO
    Public Name As String
    Public Id As String
    Public Description As String
    Public Enabled As String
End Class

Public Class GrantItemDTO
    Public CompanyId As String
    Public RoleId As String
    Public ExtendedStatusId As String
    Public Id As String
End Class

Public Class Utf8StringWriter
    Inherits StringWriter

    Public Overrides ReadOnly Property Encoding As Encoding
        Get
            Return System.Text.Encoding.UTF8
        End Get
    End Property
End Class



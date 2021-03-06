﻿Public Class GetSalutations
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_LANGUAGE_CODE As String = "language_code"
    Private Const TABLE_NAME As String = "GetSalutations"
    Private Const OUTPUT_TABLE_NAME As String = "Salutations"
    Private Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Private Const COL_NAME_SALUTATION_ID = "id"

    'error msg
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const LANGUAGE_NOT_FOUND As String = "ERR_LANGUAGE_NOT_FOUND"
    Private Const SALUTATION_NOT_FOUND As String = "ERR_SALUTATION_NOT_FOUND"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetSalutationsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetSalutationsDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GetSalutationsDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetSalutations Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetSalutationsDs)
        Try
            If ds.GetSalutations.Count = 0 Then Exit Sub
            With ds.GetSalutations.Item(0)
                If Not .Islanguage_codeNull Then Me.LanguageCode = .language_code
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetSalutations Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property LanguageCode() As String
        Get
            If Row(Me.DATA_COL_NAME_LANGUAGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_LANGUAGE_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_LANGUAGE_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim salutationDV As DataView
        Dim objSalutationDS As DataSet

        'if the country code was not provided, get it from the user object.
        If Me.LanguageCode Is Nothing OrElse Me.LanguageCode.Equals(String.Empty) Then
            Dim objLanguage As Language = New Language(Authentication.LangId)
            Me.LanguageCode = objLanguage.Code
        End If
        Dim objLanguageDV As DataView = Language.getList(Me.LanguageCode, Nothing, Nothing, Nothing)
        Try
            Me.Validate()

            If objLanguageDV Is Nothing Then
                Throw New BOValidationException("GetSalutations Error: ", ERROR_ACCESSING_DATABASE)
            ElseIf objLanguageDV.Count <> 1 Then
                Throw New BOValidationException("GetSalutations Error: ", LANGUAGE_NOT_FOUND)
            Else
                Dim language_id As New Guid(CType(objLanguageDV.Table.Rows(0).Item(Me.COL_NAME_LANGUAGE_ID), Byte()))

                salutationDV = LookupListNew.GetSalutationLanguageLookupList(language_id)

                objSalutationDS = salutationDV.Table.DataSet.Copy()


                If objSalutationDS Is Nothing Then
                    Throw New BOValidationException("GetSalutations Error: ", ERROR_ACCESSING_DATABASE)
                ElseIf objSalutationDS.Tables.Count > 0 AndAlso objSalutationDS.Tables(0).Rows.Count > 0 Then
                    objSalutationDS.Tables(0).Columns.Remove(Me.COL_NAME_SALUTATION_ID)
                    objSalutationDS.Tables(0).Columns.Remove(DALBase.SYSTEM_SEQUENCE_COL_NAME)
                    objSalutationDS.Tables(0).TableName = OUTPUT_TABLE_NAME
                    Return (XMLHelper.FromDatasetToXML(objSalutationDS))
                ElseIf objSalutationDS.Tables.Count > 0 AndAlso objSalutationDS.Tables(0).Rows.Count = 0 Then
                    Throw New BOValidationException("GetSalutations Error: ", SALUTATION_NOT_FOUND)
                End If
            End If
            objSalutationDS.Tables(0).Columns.RemoveAt(1)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/24/2004)  ********************
Public Class CountryPostalCodeFormat
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_COUNTRY_POSTALCODE_FORMAT_ID As String = "country_postal_format_id"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CountryPostalCodeFormatDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CountryPostalCodeFormatDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CountryPostalCodeFormatDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_POSTAL_FORMAT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PostalCodeFormatId() As Guid
        Get
            CheckDeleted()
            If Row(CountryPostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryPostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CountryPostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CountryPostalCodeFormatDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then Me.Load(Me.Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "List Methods"
    Public Shared Sub LoadList(ByVal ds As Dataset, ByVal countryID As Guid, ByVal reloadData As Boolean)
        Try
            If reloadData Then
                Dim tableIdx As Integer = ds.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME)
                If tableIdx <> -1 Then
                    ds.Tables.Remove(CountryPostalCodeFormatDAL.TABLE_NAME)
                End If
            End If
            Dim cntryPostalFormatDAL As New CountryPostalCodeFormatDAL
            If ds.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME) < 0 Then
                cntryPostalFormatDAL.LoadList(ds, countryID)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Shared Function Find(ByVal ds As Dataset, ByVal countryID As Guid, ByVal postalCodeID As Guid) As CountryPostalCodeFormat
        Dim i As Integer
        For i = 0 To ds.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).Rows.Count - 1
            Dim row As DataRow = ds.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).Rows(i)
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim couPosCode As New CountryPostalCodeFormat(row)
                If couPosCode.CountryId.Equals(countryID) AndAlso couPosCode.PostalCodeFormatId.Equals(postalCodeID) Then
                    Return couPosCode
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function Find(ByVal ds As Dataset, ByVal countryID As Guid) As CountryPostalCodeFormat
        Dim row As DataRow = FindRow(countryID, CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_ID, ds.Tables(CountryPostalCodeFormatDAL.TABLE_NAME))
        If row Is Nothing Then
            Return Nothing
        Else
            Return New CountryPostalCodeFormat(row)
        End If
    End Function

    Public Shared Function GetSelectedPostalCodeFormat(ByVal ds As Dataset, ByVal countryID As Guid) As DataView
        Return GetFilteredPostalCodeFormat(ds, countryID, True)
    End Function

    Public Shared Function GetAvailablePostalCodeFormat(ByVal ds As Dataset, ByVal countryID As Guid) As DataView
        Return GetFilteredPostalCodeFormat(ds, countryID, False)
    End Function

    Protected Shared Function GetFilteredPostalCodeFormat(ByVal ds As Dataset, ByVal countryID As Guid, ByVal isFilterInclusive As Boolean) As DataView
        Dim i As Integer
        Dim dv As New DataView(LookupListNew.GetPostalCodeFormatLookupList().Table)
        Dim inClause As String = "-1"
        For i = 0 To ds.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).Rows.Count - 1
            Dim row As DataRow = ds.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).Rows(i)
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim pCode As New CountryPostalCodeFormat(row)
                If pCode.CountryId.Equals(countryID) Then
                    inClause &= "," & LookupListNew.GetSequenceFromId(dv, pCode.PostalCodeFormatId)
                End If
            End If
        Next
        Dim rowFilter As String = dv.RowFilter
        If Not rowFilter Is Nothing AndAlso dv.RowFilter.Trim.Length > 0 Then
            rowFilter = "(" & rowFilter & ") AND"
        Else
            rowFilter = ""
        End If
        rowFilter &= BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN (" & inClause & ")"
        Else
            rowFilter &= " NOT IN (" & inClause & ")"
        End If
        dv.RowFilter = rowFilter
        Return dv
    End Function

    Public Shared Sub LoadFormatList(ByVal ds As Dataset, ByVal countryID As Guid)
        Try
            Dim cntryPostalFormatDAL As New CountryPostalCodeFormatDAL
            If ds.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME) < 0 Then
                cntryPostalFormatDAL.LoadFormatList(ds, countryID)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Shared Function IsValidFormat(ByVal countryID As Guid, ByVal postalCode As String, Optional ByVal reformatFlag As Boolean = False) As PostalCodeFormatResult
        Dim retPostalCodeFormatResult As PostalCodeFormatResult = New PostalCodeFormatResult
        retPostalCodeFormatResult.IsValid = False
        retPostalCodeFormatResult.PostalCode = postalCode
        retPostalCodeFormatResult.ErrorMessage = Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POSTALCODEFORMAT_ERR

        Try
            Dim ds As Dataset = New Dataset
            Dim cntryPostalFormatDAL As New CountryPostalCodeFormatDAL

            cntryPostalFormatDAL.LoadFormatList(ds, countryID)

            If ds.Tables.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim pFormat As String = CType(row("FORMAT"), String)
                    Dim reformatInputFlag As Boolean = reformatFlag And (CType(row("REFORMAT_FILE_INPUT_FLAG"), String) = "Y")

                    Dim regExpValidator As RegExValidator = New RegExValidator(pFormat, postalCode, reformatInputFlag)
                    If regExpValidator.IsValid Then
                        retPostalCodeFormatResult.IsValid = True
                        retPostalCodeFormatResult.PostalCode = postalCode
                        retPostalCodeFormatResult.ReformattedPostalCode = regExpValidator.ReformattedString
                        retPostalCodeFormatResult.LocatorLength = CType(row("LOCATOR_LENGTH"), Integer)
                        retPostalCodeFormatResult.LocatorStart = CType(row("LOCATOR_START_POSITION"), Integer)
                        retPostalCodeFormatResult.ComunaEnabled = CType(row("COMUNA_ENABLED"), String)
                        retPostalCodeFormatResult.ErrorMessage = ""
                        Exit For
                    End If
                Next
            End If

            Return retPostalCodeFormatResult
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

#End Region


#Region "DataView Retrieveing Methods"

#End Region

End Class



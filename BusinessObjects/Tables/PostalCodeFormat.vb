'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/23/2004)  ********************

Public Class PostalCodeFormat
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PostalCodeFormatDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize(True)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New PostalCodeFormatDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
            Initialize(False)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    Private _regularExpression As RegularExpression
    'Initialization code for new objects
    Private Sub Initialize(blnNew As Boolean)
        If _regularExpression Is Nothing Then
            If blnNew Then
                _regularExpression = New RegularExpression(Dataset)
            Else
                _regularExpression = New RegularExpression(RegularExpressionId, Dataset)
            End If
            RegularExpressionId = _regularExpression.Id
        End If
    End Sub

#End Region

#Region "Constants"

    Public Const COL_NAME_COMUNA_ENABLED As String = PostalCodeFormatDAL.COL_NAME_COMUNA_ENABLED
#End Region
#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(PostalCodeFormatDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(PostalCodeFormatDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PostalCodeFormatDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PostalCodeFormatDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidLocatorStart("")> _
    Public Property LocatorStartPosition As LongType
        Get
            CheckDeleted()
            If row(PostalCodeFormatDAL.COL_NAME_LOCATOR_START_POSITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(PostalCodeFormatDAL.COL_NAME_LOCATOR_START_POSITION), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PostalCodeFormatDAL.COL_NAME_LOCATOR_START_POSITION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidLocatorLength("")> _
    Public Property LocatorLength As LongType
        Get
            CheckDeleted()
            If row(PostalCodeFormatDAL.COL_NAME_LOCATOR_LENGTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(PostalCodeFormatDAL.COL_NAME_LOCATOR_LENGTH), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PostalCodeFormatDAL.COL_NAME_LOCATOR_LENGTH, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ReformatFileInputFlag As Boolean
        Get
            CheckDeleted()
            If row(PostalCodeFormatDAL.COL_NAME_REFORMAT_FILE_INPUT_FLAG) Is DBNull.Value Then
                Return False
            Else
                Return CType(row(PostalCodeFormatDAL.COL_NAME_REFORMAT_FILE_INPUT_FLAG), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(PostalCodeFormatDAL.COL_NAME_REFORMAT_FILE_INPUT_FLAG, "Y")
            Else
                SetValue(PostalCodeFormatDAL.COL_NAME_REFORMAT_FILE_INPUT_FLAG, "N")
            End If
        End Set
    End Property
    Public Property ComunaEnabled As Boolean
        Get
            CheckDeleted()
            If Row(PostalCodeFormatDAL.COL_NAME_COMUNA_ENABLED) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(PostalCodeFormatDAL.COL_NAME_COMUNA_ENABLED), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(PostalCodeFormatDAL.COL_NAME_COMUNA_ENABLED, "Y")
            Else
                SetValue(PostalCodeFormatDAL.COL_NAME_COMUNA_ENABLED, "N")
            End If
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RegularExpressionId As Guid
        Get
            CheckDeleted()
            If Row(PostalCodeFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PostalCodeFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PostalCodeFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID, Value)
        End Set
    End Property

    Public Function RegularExpressionBO() As RegularExpression

        If _regularExpression Is Nothing Then
            _regularExpression = New RegularExpression(RegularExpressionId)
        End If
        Return _regularExpression

    End Function

#End Region

#Region "Custom Validation"
    Private Shared Function GetRegExLength(regexformat As String) As Integer
        Dim tempLen As String = ""
        Dim totalLen As Integer = 0
        Dim startPos As Integer = 0
        Dim endPos As Integer = 0

        Do
            tempLen = ""
            startPos = regexformat.IndexOf("{", endPos)
            If startPos = -1 Then
                Exit Do
            End If
            endPos = regexformat.IndexOf("}", startPos)
            ' actually its an error
            If endPos = -1 Then
                Exit Do
            End If
            tempLen = regexformat.Substring(startPos + 1, endPos - startPos - 1)
            totalLen = totalLen + CType(tempLen, Integer)
        Loop

        Return totalLen
    End Function


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidLocatorStartAttribute
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_LOCATORSTARTPOSITION_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PostalCodeFormat = CType(objectToValidate, PostalCodeFormat)

            If obj.RegularExpressionBO.Format Is Nothing Then
                Return False
            End If

            If obj.LocatorStartPosition Is Nothing Then
                Return False
            Else
                If (obj.LocatorStartPosition.Value < 1) OrElse (obj.LocatorStartPosition.Value > GetRegExLength(obj.RegularExpressionBO.Format.Trim())) AndAlso Not obj.ComunaEnabled Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
      Public NotInheritable Class ValidLocatorLengthAttribute
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_LOCATORLENGTH_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PostalCodeFormat = CType(objectToValidate, PostalCodeFormat)
            If obj.ComunaEnabled Then
                Return True
            End If
            If obj.RegularExpressionBO.Format Is Nothing Then
                Return False
            End If
            If obj.LocatorStartPosition Is Nothing Then
                Return False
            End If
            If obj.LocatorLength Is Nothing Then
                Return False
            End If

            If (obj.LocatorLength.Value < 0) OrElse (obj.LocatorLength.Value > (GetRegExLength(obj.RegularExpressionBO.Format.Trim()) - obj.LocatorStartPosition.Value + 1)) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

#End Region


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If IsFamilyDirty Then RegularExpressionBO.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PostalCodeFormatDAL
                dal.UpdateFamily(Dataset)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(descriptionMask As String) As PostalCodeFormatDV
        Try
            Dim dal As New PostalCodeFormatDAL
            Return New PostalCodeFormatDV(dal.LoadList(descriptionMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "PostalCodeFormatDV"
    Public Class PostalCodeFormatDV
        Inherits DataView

#Region "Constants"
        Public Const COL_POSTALCODE_FORMAT_ID As String = "postal_code_format_id"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_FORMAT As String = "format"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#End Region

End Class




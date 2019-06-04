'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/16/2005)  ********************

Public Class CertEndorseCov
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
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
            Dim dal As New CertEndorseCovDAL
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
            Dim dal As New CertEndorseCovDAL
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
            If row(CertEndorseCovDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertEndorseCovDAL.COL_NAME_CERT_ENDORSE_COV_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertEndorseId() As Guid
        Get
            CheckDeleted()
            If row(CertEndorseCovDAL.COL_NAME_CERT_ENDORSE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertEndorseCovDAL.COL_NAME_CERT_ENDORSE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_CERT_ENDORSE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If row(CertEndorseCovDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertEndorseCovDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BeginDatePre() As DateType
        Get
            CheckDeleted()
            If row(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_PRE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_PRE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BeginDatePost() As DateType
        Get
            CheckDeleted()
            If row(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EndDatePre() As DateType
        Get
            CheckDeleted()
            If row(CertEndorseCovDAL.COL_NAME_END_DATE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertEndorseCovDAL.COL_NAME_END_DATE_PRE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_END_DATE_PRE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidTerm("")> _
    Public Property EndDatePost() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseCovDAL.COL_NAME_END_DATE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertEndorseCovDAL.COL_NAME_END_DATE_POST), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseCovDAL.COL_NAME_END_DATE_POST, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertEndorseCovDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub IsCertEndorsable(ByVal ds As DataSet, ByVal certId As Guid, ByVal beginDatePost As Date, ByVal endDatePost As Date)
        Try
            Dim dal As New CertEndorseCovDAL
            dal.IsCertEndorsable(ds, certId, beginDatePost, endDatePost)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "List Methods"
    Public Class CertEndorsementCovCollection
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As CertEndorse)
            MyBase.New(parent.Dataset.Tables(CertEndorseDAL.TABLE_NAME), GetType(CertEndorseCov), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            CType(bo, CertEndorseCov).CertEndorseId = CType(Parent, CertEndorse).CertEndorseId
        End Function
    End Class

    Public Shared Sub LoadListIntoParentFamily(ByVal parent As CertEndorse)
        Dim dal As New CertEndorseCovDAL
        dal.LoadList(parent.Dataset, parent.CertEndorseId)
    End Sub
#End Region

#Region "List Methods"
    Public Class EndCovList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As BusinessObjectBase)
            MyBase.New(parent.Dataset.Tables(CertEndorseCovDAL.TABLE_NAME), GetType(CertEndorseCov), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function
    End Class
    Public Shared Function GetEndorsementCovListForEndorsement(ByVal endoseId As Guid, ByVal parent As BusinessObjectBase) As EndCovList
        If parent.Dataset.Tables.IndexOf(CertItemCoverageDAL.TABLE_NAME) < 0 Then
            Dim dal As New CertEndorseCovDAL
            dal.LoadList(parent.Dataset, endoseId)
        End If
        Return New EndCovList(parent)
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    'Public Shared Function GetClaims(ByVal certEndorseId As Guid) As DataView
    '    Try
    '        Dim dal As New CertEndorseCovDAL
    '        Dim ds As New Dataset

    '        ds = dal.LoadList(certEndorseId)
    '        Return ds.Tables(CertEndorseCovDAL.TABLE_NAME).DefaultView

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function

    Public Shared Function GetEndorsementCoverages(ByVal certEndoseId As Guid) As CertEndorsementCoverageSearchDV
        Try
            Dim dal As New CertEndorseCovDAL
            Dim ds As Dataset
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertEndorsementCoverageSearchDV(dal.LoadList(certEndoseId, langId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class CertEndorsementCoverageSearchDV
        Inherits DataView

#Region "Constants"

        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION As String = "coverage_type_description"
        Public Const GRID_COL_COVERAGE_BEGIN_DATE_PRE As String = "begin_date_pre"
        Public Const GRID_COL_COVERAGE_BEGIN_DATE_POST As String = "begin_date_post"
        Public Const GRID_COL_COVERAGE_END_DATE_PRE As String = "end_date_pre"
        Public Const GRID_COL_COVERAGE_END_DATE_POST As String = "end_date_post"
        Public Const GRID_COL_TERM_PRE As String = "term_pre"
        Public Const GRID_COL_TERM_POST As String = "term_post"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidTerm
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_WILL_CAURSE_AN_INVALID_ESC_COVERAGE_TERM)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorseCov = CType(objectToValidate, CertEndorseCov)

            Dim MftCovID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COVERAGE_TYPES, Codes.COVERAGE_TYPE__MANUFACTURER)

            If MftCovID.Equals(obj.CoverageTypeId) Then 'Only check for non-mft coverage
                Return True
            End If

            Dim newBeginDate As Date = obj.BeginDatePost.Value
            Dim newEndDate As Date = obj.EndDatePost.Value.AddDays(1)

            If Not obj.IsDeleted Then 'Edit or add new
                ' the new term must greater than 0 month
                If newBeginDate.AddMonths(1) >= newEndDate Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class
#End Region
End Class




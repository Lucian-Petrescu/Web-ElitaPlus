'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/18/2005)  ********************

Public Class PartsDescription
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PartsDescriptionDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New PartsDescriptionDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
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
            If row(PartsDescriptionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PartsDescriptionDAL.COL_NAME_PARTS_DESCRIPTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(PartsDescriptionDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PartsDescriptionDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(PartsDescriptionDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskGroupId() As Guid
        Get
            CheckDeleted()
            If Row(PartsDescriptionDAL.COL_NAME_RISK_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PartsDescriptionDAL.COL_NAME_RISK_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(PartsDescriptionDAL.COL_NAME_RISK_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(PartsDescriptionDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PartsDescriptionDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(PartsDescriptionDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=60), EnglishDescriptionValidator("")> _
    Public Property DescriptionEnglish() As String
        Get
            CheckDeleted()
            If Row(PartsDescriptionDAL.COL_NAME_DESCRIPTION_ENGLISH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PartsDescriptionDAL.COL_NAME_DESCRIPTION_ENGLISH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(PartsDescriptionDAL.COL_NAME_DESCRIPTION_ENGLISH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30), PartCodeValidator("")> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(PartsDescriptionDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PartsDescriptionDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(PartsDescriptionDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    Public Shared ReadOnly Property NextCode() As String
        Get
            Dim dalParts As PartsDescriptionDAL = New PartsDescriptionDAL
            Return dalParts.GetNextCode(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PartsDescriptionDAL

                'If Me.Row.RowState = DataRowState.Added Then
                '    Me.Row(PartsDescriptionDAL.COL_NAME_CODE) = NextCode()
                'End If

                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal riskGroupID As Guid, ByVal description As String) As PartsDescriptionDV

        Try
            Dim dal As New PartsDescriptionDAL


            If (Not (description Is Nothing)) Then
                description = description.ToUpper
            End If


            Return New PartsDescriptionDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, riskGroupID, description).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getListForWS(ByVal riskGroupID As Guid) As DataTable

        Try
            Dim dal As New PartsDescriptionDAL

            Return (dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, riskGroupID, Nothing).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function getAssignedList() As PartsDescriptionDV

        Try
            Dim dal As New PartsDescriptionDAL

            Return New PartsDescriptionDV(dal.LoadAssignedList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function IsValidCode(ByVal code As String) As Boolean
        Dim dalParts As PartsDescriptionDAL = New PartsDescriptionDAL
        Return dalParts.IsValidCode(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, code)
    End Function

    Public Shared Function GetPartDescriptionByCode(ByVal code As String) As Guid
        Dim dalParts As PartsDescriptionDAL = New PartsDescriptionDAL
        Return dalParts.GetPartDescriptionByCode(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, code)
    End Function
    Public Shared Function GetPartDescriptionByCode(ByVal code As String, ByVal ClaimId As Guid) As Guid
        Dim dalParts As PartsDescriptionDAL = New PartsDescriptionDAL
        Return dalParts.GetPartDescriptionByCode(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, code, ClaimId)
    End Function

    Public Class PartsDescriptionDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PARTS_DESCRIPTION_ID As String = "parts_description_id"
        Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_NAME_RISK_GROUP_ID As String = "risk_group_id"
        Public Const COL_NAME_DESCRIPTION As String = "description"
        Public Const COL_NAME_DESCRIPTION_ENGLISH As String = "description_english"
        Public Const COL_NAME_RISK_GROUP As String = "risk_group"
        Public Const COL_NAME_CODE As String = "code"

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PartsDescID(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PARTS_DESCRIPTION_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property RiskGroup(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_RISK_GROUP).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property RiskGroupID(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_RISK_GROUP_ID), Byte()))
            End Get
        End Property

    End Class

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal partsDescId As Guid, ByVal riskGroupID As Guid, ByVal companyGrpID As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(PartsDescriptionDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(PartsDescriptionDAL.COL_NAME_DESCRIPTION_ENGLISH) = String.Empty
        row(PartsDescriptionDAL.COL_NAME_PARTS_DESCRIPTION_ID) = partsDescId.ToByteArray
        row(PartsDescriptionDAL.COL_NAME_RISK_GROUP_ID) = riskGroupID.ToByteArray
        row(PartsDescriptionDAL.COL_NAME_COMPANY_GROUP_ID) = companyGrpID.ToByteArray
        row(PartsDescriptionDAL.COL_NAME_CODE) = String.Empty

        dt.Rows.Add(row)

        Return (dv)

    End Function
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class PartCodeValidator
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PART_CODE_ALREADY_EXIST)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PartsDescription = CType(objectToValidate, PartsDescription)
            Dim dal As New PartsDescriptionDAL

            If (Not obj.Code Is Nothing) AndAlso (obj.Code.Trim <> String.Empty) Then

                If Not dal.IsPartCodeUnique(obj.RiskGroupId, obj.CompanyGroupId, obj.Code.Trim) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class EnglishDescriptionValidator
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ENGLISH_DESCRIPTION_ALREADY_EXIST)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PartsDescription = CType(objectToValidate, PartsDescription)
            Dim dal As New PartsDescriptionDAL

            If (Not obj.DescriptionEnglish Is Nothing) AndAlso (obj.DescriptionEnglish.Trim <> String.Empty) Then

                If Not dal.IsEnglishDescriptionUnique(obj.RiskGroupId, obj.CompanyGroupId, obj.DescriptionEnglish.Trim) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class


#End Region

End Class




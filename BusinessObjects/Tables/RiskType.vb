''/*-----------------------------------------------------------------------------------------------------------------

''  AA      SSS  SSS  UU  UU RRRRR      AA    NN   NN  TTTTTTTT
''A    A   SS    SS   UU  UU RR   RR  A    A  NNN  NN     TT 
''AAAAAA   SSS   SSS  UU  UU RRRR     AAAAAA  NN N NN     TT
''AA  AA     SS    SS UU  UU RR RR    AA  AA  NN  NNN     TT
''AA  AA  SSSSS SSSSS  UUUU  RR   RR  AA  AA  NN  NNN     TT

''Copyright 2004, Assurant Group Inc..  All Rights Reserved.
''------------------------------------------------------------------------------
''This information is CONFIDENTIAL and for Assurant Group's exclusive use ONLY.
''Any reproduction or use without Assurant Group's explicit, written consent 
''is PROHIBITED.
''------------------------------------------------------------------------------

''Purpose: This Class is responsible for interacting with the RiskTypeDAL class.
''
''Author/s:  Ravi Chillikatil, Rosalba Monterrosas
''
''Date:    06/30/2004

''MODIFICATION HISTORY:
''
''===========================================================================================

Imports Assurant.ElitaPlus.DALObjects

Public Class RiskType
    Inherits BusinessObjectBase

#Region " Constructors "

    Public Sub New()

        MyBase.New()
        Dataset = New Dataset
        Load()

    End Sub

    Public Sub New(ByVal id As Guid)

        MyBase.New()
        Dataset = New Dataset
        Load(id)

    End Sub
    'Existing BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
        Dim dal As RiskTypeDAL = New RiskTypeDAL
        Dim ds As Dataset = New Dataset

        Dataset = dal.LoadSchema(ds)

        Dim newRow As DataRow = Dataset.Tables(dal.RISK_TYPE_TABLE_NAME).NewRow
        Dataset.Tables(dal.RISK_TYPE_TABLE_NAME).Rows.Add(newRow)
        Row = newRow
        setvalue(dal.RISK_TYPE_ID_COL, Guid.NewGuid)
    End Sub

    'Call this method from the Constructor for Update and Delete
    Protected Sub Load(ByVal id As Guid)

        'Dim dal As RiskTypeDAL = New RiskTypeDAL
        'Dim ds As Dataset = New Dataset

        'Me.Dataset = dal.Load(ds, id)
        'Me.Row = Me.FindRow(id, dal.RISK_TYPE_ID_COL, Me.Dataset.Tables(dal.RISK_TYPE_TABLE_NAME))

        Try
            Dim dal As New RiskTypeDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.RISK_TYPE_TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Not Dataset Is Nothing Then
                If Dataset.Tables.IndexOf(dal.RISK_TYPE_TABLE_NAME) >= 0 Then
                    Row = FindRow(id, dal.RISK_TYPE_ID_COL, Dataset.Tables(dal.RISK_TYPE_TABLE_NAME))
                End If
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.RISK_TYPE_ID_COL, Dataset.Tables(dal.RISK_TYPE_TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub



#End Region

#Region "Constants"

    'Private Const RISK_TYPE_TABLE_NAME As String = "RISK_TYPE"
    Public Const RISK_GROUP_ID_COL As String = RiskTypeDAL.RISK_TYPE_ID_COL
    Public Const PRODUCT_TAX_TYPE As String = "Product_Tax_Type"
    Public Const RISK_GROUP As String = "Risk_Group"
    'Public Const COMPANY_ID_COL As String = RiskTypeDAL.COMPANY_ID_COL
    Public Const COMPANY_GROUP_ID_COL As String = RiskTypeDAL.COMPANY_GROUP_ID_COL
    Public Const DESCRIPTION_COL As String = RiskTypeDAL.DESCRIPTION_COL
    Public Const RISK_TYPE_ID_COL As String = RiskTypeDAL.RISK_TYPE_ID_COL
    Public Const RISK_TYPE_ENGLISH_COL As String = RiskTypeDAL.RISK_TYPE_ENGLISH_COL
    Public Const LANGUAGE_ID_COL As String = RiskTypeDAL.LANGUAGE_ID_COL

    'Private Const BO_NAME As String = "RiskType"

#End Region

#Region "BO Properties"


    <ValueMandatory("")> _
    Public Property RiskTypeId As Guid
        Get
            If Row(RiskTypeDAL.RISK_TYPE_ID_COL) Is DBNull.Value Then Return Nothing
            Return New Guid(CType(row(RiskTypeDAL.RISK_TYPE_ID_COL), Byte()))
        End Get
        Set
            If Not IsNew Then
                Throw New BOInvalidOperationException("A new Risk Type Id can only be assigned to a new RiskType")
            End If
            SetValue(RiskTypeDAL.RISK_TYPE_ID_COL, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", max:=60)> _
    Public Property Description As String
        Get
            If Row(RiskTypeDAL.DESCRIPTION_COL) Is DBNull.Value Then Return Nothing
            Return Row(RiskTypeDAL.DESCRIPTION_COL)
        End Get
        Set
            SetValue(RiskTypeDAL.DESCRIPTION_COL, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", max:=60)> _
    Public Property RiskTypeEnglish As String
        Get
            If Row(RiskTypeDAL.RISK_TYPE_ENGLISH_COL) Is DBNull.Value Then Return Nothing
            Return Row(RiskTypeDAL.RISK_TYPE_ENGLISH_COL)
        End Get
        Set
            SetValue(RiskTypeDAL.RISK_TYPE_ENGLISH_COL, value)
        End Set
    End Property

    '<ValueMandatory("")> _
    'Public Property CompanyId() As Guid
    '    Get
    '        If Row(RiskTypeDAL.COMPANY_ID_COL) Is DBNull.Value Then Return Nothing
    '        Return New Guid(CType(row(RiskTypeDAL.COMPANY_ID_COL), Byte()))
    '    End Get
    '    Set(ByVal value As Guid)
    '        Me.SetValue(RiskTypeDAL.COMPANY_ID_COL, value)
    '    End Set
    'End Property
    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            If Row(RiskTypeDAL.COMPANY_GROUP_ID_COL) Is DBNull.Value Then Return Nothing
            Return New Guid(CType(Row(RiskTypeDAL.COMPANY_GROUP_ID_COL), Byte()))
        End Get
        Set
            SetValue(RiskTypeDAL.COMPANY_GROUP_ID_COL, value)
        End Set
    End Property



    <ValueMandatory("")> _
    Public Property ProductTaxTypeId As Guid
        Get
            If Row(RiskTypeDAL.COL_NAME_PRODUCT_TAX_TYPE_ID) Is DBNull.Value Then Return Nothing
            Return New Guid(CType(Row(RiskTypeDAL.COL_NAME_PRODUCT_TAX_TYPE_ID), Byte()))
        End Get
        Set
            SetValue(RiskTypeDAL.COL_NAME_PRODUCT_TAX_TYPE_ID, value)
        End Set

    End Property

    Public Property SoftQuestionGroupId As Guid
        Get
            If Row(RiskTypeDAL.COL_NAME_SOFT_QUESTION_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RiskTypeDAL.COL_NAME_SOFT_QUESTION_GROUP_ID), Byte()))
            End If
        End Get
        Set
            SetValue(RiskTypeDAL.COL_NAME_SOFT_QUESTION_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RiskGroupId As Guid
        Get
            If Row(RiskTypeDAL.RISK_GROUP_ID_COL) Is DBNull.Value Then Return Nothing
            Return New Guid(CType(Row(RiskTypeDAL.RISK_GROUP_ID_COL), Byte()))
        End Get
        Set
            SetValue(RiskTypeDAL.RISK_GROUP_ID_COL, value)
        End Set

    End Property

#End Region

#Region "Public Methods"

    'Public Overrides Sub Save()
    '    'do validation and set Audit info
    '    MyBase.Save()

    '    Dim dal As New RiskTypeDAL
    '    dal.Update(MyBase.Dataset)
    '    'Reload the Data
    '    If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
    '        'Reload the Data from the DB
    '        Me.Load(Me.RiskTypeId)
    '    End If
    'End Sub
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RiskTypeDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = RiskTypeId
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

    '#Region "LoadRow"

    '    Public Function LoadSavedRow(ByVal id As Guid) As DataView

    '        Dim dal As RiskTypeDAL = New RiskTypeDAL

    '        Me.Dataset = dal.Load(Me.Dataset, id)
    '        Me.Save()
    '        Return Me.Dataset.Tables(RiskTypeDAL.RISK_TYPE_TABLE_NAME).DefaultView

    '    End Function

    '#End Region

#Region "List Methods"

    Public Shared Function GetRiskTypeList(ByVal descriptionMask As String, ByVal riskTypeEnglishMask As String, _
                                           ByVal riskGroupIdForSearch As Guid, ByVal languageId As Guid, _
                                           ByVal CompanyGroupId As Guid) As DataView
        Try
            Dim dal As New RiskTypeDAL
            Dim ds As Dataset

            ds = dal.GetRiskTypeList(descriptionMask, riskTypeEnglishMask, _
                                     riskGroupIdForSearch, languageId, CompanyGroupId)
            Return ds.Tables(RiskTypeDAL.RISK_TYPE_LIST).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(RiskTypeDAL.DESCRIPTION_COL) = String.Empty
        row(RiskTypeDAL.RISK_TYPE_ENGLISH_COL) = String.Empty
        row(RISK_GROUP) = String.Empty
        row(RiskTypeDAL.RISK_TYPE_ID_COL) = id.ToByteArray
        row(RiskTypeDAL.COL_NAME_SOFT_QUESTION_GROUP_ID) = Guid.Empty.ToByteArray


        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Shared Function Is_TaxByProductType_Yes() As Boolean
        Dim countryIds As ArrayList
        Dim isYes As Boolean
        Dim dal As New RiskTypeDAL

        countryIds = Authentication.CountryIds
        isYes = dal.Is_TaxByProductType_Yes(countryIds)
        Return isYes
    End Function
#End Region

End Class


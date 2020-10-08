'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/28/2006)  ********************

Public Class MfgStandardization
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New MfgStandardizationDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New MfgStandardizationDAL
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

    Public Const COL_NAME_DESCRIPTION As String = MfgStandardizationDAL.COL_NAME_DESCRIPTION
    Public Const COL_NAME_Mfg As String = MfgStandardizationDAL.COL_NAME_Mfg
    Public Const RISK_GROUP As String = "Risk_Group"
    'not being used
    'Public Const COMPANY_ID_COL As String = RiskTypeDAL.COMPANY_ID_COL 
    Public Const DESCRIPTION_COL As String = RiskTypeDAL.DESCRIPTION_COL
    Public Const RISK_TYPE_ID_COL As String = RiskTypeDAL.RISK_TYPE_ID_COL
    Public Const RISK_TYPE_ENGLISH_COL As String = RiskTypeDAL.RISK_TYPE_ENGLISH_COL
    Public Const LANGUAGE_ID_COL As String = RiskTypeDAL.LANGUAGE_ID_COL



#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(MfgStandardizationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgStandardizationDAL.COL_NAME_Mfg_STANDARDIZATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(MfgStandardizationDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(MfgStandardizationDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(MfgStandardizationDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MfgId As Guid
        Get
            CheckDeleted()
            If Row(MfgStandardizationDAL.COL_NAME_Mfg_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgStandardizationDAL.COL_NAME_Mfg_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(MfgStandardizationDAL.COL_NAME_Mfg_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New MfgStandardizationDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
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
    Public Shared Function getEmptyList(dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(MfgStandardizationDAL.COL_NAME_MFG_STANDARDIZATION_ID) = System.Guid.NewGuid.ToByteArray
            row.Item(MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID) = System.Guid.NewGuid.ToByteArray

            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetMfgAliasList(descriptionMask As String, _
                                               MfgIdForSearch As Guid, _
                                               companygroupId As Guid) As DataView
        Try
            Dim dal As New MfgStandardizationDAL
            Dim ds As DataSet

            ds = dal.GetMfgAliasList(descriptionMask, MfgIdForSearch, _
                                     companygroupId)
            Return ds.Tables(MfgStandardizationDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView
        Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim companygroupId As Guid = company.CompanyGroupId
        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(MfgStandardizationDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(MfgStandardizationDAL.COL_NAME_MFG) = String.Empty
        row(MfgStandardizationDAL.COL_NAME_MFG_STANDARDIZATION_ID) = id.ToByteArray
        row(MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID) = companygroupId.ToByteArray


        dt.Rows.Add(row)

        Return (dv)

    End Function

#End Region

#Region "SearchDV"
    Public Class MfgStandardizationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MFG_STANDARDIZATION_ID As String = MfgStandardizationDAL.COL_NAME_MFG_STANDARDIZATION_ID
        Public Const COL_MFG As String = MfgStandardizationDAL.COL_NAME_MFG
        Public Const COL_MFG_ALIAS As String = MfgStandardizationDAL.COL_NAME_DESCRIPTION
        Public Const COL_COMPANY_GROUP_ID As String = MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_ID
        Public Const COL_COMPANY_GROUP_NAME As String = MfgStandardizationDAL.COL_NAME_COMPANY_GROUP_NAME
        Public Const COL_MANUFACTURER_ID As String = MfgStandardizationDAL.COL_NAME_MFG_ID
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region
End Class

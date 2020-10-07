'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/13/2004)  ********************

Public Class PriceGroup
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

    Protected Sub Load()
        Try
            Dim dal As New PriceGroupDAL
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
            Dim dal As New PriceGroupDAL
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

        CountryId = CType(ElitaPlusIdentity.Current.ActiveUser.Countries.Item(0), Guid)

    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(PriceGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PriceGroupDAL.COL_NAME_PRICE_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(PriceGroupDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceGroupDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property ShortDesc As String
        Get
            CheckDeleted()
            If Row(PriceGroupDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceGroupDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(PriceGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PriceGroupDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Dataset) 'New Code Added Manually
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As PriceGroup)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Price Group")
        End If
        MyBase.CopyFrom(original)
        'copy the childrens        
        Dim detail As PriceGroupDetail
        For Each detail In original.PriceGroupDetailChildren
            Dim newDetail As PriceGroupDetail = PriceGroupDetailChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.PriceGroupId = Id
            newDetail.Save()
        Next
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal searchCode As String, ByVal searchDesc As String, ByVal oCountryId As Guid) As PriceGroupSearchDV
        Try
            Dim dal As New PriceGroupDAL
            Dim oCountryIds As ArrayList

            If DALBase.IsNothing(oCountryId) Then
                ' Get All User Countries
                oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            Else
                oCountryIds = New ArrayList
                oCountryIds.Add(oCountryId)
            End If

            Return New PriceGroupSearchDV(dal.LoadList(oCountryIds, searchCode, searchDesc).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class PriceGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COUNTRY_DESC As String = PriceGroupDAL.COL_NAME_COUNTRY_DESC
        Public Const COL_NAME_DESCRIPTION As String = PriceGroupDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = PriceGroupDAL.COL_NAME_SHORT_DESC
        Public Const COL_NAME_PRICE_GROUP_ID As String = PriceGroupDAL.COL_NAME_PRICE_GROUP_ID
        Public Const COL_NAME_COUNTRY_DESC As String = PriceGroupDAL.COL_NAME_COUNTRY_DESC
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PriceGroupId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PRICE_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SHORT_DESC).ToString
            End Get
        End Property


    End Class

#End Region

#Region "Children Related"
    Public ReadOnly Property PriceGroupDetailChildren As PriceGroupDetailList
        Get
            Return New PriceGroupDetailList(Me)
        End Get
    End Property

    Public ReadOnly Property PriceGroupDetailChildren(ByVal riskTypeId As Guid) As PriceGroupDetailList
        Get
            Return New PriceGroupDetailList(Me, riskTypeId)
        End Get
    End Property
    Public ReadOnly Property PriceGroupDetailChildren(ByVal company_group_id As Guid, ByVal flag As Boolean) As PriceGroupDetailList
        Get
            Return New PriceGroupDetailList(Me, company_group_id, True)
        End Get
    End Property

    Public Function GetDetailSelectionView() As PriceGroupDetailSelectionView
        Dim t As DataTable = PriceGroupDetailSelectionView.CreateTable
        Dim detail As PriceGroupDetail
        For Each detail In PriceGroupDetailChildren
            Dim row As DataRow = t.NewRow
            row(PriceGroupDetailSelectionView.DETAIL_ID_COL_NAME) = detail.Id.ToByteArray
            row(PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME) = detail.EffectiveDate.Value
            row(PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME) = detail.RiskTypeDescription
            If detail.PriceBandRangeFrom Is Nothing Then
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = ""
            Else
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = detail.PriceBandRangeFrom.Value
            End If

            If detail.PriceBandRangeTo Is Nothing Then
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = ""
            Else
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = detail.PriceBandRangeTo.Value
            End If
            t.Rows.Add(row)
        Next
        Return New PriceGroupDetailSelectionView(t)
    End Function
    Public Function GetDetailSelectionView(ByVal company_group_id As Guid) As PriceGroupDetailSelectionView
        Dim t As DataTable = PriceGroupDetailSelectionView.CreateTable
        Dim detail As PriceGroupDetail
        For Each detail In PriceGroupDetailChildren(company_group_id, True)
            Dim row As DataRow = t.NewRow
            row(PriceGroupDetailSelectionView.DETAIL_ID_COL_NAME) = detail.Id.ToByteArray
            row(PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME) = detail.EffectiveDate.Value
            row(PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME) = detail.RiskTypeDescription
            If detail.PriceBandRangeFrom Is Nothing Then
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = ""
            Else
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME) = detail.PriceBandRangeFrom.Value
            End If
            If detail.PriceBandRangeTo Is Nothing Then
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = ""
            Else
                row(PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME) = detail.PriceBandRangeTo.Value
            End If

            t.Rows.Add(row)
        Next
        Return New PriceGroupDetailSelectionView(t)
    End Function

    Public Class PriceGroupDetailSelectionView
        Inherits DataView
        Public Const DETAIL_ID_COL_NAME As String = "DetailId"
        Public Const RISK_TYPE_COL_NAME As String = "RiskTypeDescription"
        Public Const EFFECTIVE_DATE_COL_NAME As String = "EffectiveDate"
        Public Const PRICE_BAND_RANGE_FROM_COL_NAME As String = "PriceBandRangeFrom"
        Public Const PRICE_BAND_RANGE_TO_COL_NAME As String = "PriceBandRangeTo"

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(DETAIL_ID_COL_NAME, GetType(Byte()))
            t.Columns.Add(RISK_TYPE_COL_NAME, GetType(String))
            t.Columns.Add(EFFECTIVE_DATE_COL_NAME, GetType(Date))
            t.Columns.Add(PRICE_BAND_RANGE_FROM_COL_NAME, GetType(String))
            t.Columns.Add(PRICE_BAND_RANGE_TO_COL_NAME, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetChild(ByVal childId As Guid) As PriceGroupDetail
        Return CType(PriceGroupDetailChildren.GetChild(childId), PriceGroupDetail)
    End Function

    Public Function GetNewChild() As PriceGroupDetail
        Dim newPgDetail As PriceGroupDetail = CType(PriceGroupDetailChildren.GetNewChild, PriceGroupDetail)
        newPgDetail.PriceGroupId = Id
        Return newPgDetail
    End Function

    Public Function GetCurrentPriceGroupDetail(ByVal riskTypeId As Guid, ByVal ProductPrice As Decimal) As PriceGroupDetail

        Dim pgDetailBO As PriceGroupDetail = Nothing

        '     Dim minDaysDifference As Integer = Integer.MaxValue

        Dim effectivePGDetailBO As PriceGroupDetail

        For Each pgDetailBO In PriceGroupDetailChildren(riskTypeId)

            '            If ((pgDetailBO.EffectiveDate.Value < Today) AndAlso _
            '               (Today.Subtract(pgDetailBO.EffectiveDate.Value).Days < minDaysDifference)) Then
            'This is the Closest Effective Date so far
            '          minDaysDifference = Today.Subtract(pgDetailBO.EffectiveDate.Value).Days
            If ProductPrice >= pgDetailBO.PriceBandRangeFrom.Value And ProductPrice <= pgDetailBO.PriceBandRangeTo.Value Then
                effectivePGDetailBO = pgDetailBO
            End If
        Next

        Return (effectivePGDetailBO)

    End Function

#End Region


End Class




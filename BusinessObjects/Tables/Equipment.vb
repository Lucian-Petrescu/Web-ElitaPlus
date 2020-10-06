Imports System.Collections.Generic

Public Class Equipment
    Inherits BusinessObjectBase
    Implements IAttributable

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
            Dim dal As New EquipmentDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            Dim noGuid As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, "N")
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_IS_MASTER_EQUIPMENT, noGuid)
            SetValue(dal.COL_NAME_REPAIRABLE_ID, noGuid)
            SetValue(dal.COL_NAME_EFFECTIVE, System.DateTime.Today)
            SetValue(dal.COL_NAME_EXPIRATION, EQUIPMENT_EXPIRATION_DEFAULT)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New EquipmentDAL
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

    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(EquipmentDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentDAL.COL_NAME_DESCRIPTION)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100), ValueMandatoryIfNotMasterModel(""), CheckDuplicate("")>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentDAL.COL_NAME_MODEL)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    Public Property MasterEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MASTER_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_MASTER_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_MASTER_EQUIPMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RepairableId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_REPAIRABLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_REPAIRABLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_REPAIRABLE_ID, Value)
        End Set
    End Property

    Public Property ManufacturerWarrenty() As LongType
        Get
            Dim manufacurerCoverage As MfgCoverage
            CheckDeleted()
            If (Not Row.Table.DataSet.Tables.Contains(MfgCoverageDAL.TABLE_NAME)) Then
                Try
                    manufacurerCoverage = New MfgCoverage(Row.Table.DataSet, Id)
                Catch ex As DataNotFoundException
                    manufacurerCoverage = New MfgCoverage(Row.Table.DataSet)
                    manufacurerCoverage.Delete()
                End Try
            End If
            If (Row.Table.DataSet.Tables(MfgCoverageDAL.TABLE_NAME).Rows.Count = 1) Then
                manufacurerCoverage = New MfgCoverage(Row.Table.DataSet.Tables(MfgCoverageDAL.TABLE_NAME).Rows(0))
                Return New LongType(manufacurerCoverage.MfgWarranty)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            Dim manufacurerCoverage As MfgCoverage
            If (Row.Table.DataSet.Tables.Contains(MfgCoverageDAL.TABLE_NAME)) AndAlso _
                (Row.Table.DataSet.Tables(MfgCoverageDAL.TABLE_NAME).Rows.Count = 1) Then
                manufacurerCoverage = New MfgCoverage(Row.Table.DataSet.Tables(MfgCoverageDAL.TABLE_NAME).Rows(0))
                If (value Is Nothing) Then
                    manufacurerCoverage.Delete()
                    manufacurerCoverage.Save()
                End If
            Else
                If (Not value Is Nothing) Then
                    manufacurerCoverage = New MfgCoverage(Row.Table.DataSet)
                End If
            End If
            If (Not value Is Nothing) Then
                manufacurerCoverage.BeginEdit()
                manufacurerCoverage.MfgWarranty = CInt(value.Value)
                manufacurerCoverage.EquipmentTypeId = EquipmentTypeId
                manufacurerCoverage.EquipmentId = Id
                manufacurerCoverage.Model = Model
                manufacurerCoverage.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                manufacurerCoverage.ManufacturerId = ManufacturerId
                manufacurerCoverage.MfgWarranty = CInt(value.Value)
                manufacurerCoverage.EndEdit()
            End If
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentClassId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_EQUIPMENT_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_EQUIPMENT_CLASS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_EQUIPMENT_CLASS_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID, Value)
        End Set
    End Property

    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(EquipmentDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(EquipmentDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsMasterEquipment() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_IS_MASTER_EQUIPMENT) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_IS_MASTER_EQUIPMENT), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_IS_MASTER_EQUIPMENT, Value)
        End Set
    End Property

    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return EquipmentDAL.TABLE_NAME
        End Get
    End Property


    Public Property Color() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_COLOR_XCD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentDAL.COL_NAME_COLOR_XCD_ID)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EquipmentDAL.COL_NAME_COLOR_XCD_ID, Value)
        End Set
    End Property

    Public Property Memory() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MEMORY_XCD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentDAL.COL_NAME_MEMORY_XCD_ID)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()            
            SetValue(EquipmentDAL.COL_NAME_MEMORY_XCD_ID, Value)
        End Set
     End Property
    
        Public Property Carrier() As String
            Get
                CheckDeleted()
                If Row(EquipmentDAL.COL_NAME_CARRIER_XCD_ID) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return Row(EquipmentDAL.COL_NAME_CARRIER_XCD_ID)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                SetValue(EquipmentDAL.COL_NAME_CARRIER_XCD_ID, Value)
            End Set
    
        End Property


#End Region

#Region "Constants"
    Friend Const EQUIPMENT_FORM001 As String = "EQUIPMENT_FORM001" ' Equipment Image Path is Duplicate.
    Friend Const EQUIPMENT_FORM002 As String = "EQUIPMENT_FORM002" ' Equipment Image Code is Duplicate.
    Friend Const EQUIPMENT_FORM003 As String = "EQUIPMENT_FORM003" ' Make and Model is Duplicate.

    Private ReadOnly EQUIPMENT_EXPIRATION_DEFAULT As New Date(2499, 12, 31, 23, 59, 59)
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached AndAlso Row.RowState <> DataRowState.Deleted Then
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
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As Equipment)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Equipment")
        End If
        MyBase.CopyFrom(original)
        'copy the childrens        
        For Each detail As EquipmentComment In original.EquipmentCommentChildren
            Dim newDetail As EquipmentComment = EquipmentCommentChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.EquipmentId = Id
            newDetail.Save()
        Next
        For Each detail As AttributeValue In original.AttributeValues
            Dim newDetail As AttributeValue = AttributeValues.GetNewAttributeChild()
            newDetail.Copy(detail)
            newDetail.ReferenceId = Id
            newDetail.Save()
        Next
        For Each detail As EquipmentImage In original.EquipmentImageChildren
            Dim newDetail As EquipmentImage = EquipmentImageChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.EquipmentId = Id
            newDetail.Save()
        Next
        For Each detail As RelatedEquipment In original.RelatedEquipmentChildren
            Dim newDetail As RelatedEquipment = RelatedEquipmentChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.EquipmentId = Id
            newDetail.Save()
        Next
    End Sub

    Public Shared Function FindEquipment(ByVal dealer As String, ByVal manufacturer As String, ByVal model As String, ByVal lookupDate As Date) As Guid
        Try
            Dim dal As New EquipmentDAL
            Return dal.FindEquipment(dealer, manufacturer, model, lookupDate)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Shared Function GetEquipmentClassIdByEquipmentId(ByVal equipmentId As Guid) As Guid
        Try
            Dim equipmentDAL As New EquipmentDAL
            Return equipmentDAL.GetEquipmentClassIdByEquipmentId(equipmentId)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal description As String, ByVal model As String, _
                                        ByVal manufacturerName As String, ByVal equipmentClassName As String, _
                                        ByVal equipmentTypeName As String, Optional ByVal sku As String = "", Optional ByVal EquipmentList_Code As String = "") As Equipment.EquipmentSearchDV
        Try
            Dim dal As New EquipmentDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If (Not (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) And String.IsNullOrEmpty(description.Trim)) Then
                description = description & DALBase.ASTERISK
            End If
            If (Not (model.Contains(DALBase.WILDCARD_CHAR) Or model.Contains(DALBase.ASTERISK)) And String.IsNullOrEmpty(model.Trim)) Then
                model = model & DALBase.ASTERISK
            End If
            If (Not (sku.Contains(DALBase.WILDCARD_CHAR) Or sku.Contains(DALBase.ASTERISK)) And String.IsNullOrEmpty(sku.Trim)) Then
                sku = sku & DALBase.ASTERISK
            End If
            Return New EquipmentSearchDV(dal.LoadList(description, model, manufacturerName, equipmentClassName, _
                equipmentTypeName, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, sku, EquipmentList_Code, DateTime.Now).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetEquipmentForBenefitsList() As Equipment.EquipmentSearchDV
        Try
            Dim dal As New EquipmentDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Return New EquipmentSearchDV(dal.LoadEquipmentForBenefitsList(oCompanyGroupIds, DateTime.Now, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Class EquipmentSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_EQUIPMENT_ID As String = EquipmentDAL.COL_NAME_EQUIPMENT_ID
        Public Const COL_NAME_DESCRIPTION As String = EquipmentDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_MODEL As String = EquipmentDAL.COL_NAME_MODEL
        Public Const COL_NAME_SKU As String = EquipmentDAL.COL_NAME_SKU
        Public Const COL_NAME_MANUFACTURER As String = EquipmentDAL.COL_NAME_MANUFACTURER
        Public Const COL_NAME_EQUIPMENT_CLASS As String = EquipmentDAL.COL_NAME_EQUIPMENT_CLASS
        Public Const COL_NAME_EQUIPMENT_TYPE As String = EquipmentDAL.COL_NAME_EQUIPMENT_TYPE
        Public Const COL_NAME_RISK_TYPE_ID As String = WarrantyMasterDAL.COL_NAME_RISK_TYPE_ID

        Public Const COL_NAME_COLOR_XCD_ID As String = EquipmentDAL.COL_NAME_COLOR_XCD_ID
        Public Const COL_NAME_MEMORY_XCD_ID As String = EquipmentDAL.COL_NAME_MEMORY_XCD_ID
        Public Const COL_NAME_CARRIER_XCD_ID As String = EquipmentDAL.COL_NAME_CARRIER_XCD_ID
        
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property EquipmentId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EQUIPMENT_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Model(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MODEL).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Manufacturer(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_MANUFACTURER), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property EquipmentClass(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EQUIPMENT_CLASS), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property EquipmentType(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EQUIPMENT_TYPE), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property sku(ByVal row) As String
            Get
                Return CType(row(COL_NAME_SKU), String)
            End Get
        End Property

        Public Shared ReadOnly Property color(ByVal row) As String
            Get
                Return CType(row(COL_NAME_COLOR_XCD_ID), String)
            End Get
        End Property

        Public Shared ReadOnly Property memory(ByVal row) As String
            Get
                Return CType(row(COL_NAME_MEMORY_XCD_ID), String)
            End Get
        End Property

        Public Shared ReadOnly Property carrier(ByVal row) As String
            Get
                Return CType(row(COL_NAME_CARRIER_XCD_ID), String)
            End Get
        End Property

    End Class

    Public Shared Function LoadEquipmentListForWS(ByVal EquipmentListCode As String, ByVal CompanyGroupId As Guid) As DataSet
        Try
            Dim dal As New EquipmentDAL
            Return dal.LoadEquipmentListForWS(EquipmentListCode, CompanyGroupId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "Children Related"

#Region "Images"
    Public ReadOnly Property EquipmentImageChildren() As EquipmentImage.EquipmentImageList
        Get
            Return New EquipmentImage.EquipmentImageList(Me)
        End Get
    End Property

    Public Function GetImageSelectionView() As EquipmentImageSelectionView
        Dim t As DataTable = EquipmentImageSelectionView.CreateTable
        Dim detail As EquipmentImage

        For Each detail In EquipmentImageChildren
            Dim row As DataRow = t.NewRow
            row(EquipmentImageSelectionView.COL_NAME_EQUIPMENT_COMMENT_ID) = detail.Id.ToByteArray()
            row(EquipmentImageSelectionView.COL_NAME_CODE) = detail.Code
            row(EquipmentImageSelectionView.COL_NAME_DESCRIPTION) = detail.Description
            row(EquipmentImageSelectionView.COL_NAME_PATH) = detail.Path
            row(EquipmentImageSelectionView.COL_NAME_IMAGE_TYPE) = detail.ImageType
            t.Rows.Add(row)
        Next
        Return New EquipmentImageSelectionView(t)
    End Function

    Public Class EquipmentImageSelectionView
        Inherits DataView
        Public Const COL_NAME_EQUIPMENT_COMMENT_ID As String = EquipmentImageDAL.COL_NAME_EQUIPMENT_IMAGE_ID
        Public Const COL_NAME_CODE As String = EquipmentImageDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = EquipmentImageDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_PATH As String = EquipmentImageDAL.COL_NAME_PATH
        Public Const COL_NAME_IMAGE_TYPE As String = EquipmentImageDAL.COL_NAME_IMAGE_TYPE

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_EQUIPMENT_COMMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CODE, GetType(String))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_PATH, GetType(String))
            t.Columns.Add(COL_NAME_IMAGE_TYPE, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetImageChild(ByVal childId As Guid) As EquipmentImage
        Return CType(EquipmentImageChildren.GetChild(childId), EquipmentImage)
    End Function

    Public Function GetNewImageChild() As EquipmentImage
        Dim newEquipmentImage As EquipmentImage = CType(EquipmentImageChildren.GetNewChild, EquipmentImage)
        newEquipmentImage.EquipmentId = Id
        Return newEquipmentImage
    End Function
#End Region

#Region "Comments"
    Public ReadOnly Property EquipmentCommentChildren() As EquipmentComment.EquipmentCommentList
        Get
            Return New EquipmentComment.EquipmentCommentList(Me)
        End Get
    End Property

    Public Function GetCommentSelectionView() As EquipmentCommentSelectionView
        Dim t As DataTable = EquipmentCommentSelectionView.CreateTable
        Dim detail As EquipmentComment

        For Each detail In EquipmentCommentChildren
            Dim row As DataRow = t.NewRow
            row(EquipmentCommentSelectionView.COL_NAME_EQUIPMENT_COMMENT_ID) = detail.Id.ToByteArray()
            row(EquipmentCommentSelectionView.COL_NAME_COMMENT) = detail.Comment
            t.Rows.Add(row)
        Next
        Return New EquipmentCommentSelectionView(t)
    End Function

    Public Class EquipmentCommentSelectionView
        Inherits DataView
        Public Const COL_NAME_EQUIPMENT_COMMENT_ID As String = EquipmentCommentDAL.COL_NAME_EQUIPMENT_COMMENT_ID
        Public Const COL_NAME_COMMENT As String = EquipmentCommentDAL.COL_NAME_COMMENT

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_EQUIPMENT_COMMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_COMMENT, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetCommentChild(ByVal childId As Guid) As EquipmentComment
        Return CType(EquipmentCommentChildren.GetChild(childId), EquipmentComment)
    End Function

    Public Function GetNewCommentChild() As EquipmentComment
        Dim newEquipmentComment As EquipmentComment = CType(EquipmentCommentChildren.GetNewChild, EquipmentComment)
        newEquipmentComment.EquipmentId = Id
        Return newEquipmentComment
    End Function
#End Region

#Region "Related Equipment"

    Public ReadOnly Property RelatedEquipmentChildren() As RelatedEquipment.RelatedEquipmentList
        Get
            Return New RelatedEquipment.RelatedEquipmentList(Me)
        End Get
    End Property

    Public Function GetEquipmentSelectionView() As RelatedEquipmentSelectionView
        Dim t As DataTable = RelatedEquipmentSelectionView.CreateTable
        Dim detail As RelatedEquipment

        For Each detail In RelatedEquipmentChildren
            Dim row As DataRow = t.NewRow
            row(RelatedEquipmentSelectionView.COL_NAME_RELATED_EQUIPMENT_ID) = detail.Id.ToByteArray()
            row(RelatedEquipmentSelectionView.COL_NAME_CHILD_EQUIPMENT_ID) = detail.ChildEquipmentId.ToByteArray()
            If Not detail.IsNew Then
                row(RelatedEquipmentSelectionView.COL_NAME_EQUIPMENT_TYPE) = LookupListNew.GetDescriptionFromId(LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), detail.EquipmentTypeID)
                row(RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION) = detail.EquipmentDescription
                row(RelatedEquipmentSelectionView.COL_NAME_MANUFACTURER) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_MANUFACTURERS, detail.MakeID)
                row(RelatedEquipmentSelectionView.COL_NAME_MODEL) = detail.Model
            Else
                Dim equip As New Equipment(detail.ChildEquipmentId)
                row(RelatedEquipmentSelectionView.COL_NAME_EQUIPMENT_TYPE) = LookupListNew.GetDescriptionFromId(LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), equip.EquipmentTypeId)
                row(RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION) = equip.Description
                row(RelatedEquipmentSelectionView.COL_NAME_MANUFACTURER) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_MANUFACTURERS, equip.ManufacturerId)
                row(RelatedEquipmentSelectionView.COL_NAME_MODEL) = equip.Model
            End If
            row(RelatedEquipmentSelectionView.COL_NAME_IN_OEM_BOX) = LookupListNew.GetDescriptionFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), detail.IsInOemBoxId)
            row(RelatedEquipmentSelectionView.COL_NAME_IS_COVERED) = LookupListNew.GetDescriptionFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), detail.IsCoveredId)
            t.Rows.Add(row)
        Next
        Return New RelatedEquipmentSelectionView(t)
    End Function

    Public Class RelatedEquipmentSelectionView
        Inherits DataView
        Public Const COL_NAME_RELATED_EQUIPMENT_ID As String = RelatedEquipmentDAL.COL_NAME_RELATED_EQUIPMENT_ID
        Public Const COL_NAME_EQUIPMENT_TYPE As String = RelatedEquipmentDAL.COL_NAME_EQUIPMENT_TYPE
        Public Const COL_NAME_DESCRIPTION As String = RelatedEquipmentDAL.COL_NAME_EQUIPMENT_DESCRIPTION
        Public Const COL_NAME_MANUFACTURER As String = RelatedEquipmentDAL.COL_NAME_EQUIPMENT_MANUFACTURER
        Public Const COL_NAME_MODEL As String = RelatedEquipmentDAL.COL_NAME_EQUIPMENT_MODEL
        Public Const COL_NAME_IN_OEM_BOX As String = RelatedEquipmentDAL.COL_NAME_IS_IN_OEM_BOX
        Public Const COL_NAME_IS_COVERED As String = RelatedEquipmentDAL.COL_NAME_IS_COVERED
        Public Const COL_NAME_CHILD_EQUIPMENT_ID As String = RelatedEquipmentDAL.COL_NAME_CHILD_EQUIPMENT_ID

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_RELATED_EQUIPMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CHILD_EQUIPMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EQUIPMENT_TYPE, GetType(String))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_MANUFACTURER, GetType(String))
            t.Columns.Add(COL_NAME_MODEL, GetType(String))
            t.Columns.Add(COL_NAME_IN_OEM_BOX, GetType(String))
            t.Columns.Add(COL_NAME_IS_COVERED, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetRelatedEquipmentChild(ByVal childId As Guid) As RelatedEquipment
        Return CType(RelatedEquipmentChildren.GetChild(childId), RelatedEquipment)
    End Function

    Public Function GetNewRelatedEquipmentChild() As RelatedEquipment
        Dim newRelatedEquipment As RelatedEquipment = CType(RelatedEquipmentChildren.GetNewChild, RelatedEquipment)
        newRelatedEquipment.EquipmentId = Id
        Return newRelatedEquipment
    End Function

    Public Function GetRelatedEquipmentDV() As DataView
        Dim t As New DataTable
        Dim ID_COL As String = "ID"
        Dim DESCRIPTION_COL As String = "DESCRIPTION"

        t.Columns.Add(ID_COL, GetType(Byte()))
        t.Columns.Add(DESCRIPTION_COL, GetType(String))
        For Each RE As DataRowView In GetEquipmentSelectionView()
            Dim dtrow As DataRow = t.Rows.Add
            dtrow(ID_COL) = RE(Equipment.RelatedEquipmentSelectionView.COL_NAME_CHILD_EQUIPMENT_ID)
            dtrow(DESCRIPTION_COL) = RE(Equipment.RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION)
        Next
        Return t.DefaultView
    End Function

#End Region

#End Region

#Region "Public Methods"

    Public Function GetManufacturerCoverages() As MfgCoverage.MfgCoverageSearchDV
        Return New MfgCoverage.MfgCoverageSearchDV(Dataset.Tables(MfgCoverageDAL.TABLE_NAME))
    End Function

    Public Function ExecuteEquipmentListFilter(ByVal makeid As Guid, ByVal equipmentClass As Guid, ByVal equipmentType As Guid, ByVal Model As String, ByVal Description As String, ByVal parentEquipmenttype As Guid) As DataView
        Dim eqListDal As New EquipmentDAL

        If String.IsNullOrEmpty(Description) Then Description &= DALBase.ASTERISK
        Description = Description.Replace(DALBase.ASTERISK, DALBase.WILDCARD_CHAR)

        If String.IsNullOrEmpty(Model) Then Model &= DALBase.ASTERISK
        Model = Model.Replace(DALBase.ASTERISK, DALBase.WILDCARD_CHAR)

        Return eqListDal.ExecuteEquipmentFilter(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, makeid, equipmentClass, equipmentType, Model, Description, parentEquipmenttype).Tables(0).DefaultView
    End Function


    Public Shared Function GetEquipmentIdByEquipmentList(ByVal equipmentListCode As String, _
                                                         ByVal effectiveDate As DateTime, ByVal manufacturer_id As Guid, ByVal model As String) As Guid
        Dim dal As New EquipmentDAL
        Return dal.GetEquipmentIdByEquipmentList(equipmentListCode, effectiveDate, manufacturer_id, model)

    End Function

#End Region

#Region "Custom Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryIfNotMasterModel
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Equipment = CType(objectToValidate, Equipment)
            If (obj.IsMasterEquipment = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")) Then
                Dim vma As ValueMandatoryAttribute = New ValueMandatoryAttribute(_fieldDisplayName)
                Return vma.IsValid(objectToCheck, objectToValidate)
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM003)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Equipment = CType(objectToValidate, Equipment)
            If (obj.CheckDuplicateMakeModel()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckDuplicateMakeModel() As Boolean
        Dim eqptDal As New EquipmentDAL
        Dim manufacturerName As String
        If (LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), IsMasterEquipment) = "Y") Then
            Return False
        End If
        manufacturerName = LookupListNew.GetDescriptionFromId(LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), ManufacturerId)
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        Dim dv As Equipment.EquipmentSearchDV = New Equipment.EquipmentSearchDV(eqptDal.LoadList(String.Empty, Model, manufacturerName, String.Empty, String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, String.Empty).Tables(0))

        For Each dr As DataRow In dv.Table.Rows
            If (Not New Guid(CType(dr(EquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte())).Equals(Id)) Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region


End Class



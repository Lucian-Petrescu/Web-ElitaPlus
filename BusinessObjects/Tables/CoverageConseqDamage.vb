'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/25/2017)  ********************

Public Class CoverageConseqDamage
    Inherits BusinessObjectBase


#Region "Constants"

    Public Const COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID As String = CoverageConseqDamageDAL.COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID
    Public Const COL_NAME_COVERAGE_ID As String = CoverageConseqDamageDAL.COL_NAME_COVERAGE_ID
    Public Const COL_NAME_CONSEQ_DAMAGE_TYPE As String = CoverageConseqDamageDAL.COL_NAME_CONSEQ_DAMAGE_TYPE_XCD
    Public Const COL_NAME_LIABILITY_LIMIT_BASED_ON As String = CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_BASE_XCD
    Public Const COL_NAME_LIABILITY_LIMIT_PER_INCIDENT As String = CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT
    Public Const COL_NAME_LIABILITY_LIMIT_CUMULATIVE As String = CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_CUMULATIVE
    Public Const COL_NAME_EFFECTIVE As String = CoverageConseqDamageDAL.COL_NAME_EFFECTIVE
    Public Const COL_NAME_EXPIRATION As String = CoverageConseqDamageDAL.COL_NAME_EXPIRATION
    Public Const COL_NAME_FULFILMENT_METHOD_XCD As String = CoverageConseqDamageDAL.COL_NAME_FULFILMENT_METHOD_XCD

    Public Const COL_NAME_CONSEQ_DAMAGE_TYPE_DESC As String = CoverageConseqDamageDAL.COL_NAME_CONSEQ_DAMAGE_TYPE_DESC
    Public Const COL_NAME_LIABILITY_LIMIT_BASED_ON_DESC As String = CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_BASE_DESC
    Public Const COL_NAME_FULFILMENT_METHOD_DESC As String = CoverageConseqDamageDAL.COL_NAME_FULFILMENT_METHOD_DESC

    Public Const COVERAGE_CONSEQ_DAMAGE_FORM001 As String = "COVERAGE_PERIL_FORM001"  ' Expiration date must be greater than or equal to Effective date 
    Public Const COVERAGE_CONSEQ_DAMAGE_FORM002 As String = "COVERAGE_PERIL_FORM002"  ' There should be no overlaps  (Effective/Expiration)
    Public Const COVERAGE_CONSEQ_DAMAGE_FORM003 As String = "COVERAGE_PERIL_FORM003"  ' Expiration date must be greater than today date
    Public Const COVERAGE_CONSEQ_DAMAGE_FORM004 As String = "COVERAGE_PERIL_FORM004"  ' Effective date must be greater than today date
    Public Const COVERAGE_CONSEQ_DAMAGE_FORM005 As String = "COVERAGE_PERIL_FORM005"  ' Value should be greater than zero

    Private Const COVERAGE_CONSEQ_DAMAGE_ID As Integer = 0
#End Region

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

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CoverageConseqDamageDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New CoverageConseqDamageDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(CoverageConseqDamageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageConseqDamageDAL.COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CoverageId As Guid
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageConseqDamageDAL.COL_NAME_COVERAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_COVERAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property ConseqDamageTypeXcd As String
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_CONSEQ_DAMAGE_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageConseqDamageDAL.COL_NAME_CONSEQ_DAMAGE_TYPE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_CONSEQ_DAMAGE_TYPE_XCD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property LiabilityLimitBaseXcd As String
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_BASE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_BASE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_BASE_XCD, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property FulfilmentMethodXcd As String
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_FULFILMENT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageConseqDamageDAL.COL_NAME_FULFILMENT_METHOD_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_FULFILMENT_METHOD_XCD, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=Decimal.MaxValue)>
    Public Property LiabilityLimitPerIncident As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT, Value)
        End Set
    End Property


    <RequiredConditionally_Liability_Limit_Cumulative(""), ValidNumericRange("", Min:=0, Max:=Decimal.MaxValue)>
    Public Property LiabilityLimitCumulative As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_CUMULATIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_CUMULATIVE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_LIABILITY_LIMIT_CUMULATIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateEffectiveDate("")>
    Public Property Effective As DateType
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CoverageConseqDamageDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidExpirationDate("")>
    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If Row(CoverageConseqDamageDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CoverageConseqDamageDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CoverageConseqDamageDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageConseqDamageDAL
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
    Public Class ConseqDamageSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COVERAGE_CONSEQ_DAMAGE_ID As String = "coverage_conseq_damage_id"
        Public Const COL_COVERAGE_ID As String = "coverage_id"
        Public Const COL_CONSEQ_DAMAGE_TYPE_XCD As String = "conseq_damage_type_xcd"
        Public Const COL_LIABILITY_LIMIT_BASE_XCD As String = "liability_limit_base_xcd"
        Public Const COL_LIABILITY_LIMIT_PER_INCIDENT As String = "liability_limit_per_incident"
        Public Const COL_LIABILITY_LIMIT_CUMULATIVE As String = "liability_limit_cumulative"
        Public Const COL_EFFECTIVE As String = "effective"
        Public Const COL_EXPIRATION As String = "expiration"
        Public Const COL_FULFILMENT_METHOD_XCD As String = "fulfilment_method_xcd"


#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ConseqDamageSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_COVERAGE_CONSEQ_DAMAGE_ID) = (New Guid()).ToByteArray
            row(COL_COVERAGE_ID) = Guid.Empty.ToByteArray
            row(COL_CONSEQ_DAMAGE_TYPE_XCD) = DBNull.Value
            row(COL_LIABILITY_LIMIT_BASE_XCD) = DBNull.Value
            row(COL_LIABILITY_LIMIT_PER_INCIDENT) = DBNull.Value
            row(COL_LIABILITY_LIMIT_CUMULATIVE) = DBNull.Value
            row(COL_EFFECTIVE) = DBNull.Value
            row(COL_EXPIRATION) = DBNull.Value
            row(COL_FULFILMENT_METHOD_XCD) = DBNull.Value
            dt.Rows.Add(row)
            Return New ConseqDamageSearchDV(dt)
        End Function

    End Class


#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(CoverageId As Guid, LanguageId As Guid) As DataView
        Try
            Dim dal As New CoverageConseqDamageDAL
            Return New DataView(dal.LoadList(CoverageId, LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RequiredConditionally_Liability_Limit_Cumulative
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_CONSEQ_DAMAGE_FORM005)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean

            Dim obj As CoverageConseqDamage = CType(objectToValidate, CoverageConseqDamage)
            If Not String.IsNullOrEmpty(obj.LiabilityLimitBaseXcd) AndAlso Not obj.LiabilityLimitBaseXcd.Equals(Codes.COVERAGE_CONSEQ_DAMAGE_LIABILITY_LIMIT_BASED_ON_NOTAPPL) Then
                If obj.LiabilityLimitCumulative Is Nothing Or obj.LiabilityLimitCumulative < 1.0 Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_CONSEQ_DAMAGE_FORM001)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CoverageConseqDamage = CType(objectToValidate, CoverageConseqDamage)

            Dim bValid As Boolean = True

            If obj IsNot Nothing AndAlso obj.ConseqDamageTypeXcd IsNot Nothing AndAlso obj.Effective IsNot Nothing AndAlso obj.Expiration IsNot Nothing Then
                If Convert.ToDateTime(obj.Expiration.Value) > DateTime.Now Then
                    If Convert.ToDateTime(obj.Effective.Value) > Convert.ToDateTime(obj.Expiration.Value) Then
                        Message = COVERAGE_CONSEQ_DAMAGE_FORM001
                        bValid = False
                    Else
                        If CheckEffectiveDate(obj.Effective, obj.Expiration, obj.ConseqDamageTypeXcd, obj) = False Then
                            Message = COVERAGE_CONSEQ_DAMAGE_FORM002
                            Return False
                        End If
                    End If
                Else
                    Message = COVERAGE_CONSEQ_DAMAGE_FORM003
                    bValid = False
                End If


            End If

            Return bValid

        End Function
        Private Function CheckEffectiveDate(sNewEffective As Assurant.Common.Types.DateType, sNewExpiration As Assurant.Common.Types.DateType, sNewConseqDamageType As String, oCoverageConseqDamage As CoverageConseqDamage) As Boolean
            Dim bValid As Boolean = True
            Dim oNewEffective As DateTime = Convert.ToDateTime(sNewEffective.Value)
            Dim oNewExpiration As DateTime = Convert.ToDateTime(sNewExpiration.Value)
            Dim oCoverageConseqDamageId As Guid = oCoverageConseqDamage.Id


            Dim strWhereCondition As String = "conseq_damage_type_xcd = '" + sNewConseqDamageType + "'"
            strWhereCondition += " And ("
            strWhereCondition += " (#" + oNewEffective + "# >= effective  And  #" + oNewEffective + "# <= expiration ) "
            strWhereCondition += " OR "
            strWhereCondition += " (#" + oNewExpiration + "# >= effective  And  #" + oNewExpiration + "# <= expiration ) "
            strWhereCondition += " )"

            Dim oCoverageConseqDamages As DataView = oCoverageConseqDamage.GetList(oCoverageConseqDamage.CoverageId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            oCoverageConseqDamages.RowFilter = strWhereCondition


            If oCoverageConseqDamages.Count > 1 Then
                bValid = False
            ElseIf oCoverageConseqDamages.Count = 1 Then
                For Each rowView As DataRowView In oCoverageConseqDamages
                    Dim row As DataRow = rowView.Row
                    oCoverageConseqDamageId = New Guid(CType(row(COVERAGE_CONSEQ_DAMAGE_ID), Byte()))
                    If Not oCoverageConseqDamage.Id.Equals(oCoverageConseqDamageId) Then
                        bValid = False
                        Exit For
                    End If
                Next
            End If

            Return bValid
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_CONSEQ_DAMAGE_FORM002)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CoverageConseqDamage = CType(objectToValidate, CoverageConseqDamage)
            If obj IsNot Nothing AndAlso obj.Effective IsNot Nothing Then
                If Convert.ToDateTime(obj.Effective.Value) > DateTime.Now Then
                    Return True
                Else
                    Message = COVERAGE_CONSEQ_DAMAGE_FORM004
                    Return False
                End If
            End If
            Return True

        End Function

    End Class

#End Region


End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/9/2015)  ********************
Imports System.Collections.Generic
Imports System.Linq

Public Class AttributeValue
    Inherits BusinessObjectBase

    Public Const DUPLICATE_ELITA_ATTRIBUTE As String = "DUPLICATE_ELITA_ATTRIBUTE"

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
            Dim dal As New AttributeValueDAL
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
            Dim dal As New AttributeValueDAL
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
            If row(AttributeValueDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AttributeValueDAL.COL_NAME_ATTRIBUTE_VALUE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AttributeId() As Guid
        Get
            CheckDeleted()
            If row(AttributeValueDAL.COL_NAME_ATTRIBUTE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AttributeValueDAL.COL_NAME_ATTRIBUTE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            If (Me.AttributeId <> Value) Then
                Me.Attribute = Nothing
            End If
            Me.SetValue(AttributeValueDAL.COL_NAME_ATTRIBUTE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255), CheckDuplicateAttribute("")> _
    Public Property Value() As String
        Get
            CheckDeleted()
            If Row(AttributeValueDAL.COL_NAME_ATTRIBUTE_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AttributeValueDAL.COL_NAME_ATTRIBUTE_VALUE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AttributeValueDAL.COL_NAME_ATTRIBUTE_VALUE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ReferenceId() As Guid
        Get
            CheckDeleted()
            If row(AttributeValueDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AttributeValueDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AttributeValueDAL.COL_NAME_REFERENCE_ID, Value)
        End Set
    End Property


    <EffectiveDateMandatory(""), AttributeValue.EffectiveExpirationDate("")> _
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(AttributeValueDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AttributeValueDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AttributeValueDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ExpirationDateMandatory("")> _
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(AttributeValueDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AttributeValueDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AttributeValueDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


#End Region

#Region "Lazy Initialize Fields"
    Private _attribute As ElitaAttribute = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Attribute As ElitaAttribute
        Get
            If (_attribute Is Nothing) Then
                If Not Me.AttributeId.Equals(Guid.Empty) Then
                    Me.Attribute = New ElitaAttribute(Me.AttributeId, Me.Dataset)
                End If
            End If
            Return _attribute
        End Get
        Private Set(ByVal value As ElitaAttribute)
            If (_attribute Is Nothing OrElse value Is Nothing OrElse Not _attribute.Equals(value)) Then
                _attribute = value
            End If
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            ' Me._isDSCreator AndAlso
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AttributeValueDAL
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

    Public Sub Copy(ByVal original As AttributeValue)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Object.")
        End If
        MyBase.CopyFrom(original)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class EffectiveDateMandatoryAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AttributeValue = CType(objectToValidate, AttributeValue)
            Dim oAttribute As ElitaAttribute = obj.Attribute
            If (oAttribute Is Nothing) Then Return True
            If (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                Return Not (obj.EffectiveDate = Nothing)
            Else
                Return (obj.EffectiveDate = Nothing)
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ExpirationDateMandatoryAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AttributeValue = CType(objectToValidate, AttributeValue)
            Dim oAttribute As ElitaAttribute = obj.Attribute
            If (oAttribute Is Nothing) Then Return True
            If (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                Return Not (obj.ExpirationDate = Nothing)
            Else
                Return (obj.ExpirationDate = Nothing)
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class EffectiveExpirationDateAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.INVALID_EXP_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AttributeValue = CType(objectToValidate, AttributeValue)
            Dim oAttribute As ElitaAttribute = obj.Attribute
            If (oAttribute Is Nothing) Then Return True

            If (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                If (obj.EffectiveDate = Nothing) OrElse (obj.ExpirationDate = Nothing) Then Return True
                Return (obj.EffectiveDate.Value < obj.ExpirationDate.Value)
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_ELITA_ATTRIBUTE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AttributeValue = CType(objectToValidate, AttributeValue)
            Dim oAttribute As ElitaAttribute = obj.Attribute
            If (oAttribute Is Nothing) Then Return True

            If (oAttribute.AllowDuplicates = Codes.YESNO_Y) Then
                Return True
            End If

            For Each dr As DataRow In obj.Row.Table.Rows

                If dr.RowState = DataRowState.Deleted Then
                    Continue For
                End If

                Dim oAv As AttributeValue = New AttributeValue(dr)

                If (oAv.Id = obj.Id) Then
                    Continue For
                End If

                If (oAv.AttributeId <> obj.AttributeId) Then
                    Continue For
                End If

                If (obj.Attribute.UseEffectiveDate = Codes.YESNO_Y) Then
                    If (obj.EffectiveDate = Nothing) OrElse (obj.ExpirationDate = Nothing) Then Return True
                    ' Check Uniqueness with Effective Date
                    If (oAv.ExpirationDate.Value < obj.EffectiveDate.Value) OrElse (oAv.EffectiveDate.Value > obj.ExpirationDate.Value) Then
                        Continue For
                    Else
                        Return False
                    End If
                Else
                    ' Check Uniqueness w/o Effective Dates
                    Return False
                End If
            Next

            Return True
        End Function
    End Class

End Class

Public Class AttributeValueList(Of TParent As {IAttributable})
    Implements IEnumerable(Of AttributeValue)

    Private ReadOnly _DataSet As DataSet
    Private ReadOnly _Parent As TParent

    Public Sub New(ByVal pDataSet As DataSet, ByVal pParent As TParent)
        Me._DataSet = pDataSet
        Me._Parent = pParent
    End Sub

#Region "Read/Load Properties and Methods"

    Private ReadOnly Property AttributeValuesTable As DataTable
        Get
            If (Not _DataSet.Tables.Contains(AttributeValueDAL.TABLE_NAME)) Then
                LoadAttributeData()
            End If

            Return _DataSet.Tables(AttributeValueDAL.TABLE_NAME)
        End Get
    End Property

    Private ReadOnly Property AttributesTable As DataTable
        Get
            If (Not _DataSet.Tables.Contains(AttributeDAL.TABLE_NAME)) Then
                LoadAttributeData()
            End If

            Return _DataSet.Tables(AttributeDAL.TABLE_NAME)
        End Get
    End Property

    Public ReadOnly Property Attribues As IEnumerable(Of ElitaAttribute)
        Get
            Return AttributesTable.AsEnumerable().Select(Function(dr) New ElitaAttribute(dr))
        End Get
    End Property

    Private Sub LoadAttributeData()
        ' Need to get data from database, but before that delete Attribute Table
        If (_DataSet.Tables.Contains(AttributeDAL.TABLE_NAME)) Then
            _DataSet.Tables.Remove(AttributeDAL.TABLE_NAME)
        End If

        Dim dal As New AttributeValueDAL
        Dim ds As DataSet = dal.LoadList(_Parent.TableName, _Parent.Id)

        For Each dt As DataTable In ds.Tables
            If (dt.TableName = AttributeDAL.TABLE_NAME) Then
                _DataSet.Tables.Add(dt.Copy())
            End If

            If (dt.TableName = AttributeValueDAL.TABLE_NAME) AndAlso (Not _DataSet.Tables.Contains(AttributeValueDAL.TABLE_NAME)) Then
                _DataSet.Tables.Add(dt.Copy())
            End If
        Next

    End Sub

#End Region

#Region "IEnumerable Inteface"

    Public Function GetEnumeratorGeneric() As IEnumerator(Of AttributeValue) Implements IEnumerable(Of AttributeValue).GetEnumerator
        Dim list As New List(Of AttributeValue)
        Dim row As DataRow
        For Each row In Me.AttributeValuesTable.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim bo As AttributeValue = New AttributeValue(row)
                If bo.ReferenceId = _Parent.Id Then
                    list.Add(bo)
                End If
            End If
        Next
        Return CType(list, IEnumerable(Of AttributeValue)).GetEnumerator
    End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumeratorGeneric()
    End Function

#End Region

#Region "Add new Attribute Value"

    Public Function GetNewAttributeChild() As AttributeValue
        Dim oAttributeValue As AttributeValue
        oAttributeValue = New AttributeValue(Me._DataSet)
        oAttributeValue.ReferenceId = Me._Parent.Id
        Return oAttributeValue
    End Function

#End Region

    Public Property Value(ByVal uiProgCode As String) As String
        Get
            Dim oAttribute As ElitaAttribute = Me.Attribues.Where(Function(a) a.UiProgCode = uiProgCode).FirstOrDefault()
            If (oAttribute Is Nothing) Then
                Return New List(Of String)().AsEnumerable()
            End If

            If (oAttribute.AllowDuplicates = Codes.YESNO_Y) OrElse (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                Throw New InvalidOperationException("The property can only be used with Allow Duplicates = No and Use Effective Date = No")
            End If

            Return Me.Where(Function(av) av.AttributeId = oAttribute.Id).Select(Function(av) av.Value).FirstOrDefault()
        End Get
        Set(ByVal value As String)
            Dim oAttribute As ElitaAttribute = Me.Attribues.Where(Function(a) a.UiProgCode = uiProgCode).FirstOrDefault()
            If (oAttribute Is Nothing) Then
                Throw New InvalidOperationException(String.Format("Attribute {0} Not Configured for table {1}", uiProgCode, Me._Parent.TableName))
            End If

            If (oAttribute.AllowDuplicates = Codes.YESNO_Y) OrElse (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                Throw New InvalidOperationException("The property can only be used with Allow Duplicates = No and Use Effective Date = No")
            End If

            Dim oAttributeValue As AttributeValue = Me.Where(Function(av) av.AttributeId = oAttribute.Id).FirstOrDefault()

            If (((value Is Nothing) OrElse (value.Trim.Length = 0)) AndAlso (Not oAttributeValue Is Nothing)) Then
                oAttributeValue.Delete()
                oAttributeValue.Save()
            ElseIf (Not ((value Is Nothing) OrElse (value.Trim.Length = 0))) Then

                If (oAttributeValue Is Nothing) Then
                    oAttributeValue = New AttributeValue(Me._DataSet)
                    oAttributeValue.AttributeId = oAttribute.Id
                    oAttributeValue.ReferenceId = Me._Parent.Id
                Else
                    oAttributeValue.BeginEdit()
                End If

                oAttributeValue.Value = value

                oAttributeValue.EndEdit()
                oAttributeValue.Save()
            End If
        End Set
    End Property

    Public ReadOnly Property Values(ByVal uiProgCode As String) As IEnumerable(Of String)
        Get
            Dim oAttribute As ElitaAttribute = Me.Attribues.Where(Function(a) a.UiProgCode = uiProgCode).FirstOrDefault()
            If (oAttribute Is Nothing) Then
                Return New List(Of String)().AsEnumerable()
            End If

            If (oAttribute.AllowDuplicates = Codes.YESNO_N) OrElse (oAttribute.UseEffectiveDate = Codes.YESNO_Y) Then
                Throw New InvalidOperationException("The property can only be used with Allow Duplicates = Yes and Use Effective Date = No")
            End If

            Return Me.Where(Function(av) av.AttributeId = oAttribute.Id).Select(Function(av) av.Value).AsEnumerable()
        End Get
    End Property

    Public ReadOnly Property Values(ByVal uiProgCode As String, ByVal activeOn As Date) As IEnumerable(Of String)
        Get
            Dim oAttribute As ElitaAttribute = Me.Attribues.Where(Function(a) a.UiProgCode = uiProgCode).FirstOrDefault()
            If (oAttribute Is Nothing) Then
                Return New List(Of String)().AsEnumerable()
            End If

            If (oAttribute.AllowDuplicates = Codes.YESNO_N) OrElse (oAttribute.UseEffectiveDate = Codes.YESNO_N) Then
                Throw New InvalidOperationException("The property can only be used with Allow Duplicates = Yes and Use Effective Date = Yes")
            End If

            Return Me.Where(Function(av) ((av.AttributeId = oAttribute.Id) AndAlso (av.EffectiveDate.Value <= activeOn) AndAlso (av.ExpirationDate >= activeOn))).Select(Function(av) av.Value).AsEnumerable()
        End Get
    End Property

    Public ReadOnly Property Value(ByVal uiProgCode As String, ByVal activeOn As Date) As String
        Get
            Dim oAttribute As ElitaAttribute = Me.Attribues.Where(Function(a) a.UiProgCode = uiProgCode).FirstOrDefault()
            If (oAttribute Is Nothing) Then
                Return New List(Of String)().AsEnumerable()
            End If

            If (oAttribute.AllowDuplicates = Codes.YESNO_Y) OrElse (oAttribute.UseEffectiveDate = Codes.YESNO_N) Then
                Throw New InvalidOperationException("The property can only be used with Allow Duplicates = No and Use Effective Date = Yes")
            End If

            Return Me.Where(Function(av) ((av.AttributeId = oAttribute.Id) AndAlso (av.EffectiveDate.Value <= activeOn) AndAlso (av.ExpirationDate >= activeOn))).Select(Function(av) av.Value).FirstOrDefault()
        End Get
    End Property

    Public Function Contains(ByVal attributeCode As String) As Boolean
        Dim oAttributeValue As AttributeValue
        oAttributeValue = (From oItem As AttributeValue In Me Where oItem.Attribute.UiProgCode.ToUpperInvariant().Equals(attributeCode.ToUpperInvariant()) Select oItem).FirstOrDefault()
        Return Not (oAttributeValue Is Nothing)
    End Function

    Public Function Contains(ByVal attributeId As Guid) As Boolean
        Dim oAttributeValue As AttributeValue
        oAttributeValue = (From oItem As AttributeValue In Me Where oItem.AttributeId.Equals(attributeId) Select oItem).FirstOrDefault()
        Return Not (oAttributeValue Is Nothing)
    End Function


End Class

Public Interface IAttributable
    ReadOnly Property Id As Guid
    ReadOnly Property TableName As String
    ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable)
End Interface


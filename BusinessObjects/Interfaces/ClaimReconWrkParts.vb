Public Class ClaimReconWrkParts
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_PART_NUMBER As String = "PART_NUMBER"
    Public Const COL_PART_SKU As String = "PART_SKU"
    Public Const COL_PART_DESCRIPTION As String = "PART_DESCRIPTION"
    

#End Region

#Region "Constructors"

    'New BO
    Public Sub New()
        MyBase.New()
    End Sub
    'New BO
    Public Sub New(ByVal dealerProcessedID As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(dealerProcessedID)
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal oRow As DataRow, ByVal ds As DataSet)
        MyBase.New()
        Me.Dataset = ds
        Me.Row = oRow
    End Sub


    Protected Sub Load(ByVal dealerFileProcessedID As Guid)
        Try
            Dim dal As New ClaimReconWrkPartsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset, dealerFileProcessedID)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            Initialize()
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

    <ValidStringLength("", Max:=2)> _
    Public Property PartNumber() As String
        Get
            CheckDeleted()
            If Row(COL_PART_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_PART_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_PART_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=18)> _
    Public Property PartSKU() As String
        Get
            CheckDeleted()
            If Row(COL_PART_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_PART_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_PART_SKU, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property PartDescription() As String
        Get
            CheckDeleted()
            If Row(COL_PART_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_PART_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_PART_DESCRIPTION, Value)
        End Set
    End Property
    
#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(ByVal DealerfileProcessedId As Guid) As Guid
        Get
            Dim oDealerfileProcessed As New DealerFileProcessed(DealerfileProcessedId)
            Dim oDealer As New Dealer(oDealerfileProcessed.DealerId)
            Dim oCompanyId As Guid = oDealer.CompanyId
            Return oCompanyId
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Validate()
        'Only validating
        MyBase.Validate()
    End Sub
    Public Function SaveParts(ByVal ds As DataSet) As Integer
        Try
            Dim dal As New ClaimReconWrkPartsDAL
            Return dal.SaveParts(ds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal claimReconWrkId As Guid) As DataSet
        Try
            Dim dal As New ClaimReconWrkPartsDAL
            Dim ds As DataSet

            ds = dal.LoadList(claimReconWrkId)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class

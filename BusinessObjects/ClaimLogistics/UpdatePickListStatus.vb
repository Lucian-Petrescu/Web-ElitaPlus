Imports System.Text.RegularExpressions

Public Class UpdatePickListStatus
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_PICK_LIST_NUMBER As String = "pick_list_number"
    Public Const DATA_COL_NAME_PICKUP_BY As String = "pickup_by"
    Private Const TABLE_NAME As String = "UpdatePickListStatus"
    
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As UpdatePickListStatusDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty
    Private _serviceNetworkID As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As UpdatePickListStatusDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As UpdatePickListStatusDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdatePickListStatus Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As UpdatePickListStatusDs)
        Try
            If ds.UpdatePickListStatus.Count = 0 Then Exit Sub
            With ds.UpdatePickListStatus.Item(0)
                PickListNumber = .PICK_LIST_NUMBER
                PickupBy = .PICKUP_BY
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdatePickListStatus Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property PickListNumber As String
        Get
            If Row(DATA_COL_NAME_PICK_LIST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PICK_LIST_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PICK_LIST_NUMBER, Value)
        End Set
    End Property


    Public Property PickupBy As String
        Get
            If Row(DATA_COL_NAME_PICKUP_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PICKUP_BY), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PICKUP_BY, Value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            PickupListHeader.UpdatePickListStatus(PickListNumber, PickupBy)


            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class

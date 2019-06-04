'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2013)  ********************

Public Class DailyObdFileHeaderTemp
    Inherits BusinessObjectBase
    Private Const SEARCH_EXCEPTION As String = "CERTIFICATELIST_FORM001"
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
            Dim dal As New DailyObdFileHeaderTempDAL
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
            Dim dal As New DailyObdFileHeaderTempDAL
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
            If row(DailyObdFileHeaderTempDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DailyObdFileHeaderTempDAL.COL_NAME_FILE_HEADER_TEMP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DailyObdFileHeaderTempDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DailyObdFileHeaderTempDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property



    Public Property FromDate() As DateType
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_FROM_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyObdFileHeaderTempDAL.COL_NAME_FROM_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_FROM_DATE, Value)
        End Set
    End Property



    Public Property ToDate() As DateType
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_TO_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyObdFileHeaderTempDAL.COL_NAME_TO_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_TO_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnCreatedDate() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CREATED_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CREATED_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnModifiedDate() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_MODIFIED_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_MODIFIED_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnCert() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CERT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CERT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CERT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnCancel() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CANCEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CANCEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_CANCEL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnBilling() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_BILLING) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_BILLING), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_BILLING, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property SelectionOnNewbusiness() As String
        Get
            CheckDeleted()
            If row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_NEWBUSINESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_NEWBUSINESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyObdFileHeaderTempDAL.COL_NAME_SELECTION_ON_NEWBUSINESS, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DailyObdFileHeaderTempDAL
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
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal FromDate As String, ByVal ToDate As String, ByVal certNumber As String, ByVal SelectOnCreatedDate As String, _
                           ByVal SelectOnModifiedDate As String, ByVal SelectOnNewEnrollmnt As String, _
                           ByVal SelectOnCancel As String, ByVal selectOnBilling As String) As ObdFilHdrtempsearchDV

        Try
            ' Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Certificate), Nothing, "Search", Nothing)}
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(DailyObdFileHeaderTemp), Nothing, "Search", Nothing)}
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (certNumber.Equals(String.Empty) AndAlso FromDate.Equals(String.Empty) AndAlso ToDate.Equals(String.Empty) AndAlso _
               SelectOnCreatedDate = "N" AndAlso SelectOnModifiedDate = "N" AndAlso _
                SelectOnNewEnrollmnt = "N" AndAlso SelectOnCancel = "N" AndAlso selectOnBilling = "N") Then
                Throw New BOValidationException(errors, GetType(DailyObdFileHeaderTemp).FullName)
            End If


            Dim dal As New DailyObdFileHeaderTempDAL
            ' Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Com




            Return New ObdFilHdrtempsearchDV(dal.Load(FromDate, ToDate, certNumber, SelectOnCreatedDate, _
                                                 SelectOnModifiedDate, SelectOnNewEnrollmnt, SelectOnCancel, selectOnBilling).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getviewList() As ObdFilHdrtempsearchDV
        Try
            Dim dal As New DailyObdFileHeaderTempDAL

            Return New ObdFilHdrtempsearchDV(dal.LoadList().Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class ObdFilHdrtempsearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_File_Header_Temp_ID As String = "File_Header_Temp_id"
        Public Const COL_File_Detail_Temp_ID As String = "File_Detail_Temp_id"
        Public Const COL_CERT_NUMBER As String = "Cert_Number"
        Public Const COL_FROM_DATE As String = "FromDate"
        Public Const COL_TO_DATE As String = "ToDate"
        Public Const COL_SELECTION_ON_CREATED_DATE As String = "Selection_On_Created_Date"
        Public Const COL_SELECTION_ON_MODIFIED_DATE As String = "Selection_On_Modified_Date"
        Public Const COL_SELECTION_ON_NEW_BUSINESS As String = "Selection_On_New_Enrollment"
        Public Const COL_SELECTION_ON_Cancel As String = "Selection_On_Cancel"
        Public Const COL_SELECTION_ON_BILLING As String = "Selection_On_Billing"

#End Region
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property FileHeaderTempId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_File_Header_Temp_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CertNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_CERT_NUMBER).ToString
            End Get
        End Property

    End Class
#End Region

    Public Shared Sub getHeaderRecordsList(ByVal CompanyCode As String, ByVal Dealercode As String, ByVal CertNumber As String, _
                                            ByVal selectoncreateddate As String, ByVal selectonmodifieddate As String, _
                                            ByVal selectonNewEnrollment As String, ByVal selectoncancel As String, ByVal selectonbilling As String, _
                                            ByVal fromdate As Date, ByVal todate As Date, ByVal callfrom As String, _
                                             Optional ByVal processeddate As Date = Nothing, _
                                            Optional ByVal selectioncertificate As String = "")
        Try
            Dim dal As New DailyObdFileHeaderTempDAL
            dal.getrecordslist(CompanyCode, Dealercode, CertNumber, selectoncreateddate, selectonmodifieddate, selectonNewEnrollment, selectoncancel, selectonbilling, fromdate, todate, callfrom, processeddate, selectioncertificate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
End Class




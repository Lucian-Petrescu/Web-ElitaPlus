'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/11/2007)********************
#Region "QuoteEngineData"

Public Class QuoteEngineData

    Public CompanyGroupID, UserId As Guid

    Public Dealer, Manufacturer, Model, VIN, VehicleLicenseTag, EngineVersion, NewUsed, ExternalCarCode As String
    Public InServiceDate, WarrantyDate As Date
    Public Year, Odometer As Integer
    Public VehicleValue As Decimal

    Public Options As String
End Class

#End Region

Public Class VSCQuoteDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_QUOTE"
    Public Const TABLE_NAME_QUOTE_INFO As String = "ELP_VSC_QUOTE"
    Public Const TABLE_NAME_QUOTE_HEADER As String = "VSC_QUOTE_HEADER"
    Public Const TABLE_NAME_QUOTE_ITEM As String = "VSC_QUOTE_ITEM"

    Public Const TABLE_NAME_QUOTE_ENGINE As String = "VSC_QUOTE_ENGINE"

    Public Const TABLE_KEY_NAME As String = "quote_id"

    Public Const COL_NAME_QUOTE_ID As String = "quote_id"
    Public Const COL_NAME_QUOTE_NUMBER As String = "quote_number"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_VSC_MODEL_ID As String = "vsc_model_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MODEL_YEAR As String = "model_year"
    Public Const COL_NAME_VSC_CLASS_CODE_ID As String = "vsc_class_code_id"
    Public Const COL_NAME_VIN As String = "vin"
    Public Const COL_NAME_ODOMETER As String = "odometer"
    Public Const COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const COL_NAME_ENGINE_VERSION As String = "engine_version"
    Public Const COL_NAME_IN_SERVICE_DATE As String = "in_service_date"
    Public Const COL_NAME_NEW_USED As String = "new_used"

    Public Const QUOTE_ENGINE_PARAM_MAKE As String = "manufacturer"
    Public Const QUOTE_ENGINE_PARAM_MODEL As String = "model"
    Public Const QUOTE_ENGINE_PARAM_VIN As String = "vin"
    Public Const QUOTE_ENGINE_PARAM_YEAR As String = "year"
    Public Const QUOTE_ENGINE_PARAM_ODOMETER As String = "odometer"
    Public Const QUOTE_ENGINE_PARAM_CONDITION As String = "condition"
    Public Const QUOTE_ENGINE_PARAM_IN_SERVICE_DATE As String = "in_service_date"
    Public Const QUOTE_ENGINE_PARAM_DEALER As String = "dealer"
    Public Const QUOTE_ENGINE_PARAM_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const QUOTE_ENGINE_PARAM_ENGINE_VERSION As String = "engine_version"

    Public Const TOTAL_INPUT_PARAM_Q_INFO As Integer = 10
    Public Const TOTAL_OUTPUT_PARAM_INFO As Integer = 2

    Public Const TOTAL_INPUT_PARAM_Q_IITEMS As Integer = 0
    Public Const TOTAL_OUTPUT_PARAM_ITEMS As Integer = 2

    'Get_VSC_QUOTE Procedure Input/Output Parameters
    Public Const P_COMPANY_GROUP_ID = 0
    Public Const P_DEALER = 1
    Public Const P_MANUFACTURER = 2
    Public Const P_MODEL = 3
    Public Const P_VIN = 4
    Public Const P_YEAR = 5
    Public Const P_ODOMETER = 6
    Public Const P_VEHICLE_LICENSE_TAG = 7
    Public Const P_ENGINE_VERSION = 8
    Public Const P_NEW_USED = 9
    Public Const P_IN_SERVICE_DATE = 10

    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1
    Public Const P_CURSOR = 2
    Public Const P_QOUTE_ID = 0

    Public Const SP_PARAM_NAME_P_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const SP_PARAM_NAME_P_USER_ID As String = "p_user_id"
    Public Const SP_PARAM_NAME_P_DEALER As String = "p_dealer"
    Public Const SP_PARAM_NAME_P_MANUFACTURER As String = "p_manufacturer"
    Public Const SP_PARAM_NAME_P_MODEL As String = "p_model"
    Public Const SP_PARAM_NAME_P_VIN As String = "p_VIN"
    Public Const SP_PARAM_NAME_P_YEAR As String = "p_year"
    Public Const SP_PARAM_NAME_P_ODOMETER As String = "p_odometer"
    Public Const SP_PARAM_NAME_P_VEHICLE_LICENSE_TAG As String = "p_vehicle_license_tag"
    Public Const SP_PARAM_NAME_P_ENGINE_VERSION As String = "p_engine_version"
    Public Const SP_PARAM_NAME_P_NEW_USED As String = "p_new_used"
    Public Const SP_PARAM_NAME_P_IN_SERVICE_DATE As String = "p_in_service_date"
    Public Const SP_PARAM_NAME_P_OPTIONS As String = "p_optional_list"
    Public Const SP_PARAM_NAME_P_RETURN As String = "p_return"
    Public Const SP_PARAM_NAME_P_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const SP_PARAM_NAME_P_CURSOR As String = "v_GET_VSC_QUOTEDETAILCursor"
    Public Const SP_PARAM_NAME_P_QUOTE_ID As String = "p_quote_id"
    Public Const SP_PARAM_NAME_P_WARRANTY_DATE As String = "p_warranty_date"
    Public Const SP_PARAM_NAME_P_EXTERNAL_CAR_CODE As String = "p_external_car_code"
    Public Const SP_PARAM_NAME_P_VEHICLE_VALUE As String = "p_vehicle_value"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("quote_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function GetQuote(oQuoteEngineData As QuoteEngineData) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_VSC_QUOTE")
        Dim inputQuoteInfoParameters() As DBHelper.DBHelperParameter
        Dim outputQuoteInfoParameter(TOTAL_OUTPUT_PARAM_INFO) As DBHelper.DBHelperParameter

        With oQuoteEngineData

            inputQuoteInfoParameters = New DBHelper.DBHelperParameter() _
                    {SetParameter(SP_PARAM_NAME_P_COMPANY_GROUP_ID, .CompanyGroupID.ToByteArray), _
                     SetParameter(SP_PARAM_NAME_P_USER_ID, .UserId.ToByteArray), _
                     SetParameter(SP_PARAM_NAME_P_DEALER, .Dealer), _
                     SetParameter(SP_PARAM_NAME_P_MANUFACTURER, .Manufacturer), _
                     SetParameter(SP_PARAM_NAME_P_MODEL, .Model), _
                     SetParameter(SP_PARAM_NAME_P_VIN, .VIN), _
                     SetParameter(SP_PARAM_NAME_P_YEAR, .Year), _
                     SetParameter(SP_PARAM_NAME_P_ODOMETER, .Odometer), _
                     SetParameter(SP_PARAM_NAME_P_VEHICLE_LICENSE_TAG, .VehicleLicenseTag), _
                     SetParameter(SP_PARAM_NAME_P_ENGINE_VERSION, .EngineVersion), _
                     SetParameter(SP_PARAM_NAME_P_NEW_USED, .NewUsed), _
                     SetParameter(SP_PARAM_NAME_P_IN_SERVICE_DATE, .InServiceDate), _
                     SetParameter(SP_PARAM_NAME_P_OPTIONS, .Options), _
                     SetParameter(SP_PARAM_NAME_P_WARRANTY_DATE, .WarrantyDate), _
                     SetParameter(SP_PARAM_NAME_P_EXTERNAL_CAR_CODE, .ExternalCarCode), _
                     SetParameter(SP_PARAM_NAME_P_VEHICLE_VALUE, IIf(.VehicleValue > 0, .VehicleValue, DBNull.Value))}

        End With
        outputQuoteInfoParameter(P_RETURN) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_RETURN, GetType(Integer))
        outputQuoteInfoParameter(P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_EXCEPTION_MSG, GetType(String), 50)
        outputQuoteInfoParameter(P_CURSOR) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_CURSOR, GetType(DataSet))
        Dim ds As New DataSet(TABLE_NAME_QUOTE_ENGINE)
        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inputQuoteInfoParameters, outputQuoteInfoParameter, ds, TABLE_NAME_QUOTE_HEADER)

        If outputQuoteInfoParameter(P_RETURN).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Quote Engine Generated Error: ", outputQuoteInfoParameter(P_EXCEPTION_MSG).Value)
        Else
            ' Get the Quote Item(s)
            Dim inputQuoteItemsParameters(TOTAL_INPUT_PARAM_Q_IITEMS) As DBHelper.DBHelperParameter
            Dim outputQuoteItemsParameter(TOTAL_OUTPUT_PARAM_ITEMS) As DBHelper.DBHelperParameter
            Dim quote_id As Guid = New Guid(CType(ds.Tables(TABLE_NAME_QUOTE_HEADER).Rows(0).Item(COL_NAME_QUOTE_ID), Byte()))
            inputQuoteItemsParameters(P_QOUTE_ID) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_QUOTE_ID, quote_id.ToByteArray)

            selectStmt = Config("/SQL/GET_VSC_QUOTE_ITEM")

            outputQuoteItemsParameter(P_RETURN) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_RETURN, GetType(Integer))
            outputQuoteItemsParameter(P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_EXCEPTION_MSG, GetType(String), 50)
            outputQuoteItemsParameter(P_CURSOR) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_CURSOR, GetType(DataSet))

            ' Call DBHelper Store Procedure
            DBHelper.FetchSp(selectStmt, inputQuoteItemsParameters, outputQuoteItemsParameter, ds, TABLE_NAME_QUOTE_ITEM)

            If outputQuoteInfoParameter(P_RETURN).Value <> 0 Then
                Throw New StoredProcedureGeneratedException("Quote Engine Generated Error: ", outputQuoteItemsParameter(P_EXCEPTION_MSG).Value)
            Else
                Return ds
            End If
        End If

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Private Methods"

    Function SetParameter(name As String, value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value)

    End Function

    Function Base64ToGuid(strGuid As String) As Guid

        Return New Guid(Convert.FromBase64String(strGuid))

    End Function

#End Region
End Class



Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Linq

<TestClass()> Public Class PriceListDetailTest

    ' Use TestInitialize to run code before running each test

    Private pldIdString = "59CE94747ED922A6E0530D28480A8F9A"
    Private pldID As Guid = New Guid(DotNetToOracle(pldIdString))

    Public Const PRICE_BAND_RANGE_FROM As Decimal = 0
    Public Const PRICE_BAND_RANGE_TO As Decimal = CDec(9999999.99)
    Private insertedId As Guid
    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub

    <TestMethod()> Public Sub Load_RecordFound()
        Dim ds As DataSet = New DataSet
        Dim recordCount As Integer = 1

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)


        Assert.IsNotNull(ds)
        Assert.IsTrue(ds.Tables(0).Rows.Count.Equals(recordCount))
        Assert.IsTrue(CType(ds.Tables(0).Rows(0)("price_list_detail_id"), Byte()).SequenceEqual(pldID.ToByteArray))
    End Sub


    <TestMethod()> Public Sub GetMakeByModel_RecordFound()

        Dim ds As DataSet = New DataSet

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)
        Dim pldBO_2 As PriceListDetail = New PriceListDetail()


        Dim eqmntId As Object = New Guid(CType(ds.Tables(0).Rows(0)("equipment_id"), Byte()))

        Dim manufacturerId As Guid = pldBO_2.GetMakeByModel(eqmntId)

        Assert.IsNotNull(manufacturerId)
    End Sub

    <TestMethod()> Public Sub GetVendorQuantiy_RecordFound()

        Dim ds As DataSet = New DataSet

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)

        Dim vQuantiyId As Guid = pldBO.GetVendorQuantiy()

        Assert.IsNotNull(vQuantiyId)
        Assert.IsTrue(Not vQuantiyId.Equals(Guid.Empty))
    End Sub

    <TestMethod()> Public Sub OverlapExists_RecordFound()


        Dim ds As DataSet = New DataSet
        Dim recordCount As Integer = 1

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)


        Dim eqmntId As Object = New Guid(CType(ds.Tables(0).Rows(0)("equipment_id"), Byte()))

        Dim overlap As Boolean = pldBO.OverlapExists(True)

        Assert.IsTrue(overlap)
    End Sub

    <TestMethod()> Public Sub GetManufactByEquipmentId_RecordFound()

        Dim ds As DataSet = New DataSet
        Dim recordCount As Integer = 1

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)
        Dim pldBO_2 As PriceListDetail = New PriceListDetail()


        Dim eqmntId As Object = New Guid(CType(ds.Tables(0).Rows(0)("equipment_id"), Byte()))

        Dim manufacturerId As Guid = pldBO_2.GetManufactByEquipmentId(eqmntId)

        Assert.IsNotNull(manufacturerId)
    End Sub

    <TestMethod()> Public Sub GetList_MultipleParameters_RecordFound()

        Dim ds As DataSet = New DataSet

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)
        Dim pldBO_2 As PriceListDetail = New PriceListDetail()
        Dim row As DataRow = ds.Tables(0).Rows(0)

        Dim plId As Guid = New Guid(CType(row("price_list_id"), Byte()))
        Dim riskTypeId As Guid = If(row("risk_type_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("risk_type_id"), Byte())))
        Dim eqmntId As Guid = If(row("equipment_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("equipment_id"), Byte())))
        Dim conditionId As Guid = If(row("condition_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("condition_id"), Byte())))
        Dim equipmentclassId As Guid = If(row("equipment_class_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("equipment_class_id"), Byte())))
        Dim EffectiveDate As DateTime = CType(row("Effective"), DateTime)
        Dim ServiceClassId As Guid = If(row("service_class_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("service_class_id"), Byte())))
        Dim ServiceTypeId As Guid = If(row("service_type_id") Is DBNull.Value, Guid.Empty, New Guid(CType(row("service_type_id"), Byte())))

        Dim dv As DataView = PriceListDetail.GetList(plId,
                                                    riskTypeId,
                                                    eqmntId,
                                                    conditionId,
                                                    equipmentclassId,
                                                    EffectiveDate,
                                                    ServiceClassId,
                                                    ServiceTypeId)

        Assert.IsNotNull(dv)
        Assert.IsNotNull(dv.Table)
        Assert.IsTrue(dv.Table.Rows.Count > 0)
        Assert.IsTrue(CType(dv.Table.Rows(0)("price_list_id"), Byte()).SequenceEqual(plId.ToByteArray))
    End Sub

    <TestMethod()> Public Sub ValidateRange_RecordFound()

        Dim ds As DataSet = New DataSet

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)

        Dim row As DataRow = ds.Tables(0).Rows(0)

        Dim plId As Guid = New Guid(CType(row("price_list_id"), Byte()))
        Dim riskTypeId As Guid = If(row("risk_type_id") Is DBNull.Value, Nothing, New Guid(CType(row("risk_type_id"), Byte())))
        Dim eqmntId As Guid = If(row("equipment_id") Is DBNull.Value, Nothing, New Guid(CType(row("equipment_id"), Byte())))
        Dim conditionId As Guid = If(row("condition_id") Is DBNull.Value, Nothing, New Guid(CType(row("condition_id"), Byte())))
        Dim equipmentclassId As Guid = If(row("equipment_class_id") Is DBNull.Value, Nothing, New Guid(CType(row("equipment_class_id"), Byte())))
        Dim EffectiveDate As DateTime = If(row("Effective") Is DBNull.Value, Nothing, CType(row("Effective"), DateTime))
        Dim ServiceClassId As Guid = If(row("service_class_id") Is DBNull.Value, Nothing, New Guid(CType(row("service_class_id"), Byte())))
        Dim ServiceTypeId As Guid = If(row("service_type_id") Is DBNull.Value, Nothing, New Guid(CType(row("service_type_id"), Byte())))
        Dim languageId = New Guid(DotNetToOracle("CB5D3DDE11EAEB49BB0F9E799906705A"))


        Dim pldDAL As PriceListDetailDAL = New PriceListDetailDAL()

        Dim dv As DataView = pldDAL.FormPriceRangeQuery(equipmentclassId,
                                                       eqmntId,
                                                       conditionId,
                                                       riskTypeId,
                                                       ServiceClassId,
                                                       ServiceTypeId,
                                                       plId,
                                                       languageId,
                                                       EffectiveDate.ToShortDateString())

        Assert.IsNotNull(dv)
        Assert.IsNotNull(dv.Table)
        Assert.IsTrue(dv.Table.Rows.Count > 0)
        Assert.IsTrue(CType(dv.Table.Rows(0)("price_list_id"), Byte()).SequenceEqual(plId.ToByteArray))
    End Sub

    <TestMethod()> Public Sub GetCurrentDateTime_SuccessfulResponse()

        Dim currDate As Date = PriceListDetail.GetCurrentDateTime()

        Assert.IsNotNull(currDate)
        Assert.IsTrue(currDate.Date.Equals(DateTime.Today.Date))
    End Sub

    <TestMethod()> Public Sub LoadTable_RecordsFound()

        Dim ds As DataSet = New DataSet
        Dim languageId = New Guid(DotNetToOracle("CB5D3DDE11EAEB49BB0F9E799906705A"))

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)

        Dim plId As Guid = New Guid(CType(ds.Tables(0).Rows(0)("price_list_id"), Byte()))

        Dim dal As New PriceListDetailDAL
        Dim dalDS As DataSet = New DataSet()

        dal.LoadPriceListDetailsForPriceList(dalDS, plId, languageId, ElitaPlusIdentity.Current.ActiveUser.Id)

        Assert.IsNotNull(dalDS)
        Assert.IsTrue(dalDS.Tables(0).Rows.Count > 0)
        Assert.IsTrue(CType(dalDS.Tables(0).Rows(0)("price_list_id"), Byte()).SequenceEqual(plId.ToByteArray))
    End Sub

    <TestMethod()> Public Sub GetMakeModelByEquipmentId_RecordsFound()

        Dim eqId As Guid = New Guid(DotNetToOracle("59CE94747B9F22A6E0530D28480A8F9A"))
        Dim compGroupId As Guid = New Guid(DotNetToOracle("D9EFC08FD369A9438B67389AD196D841"))
        Dim ds As DataSet = New DataSet()
        Dim make As String = "Apple"

        ds = PriceListDetail.GetMakeModelByEquipmentId(eqId, compGroupId)

        Assert.IsNotNull(ds)
        Assert.IsTrue(ds.Tables(0).Rows.Count > 0)
        Assert.IsTrue(ds.Tables(0).Rows(0)("MAKE").ToString.Trim.Equals(make, StringComparison.InvariantCultureIgnoreCase))
    End Sub

    <TestMethod()> Public Sub FormPriceRangeQuery_RecordsFound()

        Dim ds As DataSet = New DataSet

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)
        Dim pldDAL As PriceListDetailDAL = New PriceListDetailDAL()


        Dim oPriceBands As DataView = pldDAL.FormPriceRangeQuery(pldBO.EquipmentClassId,
                                                                 pldBO.EquipmentId,
                                                                 pldBO.ConditionId,
                                                                 pldBO.RiskTypeId,
                                                                 pldBO.ServiceClassId,
                                                                 pldBO.ServiceTypeId,
                                                                 pldBO.PriceListId,
                                                                 ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                 pldBO.Effective.Value.ToShortDateString())

        Assert.IsNotNull(oPriceBands)
        Assert.IsNotNull(oPriceBands.Table)
        Assert.IsTrue(oPriceBands.Table.Rows.Count > 0)
        Assert.IsTrue(CType(oPriceBands.Table.Rows(0)("price_list_id"), Byte()).SequenceEqual(pldBO.PriceListId.ToByteArray))

    End Sub

    <TestMethod()> Public Sub GetModelsByMake_RecordsFound()

        Dim eqId As Guid = New Guid(DotNetToOracle("59CE94747B9F22A6E0530D28480A8F9A"))
        Dim compGroupId As Guid = New Guid(DotNetToOracle("D9EFC08FD369A9438B67389AD196D841"))
        Dim ds As DataSet = New DataSet()

        ds = PriceListDetail.GetMakeModelByEquipmentId(eqId, compGroupId)
        Dim manufId As Guid = New Guid(CType(ds.Tables(0).Rows(0)("Manufacturer_Id"), Byte()))

        Dim ds2 As DataSet = PriceListDetail.GetModelsByMake(manufId)

        Assert.IsNotNull(ds2)
        Assert.IsTrue(ds2.Tables(0).Rows.Count > 0)
        Assert.IsTrue(CType(ds2.Tables(0).Rows(0)("id"), Byte()).SequenceEqual(eqId.ToByteArray))
    End Sub

    <TestMethod()> Public Sub GetMaxExpirationMinEffectiveDateForPriceList_RecordsFound()


        Dim ds As DataSet = New DataSet()

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)

        Dim pldDAL As PriceListDetailDAL = New PriceListDetailDAL()
        Dim dsDAL As DataSet = pldDAL.GetMaxExpirationMinEffectiveDateForPriceList(pldBO.PriceListId, pldID)

        Assert.IsNotNull(dsDAL)
        Assert.IsTrue(dsDAL.Tables(0).Rows.Count > 0)
    End Sub


    <TestMethod()> Public Sub Insert_Success()
        Dim price As Assurant.Common.Types.DecimalType = 8000
        Dim ds As DataSet = New DataSet()

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID)

        Dim newpldBO As PriceListDetail = New PriceListDetail

        With newpldBO
            .Code = pldBO.Code
            .ConditionId = pldBO.ConditionId
            .CurrencyId = pldBO.CurrencyId
            .Effective = DateAndTime.Today
            .EquipmentClassId = pldBO.EquipmentClassId
            .EquipmentId = pldBO.EquipmentId
            .Expiration = pldBO.Expiration
            .MakeId = pldBO.MakeId
            .Price = price
            .PriceBandRangeFrom = PRICE_BAND_RANGE_FROM
            .PriceBandRangeTo = PRICE_BAND_RANGE_TO
            .PriceListId = pldBO.PriceListId
            .ReplacementTaxType = pldBO.ReplacementTaxType
            .RiskTypeId = pldBO.RiskTypeId
            .ServiceClassId = pldBO.ServiceClassId
            .ServiceLevelId = pldBO.ServiceLevelId
            .ServiceTypeId = pldBO.ServiceTypeId
            .VendorSku = pldBO.VendorSku
            .VendorSkuDescription = pldBO.VendorSkuDescription
            .PriceListDetailTypeId = pldBO.PriceListDetailTypeId
        End With

        newpldBO.Save()

        pldBO = New PriceListDetail(newpldBO.Id)

        Assert.IsNotNull(pldBO)
        Assert.IsTrue(pldBO.Price.Equals(price))
    End Sub

    <TestMethod()> Public Sub Update_Success()
        Dim price As Assurant.Common.Types.DecimalType = 70001

        Dim idToUpdate As Guid = New Guid(DotNetToOracle("E7FD17BF2FBB4B49B60B1E1203244F99"))
        Dim pldBO As PriceListDetail = New PriceListDetail(idToUpdate)

        pldBO.Price = price
        pldBO.PriceBandRangeFrom = PRICE_BAND_RANGE_FROM
        pldBO.PriceBandRangeTo = PRICE_BAND_RANGE_TO

        pldBO.Save()


        pldBO = New PriceListDetail(pldID)
        Assert.IsNotNull(pldBO)
        Assert.IsTrue(pldBO.Price.Equals(price))
    End Sub

    <ExpectedException(GetType(DataNotFoundException))>
    <TestMethod()> Public Sub Delete_Success()
        Dim idToDelete As Guid = New Guid(DotNetToOracle("E7FD17BF2FBB4B49B60B1E1203244F99"))

        Dim pldBO As PriceListDetail = New PriceListDetail(idToDelete)

        pldBO.Delete()

        pldBO.Save()

        pldBO = New PriceListDetail(idToDelete)
    End Sub


    <ExpectedException(GetType(DataNotFoundException))>
    <TestMethod()> Public Sub Load_NoRecordFound()
        Dim id As Guid = New Guid("bd74ec57-6074-9bb5-e053-0c28480a9dd0")
        Dim ds As DataSet = New DataSet
        Dim recordCount As Integer = 0

        Dim pldBO As PriceListDetail = New PriceListDetail(id, ds)

        Assert.IsNotNull(ds)
        Assert.IsTrue(ds.Tables(0).Rows.Count.Equals(recordCount))
    End Sub

End Class
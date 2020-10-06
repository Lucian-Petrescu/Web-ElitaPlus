Imports System.Text
Imports System.Collections.Generic
Imports Assurant.Common.MessagePublishing

Public Class InvMgmtFileLoad
    Inherits FileLoadBase(Of ClaimloadFileProcessed, ClaimloadReconWrk)

#Region "Constructor"
    Public Sub New(ByVal threadCount As Integer, ByVal transactionSize As Integer)
        MyBase.New(True) '' Custom Save Constructor
    End Sub

    Public Sub New()
        MyBase.New(True) '' Custom Save Constructor
    End Sub
#End Region

#Region "Fields"
    Private _claimloadFileProcessed As ClaimloadFileProcessed
    Private ReadOnly YesId As Guid
    Private ReadOnly NoId As Guid
    Private _claim As Claim
#End Region

#Region "Constants"
    Private Const claim_type_Replacement = "RPL"
#End Region

#Region "Properties"
    Private Property ClaimLoadFileProcessed As ClaimloadFileProcessed
        Get
            Return _claimloadFileProcessed
        End Get
        Set(ByVal value As ClaimloadFileProcessed)
            _claimloadFileProcessed = value
        End Set
    End Property

    Private Property Claim As Claim
        Get
            Return _claim
        End Get
        Set(ByVal value As Claim)
            _claim = value
        End Set
    End Property
#End Region

    Protected Overrides Function CreateFileLoadHeader(ByVal fileLoadHeaderId As System.Guid) As ClaimloadFileProcessed
        ClaimLoadFileProcessed = New ClaimloadFileProcessed(fileLoadHeaderId)
        Return ClaimLoadFileProcessed
    End Function

    Protected Overrides Function CreateFileLoadDetail(ByVal fileLoadDetailId As System.Guid, ByVal headerRecord As ClaimloadFileProcessed) As ClaimloadReconWrk
        Dim returnValue As ClaimloadReconWrk
        returnValue = New ClaimloadReconWrk(fileLoadDetailId, headerRecord.Dataset)
        Return returnValue
    End Function

    Public Overrides Sub AfterCreateFileLoadHeader()
        MyBase.AfterCreateFileLoadHeader()
    End Sub

    Protected Overrides Function ProcessDetailRecord(ByVal reconRecord As ClaimloadReconWrk, ByVal familyDataSet As System.Data.DataSet) As ProcessResult
        Try
            Dim Claim As Claim
            Dim claimShipping As ClaimShipping

            Dim ShipType_To_SC_Id As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SHIPPING_TYPES, Codes.SHIP_TYPE_TO_SC)
            Dim ShipType_To_CUST_Id As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SHIPPING_TYPES, Codes.SHIP_TYPE_TO_CUST)

            ' Create Instance of Claim based on Recon Record
            Claim = ClaimFacade.Instance.GetClaimByDealerCodeandClaimNumber(Of Claim)(reconRecord.DealerCode, reconRecord.ClaimNumber) '' Create New DataSet
            Claim.RepairDate = reconRecord.RepairDate

            'check if the claim type is replacement
            If ((reconRecord.ClaimType = claim_type_Replacement) AndAlso (Not reconRecord.DeliveryDate Is Nothing)) Then
                Dim replacedEquipment As ClaimEquipment
                Dim claimEquipmentReplacementId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT)
                replacedEquipment = Claim.ClaimEquipmentChildren.Where(Function(ce) ce.ClaimEquipmentTypeId = claimEquipmentReplacementId).FirstOrDefault()
                If (replacedEquipment Is Nothing) Then
                    replacedEquipment = New ClaimEquipment(Claim.Dataset)
                    replacedEquipment.ClaimId = Claim.Id
                    replacedEquipment.ClaimEquipmentTypeId = claimEquipmentReplacementId
                End If

                With replacedEquipment
                    .Model = reconRecord.ReplacementModel
                    .SerialNumber = reconRecord.ReplacementSerialNumber
                    .ManufacturerId = Manufacturer.ResolveManufacturer(reconRecord.ReplacementManufacturer, Claim.Company.CompanyGroupId)
                    .SKU = reconRecord.ReplacementSku
                    .DeviceTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_DEVICE, reconRecord.ReplacementType)
                    .Save()
                End With

                Dim oCertItem As New CertItem(Claim.CertificateItem.Id)
                oCertItem.Model = reconRecord.ReplacementModel
                oCertItem.SerialNumber = reconRecord.ReplacementSerialNumber
                oCertItem.ManufacturerId = Manufacturer.ResolveManufacturer(reconRecord.ReplacementManufacturer, Claim.Company.CompanyGroupId)
                oCertItem.SkuNumber = reconRecord.ReplacementSku
                oCertItem.Save()
            End If

            '***Save Claim Shipping information***'
            claimShipping = New ClaimShipping()

            claimShipping.ClaimId = Claim.Id
            'Date device shipped to customer
            If (Not reconRecord.DeliveryDate Is Nothing) Then
                claimShipping.ShippingTypeId = ShipType_To_CUST_Id
                claimShipping.TrackingNumber = reconRecord.TrackingNumberToCust
                claimShipping.ShippingDate = reconRecord.DeliveryDate
                claimShipping.ReceivedDate = reconRecord.PickupDate
            ElseIf (Not reconRecord.DateDeviceShippedToSC Is Nothing) Then 'Date device shipped to SC
                claimShipping.ShippingTypeId = ShipType_To_SC_Id
                claimShipping.TrackingNumber = reconRecord.TrackingNumberToSC
                claimShipping.ShippingDate = reconRecord.DateDeviceShippedToSC
            End If

            claimShipping.Save()
            Claim.Save()

            ' Trigger extended statuses
            If (Not reconRecord.DeliveryDate Is Nothing) Then
                'Date device shipped to customer
                With Claim
                    PublishedTask.AddEvent(companyGroupId:=.Company.CompanyGroupId, _
                                           companyId:=.CompanyId, _
                                           countryId:=.Company.CountryId, _
                                           dealerId:=.Dealer.Id, _
                                           productCode:=.Certificate.ProductCode, _
                                           coverageTypeId:=.CertificateItemCoverage.CoverageTypeId, _
                                           sender:="Inventory Management File", _
                                           arguments:="ClaimId:" & DALBase.GuidToSQLString(.Id), _
                                           eventDate:=DateTime.UtcNow, _
                                           eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__PRD_SHPD_C), _
                                           eventArgumentId:=Nothing)
                End With
            ElseIf (Not reconRecord.DateDeviceShippedToSC Is Nothing) Then
                'Date device shipped to SC
                With Claim
                    PublishedTask.AddEvent(companyGroupId:=.Company.CompanyGroupId, _
                                           companyId:=.CompanyId, _
                                           countryId:=.Company.CountryId, _
                                           dealerId:=.Dealer.Id, _
                                           productCode:=.Certificate.ProductCode, _
                                           coverageTypeId:=.CertificateItemCoverage.CoverageTypeId, _
                                           sender:="Inventory Management File", _
                                           arguments:="ClaimId:" & DALBase.GuidToSQLString(.Id), _
                                           eventDate:=DateTime.UtcNow, _
                                           eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__SHIPPED_TO_SERVICE_CENTER), _
                                           eventArgumentId:=Nothing)
                End With
            End If

            Return (ProcessResult.Loaded)
        Catch ex As DataBaseAccessException
            Common.AppConfig.Log(DirectCast(ex, Exception))
            If (ex.ErrorType = DataBaseAccessException.DatabaseAccessErrorType.BusinessErr) Then
                If (ex.Code Is Nothing OrElse ex.Code.Trim().Length = 0) Then
                    reconRecord.RejectReason = "Rejected During Load process"
                Else
                    reconRecord.RejectReason = TranslationBase.TranslateLabelOrMessage(ex.Code)
                    reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
                End If
            Else
                reconRecord.RejectReason = "Rejected During Load process"
            End If
            reconRecord.RejectCode = "000"
            Return ProcessResult.Rejected
        Catch ex As BOValidationException
            Common.AppConfig.Log(DirectCast(ex, Exception))
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = ex.ToRejectReason()
            reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
            Return ProcessResult.Rejected
        Catch ex As Exception
            Common.AppConfig.Log(ex)
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = "Rejected During Load process"
            Return ProcessResult.Rejected
        End Try
    End Function

    Protected Overrides Sub CustomSave(ByVal headerRecord As ClaimloadFileProcessed)
        MyBase.CustomSave(headerRecord)
        headerRecord.Save()
    End Sub
End Class

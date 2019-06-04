<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" indent="no" />
  <xsl:decimal-format name="number_format" decimal-separator="." grouping-separator="," NaN="0.00"/>

  <xsl:param name="report_type" />
  <xsl:param name="field_separator" />
  <xsl:param name="created_date" />
  <xsl:param name="file_name" />

  <xsl:variable name="newline">
    <xsl:text>
</xsl:text>
  </xsl:variable>

  <xsl:variable name="date_separator">
    <xsl:text>/</xsl:text>
  </xsl:variable>
  
  <xsl:template match="/">

    <xsl:choose>
      <xsl:when test="$report_type='CUSTOMER'">
        <xsl:call-template name="AGCustomer" />
      </xsl:when>
      <xsl:when test="$report_type='ADDRESS'">
        <xsl:call-template name="AGAddress" />
      </xsl:when>
      <xsl:when test="$report_type='SUPPLIER'">
        <xsl:call-template name="AGSupplier" />
      </xsl:when>
      <xsl:when test="$report_type='BANKDETAILS'">
        <xsl:call-template name="AGBankDetails" />
      </xsl:when>
      <xsl:when test="$report_type='ACCOUNT'">
        <xsl:call-template name="AGAccount" />
      </xsl:when>
      <xsl:when test="$report_type='JOURNAL'">
        <xsl:call-template name="AGJournal" />
      </xsl:when>
      <xsl:when test="$report_type='SSINVOICE'">
        <xsl:call-template name="Invoice" />
      </xsl:when>
      <xsl:when test="$report_type='SSJOURNAL'">
        <xsl:call-template name="Journal" />
      </xsl:when>
    </xsl:choose>

  </xsl:template>

  <xsl:template name="AGCustomer">

    <xsl:value-of select="concat(
    'ActualAccount', $field_separator, 
    'CompanyAddressCode', $field_separator,
    'CustomerCode', $field_separator,
    'DefaultCurrencyCode', $field_separator,
    'Description', $field_separator,
    'DirectDebitPayments', $field_separator,
    'EmailAddress', $field_separator,
    'LookupCode', $field_separator,
    'Name', $field_separator,
    'PaymentTermsGroupCode', $field_separator,
    'ShortHeading', $field_separator,
    'Status', $field_separator,
    'File Created Date', $field_separator,
    'File Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/Customer">
      <xsl:value-of select="concat(ActualAccount, $field_separator)"/>
      <xsl:value-of select="concat(CompanyAddressCode, $field_separator)"/>
      <xsl:value-of select="concat(CustomerCode, $field_separator)"/>
      <xsl:value-of select="concat(DefaultCurrencyCode, $field_separator)"/>
      <xsl:value-of select ="concat(Description, $field_separator)"/>
      <xsl:value-of select ="concat(DirectDebitPayments, $field_separator)"/>
      <xsl:value-of select ="concat(EmailAddress, $field_separator)"/>
      <xsl:value-of select ="concat(LookupCode, $field_separator)"/>
      <xsl:value-of select ="concat(Name, $field_separator)"/>
      <xsl:value-of select ="concat(PaymentTermsGroupCode, $field_separator)"/>
      <xsl:value-of select="concat(ShortHeading, $field_separator)"/>
      <xsl:value-of select ="concat(Status, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="AGAddress">

    <xsl:value-of select="concat(
    'AddressCode', $field_separator,
    'AddressLine1', $field_separator,
    'Area', $field_separator,
    'Country', $field_separator,
    'CountryCode', $field_separator,
    'LookupCode', $field_separator,
    'PostalCode', $field_separator,
    'ShortHeading', $field_separator,
    'State', $field_separator,
    'StateCode', $field_separator,
    'TownCity', $field_separator,
    'Status', $field_separator,
    'TelePhoneNumber', $field_separator,
    'FaxNumber', $field_separator,
    'File_Created_Date', $field_separator,
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/Address">
      <xsl:value-of select="concat(AddressCode, $field_separator)"/>
      <xsl:value-of select="concat(AddressLine1, $field_separator)"/>
      <xsl:value-of select="concat(Area, $field_separator)"/>
      <xsl:value-of select="concat(Country, $field_separator)"/>
      <xsl:value-of select="concat(CountryCode, $field_separator)"/>
      <xsl:value-of select="concat(LookupCode, $field_separator)"/>
      <xsl:value-of select="concat(PostalCode, $field_separator)"/>
      <xsl:value-of select="concat(ShortHeading, $field_separator)"/>
      <xsl:value-of select="concat(State, $field_separator)"/>
      <xsl:value-of select="concat(StateCode, $field_separator)"/>
      <xsl:value-of select="concat(TownCity, $field_separator)"/>
      <xsl:value-of select="concat(Status, $field_separator)"/>
      <xsl:value-of select="concat(TelePhoneNumber, $field_separator)"/>
      <xsl:value-of select="concat(FaxNumber, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="AGSupplier">

    <xsl:value-of select="concat(
    'AddressCode', $field_separator,
    'AddressLine1', $field_separator,
    'Area', $field_separator,
    'Country', $field_separator,
    'CountryCode', $field_separator,
    'LookupCode', $field_separator,
    'PostalCode', $field_separator,
    'ShortHeading', $field_separator,
    'State', $field_separator,
    'StateCode', $field_separator,
    'TownCity', $field_separator,
    'Status', $field_separator,
    'TelePhoneNumber', $field_separator,
    'FaxNumber', $field_separator,
    'File_Created_Date', $field_separator,
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/Supplier">
      <xsl:value-of select="concat(AccountCode, $field_separator)"/>
      <xsl:value-of select="concat(CompanyAddressCode, $field_separator)"/>
      <xsl:value-of select="concat(DefaultCurrencyCode, $field_separator)"/>
      <xsl:value-of select="concat(PayToAddressCode, $field_separator)"/>
      <xsl:value-of select="concat(PaymentMethod, $field_separator)"/>
      <xsl:value-of select="concat(PaymentTermsGroupCode, $field_separator)"/>
      <xsl:value-of select="concat(ShortHeading, $field_separator)"/>
      <xsl:value-of select="concat(Status, $field_separator)"/>
      <xsl:value-of select="concat(SupplierCode, $field_separator)"/>
      <xsl:value-of select="concat(SupplierName, $field_separator)"/>
      <xsl:value-of select="concat(ConversionCodeControl, $field_separator)"/>
      <xsl:value-of select="concat(ReportConversionControl, $field_separator)"/>
      <xsl:for-each select="Analysis1">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:for-each select="Analysis2">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:choose>
        <xsl:when test="/Analysis3">
          <xsl:for-each select="Analysis3">
            <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
            <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="concat($field_separator, $field_separator)"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:for-each select="Analysis4">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:for-each select="Analysis5">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:for-each select="Analysis6">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:for-each select="Analysis7">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:for-each select="Analysis8">
        <xsl:value-of select="concat(VSuppCatAnalysis_SuppCode, $field_separator)"/>
        <xsl:value-of select="concat(VSuppCatAnalysis_AnlCode, $field_separator)"/>
      </xsl:for-each>
      <xsl:value-of select="concat(Description, $field_separator)"/>
      <xsl:value-of select="concat(LookupCode, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="AGBankDetails">

    <xsl:value-of select="concat(
    'BankDetailsCode', $field_separator,
    'SupplierCode', $field_separator,
    'BankSubCode', $field_separator,
    'BankName', $field_separator,
    'BankAccountNumber', $field_separator,
    'BankBranch', $field_separator,
    'BankSortCode', $field_separator,
    'BankAddressCode', $field_separator,
    'BankAccountName', $field_separator,
    'LookupCode', $field_separator,
    'TransactionLimit', $field_separator,
    'File_Created_Date', $field_separator,
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/BankDetails">
      <xsl:value-of select="concat(BankDetailsCode, $field_separator)"/>
      <xsl:value-of select="concat(SupplierCode, $field_separator)"/>
      <xsl:value-of select="concat(BankSubcode, $field_separator)"/>
      <xsl:value-of select="concat(BankName, $field_separator)"/>
      <xsl:value-of select="concat(BankAccountNumber, $field_separator)"/>
      <xsl:value-of select="concat(BankBranch, $field_separator)"/>
      <xsl:value-of select="concat(BankSortCode, $field_separator)"/>
      <xsl:value-of select="concat(BankAddressCode, $field_separator)"/>
      <xsl:value-of select="concat(BankAccountName, $field_separator)"/>
      <xsl:value-of select="concat(LookupCode, $field_separator)"/>
      <xsl:value-of select="concat(format-number(TransactionLimit, '#,###.00', 'number_format'), $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="AGAccount">

    <xsl:value-of select="concat(
    'AccountCode', $field_separator,
    'AccountType', $field_separator,
    'BalanceType', $field_separator,
    'DefaultCurrencyCode', $field_separator,
    'ConversionCodeControl', $field_separator,
    'EnterAnalysis1', $field_separator,
    'EnterAnalysis2', $field_separator,
    'EnterAnalysis3', $field_separator,
    'EnterAnalysis4', $field_separator,
    'EnterAnalysis5', $field_separator,
    'EnterAnalysis6', $field_separator,
    'EnterAnalysis7', $field_separator,
    'EnterAnalysis8', $field_separator,
    'EnterAnalysis9', $field_separator,
    'EnterAnalysis10', $field_separator,
    'LongDescription', $field_separator,
    'LookupCode', $field_separator,
    'ReportConversionControl', $field_separator,
    'ShortHeading', $field_separator,
    'File_Created_Date', $field_separator,
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/Accounts">
      <xsl:value-of select="concat(AccountCode, $field_separator)"/>
      <xsl:value-of select="concat(AccountType, $field_separator)"/>
      <xsl:value-of select="concat(BalanceType, $field_separator)"/>
      <xsl:value-of select="concat(DefaultCurrencyCode, $field_separator)"/>
      <xsl:value-of select="concat(ConversionCodeControl, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis1, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis2, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis3, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis4, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis5, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis6, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis7, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis8, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis9, $field_separator)"/>
      <xsl:value-of select="concat(EnterAnalysis10, $field_separator)"/>
      <xsl:value-of select="concat(LongDescription, $field_separator)"/>
      <xsl:value-of select="concat(LookupCode, $field_separator)"/>
      <xsl:value-of select="concat(ReportConversionControl, $field_separator)"/>
      <xsl:value-of select="concat(ShortHeading, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="AGJournal">

    <xsl:value-of select="concat(                
    'AccountCode', $field_separator, 
    'AccountingPeriod', $field_separator, 
    'CurrencyCode', $field_separator, 
    'DebitCredit', $field_separator, 
    'Description', $field_separator, 
    'JournalSource', $field_separator, 
    'JournalType', $field_separator, 
    'TransactionAmount', $field_separator, 
    'TransactionAmountDecimalPlaces', $field_separator, 
    'TransactionDate', $field_separator, 
    'TransactionReference', $field_separator, 
    'AnalysisCode1', $field_separator, 
    'AnalysisCode2', $field_separator, 
    'AnalysisCode3', $field_separator, 
    'AnalysisCode4', $field_separator, 
    'AnalysisCode5', $field_separator, 
    'AnalysisCode6', $field_separator, 
    'AnalysisCode7', $field_separator, 
    'AnalysisCode8', $field_separator, 
    'AnalysisCode9', $field_separator, 
    'AnalysisCode10', $field_separator, 
    'File_Created_Date', $field_separator, 
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SSC/Payload/Ledger/Line">
      <xsl:value-of select="concat(AccountCode, $field_separator)"/>
      <xsl:value-of select="concat(AccountingPeriod, $field_separator)"/>
      <xsl:value-of select="concat(CurrencyCode, $field_separator)"/>
      <xsl:value-of select="concat(DebitCredit, $field_separator)"/>
      <xsl:value-of select="concat(Description, $field_separator)"/>
      <xsl:value-of select="concat(JournalSource, $field_separator)"/>
      <xsl:value-of select="concat(JournalType, $field_separator)"/>
      <xsl:value-of select="concat(format-number(TransactionAmount, '#,###.00', 'number_format'), $field_separator)"/>
      <xsl:value-of select="concat(TransactionAmountDecimalPlaces, $field_separator)"/>
      <xsl:value-of select="concat(substring(TransactionDate,3,2), $date_separator, substring(TransactionDate,1,2), $date_separator, substring(TransactionDate,5,4), $field_separator)"/>
      <xsl:value-of select="concat(TransactionReference, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode1, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode2, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode3, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode4, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode5, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode6, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode7, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode8, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode9, $field_separator)"/>
      <xsl:value-of select="concat(AnalysisCode10, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="Invoice">

    <xsl:value-of select="concat(
    'User_ID', $field_separator, 
    'Location_Code', $field_separator, 
    'Invoice_Number', $field_separator, 
    'Invoice_Date', $field_separator, 
    'Payment_Method', $field_separator, 
    'Payable_Entity', $field_separator, 
    'Line_Number', $field_separator, 
    'Line_Amount', $field_separator, 
    'Entity_Number', $field_separator, 
    'Account_Number', $field_separator, 
    'Cost_Center', $field_separator, 
    'LOB', $field_separator, 
    'Business_Primary', $field_separator, 
    'Business_Secondary', $field_separator, 
    'Business_Type', $field_separator, 
    'Block_Business', $field_separator, 
    'Distribution_Channel', $field_separator, 
    'Invoice_Description', $field_separator, 
    'OneTime_Vendor', $field_separator, 
    'Vendor_ID', $field_separator, 
    'File_Created_Date', $field_separator, 
    'File_Name', $newline)"></xsl:value-of>

    <xsl:for-each select="SmartStreamInvoice/Invoice">
      <xsl:value-of select="concat(User_ID, $field_separator)"/>
      <xsl:value-of select="concat(Location_Code, $field_separator)"/>
      <xsl:value-of select="concat(Invoice_Number, $field_separator)"/>
      <xsl:value-of select="concat(Invoice_Date, $field_separator)"/>
      <xsl:value-of select="concat(Payment_Method, $field_separator)"/>
      <xsl:value-of select="concat(Payable_Entity, $field_separator)"/>
      <xsl:value-of select="concat(Line_Number, $field_separator)"/>
      <xsl:value-of select="concat(format-number(Line_Amount, '#,###.00', 'number_format'), $field_separator)"/>
      <xsl:value-of select="concat(Entity_Number, $field_separator)"/>
      <xsl:value-of select="concat(Account_Number, $field_separator)"/>
      <xsl:value-of select="concat(Cost_Center, $field_separator)"/>
      <xsl:value-of select="concat(LOB, $field_separator)"/>
      <xsl:value-of select="concat(Business_Primary, $field_separator)"/>
      <xsl:value-of select="concat(Business_Secondary, $field_separator)"/>
      <xsl:value-of select="concat(Business_Type, $field_separator)"/>
      <xsl:value-of select="concat(Block_Business, $field_separator)"/>
      <xsl:value-of select="concat(Distribution_Channel, $field_separator)"/>
      <xsl:value-of select="concat(Invoice_Description, $field_separator)"/>
      <xsl:value-of select="concat(OneTime_Vendor, $field_separator)"/>
      <xsl:value-of select="concat(Vendor_ID, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="Journal">

    <xsl:value-of select="concat(
    'Entity_ID', $field_separator, 
    'JournalID', $field_separator, 
    'EffectiveTimePeriod', $field_separator, 
    'JournalSequenceNumber', $field_separator, 
    'JournalEntryLineID', $field_separator, 
    'GLDestinationEntityID', $field_separator, 
    'Account', $field_separator, 
    'CostCenterID', $field_separator, 
    'LineOfBusiness', $field_separator, 
    'PrimaryBusiness', $field_separator, 
    'SecondaryBusiness', $field_separator, 
    'BusinessType', $field_separator, 
    'BlockOfBusiness', $field_separator, 
    'DistributionChannel', $field_separator, 
    'UDF', $field_separator, 
    'JournalUserAlphaField1', $field_separator, 
    'JournalUserAlphaField2', $field_separator, 
    'AmountClass1', $field_separator, 
    'SourceDocumentReference', $field_separator, 
    'Description', $field_separator, 
    'PrimaryDRCRCode', $field_separator, 
    'PrimaryAmount', $field_separator, 
    'TransactionCurrencyCode', $field_separator, 
    'PrimaryCurrencyCode', $field_separator, 
    'TransactionAmount', $field_separator, 
    'EntryCreatorID', $field_separator, 
    'ValidateAccount', $newline)"/>

    <xsl:for-each select="SmartStreamJournal/JournalHeader/JournalEntry">
      <xsl:value-of select="concat(GLEntityID, $field_separator)"/>
      <xsl:value-of select="concat(JournalID, $field_separator)"/>
      <xsl:value-of select="concat(EffectiveTimePeriod, $field_separator)"/>
      <xsl:value-of select="concat(JournalSequenceNumber, $field_separator)"/>
      <xsl:value-of select="concat(JournalEntryLineID, $field_separator)"/>
      <xsl:value-of select="concat(GLDestinationEntityID, $field_separator)"/>
      <xsl:value-of select="concat(Account, $field_separator)"/>
      <xsl:value-of select="concat(CostCenterID, $field_separator)"/>
      <xsl:value-of select="concat(LineOfBusiness, $field_separator)"/>
      <xsl:value-of select="concat(PrimaryBusiness, $field_separator)"/>
      <xsl:value-of select="concat(SecondaryBusiness, $field_separator)"/>
      <xsl:value-of select="concat(BusinessType, $field_separator)"/>
      <xsl:value-of select="concat(BlockOfBusiness, $field_separator)"/>
      <xsl:value-of select="concat(DistributionChannel, $field_separator)"/>
      <xsl:value-of select="concat(UDF, $field_separator)"/>
      <xsl:value-of select="concat(JournalUserAlphaField1, $field_separator)"/>
      <xsl:value-of select="concat(JournalUserAlphaField2, $field_separator)"/>
      <xsl:value-of select="concat(AmountClass1, $field_separator)"/>
      <xsl:value-of select="concat(SourceDocumentReference, $field_separator)"/>
      <xsl:value-of select="concat(Description, $field_separator)"/>
      <xsl:value-of select="concat(PrimaryDRCRCode, $field_separator)"/>
      <xsl:value-of select="concat(format-number(PrimaryAmount, '#,###.00', 'number_format'), $field_separator)"/>
      <xsl:value-of select="concat(TransactionCurrencyCode, $field_separator)"/>
      <xsl:value-of select="concat(PrimaryCurrencyCode, $field_separator)"/>
      <xsl:value-of select="concat(format-number(TransactionAmount, '#,###.00', 'number_format'), $field_separator)"/>
      <xsl:value-of select="concat(EntryCreatorID, $field_separator)"/>
      <xsl:value-of select="concat(ValidateAccount, $field_separator)"/>
      <xsl:value-of select="concat($created_date, $field_separator)"/>
      <xsl:value-of select="concat($file_name, $newline)"/>
    </xsl:for-each>

  </xsl:template>

</xsl:stylesheet>
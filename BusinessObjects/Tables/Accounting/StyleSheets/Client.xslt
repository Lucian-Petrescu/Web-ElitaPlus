<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="xml" encoding="UTF-8" indent="yes" />
  <xsl:template match="/">
    <SSC>
      <SunSystemsContext>
        <BusinessUnit>
          <xsl:value-of select="a:SSC/a:SunSystemsContext/a:BUSINESSUNIT"/>
        </BusinessUnit>
      </SunSystemsContext>
      <Payload>
        <xsl:for-each select="a:SSC/a:VENDOR">
          <xsl:if test="a:ACCOUNTTYPE = 0">
            <Customer>
              <ActualAccount>
                <xsl:value-of select="a:ACCOUNTCODE"/>
              </ActualAccount>
              <CompanyAddressCode>
                <xsl:value-of select="a:ACCOUNTCODE"/>
              </CompanyAddressCode>
              <CustomerCode>
                <xsl:value-of select="a:ACCOUNTCODE"/>
              </CustomerCode>
              <xsl:if test="a:DEFAULTCURRENCYCODE">
                <DefaultCurrencyCode>
                  <xsl:value-of select="a:DEFAULTCURRENCYCODE"/>
                </DefaultCurrencyCode>
              </xsl:if>
              <xsl:choose>
                <xsl:when test="a:BENEFICIARY">
                  <Description>
                    <xsl:value-of select="substring(a:BENEFICIARY, 1, 50)"/>
                  </Description>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:if test="a:LONGDESCRIPTION">
                    <Description>
                      <xsl:value-of select="substring(a:LONGDESCRIPTION, 1, 50)"/>
                    </Description>
                  </xsl:if>
                </xsl:otherwise>
              </xsl:choose>
              <xsl:if test="a:DATAACCESSGROUPCODE">
                <DataAccessGroupCode>
                  <xsl:value-of select="a:DATAACCESSGROUPCODE"/>
                </DataAccessGroupCode>
              </xsl:if>
              <xsl:if test="a:BANKSUBCODE">
                <DefaultBankSubcode>
                  <xsl:value-of select="a:BANKSUBCODE"/>
                </DefaultBankSubcode>
              </xsl:if>
              <DirectDebitPayments>1</DirectDebitPayments>
              <xsl:if test="a:EMAILADDRESS">
                <EmailAddress>
                  <xsl:value-of select="a:EMAILADDRESS"/>
                </EmailAddress>
              </xsl:if>
              <xsl:if test="a:SUPPLIERLOOKUPCODE">
                <LookupCode>
                  <xsl:value-of select="a:SUPPLIERLOOKUPCODE"/>
                </LookupCode>
              </xsl:if>
              <xsl:if test="a:SUPPLIERNAME">
                <Name>
                  <xsl:value-of select="a:SUPPLIERNAME"/>
                </Name>
              </xsl:if>
              <xsl:if test="a:PAYMENTMETHOD">
                <PaymentMethod>
                  <xsl:value-of select="a:PAYMENTMETHOD"/>
                </PaymentMethod>
              </xsl:if>
              <PaymentTermsGroupCode>
                <xsl:value-of select="a:PAYMENTTERMS"/>
              </PaymentTermsGroupCode>
              <ShortHeading>DEBTOR</ShortHeading>
              <xsl:if test="a:SUPPLIERSTATUS">
                <Status>
                  <xsl:value-of select="a:SUPPLIERSTATUS"/>
                </Status>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS1">
                <Analysis1>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS1"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis1>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS2">
                <Analysis2>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS2"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis2>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS3">
                <Analysis3>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS3"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis3>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS4">
                <Analysis4>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS4"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis4>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS5">
                <Analysis5>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS5"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis5>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS6">
                <Analysis6>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS6"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis6>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS7">
                <Analysis7>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS7"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis7>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS8">
                <Analysis8>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS8"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis8>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS9">
                <Analysis9>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS9"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis9>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS10">
                <Analysis10>
                  <VCustCatAnalysis_CustCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VCustCatAnalysis_CustCode>
                  <VCustCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS10"/>
                  </VCustCatAnalysis_AnlCode>
                </Analysis10>
              </xsl:if>
            </Customer>
          </xsl:if>
        </xsl:for-each>
      </Payload>
    </SSC>
  </xsl:template>
</xsl:stylesheet>
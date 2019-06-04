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
          <xsl:if test="a:ACCOUNTTYPE = 1">
            <Supplier>
              <AccountCode>
                <xsl:value-of select="a:ACCOUNTCODE"/>
              </AccountCode>
              <CompanyAddressCode>
                <xsl:value-of select="a:ACCOUNTCODE"/>
              </CompanyAddressCode>
              <xsl:if test="a:DATAACCESSGROUPCODE">
                <DataAccessGroupCode>
                  <xsl:value-of select="a:DATAACCESSGROUPCODE"/>
                </DataAccessGroupCode>
              </xsl:if>
              <xsl:if test="a:DEFAULTCURRENCYCODE">
                <DefaultCurrencyCode>
                  <xsl:value-of select="a:DEFAULTCURRENCYCODE"/>
                </DefaultCurrencyCode>
              </xsl:if>
              <xsl:if test="a:ACCOUNTCODE">
                <PayToAddressCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </PayToAddressCode>
              </xsl:if>
              <xsl:if test="a:PAYMENTMETHOD">
                <PaymentMethod>
                  <xsl:value-of select="a:PAYMENTMETHOD"/>
                </PaymentMethod>
              </xsl:if>
              <PaymentTermsGroupCode>
                <xsl:value-of select="a:PAYMENTTERMS"/>
              </PaymentTermsGroupCode>
              <ShortHeading>CREDITOR</ShortHeading>
              <xsl:if test="a:SUPPLIERSTATUS">
                <Status>
                  <xsl:value-of select="a:SUPPLIERSTATUS"/>
                </Status>
              </xsl:if>
              <xsl:if test="a:ACCOUNTCODE">
                <SupplierCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </SupplierCode>
              </xsl:if>
              <xsl:if test="a:SUPPLIERNAME">
                <SupplierName>
                  <xsl:value-of select="a:SUPPLIERNAME"/>
                </SupplierName>
              </xsl:if>
              <xsl:if test="a:CONVERSIONCODECONTROL">
                <ConversionCodeControl>
                  <xsl:value-of select="a:CONVERSIONCODECONTROL"/>
                </ConversionCodeControl>
              </xsl:if>
              <xsl:if test="a:REPORTCONVERSIONCONTROL">
                <ReportConversionControl>
                  <xsl:value-of select="a:REPORTCONVERSIONCONTROL"/>
                </ReportConversionControl>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS1">
                <Analysis1>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS1"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis1>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS2">
                <Analysis2>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS2"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis2>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS3">
                <Analysis3>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS3"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis3>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS4">
                <Analysis4>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS4"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis4>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS5">
                <Analysis5>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS5"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis5>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS6">
                <Analysis6>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS6"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis6>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS7">
                <Analysis7>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS7"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis7>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS8">
                <Analysis8>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS8"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis8>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS9">
                <Analysis9>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS9"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis9>
              </xsl:if>
              <xsl:if test="a:SUPPLIERANALYSIS10">
                <Analysis10>
                  <VSuppCatAnalysis_SuppCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </VSuppCatAnalysis_SuppCode>
                  <VSuppCatAnalysis_AnlCode>
                    <xsl:value-of select="a:SUPPLIERANALYSIS10"/>
                  </VSuppCatAnalysis_AnlCode>
                </Analysis10>
              </xsl:if>
              <!-- payment method = 1 = EFT -->
              <xsl:choose>
                <xsl:when test="a:SUPPLIERANALYSIS1 = 'EP'">
                  <Priority>
                    <xsl:text>1</xsl:text>
                  </Priority>
                  <xsl:if test="a:DEFAULTBANKSUBCODE">
                    <DefaultBankSubcode>
                      <xsl:value-of select="a:DEFAULTBANKSUBCODE"/>
                    </DefaultBankSubcode>
                  </xsl:if>
                  <xsl:choose>
                    <xsl:when test="a:BANKACCOUNTNAME">
                      <Description>
                        <xsl:value-of select="substring(a:BANKACCOUNTNAME, 1, 50)"/>
                      </Description>
                    </xsl:when>
                    <xsl:otherwise>
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
                    </xsl:otherwise>
                  </xsl:choose>
                  <xsl:if test="a:BANKDETAILSLOOKUPCODE">
                    <LookupCode>
                      <xsl:value-of select="a:BANKDETAILSLOOKUPCODE"/>
                    </LookupCode>
                    <Tax>
                      <SequenceNumber>
                        <xsl:text>1</xsl:text>
                      </SequenceNumber>
                      <TaxIdentificationCode>
                        <xsl:value-of select="a:BANKDETAILSLOOKUPCODE"/>
                      </TaxIdentificationCode>
                    </Tax>
                  </xsl:if>
                </xsl:when>
                <xsl:otherwise>
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
                  <xsl:if test="a:SUPPLIERLOOKUPCODE">
                    <LookupCode>
                      <xsl:value-of select="a:SUPPLIERLOOKUPCODE"/>
                    </LookupCode>
                  </xsl:if>
                </xsl:otherwise>
              </xsl:choose>
            </Supplier>
          </xsl:if>
        </xsl:for-each>
      </Payload>
    </SSC>
  </xsl:template>

</xsl:stylesheet>
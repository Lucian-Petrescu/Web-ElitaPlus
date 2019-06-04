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
          <xsl:if test='a:INCLUDEBANKDETAILS = "Y"'>
            <xsl:if test="a:BANKACCOUNTNUMBER">
              <BankDetails>
                <xsl:if test="a:ACCOUNTCODE">
                  <BankDetailsCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </BankDetailsCode>
                </xsl:if>
                <xsl:if test="a:ACCOUNTCODE">
                  <SupplierCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </SupplierCode>
                </xsl:if>
                <xsl:if test="a:BANKSUBCODE">
                  <BankSubcode>
                    <xsl:value-of select="a:BANKSUBCODE"/>
                  </BankSubcode>
                </xsl:if>
                <xsl:choose>
                  <xsl:when test="a:BANKNAME">
                    <BankName>
                      <xsl:value-of select="a:BANKNAME"/>
                    </BankName>
                  </xsl:when>
                  <xsl:otherwise>
                    <BankName>
                      <xsl:value-of select="a:BANK_ID"/>
                    </BankName>
                  </xsl:otherwise>
                </xsl:choose>

                <!-- FELITA EFT LOGIC for FELITA-->
                <xsl:choose>
                  <xsl:when test="a:IBAN_NUMBER">
                    <BankBranch>
                      <xsl:value-of select="a:IBAN_NUMBER"/>
                    </BankBranch>
                    <BankAccountNumber>
                      <xsl:value-of select="a:BANKACCOUNTNUMBER"/>
                    </BankAccountNumber>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:choose>
                      <xsl:when test='a:BANKSUBCODE = "ARCI2" and substring(a:BANKSORTCODE,1,3) = "072"'>
                        <!--Argentina CITI -->
                        <BankAccountNumber>
                          <xsl:value-of select="a:BANK_ID"/>
                          <xsl:value-of select="a:BANKACCOUNTNUMBER"/>
                        </BankAccountNumber>
                        <BankBranch>
                          <xsl:text>0000000000000000000000</xsl:text>
                        </BankBranch>
                      </xsl:when>
                      <xsl:otherwise>
                        <BankBranch>
                          <xsl:if test="a:COUNTRYCODE = 'AR'">
                            <xsl:value-of select="a:BANK_ID"/>
                          </xsl:if>
                          <xsl:value-of select="a:BANKACCOUNTNUMBER"/>
                        </BankBranch>
                        <BankAccountNumber>
                          <xsl:text>0000000000000000000000</xsl:text>
                        </BankAccountNumber>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="a:SWIFT_CODE">
                    <BankSortCode>
                      <xsl:value-of select="a:SWIFT_CODE"/>
                    </BankSortCode>
                  </xsl:when>
                  <xsl:otherwise>
                    <BankSortCode>
                      <xsl:value-of select="a:BANKSORTCODE"/>
                    </BankSortCode>
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:if test="a:ACCOUNTCODE">
                  <BankAddressCode>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </BankAddressCode>
                </xsl:if>
                <xsl:if test="a:BANKACCOUNTNAME">
                  <BankAccountName>
                    <xsl:value-of select="a:BANKACCOUNTNAME"/>
                  </BankAccountName>
                </xsl:if>
                <xsl:if test="a:BANKDETAILSLOOKUPCODE">
                  <LookupCode>
                    <xsl:value-of select="a:BANKDETAILSLOOKUPCODE"/>
                  </LookupCode>
                </xsl:if>
                <xsl:if test="a:TRANSACTIONLIMIT">
                  <TransactionLimit>
                    <xsl:value-of select="a:TRANSACTIONLIMIT"/>
                  </TransactionLimit>
                </xsl:if>
              </BankDetails>
            </xsl:if>
          </xsl:if>
        </xsl:for-each>
      </Payload>
    </SSC>
  </xsl:template>
</xsl:stylesheet>

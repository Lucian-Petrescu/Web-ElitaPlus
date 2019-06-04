<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"  />
  <xsl:template match="/">
    <SmartStreamJournal>
      <xsl:element name="JournalHeader">
        <xsl:attribute name="JournalHeaderID">
          <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:JOURNALHEADERID"/>
        </xsl:attribute>
        <xsl:attribute name="BatchID">
          <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:TIMESTAMP"/>
        </xsl:attribute>
        <xsl:for-each select="a:SSC/a:Line">
          <xsl:if test="a:TRANSACTIONAMOUNT != 0">
            <JournalEntry>
              <GLEntityID>
                <xsl:value-of select="a:JOURNALTYPE"/>
              </GLEntityID>
              <JournalID>
                <xsl:value-of select="a:JOURNAL_ID_SUFFIX"/>
              </JournalID>
              <EffectiveTimePeriod>
                <xsl:value-of select="substring(a:TRANSACTIONDATE,5,4)"/>
                <xsl:text>-</xsl:text>
                <xsl:value-of select="substring(a:TRANSACTIONDATE,3,2)"/>
                <xsl:text>-</xsl:text>
                <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)"/>
              </EffectiveTimePeriod>
              <JournalSequenceNumber>
                <xsl:value-of select="a:JOURNALSEQUENCE"/>
              </JournalSequenceNumber>
              <JournalEntryLineID>
                <xsl:value-of select="a:LINENUM"/>
              </JournalEntryLineID>
              <GLDestinationEntityID>
                <xsl:value-of select="a:JOURNALTYPE"/>
              </GLDestinationEntityID>
              <Account>
                <xsl:choose>
                  <xsl:when test="a:COMPANY_CODE = 'ACA'">
                    <xsl:choose>
                      <xsl:when test="a:ORIGINALACCOUNTCODE">
                        <xsl:value-of select="a:ORIGINALACCOUNTCODE"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="a:ACCOUNTCODE"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="a:ACCOUNTCODE"/>
                  </xsl:otherwise>
                </xsl:choose>
              </Account>
              <CostCenterID>
                <xsl:value-of select="a:ANALYSISCODE1"/>
              </CostCenterID>
              <LineOfBusiness>
                <xsl:value-of select="a:ANALYSISCODE2"/>
              </LineOfBusiness>
              <PrimaryBusiness>
                <xsl:value-of select="a:ANALYSISCODE3"/>
              </PrimaryBusiness>
              <SecondaryBusiness>
                <xsl:value-of select="a:ANALYSISCODE4"/>
              </SecondaryBusiness>
              <BusinessType>
                <xsl:value-of select="a:ANALYSISCODE5"/>
              </BusinessType>
              <BlockOfBusiness>
                <xsl:value-of select="a:ANALYSISCODE6"/>
              </BlockOfBusiness>
              <DistributionChannel>
                <xsl:value-of select="a:ANALYSISCODE7"/>
              </DistributionChannel>
              <UDF>
                <xsl:value-of select="a:ANALYSISCODE8"/>
              </UDF>
              <JournalUserAlphaField1>
                <xsl:value-of select="a:ANALYSISCODE9"/>
              </JournalUserAlphaField1>
              <JournalUserAlphaField2>
                <xsl:value-of select="a:ANALYSISCODE10"/>
              </JournalUserAlphaField2>
              <AmountClass1>
                <xsl:choose>
                  <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'A'">ACCRUAL</xsl:when>
                  <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'C'">CASH</xsl:when>
                  <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'G'">GAAP</xsl:when>
                  <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'S'">STATUATORY</xsl:when>
                </xsl:choose>
              </AmountClass1>
              <SourceDocumentReference>
                <xsl:value-of select="a:TRANSACTIONREFERENCE"/>
              </SourceDocumentReference>
              <Description>
                <xsl:value-of select="a:DESCRIPTION"/>
              </Description>
              <PrimaryDRCRCode>
                <xsl:value-of select="a:DEBITCREDIT"/>
              </PrimaryDRCRCode>
              <PrimaryAmount>
                <xsl:choose>
                  <xsl:when test="(a:GENERALDESCRIPTION24 = 'REFUNDS' or a:GENERALDESCRIPTION24 = 'CLAIMS') and a:DEBITCREDIT = 'C'">
                    <xsl:if test='substring(a:TRANSACTIONAMOUNT,1,1) != "-"'>-</xsl:if>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:when>
                  <xsl:when test="(a:GENERALDESCRIPTION24 = 'REFUNDS' or a:GENERALDESCRIPTION24 = 'CLAIMS') and a:DEBITCREDIT = 'D'">
                    <xsl:choose>
                      <xsl:when test='substring(a:TRANSACTIONAMOUNT,1,1) = "-"'>
                        <xsl:value-of select='translate(format-number(a:TRANSACTIONAMOUNT,"0.00"),"-","")'/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:when>
                  <xsl:when test="a:DEBITCREDIT = 'C'">
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:otherwise>
                </xsl:choose>
              </PrimaryAmount>
              <TransactionCurrencyCode>
                <xsl:value-of select="a:CURRENCYCODE"/>
              </TransactionCurrencyCode>
              <PrimaryCurrencyCode>
                <xsl:value-of select="a:CURRENCYCODE"/>
              </PrimaryCurrencyCode>
              <TransactionAmount>
                <xsl:choose>
                  <xsl:when test="(a:GENERALDESCRIPTION24 = 'REFUNDS' or a:GENERALDESCRIPTION24 = 'CLAIMS') and a:DEBITCREDIT = 'C'">
                    <xsl:if test='substring(a:TRANSACTIONAMOUNT,1,1) != "-"'>-</xsl:if>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:when>
                  <xsl:when test="(a:GENERALDESCRIPTION24 = 'REFUNDS' or a:GENERALDESCRIPTION24 = 'CLAIMS') and a:DEBITCREDIT = 'D'">
                    <xsl:choose>
                      <xsl:when test='substring(a:TRANSACTIONAMOUNT,1,1) = "-"'>
                        <xsl:value-of select='translate(format-number(a:TRANSACTIONAMOUNT,"0.00"),"-","")'/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:when>
                  <xsl:when test="a:DEBITCREDIT = 'C'">
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                  </xsl:otherwise>
                </xsl:choose>
              </TransactionAmount>
              <EntryCreatorID>ELITA</EntryCreatorID>
              <ValidateAccount>Y</ValidateAccount>
            </JournalEntry>
          </xsl:if>
        </xsl:for-each>
      </xsl:element>

    </SmartStreamJournal>
  </xsl:template>
</xsl:stylesheet>
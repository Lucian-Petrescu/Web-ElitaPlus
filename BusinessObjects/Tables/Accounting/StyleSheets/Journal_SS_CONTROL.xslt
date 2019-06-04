<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"  />
  <xsl:template match="/">
    <xsl:element name="SmartStreamJournalControl">
      <xsl:for-each select="a:SSC/a:ControlGroup">
        <xsl:if test="a:DEBITCREDIT = 'C'">
        <xsl:element name="ControlRecord">
          <xsl:element name="JournalHeaderID">
            <xsl:value-of select="/a:SSC/a:LedgerPostingParameters/a:JOURNALHEADERID"/>
          </xsl:element>
          <xsl:element name="BatchID">
            <xsl:value-of select="/a:SSC/a:LedgerPostingParameters/a:TIMESTAMP"/>
          </xsl:element>
          <SysCode>
            <xsl:value-of select="substring(a:JOURNAL_ID_SUFFIX,10,3)"/>
          </SysCode>
          <GLEntityID>
            <xsl:value-of select="a:JOURNALTYPE"/>
          </GLEntityID>
          <EffectiveTimePeriod>
            <xsl:value-of select="substring(a:TRANSACTIONDATE,5,4)"/>
            <xsl:text>-</xsl:text>
            <xsl:value-of select="substring(a:TRANSACTIONDATE,3,2)"/>
            <xsl:text>-</xsl:text>
            <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)"/>
          </EffectiveTimePeriod>
          <PostingDate>
            <xsl:value-of select="substring(a:CURRENTDATE,5,4)"/>
            <xsl:text>-</xsl:text>
            <xsl:value-of select="substring(a:CURRENTDATE,3,2)"/>
            <xsl:text>-</xsl:text>
            <xsl:value-of select="substring(a:CURRENTDATE,1,2)"/>
          </PostingDate>
          <AmountClass>
            <xsl:choose>
              <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'A'">ACCRUAL</xsl:when>
              <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'C'">CASH</xsl:when>
              <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'G'">GAAP</xsl:when>
              <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'S'">STATUATORY</xsl:when>
            </xsl:choose>
          </AmountClass>
          <TotalDebits>
            <xsl:value-of select='format-number(a:TRANSACTIONAMOUNT,"0.00")'/>
          </TotalDebits>
          <TotalCredits>
            <xsl:value-of select='format-number(a:TRANSACTIONAMOUNT,"0.00")'/>
          </TotalCredits>
          <Description>
            <xsl:text>Elita GL Interface</xsl:text>
          </Description>
        </xsl:element>
        </xsl:if>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
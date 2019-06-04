<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="text"  encoding="UTF-8" indent="no" />
    <xsl:template match="/">
      <xsl:for-each select="a:SSC/a:Line">
        <xsl:if test="a:TRANSACTIONAMOUNT > 0 and a:REV != 1">
          <xsl:value-of select="substring(concat(' ', '                                                          '), 1, 50)"/>
          <xsl:value-of select="substring(concat(concat('E',a:ACCOUNTCODE), '                                                          '), 1, 35)"/>
          <xsl:value-of select="substring(concat(a:TIMESTAMP, '                                                          '), 1, 35)"/>
          <xsl:value-of select="substring(concat(a:TRANSACTIONAMOUNT, '                                                          '), 1, 15)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 8)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 57)"/>
          <xsl:value-of select="substring(concat(a:ACCT_COMPANY_CODE, '                                                          '), 1, 1)"/>
          <xsl:value-of select="substring(concat(//a:SSC/a:SunSystemsContext/a:BUSINESSUNIT, '                                                          '), 1, 3)"/>
          <xsl:value-of select="substring(concat(a:CURRENCYCODE, '                                                          '), 1, 3)"/>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE2, '                                                          '), 1, 15)"/>
          <xsl:choose>
            <xsl:when test="a:DEBITCREDIT = 'C'">
              <xsl:text>1</xsl:text>
            </xsl:when>
            <xsl:otherwise>
              <xsl:text>2</xsl:text>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:value-of select="substring(concat(a:ACCT_EVENT_MAPPING, '                                                          '), 1, 1)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 8)"/>
          <xsl:value-of select="substring(concat(a:DESCRIPTION, '                                                                                  '), 1, 80)"/>
          <xsl:value-of select="substring(a:TRANSACTIONDATE, 5, 4)" />
          <xsl:value-of select="substring(a:TRANSACTIONDATE, 3, 2)" />
          <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)" />
          <xsl:value-of select="substring(concat(a:JOURNALTYPE, '                                                          '), 1, 5)"/>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE3, '                                                          '), 1, 25)"/>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE4, '                                                          '), 1, 25)"/>
          <xsl:text>&#10;</xsl:text>
        </xsl:if>
      </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>

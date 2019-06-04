<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="text"  encoding="UTF-8" indent="no" />
    <xsl:template match="/">
      <xsl:for-each select="a:SSC/a:Line">
        <xsl:if test="a:TRANSACTIONAMOUNT > 0 and a:REV != 1">
          <xsl:value-of select="substring(concat(' ', '                                                                                 '), 1, 50)"/>
          <xsl:value-of select="substring(concat(concat('E',a:ACCOUNTCODE), '                                                          '), 1, 35)"/>
          <xsl:value-of select="substring(concat(a:JOURNALSEQUENCE, '                                                          '), 1, 35)"/>
          <xsl:value-of select="substring(concat(a:TRANSACTIONAMOUNT, '                                                          '), 1, 15)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 8)"/>
          <xsl:value-of select="substring(concat(a:JOURNALTYPE, '                                               '), 1, 5)"/>
          <xsl:value-of select="substring(concat('', '                                                                                   '), 1, 50)"/>
          <xsl:value-of select="substring(concat(a:JOURNAL_ID_SUFFIX, '                                                          '), 10, 3)"/>
          <xsl:value-of select="substring(concat(a:ACCT_COMPANY_CODE, '                                                          '), 1, 1)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 3)"/>          
          <xsl:value-of select="substring(concat(a:CURRENCYCODE, '                                                          '), 1, 3)"/>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE6, '                                                          '), 1, 15)"/>
          <xsl:choose>
            <xsl:when test="a:DEBITCREDIT = 'C'">
              <xsl:text>1</xsl:text>
            </xsl:when>
            <xsl:otherwise>
              <xsl:text>2</xsl:text>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE7, '                                                          '), 1, 1)"/>
          <xsl:value-of select="substring(concat('', '                                                          '), 1, 8)"/>
          <xsl:value-of select="substring(concat(a:DESCRIPTION, '                                                                                  '), 1, 35)"/>
          <xsl:value-of select="substring(concat('', '                                                                                             '), 1, 35)"/>
          <xsl:value-of select="substring(concat('', '                                                                                             '), 1, 10)"/>
          <xsl:value-of select="substring(concat('', '                                                                                             '), 1, 4)"/>
          <xsl:value-of select="substring(a:TRANSACTIONDATE, 5, 4)" />
          <xsl:value-of select="substring(a:TRANSACTIONDATE, 3, 2)" />
          <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)" />
          <xsl:value-of select="substring(concat('', '                                                                                             '), 1, 5)"/>
          <xsl:choose>
            <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'A'">
              <xsl:value-of select="substring(concat('ACCRUAL', '                                                                                             '), 1, 25)"/>
            </xsl:when>
            <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'C'">
              <xsl:value-of select="substring(concat('CASH', '                                                                                                '), 1, 25)"/>  
            </xsl:when>
            <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'G'">
              <xsl:value-of select="substring(concat('GAAP', '                                                                                                '), 1, 25)"/>    
            </xsl:when>
            <xsl:when test="substring(a:JOURNAL_ID_SUFFIX,4,1) = 'S'">
              <xsl:value-of select="substring(concat('STATUATORY', '                                                                                         '), 1, 25)"/>      
            </xsl:when>
          </xsl:choose>
          <xsl:value-of select="substring(concat(a:ANALYSISCODE3, '                                                                                         '), 1, 25)"/>
          <xsl:text>&#10;</xsl:text>
        </xsl:if>
      </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="text" encoding="UTF-8" />
  <xsl:template match="/">
    <xsl:for-each select="a:SSC/a:PRCG">
      <xsl:text>PRCG</xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:choose>
        <xsl:when test="string-length(a:ROWNUMBER) = 3">
          <xsl:value-of select="a:CONTROL_GROUP"/>
          <xsl:text>PR</xsl:text>
          <xsl:value-of select="a:ROWNUMBER"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="a:CONTROL_GROUP"/>
          <xsl:text>PR</xsl:text>
          <xsl:value-of select="substring('000',1,3 - string-length(a:ROWNUMBER))"/>
          <xsl:value-of select="a:ROWNUMBER"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 1, 4)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 6, 2)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE,9,2)" />
      <xsl:text>,</xsl:text>
      <xsl:value-of select="a:TOTAL_AMOUNT"/>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="a:INVOICE_COUNT"/>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="a:CONTROL_GROUP"/>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 1, 4)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 6, 2)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE,9,2)" />
      <xsl:text>&#10;</xsl:text>
    </xsl:for-each>
    <xsl:for-each select="a:SSC/a:PRQT">
      <xsl:text>PRQT</xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:choose>
        <xsl:when test="string-length(a:BLOCK_OF_BUSINESS) = 10">
          <xsl:value-of select="a:BLOCK_OF_BUSINESS"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="substring('0000000000',1,10 - string-length(a:BLOCK_OF_BUSINESS))"/>
          <xsl:value-of select="a:BLOCK_OF_BUSINESS"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:text>,</xsl:text>
      <xsl:text>000</xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="a:GENERALDESCRIPTION8"/>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="substring(a:TRANSACTIONDATE,5,4)"/>
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:TRANSACTIONDATE,3,2)"/>
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)"/>
      <!--5-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="a:CONTROL_GROUP"/>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text>PRSC</xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:choose>
        <xsl:when test="a:TOTAL_AMOUNT > 0">INVC</xsl:when>
        <xsl:when test="a:TRANSTYPE = 'REFUNDS'">INVC</xsl:when>
        <xsl:otherwise>CRMM</xsl:otherwise>
      </xsl:choose>
      <!--10-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text>USD</xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--15-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:choose>
        <xsl:when test="a:TRANSTYPE = 'REFUNDS'">
          <xsl:value-of select="a:ABS_AMOUNT"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="a:TOTAL_AMOUNT"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:text>,</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 1, 4)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 6, 2)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE,9,2)" />
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--20-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text>0600</xsl:text>
      <xsl:value-of select="a:CONTROL_GROUP"/>
      <!--25-->
      <xsl:text>,</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 1, 4)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE, 6, 2)" />
      <xsl:text>/</xsl:text>
      <xsl:value-of select="substring(a:CURRENT_DATE,9,2)" />
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--30-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--35-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--40-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <!--45-->
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>,</xsl:text>
      <xsl:text> </xsl:text>
      <xsl:text>&#10;</xsl:text>
    </xsl:for-each>
    <xsl:for-each select="a:SSC/a:AP_Item">
      <xsl:if test="a:ORIG_DEBIT_CREDIT = 'D'">
        <xsl:text>PRLN</xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:choose>
          <xsl:when test="string-length(a:BLOCK_OF_BUSINESS) = 10">
            <xsl:value-of select="a:BLOCK_OF_BUSINESS"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="substring('0000000000',1,10 - string-length(a:BLOCK_OF_BUSINESS))"/>
            <xsl:value-of select="a:BLOCK_OF_BUSINESS"/>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:text>,</xsl:text>
        <xsl:text>000</xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:TRANSACTIONREFERENCE"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="substring(a:TRANSACTIONDATE,5,4)"/>
        <xsl:text>/</xsl:text>
        <xsl:value-of select="substring(a:TRANSACTIONDATE,3,2)"/>
        <xsl:text>/</xsl:text>
        <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)"/>
        <!--5-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text>0001</xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:DESCRIPTION"/>
        <xsl:text>,</xsl:text>
        <xsl:choose>
          <xsl:when test="a:TRANSTYPE = 'REFUNDS'">
            <xsl:value-of select="a:ABS_AMOUNT"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--10-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--15-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--20-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--25-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--30-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--35-->
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:JOURNALTYPE"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ACCOUNTCODE"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE1"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE2"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:UDAK_PRIMARY"/>
        <!--40-->
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE4"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE5"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:BLOCK_OF_BUSINESS"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE7"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="a:ANALYSISCODE8"/>
        <!--45-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--50-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--55-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--60-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <!--65-->
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>,</xsl:text>
        <xsl:text> </xsl:text>
        <xsl:text>&#10;</xsl:text>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>

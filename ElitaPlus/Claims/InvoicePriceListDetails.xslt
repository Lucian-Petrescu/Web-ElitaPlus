<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  <xsl:variable name="recordCount" select="5" />
  <xsl:template match="/">
    <xsl:if test="count(/PriceListDs/PriceListDetail) &gt; 0">
      <table width="100%" class="dataGrid" border="0" cellSpacing="0" cellPadding="0">
        <tbody>
          <tr>
            <td align="right" class="bor" colSpan="9">
              <xsl:value-of select="count(/PriceListDs/PriceListDetail)" /> &#160;
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='MSG_RECORDS_FOUND']/TRANSLATION" />
            </td>
          </tr>
          <xsl:if test="(ceiling(count(//PriceListDs/PriceListDetail) div $recordCount)) &gt; 1">
            <tr>
              <td colspan="5" class="gridPager" style="text-align:center;">
                <table align="center" border="0">
                  <tbody>
                    <tr>
                      <xsl:call-template name="for.loop.pager">
                        <xsl:with-param name="i" select="1"></xsl:with-param>
                        <xsl:with-param name="count" select="ceiling(count(//PriceListDs/PriceListDetail) div $recordCount)"></xsl:with-param>
                        <xsl:with-param name="pagerPrefix" select='1'></xsl:with-param>
                      </xsl:call-template>
                    </tr>
                  </tbody>
                </table>
              </td>
            </tr>
          </xsl:if>
          <tr>
            <th>
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='VENDOR_SKU']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='VENDOR_SKU_DESCRIPTION']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='SERVICE_CLASS']/TRANSLATION" /> -
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='SERVICE_TYPE']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='RISK_TYPE']/TRANSLATION" /> &#47;
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='EQUIPMENT_CLASS']/TRANSLATION" /> &#47;
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='EQUIPMENT']/TRANSLATION" /> &#47;
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='CONDITION']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='PRICE']/TRANSLATION" />
            </th>
          </tr>
          <xsl:for-each select="PriceListDs/PriceListDetail">
            <tr>
              <xsl:attribute name="id">tr_<xsl:value-of select="position()" /></xsl:attribute>
              <xsl:if test="position() &gt; $recordCount">
                <xsl:attribute name="style">display:none</xsl:attribute>
              </xsl:if>
              <td nowrap="nowrap">
                <a href="#">
                  <xsl:attribute name="onclick">
                    AddInvoiceLineItem('<xsl:value-of select="SERVICE_CLASS_ID" />', '<xsl:value-of select="SERVICE_TYPE_ID" />', '<xsl:value-of select="VENDOR_SKU" />', '<xsl:value-of select="VENDOR_SKU_DESCRIPTION" />', '<xsl:value-of select="PRICE" />');</xsl:attribute>
                  <xsl:attribute name="id">pli_<xsl:value-of select="position()" /></xsl:attribute>
                  <xsl:value-of select="VENDOR_SKU" />
                </a>
              </td>
              <td>
                <xsl:value-of select="VENDOR_SKU_DESCRIPTION" />
              </td>
              <td nowrap="nowrap">
                <xsl:value-of select="SERVICE_CLASS" />
                <br />
                <xsl:value-of select="SERVICE_TYPE" />
              </td>
              <td>
                <xsl:choose>
                  <xsl:when test="string-length(RISK_TYPE_DESCRIPTION) > 0">
                    <b>
                      <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='RISK_TYPE']/TRANSLATION" />:
                    </b>
                    <xsl:value-of select="RISK_TYPE_DESCRIPTION"/>
                  </xsl:when>
                  <xsl:when test="string-length(EQUIPMENT_CLASS) > 0">
                    <b>
                      <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='EQUIPMENT_CLASS']/TRANSLATION" />:
                    </b>
                    <xsl:value-of select="EQUIPMENT_CLASS"/>
                  </xsl:when>
                  <xsl:when test="string-length(EQUIPMENT_DESCRIPTION) > 0">
                    <b>
                      <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='EQUIPMENT']/TRANSLATION" />:
                    </b>
                    <xsl:value-of select="EQUIPMENT_DESCRIPTION"/>
                    <xsl:if test="string-length(CONDITION) > 0">
                      <br />
                      <b>
                        <xsl:value-of select="/PriceListDs/Headers[UI_PROG_CODE='CONDITION']/TRANSLATION" />:
                      </b>
                      <xsl:value-of select="CONDITION"/>
                    </xsl:if>
                  </xsl:when>
                </xsl:choose>
              </td>
              <td align="right">
                <xsl:value-of select="format-number(PRICE, '###,##0.00')" />
              </td>
            </tr>
          </xsl:for-each>
          <xsl:if test="(ceiling(count(//PriceListDs/PriceListDetail) div $recordCount)) &gt; 1">
            <tr>
              <td colspan="5" class="gridPager" style="text-align:center;">
                <table align="center" border="0">
                  <tbody>
                    <tr>
                      <xsl:call-template name="for.loop.pager">
                        <xsl:with-param name="i" select="1"></xsl:with-param>
                        <xsl:with-param name="count" select="ceiling(count(//PriceListDs/PriceListDetail) div $recordCount)"></xsl:with-param>
                        <xsl:with-param name="pagerPrefix" select='2'></xsl:with-param>
                      </xsl:call-template>
                    </tr>
                  </tbody>
                </table>
              </td>
            </tr>
          </xsl:if>
        </tbody>
      </table>
    </xsl:if>
  </xsl:template>
  <xsl:template name="for.loop.pager">
    <xsl:param name="i" />
    <xsl:param name="count" />
    <xsl:param name="pagerPrefix" />
    <xsl:if test="$i &lt;= $count">
      <td>
        <a>
          <xsl:attribute name="href">
            javascript:showHidePage(<xsl:value-of select="$i"/>,<xsl:value-of select="count(//PriceListDs/PriceListDetail)"/>,<xsl:value-of select="$recordCount"/>);
          </xsl:attribute>
          <xsl:if test="$i = 1">
            <xsl:attribute name="style">display:none;</xsl:attribute>
          </xsl:if>
          <xsl:attribute name="id">pg<xsl:value-of select="$pagerPrefix" />a_<xsl:value-of select="$i"/></xsl:attribute>
          <xsl:value-of select="$i" />
        </a>
        <span>
          <xsl:if test="$i != 1">
            <xsl:attribute name="style">display:none;</xsl:attribute>
          </xsl:if>
          <xsl:attribute name="id">pg<xsl:value-of select="$pagerPrefix" />s_<xsl:value-of select="$i"/></xsl:attribute>
          <xsl:value-of select="$i" />
        </span>
      </td>
      <xsl:call-template name="for.loop.pager">
        <xsl:with-param name="i" select="$i + 1"></xsl:with-param>
        <xsl:with-param name="count" select="$count"></xsl:with-param>
        <xsl:with-param name="pagerPrefix" select="$pagerPrefix"></xsl:with-param>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
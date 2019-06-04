<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <table width="100%" class="dataGrid">
        <tbody>
          <tr>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='SERVICE_CLASS']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='SERVICE_TYPE']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='LINE_ITEM_NUMBER']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='AUTHORIZED_AMOUNT']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='INVOICE_AMOUNT']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/ClaimAuthLineItemDS/Headers[UI_PROG_CODE='RECONCILED_AMOUNT']/TRANSLATION" />
            </th>
           </tr>
          <xsl:for-each select="ClaimAuthLineItemDS/ClaimAuthLineItems">
            <tr>
              <td>
                <xsl:value-of select="SVCCLASS" />
              </td>
              <td>
                <xsl:value-of select="SVCTYPE" />
              </td>
              <td>
                <xsl:value-of select="LINE_ITEM_NUMBER" />
              </td>
              <td>
                <xsl:if test="AUTHAMOUNT != ''">
                  <xsl:value-of select="format-number(AUTHAMOUNT, '###,##0.00;(###,##0.00)')" />
                </xsl:if>
              </td>
              <td>
                <xsl:if test="INVOICEAMOUNT != ''">
                  <xsl:value-of select="format-number(INVOICEAMOUNT, '###,##0.00;(###,##0.00)')" />
                </xsl:if>
              </td>              
              <xsl:choose>
                <xsl:when test="RECONCILEDAMOUNT&lt;INVOICEAMOUNT">
                  <td>
                  <font color="red">
                    <b>
                      <xsl:if test="RECONCILEDAMOUNT != ''">
                        <xsl:value-of select="format-number(RECONCILEDAMOUNT, '###,##0.00;(###,##0.00)')" />
                      </xsl:if>
                    </b>
                  </font>
                  </td>
                </xsl:when>
                <xsl:when test="RECONCILEDAMOUNT&gt;INVOICEAMOUNT">
                  <td>
                  <font color="red">
                    <b>
                      <xsl:if test="RECONCILEDAMOUNT != ''">
                        <xsl:value-of select="format-number(RECONCILEDAMOUNT, '###,##0.00;(###,##0.00)')" />
                      </xsl:if>
                    </b>
                  </font>
                  </td>
                </xsl:when>
                <xsl:otherwise>
                  <td>
                  <font color="Green">
                    <b>
                      <xsl:if test="RECONCILEDAMOUNT != ''">
                        <xsl:value-of select="format-number(RECONCILEDAMOUNT, '###,##0.00;(###,##0.00)')" />
                      </xsl:if>
                    </b>
                  </font>
                  </td>
                </xsl:otherwise>
              </xsl:choose>           
            </tr>
          </xsl:for-each>
        </tbody>
      </table>
    </xsl:template>
</xsl:stylesheet>

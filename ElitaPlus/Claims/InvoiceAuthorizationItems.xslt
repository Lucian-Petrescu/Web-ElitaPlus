<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <table width="100%" class="dataGrid">
      <tbody>
        <tr>
          <th>
            Line &#35;
          </th>
          <th>
            <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='SKU']/TRANSLATION" />
          </th>
          <th>
            <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='DESCRIPTION']/TRANSLATION" />
          </th>
          <th>
            <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='SERVICE_CLASS']/TRANSLATION" />
          </th>
          <th>
            <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='SERVICE_TYPE']/TRANSLATION" />
          </th>
          <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
            <th style="width:100px">
              <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='AUTHORIZED_AMOUNT']/TRANSLATION" />
            </th>
          </xsl:if>
          <th style="width:100px">
            <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='AMOUNT']/TRANSLATION" />
          </th>
          <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
            <th style="width:100px">
              <xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='RECONCILED_AMOUNT']/TRANSLATION" />
            </th>
          </xsl:if>
          <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
            <th>
              
            </th>
          </xsl:if>
        </tr>
        <xsl:for-each select="InvoiceAuthorizationItemDs/InvoiceAuthorizationItem">
          <tr onmouseover="this.className='over'" onmouseout="this.className='out'">
            <td>
              <xsl:value-of select="LINE_ITEM_NUMBER" />
            </td>
            <td>
              <xsl:value-of select="VENDOR_SKU" />
            </td>
            <td>
              <xsl:value-of select="VENDOR_SKU_DESCRIPTION" />
            </td>
            <td>
              <xsl:value-of select="SERVICE_CLASS_DESCRIPTION" />
            </td>
            <td>
              <xsl:value-of select="SERVICE_TYPE_DESCRIPTION" />
            </td>
            <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
              <td align="right">
                <xsl:if test="AUTHORIZED_AMOUNT != ''">
                  <xsl:value-of select="format-number(AUTHORIZED_AMOUNT, '###,##0.00;(###,##0.00)')" />
                </xsl:if>
              </td>
            </xsl:if>
            <td align="right">
              <xsl:choose>
                <xsl:when test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
                  <xsl:if test="AMOUNT != ''">
                    <xsl:value-of select="format-number(AMOUNT, '###,##0.00;(###,##0.00)')" />
                  </xsl:if>
                </xsl:when>
                <xsl:otherwise >
                  <input type="text" align="right" class="exsmall">
                    <xsl:if test="AMOUNT != ''">
                    <xsl:attribute name="value">
                        <xsl:value-of select="format-number(AMOUNT, '###,##0.00;(###,##0.00)')" />
                    </xsl:attribute>
                      <xsl:attribute name="onchange">
                        UpdateInvoiceLineItemAmount('<xsl:value-of select="INVOICE_ITEM_ID" />', this.value, '<xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='INVOICE_AUTH_AMOUNT_ID']/TRANSLATION" />')
                      </xsl:attribute>
                    </xsl:if>
                  </input>
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'TP' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'PA' or /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode = 'R'">
              <td align="right">
                <xsl:if test="RECONCILED_AMOUNT != ''">
                  <xsl:value-of select="format-number(RECONCILED_AMOUNT, '###,##0.00;(###,##0.00)')" />
                </xsl:if>
              </td>
            </xsl:if>
            <xsl:if test="/InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode != 'TP' and /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode != 'PA' and /InvoiceAuthorizationItemDs/InvoiceAuthorization/StatusCode != 'R'">
              <xsl:if test="count(/InvoiceAuthorizationItemDs/InvoiceAuthorizationItem) &gt; 1">
                <td>
                  <a href="#">
                    <xsl:attribute name="onClick">
                      RemoveInvoiceLineItem('<xsl:value-of select="INVOICE_ITEM_ID" />', '<xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='SENDER_ID']/TRANSLATION" />', '<xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='INDEX']/TRANSLATION" />', '<xsl:value-of select="/InvoiceAuthorizationItemDs/Headers[UI_PROG_CODE='INVOICE_AUTH_AMOUNT_ID']/TRANSLATION" />')
                    </xsl:attribute>
                    <img src="../App_Themes/Default/Images/icon_delete.png" alt="Delete Line Item"  />
                  </a>
                </td>
              </xsl:if>
            </xsl:if>
          </tr>
        </xsl:for-each>
      </tbody>
    </table>
  </xsl:template>
</xsl:stylesheet>

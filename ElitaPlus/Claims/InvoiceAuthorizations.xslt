<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <table width="100%" class="dataGrid">
        <tbody>
          <tr>
            <th>
              <xsl:value-of select="/InvoiceAuthorizationDs/Headers[UI_PROG_CODE='CLAIM_NUMBER']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/InvoiceAuthorizationDs/Headers[UI_PROG_CODE='AUTHORIZATION_NUMBER']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/InvoiceAuthorizationDs/Headers[UI_PROG_CODE='BATCH_NUMBER']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/InvoiceAuthorizationDs/Headers[UI_PROG_CODE='AUTHORIZATION_STATUS']/TRANSLATION" />
            </th>
            <th>
              <xsl:value-of select="/InvoiceAuthorizationDs/Headers[UI_PROG_CODE='INVOICE_AUTHORIZATION_AMOUNT']/TRANSLATION" />
            </th>
          </tr>
          <xsl:for-each select="InvoiceAuthorizationDs/InvoiceAuthorization">
            <tr>
              <td>
                <xsl:value-of select="CLAIM_NUMBER" />
              </td>
              <td>
                <xsl:value-of select="AUTHORIZATION_NUMBER" />
              </td>
              <td>
                <xsl:value-of select="BATCH_NUMBER" />
              </td>
              <td>
                <xsl:value-of select="AUTHORIZATION_STATUS" />
              </td>
              <td align="right">
                <xsl:value-of select="format-number(INVOICE_AUTH_AMOUNT, '###,##0.00')" />
              </td>
            </tr>
          </xsl:for-each>
        </tbody>
      </table>
    </xsl:template>
</xsl:stylesheet>

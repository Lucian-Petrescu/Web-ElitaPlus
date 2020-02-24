<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
	exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { width:auto }
          TD { FONT-SIZE:13px;font-family:"Trebuchet MS";font-weight:bolder;height:5px;}
          .Header {background-color:#000;color:#fff; TEXT-ALIGN:LEFT;text-decoration: underline}
          .esp {font-weight:normal;}
        </style>
      </head>
      <body>
        <table cellpadding="0" cellspacing="0" border="0" style="PADDING-RIGHT:10px;PADDING-LEFT:10px;width:100%">
          <tr>
            <td colspan="2">
              <table style="width:100%">
                <tr>
                  <td style="width:75%"></td>
                  <td rowspan="3" style="text-align:right;vertical-align:middle">
                    <xsl:element name="img">
                      <xsl:attribute name="src">
                        <xsl:choose>
                          <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                            <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_aau.jpg")'/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_apr.jpg</xsl:text>
                          </xsl:otherwise>
                        </xsl:choose>
                      </xsl:attribute>
                    </xsl:element>
                  </td>
                </tr>
                <tr>
                  <td>
                    Replacement Request Order - Claim Number: &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />-<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_NUMBER" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">Customer Information</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Name:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              Address:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
              <xsl:text>,&#160;</xsl:text>
              <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2 != ''">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                  <xsl:text>,&#160;</xsl:text>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS3 != ''">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS3" />
                  <xsl:text>,&#160;</xsl:text>
                </xsl:when>
              </xsl:choose>
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
              <xsl:text>,&#160;</xsl:text>
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE_CODE" />
              <xsl:text>,&#160;</xsl:text>
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Number:&#160;
               <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE != ''">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE != ''">
                  <xsl:text> / &#160;</xsl:text>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                </xsl:when>
              </xsl:choose>
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Email:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">Product Information</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Description:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Brand:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Model:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Serial:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              IMEI:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:IMEI" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Purchase Price:&#160;$<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Date Of Purchase:&#160;<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 9, 2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              Retailer:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              Fault Description:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">Claim Instructions</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              Replacement Authority Limit: $<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT" />
            </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              Additional comments:<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
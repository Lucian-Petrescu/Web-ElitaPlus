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
          .Header {background-color:#000;color:#fff; TEXT-ALIGN:CENTER}
          .esp {font-weight:normal;}
        </style>
      </head>
      <body>
        <table cellpadding="0" cellspacing="0" border="0" style="PADDING-RIGHT:10px;PADDING-LEFT:10px;width:100%">
          <tr>
            <td colspan="2">
              <table style="width:100%">
                <tr>
                  <td style="width:75%">From: Federal Warranty Service Corporation</td>
                  <td rowspan="3" style="text-align:right;vertical-align:middle">
                    <xsl:element name="img">
                      <xsl:attribute name="src">
                        <xsl:choose>
                          <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                            <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_apr.jpg")'/>
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
                    To: &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                  </td>
                </tr>
                <tr>
                  <td>
                    Date:	&#160;<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" />
                    <xsl:text>/</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" />
                    <xsl:text>/</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style="width:50%"></td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header" bgcolor="#000000">WORK ORDER (ORDEN DE SERVICIO)</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>
              AUTHORIZATION NUMBER:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
            </td>
            <td>
              DATE PROCESSED:	&#160;<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
            </td>
          </tr>
          <tr>
            <td class="esp">(NUMERO DE AUTORIZACION)</td>
            <td class="esp">(FECHA PROCESADA)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              CLIENT:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
            </td>
            <td>
              SERVICE TYPE:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
            </td>
          </tr>
          <tr>
            <td class="esp">(CLIENTE)</td>
            <td></td>
          </tr>
          <tr>
            <td>
              EXTENDED CLAIM STATUS:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:EXTENDED_CLAIM_STATUS" />
            </td>
            <td>             
            </td>
          </tr>
          <tr>
            <td class="esp">(ESTADO DETALLADO)</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">CUSTOMER INFORMATION (INFORMACION DEL CLIENTE)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              CERTIFICATE NUMBER:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
            </td>
            <td></td>
          </tr>
          <tr>
            <td class="esp">(NUMERO DE CERTIFICADO)</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              CUSTOMER NAME:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
            </td>
            <td>
              E-MAIL ADDRESS:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
            </td>
          </tr>
          <tr>
            <td class="esp">(NOMBRE DEL CLIENTE)</td>
            <td class="esp">(DIRECCIÓN DE CORREO ELECTRONICO)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              CUSTOMER ADDRESS:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />&#160;
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />,<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />,
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />,<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
            </td>
          </tr>
          <tr>
            <td class="esp">(DIRECCION)</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              SHIPPING ADDRESS:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_ADDRESS1" />&#160;
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />,<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_CITY" />,
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_REGION" />,<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_ZIP" />
            </td>
          </tr>
          <tr>
            <td class="esp">(DIRECCION POSTAL)</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              HOME PHONE:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
            </td>
            <td>
              WORK PHONE:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
            </td>
          </tr>
          <tr>
            <td class="esp">(TELEFONO CASA)</td>
            <td class="esp">(TELEFONO TRABAJO)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">PRODUCT INFORMATION (INFORMACION DEL PRODUCTO)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              PRODUCT:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
            </td>
            <td>
              BRAND:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
            </td>
          </tr>
          <tr>
            <td class="esp">(PRODUCTO)</td>
            <td class="esp">(MARCA)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              MODEL:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
            </td>
            <td>
              SERIAL NUMBER:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
            </td>
          </tr>
          <tr>
            <td class="esp">(MODELO)</td>
            <td class="esp">(SERIAL NUMBER)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              PURCHASE PRICE:&#160;$<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE" />
            </td>
            <td>
              PURCHASE DATE:&#160;<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,6,2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 9, 2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
            </td>
          </tr>
          <tr>
            <td class="esp">(PRECIO DE COMPRA)</td>
            <td class="esp">(FECHA DE COMPRA)</td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td>
              EXPIRATION DATE:&#160;<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE,6,2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE, 9, 2)" />
              <xsl:text>/</xsl:text>
              <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE, 1, 4)" />
            </td>
          </tr>
          <tr>
            <td class="esp">(FECHA DE EXPIRACION)</td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">
              PROBLEM DESCRIPTION:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
            </td>
          </tr>
          <tr>
            <td class="esp">
              (DESCRIPCION DEL PROBLEMA)
            </td>
            <td></td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2" class="Header">INSTRUCTIONS FOR THE SERVICE CENTER (INSTRUCCIONES PARA EL CENTRO DE SERVICIO)</td>
          </tr>

          <tr>
            <td colspan="2">Cliente deberá ser contactado dentro de las próximas 4 horas para coordinar el servicio. </td>
          </tr>
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <xsl:choose>
              <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:TAX_AMOUNT">
                <td colspan="2">
                  Total autorizado para reparaciones es: $<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT - a:ServiceOrderReport/a:ServiceOrder/a:TAX_AMOUNT" />
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td colspan="2">
                  Total autorizado para reparaciones es: $<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT" />
                </td>
              </xsl:otherwise>
            </xsl:choose>
          </tr> 
          <tr>
            <td>&#160;</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="2">De tener alguna duda o preguntas favor de escribirnos a: authorizations.ESC.PR@assurant.com</td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
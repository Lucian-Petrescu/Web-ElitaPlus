<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd" exclude-result-prefixes="a">
    <xsl:output indent="yes" method="html" encoding="utf-8"/>
    
    <xsl:template match="/">
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <style type="text/css">
            BODY
            {
            font-family: Verdana, Arial, Tahoma;
            margin: 10px;
            padding: 10px;
            }
            TABLE
            {
            width: 100%;
            }
            .mainTable
            {
            border-style:solid;
            border-width:3px;
            border-color:Black;
            }
            .mainTable TD
            {
            padding-right: 10px;
            padding-left: 10px;
            padding-top: 10px;
            padding-bottom: 5px;
            text-wrap:none;
            }
            .title
            {
            font-size: 12pt;
            font-weight: bold;
            text-align: center;
            }
            .data
            {
            font-size: 9pt;
            color:Red;
            }
            .dataAmt
            {
            font-size: 11pt;
            font-weight: bold;
            color:Red;
            }
            .label
            {
            font-size: 9pt;
            font-weight: bold;
            }
            .sectionTitle
            {
            font-size: 11pt;
            font-weight: bold;
            text-align: center;
            background-color:LightGrey;
            }
            .rightAlign
            {
            text-align: right;
            text-wrap:none;
            }
            .leftAlign
            {
            text-align: left;
            }
            .centerAlign
            {
            text-align: center;
            }
          </style>
        </head>
        <body>
          <table align="center" class="mainTable">
            <tr>
              <td class="leftAlign" colspan="4">
                <xsl:element name="img">
                  <xsl:attribute name="src">
                    <xsl:choose>
                      <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                        <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_awm.jpg")'/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_awm.jpg</xsl:text>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:attribute>
                </xsl:element>
              </td>
            </tr>
            <tr>
              <td class="title" colspan="4">
                ORDEN DE SERVICIO CELULARES - REEMPLAZO
              </td>
            </tr>
            <tr>
              <td class="rightAlign" colspan="3">
                <span class="label">No. DE REEMPLAZO:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                </span>
              </td>
            </tr>
            <tr>
              <td colspan="4">
                <table width="100%" style="margin:0px;padding:0px;">
                  <tr>
                    <td style="width:55%;padding-bottom:10px;text-wrap:none;" class="rightAlign">
                      <span class="label">Nombre del Centro de Atención a Clientes:</span>
                    </td>
                    <td class="leftAlign" style="padding-bottom:10px;text-wrap:none;">
                      <span class="data">
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                      </span>
                    </td>
                  </tr>
                </table>
              </td>              
            </tr>
            <tr>
              <td class="sectionTitle" colspan="4">AUTORIZACIÓN DE REEMPLAZO</td>
            </tr>
            <tr>
              <td class="rightAlign" style="width: 30%;">
                <span class="label">Orden de Servicio:</span>
              </td>
              <td class="leftAlign" style="width: 30%;">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MASTER_CLAIM_NUMBER" />
                </span>
              </td>
              <td class="centerAlign" colspan="2">
                <span class="label">Importe Autorizado:</span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Distribuidor:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                </span>
              </td>
              <td class="centerAlign" colspan="2">
                <span class="dataAmt">
                  $<xsl:value-of select='format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "#0.00")' />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Fecha de Creación:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" />
                  <xsl:text>/</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" />
                  <xsl:text>/</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                </span>
              </td>
              <td class="centerAlign" colspan="2">
                <span class="label">Importe Deducible:</span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign" style="padding-bottom:20px;">
                <span class="label">Reemplazo en:</span>
              </td>
              <td class="leftAlign" style="padding-bottom:20px;">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                </span>
              </td>
              <td class="centerAlign" colspan="2" style="padding-bottom:20px;">
                <span class="dataAmt">
                  $<xsl:value-of select='format-number(a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT, "#0.00")' />
                </span>
              </td>
            </tr>
            <tr>
              <td class="sectionTitle" colspan="4">INFORMACIÓN DEL CLIENTE</td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">No. De Certificado:</span>
              </td>
              <td class="leftAlign" colspan="3">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Nombre:</span>
              </td>
              <td class="leftAlign" colspan="3">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Domicilio:</span>
              </td>
              <td class="leftAlign" colspan="3">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDR_MAILING_LABEL" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Tel. Domicilio:</span>
              </td>
              <td class="leftAlign" colspan="3">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Tel. Oficina:</span>
              </td>
              <td class="leftAlign" colspan="3">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign" style="padding-bottom:20px;">
                <span class="label">e-mail:</span>
              </td>
              <td class="leftAlign" colspan="3" style="padding-bottom:20px;">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="sectionTitle" colspan="4">INFORMACIÓN DEL PRODUCTO</td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">Marca:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                </span>
              </td>
              <td class="rightAlign">
                <span class="label">Modelo:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign">
                <span class="label">IMEI:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                </span>
              </td>
              <td class="rightAlign">
                <span class="label">Fecha del Siniestro:</span>
              </td>
              <td class="leftAlign">
                <span class="data">
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" />
                  <xsl:text>-</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" />
                  <xsl:text>-</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="rightAlign" style="padding-bottom:10px;">
                <span class="label">Motivo perdida:</span>
              </td>
              <td class="leftAlign"  style="padding-bottom:10px;">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
                </span>
              </td>
              <td class="rightAlign" style="padding-bottom:10px;">
                <span class="label">Ticket:</span>
              </td>
              <td class="leftAlign" style="padding-bottom:10px;">
                <span class="data">
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                </span>
              </td>
            </tr>
          </table>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>

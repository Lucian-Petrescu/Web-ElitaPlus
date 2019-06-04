<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          BODY { FONT-SIZE: 18px; FONT-FAMILY: arial;width:auto;}
          TABLE {width:100%;}
          TD { FONT-SIZE: 18px; FONT-FAMILY: arial;vertical-align:top;text-align:left; }
          #Address{FONT-SIZE:20px;}
          #Title{FONT-SIZE:28px;font-weight:bold;text-align:center;border-top:#000 solid 1px;border-bottom:#000 solid 1px;}
          .warn{font-weight:bold;color:#FF0000;}
          .content TD{padding-bottom:63px;padding-right:10px;}
          .land{}

        </style>
        <style type="text/css" media="print">
          .land{filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);width:8.5in;height:6.5in;}
        </style>
      </head>
      <body>
        <table class="land">
          <tr>
            <td>
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td width="40%">
                    <xsl:element name="img">
                      <xsl:attribute name="src">
                        <xsl:choose>
                          <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                            <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_abr.jpg")'/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_abr.jpg</xsl:text>
                          </xsl:otherwise>
                        </xsl:choose>
                      </xsl:attribute>
                    </xsl:element>
                  </td>
                  <td id="Address">
                    <span class="warn">ATENÇÃO: Em caso de reparo faturar para:</span><br />
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_NAME"/><br />
                    CNPJ: 038237040001-52<br />
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_ADDRESS1"/><br />
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_ADDRESS2"/> <br />
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_CITY"/>&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_STATE"/>  <br />
                    CEP <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_ZIP"/><br /><br />
                  </td>
                </tr>
              </table>
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Title">
                    AVISO DE REPARO
                  </td>
                </tr>
              </table>
              <table cellpadding="10" cellspacing="0">
                <tr>
                  <td>
                    <table cellpadding="" cellspacing="0">
                      <tr>
                        <td>
                          <table cellpadding="0" cellspacing="0" class="content">
                            <tr>
                              <td width="50%">
                                Centro de Serviço:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                              </td>
                              <td>
                                Código do Centro de Serviço:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table cellpadding="0" cellspacing="0" class="content">
                            <tr>
                              <td width="50%">
                                Nome do Consumidor: &#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table cellpadding="0" cellspacing="0" class="content">
                            <tr>
                              <td width="50%">
                                Data do pedido de reparo:&#160;&#160;
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                              </td>
                              <td>
                                Ordem de Serviço Assurant:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table cellpadding="0" cellspacing="0" class="content">
                            <tr>
                              <td width="45%">
                                Descrição do Produto:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                              <td width="30%">
                                Modelo:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                              <td>
                                Marca:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table cellpadding="0" cellspacing="0" class="content">
                            <tr>
                              <td width="50%">
                                Defeito:&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                              </td>
                              <td>
                                Valor do produto:&#160;&#160;R$
                                <xsl:choose>
                                  <xsl:when test='format-number(a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE, "0.00") = "NaN"'>
                                    0,00
                                  </xsl:when>
                                  <xsl:otherwise>
                                    <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE, "0.00"),".",",")'></xsl:value-of>
                                  </xsl:otherwise>
                                </xsl:choose>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

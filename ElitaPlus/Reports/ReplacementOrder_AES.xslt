<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 10px; FONT-FAMILY: arial;width:auto }
          TD { FONT-SIZE: 10px; FONT-FAMILY: arial;padding:0px;vertical-align:top; }
          #company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY:arial }
          #number {padding-left:20px; }
          .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center }
          .header { FONT-WEIGHT: bold; FONT-SIZE: 18px; TEXT-ALIGN: left }
          .sp { FONT-STYLE: italic }
          .bold { FONT-WEIGHT: bold; FONT-SIZE: 13px;}
          .tblBorderL{}
          .tblBorderL TD{padding:0px;}
          .tblBorderD{border:solid 1px #000;}
          .tblBorderD TD {padding:0px;}
          .tblOutline{padding:0px;width:100%;}
          .tblOutline TD{padding:0px;}
          .tblInner TD{padding:0px;}
          .TDBox {padding:0px;margin:0px;border:solid 1px #000;}
          .label {text-align:right;font-weight:bold;}
        </style>
      </head>
      <body>
        <table cellpadding="0" cellspacing="0" border="0" style="PADDING-RIGHT:25px;PADDING-LEFT:25px;width:100%">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0" style="WIDTH:100%">
                <tr>
                  <td>
                    <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                      <TR>
                        <td style="width:36%">
                          <xsl:element name="img">
                            <xsl:attribute name="src">
                              <xsl:choose>
                                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_aes.jpg")'/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_aes.jpg</xsl:text>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                          </xsl:element>
                        </td>
                        <td class="header" style="text-align:left" colspan="2">
                          AUTORIZACIÓN DE REEMPLAZO
                        </td>
                      </TR>
                    </TABLE>
                  </td>
                </tr>
                <tr>
                  <td style="text-align:center;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                      <tr>
                        <td colspan="5">
                          &#160;
                        </td>
                      </tr>
                      <tr>
                        <td class="label">Código Distribuidor: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                        </td>
                        <td  style="width:4%"></td>
                        <td class="label"  style="width:24%">N° de Autorización: &#160; </td>
                        <td class="TDBox"  style="width:24%">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </td>
                      </tr>
                      <TR>
                        <td class="label">Distribuidor: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_CODE" />
                        </td>
                        <td></td>
                        <td class="label">Fecha: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                        </td>
                      </TR>
                      <TR>
                        <td class="label">Direccíon: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_ADDRESS1" />
                        </td>
                        <td></td>
                        <td class="label">Provincia: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_STATE_PROVINCE" />
                          &#160;&#160;&#160;<span class="TDBox">&#160;C.P.:&#160;</span>&#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_ZIP" />
                        </td>
                      </TR>
                      <TR>
                        <td class="label"></td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_ADDRESS2" />
                        </td>
                        <td></td>
                        <td class="label">Teléfono: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_PHONE" />
                        </td>
                      </TR>
                      <TR>
                        <td class="label">Localidad: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_CITY" />
                        </td>
                        <td></td>
                        <td class="label">e-mail: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_EMAIL" />
                        </td>
                      </TR>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td height="20px"></td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                      <tr>
                        <td width="47%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                AUTORIZACIÓN
                              </td>
                            </tr>
                            <tr>
                              <td class="label"  style="width:45%">N° de Autorización: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Distribuidor: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:RPC_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de Certificado: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Fecha de compra: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Franquicia €: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:choose>
                                  <xsl:when test='format-number(a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT, "0.00") = "NaN"'>
                                    0,00
                                  </xsl:when>
                                  <xsl:otherwise>
                                    <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT, "0.00"),".",",")'></xsl:value-of>
                                  </xsl:otherwise>
                                </xsl:choose> 
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Autorización €: &#160; </td>
                              <td class="TDBox">
                                &#160;
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
                        <td>&#160;&#160;&#160;</td>
                        <td width="47%"  class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td class="label"  style="width:45%">Nombre: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Domicilio: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label"></td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Ciudad: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Provincia: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">CP: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                &#160;&#160;&#160;
                                <span class="TDBox">&#160;Teléfono&#160;</span>
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                
                <tr>
                  <td colspan="3" >
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                      <tr>
                        <td width="47%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                PRODUCTO A REEMPLAZAR
                              </td>
                            </tr>
                            <tr>
                              <td class="label" style="width:45%">Marca: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Modelo: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Descr. producto: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de serie: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de imei: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:IMEI" />
                              </td>
                            </tr>                            
                            <tr>
                              <td class="label">Fecha de compra: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de factura: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td>&#160;&#160;&#160;</td>
                        <td width="47%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                PRODUCTO DE REEMPLAZO
                              </td>
                            </tr>
                            <tr>
                              <td class="label"  style="width:45%">Marca: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Modelo: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Descr. producto: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de serie: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="label">N° de imei: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>                            
                            <tr>
                              <td class="label">Fecha de compra: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                </td>
                            </tr>
                            <tr>
                              <td class="label">N° de factura: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td class="TDBox title" colspan="3">
                    COMENTARIOS
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="height:100px;">
                    &#160;
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

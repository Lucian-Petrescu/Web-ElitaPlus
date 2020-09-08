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
          .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; text-align: center }
          .Header { FONT-WEIGHT: bold; FONT-SIZE: 18px; text-align: center; background-color:red; padding:3px }
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
                        <TD style="width:20%">
                          <xsl:element name="img">
                            <xsl:attribute name="src">
                              <xsl:choose>
                                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_aba.jpg")'/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_aba.jpg</xsl:text>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                          </xsl:element>
                        </TD>
                        <TD  style="width:60%;text-align:left">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr >
                              <td class="Header">
                                SERVICIO DE RECLAMACIÓN
                              </td>
                            </tr>
                            <tr>
                              <td>
                                &#160;
                              </td>
                            </tr>
                          </table>
                        </TD>
                        <td>
                          &#160;
                        </td>
                      </TR>
                    </TABLE>
                  </td>
                </tr>
                <tr>
                  <td style="text-align:center;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                      <tr>
                        <td class="label" style="width:23%">Número de orden: &#160; </td>
                        <td class="TDBox"  style="width:23%">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                        </td>
                        <td  style="width:4%"></td>
                        <td class="label"  style="width:24%">N° de Autorización: &#160; </td>
                        <td class="TDBox"  style="width:24%">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </td>
                      </tr>
                      <tr>
                        <td class="label">Centro: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                        </td>
                        <td></td>
                        <td class="label">Tipo de Servicio: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                        </td>
                      </tr>
                      <tr>
                        <td class="label">Código de Centro: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                        </td>
                        <td></td>
                        <td class="label">Teléfono: &#160; </td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                        </td>
                      </tr>
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
                        <td width="47%"  class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Nombre: &#160; </td>
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
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Teléfono: &#160; </td>
                              <td class="TDBox">
                                &#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td>&#160;&#160;&#160;</td>
                        <td width="47%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td colspan="2" class="TDBox title">
                                PRODUCTO
                              </td>
                            </tr>
                            <tr>
                              <td class="label">Marca: &#160; </td>
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
                              <td class="label">Descrip. producto: &#160; </td>
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
                      </tr>
                      <tr>
                        <td  colspan="3">
                          &#160;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" style="text-align:center;padding:5px;vertical-align:middle;" class="TDBox title">
                          DESCRIPCIÓN DE LA RECLAMACION
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="TDBox" style="height:50px">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
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
                    REPARACIÓN
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="height:50px;text-align:center">
                    <table cellpadding="0" width="80%" align="center">
                      <tr>
                        <td style="height:5px;">
                          &#160;
                        </td>
                        <td></td>
                      </tr>
                      <tr>
                        <td style="width:45%;text-align:right">Número de serie del producto: &#160; </td>
                        <td class="TDBox" style="width:55%">
                          &#160;
                        </td>
                      </tr>
                      <tr>
                        <td style="height:5px;">
                          &#160;
                        </td>
                        <td></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="text-align:center">
                    <table cellspacing="0" cellpadding="0" width="70%">
                      <tr>
                        <td style="width:33%">
                          <table cellspacing="0" cellpadding="0" width="50%" align="center">
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center" colspan="2">
                                Retirada
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                Sí
                              </td>
                              <td style="text-align:center">
                                No
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                              <td  style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                          </table>

                        </td>
                        <td style="width:20%">
                          &#160;
                        </td>
                        <td style="width:47%">
                          <table cellspacing="0" cellpadding="0" width="90%" align="center">
                            <td colspan="2">
                              &#160;
                            </td>
                            <tr>
                              <td style="height:30px;vertical-align:middle;text-align:right;white-space:nowrap">Firma del cliente: &#160; </td>
                              <td class="TDBox" style="width:220px">
                                &#160;
                              </td>
                            </tr>
                            <td colspan="2">
                              &#160;
                            </td>
                            <tr>
                              <td style="text-align:right;white-space:nowrap">Fecha de retirada: &#160; </td>
                              <td class="TDBox">
                                &#160;
                              </td>
                            </tr>
                            <td colspan="2">
                              &#160;
                            </td>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="text-align:center">
                    <table cellspacing="0" cellpadding="0" width="85%">
                      <tr>
                        <td style="width:33%">
                          <table cellspacing="0" cellpadding="0" width="50%" align="center">
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center" colspan="2">
                                Desplazamiento
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                Sí
                              </td>
                              <td style="text-align:center">
                                No
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                              <td  style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width:33%">
                          <table cellspacing="0" cellpadding="0" width="50%" align="center">
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center" colspan="2">
                                Reparación
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                Sí
                              </td>
                              <td style="text-align:center">
                                No
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                              <td  style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width:33%">
                          <table cellspacing="0" cellpadding="0" width="50%" align="center">
                            <tr>
                              <td colspan="2">
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center" colspan="2">
                                Materiales
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                Sí
                              </td>
                              <td style="text-align:center">
                                No
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                              <td  style="text-align:center">
                                <input type="checkbox" class="TDBox"></input>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
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
                  <td class="TDBox" colspan="3">
                    Descripción del servicio realizado o codigo de la reparación:
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="height:50px;">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3">
                    Material utilizado:
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3" style="height:50px;">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <table cellpadding="0" border="0" cellspacing="0" width="100%">
                      <tr>
                        <td class="TDBox" style="text-align:center">
                          Fecha
                        </td>
                        <td class="TDBox" style="text-align:center">
                          Hora entrada
                        </td>
                        <td class="TDBox" style="text-align:center">
                          Hora salida
                        </td>
                        <td class="TDBox" style="text-align:center">
                          Firma Cliente
                        </td>
                      </tr>
                      <tr>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                      </tr>
                      <tr>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                      </tr>
                      <tr>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                        <td class="TDBox" style="height:30px">
                          &#160;
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="TDBox" colspan="3">
                    Observaciones del cliente:
                  </td>
                </tr>
                <tr>
                  <td colspan="3" class="TDBox" style="height:50px">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="height:30px">
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

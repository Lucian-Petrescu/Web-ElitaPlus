<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 10px; FONT-FAMILY: arial;width:auto }
          TD { FONT-SIZE: 10px; FONT-FAMILY: arial;padding:3px;vertical-align:top; }
          #company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY:arial }
          #number {padding-left:20px; }
          #title {FONT-WEIGHT: bold; FONT-SIZE: 22px; TEXT-ALIGN: center;color:#ff0000;}
          .title {FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; }
          .xtitle {FONT-SIZE: 30px; color:#ff0000;}
          .sp { FONT-STYLE: italic }
          .bold { FONT-WEIGHT: bold; FONT-SIZE: 13px;}
          .tblBorderL{border:2px solid #000;}
          .tblBorderL TD{padding:0px;}
          .tblBorderD{}
          .tblBorderD TD {padding:20px;}
          .tblOutline{padding:10px;width:97%;}
          .tblOutline TD{padding:2px;}
          .tblInner TD{padding:0px;}
          .TDBox {padding:1px;margin:1px;border:solid 1px #111;}
          .label {text-align:right;font-weight:bold;}
          .red {color:#ff0000;}
        </style>
      </head>
      <body>
        <div style="position:absolute;top:10;left:0;z-index:900;">
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
        </div>
        <table cellpadding="0" cellspacing="0" border="0" style="PADDING-RIGHT:25px;PADDING-LEFT:25px;width:100%;z-index:1000">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0" style="WIDTH:100%">
                <tr>
                  <td>
                    <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                      <tr>
                        <td colspan="3" id="title" style="text-align:center;">
                          REPARACION
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" id="title" style="text-align:center;">
                          <br/><br/>ORDEN DE SERVICIO HOME WARRANTY&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<span class="xtitle">HW</span>
                        </td>
                      </tr>
                      <tr>
                        <td></td>
                        <td class="title" style="text-align:right">N° de Autorización:</td>
                        <td class="title TDBox xtitle">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </td>
                      </tr>
                    </TABLE>
                  </td>
                </tr>
                <tr></tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td class="label">Fecha:</td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                      </tr>
                      <TR>
                        <TD class="label">Destinario:</TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                        </TD>
                        <TD></TD>
                        <TD></TD>
                        <td class="label">Código Centro Servicio</td>
                        <td class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                        </td>
                      </TR>
                      <TR>
                        <TD></TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                        </TD>
                        <TD></TD>
                        <TD></TD>
                        <TD></TD>
                        <TD></TD>
                      </TR>
                      <TR>
                        <TD></TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                        </TD>
                        <TD></TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                        </TD>
                        <TD class="label">CP:</TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                        </TD>
                      </TR>
                      <TR>
                        <TD class="label">TE:</TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                        </TD>
                        <TD class="label">Fax:</TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                        </TD>
                        <TD class="label">EMail:</TD>
                        <TD class="TDBox">
                          &#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                        </TD>
                      </TR>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                      <tr>
                        <td width="50%" style="text-align:center;padding-top:3px;border-bottom:2px #000 solid;border-right:2px #000 solid;"
                            class="title">
                          AUTORIZACION
                        </td>
                        <td width="50%" style="text-align:center;padding-top:3px;border-bottom:2px #000 solid;"
                            class="title">
                          CLIENTE
                        </td>
                      </tr>
                      <tr>
                        <td width="47%" class="tblBorderD" style="border-right:solid 2px #000;">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
                                  <tr>
                                    <td class="label">N° de Autorización :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Dealer :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">N° de Certificado :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Fecha de compra :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                      <xsl:text>-</xsl:text>
                                      <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                      <xsl:text>-</xsl:text>
                                      <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">N° de factura :</td>
                                    <td class="TDBox">
                                      &#160;
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Deducible $ :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT, "0.00"),".",".")' />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Autorización $ :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00"),".",".")' />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label"> Reparación en :</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td width="47%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
                                  <tr>
                                    <td class="label">Nombre:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Domicilio:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Cuidado</td>
                                    <td>
                                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                          <td class="TDBox">
                                            &#160;
                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                          </td>
                                          <td class="label">Pcias:</td>
                                          <td class="TDBox">
                                            &#160;
                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td></td>
                                    <td></td>
                                  </tr>
                                  <tr>
                                    <td class="label">CP:</td>
                                    <td>
                                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                          <td class="TDBox">
                                            &#160;
                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                          </td>
                                          <td class="label">TE:</td>
                                          <td class="TDBox">
                                            &#160;
                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">DNI:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:IDENTIFICATION_NUMBER" />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                    <br />
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                      <tr>
                        <td width="50%" style="text-align:center;padding-top:3px;border-bottom:2px #000 solid;border-right:2px #000 solid;"
                            class="title">
                          PRODUCTO

                        </td>
                        <td width="50%" style="text-align:center;padding-top:3px;border-bottom:2px #000 solid;"
                            class="title">
                          IMPORTANTE
                        </td>
                      </tr>
                      <tr>
                        <td width="50%" class="tblBorderD" style="border-right:solid 2px #000;">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
                                  <tr>
                                    <td class="label">Marca:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Modelo:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Producto:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">No. de serie:</td>
                                    <td class="TDBox">
                                      &#160;
                                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="label">Fecha de Compra:</td>
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
                                    <td class="label">N° de factura:</td>
                                    <td class="TDBox">
                                      &#160;
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td width="50%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
                            <tr>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
                                  <tr>
                                    <td class="bold">
                                      1).  Confirmar recepción del mensaje<br />
                                      2).  Combinar visita/retiro con cliente<br />
                                      3).  Completar datos faltantes<br/>
                                      <span class="red">4). Enviar presupuesto por e-mail a Assurant Argentina</span>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td  class="title" style="text-align:center;padding-top:3px;border-top:2px #000 solid;border-bottom:2px #000 solid;border-right:solid 2px #000;">
                          Descripción de Fallas:
                        </td>
                        <td  class="title" style="text-align:center;padding-top:3px;border-top:2px #000 solid;border-bottom:2px #000 solid;">
                          Instrucciones Especiales:
                        </td>
                      </tr>
                      <tr>
                        <td  style="height:50px;border-right:solid 2px #000;">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                        </td>
                        <td  style="height:50px;">
                          Verificar mal uso.<br/><br/>
                          <span class="red">
                            El precio de la visita a domicilio es $50, debe ser cobrado al cliente por el técnico en el momento del diagnostico.<br/><br/>
                            Todos los presupuestos deben ser enviados por e-mail a Assurant Argentina, incluso cuando se trate de autorizaciones pre-aprobadas.<br/>
                          </span>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
              <br />
              <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                <tr>
                  <td style="text-align:center;padding-top:3px;border-bottom:1px #000 solid;"
                      class="title">
                    AVISO DE REPARACION
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;" class="bold">
                    Complete el siguiente formulario y envielo junto con la factura
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                    Número de serie del producto reparado:
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                    Fecha CUMPLIDO
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                    Descripción de la reparación o Código reparación

                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                    Utilizó repuestos&#160;&#160;&#160;&#160;SI&#160;&#160;&#160;&#160;NO
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                    Firma cliente
                  </td>
                </tr>
                <tr>
                  <td style="border-bottom:1px #000 solid;text-align:center;" class="bold">
                    COMENTARIOS del centro de servicio
                  </td>
                </tr>
                <tr>
                  <td style="height:50px" class="bold">

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

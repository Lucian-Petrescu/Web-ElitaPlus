<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 13px; FONT-FAMILY: Trebuchet MS,arial;width:auto }
          TD { FONT-SIZE: 13px; FONT-FAMILY: Trebuchet MS,arial;padding:0px;padding-left:2px;vertical-align:top; }
          #company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY: Trebuchet MS,arial }
          #title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center;color:#fff;background-color:#000; }
          #number {padding-left:20px; }
          .sp { FONT-STYLE: italic }
          .bold { FONT-WEIGHT: bold }
          .tblBorderL{border:solid 1px #a2a2a2}
          .tblBorderL TD{padding:0px;}
          .tblBorderD{border:solid 1px #000}
          .tblBorderD TD {padding:3px;}
          .footer{font-size:10px;}
        </style>
      </head>
      <body>
        <table cellpadding="0" cellspacing="0" border="0" style="PADDING-RIGHT:25px;PADDING-LEFT:25px;width:100%">
          <tr>
            <td>
              <table cellpadding="0" cellspacing="0" border="0" style="WIDTH:100%">
                <tr>                  
                  <td rowspan="5" style="vertical-align:top;text-align:right;">
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
                  <td colspan="2">
                    <span class="bold">To:&#160;</span>
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <span class="bold">Date:&#160;</span>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" />
                    <xsl:text>/</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" />
                    <xsl:text>/</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td id="title" colspan="2">
                    REPLACEMENT AUTHORIZATION (AUTORIZACION PARA REEMPLAZO)
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td>
                    <span class="bold">AUTHORIZATION NUMBER:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                  <td>
                    <span class="bold">DATE PROCESSED:&#160;</span><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                  </td>
                </tr>
                <tr>
                  <td>(NUMERO DE AUTORIZACION)</td>
                  <td>(FECHA PROCESADA)</td>
                
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                
                </tr>
                <tr>
                  <td>
                    <span class="bold">CLIENT:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                  </td>
                  <td>
                    <span class="bold">COVERAGE TYPE:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
                  </td>
                
                </tr>
                <tr>
                  <td>(CLIENTE)</td>
                  <td>(TIPO DE SERVICIO)</td>
                
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td><span class="bold">METHOD OF REPAIR:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" /></td>
                </tr>                
                <tr>
                  <td>&#160;</td>
                  <td></td>
                
                </tr>
                <tr>
                  <td id="title" colspan="2">
                    CUSTOMER INFORMATION (INFORMACION DEL CLIENTE)
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td colspan="2">
                    <span class="bold">CERTIFICATE NUMBER:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    (NUMERO DE CERTIFICADO)
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <span class="bold">CUSTOMER NAME:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    (NOMBRE DEL CLIENTE)
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <span class="bold">CUSTOMER ADDRESS:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />&#160; <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />&#160; <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />&#160; <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    (DIRECCION)
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    &#160;
                  </td>
                </tr>
                <tr>
                  <td>
                    <span class="bold">HOME PHONE:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                  </td>
                  <TD>
                    <span class="bold">WORK PHONE:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                  </TD>

                </tr>
                <tr>
                  <td>
                    (TELEFONO CASA)
                  </td>
                  <td>
                    (TELEFONO TRABAJO)
                  </td>
                </tr>
                <tr>
                  <td>
                    &#160;
                  </td>
                  <td>

                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <span class="bold">E-MAIL ADDRESS:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                  </td>
                </tr>
                <tr>
                  <td>
                    (DIRECCIÓN DE CORREO ELECTRONICO)
                  </td>
                </tr>
                <tr>
                  <td id="title" colspan="2">
                    PRODUCT INFORMATION (INFORMACION DEL PRODUCTO)
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td>
                    <span class="bold">PRODUCT:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                  </td>
                  <td>
                    <span class="bold">BRAND:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                  </td>
                </tr>
                <tr>
                  <td>(PRODUCTO)</td>
                  <td>(MARCA)</td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td>
                    <span class="bold">MODEL:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                  </td>
                  <td>
                    <span class="bold">SERIAL NUMBER:&#160;</span><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                  </td>
                </tr>
                <tr>
                  <td>(MODEL0)</td>
                  <td>(SERIAL NUMBER)</td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td>
                    <span class="bold">DATE OF LOSS:&#160;</span><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                  </td>
                  <td></td>
                </tr>
                <tr>
                  <td>(FECHA DE PERDIDA)</td>
                  <td></td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td COLSPAN="2">
                    <span class="bold">SPECIAL INSTRUCTIONS:</span> (DESCRIPTION DEL PROBLEMA)</td>
                </tr>
                <tr>
                  <td COLSPAN="2">
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td colspan="2" id="title">
                    REPLACEMENT UNIT RECEIPT ACKNOWLEDGEMENT
                    <br/>(CONFIRMACION RECIBO UNIDAD DE REEMPLAZO)
                  </td>
                </tr>
                <tr>
                  <td colspan="2" class="footer">
                    I hereby acknowledge receipt of replacement unit from an authorized representative, according to the terms and conditions of the program. (Por medio de ésta CERTIFICO que el representante autorizado me ha hecho entrega de la unidad de reemplazo, acorde a los términos y condiciones del Programa).
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                  <td></td>
                </tr>
                <tr>
                  <td colspan="2" style="border-bottom:#000 1px solid">&#160;</td>
                </tr>
                <tr>
                  <td class="footer">Customer Signature</td>
                  <td class="footer">Authorized Representative Signature</td>
                </tr>
                <tr>
                  <td class="footer">(Firma Cliente)</td>
                  <td class="footer">(Firma Representante Autorizado)</td>
                </tr>
                <!--<tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td width="50%"></td>
                                                <td class="bold" align="right">
                                                    <P align="right">Authorization Number:
                                            </P>
                                                </td>
                                                <td class="bold" id="number">
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td class="bold">
                                                    <P align="right">
                                                        <span class="sp">(# de autorizacion)</span>
                                                    </P>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td>Date: <span class="sp">(Fecha)</span></td>
                                                <td>
                                                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>Service Center Code:</td>
                                                <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                                                </td>
                                            </tr>
                                            <TR>
                                                <TD>Recipient:</TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD colspan="2">
                                                    <span class="sp">(Codigo Tienda o Centro de Servicio)</span>
                                                </TD>
                                            </TR>
                                            <TR>
                                                <TD>
                                                    <span class="sp">(Destinario)</span>
                                                </TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                            </TR>
                                             <TR>
                                                <TD>
                                                    <span class="sp"></span>
                                                </TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                            </TR>
                                            <TR>
                                                <TD></TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                                </TD>
                                                <TD></TD>
                                                <TD align="left">
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                                </TD>
                                                <TD>Zip Code <span class="sp">(Codigo Postal)</span></TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                                                </TD>
                                            </TR>
                                            <TR>
                                                <TD>Telephone:</TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                                                </TD>
                                                <TD>Fax:</TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                                                </TD>
                                                <TD>E-Mail:</TD>
                                                <TD>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                                                </TD>
                                            </TR>
                                            <TR>
                                                <TD>
                                                    <span class="sp">(Telefono)</span>
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                            </TR>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                                            <tr>
                                                <td width="50%" style="text-align:center;padding-top:3px" class="bold">
                                                    AUTHORIZATION <span class="sp">(Autorizacion)</span>
                                                
                                                </td>
                                                <td width="50%" style="text-align:center;padding-top:3px" class="bold">
                                                    CLIENT <span class="sp">(Cliente)</span>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" class="tblBorderD">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td>Authorization # <span class="sp">(# Autorizacion)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Account # <span class="sp">(Numero de Cuenta)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CAMPAIGN_NUMBER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Certificate #:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Product Date <span class="sp">(Fecha de compra)</span>:</td>
                                                            <td>
                                                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,6,2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 9, 2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Invoice # <span class="sp">(# Factura)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Coverage Type <span class="sp">(Tipo de Cobertura)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Date of Loss <span class="sp">(Fecha de Perdida)</span>:</td>
                                                            <td>
                                                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" /><xsl:text>/</xsl:text><xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Deductible <span class="sp">(Deducible)</span>$:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="50%" class="tblBorderD">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td>Customer's Name:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="sp">(Nombre del Cliente)</span>
                                                            </td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Customer's Address:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                             <span class="sp">(Direccion)</span>
                                                             </td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               
                                                            </td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Zip Code <span class="sp">(Codigo Postal)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Telephone <span class="sp">(Telefono)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold">
                                                    PRODUCT BEING REPLACED<BR /> <span class="sp">(Producto a Reemplazarse)</span>
                                                
                                                </td>
                                                <td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold">
                                                    REPLACEMENT PRODUCT INFORMATION<BR /> <span class="sp">(Informacion Reemplazo del Producto)</span>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" class="tblBorderD">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td>Make <span class="sp">(Marca)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Model <span class="sp">(Modela)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Product <span class="sp">(Producto)</span>:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>IMEI/Serial Number #:</td>
                                                            <td>
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="50%" class="tblBorderD">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td>Make <span class="sp">(Marca)</span>:</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Model <span class="sp">(Modela)</span>:</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Product <span class="sp">(Producto)</span>:</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>IMEI/Serial Number #:</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Pick-Up date <span class="sp">(Fecha Entrega)</span>:</td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bold">
                                        Comments
                                    </td>
                                </tr>
                                <tr>
                                    <td class="sp">
                                        (Comentarios)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tblBorderD" style="height:50px;">
                                        &#160;
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
                                    </td>
                                </tr>
                                <tr>
                                    <td id="title" style="padding-top:10px;padding-bottom:10px;">
                                        REPLACEMENT UNIT RECEIPT ACKNOWLEDGEMENT<BR />
                                        <SPAN CLASS="SP">(CONFIRMACION RECIBO UNIDAD DE REEMPLAZO)</SPAN>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="line-height:20px">I hereby acknowledge receipt of replacement unit from an authorized representative, according to the terms and conditions of the program.  
                                    <span class="sp">Por medio de ésta CERTIFICO que el representante autorizado me ha hecho entrega de la unidad de reemplazo, acorde a los términos y condiciones del Programa.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspaccing="0" border="0" width="100%">
                                            <td style="vertical-align:bottom;padding:0px;" colspan="2">
                                                <hr style="border-top:1px solid #000;padding:0px;" />
                                            </td>
                                            <tr>
                                                <td width="50%" style="vertical-align:top;padding:0px;" >Customer Signature</td>
                                                <td style="vertical-align:top;padding:0px;">Authorized Representative Signature</td>
                                            </tr>
                                            <tr>
                                                <td width="50%" style="vertical-align:top;padding:0px;">
                                                    <span class="sp">(Firma Cliente)</span>
                                                </td>
                                                <td style="vertical-align:top;padding:0px;">
                                                    <span class="sp">(Firma Representante Autorizado)</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>-->
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

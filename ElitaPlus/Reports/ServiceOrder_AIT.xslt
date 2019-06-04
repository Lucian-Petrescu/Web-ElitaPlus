<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
    <xsl:output indent="yes" method="html" encoding="utf-8"/>
    <xsl:template match="/">
        <html>
            <head>
                <style> BODY { FONT-SIZE: 10px; FONT-FAMILY: arial;width:auto }
	TD { FONT-SIZE: 10px; FONT-FAMILY: arial;padding:3px;vertical-align:top; }
	#company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY:arial }
	#number {padding-left:20px; }
	#title { FONT-WEIGHT: bold; FONT-SIZE: 18px; TEXT-ALIGN: left;}
	.title { FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; }
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
                                                <TD>
                                                  <xsl:element name="img">
                                                    <xsl:attribute name="src">
                                                      <xsl:choose>
                                                        <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                                          <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_ita.jpg")'/>
                                                        </xsl:when>
                                                        <xsl:otherwise>
                                                          <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_ita.jpg</xsl:text>
                                                        </xsl:otherwise>
                                                      </xsl:choose>
                                                    </xsl:attribute>
                                                  </xsl:element>
                                                </TD>
                                                <td id="title" colspan="2" style="vertical-align:middle">
                                                    ORDINE DI INTERVENTO
                                                </td>
                                            </TR>
                                            <tr>
                                                <td></td>
                                                <td id="company">Autorizzazione N°:</td>
                                                <td id="company" class="TDBox">&#160;
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
                                                <td class="label">Data:</td>
                                                <td class="TDBox">&#160;
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
                                                <TD class="label">Destinazione:</TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <td class="label">Codice Centro Assistenza:</td>
                                                <td class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                                                </td>
                                            </TR>
                                            <TR>
                                                <TD></TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                                                </TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                                <TD></TD>
                                            </TR>
                                            <TR>
                                                <TD></TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                                </TD>
                                                <TD></TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                                </TD>
                                                <TD class="label">Cap:</TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                                                </TD>
                                            </TR>
                                            <TR>
                                                <TD class="label">Telefono:</TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                                                </TD>
                                                <TD class="label">Fax:</TD>
                                                <TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                                                </TD>
                                                <TD class="label">Email:</TD>
                                                <TD class="TDBox">&#160;
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
                                                    AUTORIZZAZIONE
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
                                                                        <td class="label"> Autorizzazione N° :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Negozio :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">N° Certificato :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Data d’acquisto :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">N° fattura :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Franchigia :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Autorizzazzione :</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Riparazione :</td>
                                                                        <td class="TDBox">&#160;
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
                                                                        <td class="label">Nome:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Indirizzo:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Città:</td>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                <tr>
                                                                                    <td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                                                                    </td>
                                                                                    <td class="label">Prov.:</td>
                                                                                    <td class="TDBox">&#160;
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
                                                                        <td class="label">Cap:</td>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                <tr>
                                                                                    <td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                                                                    </td>
                                                                                    <td class="label">TEL:</td>
                                                                                    <td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">CF:</td>
                                                                        <td class="TDBox">&#160;
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
                                                    PRODOTTO
                                                
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
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Modello:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Prodotto:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Serie N°:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">Data d’acquisto:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">N° Fattura:</td>
                                                                        <td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" class="title" style="text-align:center;padding-top:3px;border-top:2px #000 solid;border-bottom:2px #000 solid;">
                                                                            Descrizione del guasto
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="height:50px;">
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
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
                                                                            1).  Confermare ricezione del  messaggio<br />
                                                                            2).  Coordinare visita/ritiro con il cliente<br />
                                                                            3).  Completare dati mancanti
                                                                            
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
                            <br />
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
                                <tr>
                                    <td style="text-align:center;padding-top:3px;border-bottom:1px #000 solid;background-color:#b0b0b0"
                                        class="title">
                                                    AVVISO DI RIPARAZIONE
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                                    Compilare il seguente formulario ed inviarlo con la fattura
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                                    Numero di serie del prodotto riparato:
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                                    Data Terminazione
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                                    Descrizione della riparazione o codice riparazione
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                        Utilizzo pezzi di ricambio&#160;&#160;&#160;&#160;SÌ&#160;&#160;&#160;&#160;NO
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;" class="bold">
                                                    Firma del Cliente
                                               </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom:1px #000 solid;text-align:center;background-color:#b0b0b0"
                                        class="bold">
                                                    COMMENTI del centro Assistenza
                                               </td>
                                </tr>
                                <tr>
                                    <td style="height:50px" class="bold"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>

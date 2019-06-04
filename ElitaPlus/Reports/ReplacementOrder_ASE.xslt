<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
    <xsl:template match="/">
        <html>
            <head>
                <style>
                  BODY {
                  FONT-SIZE: 8px;
                  FONT-FAMILY: times new roman, arial;
                  width:auto;
                  margin-top:2px;
                  background: url(
                  <xsl:choose>
                    <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                      <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"weave_symbol_line.jpg")'/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>http://w1.assurant.com/elitalogos/weave_symbol_line.jpg</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>) no-repeat center center;
                  }
                  TD {
                  padding-right:3px;
                  padding-left:3px;
                  vertical-align:top;
                  white-space:nowrap;
                  }
                  .headerText {
                  font-size:10px;
                  font-style:italic;
                  font-weight:bold;
                  FONT-FAMILY: Palatino, arial
                  }

                  .labelr {
                  text-align:right;
                  padding-right:10px;
                  font-weight:normal;
                  }

                  .label {
                  text-align:left;
                  padding-right:10px;
                  font-weight:normal;
                  width:110px;
                  white-space:nowrap;
                  }

                  #company {
                  FONT-WEIGHT: bold;
                  FONT-SIZE: 14px;
                  FONT-FAMILY: Trebuchet MS, arial
                  }
                </style>
            </head>
            <body>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                        <td id="company">
                            Assurant Solutions Nordic
                        </td>
                        <td rowspan="5" width="33%" style="text-align:right">
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
                        </td>
                    </tr>
                    <tr>
                        <td width="33%">
                        </td>
                        <td width="33%" class="headerText">
                            Borupvang 2 B, 2. sal
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="headerText">
                            DK-2750 Ballerup
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                            &#160;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                        </td>
                        <td class="headerText">
                            Telefon: +45 33 36 00 00
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                        </td>
                        <td class="headerText">
                            Telefax: +45 33 36 00 01
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                            ,&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                        </td>
                        <td class="headerText">
                            CVR.nr.: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:TAX_ID" />
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%" style="text-align:right">
                            Dato:
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                            <xsl:text>-</xsl:text>
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                            <xsl:text>-</xsl:text>
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%" style="text-align:right">
                            Ordrenummer:&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">

                            <table cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="3">
                                        Re.: Tilbud/Dokumentation/Ordre
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        &#160;
                                    </td>
                                    <td>
                                    </td>
                                    <td rowspan="1" width="33%">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vi skal hermed
                                    </td>
                                    <td>
                                        &#160;&#160;
                                        <xsl:choose>
                                            <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT = 0" >
                                                <input id="Checkbox1" type="checkbox" checked="true" />
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <input id="Checkbox1" type="checkbox" />
                                            </xsl:otherwise>
                                        </xsl:choose>
                                        &#160;anmode om tilbud/dokumentation
                                    </td>
                                    <td rowspan="1" width="33%">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &#160;
                                    </td>
                                    <td>
                                        &#160;&#160;
                                        <xsl:choose>
                                            <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT != 0" >
                                                <input id="Checkbox2" type="checkbox" checked="true" />
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <input id="Checkbox2" type="checkbox"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                        &#160;bekræfte ordre
                                    </td>
                                    <td rowspan="1" width="33%">
                                    </td>
                                </tr>

                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="text-align: right; width: 30%;" class="labelr">
                                        Dato
                                    </td>
                                    <td class="label">
                                        .........................&#160;
                                    </td>
                                    <td>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,9,2)" />
                                        <xsl:text>-</xsl:text>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 6, 2)" />
                                        <xsl:text>-</xsl:text>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; height: 19px; text-align: right" class="labelr">
                                        Navn
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; text-align: right; height: 25px;" class="labelr">
                                        Adresse
                                    </td>
                                    <td style="height: 25px;" class="label">
                                        .........................
                                    </td>
                                    <td style="height: 25px">
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Post nr.
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        By
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%; height: 19px; text-align: right" class="labelr">
                                        Telefon
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Mobil
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        e-mail
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Police
                                    </td>
                                    <td class="label">
                                        Nummer............
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Udløbsdato........
                                    </td>
                                    <td>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE,9,2)" />
                                        <xsl:text>-</xsl:text>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE, 6, 2)" />
                                        <xsl:text>-</xsl:text>
                                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_END_DATE, 1, 4)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Forsikringsværdi
                                    </td>
                                    <td>
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
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Skadehistorik
                                    </td>
                                    <td>
                                        <xsl:choose>
                                            <xsl:when test='format-number(a:ServiceOrderReport/a:ServiceOrder/a:TOTAL_PAID, "0.00") = "NaN"'>
                                                0,00
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:TOTAL_PAID, "0.00"),".",",")'></xsl:value-of>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Produkt
                                    </td>
                                    <td class="label">
                                        Mærke...............
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Model................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Serie nummer.....
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Reference
                                    </td>
                                    <td class="label">
                                        Nummer............
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_NUMBER" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Skade
                                    </td>
                                    <td class="label">
                                        Nummer............
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                    </td>
                                    <td class="label">
                                        Type (kode)
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CAUSE_OF_LOSS" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Beskrivelse
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td style="white-space:normal">
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        &#160;
                                    </td>
                                    <td class="label">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Servicetype
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelr">
                                        Godkendt beløb
                                    </td>
                                    <td class="label">
                                        .........................
                                    </td>
                                    <td>
                                        <xsl:choose>
                                            <xsl:when test='format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00") = "NaN"'>
                                                0,00
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00"),".",",")'></xsl:value-of>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &#160;Med venlig hilsen
                            <br/>
                            <br/>
                            Skadesafdelingen
                            <br/>
                            Assurant Solutions Nordic A/S
                            <br/>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COMPANY_EMAIL" />
                        </td>
                        <td>
                        </td>
                        <td rowspan="1" width="33%">
                        </td>
                    </tr>
                </table>
            </body>
        </html>

    </xsl:template>
</xsl:stylesheet>

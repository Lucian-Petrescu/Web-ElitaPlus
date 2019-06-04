<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 12px; FONT-FAMILY: Trebuchet MS;width:auto;color:#000; }
          TD { FONT-SIZE: 12px; FONT-FAMILY: Trebuchet MS;padding:5px;vertical-align:middle; }
          #address {font-size: 10px;color:#bbb;}
          #title {text-align:center;font-size:20px;font-weight:normal;text-decoration:underline;}
          #centername {font-size: 9px;padding:3px;text-align:right}
          #centername TABLE{border:1px #000 solid;}
          #centername TD{vertical-align:top;font-size: 9px;padding:0px;padding-left:3px;}
          .header{text-align:center;font-size:14px;}
          .section{text-align:center;font-size:16px;font-weight:bold;}
          .desc{text-align:center;}
          .boxed {border:1px solid #000;}
          .boxed TD {border-bottom:1px solid #000;}
          .content{width:100%}
          .content TD{border:none;}
          .chk{display:inline;border:solid 1px #000;width:25px;height:25px;}
          .footer{FONT-SIZE: 10px;}
        </style>
      </head>
      <body>
        <table cellpadding="0" cellspacing="0" border="0" style="margin-RIGHT:60px;margin-LEFT:60px;width:auto;">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0" style="WIDTH:100%">
                <tr>
                  <td>
                    <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                      <TR>
                        <TD style="width:150px;vertical-align:top;">
                          <xsl:element name="img">
                            <xsl:attribute name="src">
                              <xsl:choose>
                                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_adg.jpg")'/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_adg.jpg</xsl:text>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                          </xsl:element>
                        </TD>
                        <TD id="address">
                          Assurant Deutschland GmbH<br />
                          ATRICOM<br />
                          Lyoner Strasse 15<br />
                          D-60528 Frankfurt am Main<br />
                          Tel. 0700-73526372<br />
                          Fax +49 (0)69/66164867<br />
                          www.assurantsolutions.de

                        </TD>
                        <td id="centername">
                          <table cellpadding="0" cellspacing="0" border="0" style="text-align:left">
                            <tr>
                              <td>
                                Werkstatt:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />&#160;

                              </td>
                            </tr>
                            <tr>
                              <td>
                                Kundenzentrum -<br />
                                Code:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                CP:<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>Telefon:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>Fax:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                              </td>
                            </tr>
                            <tr>
                              <td>Email:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </TR>
                    </TABLE>
                  </td>
                </tr>
                <tr>
                  <td style="text-align:right">
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                    <xsl:text>.</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                    <xsl:text>.</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                  </td>
                </tr>
                <tr>
                  <td id="title">
                    Werkstattgarantie
                  </td>
                </tr>
                <tr>
                  <td class="header">
                    zur Leistungsnummer&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="100%" class="boxed">
                      <tr>
                        <td class="section" colspan="2">
                          Wichtig
                        </td>
                      </tr>
                      <tr>
                        <td class="desc" colspan="2">
                          Unten genannter Reparaturfall fällt unter die Werkstattgarantie. Das Produkt wird Ihnen direkt vom Kunden zugesandt.
                        </td>
                      </tr>
                      <tr>
                        <td class="section" style="border-right:1px solid #000;width:50%">
                          Originalprodukt
                        </td>
                        <td class="section">
                          Kunde
                        </td>
                      </tr>
                      <tr>
                        <td style="border-right:1px solid #000;">
                          <table cellpadding="5" cellspacing="0" border="0" class="content">
                            <tr>
                              <td width="40%">Hersteller</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                            <tr>
                              <td>Modell</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                            </tr>
                            <tr>
                              <td>IMEI</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Sonstiges /<br />
                                Zubehör
                              </td>
                              <td></td>
                            </tr>
                          </table>
                        </td>
                        <td>
                          <table cellpadding="5" cellspacing="0" border="0" class="content">
                            <tr>
                              <td width="20%">Name</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>Straße</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td>PLZ / Ort</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />&#160;&#160;/&#160;&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                              </td>
                            </tr>
                            <tr>
                              <td>Telefon</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>Email</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="section">
                          Problembeschreibung
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="100%" class="boxed">
                      <tr>
                        <td class="section">
                          Versandadresse
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table cellpadding="5" cellspacing="0" border="0" class="content">
                            <tr>
                              <td width="35%">
                                <div class="chk"></div>&#160;Wie Kundenadresse
                              </td>
                              <td>&#160;</td>
                            </tr>
                            <tr>
                              <td>
                                <div class="chk"></div>&#160;Andere Lieferadresse:
                              </td>
                              <td>
                                Name
                              </td>
                            </tr>
                            <tr>
                              <td>&#160;</td>
                              <td>
                                Straße
                              </td>
                            </tr>
                            <tr>
                              <td>&#160;</td>
                              <td>
                                PLZ/ Ort
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <div class="chk"></div>&#160;Selbstabholer
                              </td>
                              <td>&#160;</td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="100%" class="boxed">
                      <tr>
                        <td class="section" colspan="2">
                          Durch Werkstatt auszufüllen!
                        </td>
                      </tr>
                      <tr>
                        <td class="desc" colspan="2">
                          Bitte nach erfolgter Reparatur und Versand ausgefüllt per Fax oder Email an uns zurücksenden!
                        </td>
                      </tr>
                      <tr>
                        <td width="35%" style="border-right:1px solid #000;">
                          Reparatur erfolgte am:
                        </td>
                        <td>&#160;</td>
                      </tr>
                      <tr>
                        <td style="border-right:1px solid #000;">
                          Reparatur erfolgte durch:
                        </td>
                        <td>&#160;</td>
                      </tr>
                      <tr>
                        <td style="border-right:1px solid #000;">
                          Versand des Gerätes am:
                        </td>
                        <td>&#160;</td>
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

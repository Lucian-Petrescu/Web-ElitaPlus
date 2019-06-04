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
          #lined{border-bottom:1px #000 solid;}
          .content{width:100%}
          .content TD{border:none;}
          .chk{display:inline;border:solid 1px #000;width:25px;height:25px;}
          .chk2{display:inline;border:solid 1px #000;width:35px;height:20px;}
        </style>
      </head>
      <body>
        <div class="Section1" style="width:631px">

          <p class="MsoNormal" style='margin-left:4.0in'>
            Assurant Allgemeine,
            <br/>
            Zweigniederlassung der Assurant
            <br/>
            General Insurance Limited
            <br/>
            Lyoner Strasse 15
            <br/>
            60528 Frankfurt am Main
            <br/>
            Germany
            <br/>
            T +49 (0)800 6644704
            <br/>
            F +49 (0)180 5008116
            <br/>
            DE.Servicepartner@assurant.com
            <br/>
            www.assurantsolutions.com
          </p>

          <p class='MsoHeader'>
            <span lang='EN-GB' style='font-size: 7.0pt;font-family:"Trebuchet MS";color:#999999'>
              Assurant Ltd.,
              Lyoner Strasse 15, 60528 Frankfurt am Main
            </span>
          </p>
          <p class='MsoNormal'>
            <span lang='EN-GB' style='font-size:10.0pt;font-family:"Trebuchet MS"'>
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
              <br/>    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
              <br/>     <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />&#160;&#160;
              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />

            </span>
          </p>

          <p class='MsoNormal' style='margin-left:4.0in;text-align:right'>
            Frankfurt,
            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
            <xsl:text>.</xsl:text>
            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
            <xsl:text>.</xsl:text>
            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
          </p>
          <p class='MsoNormal' align='center' style='text-align:center'>
            <b>
              <span style='font-family:"Trebuchet MS";font-size:14pt;'>Reparaturauftrag</span>
            </b>
          </p>
          <table class='MsoTableGrid' border='1' cellspacing='0' cellpadding='0' width='631'
           style='width:472.9pt;border-collapse:collapse;border:none'>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
  background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Wichtig</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' valign='top' style='width:472.9pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <span lang='DE'
  style='font-size:10.0pt;font-family:"Trebuchet MS"'>
                    Bitte informieren Sie uns
                    über die Höhe der Reparaturkosten per Fax, Email oder Telefon an die o.g.
                    Rufnummern oder Emailadresse.
                    <br/>
                    <br/>
                    Bei Reparaturen vor Ort
                    melden Sie uns die Reparaturkosten bitte nur telefonisch unter
                    <br/>
                    <br/>
                    <b>
                      0800 – 6644704
                    </b>
                  </span>
                </p>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:9.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' valign='top' style='width:631px;border:none;
  border-bottom:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
  border-top:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Basisdaten</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td  colspan='2' valign='top' style='border:none;border-left:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;'>
                <p class='MsoNormal' style='text-align:justify;white-space:no-wrap'>
                  Assurant Schadennummer:
                  <br/>
                  Zertifikatsnummer:
                  <br/>
                  Versicherungsschutz:
                </p>
              </td>
              <td colspan="4" valign='top' style='border:none;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify;white-space:no-wrap'>
                  &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  <br/>
                  &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                  <br/>
                  &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
                </p>
              </td>
            </tr>
            <tr>
              <td width='307' colspan='3' style='width:230.55pt;border:solid windowtext 1.0pt;
  border-right:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Produktdaten</span>
                  </b>
                </p>
              </td>
              <td width='323' colspan='3' style='width:242.35pt;border:solid windowtext 1.0pt;
  border-left:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Kundendaten</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='102' valign='top' style='width:76.3pt;border:none;border-left:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                <p class='MsoNormal' style='text-align:justify;white-space:no-wrap;vertical-align:top'>
                  Hersteller:
                  <br/>Modell:
                  <br/>IMEI:
                  <br/>Kaufdatum:
                  <br/>MSISDN:
                  <br/>Selbstbehalt:
                </p>
              </td>
              <td width='206' colspan='2' style='width:154.25pt;border:none;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                <p class='MsoNormal' style='text-align:justify;white-space:no-wrap;vertical-align:top'>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                  <br/>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                  <br/>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                  <br/>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                  <xsl:text>.</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                  <xsl:text>.</xsl:text>
                  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                  <br/>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                  <br/>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT" />
                </p>
              </td>

              <td width='78' valign='top' style='width:58.35pt;border:none;padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                <p class='MsoNormal' style='text-align:justify;vertical-align:top'>
                  Name:
                  <br/>Adresse:
                  <br/>PLZ/Ort:
                  <br/>Telefon:
                  <br/>Email:
                </p>
              </td>
              <td width='245' colspan='2' valign='top' style='width:184.0pt;border:none;
  border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                <p class='MsoNormal' style='text-align:justify;vertical-align:top'>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                  <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                  <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />&#160;&#160;/&#160;&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                  <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                  <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
              background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span
  lang='EN-GB' style='font-size:11.0pt;font-family:"Trebuchet MS"'>Problembeschreibung</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal'>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:none;border-bottom:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <span lang='EN-GB'
  style='font-size:10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
  border-top:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span
  lang='EN-GB' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                      Spezielle Anweisung
                    </span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='631' colspan='6' style='width:472.9pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal'>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />&#160;
                </p>
              </td>
            </tr>
            <tr>
              <td width='633' colspan='10' valign='top' style='width:474.9pt;border:solid windowtext 1.0pt;
  border-top:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Adresse für die Abholung:</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td width='180' colspan='4' valign='top' style='width:134.7pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>Filiale</span>
                </p>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>Straße, Nr.</span>
                </p>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>PLZ Ort</span>
                </p>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>Telefonnummer</span>
                </p>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>Ansprechpartner</span>
                </p>
              </td>
              <td width='454' colspan='6' valign='top' style='width:340.2pt;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
                <table width='"100%"' align="left">
                  <tr>
                    <td width="70%" align="left" style='padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                      <p class='MsoNormal' style='text-align:justify'>
                        Persönliche Details :
                        <br/>Name:
                        <br/>Address1:
                        <br/>Address2:
                        <br/>Address3:
                        <br/>Postleitzahl:
                        <br/>Region, Stadt:
                        <br/>Land:
                        <br/>Telefon(Private):
                        <br/>Telefon(Arbeit):
                        <br/>Mobiltelefon:
                        <br/>Email:
                      </p>
                    </td>
                    <td width='30%' valign='top' style='padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                      <p class='MsoNormal' style='text-align:justify'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_SALUTATIONID" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_NAME" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_ADDRESS1" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_ADDRESS2" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_ADDRESS3" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_POSTALCODE" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_REGION" />&#160;&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_CITY" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_COUNTRY" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_HOMEPHONE" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_WORKPHONE" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_CELLPHONE" />
                        <br/><xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SHIPPING_EMAIL" />
                      </p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr height='0'>
              <td width='102' style='border:none'></td>
              <td width='57' style='border:none'></td>
              <td width='149' style='border:none'></td>
              <td width='78' style='border:none'></td>
              <td width='123' style='border:none'></td>
              <td width='123' style='border:none'></td>
            </tr>
          </table>
          <p class='MsoNormal' style='text-align:justify'>
            <span lang='DE' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
              <br/>
              Mit freundlichen Grüßen
              <br/> Assurant Ltd.
            </span>
          </p>

          <p class='MsoNormal' style='margin-left:4.0in'>
            <span lang='DE'>


            </span>
          </p>

          <p>
            <span lang='DE' style='font-size:8.0pt;font-family:"Trebuchet MS"'>
              Assurant Deutschland GmbH - Geschäftsführer: Andrew James Morris, Timothy Patrick Clancy
              <br/>Amtsgericht Frankfurt a.M. (Sitz der Gesellschaft) HRB 75647; Steuer- Nummer: 045 228 51885;
              <br/>Umsatzsteuer ID-Nummer: DE 251426332
              <br/>HSBC Trinkaus Burkhardt, Düsseldorf - Kto-Nr. 11887007 (BLZ 30030880) IBAN: DE37 3003 0880 0011 8870 07
            </span>
          </p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

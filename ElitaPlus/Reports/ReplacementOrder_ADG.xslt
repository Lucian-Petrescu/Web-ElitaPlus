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
              <br/>  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />&#160;&#160;
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
              <span style='font-family:"Trebuchet MS";font-size:14pt;'>Geräteaustausch</span>
            </b>
          </p>
          <table class='MsoTableGrid' border='1' cellspacing='0' cellpadding='0' width='631'
           style='width:633px;border-collapse:collapse;border:none'>
            <tr>
              <td colspan='10' style='width:633px;border:solid windowtext 1.0pt;
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
              <td colspan='10' valign='top' style='width:633px;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <span lang='DE'
  style='font-size:10.0pt;font-family:"Trebuchet MS"'>
                    Bei dem u.g. Gerät liegt ein Totalschaden vor. Daher soll ein Austausch mit dem gleichen Gerät erfolgen. Bitte prüfen Sie, ob dieses Gerät noch vorrätig ist und informieren Sie uns innerhalb der nächsten 24 Stunden über Email, Fax oder telefonisch (Rufnummern und Emailadresse siehe Briefkopf)
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
              <td colspan='10' valign='top' style='width:631px;border:none;
  border-bottom:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr>
              <td colspan='10' style='width:633px;border:solid windowtext 1.0pt;
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
              <td  colspan='3' valign='top' style='border:none;border-left:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;'>
                <p class='MsoNormal' style='text-align:justify;white-space:no-wrap'>
                  Assurant Schadennummer:
                  <br/>
                  Zertifikatsnummer:
                  <br/>
                  Versicherungsschutz:
                </p>
              </td>
              <td colspan="7" valign='top' style='border:none;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
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
              <td width='307' colspan='5' style='width:230.55pt;border:solid windowtext 1.0pt;
  border-right:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>Produktdaten</span>
                  </b>
                </p>
              </td>
              <td width='323' colspan='5' style='width:242.35pt;border:solid windowtext 1.0pt;
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
              <td colspan='10' valign='top' style='border:none;border-left:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;vertical-align:top'>
                <table cellpadding='0' cellspacing='0' width='100%'>
                  <tr>
                    <td style="width:50%;white-space:no-wrap;vertical-align:top;border-right:solid windowtext 1.0pt;">
                      <table cellspacing='0' cellpadding='0' width='100%' style='white-space:no-wrap;vertical-align:top'>
                        <tr>
                          <td style='width:20%'>Hersteller:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                          </td>
                        </tr>
                        <tr>
                          <td>Modell:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                          </td>
                        </tr>
                        <tr>
                          <td>Seriennummer:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                          </td>
                        </tr>
                        <tr>
                          <td>Kaufdatum:</td>
                          <td>
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                            <xsl:text>.</xsl:text>
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                            <xsl:text>.</xsl:text>
                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                          </td>
                        </tr>
                      </table>
                    </td>
                    <td style="width:50%;white-space:no-wrap;vertical-align:top">
                      <table  cellspacing='0' cellpadding='0' width='100%' style='white-space:no-wrap;vertical-align:top'>
                        <tr>
                          <td style='width:20%'>Name:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                          </td>
                        </tr>
                        <tr>
                          <td>Adresse:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                          </td>
                        </tr>
                        <tr>
                          <td>PLZ/Ort:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />&#160;&#160;/&#160;&#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                          </td>
                        </tr>
                        <tr>
                          <td>Telefon:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                          </td>
                        </tr>
                        <tr>
                          <td>Email:</td>
                          <td>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td colspan='10' style='width:633px;border:solid windowtext 1.0pt;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span
  lang='EN-GB' style='font-size:11.0pt;font-family:"Trebuchet MS"'>Notizen / Problembeschreibung</span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td colspan='10' style='width:633px;border:solid windowtext 1.0pt;
              padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal'>
                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                </p>
              </td>
            </tr>
            <tr style='page-break-before:always'>
              <td colspan='10' style='width:633px;border:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <span lang='EN-GB'
  style='font-size:10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr>
              <td width='633' colspan='9' style='width:474.6pt;border:none;border-bottom:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal'>
                  <span lang='EN-GB' style='font-size:10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td style='border:none;border-bottom:solid windowtext 1.0pt' width='0'>
                <p class='MsoNormal'>&#160;</p>
              </td>
            </tr>

            <tr>
              <td width='633' colspan='10' valign='top' style='width:633px;border:solid windowtext 1.0pt;
  border-top:none;background:#CCCCCC;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <b>
                    <span lang='DE'
  style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                      Bitte kreuzen Sie Zutreffendes
                      an. Wir werden Sie so schnell wie möglich kontaktieren:
                    </span>
                  </b>
                </p>
              </td>
            </tr>
            <tr>
              <td  valign='top' style='width:50px;border:solid windowtext 2.25pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td width='605' colspan='9' valign='top' style='width:6.3in;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  9.0pt;font-family:"Trebuchet MS"'>
                    Das gleiche Modell ist noch vorrätig und
                    wird für den Kunden in der u.g. Filiale zur Abholung zurückgelegt.
                  </span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td  rowspan='2' valign='top' style='width:50px;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td width='151' colspan='3' style='width:113.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal'>
                  <span lang='DE' style='font-size:9.0pt;font-family:"Trebuchet MS"'>Seriennummer:</span>
                </p>
              </td>
              <td width='454' colspan='6' valign='top' style='width:340.2pt;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td width='151' colspan='3' style='width:113.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal'>
                  <span lang='DE' style='font-size:9.0pt;font-family:"Trebuchet MS"'>
                    Preis
                    für Assurant:
                  </span>
                </p>
              </td>
              <td width='454' colspan='6' valign='top' style='width:340.2pt;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td width='633' colspan='10' valign='top' style='width:474.9pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  9.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td width='28' valign='top' style='width:21.3pt;border:solid windowtext 2.25pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td width='605' colspan='9' valign='top' style='width:6.3in;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  9.0pt;font-family:"Trebuchet MS"'>
                    Das gleiche Modell ist nicht mehr
                    vorhanden. Ein Ersatzgerät gleicher Art und Funktionalität ist vorhanden:
                  </span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td colspan='10' style='border-right:solid windowtext 1.0pt;height:14.2pt;text-align:left;padding:0'>
                <table cellspacing='0' cellpadding='0' width='100%' style='padding:0;margin:0' align='left'>
                  <tr>
                    <td rowspan='5' valign='top' style='width:14%;border:solid windowtext 2.0pt;
  border-top:none;height:14.2pt'>
                      <p class='MsoNormal' style='text-align:justify'>
                        <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                      </p>
                    </td>
                    <td style='width:36%;border:solid windowtext 1.0pt;border-top:none;border-left:none'>&#160;</td>
                    <td style='width:25%;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;text-align:center'>
                      <b>Alternative 1</b>
                    </td>
                    <td style='width:25%;border-bottom:solid windowtext 1.0pt;text-align:center'>
                      <b>Alternative 2</b>
                    </td>
                  </tr>
                  <tr>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>Hersteller:</td>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>&#160;</td>
                    <td style='border-bottom:solid windowtext 1.0pt'>&#160;</td>
                  </tr>
                  <tr>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>Modell:</td>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>&#160;</td>
                    <td style='border-bottom:solid windowtext 1.0pt'>&#160;</td>
                  </tr>
                  <tr>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>Seriennummer:</td>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>&#160;</td>
                    <td style='border-bottom:solid windowtext 1.0pt'>&#160;</td>
                  </tr>
                  <tr>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>
                      Preis
                      für Assurant:
                    </td>
                    <td style='border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;'>&#160;</td>
                    <td style='border-bottom:solid windowtext 1.0pt'>&#160;</td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td width='633' colspan='10' valign='top' style='width:474.9pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  9.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
            </tr>
            <tr>
              <td width='28' valign='top' style='width:21.3pt;border:solid windowtext 2.25pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td width='605' colspan='9' valign='top' style='width:6.3in;border-top:none;
  border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  9.0pt;font-family:"Trebuchet MS"'>
                    Ein Ersatzgerät gleicher Art und Funktionalität
                    ist nicht vorhanden.
                  </span>
                </p>
              </td>
            </tr>
            <tr style='height:14.2pt'>
              <td width='28' valign='top' style='width:21.3pt;border:solid windowtext 1.0pt;
  border-top:none;background:white;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal' align='center' style='text-align:center'>
                  <span lang='DE'
  style='font-size:9.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>
              </td>
              <td width='378' colspan='6' style='width:283.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:white;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal'>
                  <span lang='DE' style='font-size:9.0pt;font-family:"Trebuchet MS"'>
                    Der
                    Wiederbeschaffungswert für das o.g. Gerät beträgt:
                  </span>
                </p>
              </td>
              <td width='227' colspan='3' style='width:170.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:white;padding:0in 5.4pt 0in 5.4pt;height:14.2pt'>
                <p class='MsoNormal'>
                  <span lang='DE' style='font-size:9.0pt;font-family:"Trebuchet MS"'>€</span>
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
                <p class='MsoNormal' style='text-align:justify'>
                  <span lang='DE' style='font-size:
  10.0pt;font-family:"Trebuchet MS"'>&#160;</span>
                </p>

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

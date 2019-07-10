<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE xsl:stylesheet [
  <!ENTITY nbsp "&#160;"> 
]>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd" exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <style type="text/css">
          BODY
          {
          font-family: Verdana, Arial, Tahoma;
          font-size: 6pt;
          }
          TD
          {
          text-align: left;
          vertical-align: top;
          }
          TABLE
          {
          width: 100%;
          }
          .mainTable
          {
          z-index:10000;
          position:relative;
          }
          .mainTable TD
          {
          padding-right: 10px;
          padding-left: 10px;
          }
          .innerTable TD
          {
          padding: 0 0 0 5px;
          font-size: 7pt;
          }
          .data
          {
          font-weight: bold;

          }
          .cutLine
          {
          border-bottom-color: #333333;
          border-bottom-style: dotted;
          border-bottom-width: thin;
          font-size:7pt;
          }
          .chkBox
          {
          margin: 2px 5px 2px 2px;
          height: 15px;
          width: 25px;
          border: 1px solid #000;
          margin-right: 5px;
          display: run-in;
          }
          .headerCell
          {
          font-size: 8pt;
          font-weight: bold;
          text-decoration: underline;
          padding-left: 20px;
          }
          .topBorder
          {
          border-top: 1px #000 solid;
          }
          .verticalText
          {
          width: 2%;
          filter: flipV flipH;
          writing-mode: tb-rl;
          vertical-align: middle;
          text-align: center;
          font-size: 9pt;
          font-weight: bold;
          padding-right: 10px;
          }
          .title
          {
          font-size: 13pt;
          font-weight: bold;
          text-align: center;
          }
          .titleOrder
          {
          font-size: 10pt;
          font-weight: bold;
          text-align: right;
          }
          .boldLarger
          {
          font-weight: bold;
          font-size: 8pt;
          }
          .larger
          {
          font-size: 7pt;
          text-align: justify;
          }

          /************** Font Definitions *********************************/
          @font-face
          {font-family:Tahoma;
          panose-1:2 11 6 4 3 5 4 4 2 4;}
          @font-face
          {font-family:"Trebuchet MS";
          panose-1:2 11 6 3 2 2 2 2 2 4;}
          /* Style Definitions */
          p.MsoNormal, li.MsoNormal, div.MsoNormal
          {margin:0in;
          margin-bottom:.0001pt;
          font-size:12.0pt;
          font-family:"Times New Roman";}
          a:link, span.MsoHyperlink
          {font-family:"Times New Roman";
          color:black;
          text-decoration:none;}
          a:visited, span.MsoHyperlinkFollowed
          {color:#606420;
          text-decoration:underline;}
          p.MsoAcetate, li.MsoAcetate, div.MsoAcetate
          {margin:0in;
          margin-bottom:.0001pt;
          font-size:8.0pt;
          font-family:Tahoma;}
          span.sp
          {font-family:"Times New Roman";}
          span.BalloonTextChar
          {font-family:Tahoma;}
          @page Section1
          {size:8.5in 11.0in;
          margin:.5in .5in 27.0pt .5in;}
          div.Section1
          {page:Section1;}
        </style>

      </head>
      <body lang="EN-US" link="black" vlink="#606420">
        <table align="center" class="mainTable">
          <tr>
            <td>        
              <div class="Section1">
                <p class="MsoNormal">
                  <b>
                    <span style='font-size:7.5pt;font-family:Arial'>
                    <img width='150' height='78' id="Picture 2" src="http://w1.assurant.com/elitalogos/assurant_logo_aba.jpg" alt="http://w1.assurant.com/elitalogos/assurant_logo_aba.jpg"></img >
                  </span>
                  </b>
                </p>
              </div>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td>
                    <p class="MsoNormal" align="center" style='text-align:center'>
                    <b>
                      <span lang='ES-PR' style='font-family:"Trebuchet MS"'>ORDEN DE SERVICIO CELULARES - REEMPLAZO</span>
                    </b>
                    </p>

                    <p class='MsoNormal' align='right' style='text-align:right'>
                    <b>
                      <span lang='ES-PR' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                    </b>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal' align='right' style='text-align:right'>
                      <b>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS"'>N° de Reclamo: </span>
                      </b>
                      <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#A6A6A6'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                      </span>
                    </p>
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                      </b>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                      <b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                        Nombre del Centro de Atención Autorizado: <span style='color:#999999'></span>
                      </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />                   
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                    <b>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                      Código:
                      </span>
                      </b>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table>
                      <tr>
                        <td width ="30%">                      
                          <p class='MsoNormal'>
                          <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                          </span>
                          </p >
                        </td>
                        <td width ="70%">
                          <p class='MsoNormal'>
                            <b>
                              <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>Tel.:</span>
                            </b>
                            <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                            </span>
                          </p>
                        </td>
                     </tr>                  
                      <tr>
                        <td width ="30%">
                          <p class='MsoNormal'>
                            <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />                                                
                            </span>
                          </p>
                         </td>
                        <td width ="70%">
                          <p class='MsoNormal'>
                            <b>
                              <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>Fax:</span>
                            </b>
                            <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                            </span>
                          </p>
                        </td>
                      </tr>
                    </table>
                   </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                    <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                      <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                    </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                  <p class='MsoNormal'>
                    <b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>C.P.:</span>
                    </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                      </span>
                  </p>

                  <p class='MsoNormal'>
                    <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>&nbsp;</span>
                  </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td colspan ='2'>
                    <div style='border:solid windowtext 1.0pt;padding:1.0pt 4.0pt 1.0pt 4.0pt;background:#0C0C0C'>
                        <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C;border:none;padding:0in'>
                        <b>
                          <span lang='ES-PR' style='font-family:"Trebuchet MS";color:white'>AUTORIZACIÓN DE REEMPLAZO</span>
                        </b>
                        </p>                        
                    </div >
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                      </b>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td width ="40%">
                      <p class='MsoNormal'>
                        <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Orden de Servicio: <span style='color:#999999'></span>
                        </span>
                        </b>
                        <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </span>                        
                       </p>
                  </td>
                  <td width ="60%">
                     <p class='MsoNormal'>
                      <b>                  
                      <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                        Fecha de Creación: <span style='color:#999999'></span>
                      </span>
                      </b>
                      <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                        <xsl:text>-</xsl:text>
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                        <xsl:text>-</xsl:text>
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td width ="40%">
                    <p class='MsoNormal' style='margin-right:-9.0pt'>
                      <b>
                        <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Nombre del Distribuidor: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                        <b>      </b>
                      </span>
                    </p>                      
                    </td>
                    <td width ="60%">
                      <p class='MsoNormal'>
                        <b>
                          <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                            Reemplazo en: <span style='color:#999999'></span>
                          </span>
                        </b>
                          <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                          </span>
                      </p>
                      <p class='MsoNormal'>
                        <span lang='ES-MX' style='font-family:"Trebuchet MS"'>                                                </span>
                      </p>
                  </td>
                </tr>
              </table>   
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td colspan ='2'>
                      <div style='border:solid windowtext 1.0pt;padding:1.0pt 4.0pt 1.0pt 4.0pt;background:#0C0C0C'>
                        <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C;border:none;padding:0in'>
                          <b>
                            <span lang='ES-PR' style='font-family:"Trebuchet MS";color:white'>INFORMACIÓN DEL CLIENTE</span>
                          </b>                          
                        </p>
                      </div>
                      <p class='MsoNormal'>
                        <b>
                          <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>&nbsp;</span>
                        </b>
                      </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          N° de Certificado: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Nombre: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td colspan ="2">
                    <p class='MsoNormal'>
                      <b>
                        <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Domicilio:
                          <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />&nbsp;
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />&nbsp; <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />&nbsp; <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td width ="30%">
                    <p class='MsoNormal'>
                      <b>
                        <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Tel.
                          Domicilio: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />                    
                      </span>
                    </p>
                  </td>
                  <td width ="70%">
                    <p class='MsoNormal'>
                      <b>
                        <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Tel. Oficina: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td>
                      <p class='MsoNormal'>
                        <b>
                          <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>RFC:</span>
                        </b>
                        <span style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:TAX_ID" />
                        </span>
                        <b>
                          <span style='font-size:11.0pt;font-family:"Trebuchet MS"'>                                             </span>
                        </b>
                      </p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Email: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                      </span>
                    </p>
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>&nbsp;</span>
                      </b>
                    </p>
                  </td>
                </tr>          
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td colspan ='2'>
                      <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C'>
                        <b>
                          <span lang='ES' style='font-family:"Trebuchet MS";color:white'>INFORMACIÓN DEL PRODUCTO</span>
                        </b>
                      </p>

                      <p class='MsoNormal' align='center' style='text-align:center'>
                        <span lang='ES' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                      </p>
                  </td>
                </tr>
                <tr>
                  <td width ="40%">
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Producto: <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                          <span style='color:#999999'>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                          </span>             
                      </span>
                    </p>
                  </td>
                  <td width ="60%">
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Marca:
                          <span style='color:#999999'></span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>              
                          <span style='color:#999999'>
                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                          </span>             
                      </span>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td width ="40%">
                      <p class='MsoNormal'>
                        <b>
                          <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                            Modelo: <span style='color:#999999'></span>
                          </span>
                        </b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>              
                            <span style='color:#999999'>
                              <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                            </span>             
                        </span>
                      </p>
                    </td>
                    <td width ="60%">
                      <p class='MsoNormal'>
                        <b>
                          <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'></span>
                        </b>
                        <b>
                          <span lang='ES' style='font-size:11.0pt;font-family:"Trebuchet MS"'></span>
                        </b>
                        <b>
                          <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                            IMEI: <span style='color:#999999'></span>
                          </span>
                        </b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                        </span>
                      </p>
                  </td>
                </tr>
                <tr>
                  <td width ="40%">
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>Color:</span>
                      </b>
                      <span lang='ES-MX' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                        <span style='color:#A6A6A6'></span>                                                                  
                      </span>
                    </p>
                  </td>
                  <td width ="60%">
                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                          Fecha de Pérdida:<span style='color:#999999'> </span>
                        </span>
                      </b>
                      <span lang='ES-PR' style='font-size:11.0pt;font-family:"Trebuchet MS";color:#999999'>             
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,9,2)" />
                        <xsl:text>-</xsl:text>
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 6, 2)" />
                        <xsl:text>-</xsl:text>
                        <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                      </span>
                    </p>
                    <p class='MsoNormal'>
                      <span lang='ES-PR' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td>
                      <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C'>
                        <b>
                          <span lang='ES' style='font-family:"Trebuchet MS";color:white'>DESCRIPCIÓN DE LA FALLA</span>
                        </b>
                      </p>
                      <p class='MsoNormal'>
                        <b>
                          <span lang='ES-PR' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                        </b>
                      </p>

                      <p class='MsoNormal'>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#A6A6A6'>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                        </span>
                      </p>

                      <p class='MsoNormal'>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#999999'>&nbsp;</span>
                      </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td>
                    <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C'>
                      <b>
                        <span lang='ES' style='font-family:"Trebuchet MS";color:white'>COMENTARIOS ADICIONALES</span>
                      </b>
                    </p>

                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#999999'>&nbsp;</span>
                      </b>
                    </p>

                    <p class='MsoNormal'>
                      <b>
                        <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#999999'></span>
                      </b>
                      <span lang='ES-PR' style='font-family:"Trebuchet MS";color:#999999'>
                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
                      </span>
                    </p>
                    <p class='MsoNormal'>
                      <span lang='ES' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table>
                <tr>
                  <td>
                    <div style='border:solid windowtext 1.0pt;padding:1.0pt 4.0pt 1.0pt 4.0pt;background:#0C0C0C'>
                      <p class='MsoNormal' align='center' style='text-align:center;background:#0C0C0C;border:none;padding:0in'>
                        <b>
                          <span lang='ES' style='font-family:"Trebuchet MS";color:white'>CONFIRMACIÓN RECIBO UNIDAD DE REEMPLAZO</span>
                        </b>
                      </p>
                    </div>
                      <p class='MsoNormal'>
                        <span class='sp'>
                          <span lang='ES' style='font-size:11.0pt;font-family:"Trebuchet MS"'>
                            Por medio de esta CERTIFICO que Assurant Daños Mexico SA de CV o Assurant Servicios de Mexico SA de CV, me ha hecho entrega de la unidad reparada, acorde a los terminos
                            y condiciones del Programa.
                          </span>
                        </span>
                      </p>
                      <p class='MsoNormal'>
                        <span class='sp'>
                          <span lang='ES' style='font-size:11.0pt;font-family:"Trebuchet MS"'>&nbsp;</span>
                        </span>
                      </p>
                      <p class='MsoNormal'>
                        <span class='sp'>
                          <span lang='ES' style='font-size:11.0pt;font-family:"Trebuchet MS"'>&nbsp;</span>
                        </span>
                      </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table class='MsoNormalTable' border='0' cellpadding='0' width="100%" style='width:100.0%'>
                <tr>
                  <td colspan='2' valign='bottom' style='padding:0in 0in 0in 0in'>
                    <div class='MsoNormal' align='center' style='text-align:center'>
                      <hr size='2' width="100%" align="center"></hr >
                    </div>
                    <p class="MsoNormal"></p>
                  </td>
                </tr>
                <tr>
                  <td width="49%" valign="top" style='width:49.18%;padding:0in 0in 0in 0in'>
                    <p class='MsoNormal' align='center' style='text-align:center'>
                    <span class='sp'>
                    <span style='font-size:10.0pt;font-family:"Trebuchet MS"'>Firma del Cliente</span>
                    </span>
                    </p>
                  </td>
                  <td valign='top' style='padding:0in 0in 0in 0in'>
                    <p class='MsoNormal' align='center' style='text-align:center'>
                    <span class='sp'>
                    <span   style='font-size:10.0pt;font-family:"Trebuchet MS"'>Firma del representante del Centro de Atencion Autorizado</span>
                    </span>
                    </p>
                    <p class='MsoNormal'>
                      <span lang='ES' style='font-family:"Trebuchet MS"'>&nbsp;</span>
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>        
        </table >
      </body>
    </html>

  </xsl:template>
</xsl:stylesheet>
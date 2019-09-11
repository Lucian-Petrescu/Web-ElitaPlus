<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
	exclude-result-prefixes="a">
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
        </style>
      </head>
      <body>
        <table align="center" class="mainTable">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                  <td style="width: 30%">
                  </td>
                  <td class="title">
                    ORDEN DE SERVICIO
                  </td>
                  <td style="width: 30%; white-space: nowrap;" class="titleOrder">
                    Orden No.
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table cellpadding="0" cellspacing="0" border="0" class="innerTable">
                <tr>
                  <td class="verticalText">
                    ORIGINAL CENTRO DE SERVICIO
                  </td>
                  <td style="border: double 1px #000000;">
                    <table cellpadding="0" cellspacing="0">
                      <tr style="border: double 1px #000">
                        <td width="35%" style="border-right: solid 1px #000;">
                          <table cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="headerCell">
                                CENTRO DE SERVICIO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="height:10px"></td>
                              <td class="boldLarger" style="text-align: right;">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                            <tr>
                              <td class="boldLarger">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                              </td>
                              <td></td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                ,
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                , C.P.
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Tel:
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                              </td>
                              <td>
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
                              <td colspan="2" class="headerCell topBorder">
                                DATOS DEL CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CALLE, No. INT / No. EXT.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                COLONIA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CIUDAD
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                ESTADO&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; C.P.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO&#160;&#160;&#160;&#160;&#160; CELULAR
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                &#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                EMAIL
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td rowspan="2"  >
                          <table cellpadding="5" cellspacing="0" border="0" >
                            <tr>
                              <td class="headerCell">
                                DATOS DEL EQUIPO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                &#160;
                              </td>
                              <td class="boldLarger" style="text-align: right">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MARCA
                              </td>
                              <td>
                                NO. DE CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MODELO
                              </td>
                              <td>
                                TIPO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NUMERO IMEI
                              </td>
                              <td>
                                COSTO DEL SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                $&#160;<xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00"),".",".")' />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                PRODUCTO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                FECHA DE COMPRA
                              </td>
                              <td>
                                TIPO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                              </td>
                              <td class="data" style="padding-left:10px;">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="headerCell topBorder">
                                ESTADO FISICO DEL EQUIPO
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                <table cellpadding="2" cellspacing="2">
                                  <tr>
                                    <td style="height:5px;">

                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      ROTO / ESTRELLADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      QUEMADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      INSECTOS PRESENTES
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      RAYADO / RASPADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      OXIDADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      SALINIZACION VISIBLE
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      GOLPEADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      HUMEDO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      INTERVENIDO
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      MANCHADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      POLVOSO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      SIN ACCESORIOS
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="headerCell">
                                PROBLEMA REPORTADO
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="data" style="padding-left:10px;height:30px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td style="width: 1%">
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                    <span class="boldLarger">ESTIMADO CLIENTE:</span>
                    <span class="larger">
                      Le sugerimos
                      revisar detalladamente el funcionamiento y el estado fisico de su producto antes
                      de firma de Conformidad.
                    </span>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                <td>
                </td>
                  <td class="boldLarger" style="text-align: center; word-wrap:normal;">
                    Por medio de esta CERTIFICO que Assurant Daños SA de CV o Assurant Servicios de Mexico SA de CV. me ha hecho entrega de la unidad reparada, acorde a los terminos y condiciones del Programa.
                  </td>
                <td>
                </td>
                </tr>                
                <tr>
                  <td colspan="3" style="height: 40px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td style="width: 10%">
                        </td>
                        <td class="boldLarger" style="width: 37%; text-align: center; border-top: #000 solid 1px;">
                          Cliente Entrega Equipo / Fecha y Firma Cliente
                        </td>
                        <td style="width: 6%">
                        </td>
                        <td class="boldLarger" style="width: 37%; text-align: center; border-top: #000 solid 1px;">
                          Cliente Recibe Equipo / Fecha y Firma Cliente
                          <br />
                          Conformidad
                        </td>
                        <td style="width: 10%">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td class="cutLine">
              <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                  <td style="text-align: left;font-size:7pt;">
                    CORTE AQUÍ
                  </td>
                  <td style="text-align: right;font-size:7pt;">
                    CORTE AQUÍ
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
            </td>
          </tr>
          <!-- Bottom Half -->
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                  <td style="width: 30%">
                  </td>
                  <td class="title">
                    ORDEN DE SERVICIO
                  </td>
                  <td style="width: 30%; white-space: nowrap;" class="titleOrder">
                    Orden No.
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table cellpadding="0" cellspacing="0" border="0" class="innerTable">
                <tr>
                  <td class="verticalText">
                    ORIGINAL CLIENTE
                  </td>
                  <td style="border: double 1px #000000;">
                    <table cellpadding="0" cellspacing="0">
                      <tr style="border: double 1px #000">
                        <td width="35%" style="border-right: solid 1px #000;">
                          <table cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="headerCell">
                                CENTRO DE SERVICIO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td style="height:10px"></td>
                              <td class="boldLarger" style="text-align: right;">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                            <tr>
                              <td class="boldLarger">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                              </td>
                              <td></td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                ,
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                , C.P.
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                              </td>
                              <td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Tel:
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                              </td>
                              <td>
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
                              <td colspan="2" class="headerCell topBorder">
                                DATOS DEL CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CALLE, No. INT / No. EXT.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                COLONIA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CIUDAD
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                ESTADO&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; C.P.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO&#160;&#160;&#160;&#160;&#160; CELULAR
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                &#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                EMAIL
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td rowspan="2">
                          <table cellpadding="5" cellspacing="0" border="0" >
                            <tr>
                              <td class="headerCell">
                                DATOS DEL EQUIPO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                &#160;
                              </td>
                              <td class="boldLarger" style="text-align: right">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MARCA
                              </td>
                              <td>
                                NO. DE CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MODELO
                              </td>
                              <td>
                                TIPO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NUMERO IMEI
                              </td>
                              <td>
                                COSTO DEL SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                $&#160;<xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00"),".",".")' />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                PRODUCTO
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                              <td>
                                &#160;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                FECHA DE COMPRA
                              </td>
                              <td>
                                TIPO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                              </td>
                              <td class="data" style="padding-left:10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="headerCell topBorder">
                                ESTADO FISICO DEL EQUIPO
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                <table cellpadding="2" cellspacing="2">
                                  <tr>
                                    <td style="height:5px;">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      ROTO / ESTRELLADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      QUEMADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      INSECTOS PRESENTES
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      RAYADO / RASPADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      OXIDADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      SALINIZACION VISIBLE
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      GOLPEADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      HUMEDO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      INTERVENIDO
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      MANCHADO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      POLVOSO
                                    </td>
                                    <td class="chkBox">
                                      &#160;
                                    </td>
                                    <td>
                                      SIN ACCESORIOS
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="headerCell">
                                PROBLEMA REPORTADO
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" class="data" style="padding-left:10px;height:30px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td style="width: 1%">
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                    <span class="boldLarger">ESTIMADO CLIENTE:</span>
                    <span class="larger">
                      Le sugerimos
                      revisar detalladamente el funcionamiento y el estado fisico de su producto antes
                      de firma de Conformidad.
                    </span>
                  </td>
                  <td>
                  </td>
                </tr>                
                <tr>
                  <tr>
                <td>
                </td>
                  <td class="boldLarger" style="text-align: center; word-wrap:normal;">
                    Por medio de esta CERTIFICO que Assurant Daños SA de CV o Assurant Servicios de Mexico SA de CV. me ha hecho entrega de la unidad reparada, acorde a los terminos y condiciones del Programa.
                  </td>
                <td>
                </td>
                </tr>
                  <td colspan="3" style="height: 40px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td style="width: 10%">
                        </td>
                        <td class="boldLarger" style="width: 37%; text-align: center; border-top: #000 solid 1px;">
                          Cliente Entrega Equipo / Fecha y Firma Cliente
                        </td>
                        <td style="width: 6%">
                        </td>
                        <td class="boldLarger" style="width: 37%; text-align: center; border-top: #000 solid 1px;">
                          Cliente Recibe Equipo / Fecha y Firma Cliente
                          <br />
                          Conformidad
                        </td>
                        <td style="width: 10%">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <tr>
            <td>
            </td>
          </tr>
        </table>
        <div style="position:absolute;z-index:1000;right:0px;top:120px">
          <xsl:element name="img">
            <xsl:attribute name="src">
              <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_asm.jpg")'/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_asm.jpg</xsl:text>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:attribute>
          </xsl:element>
        </div>
        <div style="position:absolute;z-index:1000;right:0px;top:530px">
          <xsl:element name="img">
            <xsl:attribute name="src">
              <xsl:choose>
                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_asm.jpg")'/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_asm.jpg</xsl:text>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:attribute>
          </xsl:element>
        </div>
      </body>
    </html>

  </xsl:template>
</xsl:stylesheet>
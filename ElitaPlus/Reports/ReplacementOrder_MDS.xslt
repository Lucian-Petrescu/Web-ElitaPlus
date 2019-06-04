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
          margin:0;
          padding:0;
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
          font-size: 7pt;
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
          text-align: center;
          }
          .topBorder
          {
          border-top: 1px #000 solid;
          }
          .verticalText
          {
          width: 2%;
          filter: flipV() flipH();
          writing-mode: tb-rl;
          vertical-align: middle;
          text-align: center;
          font-size: 9pt;
          font-weight: bold;
          padding-right: 10px;
          }
          .verticalTextRight
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
          font-size: 10pt;
          font-weight: bold;
          text-align: center;
          }
          .titleOrder
          {
          font-size: 7pt;
          font-weight: bold;
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
          .smaller
          {
          font-size: smallest;
          vertical-align:sub;
          letter-spacing: .1px;
          }
        </style>
      </head>
      <body>
        <table align="center" class="mainTable">
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                  <td class="titleOrder" style="width: 10%; white-space: nowrap; text-align: left">
                    Autoriacíon No.
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                  <td class="title" style="white-space: nowrap;" >
                    AUTORIZACIÓN DE REEMPLAZO
                  </td>
                  <td class="titleOrder" style="width: 10%; white-space: nowrap; text-align: right">
                    Fecha de Emision:
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
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
                    NO NEGOCIABLE POR EFECTIVO
                  </td>
                  <td style="border: double 1px #000000;">
                    <table cellpadding="0" border="0" cellspacing="0" width="100%">
                      <tr >
                        <td style="width: 50%; border-right: solid 1px #000;">
                          <table>
                            <tr>
                              <td colspan="2" class="headerCell">
                                CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DIRECCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                COLONIA&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; C.P.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CIUDAD&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ESTADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;CORREO ELECTRONICO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                &#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 50%">
                          <table>
                            <tr>
                              <td class="headerCell" colspan="2">
                                DATOS CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px" colspan="2">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DISTRIBUIDOR
                              </td>
                              <td>
                                TARJETA DE REGALO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px;width:250px;">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                              </td>
                              <td class="data" style="padding-left: 10px; white-space:normal">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION,1,50)" />
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                FECHA DE COMPRA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px" colspan="2">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td style="vertical-align:top">
                                IMPORTE DE LA AUTORIZACION
                              </td>
                              <td class="data" style="padding-left: 10px; text-align: left; font-size: 14pt">
                                $&#160;<xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE, "0.00"),".",".")' />
                              </td>
                            </tr>

                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td class="topBorder" style="width: 50%; border-right: solid 1px #000;">
                          <table>
                            <tr>
                              <td class="headerCell">
                                PRODUCTO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MARCA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MODELO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NO. DE SERIE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                PRODUCTO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                FECHA COMPRA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td class="topBorder" style="width: 50%">
                          <xsl:element name="table">
                            <xsl:attribute name="style">
                              <xsl:choose>
                                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                  <xsl:value-of select='concat("background: url(&apos;",a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_asm.jpg&apos;)  bottom right no-repeat;") '/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:value-of select='concat("background: url(&apos;","http://w1.assurant.com/elitalogos/assurant_logo_asm.jpg&apos;)  bottom right no-repeat;") '/>
                                </xsl:otherwise>
                              </xsl:choose>  
                            </xsl:attribute>
                            <tr>
                              <td class="headerCell">
                                CENTRO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CLAVE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DIRECCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                                <xsl:if test="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2">
                                  <br />
                                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                                </xsl:if>
                                <br />
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                ,
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                , C.P.
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                ATENCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CONTACT" />
                              </td>
                            </tr>
                          </xsl:element>
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td class="verticalTextRight">
                    NO NEGOCIABLE POR EFECTIVO
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="height: 23px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td style="width: 5%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Firma y Sello Centro de Servicio
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Fecha de Entrega de Tarjeta de
                          <br />
                          Regalo
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Firma del Cliente de Recibo y
                          <br />
                          Conformidad
                        </td>
                        <td style="width: 5%">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="height: 3px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3"  class="smaller" style="text-align:center">
                    Esta notificación no es valida sin la firma y el sello original  del Centro de Servicio y  tiene una vigencia de  60 días  a partir de la fecha de firma de conformidad.
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td class="cutLine">
              <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                  <td style="text-align: left; font-size: 7pt;">
                    CORTE AQUÍ
                  </td>
                  <td  class="boldLarger" style="text-align: center">
                    ORIGINAL CENTRO DE SERVICIO
                  </td>
                  <td style="text-align: right; font-size: 7pt;">
                    CORTE AQUÍ
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td style="height:15px">
            </td>
          </tr>
          <!-- Bottom Half -->
          <tr>
            <td>
              <table cellpadding="2" cellspacing="0" border="0">
                <tr>
                  <td class="titleOrder" style="width: 10%; white-space: nowrap; text-align: left">
                    Autoriacíon No.
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                  <td class="title" style="white-space: nowrap;" >
                    AUTORIZACIÓN DE REEMPLAZO
                  </td>
                  <td class="titleOrder" style="width: 10%; white-space: nowrap; text-align: right">
                    Fecha de Emision:
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
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
                    NO NEGOCIABLE POR EFECTIVO
                  </td>
                  <td style="border: double 1px #000000;">
                    <table cellpadding="0" border="0" cellspacing="0" width="100%">
                      <tr>
                        <td style="width: 50%; border-right: solid 1px #000;">
                          <table>
                            <tr>
                              <td colspan="2" class="headerCell">
                                CLIENTE
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DIRECCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                COLONIA&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; C.P.
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CIUDAD&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ESTADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;CORREO ELECTRONICO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                &#160;&#160;&#160;&#160;&#160;
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_EMAIL" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 50%">
                          <table>
                            <tr>
                              <td class="headerCell" colspan="2">
                                DATOS CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                CERTIFICADO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px" colspan="2">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DISTRIBUIDOR
                              </td>
                              <td>
                                TARJETA DE REGALO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px;width:250px;">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                              </td>
                              <td class="data" style="padding-left: 10px; white-space:normal">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION,1,50)" />
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                FECHA DE COMPRA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px" colspan="2">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td style="vertical-align:top">
                                IMPORTE DE LA AUTORIZACION
                              </td>
                              <td class="data" style="padding-left: 10px; text-align: left; font-size: 14pt">
                                $&#160;<xsl:value-of select='translate(format-number(a:ServiceOrderReport/a:ServiceOrder/a:SALES_PRICE, "0.00"),".",".")' />
                              </td>
                            </tr>

                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td class="topBorder" style="width: 50%; border-right: solid 1px #000;">
                          <table>
                            <tr>
                              <td class="headerCell">
                                PRODUCTO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MARCA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                MODELO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NO. DE SERIE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                PRODUCTO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                FECHA COMPRA
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                <xsl:text>-</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td class="topBorder" style="width: 50%">
                          <xsl:element name="table">
                            <xsl:attribute name="style">
                              <xsl:choose>
                                <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                  <xsl:value-of select='concat("background: url(&apos;",a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_asm.jpg&apos;)  bottom right no-repeat;") '/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:value-of select='concat("background: url(&apos;","http://w1.assurant.com/elitalogos/assurant_logo_asm.jpg&apos;)  bottom right no-repeat;") '/>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                            <tr>
                              <td class="headerCell">
                                CENTRO DE SERVICIO
                              </td>
                            </tr>
                            <tr>
                              <td style="height:2px">
                              </td>
                            </tr>
                            <tr>
                              <td>
                                CLAVE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                NOMBRE
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                DIRECCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                                <xsl:if test="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2">
                                  <br />
                                  <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS2" />
                                </xsl:if>
                                <br />
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                ,
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                , C.P.
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                TELEFONO
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                ATENCION
                              </td>
                            </tr>
                            <tr>
                              <td class="data" style="padding-left: 10px">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CONTACT" />
                              </td>
                            </tr>
                          </xsl:element >
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td class="verticalTextRight">
                    NO NEGOCIABLE POR EFECTIVO
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="height: 23px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td style="width: 5%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Firma y Sello Centro de Servicio
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Fecha de Entrega de Tarjeta de
                          <br />
                          Regalo
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td class="boldLarger" style="width: 28%; text-align: center; border-top: #000 solid 1px;">
                          Firma del Cliente de Recibo y
                          <br />
                          Conformidad
                        </td>
                        <td style="width: 5%">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="height: 3px;">
                  </td>
                </tr>
                <tr>
                  <td colspan="3"  class="smaller" style="text-align:center">
                    Esta notificación no es valida sin la firma y el sello original  del Centro de Servicio y  tiene una vigencia de  60 días  a partir de la fecha de firma de conformidad.
                  </td>
                </tr>
                <tr>
                  <td colspan="3" class="boldLarger" style="text-align: center">
                    COPIA ASSURANT
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
<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
	exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 10px; FONT-FAMILY: Trebuchet MS,arial;width:auto }
          TD { FONT-SIZE: 10px; FONT-FAMILY: Trebuchet MS,arial;padding:3px;vertical-align:top; white-space:nowrap;}
          #company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY: Trebuchet MS,arial }
          #title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center }
          #number {padding-left:20px; }
          .sp { FONT-STYLE: italic }
          .bold { FONT-WEIGHT: bold; font-size: 13px}
          .tblBorderL{border:none 1px #a2a2a2}
          .tblBorderL TD{padding:0px;}
          .tblBorderD{border:solid 1px #000}
          .tblBorderD TD {padding:3px;}

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
                                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_asm.jpg")'/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_asm.jpg</xsl:text>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                          </xsl:element>
                        </TD>
                        <TD id="company">Assurant Services Canada, Inc.</TD>
                      </TR>
                    </TABLE>
                  </td>
                </tr>
                <tr>
                  <td id="title">
                    Replacement Authorization
                    <br/>
                    <br/>
                    <br/>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td width="50%"></td>
                        <td class="bold" align="right">
                          <P align="right">
                            Authorization Number:
                          </P>
                        </td>
                        <td class="bold" id="number">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <tr>
                        <td style="width:5%; white-space:no-wrap;">
                          Date:
                        </td>
                        <td>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" />
                          <xsl:text>/</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" />
                          <xsl:text>/</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                          Service Centre Code:
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                        </td>
                        <td>
                        </td>
                      </tr>
                      <TR>
                        <TD style="width:5%; white-space:no-wrap;">
                          Recipient:
                        </TD>
                        <TD>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                        </TD>
                        <TD></TD>
                        <TD></TD>
                        <TD></TD>
                        <TD></TD>
                      </TR>
                      <TR>
                        <TD>
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
                          &#160;&#160;
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                        </TD>
                        <TD>
                        </TD>
                        <TD>
                        </TD>
                        <TD>
                          Postal Code:
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                        </TD>
                        <TD>
                        </TD>
                      </TR>
                      <TR>
                        <TD style="width:5%; white-space:no-wrap;">
                          Telephone:
                        </TD>
                        <TD>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                        </TD>
                        <TD>
                          Fax:
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                        </TD>
                        <TD>
                        </TD>
                        <TD>
                          E-Mail:
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                        </TD>
                        <TD>
                        </TD>
                      </TR>
                      <TR>
                        <TD>
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
                        <td width="50%" style="text-align:center;padding-top:3px" class="bold tblBorderD">
                          AUTHORIZATION
                        </td>
                        <td width="50%" style="text-align:center;padding-top:3px" class="bold tblBorderD">
                          CUSTOMER INFORMATION
                        </td>
                      </tr>
                      <tr>
                        <td width="50%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td style="width:50%;white-space:normal">
                                Authorization # (include on your billing document):
                              </td>
                              <td  style="width:50%">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td>Contract #:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Plan Date of Purchase:
                              </td>
                              <td>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,6,2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 9, 2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Coverage Type:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Date of Loss:
                              </td>
                              <td>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Service Location:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:REPAIR_METHOD" />
                              </td>
                            </tr>
                            <tr>
                              <td style="white-space:normal">
                                Amount authorized for replacement or store credit up to:
                              </td>
                              <td>
                                <xsl:text>$</xsl:text>
                                <xsl:choose>
                                  <xsl:when test='format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00") = "NaN"'>
                                    0.00
                                  </xsl:when>
                                  <xsl:otherwise>
                                    <xsl:value-of select='format-number(a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT, "0.00")'></xsl:value-of>
                                  </xsl:otherwise>
                                </xsl:choose>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td width="50%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td style="width:25%">Customer's Name:</td>
                              <td style="width:75%">
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
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS2" />
                              </td>
                            </tr>
                            <tr>
                              <td></td>
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
                              <td>
                                Postal Code:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Telephone:
                              </td>
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
                        <td colspan="2">
                          <br/>
                        </td>
                      </tr>
                      <tr>
                        <td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold tblBorderD">
                          PRODUCT BEING REPLACED
                        </td>
                        <td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold tblBorderD">
                          COMMENTS
                        </td>
                      </tr>
                      <tr>
                        <td width="50%" class="tblBorderD">
                          <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td style="width:25%">
                                Make:
                              </td>
                              <td  style="width:75%">
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Model:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Product Description:
                              </td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                              </td>
                            </tr>
                            <tr>
                              <td>
                                Product Date of Purchase:
                              </td>
                              <td>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,6,2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 9, 2)" />
                                <xsl:text>/</xsl:text>
                                <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                              </td>
                            </tr>
                            <tr>
                              <td>IMEI/Serial Number #:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                              </td>
                            </tr>
                            <tr>
                              <td>Purchased From:</td>
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
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
                              <td>
                                <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
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
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
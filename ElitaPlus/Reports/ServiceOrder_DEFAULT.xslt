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
                                      <xsl:element name="img">
                                        <xsl:attribute name="src">
                                          <xsl:choose>
                                            <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                                              <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_aba.jpg")'/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                              <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_aba.jpg</xsl:text>
                                            </xsl:otherwise>
                                          </xsl:choose>
                                        </xsl:attribute>
                                      </xsl:element>
                                    </td>
                                 </tr>
                                 <tr>
									<td style="height:10px"></td>
                                 </tr>
                                 <tr>
									<td style="text-align:center" class="bold">
										Correct Service Order Formats Have Not Been Defined.
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

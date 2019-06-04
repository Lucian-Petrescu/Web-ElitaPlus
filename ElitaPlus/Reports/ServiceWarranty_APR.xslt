<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
	exclude-result-prefixes="a">
	<xsl:output indent="yes" method="html" encoding="utf-8"/>
	<xsl:template match="/">
		<html>
			<head>
				<style> BODY { FONT-SIZE: 10px; FONT-FAMILY: Trebuchet MS,arial;width:auto }
	TD { FONT-SIZE: 10px; FONT-FAMILY: Trebuchet MS,arial;padding:3px;vertical-align:top; }
	#company { FONT-WEIGHT: bold; FONT-SIZE: 13px; TEXT-ALIGN: right; FONT-FAMILY: Trebuchet MS,arial }
	#title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center }
	#number {padding-left:20px; }
	.sp { FONT-STYLE: italic }
	.bold { FONT-WEIGHT: bold }
	.tblBorderL{border:solid 1px #a2a2a2}
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
                                  <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_apr.jpg")'/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_apr.jpg</xsl:text>
                                </xsl:otherwise>
                              </xsl:choose>
                            </xsl:attribute>
                          </xsl:element>
												</TD>
												<TD id="company">Caribbean American Property Insurance Company</TD>
											</TR>
										</TABLE>
									</td>
								</tr>
								<tr>
									<td id="title">WARRANTY SERVICE 
                                <span class="sp">(ORDEN DE GARANTÍA)</span></td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td width="50%"></td>
												<td class="bold" align="right">
													<P align="right">Authorization Number:
                                            </P>
												</td>
												<td class="bold" id="number">
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
												</td>
											</tr>
											<tr>
												<td></td>
												<td class="bold">
													<P align="right">
														<span class="sp">(# de autorización)</span>
													</P>
												</td>
												<td></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td>Date: <span class="sp">(Fecha)</span></td>
												<td>
													<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,6,2)" />
													<xsl:text>/</xsl:text>
													<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 9, 2)" />
													<xsl:text>/</xsl:text>
													<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
												</td>
												<td></td>
												<td></td>
												<td>Service Center Code:</td>
												<td>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
												</td>
											</tr>
											<TR>
												<TD>Recipient:</TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
												</TD>
												<TD></TD>
												<TD></TD>
												<TD colspan="2">
													<span class="sp">(Código Tienda o Centro de Servicio)</span>
												</TD>
											</TR>
											<TR>
												<TD>
													<span class="sp">(Destinario)</span>
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
													<span class="sp"></span>
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
												</TD>
												<TD></TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
												</TD>
												<TD>Zip Code <span class="sp">(Código Postal)</span></TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
												</TD>
											</TR>
											<TR>
												<TD>Telephone:</TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
												</TD>
												<TD>Fax:</TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
												</TD>
												<TD>E-Mail:</TD>
												<TD>
													<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
												</TD>
											</TR>
											<TR>
												<TD>
													<span class="sp">(Teléfono)</span>
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
												<td width="50%" style="text-align:center;padding-top:3px" class="bold">
                                                    AUTHORIZATION <span class="sp">(Autorización)</span>
                                                
                                                </td>
												<td width="50%" style="text-align:center;padding-top:3px" class="bold">
                                                    CLIENT <span class="sp">(Cliente)</span>
                                                    
                                                </td>
											</tr>
											<tr>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%">
														<tr>
															<td>Authorization # <span class="sp">(# Autorización)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
															</td>
														</tr>
														<tr>
															<td>Account # <span class="sp">(Numero de Cuenta)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CAMPAIGN_NUMBER" />
															</td>
														</tr>
														<tr>
															<td>Certificate #:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
															</td>
														</tr>
														<tr>
															<td>Product Date <span class="sp">(Fecha de compra)</span>:</td>
															<td>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,6,2)" />
																<xsl:text>/</xsl:text>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 9, 2)" />
																<xsl:text>/</xsl:text>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
															</td>
														</tr>
														<tr>
															<td>Invoice # <span class="sp">(# Factura)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
															</td>
														</tr>
														<tr>
															<td>Coverage Type <span class="sp">(Tipo de Cobertura)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:COVERAGE_TYPE" />
															</td>
														</tr>
														<tr>
															<td>Date of Loss <span class="sp">(Fecha de Perdida)</span>:</td>
															<td>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE,6,2)" />
																<xsl:text>/</xsl:text>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 9, 2)" />
																<xsl:text>/</xsl:text>
																<xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:LOSS_DATE, 1, 4)" />
															</td>
														</tr>
														<tr>
															<td>Deductible <span class="sp">(Deducible)</span>$:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT" />
															</td>
														</tr>
													</table>
												</td>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%">
														<tr>
															<td>Customer's Name:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
															</td>
														</tr>
														<tr>
															<td>
																<span class="sp">(Nombre del Cliente)</span>
															</td>
															<td>
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
																<span class="sp">(Dirección)</span>
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
															<td>Zip Code <span class="sp">(Codigo Postal)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
															</td>
														</tr>
														<tr>
															<td>Telephone <span class="sp">(Teléfono)</span>:</td>
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
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold">
                                                    PRODUCT INFORMATION<BR /> <span class="sp">(Informacion del Producto)</span>
                                                
                                                </td>
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="bold">
                                                    PROBLEM DESCRIPTION<BR /> <span class="sp">(Descripción del Problema)</span>
                                                    
                                                </td>
											</tr>
											<tr>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%">
														<tr>
															<td>Make <span class="sp">(Marca)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
															</td>
														</tr>
														<tr>
															<td>Model <span class="sp">(Modela)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
															</td>
														</tr>
														<tr>
															<td>Product <span class="sp">(Producto)</span>:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
															</td>
														</tr>
														<tr>
															<td>IMEI/Serial Number #:</td>
															<td>
																<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
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
								<tr>
									<td class="bold">
										<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
											<tr>
												<td class="bold" style="text-align:center;border-bottom:2px solid #a2a2a2;">
                                                    IMPORTANT <span class="sp"> (IMPORTANTE)</span>
                                                </td>
											</tr>
											<tr>
												<td style="border-bottom:1px solid #a2a2a2;">
													<table cellpadding="0" cellspacing="0" border="0" width="100%">
														<tr>
															<td colspan="2">
																<span class="sp" style="padding-right:10px;">(Complete el siguiente formulario y envíelo junto con la factura)</span>
																<span class="bold"></span>
															</td>
														</tr>
														<tr>
															<td>
                                                                1.  Confirmar la recepción de esta orden.<br />
                                                                2.  Coordinar visita/recogido con el cliente dentro de las próximas cuatro (4) horas.<br />
                                                                3.  Completar los detalles de la reparación.
                                                                </td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td style="height:35px;vertical-align:bottom">
													<table cellpadding="0" cellspacing="0" width="100%" border="0">
														<tr>
															<td width="60%">
																<span class="bold">Customer Signature</span>
																<span class="sp"> (Firma Cliente) </span>
															</td>
															<td>
																<span class="bold">Date </span>
																<span class="sp">(Fecha)</span>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td class="bold" style="padding-top:10px;padding-bottom:10px;text-align:left;">
                                        Service Center Comments<BR />
                                        <SPAN CLASS="SP">(Comentarios del Centro de Servicio)</SPAN>
                                    </td>
								</tr>
								<tr>
									<td class="tblBorderD" style="height:150px;">
                                        &#160;
                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
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

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
	#title { TEXT-ALIGN: right; }
	.title { FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center }
	.sp { FONT-STYLE: italic }
	.bold { FONT-WEIGHT: bold; FONT-SIZE: 13px;}
	.tblBorderL{}
	.tblBorderL2{border:2px solid #000;}
	.tblBorderL TD{padding:0px;}
	.tblBorderD{border-top:solid 1px #000;border-left:solid 1px #000;border-bottom:ridge 4px #000;border-right:ridge 4px #000;}
	.tblBorderD TD {padding:20px;}
	.tblOutline{padding:10px;width:97%}
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
										<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD>
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
												</TD>
												<TD id="company" colspan="2">Assurant Argentina Compañia de Seguros S.A.</TD>
											</TR>
											<tr>
												<td></td>
												<td id="company">N° de Autorización:</td>
												<td id="company" class="TDBox" style="text-align:center;">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                </td>
											</tr>
										</TABLE>
									</td>
								</tr>
								<tr>
									<td class="title" id="title">
                                        REPARACION EN GARANTIA DE SERVICIO
                                    </td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td class="label">Fecha:</td>
												<td class="TDBox">&#160;
                                                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                                                    <xsl:text>-</xsl:text>
                                                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                                                    <xsl:text>-</xsl:text>
                                                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                                                </td>
												<td></td>
												<td></td>
												<td></td>
												<td></td>
											</tr>
											<TR>
												<TD class="label">Destinario:</TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                                                </TD>
												<TD></TD>
												<TD></TD>
												<td class="label">Código Centro Serv.</td>
												<td class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                                                </td>
											</TR>
											<TR>
												<TD></TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />
                                                </TD>
												<TD></TD>
												<TD></TD>
												<TD></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD></TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />
                                                </TD>
												<TD></TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                                                </TD>
												<TD class="label">CP:</TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                                                </TD>
											</TR>
											<TR>
												<TD class="label">TE:</TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                                                </TD>
												<TD class="label">Fax:</TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                                                </TD>
												<TD class="label">EMail:</TD>
												<TD class="TDBox">&#160;
                                                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                                                </TD>
											</TR>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL">
											<tr>
												<td width="50%" style="text-align:center;padding-top:3px" class="title">
                                                    Autorización
                                                </td>
												<td>&#160;&#160;&#160;</td>
												<td width="50%" style="text-align:center;padding-top:3px" class="title">
                                                    Cliente
                                                </td>
											</tr>
											<tr>
												<td width="47%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
																	<tr>
																		<td class="label">N° de Autorización:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Dealer:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEALER_NAME" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">N° de Certificado:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Fecha de compra:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE,9,2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 6, 2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:WARRANTY_SALES_DATE, 1, 4)" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">N° de factura:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Deducible $ :</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:DEDUCTIBLE_AMOUNT" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Autorización $ :</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT" />
                                                                        </td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
												<td></td>
												<td width="47%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
																	<tr>
																		<td class="label">Nombre:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td></td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td></td>
																		<td>
																			<table cellpadding="0" cellspacing="0" border="0" width="100%">
																				<tr>
																					<td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                                                                                    </td>
																					<td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                                                                                    </td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr>
																		<td></td>
																		<td></td>
																	</tr>
																	<tr>
																		<td class="label">TE:</td>
																		<td>
																			<table cellpadding="0" cellspacing="0" border="0" width="100%">
																				<tr>
																					<td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                                                                                    </td>
																					<td class="label">CP:</td>
																					<td class="TDBox">&#160;
                                                                                        <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                                                                                    </td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr>
																		<td class="label">DNI:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:IDENTIFICATION_NUMBER" />
                                                                        </td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="title">
                                                    Producto
                                                
                                                </td>
												<td style="width:10px;"></td>
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="title">
                                                    Importante
                                                    
                                                </td>
											</tr>
											<tr>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
																	<tr>
																		<td class="label">Marca:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Modelo:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Producto:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">No. de serie:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">Fecha de Compra:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                                                                            <xsl:text>-</xsl:text>
                                                                            <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                                                                        </td>
																	</tr>
																	<tr>
																		<td class="label">N° de factura:</td>
																		<td class="TDBox">&#160;
                                                                            <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                                                                        </td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
												<td></td>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
																	<tr>
																		<td class="bold">
                                                                            1).  Confirmar recepción del mensaje<br />
                                                                            2).  Combinar visita/retiro con cliente<br />
                                                                            3).  Completar datos faltantes
                                                                            
                                                                        </td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="title">
                                                    Descripción de falla
                                                </td>
												<td style="width:10px;"></td>
												<td width="50%" style="text-align:center;padding:5px;vertical-align:middle;" class="title">
                                                    Instrucciones Especiales
                                                </td>
											</tr>
											<tr>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
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
												<td style="width:10px;"></td>
												<td width="50%" class="tblBorderD">
													<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblOutline">
														<tr>
															<td>
																<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblInner">
																	<tr>
																		<td>
																			<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
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
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="100%" class="tblBorderL2">
											<tr>
												<td style="text-align:center;padding-top:3px;border-bottom:1px #000 solid;" class="title">
                                                    AVISO DE REPARACION
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;" class="bold">
                                                    Complete el siguiente formulario y envielo junto con la factura
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                                                    Número de serie del producto reparado:
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                                                    Fecha CUMPLIDO
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                                                    Descripción de la reparación o Código reparación
                                                  
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                                                    Utilizó repuestos&#160;&#160;&#160;&#160;SI&#160;&#160;&#160;&#160;NO
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;height:50px;" class="bold">
                                                    Firma cliente
                                               </td>
											</tr>
											<tr>
												<td style="border-bottom:1px #000 solid;text-align:center;" class="bold">
                                                    COMENTARIOS del centro de servicio
                                               </td>
											</tr>
											<tr>
												<td style="height:50px" class="bold"></td>
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

<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:key name="NYTiers" match="Group[@nyProd='Y']/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="NoneNYTiers" match="Group[@nyProd='N']/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="NYRCTiers" match="Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']" use="@value"/>
  <xsl:key name="NoneNYRCTiers" match="Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']" use="@value"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          td {FONT-FAMILY: arial;font-size:12px;}
          .pageTitle { FONT-WEIGHT: bold; FONT-SIZE: 20px; TEXT-ALIGN: left;}
          .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center; }
          .tblInvoicePage{border:2px solid #000;}
          .tblInvoicePage td{padding:3px;white-space:nowrap;text-align:center;}
          .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:14px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
          .tdBorder {border-bottom:1px solid black;padding-left:0px;}
          .tdBorderTop {border-top:1px solid black;}
          .tdBorderLeft {border-left:1px solid black;}
          .trTotal {background-color:#F0F8FF;)
        </style>
      </head>
      <body>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">Summary Invoice</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="pageTitle" style="text-align:left;">The Signal</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="pageTitle" colspan="2" style="text-align:left;">Invoice</td>
          </tr>
          <tr>
            <td rowspan="2">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td rowspan="2" style="text-align:left;">
              676 E. Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone: 610-341-1300<br/>
              Fax: 610-341-8939<br/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title"  style="text-align:left;">Date</td>
            <td class="title" style="text-align:right;">Invoice Number</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td  style="text-align:left;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
            <td style="text-align:right;">
              402-<xsl:value-of select="//InvoiceYear" />-<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td align="left" class="title" style="text-align:left;">Ship/Bill To:</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td style="text-align:left;">
              T-Mobile USA, Inc.<br/>
              PO BOX 3245<br/>
              Portland, OR  92708-3245<br/>
              Attn: <xsl:value-of select="//ATTNName" /><br/>
              <xsl:value-of select="//ATTNEmail" />
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="vertical-align:text-top;">P.O. Number:</td>
            <td style="vertical-align:text-top;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle">Item</td>
            <td class="tdTitle">Description</td>
            <td class="tdTitle">Quantity</td>
            <td class="tdTitle">Price</td>
            <td class="tdTitle">Total</td>
          </tr>
          <xsl:variable name="vACTotal" select="sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCPrem']) - sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCFee']) + sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC'])" />
          <xsl:variable name="vNYTotal" select="sum(//Group[@nyProd='Y']/Group[not(@value='ESC' or @value='INS')]/Group[@level='3']/InvField[@name='ESCPrem']) + sum(//Group[@nyProd='Y']/Group[not(@value='ESC' or @value='INS')]/Group[@level='3']/InvField[@name='INSPrem']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']) + sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']) + sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSCommRC'])"/>
          <xsl:variable name="vCessionLoss" select="//CessionLoss" />
          <xsl:variable name="vMDFRecon" select="//MDFRecon" />
          <xsl:variable name="vGrandTotal" select="$vACTotal + $vNYTotal + $vCessionLoss + $vMDFRecon" />
          <tr>
            <td >1</td>
            <td>
              <xsl:value-of select="//Period" /> Assure Connection
            </td>
            <td >1</td>
            <td >
              $<xsl:value-of select="format-number($vACTotal,'###,##0.00')"/>
            </td>
            <td>
              $<xsl:value-of select="format-number($vACTotal,'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td>2</td>
            <td>
              <xsl:value-of select="//Period" /> WEIP (NY)
            </td>
            <td>1</td>
            <td>
              $<xsl:value-of select="format-number($vNYTotal,'###,##0.00')"/>
            </td>
            <td >
              $<xsl:value-of select="format-number($vNYTotal,'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td>3</td>
            <td>
              <xsl:value-of select="//Period" /> Reinsurance Cession Loss
            </td>
            <td>1</td>
            <xsl:choose>
              <xsl:when test="$vCessionLoss >= 0">
                <td>
                  $<xsl:value-of select="format-number($vCessionLoss,'###,##0.00')"/>
                </td>
                <td >
                  $<xsl:value-of select="format-number($vCessionLoss,'###,##0.00')"/>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td style="color:red">
                  ($ <xsl:value-of select="format-number($vCessionLoss * -1,'###,##0.00')" />)
                </td>
                <td style="color:red">
                  ($ <xsl:value-of select="format-number($vCessionLoss * -1,'###,##0.00')" />)
                </td>
              </xsl:otherwise>
            </xsl:choose>                       
          </tr>
          <tr>
            <td>4</td>
            <td>
              MDF Reconciliation
            </td>
            <td>1</td>
            <xsl:choose>
              <xsl:when test="$vMDFRecon >= 0">
                <td>
                  $<xsl:value-of select="format-number($vMDFRecon,'###,##0.00')"/>
                </td>
                <td >
                  $<xsl:value-of select="format-number($vMDFRecon,'###,##0.00')"/>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td style="color:red">
                  ($ <xsl:value-of select="format-number($vMDFRecon * -1,'###,##0.00')" />)
                </td>
                <td style="color:red">
                  ($ <xsl:value-of select="format-number($vMDFRecon * -1,'###,##0.00')" />)
                </td>
              </xsl:otherwise>
            </xsl:choose>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Total</td>
            <td>
              $<xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Payments/Credits</td>
            <td></td>
          </tr>
          <tr>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Balance Due</td>
            <td>
              $<xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
            </td>
          </tr>
          <td colspan="5">
            <hr/>
          </td>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title">TIN: 22-2623205</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Due Date</td>
            <td >DUE UPON RECEIPT</td>
          </tr>
          <td colspan="5">
            <hr/>
          </td>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">
              Remit Address:<br/><br/>
              * Wire Transfer Information:<br/>
              Bank:   <span style="font-weight:normal;">JP Morgan Chase Bank</span><br/>
              Account Name:   <span style="font-weight:normal;">The Signal LP</span><br/>
              ABA# : <span style="font-weight:normal;">021000021</span><br/>
              Account# :  <span style="font-weight:normal;">771060613</span><br/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
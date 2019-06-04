<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:key name="NYTiers" match="Group[@nyProd='Y']/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="NoneNYTiers" match="Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="PRTiers" match="Group[@PRProd='Y']/Group/Group[@level='3']" use="@value"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          .pageTitle { FONT-WEIGHT: bold; FONT-SIZE: 24px; TEXT-ALIGN: left;}
          .title { FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; }
          .tblInvoicePage{border:2px solid #000;}
          .tblInvoicePage td{padding:0px;white-space:nowrap;text-align:center;padding-right:5px;}
          .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:16px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
          .tdBorder {border-bottom:1px solid black;padding-left:0px;}
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
            <td class="pageTitle" colspan="5" style="text-align:left;">The Signal Invoice</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td rowspan="3" style="white-space:pre-wrap;text-align:left;">
              676 E. Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone: 610-341-1300<br/>
              Fax: 610-341-8939<br/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title"  style="text-align:right;">Date</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Invoice Number</td>
            <td style="text-align:left;padding-left:5px;">
              405-<xsl:value-of select="//InvoiceYear" />-<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Payment Due Date</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//DueDate" />
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
            <td colspan="4" align="left" class="title" style="text-align:left;">Bill To:</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td style="white-space:pre; text-align:left;">
              T-Mobile USA, Inc.<br/>
              PO BOX 3245<br/>
              Portland, OR  92708-3245
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="vertical-align:text-top; text-align:right;">P.O. Number:</td>
            <td style="vertical-align:text-top; text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="title">Item</td>
            <td class="title">Description</td>
            <td class="title">Quantity</td>
            <td class="title">Price</td>
            <td class="title">Total</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <xsl:variable name="vACTotal" select="sum(/InvoiceData/Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group/InvField[@name='ESCPrem']) + sum(/InvoiceData/Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group/InvField[@name='JumpPrem'])"/>
          <xsl:variable name="vNYTotal" select="sum(/InvoiceData/Group[@nyProd='Y']/Group/Group/InvField[@name='ESCPrem']) + sum(/InvoiceData/Group[@nyProd='Y']/Group/Group/InvField[@name='JumpPrem']) + sum(/InvoiceData/Group[@nyProd='Y']/Group/Group/InvField[@name='INSPrem'])"/>
          <xsl:variable name="vPRTotal" select="sum(/InvoiceData/Group[@nyProd='N' and @PRProd='Y']/Group/Group/InvField[@name='JumpPrem'])"/>
          <xsl:variable name="vGrandTotal" select="$vACTotal + $vNYTotal + $vPRTotal" />
          <tr>
            <td >1</td>
            <td>
              <xsl:value-of select="//Period" /> Assure Connection
            </td>
            <td >1</td>
            <td >
              $ <xsl:value-of select="format-number($vACTotal,'###,##0.00')"/>
            </td>
            <td>
              $ <xsl:value-of select="format-number($vACTotal,'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td>2</td>
            <td>
              <xsl:value-of select="//Period" /> WEIP (NY)
            </td>
            <td>1</td>
            <td>
              $ <xsl:value-of select="format-number($vNYTotal,'###,##0.00')"/>
            </td>
            <td >
              $ <xsl:value-of select="format-number($vNYTotal,'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td>3</td>
            <td>
              <xsl:value-of select="//Period" /> JUMP! - Puerto Rico - $2.00 Only
            </td>
            <td>1</td>
            <td>
              $ <xsl:value-of select="format-number($vPRTotal,'###,##0.00')"/>
            </td>
            <td >
              $ <xsl:value-of select="format-number($vPRTotal,'###,##0.00')"/>
            </td>
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
              $ <xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
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
            <td class="title" style="text-align:right;">Balance Due The Signal</td>
            <td>
              $ <xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
            </td>
          </tr>
          <td colspan="5">
            <hr/>
          </td>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">TIN: 22-2623205</td>
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
        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="7" style="text-align:left;">AC - ESC Invoice</td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">The Signal</td>
            <td class="title" style="text-align:right;">Invoice #:</td>
            <td style="text-align:left;padding-left:5px;">
              405 - PHPJUMPAC <xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td rowspan="5" colspan="5" style="text-align:left;padding-left:5px;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td class="title" style="text-align:right;">Invoice Date:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Prepared By:</td>
            <td style="text-align:left;padding-left:5px;">ePrism</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Due Date:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//DueDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="7">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="4" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="4" style="text-align:left;">PO BOX 3245</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="4" style="text-align:left;">Portland, OR  92708-3245</td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Data</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">ESC Retail Rate</td>
            <td class="tdTitle" style="text-align:left;">Jump Retail Rate</td>
            <td class="tdTitle" style="text-align:left;">Sum Due The Signal</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N' and (@PRProd='N' or not(@PRProd))]">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='ESCPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='JumpPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] + ./InvField[@name='JumpPrem'],'###,##0.00')" />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and generate-id() = generate-id(key('NoneNYTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCSum" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCPrem'])"/>
            <xsl:variable name="vTierJumpSum" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='JumpPrem'])"/>
            <xsl:variable name="vTierTotal" select="$vTierESCSum + $vTierJumpSum"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT:bold; padding:0px;">
                Total Sum Of <xsl:value-of select="$vnoneNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierESCSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierJumpSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierTotal,'###,##0.00')"/>
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='ESCPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='JumpPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='ESCPrem']) +sum(//Group[@nyProd='N' and @PRProd='N']/Group/Group[@level='3']/InvField[@name='JumpPrem']),'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;" colspan="7">
              * Wire Transfer Information:<br/>
              Bank:   <span style="font-weight:normal;">JP Morgan Chase Bank</span><br/>
              Account Name:   <span style="font-weight:normal;">The Signal LP</span><br/>
              ABA# : <span style="font-weight:normal;">021000021</span><br/>
              Account# :  <span style="font-weight:normal;">771060613</span><br/>
            </td>
          </tr>
        </table>

        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="8" style="text-align:left;">NY - Invoice - WEIP (NY)</td>
          </tr>
          <tr>
            <td colspan="8">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="6" style="text-align:left;">The Signal</td>
            <td class="title" style="text-align:right;">Invoice #:</td>
            <td style="text-align:right;">
              405 - PHPJUMPNY <xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td rowspan="5" colspan="6" style="text-align:left;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td class="title" style="text-align:right;">Invoice Date:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Prepared By:</td>
            <td style="text-align:left;">ePrism</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Due Date:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//DueDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="8">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="5" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="7" style="text-align:left;">PO BOX 3245</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="7" style="text-align:left;">Portland, OR  92708-3245</td>
          </tr>
          <tr>
            <td colspan="8">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Data</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">ESC Retail Rate</td>
            <td class="tdTitle" style="text-align:left;">Jump Retail Rate</td>
            <td class="tdTitle" style="text-align:left;">INS Premium</td>
            <td class="tdTitle" style="text-align:left;">Sum Due The Signal</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='Y']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='ESCPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='JumpPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='INSPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] + ./InvField[@name='JumpPrem'] + ./InvField[@name='INSPrem'],'###,##0.00')" />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='Y']/Group/Group[@level='3' and generate-id() = generate-id(key('NYTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCPrem'])"/>
            <xsl:variable name="vTierJumpSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='JumpPrem'])"/>
            <xsl:variable name="vTierINSSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSPrem'])"/>
            <xsl:variable name="vTierTotal" select="$vTierESCSum + $vTierJumpSum + $vTierINSSum"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                Total Sum Of <xsl:value-of select="$vNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierESCSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierJumpSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierINSSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierTotal,'###,##0.00')"/>
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='JumpPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCPrem']) +sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='JumpPrem']) +sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td colspan="8">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;" colspan="8">
              * Wire Transfer Information:<br/>
              Bank:   <span style="font-weight:normal;">JP Morgan Chase Bank</span><br/>
              Account Name:   <span style="font-weight:normal;">The Signal LP</span><br/>
              ABA# : <span style="font-weight:normal;">021000021</span><br/>
              Account# :  <span style="font-weight:normal;">771060613</span><br/>
            </td>
          </tr>
        </table>

        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="6" style="text-align:left;">Puerto Rico - JUMP</td>
          </tr>
          <tr>
            <td colspan="6">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="4" style="text-align:left;">The Signal</td>
            <td class="title" style="text-align:right;">Invoice #:</td>
            <td style="text-align:left;padding-left:5px;">
              405 - PHPJUMPPR <xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td rowspan="5" colspan="4" style="text-align:left;padding-left:5px;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td class="title" style="text-align:right;">Invoice Date:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Prepared By:</td>
            <td style="text-align:left;padding-left:5px;">ePrism</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Due Date:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//DueDate" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="6">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="3" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;padding-left:5px;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="5" style="text-align:left;">PO BOX 3245</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="5" style="text-align:left;">Portland, OR  92708-3245</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="5" style="text-align:left;">JUMP - Puerto Rico</td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Values</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">JUMP Retail Rate</td>
            <td class="tdTitle" style="text-align:left;">Sum Due Assurant</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N' and @PRProd='Y']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>                
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='JumpPrem'],'###,##0.00')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='JumpPrem'],'###,##0.00')" />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3' and generate-id() = generate-id(key('PRTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierJumpSum" select="sum(//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='JumpPrem'])"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT:bold; padding:0px;">
                Total Sum Of <xsl:value-of select="$vnoneNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierJumpSum,'###,##0.00')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierJumpSum,'###,##0.00')"/>
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3']/InvField[@name='JumpPrem']),'###,##0.00')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and @PRProd='Y']/Group/Group[@level='3']/InvField[@name='JumpPrem']),'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;" colspan="7">
              * Wire Transfer Information:<br/>
              Bank:   <span style="font-weight:normal;">JP Morgan Chase Bank</span><br/>
              Account Name:   <span style="font-weight:normal;">The Signal LP</span><br/>
              ABA# : <span style="font-weight:normal;">021000021</span><br/>
              Account# :  <span style="font-weight:normal;">771060613</span><br/>
            </td>
          </tr>
        </table>

        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">Summary Remittance</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">The Signal Invoice</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td rowspan="3" style="white-space:pre-wrap;text-align:left;">
              676 E. Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone: 610-341-1300<br/>
              Fax: 610-341-8939<br/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title"  style="text-align:right;">Date</td>
            <td>
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Invoice Number</td>
            <td>
              405-<xsl:value-of select="//InvoiceYear" />-<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
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
            <td colspan="4" align="left" class="title" style="text-align:left;">Bill To:</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td style="white-space:pre; text-align:left;">
              T-Mobile USA, Inc.<br/>
              PO BOX 3245<br/>
              Portland, OR  92708-3245
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="vertical-align:text-top;text-align:right;">P.O. Number:</td>
            <td style="vertical-align:text-top;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="title">Item</td>
            <td class="title">Description</td>
            <td class="title">Quantity</td>
            <td class="title">Price</td>
            <td class="title">Total</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <xsl:variable name="vESCBilling" select="sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCFee']) + sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='INSPrem'])"/>
          <xsl:variable name="vNYBilling" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']) + sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee'])"/>
          <xsl:variable name="vInsPrem" select="sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='INSPrem'])"/>
          <xsl:variable name="vGrandTotal" select="$vESCBilling + $vNYBilling - $vInsPrem" />
          <tr>
            <td >1</td>
            <td>
              <xsl:value-of select="//Period" /> - Non-Contributory Insurance Cost
            </td>
            <td >1</td>
            <td style="color:red">
              ($ <xsl:value-of select="format-number($vInsPrem,'###,##0.00')"/>)
            </td>
            <td style="color:red">
              ($ <xsl:value-of select="format-number($vInsPrem,'###,##0.00')"/>)
            </td>
          </tr>
          <tr>
            <td>2</td>
            <td>
              <xsl:value-of select="//Period" /> - ESC Billing Fee
            </td>
            <td>1</td>
            <td>
              $ <xsl:value-of select="format-number($vESCBilling,'###,##0.00')"/>
            </td>
            <td >
              $ <xsl:value-of select="format-number($vESCBilling,'###,##0.00')"/>
            </td>
          </tr>
          <tr>
            <td>3</td>
            <td>
              <xsl:value-of select="//Period" /> - NY Billing Fee
            </td>
            <td>1</td>
            <td>
              $ <xsl:value-of select="format-number($vNYBilling,'###,##0.00')"/>
            </td>
            <td >
              $ <xsl:value-of select="format-number($vNYBilling,'###,##0.00')"/>
            </td>
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
            <xsl:choose>
              <xsl:when test="$vGrandTotal >= 0">
                <td>
                  $ <xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td style="color:red">
                  ($ <xsl:value-of select="format-number($vGrandTotal * -1,'###,##0.00')" />)
                </td>
              </xsl:otherwise>
            </xsl:choose>
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
            <td class="title" style="text-align:right;">Balance Due T-Mobile (If number is positive)</td>
            <xsl:choose>
              <xsl:when test="$vGrandTotal >= 0">
                <td>
                  $ <xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td style="color:red">
                  $0.00
                </td>
              </xsl:otherwise>
            </xsl:choose>
          </tr>
          <td colspan="5">
            <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            <hr/>
          </td>
          <tr>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Balance Due The Signal (If number is negative)</td>
            <xsl:choose>
              <xsl:when test="$vGrandTotal >= 0">
                <td>
                  $0.00
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td style="color:red">
                  $ <xsl:value-of select="format-number($vGrandTotal,'###,##0.00')" />
                </td>
              </xsl:otherwise>
            </xsl:choose>
          </tr>
          <td colspan="5">
            <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            <hr/>
          </td>
          <tr>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:right;">Due Date</td>
            <td >One Day after Receipt of Invoice Payment</td>
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
              Bank:
              Account Name:
              ABA# :
              Account# :
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
        </table>
        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">AC - ESC Billing Fee</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">The Signal</td>
          </tr>
          <tr>
            <td rowspan="5" colspan="3" style="text-align:left;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2" class="title" style="text-align:center;">AC - ESC Billing Fee</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="2" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="4" style="text-align:left;">12920 SE 38th Street</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="4" style="text-align:left;">Bellevue, WA 98006</td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Attn:</td>
            <td colspan="4" style="text-align:left;" class="title">Dale Quick</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="4" style="text-align:left;" class="title">dale.quick1@t-mobile.com</td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Data</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">ESC Billing Fee due to T-Mobile</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N' and (@PRProd='N' or not(@PRProd))]">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;color:red">
                    ($<xsl:value-of select="format-number(./InvField[@name='ESCFee'] + ./InvField[@name='INSPrem'],'###,##0.00')" />)
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and generate-id() = generate-id(key('NoneNYTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCFee" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCFee']) + sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='INSPrem'])"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                Total Sum Of <xsl:value-of select="$vnoneNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;color:red">
                ($ <xsl:value-of select="format-number($vTierESCFee,'###,##0.00')"/>)
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;color:red">
              ($ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='ESCFee']) + sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/>)
            </td>
          </tr>
        </table>

        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="7" style="text-align:left;">NY - Billing Fee</td>
          </tr>
          <tr>
            <td colspan="7">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="7" style="text-align:left;">The Signal</td>
          </tr>
          <tr>
            <td rowspan="5" colspan="5" style="text-align:left;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2" class="title" style="text-align:center;">NY - Billing Fee</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="7">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="4" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="6" style="text-align:left;">12920 SE 38th Street</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="6" style="text-align:left;">Bellevue, WA 98006</td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Attn:</td>
            <td colspan="6" style="text-align:left;" class="title">Dale Quick</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="6" style="text-align:left;" class="title">dale.quick1@t-mobile.com</td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Data</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">ESC Billing Fee</td>
            <td class="tdTitle" style="text-align:left;">INS Billing Fee</td>
            <td class="tdTitle" style="text-align:left;">Amount Due T-Mobile</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='Y']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>
                <xsl:variable name="vNYESCBillFee" select="./InvField[@name='ESCFee']"/>
                <xsl:variable name="vNYInsBillFee" select="./InvField[@name='INSFee']"/>
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <xsl:choose>
                    <xsl:when test="$vNYESCBillFee = 0">
                      <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                    </xsl:when>
                    <xsl:otherwise>
                      <td class="tdBorder" style="text-align:left;color:red;">
                        ($<xsl:value-of select="format-number($vNYESCBillFee,'###,##0.00')" />)
                      </td>
                    </xsl:otherwise>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="$vNYInsBillFee = 0">
                      <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                    </xsl:when>
                    <xsl:otherwise>
                      <td class="tdBorder" style="text-align:left;color:red;">
                        ($<xsl:value-of select="format-number($vNYInsBillFee,'###,##0.00')" />)
                      </td>
                    </xsl:otherwise>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="$vNYInsBillFee = 0 and $vNYESCBillFee = 0">
                      <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                    </xsl:when>
                    <xsl:otherwise>
                      <td class="tdBorder" style="text-align:left;color:red">
                        ($<xsl:value-of select="format-number($vNYESCBillFee + $vNYInsBillFee,'###,##0.00')" />)
                      </td>
                    </xsl:otherwise>
                  </xsl:choose>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='Y']/Group/Group[@level='3' and generate-id() = generate-id(key('NYTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vnNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vnNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCFee" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vnNYTier]/InvField[@name='ESCFee'])"/>
            <xsl:variable name="vTierInsFee" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vnNYTier]/InvField[@name='INSFee'])"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                Total Sum Of <xsl:value-of select="$vnNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <xsl:choose>
                <xsl:when test="$vTierESCFee = 0">
                  <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                </xsl:when>
                <xsl:otherwise>
                  <td class="tdBorder" style="text-align:left;color:red;">
                    ($<xsl:value-of select="format-number($vTierESCFee,'###,##0.00')" />)
                  </td>
                </xsl:otherwise>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="$vTierInsFee = 0">
                  <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                </xsl:when>
                <xsl:otherwise>
                  <td class="tdBorder" style="text-align:left;color:red;">
                    ($<xsl:value-of select="format-number($vTierInsFee,'###,##0.00')" />)
                  </td>
                </xsl:otherwise>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="$vTierESCFee = 0 and $vTierInsFee = 0">
                  <td class="tdBorder" style="text-align:left;">$ 0.00</td>
                </xsl:when>
                <xsl:otherwise>
                  <td class="tdBorder" style="text-align:left;color:red;">
                    ($<xsl:value-of select="format-number($vTierESCFee + $vTierInsFee,'###,##0.00')" />)
                  </td>
                </xsl:otherwise>
              </xsl:choose>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;color:red">
              ($ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']),'###,##0.00')"/>)
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;color:red">
              ($ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']),'###,##0.00')"/>)
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;color:red">
              ($ <xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']) + sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']),'###,##0.00')"/>)
            </td>
          </tr>
        </table>

        <br/>
        <br/>
        <br/>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">AC - INS Invoice</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="pageTitle" colspan="5" style="text-align:left;">The Signal</td>
          </tr>
          <tr>
            <td rowspan="5" colspan="3" style="text-align:left;">
              676 E.Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone  610-341-1300    Fax 610-341-8939<br/>
              TIN: 22-2623205<br/>
            </td>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2"></td>
          </tr>
          <tr>
            <td colspan="2" class="title" style="text-align:center;">AC - T-Mobile Non-Contributory Insurance Purchased</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Period Days:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//DaysInPeriod" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Bill/Ship to Name</td>
            <td colspan="2" style="text-align:left;">T-Mobile USA, Inc.</td>
            <td class="title" style="text-align:right;">P.O.Number:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Address:</td>
            <td colspan="4" style="text-align:left;">12920 SE 38th Street</td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">City:</td>
            <td colspan="4" style="text-align:left;">Bellevue, WA 98006</td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:rigth;" class="title">Attn:</td>
            <td colspan="4" style="text-align:left;" class="title">Dale Quick</td>
          </tr>
          <tr>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="4" style="text-align:left;" class="title">dale.quick1@t-mobile.com</td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdTitle" style="text-align:left;">Product Description</td>
            <td class="tdTitle" style="text-align:left;">T-Mobile Billing Code</td>
            <td class="tdTitle" style="text-align:left;">Data</td>
            <td class="tdTitle" style="text-align:left;">Total</td>
            <td class="tdTitle" style="text-align:left;">SUM OF AMOUNT DUE SIGNAL</td>
          </tr>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N' and (@PRProd='N' or not(@PRProd))]">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:sort select="./@value"/>
                <xsl:variable name="vTierPos" select="position()"/>
                <tr>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vInsCodePos = 1 and $vTierPos = 1">
                      <xsl:value-of select="$vProdDesc" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    <xsl:if test="$vTierPos = 1">
                      <xsl:value-of select="$vInsCode" />
                    </xsl:if>
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:15px;">
                    <xsl:value-of select="./@value" />
                  </td>
                  <td class="tdBorder" style="text-align:left;padding-right:10px;">
                    <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
                  </td>
                  <td class="tdBorder" style="text-align:left;">
                    $<xsl:value-of select="format-number(./InvField[@name='INSPrem'],'###,##0.00')" />
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:for-each select="//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and generate-id() = generate-id(key('NoneNYTiers',@value)[1])]">
            <xsl:sort select="./@value"/>
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierInsPrem" select="sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='INSPrem'])"/>
            <tr>
              <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                Total Sum Of <xsl:value-of select="$vnoneNYTier"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                <xsl:value-of select="format-number($vTierCount,'###,##0')"/>
              </td>
              <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
                $ <xsl:value-of select="format-number($vTierInsPrem,'###,##0.00')"/>
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td colspan="3" class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              Grand Totals<br/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>
            <td class="tdBorder" style="text-align:left;FONT-WEIGHT: bold;">
              $ <xsl:value-of select="format-number(sum(//Group[@nyProd='N' and (@PRProd='N' or not(@PRProd))]/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/>
            </td>
          </tr>
        </table>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
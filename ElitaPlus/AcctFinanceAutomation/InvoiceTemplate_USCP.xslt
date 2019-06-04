<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:msxsl="urn:schemas-microsoft-com:xslt">

  <xsl:template match="/">
    <html>
      <head>
        <style>
          td {FONT-FAMILY: arial;font-size:12px;}
          .pageTitle { FONT-WEIGHT: bold; FONT-SIZE: 20px; TEXT-ALIGN: left;}
          .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center; }
          .tblInvoicePage{border:2px solid #000;}
          .tblInvoicePage td{padding:3px;white-space:nowrap;text-align:left;}
          .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:14px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
          .tdBorder {border-bottom:1px solid black;padding-left:0px; border-left:1px solid black;}
          .tdBorderTop {border-top:1px solid black;}
          .tdBorderBottom {border-bottom:1px solid black; border-left:1px solid black;}
          .tdBorderLeft {border-left:1px solid black;}
          .trTotal {background-color:#FFFF00;}
          .trWrapText {text-wrap:normal;}
          .titleLeft {FONT-WEIGHT: bold; FONT-SIZE: 14px; text-align:left; }
        </style>
      </head>
      <body>
        <xsl:variable name="vTotal">
          <xsl:for-each select="//Group[@level='1']">
            <ESCCount>
              <xsl:value-of select="sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCCount>
            <ESCClaimExp>
              <xsl:value-of select="./@ESCClaimExp * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCClaimExp>
            <ESCRiskFee>
              <xsl:value-of select="./@ESCRiskFee * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCRiskFee>
            <ESCAdmin>
              <xsl:value-of select="./@ESCAdmin * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCAdmin>
            <ESCTax>
              <xsl:value-of select="./@ESCTax * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCTax>
            <ESCCustNoti>
              <xsl:value-of select="./@ESCCustNoti * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCCustNoti>
            <INSCount>
              <xsl:value-of select="sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSCount>
            <INSClaimExp>
              <xsl:value-of select="./@INSClaimExp * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSClaimExp>
            <INSRiskFee>
              <xsl:value-of select="./@INSRiskFee * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSRiskFee>
            <INSTax>
              <xsl:value-of select="./@INSTax * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSTax>
            <INSAdmin>
              <xsl:value-of select="./@INSAdmin * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSAdmin>
            <INSCustNoti>
              <xsl:value-of select="./@INSCustNoti * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSCustNoti>
            <ActivCount>
              <xsl:value-of select="sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ActivCount'])"/>
            </ActivCount>
            <CancelledCount>
              <xsl:value-of select="sum(./Group[@level='2']/Group[@level='3']/InvField[@name='CancelledCount'])"/>
            </CancelledCount> 
            <ESCPrem>
              <xsl:value-of select="./@ESCPrem * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='ESCCount'])"/>
            </ESCPrem>
            <INSPrem>
              <xsl:value-of select="./@INSPrem * sum(./Group[@level='2']/Group[@level='3']/InvField[@name='INSCount'])"/>
            </INSPrem>
          </xsl:for-each>
        </xsl:variable>
          
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td class="pageTitle" colspan="5">Summary</td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td colspan="7">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="2" class="pageTitle" style="text-align:center;">The Signal</td>
            <td>
              Vendor Number: 216542
            </td>
            <td class="title" style="text-align:left;">Invoice #</td>
            <td style="text-align:left;">
              PRP-<xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" />
            </td>
          </tr>
          <tr>
            <td colspan="3" rowspan="4" style="text-align:left;">
              676 E. Swedesford Road<br/>
              Suite 300<br/>
              Wayne, PA 19087-1631<br/>
              Phone: 610-341-1300 Fax: 610-341-8939
            </td>
            <td class="title"  style="text-align:left;">Invoice Date:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//InvoiceDate" />
            </td>
          </tr>
          <tr>
            <td class="title"  style="text-align:left;">Prepared By:</td>
            <td  style="text-align:left;">Elita</td>
          </tr>
          <tr>
            <td class="title"  style="text-align:left;">Due Date:</td>
            <td  style="text-align:left;">
              <xsl:value-of select="//DueDate" />
            </td>
          </tr>
          <tr>
            <td class="title"  style="text-align:left;">Period:</td>
            <td  style="text-align:left;">
              <xsl:value-of select="//Period" />
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Bill To Name:</td>
            <td style="text-align:left;">U.S. Cellular®</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Address:</td>
            <td style="text-align:left;">PO Box 620989</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">City:</td>
            <td style="text-align:left;">Middleton, WI  53562-0989</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">ATTN:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//AttnName" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Cost Center #:</td>
            <td style="text-align:left;">239005</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Data</td>
            <td class="title" style="text-align:right;">Total</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of ESC Count</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCCount),'###,##0')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of ESC Claims Expense</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCClaimExp),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of ESC Risk Fee (5%)</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCRiskFee),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">
              Sum of ESC Enroll <xsl:text disable-output-escaping="yes">&amp;</xsl:text> Claims Admin (ECA)
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCAdmin),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of ESC Taxes and Assessments</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCTax),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of ESC Customer Notifications</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCCustNoti),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total ESC Payable to Assurant</td>
            <td class="title trTotal" style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCClaimExp) + sum(msxsl:node-set($vTotal)/ESCRiskFee) + sum(msxsl:node-set($vTotal)/ESCAdmin) + sum(msxsl:node-set($vTotal)/ESCTax) + sum(msxsl:node-set($vTotal)/ESCCustNoti),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Data</td>
            <td class="title" style="text-align:right;">Total</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of INS Count</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSCount),'###,##0')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of INS Claims Expense</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSClaimExp),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of INS Risk Fee (5%)</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSRiskFee),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">
              Sum of INS Enroll <xsl:text disable-output-escaping="yes">&amp;</xsl:text> Claims Admin (ECA)
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSAdmin),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of INS Taxes and Assessments</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSTax),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td style="text-align:left;">Sum of INS Customer Notifications</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSCustNoti),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total INS Payable to Assurant</td>
            <td class="title trTotal" style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSClaimExp) + sum(msxsl:node-set($vTotal)/INSRiskFee) + sum(msxsl:node-set($vTotal)/INSAdmin) + sum(msxsl:node-set($vTotal)/INSTax) + sum(msxsl:node-set($vTotal)/INSCustNoti),'###,##0.00')"/>
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Grand Total Assurant</td>
            <td class="title trTotal" style="text-align:right;">
              <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCClaimExp) + sum(msxsl:node-set($vTotal)/ESCRiskFee) + sum(msxsl:node-set($vTotal)/ESCAdmin) + sum(msxsl:node-set($vTotal)/ESCTax) + sum(msxsl:node-set($vTotal)/ESCCustNoti) + sum(msxsl:node-set($vTotal)/INSClaimExp) + sum(msxsl:node-set($vTotal)/INSRiskFee) + sum(msxsl:node-set($vTotal)/INSAdmin) + sum(msxsl:node-set($vTotal)/INSTax) + sum(msxsl:node-set($vTotal)/INSCustNoti),'###,##0.00')"/>
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
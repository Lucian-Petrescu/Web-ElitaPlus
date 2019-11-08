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
          .tblInvoicePage td{padding:3px;white-space:nowrap;text-align:center; border-bottom:1px solid black;padding-left:0px;}
          .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:14px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
          .tdBorder {border-bottom:1px solid black;padding-left:0px;}
          .tdBorderTop {border-top:1px solid black;}
          .tdBorderBottom {border-bottom:1px solid black;}
          .tdBorderLeft {border-left:1px solid black;}
          .trTotal {background-color:#FFFF00;)          
        </style>
      </head>
      <body>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="4" class="pageTitle" style="text-align:left;">
              <xsl:element name="img">
                <xsl:attribute name="src">
                  <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_apr.jpg</xsl:text>
                </xsl:attribute>
              </xsl:element>
            </td>
          </tr>          
          <tr>
            <td class="title"  style="text-align:left;">Market Code</td>
            <td class="title"  style="text-align:left;">Data</td>
            <td class="title"  style="text-align:left;">Billing Day:</td>
            <td class="title leftPadding"  style="text-align:left;padding-left:20px;">
              <xsl:value-of select="//BillingDay" />
            </td>
          </tr>

          <xsl:for-each select="/InvoiceData/Group[@level='1']">
            <tr>
              <td class="title" style="text-align:left;">
                <xsl:value-of select="./@value" />
              </td>
              <td style="text-align:left;">
                Sum of Billable Count
              </td>
              <td style="text-align:right;">
                <xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" />
              </td>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td style="text-align:left;">
                Sum of MRC
              </td>
              <td style="text-align:right;">
                $<xsl:value-of select="format-number(./InvField[@name='MRC'],'###,##0.00')" />
              </td>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td style="text-align:left;">
                Sum of Assurant Payable
              </td>
              <td style="text-align:right;">
                $<xsl:value-of select="format-number(./InvField[@name='AssurantPayable'],'###,##0.00')" />
              </td>
              <td style="text-align:left;padding-left:20px;">
                <xsl:value-of select="./InvField[@name='APCode']" />
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td  style="text-align:left;">
                Sum of BCI Revenue
              </td>
              <td style="text-align:right;color:red;">
                ($<xsl:value-of select="format-number(./InvField[@name='BCIRevenue'],'###,##0.00')" />)
              </td>
              <td style="text-align:left;color:red;padding-left:20px;">
                <xsl:value-of select="./InvField[@name='BCICode']" />
              </td>
            </tr>
            <tr>
              <td>
                <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
              </td>
              <td style="text-align:left;">
                Sum of KY Surcharge
              </td>
              <td style="text-align:right;">
                $<xsl:value-of select="format-number(./InvField[@name='KYSurcharge'],'###,##0.00')" />
              </td>
              <td style="text-align:left;padding-left:20px;">
                <xsl:value-of select="./InvField[@name='KYCode']" />
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td class="title" colspan="2" style="text-align:left;">
              Total Sum of Billable Count
            </td>
            <td class="title"  style="text-align:right;">
              <xsl:value-of select="format-number(sum(//Group/InvField[@name='BillingCnt']),'###,##0')"/>
            </td>            
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:left;">
              Total Sum of MRC
            </td>
            <td class="title"  style="text-align:right;">
              $<xsl:value-of select="format-number(sum(//Group/InvField[@name='MRC']),'###,##0.00')"/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:left;">
              Total Sum of Assurant Payable
            </td>
            <td class="title"  style="text-align:right;">
              $<xsl:value-of select="format-number(sum(//Group/InvField[@name='AssurantPayable']),'###,##0.00')"/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:left;">
              Total Sum of BCI Revenue
            </td>
            <td class="title"  style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number(sum(//Group/InvField[@name='BCIRevenue']),'###,##0.00')"/>)
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:left;">
              Total Sum of KY Surcharge
            </td>
            <td class="title"  style="text-align:right;">
              $<xsl:value-of select="format-number(sum(//Group/InvField[@name='KYSurcharge']),'###,##0.00')"/>
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>          
          <tr>            
            <td class="title" colspan="4" style="text-align:left;">
              The total premium includes the surcharge required by KRS 136.392
            </td>            
          </tr>          
          <tr>
            <td colspan="4">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title trTotal" colspan="2" style="text-align:left;">
              Amount Payable To Assurant
            </td>
            <td class="title trTotal"  style="text-align:right;">
              $<xsl:value-of select="format-number(sum(//Group/InvField[@name='AssurantPayable']) + sum(//Group/InvField[@name='KYSurcharge']),'###,##0.00')"/>
            </td>
            <td class="trTotal">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="4">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:right;border:none;">
              Damian Sanderson _________________________
            </td>
            <td colspan="2" style="border:none;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="2" style="text-align:right;border:none;">
              Barry Nothstine _________________________
            </td>
            <td colspan="2" style="border:none;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" colspan="3" style="text-align:right;border-top:1px solid black;">
              Date Submitted for payment <xsl:value-of select="//InvoiceDate" />
            </td>
            <td style="border-top:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
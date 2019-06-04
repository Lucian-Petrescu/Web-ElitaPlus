<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
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
          .tdBorderBottom {border-bottom:1px solid black;}
          .tdBorderLeft {border-left:1px solid black;}
          .trTotal {background-color:#FFFF00;)
        </style>
      </head>
      <body>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="2" class="pageTitle" style="text-align:center;">The Signal</td>
            <td style="width:150px;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">Invoice #</td>
            <td style="text-align:left;">
              167 - DP+ - <xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" />
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
            <td style="text-align:left;">United States Cellular</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">USCC Vendor #:</td>
            <td style="text-align:left;">108364</td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">Address:</td>
            <td style="text-align:left;">PO Box 628430</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">P.O. Number:</td>
            <td style="text-align:left;">
              <xsl:value-of select="//PONumber" />
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:right;">City:</td>
            <td style="text-align:left;">Middleton, WI  53562-8430</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">USCC Vendor #:</td>
            <td style="text-align:left;">108364</td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Basic -->
          <xsl:variable name="vBNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Basic</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBNonEmpCnt + $vBEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="2" class="title" style="text-align:left;">* Wire Transfer information</td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Employees</td>
            <td style="text-align:right;color:red;">
              (<xsl:value-of select="format-number($vBEmpCnt,'###,##0')" />)
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">Bank:</td>
            <td style="text-align:left;">JP Morgan Chase Bank</td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBNonEmpCnt,'###,##0')" />
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">Customer:</td>
            <td style="text-align:left;">The Signal LP</td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vBNonEmpGWP,'###,##0.00')" />
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">ABA# :</td>
            <td style="text-align:left;">021000021</td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vBNonEmpFee,'###,##0.00')" />)
            </td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title" style="text-align:left;">Account# :</td>
            <td style="text-align:left;">771060613</td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vBNonEmpGWP - $vBNonEmpFee,'###,##0.00')" />
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
          <!-- Basic Employee 1.00 -->
          <xsl:variable name="vBDiscount1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBCnt1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBGWP1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBFee1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[1]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Basic -- Employee ($<xsl:value-of select="format-number($vBDiscount1,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vBGWP1,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vBFee1,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vBGWP1 - $vBFee1,'###,##0.00')" />
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
          <!-- Basic Employee 2.04 -->
          <xsl:variable name="vBDiscount2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBCnt2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBGWP2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vBFee2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='BASIC']/Employee[2]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Basic -- Employee ($<xsl:value-of select="format-number($vBDiscount2,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vBGWP2,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vBFee2,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vBGWP2 - $vBFee2,'###,##0.00')" />
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
          <!-- Smart -->
          <xsl:variable name="vSNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Smart</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSNonEmpCnt + $vSEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td>
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Employees</td>
            <td style="text-align:right;color:red;">
              (<xsl:value-of select="format-number($vSEmpCnt,'###,##0')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSNonEmpGWP - $vSNonEmpFee,'###,##0.00')" />
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
          <!-- Smart Employee 2.00 -->
          <xsl:variable name="vSDiscount1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSCnt1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSGWP1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSFee1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[1]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart -- Employee ($<xsl:value-of select="format-number($vSDiscount1,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSGWP1,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSFee1,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSGWP1 - $vSFee1,'###,##0.00')" />
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
          <!-- Smart Employee 4.04 -->
          <xsl:variable name="vSDiscount2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSCnt2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSGWP2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSFee2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMART']/Employee[2]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart -- Employee ($<xsl:value-of select="format-number($vSDiscount2,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSGWP2,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSFee2,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSGWP2 - $vSFee2,'###,##0.00')" />
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
          <!-- Smart iPhone / iPad -->
          <xsl:variable name="vSPNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Smart - iPhone/iPad</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPNonEmpCnt + $vSPEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Employees</td>
            <td style="text-align:right;color:red;">
              (<xsl:value-of select="format-number($vSPEmpCnt,'###,##0')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPNonEmpGWP - $vSPNonEmpFee,'###,##0.00')" />
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
          <!-- Smart iPhone Employee 2.00 -->
          <xsl:variable name="vSPDiscount1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPCnt1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPGWP1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPFee1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[1]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart - iPhone/iPad -- Employee ($<xsl:value-of select="format-number($vSPDiscount1,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPGWP1,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPFee1,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPGWP1 - $vSPFee1,'###,##0.00')" />
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
          <!-- Smart iPhone Employee 4.04 -->
          <xsl:variable name="vSPDiscount2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPCnt2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPGWP2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPFee2">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTIPHONE']/Employee[2]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart - iPhone/iPad -- Employee ($<xsl:value-of select="format-number($vSPDiscount2,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt2,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPGWP2,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPFee2,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPGWP2 - $vSPFee2,'###,##0.00')" />
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
          <!-- Smart Advanced -->
          <xsl:variable name="vSANetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSANonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSANonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSANonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Smart Advanced</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSANonEmpCnt + $vSAEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Employees</td>
            <td style="text-align:right;color:red;">
              (<xsl:value-of select="format-number($vSAEmpCnt,'###,##0')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSANonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSANonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSANonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSANonEmpGWP - $vSANonEmpFee,'###,##0.00')" />
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
          <!-- Smart Advanced Employee 7.04 -->
          <xsl:variable name="vSADiscount1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSACnt1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAGWP1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAFee1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCED']/Employee[1]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart Advanced -- Employee ($<xsl:value-of select="format-number($vSADiscount1,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSACnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSACnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSAGWP1,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSAFee1,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSAGWP1 - $vSAFee1,'###,##0.00')" />
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
          <!-- Smart Advanced iPad & Tablets -->
          <xsl:variable name="vSAPTNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPTNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPTEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPTNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPTNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVIPADTABLETS']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Smart Advanced - iPad <xsl:text disable-output-escaping="yes">&amp;</xsl:text> Tablets</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPTNonEmpCnt + $vSAPTEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPTNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPTNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSAPTNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSAPTNonEmpGWP - $vSAPTNonEmpFee,'###,##0.00')" />
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
          <!-- Smart Advanced iPhone -->
          <xsl:variable name="vSAPNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">ASSURE CONNECT DP+ Smart Advanced - iPhone</td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPNonEmpCnt + $vSAPEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Employees</td>
            <td style="text-align:right;color:red;">
              (<xsl:value-of select="format-number($vSAPEmpCnt,'###,##0')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSAPNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSAPNonEmpGWP - $vSAPNonEmpFee,'###,##0.00')" />
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
          <!-- Smart Advanced iPhone Employee 7.04 -->
          <xsl:variable name="vSAPDiscount1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Discount']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Discount']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPCnt1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Cnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Cnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPGWP1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='GWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='GWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSAPFee1">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Fee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='SMARTADVANCEDIPHONE']/Employee[1]/InvField[@name='Fee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              ASSURE CONNECT DP+ Smart Advanced - iPhone -- Employee ($<xsl:value-of select="format-number($vSAPDiscount1,'###,##0.00')" />)
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSAPCnt1,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPGWP1,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSAPFee1,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSAPGWP1 - $vSAPFee1,'###,##0.00')" />
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
          <!-- DP+ STANDARD PROGRAM BUSINESS -->
          <xsl:variable name="vSPBNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSN']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              DP+ STANDARD PROGRAM BUSINESS
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBNonEmpCnt + $vSPBEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPBNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPBNonEmpGWP - $vSPBNonEmpFee,'###,##0.00')" />
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
          <!-- DP+ STANDARD PROGRAM BUSINESS-iPhone -->
          <xsl:variable name="vSPBINetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBINonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBIEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBINonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBINonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPHONE']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              DP+ STANDARD PROGRAM BUSINESS-iPhone
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBINonEmpCnt + $vSPBIEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBINonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBINonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPBINonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPBINonEmpGWP - $vSPBINonEmpFee,'###,##0.00')" />
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
          <!-- DP+ STANDARD PROGRAM BUSINESS-Tablet -->
          <xsl:variable name="vSPBTNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBTNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBTEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBTNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBTNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNTABLET']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              DP+ STANDARD PROGRAM BUSINESS-Tablet
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBTNonEmpCnt + $vSPBTEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBTNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBTNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPBTNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPBTNonEmpGWP - $vSPBTNonEmpFee,'###,##0.00')" />
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
          <!-- DP+ STANDARD PROGRAM BUSINESS-iPad -->
          <xsl:variable name="vSPBIPNetPrice">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NetPrice']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NetPrice']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBIPNonEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBIPEmpCnt">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='EmployeeCnt']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='EmployeeCnt']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBIPNonEmpGWP">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeGWP']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeGWP']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="vSPBIPNonEmpFee">
            <xsl:choose>
              <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']">
                <xsl:choose>
                  <xsl:when test="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeFee']='NaN'">0</xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="/InvoiceData/Group[@level='1' and @value='STDPGRMBUSNIPAD']/InvField[@name='NonEmployeeFee']" />
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:when>
              <xsl:otherwise>0</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr>
            <td class="title" style="text-align:left; border:1px solid black;">
              DP+ STANDARD PROGRAM BUSINESS-iPad
            </td>
            <td colspan="4" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">
              Active Subscribers as of <xsl:value-of select="//InvoiceMonth" />/<xsl:value-of select="//DaysInPeriod" />/<xsl:value-of select="//InvoiceYear" />
            </td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBIPNonEmpCnt + $vSPBIPEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Duplicates</td>
            <td style="text-align:right;">0</td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Total Billable Active Subscribers</td>
            <td style="text-align:right;">
              <xsl:value-of select="format-number($vSPBIPNonEmpCnt,'###,##0')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Gross Written Premium</td>
            <td style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBIPNonEmpGWP,'###,##0.00')" />
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left;">Less: Billing Fee</td>
            <td style="text-align:right;color:red;">
              ($<xsl:value-of select="format-number($vSPBIPNonEmpFee,'###,##0.00')" />)
            </td>
            <td colspan="3">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title" style="text-align:left; border-bottom:1px solid black;">Net Remittance</td>
            <td style="text-align:right; border:1px solid black;">
              $<xsl:value-of select="format-number($vSPBIPNonEmpGWP - $vSPBIPNonEmpFee,'###,##0.00')" />
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
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Total -->
          <tr>
            <td class="title" colspan="5" style="text-align:left;border-top:1px solid black;border-bottom:1px solid black;">Grand Totals</td>
          </tr>
          <tr>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title tdBorderBottom" style="text-align:right;">Quantity</td>
            <td class="title tdBorderBottom" style="text-align:right;">Net Price</td>
            <td class="title tdBorderBottom" style="text-align:right;">Totals</td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Basic -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Basic</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vBNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBNonEmpGWP - $vBNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Smart</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSNonEmpGWP - $vSNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart iPhone -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Smart - iPhone/iPad</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPNonEmpGWP - $vSPNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart Advanced -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Smart Advanced</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSANonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSANetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSANonEmpGWP - $vSANonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart Advanced iPad & Tablets -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Smart Advanced - iPad <xsl:text disable-output-escaping="yes">&amp;</xsl:text> Tablets</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSAPTNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPTNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPTNonEmpGWP - $vSAPTNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart Advanced iPhone -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- Assure Connect DP+ Smart Advanced - iPhone</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSAPNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPNonEmpGWP - $vSAPNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- STANDARD PROGRAM BUSINESS -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- DP+ STANDARD PROGRAM BUSINESS</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPBNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBNonEmpGWP - $vSPBNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- STANDARD PROGRAM BUSINESS - iPhone -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- DP+ STANDARD PROGRAM BUSINESS -iPhone</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPBINonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBINetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBINonEmpGWP - $vSPBINonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- STANDARD PROGRAM BUSINESS - Tablet -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- DP+ STANDARD PROGRAM BUSINESS -Tablet</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPBTNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBTNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBTNonEmpGWP - $vSPBTNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- STANDARD PROGRAM BUSINESS - iPad -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Net Remittance -- DP+ STANDARD PROGRAM BUSINESS -iPad</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPBIPNonEmpCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBIPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPBIPNonEmpGWP - $vSPBIPNonEmpFee,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Grand Total Assure Connect -->
          <xsl:variable name="vNetRemtCount">
            <xsl:value-of select="$vBNonEmpCnt + $vSNonEmpCnt + $vSPNonEmpCnt + $vSANonEmpCnt + $vSAPTNonEmpCnt + $vSAPNonEmpCnt + $vSPBNonEmpCnt + $vSPBINonEmpCnt + $vSPBTNonEmpCnt + $vSPBIPNonEmpCnt" />
          </xsl:variable>
          <xsl:variable name="vNetRemtTotal">
            <xsl:value-of select="($vBNonEmpGWP - $vBNonEmpFee) + ($vSNonEmpGWP - $vSNonEmpFee) + ($vSPNonEmpGWP - $vSPNonEmpFee) + ($vSANonEmpGWP - $vSANonEmpFee) + ($vSAPTNonEmpGWP - $vSAPTNonEmpFee) + ($vSAPNonEmpGWP - $vSAPNonEmpFee) + ($vSPBNonEmpGWP - $vSPBNonEmpFee) + ($vSPBINonEmpGWP - $vSPBINonEmpFee) + ($vSPBTNonEmpGWP - $vSPBTNonEmpFee) + ($vSPBIPNonEmpGWP - $vSPBIPNonEmpFee)" />
          </xsl:variable>
          <tr>
            <td class="title tdBorderBottom trTotal" style="text-align:left;">Net Remittance Grand Total -- Assure Connect</td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              <xsl:value-of select="format-number($vNetRemtCount,'###,##0')" />
            </td>
            <td class="tdBorderBottom trTotal" style="text-align:right;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              $<xsl:value-of select="format-number($vNetRemtTotal,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom trTotal">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- PO BreakOut -->
          <tr>
            <td class="title" colspan="5" style="text-align:left;border-top:1px solid black;border-bottom:1px solid black;">P.O. Breakout for Employee Payment</td>
          </tr>
          <!-- PO BreakOut First Set -->
          <!-- Basic -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Basic</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt1,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBGWP1 - $vBFee1,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Basic</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vBCnt2,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vBGWP2 - $vBFee2,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt1,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSGWP1 - $vSFee1,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSCnt2,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSGWP2 - $vSFee2,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart iPhone/iPad -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart - iPhone/iPad</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt1,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPGWP1 - $vSPFee1,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart - iPhone/iPad</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSPCnt2,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSPGWP2 - $vSPFee2,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart Advanced -->
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart Advanced</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSACnt1,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSANetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAGWP1 - $vSAFee1,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <!-- Smart Advanced iPhone -->  
          <tr>
            <td class="title tdBorderBottom" style="text-align:left;">Total for Line Item 00020 <xsl:text disable-output-escaping="yes">&amp;</xsl:text> 0070 -- Assure Connect DP+ Smart Advanced - iPhone</td>
            <td class="tdBorderBottom" style="text-align:right;">
              <xsl:value-of select="format-number($vSAPCnt1,'###,##0')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPNetPrice,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom" style="text-align:right;">
              $<xsl:value-of select="format-number($vSAPGWP1 - $vSAPFee1,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <xsl:variable name="vPOBrkOutSet1">
            <xsl:value-of select="($vBGWP1 - $vBFee1) - ($vBCnt1 * $vBDiscount1) + 
                                  ($vBGWP2 - $vBFee2) - ($vBCnt2 * $vBDiscount2) + 
                                  ($vSGWP1 - $vSFee1) - ($vSCnt1 * $vSDiscount1) + 
                                  ($vSGWP2 - $vSFee2) - ($vSCnt2 * $vSDiscount2) + 
                                  ($vSPGWP1 - $vSPFee1) - ($vSPCnt1 * $vSPDiscount1) + 
                                  ($vSPGWP2 - $vSPFee2) - ($vSPCnt2 * $vSPDiscount2) + 
                                  ($vSAGWP1 - $vSAFee1) - ($vSACnt1 * $vSADiscount1) + 
                                  ($vSAPGWP1 - $vSAPFee1) - ($vSAPCnt1 * $vSAPDiscount1)" />
          </xsl:variable>
          <xsl:variable name="vPOBrkOutSet2">
            <xsl:value-of select="($vBCnt1 * $vBDiscount1) + ($vBCnt2 * $vBDiscount2) + 
                                  (($vSCnt1 + $vSPCnt1) * $vSDiscount1) + 
                                  (($vSCnt2 + $vSPCnt2) * $vSDiscount2) + 
                                  (($vSACnt1 + $vSAPCnt1) * $vSADiscount1)" />
          </xsl:variable>
          <xsl:variable name="vPOBrkOutCnt">
            <xsl:value-of select="$vBCnt1 + $vBCnt2 + $vSCnt1 + $vSCnt2 + $vSPCnt1 + $vSPCnt2 + $vSACnt1 + $vSAPCnt1" />
          </xsl:variable>
          <!-- Grand Total Assure Connect Employee -->
          <tr>
            <td class="title tdBorderBottom trTotal" style="text-align:left;">Net Remittance -- Grand Total Assure Connect Employee</td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              <xsl:value-of select="format-number($vPOBrkOutCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom trTotal" style="text-align:right;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              $<xsl:value-of select="format-number($vPOBrkOutSet1 + $vPOBrkOutSet2,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom trTotal">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="5" style="border-bottom:1px solid black;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr >
            <td class="title tdBorderBottom trTotal" style="text-align:left;" >Total Net Remittance</td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              <xsl:value-of select="format-number($vNetRemtCount + $vPOBrkOutCnt,'###,##0')" />
            </td>
            <td class="tdBorderBottom trTotal" style="text-align:right;">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
            <td class="title tdBorderBottom trTotal" style="text-align:right;">
              $<xsl:value-of select="format-number($vNetRemtTotal + $vPOBrkOutSet1 + $vPOBrkOutSet2,'###,##0.00')" />
            </td>
            <td class="tdBorderBottom trTotal">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
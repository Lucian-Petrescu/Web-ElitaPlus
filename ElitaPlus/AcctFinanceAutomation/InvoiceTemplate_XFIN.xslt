<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:variable name="lossTypeESC">CTYP_I_ESC</xsl:variable>
  <xsl:variable name="lossTypeINS">CTYP_I_INS</xsl:variable>
  <xsl:variable name="lossTypeJUMP">CTYP_I_JUMP</xsl:variable>

  <xsl:template match="/">

    <html>
      <head>
        <style>
          td {FONT-FAMILY: arial;font-size:12px;}
          .pageTitle { FONT-WEIGHT: bold; FONT-SIZE: 20px; TEXT-ALIGN: left;}
          .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center; }
          .tblInvoicePage{border:2px solid #000;}
          .tblInvoicePage td{padding:3px;white-space:nowrap;text-align:center;}
          .tblBoxes{border:1px solid #000;border-radius: 10px;padding: 5px;}
          .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:14px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
          .tdBorder {border-bottom:1px solid black;padding-left:0px;}
          .tdBorderTop {border-top:1px solid black;}
          .tdBorderBottom {border-bottom:1px solid black;}
          .tdBorderLeft {border-left:1px solid black;}
          .trTotal {background-color:#FFFF00;}

          span {FONT-FAMILY: arial;font-size:12px;}
          .number {float:right;}
          .grandTotal {float:right; border-top:1px solid black;}
          .grandGrandTotal {float:right; border-top-width:1px; border-top-style:solid; border-bottom-style:double;}
          .subtotal {float:right; font-weight: bold}
          .title {float:left; font-weight: normal}
          .titleRight {float:right; font-weight: bold}

          .productTitle {
          text-align:left;
          }
          .assurantLogo { float: right; margin-top: -5px; }
        </style>
      </head>
      <body>
        <table class="tblInvoicePage" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="4">
              <div class="assurantLogo">
                <img src="../App_Themes/Default/Images/assurant_logo.png" class="assurantLogo"/>
              </div>
              <div class="pageTitle">The Signal</div>
              <br/>
              <div style="text-align:left;">
                676 E. Swedesford Road<br/>
                Suite 300<br/>
                Wayne, PA 19087-1631<br/>
                Phone: 610-341-1300 Fax: 610-341-8939
              </div>
            </td>
            <td >
              <table class="tblBoxes" align="right">
                <tr>
                  <td class="title" style="text-align:left;">Invoice #</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//InvoiceNumber" />
                  </td>
                </tr>
                <tr>
                  <td class="title"  style="text-align:left;">Invoice Date:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//InvoiceDate" />
                  </td>
                </tr>
                <tr>
                  <td class="title"  style="text-align:left;">Prepared By:</td>
                  <td  style="text-align:left;">
                    <xsl:value-of select="//PreparedBy" />
                  </td>
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
              </table>
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <hr/>
            </td>
          </tr>
          <tr>
            <td colspan="3">
              <table class="tblBoxes">
                <tr>
                  <td class="title" style="text-align:left;">Bill To Name:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//BillTo/Name" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Address:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//BillTo/Address" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">City:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//BillTo/City" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">ATTN:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//BillTo/AttN" />
                  </td>
                </tr>
              </table>
            </td>
            <td colspan="2">
              <table class="tblBoxes" align="right">
                <tr>
                  <td class="title" style="text-align:left;">Bank:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Payment/Bank" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Customer:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Payment/Customer" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">ABA #:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Payment/ABA" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Account:</td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Payment/Account" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <h2>
                <xsl:value-of select="//Period" />
                <xsl:text disable-output-escaping="yes"> Invoice Settlement from Assurant</xsl:text>
              </h2>
            </td>
            <td/>
            <td colspan="2">
              <table style="background-color: yellow;float:right; border:2px solid #000">
                <tr>
                  <td>
                    <h2>Amount Due:</h2>
                  </td>
                  <td>
                    <h2>
                      <xsl:call-template name="moneyFormated">
                        <xsl:with-param name="value">
                          <xsl:call-template name="grandTotal"/>
                        </xsl:with-param>
                      </xsl:call-template>
                    </h2>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td colspan="5">
              <table style="width:100%">
                <tr>
                  <td>Item</td>
                  <td>Description</td>
                  <td>Price</td>
                  <td>Total</td>
                </tr>
                <xsl:apply-templates select="//Items/Group[@value='AFAPT_AC']" mode="group_a" />

                <tr>
                  <td colspan="4">
                    <br/>
                    <br/>
                    <br/>
                  </td>
                </tr>
                <xsl:apply-templates select="//Items/Group[@value='AFAPT_TIP']" mode="group_b" />

              </table>
            </td>
          </tr>
        </table>

        <br/>
        <table class="tblInvoicePage" cellpadding="4" cellspacing="2">
          <thead>
            <tr>
              <th>
                <div>Product</div>
              </th>
              <th>
                <div>
                  Sum of<br/>Billable Count
                </div>
              </th>
              <th>
                <div>
                  Sum of<br/>Cancel Credit
                </div>
              </th>
              <th>
                <div>Total Net</div>
              </th>
              <th>
                <div>
                  ESC<br/>Premium
                </div>
              </th>
              <th>
                <div>
                  ESC<br/>Commission
                </div>
              </th>
              <th>
                <div>
                  INS<br/>Premium
                </div>
              </th>
              <th>
                <div>
                  INS<br/>Commission
                </div>
              </th>
              <th>
                <div>
                  UPGRADE<br/>Ancillary
                </div>
              </th>
              <th>
                <div>
                  PHONE SUPPORT<br/>(Other Amount)
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan="10">
                <div class="productTitle">
                  <h3>Products: AssureConnection</h3>
                </div>
              </td>
            </tr>
            <xsl:apply-templates select="//Items/Group[@value='AFAPT_AC']" mode="assureConnection" />
            <tr>
              <td colspan="10">
                <hr/>
              </td>
            </tr>
            <tr>
              <td colspan="10">
                <div class="productTitle">
                  <h3>Products: INS/ESC</h3>
                </div>
              </td>
            </tr>
            <xsl:apply-templates select="//Items/Group[@value='AFAPT_TIP']" mode="assureConnection" />

            <tr>
              <td colspan="3"/>
              <td>
                <h3>Grand Total Invoice:</h3>
              </td>
              <td>
                <span class="grandGrandTotal">
                  <xsl:call-template name="moneyFormated">
                    <xsl:with-param name="value">
                      <xsl:call-template name="grandTotalPivot" />
                    </xsl:with-param>
                  </xsl:call-template>

                </span>
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="Group" mode="group_a">
    <xsl:variable name="tot_assure_conn_esc">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeESC]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_assure_conn_ins">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeINS]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_esc">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeESC]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_ins">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeINS]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_upgrade">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/AncillaryAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_phone_support">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/OtherAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_group_a">
      <xsl:value-of select="$tot_assure_conn_esc + $tot_assure_conn_ins + $tot_tip_esc + $tot_tip_ins + $tot_upgrade + $tot_phone_support" />
    </xsl:variable>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">1</xsl:with-param>
      <xsl:with-param name="description">Assure Connection ESC</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_assure_conn_esc" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">2</xsl:with-param>
      <xsl:with-param name="description">Assure Connection INS</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_assure_conn_ins" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">3</xsl:with-param>
      <xsl:with-param name="description">TIP ESC</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_tip_esc" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">4</xsl:with-param>
      <xsl:with-param name="description">TIP INS</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_tip_ins" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">5</xsl:with-param>
      <xsl:with-param name="description">Upgrade</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_upgrade" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">6</xsl:with-param>
      <xsl:with-param name="description">Phone Support</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_phone_support" />
      </xsl:with-param>
    </xsl:call-template>
    <tr>
      <td colspan="3">
        <span class="titleRight">Gross Premium Due</span>
      </td>
      <td>
        <span class="subtotal" style="border-bottom: 1px solid;">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$tot_group_a" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Group" mode="group_b">
    <xsl:variable name="tot_esc_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeESC]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_esc_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeESC]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_ins_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeINS]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_group_b">
      <xsl:value-of select="$tot_esc_fee + $tot_tip_esc_fee + $tot_tip_ins_fee" />
    </xsl:variable>

    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">1</xsl:with-param>
      <xsl:with-param name="description">Assure Connection ESC Billing Fee</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_esc_fee" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">2</xsl:with-param>
      <xsl:with-param name="description">TIP ESC Billing Fee</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_tip_esc_fee" />
      </xsl:with-param>
    </xsl:call-template>
    <xsl:call-template name="linesByGroup">
      <xsl:with-param name="position">3</xsl:with-param>
      <xsl:with-param name="description">TIP INS Billing Fee</xsl:with-param>
      <xsl:with-param name="amount">
        <xsl:value-of select="$tot_tip_ins_fee" />
      </xsl:with-param>
    </xsl:call-template>
    <tr>
      <td colspan="3">
        <span class="titleRight">Net Premium Due</span>
      </td>
      <td>
        <span class="subtotal" style="border-bottom: 1px solid;">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$tot_group_b" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Group" mode="assureConnection">
    <xsl:for-each select="LossType/Product[not(@value=preceding::*/@value)]">

      <xsl:variable name="producCode">
        <xsl:value-of select="@value" />
      </xsl:variable>
      <tr>
        <td>
          <xsl:value-of select="$producCode" />
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="numberFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="BillingCount/text()" />
              </xsl:with-param>
            </xsl:call-template>

          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="numberFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="CancelCount/text()" />
              </xsl:with-param>
            </xsl:call-template>

          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="numberFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="number(BillingCount/.) + number(CancelCount/.)" />
              </xsl:with-param>
            </xsl:call-template>

          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeESC]/Product[@value=$producCode]/PremiumAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeESC]/Product[@value=$producCode]/CommissionAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeINS]/Product[@value=$producCode]/PremiumAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeINS]/Product[@value=$producCode]/CommissionAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeJUMP]/Product[@value=$producCode]/AncillaryAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
        <td>
          <span class="number">
            <xsl:call-template name="moneyFormated">
              <xsl:with-param name="value">
                <xsl:value-of select="ancestor::Group/LossType[@value=$lossTypeJUMP]/Product[@value=$producCode]/OtherAmount" />
              </xsl:with-param>
            </xsl:call-template>
          </span>
        </td>
      </tr>
    </xsl:for-each>

    <xsl:variable name="totalBillingCount">
      <xsl:value-of select="sum(LossType/Product[not(@value=preceding::*/@value)]/BillingCount/text())" />
    </xsl:variable>
    <xsl:variable name="totalCancelCount">
      <xsl:value-of select="sum(LossType/Product[not(@value=preceding::*/@value)]/CancelCount/text())" />
    </xsl:variable>
    <xsl:variable name="totalESCPremium">
      <xsl:value-of select="sum(LossType[@value=$lossTypeESC]/Product/PremiumAmount)" />
    </xsl:variable>
    <xsl:variable name="totalESCCommission">
      <xsl:value-of select="sum(LossType[@value=$lossTypeESC]/Product/CommissionAmount)" />
    </xsl:variable>
    <xsl:variable name="totalINSPremium">
      <xsl:value-of select="sum(LossType[@value=$lossTypeINS]/Product/PremiumAmount)" />
    </xsl:variable>
    <xsl:variable name="totalINSCommission">
      <xsl:value-of select="sum(LossType[@value=$lossTypeINS]/Product/CommissionAmount)" />
    </xsl:variable>
    <xsl:variable name="totalUpgrade">
      <xsl:value-of select="sum(LossType[@value=$lossTypeJUMP]/Product/AncillaryAmount)" />
    </xsl:variable>
    <xsl:variable name="totalPhoneSupport">
      <xsl:value-of select="sum(LossType[@value=$lossTypeJUMP]/Product/OtherAmount)" />
    </xsl:variable>

    <tr>
      <td>
        <h4>Grand Total</h4>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="numberFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalBillingCount" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="numberFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalCancelCount" />
            </xsl:with-param>
          </xsl:call-template>

        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalBillingCount + $totalCancelCount" />
            </xsl:with-param>
          </xsl:call-template>

        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalESCPremium" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalESCCommission" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalINSPremium" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalINSCommission" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalUpgrade" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="grandTotal">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalPhoneSupport" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
    </tr>

    <tr>
      <td colspan="4"/>
      <td>
        <span class="grandGrandTotal">
          <xsl:attribute name="id">
            <xsl:text>total_of_</xsl:text>
            <xsl:value-of select="@value" />
          </xsl:attribute>
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$totalESCPremium + $totalINSPremium + $totalUpgrade + $totalPhoneSupport - $totalESCCommission - $totalINSCommission" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
    </tr>


  </xsl:template>

  <xsl:template name="grandTotalPivot">
    <xsl:variable name="totalESCPremium">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeESC]/Product/PremiumAmount)" />
    </xsl:variable>
    <xsl:variable name="totalESCCommission">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeESC]/Product/CommissionAmount)" />
    </xsl:variable>
    <xsl:variable name="totalINSPremium">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeINS]/Product/PremiumAmount)" />
    </xsl:variable>
    <xsl:variable name="totalINSCommission">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeINS]/Product/CommissionAmount)" />
    </xsl:variable>
    <xsl:variable name="totalUpgrade">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/AncillaryAmount)" />
    </xsl:variable>
    <xsl:variable name="totalPhoneSupport">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/OtherAmount)" />
    </xsl:variable>

    <xsl:value-of select="$totalESCPremium + $totalINSPremium +$totalUpgrade + $totalPhoneSupport - $totalESCCommission - $totalINSCommission" />
  </xsl:template>

  <xsl:template name="grandTotal">

    <xsl:variable name="tot_assure_conn_esc">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeESC]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_assure_conn_ins">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeINS]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_esc">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeESC]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_ins">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeINS]/Product/PremiumAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_upgrade">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/AncillaryAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_phone_support">
      <xsl:value-of select="sum(//Items/Group/LossType[@value=$lossTypeJUMP]/Product/OtherAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_group_a">
      <xsl:value-of select="$tot_assure_conn_esc + $tot_assure_conn_ins + $tot_tip_esc + $tot_tip_ins + $tot_upgrade + $tot_phone_support" />
    </xsl:variable>

    <xsl:variable name="tot_esc_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_AC']/LossType[@value=$lossTypeESC]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_esc_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeESC]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_tip_ins_fee">
      <xsl:value-of select="sum(//Items/Group[@value='AFAPT_TIP']/LossType[@value=$lossTypeINS]/Product/CommissionAmount/.)" />
    </xsl:variable>
    <xsl:variable name="tot_group_b">
      <xsl:value-of select="$tot_esc_fee + $tot_tip_esc_fee + $tot_tip_ins_fee" />
    </xsl:variable>

    <xsl:value-of select="$tot_group_a - $tot_group_b" />

  </xsl:template>

  <xsl:template name="linesByGroup">
    <xsl:param name="position"/>
    <xsl:param name="description"/>
    <xsl:param name="amount"/>
    <tr>
      <td>
        <xsl:value-of select="$position" />
      </td>
      <td>
        <span class="title">
          <xsl:value-of select="$description" />
        </span>
      </td>
      <td>
        <span class="number">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$amount" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
      <td>
        <span class="number">
          <xsl:call-template name="moneyFormated">
            <xsl:with-param name="value">
              <xsl:value-of select="$amount" />
            </xsl:with-param>
          </xsl:call-template>
        </span>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="moneyFormated">
    <xsl:param name="value"/>
    <xsl:choose>
      <xsl:when test="number($value) >= 0">
        <xsl:value-of select="format-number($value,'###,##0.00')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="style">
          <xsl:text>color:red</xsl:text>
        </xsl:attribute>
        <xsl:text>(</xsl:text>
        <xsl:value-of select="format-number(-1*number($value),'###,##0.00')" />
        <xsl:text>)</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="numberFormated">
    <xsl:param name="value"/>
    <xsl:choose>
      <xsl:when test="number($value) >= 0">
        <xsl:value-of select="format-number($value,'###,##0')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="style">
          <xsl:text>color:red</xsl:text>
        </xsl:attribute>
        <xsl:text>(</xsl:text>
        <xsl:value-of select="format-number(-1*number($value),'###,##0')" />
        <xsl:text>)</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:output method="text" encoding="utf-8" />
  <xsl:variable name="delimiter" select="','" />
  <xsl:variable name="quot">"</xsl:variable>
  <xsl:variable name="newline"><xsl:text>&lt;br/&gt;</xsl:text></xsl:variable>

  <xsl:key name="NYTiers" match="Group[@nyProd='Y']/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="NoneNYTiers" match="Group[@nyProd='N']/Group/Group[@level='3']" use="@value"/>
  <xsl:key name="NYRCTiers" match="Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']" use="@value"/>
  <xsl:key name="NoneNYRCTiers" match="Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']" use="@value"/>
  <xsl:template match="/">
    <csv>
          <row>
            <field>Invoice - AssureConnection</field >
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>              
            </field >
          </row>
          <row>
            <field>The Signal</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>676 E.Swedesford Road</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Invoice #:</field>
            <xsl:value-of select="$delimiter"/>
            <field>402 - AC<xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" /></field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Suite 300</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field>Invoice Date:</field>
            <xsl:value-of select="$delimiter"/>
            <field>
              <xsl:value-of select="//InvoiceDate" />
            </field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>"Wayne, PA 19087-1631"</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field>Prepared By:</field>
            <xsl:value-of select="$delimiter"/>
            <field>ePrism</field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Phone  610-341-1300    Fax 610-341-8939</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field>Due Date:</field>
            <xsl:value-of select="$delimiter"/>
            <field>
              <xsl:value-of select="//DueDate" />
            </field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>TIN: 22-2623205</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field>Period:</field>
            <xsl:value-of select="$delimiter"/>
            <field>
              <xsl:value-of select="//Period" />
            </field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>              
            </field>
          </row>
          <row>
            <field>Bill/Ship to Name</field>
            <xsl:value-of select="$delimiter"/>
            <field>"T-Mobile USA, Inc."</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field>P.O.Number:</field>
            <xsl:value-of select="$delimiter"/>
            <field>
              <xsl:value-of select="//PONumber" />
            </field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Address:</field>
            <xsl:value-of select="$delimiter"/>
            <field>12920 SE 38th Street</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>City:</field>
            <xsl:value-of select="$delimiter"/>
            <field>Bellevue, WA 98006</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>
            </field>
          </row>
          <row>
            <field>Attn:</field>
            <xsl:value-of select="$delimiter"/>
            <field>Dale Quick</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>
              <xsl:value-of select="$delimiter"/>
            </field>
            <field>dale.quick1@t-mobile.com</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>
              <xsl:value-of select="$newline"/>
            </field>
          </row>
          <row>
            <field class="tdTitle">Product Description</field>
            <xsl:value-of select="$delimiter"/>
            <field>Insurance Code</field>
            <xsl:value-of select="$delimiter"/>
            <field>Values</field>
            <xsl:value-of select="$delimiter"/>
            <field>Total</field>
            <xsl:value-of select="$delimiter"/>
            <field>Sum of Retail Rate</field>
            <xsl:value-of select="$delimiter"/>
            <field>Sum of of T-Mobile Revenue</field>
            <xsl:value-of select="$delimiter"/>
            <field>Sum of Net Premium Due</field>
            <xsl:value-of select="$delimiter"/>
            <field>Sum of INS Prem</field>
            <xsl:value-of select="$delimiter"/>
            <field>Sum of ESC Fee</field>
            <xsl:value-of select="$newline"/>
          </row>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:sort select="./@value" />
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:variable name="vTierPos" select="position()"/>
                <row>
                  <field><xsl:if test="$vInsCodePos = 1 and $vTierPos = 1"><xsl:value-of select="$vProdDesc" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:if test="$vTierPos = 1"><xsl:value-of select="$vInsCode" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field>Sum Of $<xsl:value-of select="./@value" /></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] - ./InvField[@name='ESCFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] - ./InvField[@name='ESCFee'] - ./InvField[@name='INSPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$newline"/>
                </row>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <xsl:for-each select="//Group[@nyProd='N']/Group/Group[@level='3' and generate-id() = generate-id(key('NoneNYTiers',@value)[1])]">
            <xsl:sort select="./@value" data-type="number" />
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCPremSum" select="sum(//Group[@nyProd='N']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCPrem'])"/>
            <xsl:variable name="vTierESCFeeSum" select="sum(//Group[@nyProd='N']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCFee'])"/>
            <xsl:variable name="vTierINSPremSum" select="sum(//Group[@nyProd='N']/Group/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='INSPrem'])"/>
            <row class="trTotal">
              <field>Total Sum Of $<xsl:value-of select="$vnoneNYTier"/></field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number($vTierCount,'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCFeeSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum - $vTierESCFeeSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierINSPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum - $vTierESCFeeSum -$vTierINSPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
          </xsl:for-each>
            <row>
            <field>Grand Totals</field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCPrem']) - sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCPrem']) - sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCFee']) - sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
          <row>
            <field>
              <xsl:value-of select="$newline"/>
            </field>
          </row>
          <row><field>Billing Fee Reclamation</field></row>
            <row>
            <field>Product Description</field>
              <xsl:value-of select="$delimiter"/>
            <field>Insurance Code</field>
              <xsl:value-of select="$delimiter"/>
            <field>Values</field>
              <xsl:value-of select="$delimiter"/>
            <field>Total</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of Retail Rate</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of of T-Mobile Revenue</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of Net Premium Due</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of INS Prem</field>
              <xsl:value-of select="$delimiter"/>
            <field>ESC BF</field>
              <xsl:value-of select="$newline"/>
          </row>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='N']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:for-each select="./Group[@level='2' and @BR='Y']">
              <xsl:sort select="./@value" />
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:variable name="vTierPos" select="position()"/>
                <row>
                  <field><xsl:if test="$vInsCodePos = 1 and $vTierPos = 1"><xsl:value-of select="$vProdDesc" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:if test="$vTierPos = 1"><xsl:value-of select="$vInsCode" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field>Sum Of $<xsl:value-of select="./@value" /></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(./InvField[@name='ReclaimedCnt'],'###,##0')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCPremRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCCommRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCommRC'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSPremRC'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCCommRC'] + ./InvField[@name='INSPremRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$newline"/>
                </row>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <xsl:for-each select="//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3' and generate-id() = generate-id(key('NoneNYRCTiers',@value)[1])]">
            <xsl:sort select="./@value" data-type="number" />
            <xsl:variable name="vnoneNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ReclaimedCnt'])"/>
            <xsl:variable name="vTierESCPremSum" select="sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCPremRC'])"/>            
            <xsl:variable name="vTierESCFeeSum" select="sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='ESCCommRC'])"/>
            <xsl:variable name="vTierINSPremSum" select="sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3' and @value = $vnoneNYTier]/InvField[@name='INSPremRC'])"/>
            <row class="trTotal">
              <field>Total Sum Of $<xsl:value-of select="$vnoneNYTier"/></field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number($vTierCount,'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCPremSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCFeeSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCFeeSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierINSPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCFeeSum + $vTierINSPremSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
          </xsl:for-each>
            <row>
            <field>Grand Totals</field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ReclaimedCnt']),'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCPremRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSPremRC']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='N']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSPremRC']) + sum(//Group[@nyProd='N']/Group/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
           </row>
           <row>
             <field>
               <xsl:value-of select="$newline"/>
               <xsl:value-of select="$newline"/>
             </field>
           </row>
          <row>
            <field>Invoice - WEIP (NY)</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>The Signal</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>676 E.Swedesford Road</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Invoice #:</field>
            <xsl:value-of select="$delimiter"/>
            <field>402 - WEIP<xsl:value-of select="//InvoiceYear" />.<xsl:value-of select="//InvoiceMonth" /></field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Suite 300</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Invoice Date:</field>
            <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="//InvoiceDate" /></field>
            <xsl:value-of select="$newline"/>            
          </row>
          <row>
            <field>"Wayne, PA 19087-1631"</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Prepared By:</field>
            <xsl:value-of select="$delimiter"/>
            <field>ePrism</field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Phone  610-341-1300    Fax 610-341-8939</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Due Date:</field>
            <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="//DueDate" /></field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>TIN: 22-2623205</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>Period:</field>
            <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="//Period" /></field>
            <xsl:value-of select="$newline"/>
           </row> 
          <row>
            <field>
            </field>
          </row>
          <row>
            <field>Bill/Ship to Name</field>
            <xsl:value-of select="$delimiter"/>
            <field>"T-Mobile USA, Inc."</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <field>P.O.Number:</field>            
            <xsl:value-of select="$delimiter"/>
            <field>
              <xsl:value-of select="//PONumber" />
            </field>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>Address:</field>
            <xsl:value-of select="$delimiter"/>
            <field>12920 SE 38th Street</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>City:</field>
            <xsl:value-of select="$delimiter"/>
            <field>Bellevue, WA 98006</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>
            </field>
          </row>
          <row>
            <field>Attn:</field>
            <xsl:value-of select="$delimiter"/>
            <field>Dale Quick</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field><xsl:value-of select="$delimiter"/></field>
            <field>dale.quick1@t-mobile.com</field>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/><xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field><xsl:value-of select="$newline"/></field>
          </row>
            <row>
            <field>Product Description</field>
              <xsl:value-of select="$delimiter"/>
            <field>Insurance Code</field>
              <xsl:value-of select="$delimiter"/>
            <field>Values</field>
              <xsl:value-of select="$delimiter"/>
            <field>Total</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Retail Rate</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Gross INS Prem</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of Gross ESC Fee</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of T-Mobile INS Revenue</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of T-Mobile ESC Revenue</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Premium Paid</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net Premium Due</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net INS Prem</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net ESC Fee</field>
            <xsl:value-of select="$newline"/>
          </row>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='Y']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:variable name="vStandAloneProd" select="./@standaloneProd"/>
            <xsl:for-each select="./Group[@level='2']">
              <xsl:sort select="./@value" />
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:variable name="vTierPos" select="position()"/>
                <row>
                  <field><xsl:if test="$vInsCodePos = 1 and $vTierPos = 1"><xsl:value-of select="$vProdDesc" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:if test="$vTierPos = 1"><xsl:value-of select="$vInsCode" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field>Sum of $<xsl:value-of select="./@value" /></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(./InvField[@name='BillingCnt'],'###,##0')" /><xsl:value-of select="$quot"/>
                  </field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] + ./InvField[@name='INSPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <xsl:choose>
                    <xsl:when test="$vStandAloneProd = 'Y'">
                      <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] + ./InvField[@name='INSPrem'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                      <xsl:value-of select="$delimiter"/>
                      <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCFee'] + ./InvField[@name='INSFee'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                      <xsl:value-of select="$delimiter"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <field>$0.00</field>
                      <xsl:value-of select="$delimiter"/>
                      <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem'] + ./InvField[@name='INSPrem'] - ./InvField[@name='INSFee'] - ./InvField[@name='ESCFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                      <xsl:value-of select="$delimiter"/>
                    </xsl:otherwise>
                  </xsl:choose>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSPrem'] - ./InvField[@name='INSFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCPrem']  - ./InvField[@name='ESCFee'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$newline"/>
                </row>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <xsl:for-each select="//Group[@nyProd='Y']/Group/Group[@level='3' and generate-id() = generate-id(key('NYTiers',@value)[1])]">
            <xsl:sort select="./@value" data-type="number" />
            <xsl:variable name="vNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='BillingCnt'])"/>
            <xsl:variable name="vTierESCPremSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCPrem'])"/>
            <xsl:variable name="vTierINSPremSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSPrem'])"/>
            <xsl:variable name="vTierESCFeeSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCFee'])"/>
            <xsl:variable name="vTierINSFeemSum" select="sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSFee'])"/>
            <xsl:variable name="vTierPremPaid" select="sum(//Group[@nyProd='Y' and @standaloneProd = 'Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCPrem']) + sum(//Group[@nyProd='Y' and @standaloneProd = 'Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSPrem'])"/>
            <xsl:variable name="vTierPremDue" select="sum(//Group[@nyProd='Y' and @standaloneProd = 'N']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCPrem']) + sum(//Group[@nyProd='Y' and @standaloneProd = 'N']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSPrem']) -sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCFee']) -sum(//Group[@nyProd='Y']/Group/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSFee'])"/>
            <row class="trTotal">
              <field>Total Sum Of <xsl:value-of select="$vNYTier"/></field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number($vTierCount,'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum + $vTierINSPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierINSPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierINSFeemSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCFeeSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierPremPaid,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <xsl:choose>
                <xsl:when test="$vTierPremDue >= 0">
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierPremDue,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
                </xsl:when>
                <xsl:otherwise>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierPremDue * -1,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
                </xsl:otherwise>
              </xsl:choose>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierINSPremSum - $vTierINSFeemSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierESCPremSum - $vTierESCFeeSum,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
          </xsl:for-each>
            <row>
            <field>Grand Totals</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='BillingCnt']),'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSPrem']) + sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@value='ESC' or @value='INS']/Group[@level='3']/InvField[@name='ESCPrem']) + sum(//Group[@nyProd='Y']/Group[@value='ESC' or @value='INS']/Group[@level='3']/InvField[@name='INSPrem']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[not(@value='ESC' or @value='INS')]/Group[@level='3']/InvField[@name='ESCPrem']) + sum(//Group[@nyProd='Y']/Group[not(@value='ESC' or @value='INS')]/Group[@level='3']/InvField[@name='INSPrem']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSPrem']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='INSFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCPrem']) - sum(//Group[@nyProd='Y']/Group/Group[@level='3']/InvField[@name='ESCFee']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
          </row>
          <row>
            <field>
              <xsl:value-of select="$newline"/>
            </field>
          </row>
          <row>
            <field>Billing Fee Reclamation</field>
            <xsl:value-of select="$delimiter"/>
            <xsl:value-of select="$newline"/>
          </row>
            <row>
            <field>Product Description</field>
              <xsl:value-of select="$delimiter"/>
            <field>Insurance Code</field>
              <xsl:value-of select="$delimiter"/>
            <field>Values</field>
              <xsl:value-of select="$delimiter"/>
            <field>Total</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Retail Rate</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Gross INS Prem</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum of Gross ESC Fee</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of T-Mobile INS Revenue</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of T-Mobile ESC Revenue</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Premium Paid</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net Premium Due</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net INS Prem</field>
              <xsl:value-of select="$delimiter"/>
            <field>Sum Of Net ESC Fee</field>
              <xsl:value-of select="$newline"/>
          </row>
          <xsl:for-each select="/InvoiceData/Group[@level='1' and @nyProd='Y']">
            <xsl:variable name="vProdDesc" select="./@value"/>
            <xsl:variable name="vStandAloneProd" select="./@standaloneProd"/>
            <xsl:for-each select="./Group[@level='2' and @BR='Y']">
              <xsl:sort select="./@value" />
              <xsl:variable name="vInsCode" select="./@value"/>
              <xsl:variable name="vInsCodePos" select="position()"/>
              <xsl:for-each select="./Group[@level='3']">
                <xsl:variable name="vTierPos" select="position()"/>
                <row>
                  <field><xsl:if test="$vInsCodePos = 1 and $vTierPos = 1"><xsl:value-of select="$vProdDesc" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:if test="$vTierPos = 1"><xsl:value-of select="$vInsCode" /></xsl:if></field>
                  <xsl:value-of select="$delimiter"/>
                  <field>Sum of $<xsl:value-of select="./@value" /></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(./InvField[@name='ReclaimedCnt'],'###,##0')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCPremRC'] + ./InvField[@name='INSPremRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='INSPremRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCPremRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='INSCommRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCCommRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field>$0.00</field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCommRC'] + ./InvField[@name='ESCCommRC'],'###,##0.00')" /><xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>                 
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='INSPremRC'] - ./InvField[@name='INSCommRC'],'###,##0.00')" />) <xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$delimiter"/>
                  <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(./InvField[@name='ESCPremRC'] - ./InvField[@name='ESCCommRC'],'###,##0.00')" />)<xsl:value-of select="$quot"/></field>
                  <xsl:value-of select="$newline"/>
                </row>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
          <xsl:for-each select="//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and generate-id() = generate-id(key('NYRCTiers',@value)[1])]">
            <xsl:sort select="./@value" data-type="number" />
            <xsl:variable name="vNYTier" select="./@value"/>
            <xsl:variable name="vTierCount" select="sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and @value = $vNYTier]/InvField[@name='ReclaimedCnt'])"/>
            <xsl:variable name="vTierESCPremSum" select="sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCPremRC'])"/>
            <xsl:variable name="vTierINSPremSum" select="sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSPremRC'])"/>
            <xsl:variable name="vTierESCFeeSum" select="sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and @value = $vNYTier]/InvField[@name='ESCCommRC'])"/>
            <xsl:variable name="vTierINSFeemSum" select="sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3' and @value = $vNYTier]/InvField[@name='INSCommRC'])"/>
            <xsl:variable name="vTierPremDue" select="$vTierESCFeeSum + $vTierINSFeemSum"/>
            <row>
              <field>Total Sum Of <xsl:value-of select="$vNYTier"/></field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number($vTierCount,'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCPremSum + $vTierINSPremSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierINSPremSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCPremSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierINSFeemSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCFeeSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field>$0.00</field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number($vTierPremDue,'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>              
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierINSPremSum - $vTierINSFeemSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
              <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number($vTierESCPremSum - $vTierESCFeeSum,'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
          </xsl:for-each>
            <row>
            <field>Grand Totals</field>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/><xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ReclaimedCnt']),'###,##0')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCPremRC']) + sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSPremRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSPremRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCPremRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field>$0.00</field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSCommRC']) + sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/><xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSPremRC']) - sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='INSCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$delimiter"/>
            <field><xsl:value-of select="$quot"/>($<xsl:value-of select="format-number(sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCPremRC']) - sum(//Group[@nyProd='Y']/Group[@BR='Y']/Group[@level='3']/InvField[@name='ESCCommRC']),'###,##0.00')"/>)<xsl:value-of select="$quot"/></field>
              <xsl:value-of select="$newline"/>
            </row>
    </csv>
  </xsl:template>
</xsl:stylesheet>
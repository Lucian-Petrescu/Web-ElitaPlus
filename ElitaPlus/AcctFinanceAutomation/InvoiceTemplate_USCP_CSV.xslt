<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:output method="text" encoding="utf-8" />
  <xsl:variable name="delimiter" select="','" />
  <xsl:variable name="quot">"</xsl:variable>
  <xsl:variable name="newline"><xsl:text>&lt;br/&gt;</xsl:text>
  <!--<xsl:text>&#xa;</xsl:text>-->
  </xsl:variable>
  
  <xsl:template match="/">
    <csv>
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
      <row>
        <field>Premium Detail</field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <xsl:value-of select="$newline"/>
      </row>
      <row>
        <field>Billing Date</field>
        <xsl:value-of select="$delimiter"/>
        <field>Billing Reason</field>
        <xsl:value-of select="$delimiter"/>
        <field>Reg State</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Premium</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Premium</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Count</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Count</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Claims Expense</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Risk Fee (5%)</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Enroll Claims Admin (ECA)</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Taxes and Assessments</field>
        <xsl:value-of select="$delimiter"/>
        <field>ESC Customer Notifications</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Claims Expense</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Risk Fee (5%)</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Enroll Claims Admin (ECA)</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Taxes and Assessments</field>
        <xsl:value-of select="$delimiter"/>
        <field>INS Customer Notifications</field>
        <xsl:value-of select="$delimiter"/>
        <field>Total Active Subscribers</field>
        <xsl:value-of select="$delimiter"/>
        <field>Total Cancelled Subscribers</field>
        <xsl:value-of select="$delimiter"/>
        <field>Total Subscribers</field>
        <xsl:value-of select="$delimiter"/>
        <field>Total ESC Premium</field>
        <xsl:value-of select="$delimiter"/>
        <field>Total INS Premium</field>
        <xsl:value-of select="$newline"/>
      </row>

      <xsl:for-each select="/InvoiceData/Group[@level='1']">
        <xsl:sort select="./@value"/>
        <xsl:variable name="vBillingDate" select="./@value"/>
        <xsl:variable name="vESCPrem" select="./@ESCPrem"/>
        <xsl:variable name="vINSPrem" select="./@INSPrem"/>
        <xsl:variable name="vESCClaimExp" select="./@ESCClaimExp"/>
        <xsl:variable name="vESCClaimExpFormatted" select="format-number(./InvField[@name='ESCCount'] * ./@ESCClaimExp,'###,##0.00')"/>
        <xsl:variable name="vESCRiskFee" select="./@ESCRiskFee"/>
        <xsl:variable name="vESCTax" select="./@ESCTax"/>
        <xsl:variable name="vESCAdmin" select="./@ESCAdmin"/>
        <xsl:variable name="vESCCustNoti" select="./@ESCCustNoti"/>
        <xsl:variable name="vINSClaimExp" select="./@INSClaimExp"/>
        <xsl:variable name="vINSRiskFee" select="./@INSRiskFee"/>
        <xsl:variable name="vINSTax" select="./@INSTax"/>
        <xsl:variable name="vINSAdmin" select="./@INSAdmin"/>
        <xsl:variable name="vINSCustNoti" select="./@INSCustNoti"/>
        <xsl:for-each select="./Group[@level='2']">
          <xsl:sort select="./@value"/>
          <xsl:variable name="vRegState" select="./@value"/>
          <xsl:for-each select="./Group[@level='3']">
            <xsl:sort select="./@value"/>
            <row>
              <field >
                <xsl:value-of select="$vBillingDate" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:choose>
                  <xsl:when test="./@value = 'NULL'"></xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="./@value" />
                  </xsl:otherwise>
                </xsl:choose>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$vRegState" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>
                <xsl:choose>
                  <xsl:when test="./InvField[@name='ESCCount'] = 0">$0.00</xsl:when>
                  <xsl:otherwise>
                    $<xsl:value-of select="format-number($vESCPrem,'###,##0.00')" />
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>
                <xsl:choose>
                  <xsl:when test="./InvField[@name='INSCount'] = 0">$0.00</xsl:when>
                  <xsl:otherwise>
                    $<xsl:value-of select="format-number($vINSPrem,'###,##0.00')" />
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="./InvField[@name='ESCCount']" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="./InvField[@name='INSCount']" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCClaimExp,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCRiskFee,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCAdmin,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCTax,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCCustNoti,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSClaimExp,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSRiskFee,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSAdmin,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSTax,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSCustNoti,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="./InvField[@name='ActivCount']" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="./InvField[@name='CancelledCount']" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="./InvField[@name='ActivCount'] + ./InvField[@name='CancelledCount']" />
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='ESCCount'] * $vESCPrem,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$delimiter"/>
              <field >
                <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(./InvField[@name='INSCount'] * $vINSPrem,'###,##0.00')" /><xsl:value-of select="$quot"/>
              </field>
              <xsl:value-of select="$newline"/>
            </row>
          </xsl:for-each>
        </xsl:for-each>
      </xsl:for-each>

      <row>
        <field >Total</field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$delimiter"/>
        </field>
        <field >
          <xsl:value-of select="$quot"/>
          <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCCount),'###,##0')"/>
          <xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>
          <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSCount),'###,##0')"/>
          <xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCClaimExp),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCRiskFee),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCAdmin),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCTax),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCCustNoti),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSClaimExp),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSRiskFee),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSAdmin),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSTax),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSCustNoti),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>
          <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ActivCount),'###,##0')"/>
          <xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>
          <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/CancelledCount),'###,##0')"/>
          <xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>
          <xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ActivCount) + sum(msxsl:node-set($vTotal)/CancelledCount),'###,##0')"/>
          <xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/ESCPrem),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
        <xsl:value-of select="$delimiter"/>
        <field >
          <xsl:value-of select="$quot"/>$<xsl:value-of select="format-number(sum(msxsl:node-set($vTotal)/INSPrem),'###,##0.00')"/><xsl:value-of select="$quot"/>
        </field>
      </row>
    </csv>
  </xsl:template>
</xsl:stylesheet>
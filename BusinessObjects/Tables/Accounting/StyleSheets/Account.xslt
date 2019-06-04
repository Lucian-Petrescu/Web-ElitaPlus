<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="xml" encoding="UTF-8" indent="yes" />
  <xsl:template match="/">
    <SSC>
      <SunSystemsContext>
        <BusinessUnit>
          <xsl:value-of select="a:SSC/a:SunSystemsContext/a:BUSINESSUNIT"/>
        </BusinessUnit>
      </SunSystemsContext>
      <Payload>
        <xsl:for-each select="a:SSC/a:VENDOR">
          <Accounts>
            <AccountCode>
              <xsl:value-of select="a:ACCOUNTCODE"/>
            </AccountCode>
            <xsl:if test="a:ACCOUNTTYPE">
              <AccountType>
                <xsl:value-of select="a:ACCOUNTTYPE"/>
              </AccountType>
            </xsl:if>
            <xsl:if test="a:BALANCETYPE">
              <BalanceType>
                <xsl:value-of select="a:BALANCETYPE"/>
              </BalanceType>
            </xsl:if>
            <xsl:if test="a:DATAACCESSGROUPCODE">
              <DataAccessGroupCode>
                <xsl:value-of select="a:DATAACCESSGROUPCODE"/>
              </DataAccessGroupCode>
            </xsl:if>
            <xsl:if test="a:DEFAULTCURRENCYCODE">
              <DefaultCurrencyCode>
                <xsl:value-of select="a:DEFAULTCURRENCYCODE"/>
              </DefaultCurrencyCode>
            </xsl:if>
            <xsl:if test="a:CONVERSIONCODECONTROL">
              <ConversionCodeControl>
                <xsl:value-of select="a:CONVERSIONCODECONTROL"/>
              </ConversionCodeControl>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS1">
              <EnterAnalysis1>
                <xsl:value-of select="a:ENTERANALYSIS1"/>
              </EnterAnalysis1>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS2">
              <EnterAnalysis2>
                <xsl:value-of select="a:ENTERANALYSIS2"/>
              </EnterAnalysis2>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS3">
              <EnterAnalysis3>
                <xsl:value-of select="a:ENTERANALYSIS3"/>
              </EnterAnalysis3>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS4">
              <EnterAnalysis4>
                <xsl:value-of select="a:ENTERANALYSIS4"/>
              </EnterAnalysis4>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS5">
              <EnterAnalysis5>
                <xsl:value-of select="a:ENTERANALYSIS5"/>
              </EnterAnalysis5>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS6">
              <EnterAnalysis6>
                <xsl:value-of select="a:ENTERANALYSIS6"/>
              </EnterAnalysis6>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS7">
              <EnterAnalysis7>
                <xsl:value-of select="a:ENTERANALYSIS7"/>
              </EnterAnalysis7>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS8">
              <EnterAnalysis8>
                <xsl:value-of select="a:ENTERANALYSIS8"/>
              </EnterAnalysis8>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS9">
              <EnterAnalysis9>
                <xsl:value-of select="a:ENTERANALYSIS9"/>
              </EnterAnalysis9>
            </xsl:if>
            <xsl:if test="a:ENTERANALYSIS10">
              <EnterAnalysis10>
                <xsl:value-of select="a:ENTERANALYSIS10"/>
              </EnterAnalysis10>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="a:BENEFICIARY">
                <LongDescription>
                  <xsl:value-of select="substring(a:BENEFICIARY, 1, 200)"/>
                </LongDescription>
                <Description>
                  <xsl:value-of select="substring(a:BENEFICIARY, 1, 50)"/>
                </Description>
              </xsl:when>
              <xsl:otherwise>
                <xsl:if test="a:LONGDESCRIPTION">
                  <LongDescription>
                    <xsl:value-of select="substring(a:LONGDESCRIPTION, 1, 200)"/>
                  </LongDescription>
                  <Description>
                    <xsl:value-of select="substring(a:LONGDESCRIPTION, 1, 50)"/>
                  </Description>
                </xsl:if>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:if test="a:SUPPLIERLOOKUPCODE">
              <LookupCode>
                <xsl:value-of select="a:SUPPLIERLOOKUPCODE"/>
              </LookupCode>
            </xsl:if>
            <xsl:if test="a:REPORTCONVERSIONCONTROL">
              <ReportConversionControl>
                <xsl:value-of select="a:REPORTCONVERSIONCONTROL"/>
              </ReportConversionControl>
            </xsl:if>
            <xsl:if test="a:SHORTHEADING">
              <ShortHeading>
                <xsl:choose>
                  <xsl:when test="a:ACCOUNTTYPE = 0" >
                    <xsl:text>DEBTOR</xsl:text>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:text>CREDITOR</xsl:text>
                  </xsl:otherwise>
                </xsl:choose>
              </ShortHeading>
            </xsl:if>
            <xsl:if test="a:STATUS">
              <Status>
                <xsl:value-of select="a:STATUS"/>
              </Status>
            </xsl:if>
            <xsl:if test="a:USERAREA">
              <UserArea>
                <xsl:value-of select="a:USERAREA"/>
              </UserArea>
            </xsl:if>
            <xsl:if test="a:SUPPRESSREVALUATION">
              <SuppressRevaluation>
                <xsl:value-of select="a:SUPPRESSREVALUATION"/>
              </SuppressRevaluation>
            </xsl:if>
            <xsl:if test="a:PAYASPAIDACCOUNTTYPE">
              <PayAsPaidAccountType>
                <xsl:value-of select="a:PAYASPAIDACCOUNTTYPE"/>
              </PayAsPaidAccountType>
            </xsl:if>
            <xsl:if test="a:ANALYSIS1">
              <Analysis1>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS1"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis1>
            </xsl:if>
            <xsl:if test="a:ANALYSIS2">
              <Analysis2>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS2"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis2>
            </xsl:if>
            <xsl:if test="a:ANALYSIS3">
              <Analysis3>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS3"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis3>
            </xsl:if>
            <xsl:if test="a:ANALYSIS4">
              <Analysis4>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS4"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis4>
            </xsl:if>
            <xsl:if test="a:ANALYSIS5">
              <Analysis5>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS5"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis5>
            </xsl:if>
            <xsl:if test="a:ANALYSIS6">
              <Analysis6>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS6"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis6>
            </xsl:if>
            <xsl:if test="a:ANALYSIS7">
              <Analysis7>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS7"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis7>
            </xsl:if>
            <xsl:if test="a:ANALYSIS8">
              <Analysis8>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS8"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis8>
            </xsl:if>
            <xsl:if test="a:ANALYSIS9">
              <Analysis9>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS9"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis9>
            </xsl:if>
            <xsl:if test="a:ANALYSIS10">
              <Analysis10>
                <VAcntCatAnalysis_AcntCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </VAcntCatAnalysis_AcntCode>
                <VAcntCatAnalysis_AnlCode>
                  <xsl:value-of select="a:ANALYSIS10"/>
                </VAcntCatAnalysis_AnlCode>
              </Analysis10>
            </xsl:if>
          </Accounts>
        </xsl:for-each>
      </Payload>
    </SSC>
  </xsl:template>

</xsl:stylesheet>
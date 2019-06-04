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
          <Address>
            <AddressCode>
              <xsl:value-of select="a:ACCOUNTCODE"/>
            </AddressCode>
            <xsl:if test="a:ADDRESSLINE1">
              <AddressLine1>
                <xsl:value-of select="a:ADDRESSLINE1"/>
              </AddressLine1>
            </xsl:if>
            <xsl:if test="a:ADDRESSLINE2">
              <AddressLine2>
                <xsl:value-of select="a:ADDRESSLINE2"/>
              </AddressLine2>
            </xsl:if>
            <xsl:if test="a:AREA">
              <Area>
                <xsl:value-of select="a:AREA"/>
              </Area>
            </xsl:if>
            <xsl:if test="a:COUNTRY">
              <Country>
                <xsl:value-of select="a:COUNTRY"/>
              </Country>
            </xsl:if>
            <xsl:if test="a:COUNTRYCODE">
              <CountryCode>
                <xsl:value-of select="a:COUNTRYCODE"/>
              </CountryCode>
            </xsl:if>
            <xsl:if test="a:ADDRESSLOOKUPCODE">
              <LookupCode>
                <xsl:value-of select="a:ADDRESSLOOKUPCODE"/>
              </LookupCode>
            </xsl:if>
            <xsl:if test="a:POSTALCODE">
              <PostalCode>
                <xsl:value-of select="a:POSTALCODE"/>
              </PostalCode>
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
            <xsl:if test="a:STATE">
              <State>
                <xsl:value-of select="a:STATE"/>
              </State>
            </xsl:if>
            <xsl:if test="a:STATECODE">
              <StateCode>
                <xsl:value-of select="a:STATECODE"/>
              </StateCode>
            </xsl:if>
            <xsl:if test="a:TOWNCITY">
              <TownCity>
                <xsl:value-of select="a:TOWNCITY"/>
              </TownCity>
            </xsl:if>
            <xsl:if test="a:ADDRESSSTATUS">
              <Status>
                <xsl:value-of select="a:ADDRESSSTATUS"/>
              </Status>
            </xsl:if>
            <xsl:if test="a:TELEPHONENUMBER">
              <TelePhoneNumber>
                <xsl:value-of select="a:TELEPHONENUMBER"/>
              </TelePhoneNumber>
            </xsl:if>
            <xsl:if test="a:FAXNUMBER">
              <FaxNumber>
                <xsl:value-of select="a:FAXNUMBER"/>
              </FaxNumber>
            </xsl:if>
            <xsl:if test="a:WEBPAGEADDRESS">
              <WebPageAddress>
                <xsl:value-of select="a:WEBPAGEADDRESS"/>
              </WebPageAddress>
            </xsl:if>
            <xsl:if test="a:TAXIDENTIFICATIONNUMBER">
              <Tax>
                <AddressCode>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </AddressCode>
                <SequenceNumber>
                  <xsl:text>1</xsl:text>
                </SequenceNumber>
                <xsl:if test="a:TAXIDENTIFICATIONNUMBER">
                  <TaxIdentificationCode>
                    <xsl:value-of select="a:TAXIDENTIFICATIONNUMBER"/>
                  </TaxIdentificationCode>
                </xsl:if>
              </Tax>
            </xsl:if>
          </Address>
        </xsl:for-each>
      </Payload>
    </SSC>
  </xsl:template>
</xsl:stylesheet>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <!--<xsl:output method="text" encoding="UTF-8" />-->
  <xsl:output method="xml" encoding="UTF-8" indent="yes"  />
  <xsl:template match="/">
    <xsl:element name="SmartStreamVendor">
      <xsl:for-each select="a:SSC/a:VMST">
        <xsl:if test='a:PAYMENT_TO_CUSTOMER = "Y"'>
          <xsl:element name="Vendor">
            <xsl:element name="User_ID">
              <xsl:text>ELITA</xsl:text>
            </xsl:element>
            <xsl:element name="Vendor_ID">
              <xsl:choose>
                <xsl:when test='a:PAYMENT_TO_CUSTOMER = "Y"'>
                  <xsl:text>IC</xsl:text>
                  <xsl:choose>
                    <xsl:when test='a:GENERALDESCRIPTION24 = "REFUNDS"'>
                      <xsl:text>R</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>C</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:when>
              </xsl:choose>
              <xsl:value-of select="a:VENDOR_ID"/>
            </xsl:element>
            <xsl:element name="Location_Code">
              <xsl:text>000</xsl:text>
            </xsl:element>
            <xsl:element name="Vendor_Type">
              <xsl:text>New</xsl:text>
            </xsl:element>
            <xsl:element name="Payment_Method">
              <xsl:value-of select="a:PAYMENT_METHOD"/>
            </xsl:element>
            <xsl:element name="Vendor_Name">
              <xsl:value-of select="a:PAYEE"/>
            </xsl:element>
            <xsl:element name="Legal_Name">
              <xsl:value-of select="a:PAYEE"/>
            </xsl:element>
            <xsl:element name="Transaction_Currency">
              <xsl:value-of select="a:CURRENCYCODE"/>
            </xsl:element>
            <xsl:element name="Address_Type">
              <xsl:text>R</xsl:text>
            </xsl:element>
            <xsl:choose>
              <xsl:when test="substring(a:COMPANY_CODE,2,2) = 'CA'">
                <xsl:element name="Address_Format">
                  <xsl:text>CA3</xsl:text>
                </xsl:element>
              </xsl:when>
              <xsl:otherwise>
                <xsl:element name="Address_Format">
                  <xsl:text>US3</xsl:text>
                </xsl:element>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:element name="Address_Line_1">
            </xsl:element>
            <xsl:element name="Address_Line_2">
            </xsl:element>
            <xsl:element name="Street_POBox_1">
              <xsl:value-of select="a:GENERALDESCRIPTION1"/>
            </xsl:element>
            <xsl:element name="Street_POBox_2">
              <xsl:value-of select="a:GENERALDESCRIPTION2"/>
            </xsl:element>
            <xsl:element name="City">
              <xsl:value-of select="a:CITY"/>
            </xsl:element>
            <xsl:element name="State">
              <xsl:value-of select="a:REGION"/>
            </xsl:element>
            <xsl:element name="Zip_Code">
              <xsl:value-of select="a:ZIP"/>
            </xsl:element>
            <xsl:element name="Country_Code">
              <xsl:value-of select="a:COUNTRY"/>
            </xsl:element>
            <xsl:element name="Terms_Policy">
            </xsl:element>
            <xsl:element name="Tax_ID">
            </xsl:element>
            <xsl:element name="Earns_Wh_Rule">
            </xsl:element>
            <xsl:element name="Earns_Wh_Category">
            </xsl:element>
            <xsl:element name="Withholding_Option">
            </xsl:element>
            <xsl:element name="Payment_Type">
            </xsl:element>
            <xsl:element name="Bank_Account_Number">
            </xsl:element>
            <xsl:element name="Bank_Name">
            </xsl:element>
            <xsl:element name="Bank_Number">
            </xsl:element>
            <xsl:element name="Branch_Name">
            </xsl:element>
            <xsl:element name="Bank_Country_Code">
            </xsl:element>
          </xsl:element>
        </xsl:if>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"  />
  <xsl:template match="/">
    <SmartStreamInvoice>
      <xsl:for-each select="a:SSC/a:AP_Item">
        <xsl:if test="a:TRANSACTIONAMOUNT != 0">
          <xsl:element name="Invoice">
            <User_ID>ELITA</User_ID>
            <Location_Code>000</Location_Code>
            <Invoice_Number>
              <xsl:choose>
                <xsl:when test="a:TRANSACTIONREFERENCE">
                  <xsl:value-of select="a:TRANSACTIONREFERENCE"/>
                  <xsl:text>_</xsl:text>
                  <xsl:if test="string-length(a:TRANSACTIONREFERENCE) &lt;= 14">
                    <xsl:value-of select="a:JOURNALSEQUENCE"/>
                  </xsl:if>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="a:GENERALDESCRIPTION24"/>
                </xsl:otherwise>
              </xsl:choose>
            </Invoice_Number>
            <Invoice_Date>
              <xsl:value-of select="substring(a:TRANSACTIONDATE,5,4)"/>
              <xsl:text>-</xsl:text>
              <xsl:value-of select="substring(a:TRANSACTIONDATE,3,2)"/>
              <xsl:text>-</xsl:text>
              <xsl:value-of select="substring(a:TRANSACTIONDATE,1,2)"/>
            </Invoice_Date>
            <Payment_Method>
              <xsl:value-of select="a:PAYMENT_METHOD"/>
            </Payment_Method>
            <Payable_Entity>
              <xsl:value-of select="a:CONTROL_GROUP"/>
            </Payable_Entity>
            <Line_Number>
              <xsl:value-of select="a:LINE_NUM"/>
            </Line_Number>
            <Line_Description>
              <xsl:value-of select="a:DESCRIPTION"/>
            </Line_Description>
            <Line_Amount>
              <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
            </Line_Amount>
            <Entity_Number>
              <xsl:value-of select="a:JOURNALTYPE"/>
            </Entity_Number>
            <Account_Number>
              <xsl:choose>
                <xsl:when test="a:COMPANY_CODE = 'ACA'">
                  <xsl:choose>
                    <xsl:when test="a:ORIGINALACCOUNTCODE">
                      <xsl:value-of select="a:ORIGINALACCOUNTCODE"/>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="a:ACCOUNTCODE"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="a:ACCOUNTCODE"/>
                </xsl:otherwise>
              </xsl:choose>
            </Account_Number>
            <Cost_Center>
              <xsl:value-of select="a:ANALYSISCODE1"/>
            </Cost_Center>
            <LOB>
              <xsl:value-of select="a:ANALYSISCODE2"/>
            </LOB>
            <Business_Primary>
              <xsl:value-of select="a:ANALYSISCODE3"/>
            </Business_Primary>
            <Business_Secondary>
              <xsl:value-of select="a:ANALYSISCODE4"/>
            </Business_Secondary>
            <Business_Type>
              <xsl:value-of select="a:ANALYSISCODE5"/>
            </Business_Type>
            <Block_Business>
              <xsl:value-of select="a:ANALYSISCODE6"/>
            </Block_Business>
            <Distribution_Channel>
              <xsl:value-of select="a:ANALYSISCODE7"/>
            </Distribution_Channel>
            <Invoice_Description>
              <xsl:choose>
                <xsl:when test="a:TRANSACTIONREFERENCE">
                  <xsl:value-of select="a:TRANSACTIONREFERENCE"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="a:GENERALDESCRIPTION24"/>
                </xsl:otherwise>
              </xsl:choose>
            </Invoice_Description>
            <xsl:if test='a:GENERALDESCRIPTION24 = "REFUNDS"'>
              <Remittance_Message>
                <xsl:text>#</xsl:text>
                <xsl:value-of select="a:CERTIFICATE"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="a:DEALER_NAME"/>
              </Remittance_Message>
            </xsl:if>
            <OneTime_Vendor>
              <xsl:value-of select="a:PAYMENT_TO_CUSTOMER"/>
            </OneTime_Vendor>
            <Vendor_ID>
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
            </Vendor_ID>
            <xsl:if test='a:PAYMENT_TO_CUSTOMER = "Y"'>
              <Vendor_Name>
                <xsl:value-of select="a:GENERALDESCRIPTION9"/>
              </Vendor_Name>
              <Address_Line_1>
                <xsl:value-of select="a:GENERALDESCRIPTION1"/>
              </Address_Line_1>
              <Address_Line_2>
                <xsl:value-of select="a:GENERALDESCRIPTION2"/>
              </Address_Line_2>
              <City>
                <xsl:value-of select="a:CITY"/>
              </City>
              <State>
                <xsl:value-of select="a:REGION"/>
              </State>
              <ZipCode>
                <xsl:value-of select="a:ZIP"/>
              </ZipCode>
            </xsl:if>
          </xsl:element>
        </xsl:if>
      </xsl:for-each>
    </SmartStreamInvoice>
  </xsl:template>
</xsl:stylesheet>
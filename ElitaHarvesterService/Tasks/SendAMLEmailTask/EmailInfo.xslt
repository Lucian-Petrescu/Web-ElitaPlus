<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
     <xsl:template match="/">
          <html>
            <head>
              <style>
                td {FONT-FAMILY: arial;font-size:12px;}
                .pageTitle { FONT-WEIGHT: bold; FONT-SIZE: 20px; TEXT-ALIGN: left;}
                .title { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: center; }
                .tblEmailPage{border:2px solid #000;}
                .tblEmailPage td{padding:3px;white-space:nowrap;text-align:center;}
                .tdTitle {FONT-WEIGHT: bold; FONT-SIZE:14px; TEXT-ALIGN:left; border-top:1px solid black; border-bottom:1px solid black;}
                .tdBorder {border-bottom:1px solid black;padding-left:0px;}
                .tdBorderTop {border-top:1px solid black;}
                .tdBorderBottom {border-bottom:1px solid black;}
                .tdBorderLeft {border-left:1px solid black;}
                .trTotal {background-color:#FFFF00;)
              </style>
            </head>
            <body>
              <table class="tblEmailPage" cellpadding="0" cellspacing="0">
                <tr>
                  <td colspan="2" class="pageTitle" style="text-align:center;">AML - Action Required</td>
                  <td style="width:150px;">
                    <xsl:text disable-output-escaping="yes">&amp;</xsl:text>nbsp;
                  </td>              
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Customer Tax ID: </td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//TaxId" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Customer First Name: </td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//CustomerFirstName" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Customer Last Name: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//CustomerLastName" />
                  </td>
                </tr>
               <tr>
                  <td class="title" style="text-align:left;">Nationality: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Nationality" />
                  </td>
                </tr>
               <tr>
                  <td class="title" style="text-align:left;">Gender: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Gender" />
                  </td>
               </tr>
                <tr>
                  <td class="title" style="text-align:left;">Address1: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Address1" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Address2: </td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Address2" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Address3: </td>
                  <td style="text-align:left;">
                    <xsl:value-of select="//Address3" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">City: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//City" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Postal Code: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Postal_code" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Claim Number: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Claim_Number" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Authorized Amount: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Authorized_Amount" />
                  </td>
                </tr>
                <tr>
                  <td class="title" style="text-align:left;">Deductible: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Deductible" />
                  </td>
                </tr>
              <tr>
                  <td class="title" style="text-align:left;">Certificate number: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Cert_Number" />
                  </td>
                </tr>
              <tr>
                  <td class="title" style="text-align:left;">Dealer Name: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//Dealer_Name" />
                  </td>
                </tr>
               <tr>
                  <td class="title" style="text-align:left;">UFI Begin Date: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//UFI_Begin_dt" />
                  </td>
                </tr>
              <tr>
                  <td class="title" style="text-align:left;">UFI End Date: </td>
                     <td style="text-align:left;">
                    <xsl:value-of select="//UFI_End_dt" />
                  </td>
                </tr>
              </table>
              </body>
          </html>
        </xsl:template>
</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt">
    <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <table width="100%" class="formGrid" border ="0">
       <tbody>
         <xsl:for-each select="CustomerDetailDS/CustomerDetail">
           <xsl:if test="/CustomerDetailDS/FlagCheck[FLAG_NAME='CUSTSALUTATIONEXCLUDE']/FLAG_VALUE = 'FALSE'">
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='SALUTATION']/TRANSLATION" />: </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{SALUTATION}" />
                  </td>
                  <td colspan="2"  style="border-bottom:none">
                  </td>
              </tr>
           </xsl:if>
              <tr>
                 <td align="right"  style="border-bottom:none">
                   <span>
                     <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='CUSTOMER_FIRST_NAME']/TRANSLATION" />:
                   </span>
                 </td>
                 <td align="left"  style="border-bottom:none">
                   <input type="text" readonly="readonly" class="medium" value="{CUSTOMER_FIRST_NAME}" />
                 </td>
                 <td align="right"  style="border-bottom:none">
                   <span>
                     <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='CUSTOMER_MIDDLE_NAME']/TRANSLATION" />:
                   </span>
                 </td>
                 <td align="left"  style="border-bottom:none">
                   <input type="text" readonly="readonly" class="medium" value="{CUSTOMER_MIDDLE_NAME}" />
                 </td>
              </tr>
              <tr>
                 <td align="right"  style="border-bottom:none">
                   <span>
                     <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='CUSTOMER_LAST_NAME']/TRANSLATION" />:
                   </span>
                 </td>
                 <td align="left"  style="border-bottom:none">
                   <input type="text" readonly="readonly" class="medium" value="{CUSTOMER_LAST_NAME}" />
                 </td>
                 <td colspan="2"  style="border-bottom:none">
                 </td>
              </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='EMAIL']/TRANSLATION" />: </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="medium" value="{EMAIL}" />
                  </td>
                  <td align="right"  style="border-bottom:none">
                    <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='HOME_PHONE']/TRANSLATION" />: </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{HOME_PHONE}" />
                  </td>
              </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='TAX_ID']/TRANSLATION" />: </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{IDENTIFICATION_NUMBER}"/>
                  </td>
                  <td align="right"  style="border-bottom:none">
                    <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='WORK_PHONE']/TRANSLATION" />: </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{WORK_PHONE}" />
                  </td>
               </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span>
                      <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='ADDRESS1']/TRANSLATION" />:
                    </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="medium" value="{ADDRESS1}" />
                  </td>
                  <td align="right"  style="border-bottom:none">
                    <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='COUNTRY']/TRANSLATION" />:
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="FLATTEXTBOX" value="{COUNTRY}" />
                  </td>
              </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span>
                      <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='ADDRESS2']/TRANSLATION" />:
                    </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="medium" value="{ADDRESS2}" />
                  </td>
                  <td align="right"  style="border-bottom:none">
                    <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='STATE_PROVINCE']/TRANSLATION" />:
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{STATE}" />
                  </td>
              </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span>
                      <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='ADDRESS3']/TRANSLATION" />:
                    </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="medium" value="{ADDRESS3}" />
                  </td>
                  <td align="right"  style="border-bottom:none">
                    <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='CITY']/TRANSLATION" />:
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{CITY}" />
                  </td>
              </tr>
              <tr>
                  <td align="right"  style="border-bottom:none">
                    <span>
                      <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='ZIP']/TRANSLATION" />:
                    </span>
                  </td>
                  <td align="left"  style="border-bottom:none">
                    <input type="text" readonly="readonly" class="small" value="{POSTAL_CODE}" />
                  </td>
                  <td colspan ="2"  style="border-bottom:none">
                  </td>
             </tr>
           <xsl:if test="/CustomerDetailDS/FlagCheck[FLAG_NAME='CUSTINFOEXCLUDE']/FLAG_VALUE = 'FALSE'">
             <tr>
               <td align="right"  style="border-bottom:none">
                 <span>
                   <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='MARITAL_STATUS']/TRANSLATION" />:
                 </span>
               </td>
               <td align="left"  style="border-bottom:none">
                 <input type="text" readonly="readonly" class="medium" value="{MARITAL_STATUS}" />
               </td>
               <td align="right"  style="border-bottom:none">
                 <span>
                   <xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='DATE_OF_BIRTH']/TRANSLATION" />:
                 </span>
               </td>
               <td align="left"  style="border-bottom:none">
                 <input type="text" readonly="readonly" class="FLATTEXTBOX_TAB" value="{DATE_OF_BIRTH}" />
               </td>
             </tr>
             <tr>
                <td align="right"  style="border-bottom:none">
                  <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='NATIONALITY']/TRANSLATION" />: </span>
                </td>
                <td align="left"  style="border-bottom:none">
                  <input type="text" readonly="readonly" class="medium" value="{NATIONALITY}" />
                </td>
                <td align="right"  style="border-bottom:none">
                  <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='PLACE_OF_BIRTH']/TRANSLATION" />: </span>
                </td>
                <td align="left"  style="border-bottom:none">
                  <input type="text" readonly="readonly" class="medium" value="{PLACE_OF_BIRTH}" />
                </td>
             </tr>
             <tr>
                 <td align="right"  style="border-bottom:none">
                   <span><xsl:value-of select="/CustomerDetailDS/Headers[UI_PROG_CODE='GENDER']/TRANSLATION" />: </span>
                 </td>
                 <td align="left"  style="border-bottom:none">
                   <input type="text" readonly="readonly" class="exsmall" value="{GENDER}" />
                 </td>
                 <td colspan="2"  style="border-bottom:none">
                 </td>
             </tr>
           </xsl:if>
          </xsl:for-each>
       </tbody>
    </table>
  </xsl:template>
</xsl:stylesheet>

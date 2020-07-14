<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"  encoding="UTF-8"/>
  <xsl:param name="recordCount" />

  <xsl:template name="formatdate">
    <xsl:param name="DateTimeStr" />
    <xsl:param name="DateFormatStr" />
    <xsl:param name="DateMasking" />
    <xsl:variable name="datestr">
      <xsl:value-of select="substring-before($DateTimeStr,'T')" />
    </xsl:variable>

    
    <xsl:variable name="mm">
      <xsl:value-of select="substring($datestr,6,2)" />
    </xsl:variable>
    <xsl:variable name="mmf">
      <xsl:value-of select="substring($DateFormatStr,3,1)" />
    </xsl:variable>

    <xsl:variable name="dd">
      <xsl:value-of select="substring($datestr,9,2)" />
    </xsl:variable>
    
     <xsl:variable name="ddf">
      <xsl:value-of select="substring($DateFormatStr,5,1)" />
    </xsl:variable>
     

    <xsl:variable name="yyyy">
      <xsl:value-of select="substring($datestr,1,4)" />
    </xsl:variable>
    <xsl:variable name="yyyyf">
      <xsl:value-of select="substring($DateFormatStr,1,1)" />
    </xsl:variable>
    
    
    <xsl:choose>
      
     
    <xsl:when test="$DateMasking = 'Yes'">
                         ****
                           </xsl:when>
                         <xsl:otherwise>
                             <xsl:value-of select="$yyyy"/>
                        </xsl:otherwise>
                        </xsl:choose>
    
    
    <xsl:value-of select="$yyyyf"/>
      <xsl:choose>
      <xsl:when test="$mm = '01'">1</xsl:when>
      <xsl:when test="$mm = '02'">2</xsl:when>
      <xsl:when test="$mm = '03'">3</xsl:when>
      <xsl:when test="$mm = '04'">4</xsl:when>
      <xsl:when test="$mm = '05'">5</xsl:when>
      <xsl:when test="$mm = '06'">6</xsl:when>
      <xsl:when test="$mm = '07'">7</xsl:when>
      <xsl:when test="$mm = '08'">8</xsl:when>
      <xsl:when test="$mm = '09'">9</xsl:when>
      <xsl:when test="$mm = '10'">10</xsl:when>
      <xsl:when test="$mm = '11'">11</xsl:when>
      <xsl:when test="$mm = '12'">12</xsl:when>
    </xsl:choose>
    <xsl:value-of select="$mmf"/>
    <xsl:choose>
      <xsl:when test="$dd = '01'">1</xsl:when>
      <xsl:when test="$dd = '02'">2</xsl:when>
      <xsl:when test="$dd = '03'">3</xsl:when>
      <xsl:when test="$dd = '04'">4</xsl:when>
      <xsl:when test="$dd = '05'">5</xsl:when>
      <xsl:when test="$dd = '06'">6</xsl:when>
      <xsl:when test="$dd = '07'">7</xsl:when>
      <xsl:when test="$dd = '08'">8</xsl:when>
      <xsl:when test="$dd = '09'">9</xsl:when>
      <xsl:when test="$dd = '10'">10</xsl:when>
      <xsl:when test="$dd = '11'">11</xsl:when>
      <xsl:when test="$dd = '12'">12</xsl:when>
      <xsl:when test="$dd = '13'">13</xsl:when>
      <xsl:when test="$dd = '14'">14</xsl:when>
      <xsl:when test="$dd = '14'">15</xsl:when>
      <xsl:when test="$dd = '16'">16</xsl:when>
      <xsl:when test="$dd = '17'">17</xsl:when>
      <xsl:when test="$dd = '18'">18</xsl:when>
      <xsl:when test="$dd = '19'">19</xsl:when>
      <xsl:when test="$dd = '20'">20</xsl:when>
      <xsl:when test="$dd = '21'">21</xsl:when>
      <xsl:when test="$dd = '22'">22</xsl:when>
      <xsl:when test="$dd = '23'">23</xsl:when>
      <xsl:when test="$dd = '24'">24</xsl:when>
      <xsl:when test="$dd = '25'">25</xsl:when>
      <xsl:when test="$dd = '26'">26</xsl:when>
      <xsl:when test="$dd = '27'">27</xsl:when>
      <xsl:when test="$dd = '28'">28</xsl:when>
      <xsl:when test="$dd = '29'">29</xsl:when>
      <xsl:when test="$dd = '30'">30</xsl:when>
      <xsl:when test="$dd = '31'">31</xsl:when>
    
    </xsl:choose>
     <xsl:value-of select="$ddf"/>

  </xsl:template>
  <xsl:template match="/">
    <h2 class="dataGridHeader">Search Results for Solicitations</h2>
    <table width="100%" class="dataGrid" border="0" cellSpacing="0" cellPadding="0">
      <tbody>
        <tr>
          <td align="right" class="bor" colSpan="9">
            <xsl:value-of select="count(ArrayOfSolicitDetails/SolicitDetails)" /> Solicitations found
          </td>
        </tr>
        <xsl:if test="(ceiling(count(//ArrayOfSolicitDetails/SolicitDetails) div $recordCount)) &gt; 1">
          <tr>
            <td colspan="9" class="gridPager" style="text-align:center;">
              <xsl:call-template name="for.loop.pager">
                <xsl:with-param name="i" select="1"></xsl:with-param>
                <xsl:with-param name="count" select="ceiling(count(//ArrayOfSolicitDetail/SolicitDetails) div $recordCount)"></xsl:with-param>
                <xsl:with-param name="pagerPrefix" select='1'></xsl:with-param>
              </xsl:call-template>
            </td>
          </tr>
        </xsl:if>
        <tr>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/INITIAL_SALES_ORDER" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/CUSTOMER_ID" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/CUSTOMER_LAST_NAME" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/CUSTOMER_FIRST_NAME" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/APPLY_DATE_SOLICITATION_DATE" /></a>
          </th >
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/SIM_HOME_PHONE_NUMBER" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/SOURCE" /></a>
          </th>
          <th nowrap="nowrap">
            <a href="#"><xsl:value-of select="ArrayOfSolicitDetails/SolicitDetails/labelTranslation/LEAD_RECORD_STATUS" /></a>
          </th>
        </tr>
        <xsl:for-each select="ArrayOfSolicitDetails/SolicitDetails">
          <tr class="out" onmouseover="this.className='over'" onmouseout="this.className='out'">
            <xsl:attribute name="id">tr_<xsl:value-of select="position()" /></xsl:attribute>
            <xsl:if test="position() &gt; $recordCount">
              <xsl:attribute name="style">display:none</xsl:attribute>
            </xsl:if>
            <!--<td>
              <input type="radio" name="serviceCenterSelection">
                <xsl:attribute name="onClick">
                  SelectServiceCenter('<xsl:value-of select="CODE"/>')
                </xsl:attribute>
              </input>
            </td>-->
            <td nowrap="nowrap">
              <div class="main">
                <xsl:attribute name="onclick">
                  openClose('a<xsl:value-of select="position()" />')
                </xsl:attribute>
                <span style="cursor:pointer">
                  <xsl:attribute name="id">tick_a<xsl:value-of select="position()" /></xsl:attribute>
                  +
                </span>
                <span class="">
                  &#160;<xsl:value-of select="origin/salesOrderNumber" />
                </span>
              </div>
            </td>
            <td nowrap="nowrap">
              <xsl:value-of select="customer/id" />
            </td>
            <td nowrap="nowrap">

              <xsl:value-of select="customer/lastName" />
            </td>
            <td nowrap="nowrap">

              <xsl:value-of select="customer/firstName" />
            </td>
            <td>

               <xsl:call-template name="formatdate">
                          <xsl:with-param name="DateTimeStr"  select="effectiveDate"/>
                          <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>                         
                         </xsl:call-template> 
            </td>
            <td nowrap="nowrap">

              <xsl:value-of select="customer/cellPhoneNumber"/>
            </td>
            <td nowrap="nowrap">

              <xsl:value-of select="creationSourceName"/>
            </td>
            <td nowrap="nowrap">
              <xsl:choose>
                <xsl:when test="status = 'Expired'">
                  <span class="StatInactive">
                    <xsl:value-of select="status"/>
                  </span>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="status"/>
                </xsl:otherwise>
              </xsl:choose>

            </td>
          </tr>
          <tr><xsl:attribute name="id">trd_<xsl:value-of select="position()" /></xsl:attribute>
            <xsl:if test="position() &gt; $recordCount">
              <xsl:attribute name="style">display:none</xsl:attribute>
            </xsl:if>
            <td class="noBor" colspan="9">
              <div class="disNone">
                <xsl:attribute name="id">a<xsl:value-of select="position()" /></xsl:attribute>
                <table class="" width="100%" bgColor="#f2f2f2" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/INITIAL_SALES_ORDER" />:
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/salesOrderNumber"/>
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/CUSTOMER_ID" />:
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/id" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/APPLY_DATE_SOLICITATION_DATE" />:
                      </td>
                      <td nowrap="nowrap">
                         <xsl:call-template name="formatdate">
                          <xsl:with-param name="DateTimeStr"  select="effectiveDate"/>
                          <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>                         
                         </xsl:call-template>               
                       
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/CUSTOMER_LAST_NAME" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/lastName" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/OPEN_DATE_FROM_LEAD_FILE" />:
                      </td>
                      <td nowrap="nowrap">
                        
                          <xsl:choose>
                            <xsl:when test="origin/salesOrderDate != ''">
                               <xsl:call-template name="formatdate">
                                 <xsl:with-param name="DateTimeStr"  select="origin/SalesOrderDate"/>
                                 <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>                         
                               </xsl:call-template>  
                           </xsl:when>
                         
                        </xsl:choose>
                          
                       
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/CUSTOMER_FIRST_NAME" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/firstName" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SOURCE" />:
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="creationSourceName" />
                      </td>

                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/LAST_NAME_KANA" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/lastNameKana" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/LEAD_RECORD_STATUS" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="status" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/FIRST_NAME_KANA" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/firstNameKana" />
                      </td>

                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                         <xsl:choose>
                            <xsl:when test="status = 'Converted'">
                              <xsl:value-of select="labelTranslation/CONVERSION_DATE" /> :
                           </xsl:when>
                         <xsl:otherwise>
                              <xsl:value-of select="labelTranslation/EXPIRATION_DATE" /> :
                        </xsl:otherwise>
                        </xsl:choose>
                        
                      </td>
                      <td nowrap="nowrap">
                         <xsl:choose>
                            <xsl:when test="status = 'Converted'">
                               <xsl:call-template name="formatdate">
                                 <xsl:with-param name="DateTimeStr"  select="conversionDate"/>
                                 <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>                         
                               </xsl:call-template>                               
                             
                           </xsl:when>
                         <xsl:otherwise>
                               <xsl:call-template name="formatdate">
                                 <xsl:with-param name="DateTimeStr"  select="expirationDate"/>
                                 <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>                         
                               </xsl:call-template>                             
                            
                        </xsl:otherwise>
                        </xsl:choose>
                        
                      
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/CUSTOMER_BIRTH_DATE" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:variable name="DateMasking">Yes</xsl:variable>
                       <xsl:call-template name="formatdate">
                         
                                 <xsl:with-param name="DateTimeStr"  select="customer/dateOfBirth"/>
                                 <xsl:with-param name="DateFormatStr"  select="labelTranslation/DATE_SEPARATOR"/>  
                        <xsl:with-param name="DateMasking"  select="$DateMasking"/>
                               </xsl:call-template> 
                     
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SHOP_ID" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/code" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/ADDRESS" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/address/address1" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SALES_CHANNEL" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/channelCode" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/POSTAL_CODE" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/address/zipCode" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SHOP_NAME" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/name" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/E_MAIL" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/emailAddress" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SHOP_ZIP_CODE" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/address/zipCode" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/TEL_MOB_PHONE_NUMBER" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/workPhoneNumber" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SHOP_ADDRESS" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/address/address1" />
                      </td>
                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SIM_HOME_PHONE_NUMBER" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/cellPhoneNumber" />
                      </td>
                                           
                    </tr>
                    <tr class="out">

                      <td align="right" nowrap="nowrap">
                        <xsl:value-of select="labelTranslation/SHOP_TELE_PHONE_NUMBER" /> :
                      </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/workPhoneNumber" />

                      </td>
                      <td></td>
                      <td nowrap="nowrap">
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </td>
          </tr>
        </xsl:for-each>
        <xsl:if test="(ceiling(count(//ArrayOfSolicitDetails/SolicitDetails) div $recordCount)) &gt; 1">
          <tr class="out">
            <td colspan="9" class="gridPager" style="text-align:center;">
              <xsl:call-template name="for.loop.pager">
                <xsl:with-param name="i" select="1"></xsl:with-param>
                <xsl:with-param name="count" select="ceiling(count(//ArrayOfSolicitDetails/SolicitDetails) div $recordCount)"></xsl:with-param>
                <xsl:with-param name="pagerPrefix" select='2'></xsl:with-param>
              </xsl:call-template>
            </td>
          </tr>
        </xsl:if>
      </tbody>
    </table>
  </xsl:template>
  <xsl:template name="for.loop.pager">
    <xsl:param name="i" />
    <xsl:param name="count" />
    <xsl:param name="pagerPrefix" />
    <xsl:if test="$i &lt;= $count">

      <a>
        <xsl:attribute name="id">
          pg<xsl:value-of select="$pagerPrefix" />_<xsl:value-of select="$i"/>
        </xsl:attribute>
        <xsl:attribute name="href">
          javascript:showHidePage(<xsl:value-of select="$i"/>,<xsl:value-of select="count(//ArrayOfSolicitDetails/SolicitDetails)"/>,<xsl:value-of select="$recordCount"/>);
        </xsl:attribute>
        <xsl:value-of select="$i" />
      </a>
      <xsl:call-template name="for.loop.pager">
        <xsl:with-param name="i" select="$i + 1"></xsl:with-param>
        <xsl:with-param name="count" select="$count"></xsl:with-param>
        <xsl:with-param name="pagerPrefix" select="$pagerPrefix"></xsl:with-param>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="recordCount" />
  <xsl:template match="/">
    <h2 class="dataGridHeader">Search Results for Solicit</h2>
    <table width="100%" class="dataGrid" border="0" cellSpacing="0" cellPadding="0">
      <tbody>
        <tr>
          <td align="right" class="bor" colSpan="9">
            <xsl:value-of select="count(ArrayOfSolicitDetails/SolicitDetails)" /> Solicit(s) found
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
          <th>
            <a href="#">Initial Sales Order Number</a>
          </th>
          <th>
            <a href="#">Customer ID</a>
          </th>
          <th>
            <a href="#">Customer Last Name</a>
          </th>
          <th>
            <a href="#">Customer First Name</a>
          </th>
          <th>
            <a href="#">Apply Date/Solicitation Date</a>
          </th>
          <th>
            <a href="#">SIM Phone Number</a>
          </th>
          <th>
            <a href="#">Source</a>
          </th>
          <th>
            <a href="#">Lead record status</a>
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
              
              <xsl:value-of select="effectiveDate" />
            </td>
            <td nowrap="nowrap">
                  
                  <xsl:value-of select="customer/cellPhoneNumber"/>
            </td>
            <td nowrap="nowrap">
              
              <xsl:value-of select="origin/organization/address/address1"/>
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
          <tr>
            <xsl:attribute name="id">trd_<xsl:value-of select="position()" /></xsl:attribute>
            <xsl:if test="position() &gt; $recordCount">
              <xsl:attribute name="style">display:none</xsl:attribute>
            </xsl:if>
            <td class="noBor" colspan="9">
              <div class="disNone">
                <xsl:attribute name="id">a<xsl:value-of select="position()" /></xsl:attribute>
                <table class="" width="100%" bgColor="#f2f2f2" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Initial Sales Order Number :</td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/salesOrderNumber"/>
                      </td>
                      <td align="right" nowrap="nowrap">Customer ID :</td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/id" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Apply Date from Lead file : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="effectiveDate" />
                      </td>
                      <td align="right" nowrap="nowrap">Open Date from Lead file : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="effectiveDate" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Source : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/organization/address/address1" />
                      </td>
                      <td align="right" nowrap="nowrap">Lead record status : </td>
                      <td nowrap="nowrap">
                            <xsl:value-of select="status" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Expiration Date: </td>
                      <td nowrap="nowrap">
                            <xsl:value-of select="expirationDate" />
                      </td>
                      <td align="right" nowrap="nowrap">Customer Last Name : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/lastName" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Customer Last Name : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/firstName" />
                      </td>
                      <td align="right" nowrap="nowrap">Last Name Kana : </td>
                      <td nowrap="nowrap">
                            <xsl:value-of select="customer/lastNameKana" />
                        
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">First Name Kana : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/firstNameKana" />
                      </td>
                      <td align="right" nowrap="nowrap">Customer Birthdate : </td>
                      <td nowrap="nowrap">
                            <xsl:value-of select="customer/dateOfBirth" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Address : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="address/address1" />
                      </td>
                      <td align="right" nowrap="nowrap">Postal Code : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/address/zipCode" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Email : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/emailAddress" />
                      </td>
                    
                      <td align="right" nowrap="nowrap">Telephone Number : </td>
                      <td nowrap="nowrap">
                            <xsl:value-of select="customer/workPhoneNumber" />
                       
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">SIM Phone Number: </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="customer/cellPhoneNumber" />
                      </td>
                      <td align="right" nowrap="nowrap">Sales Channel : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="origin/channelCode" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Shop ID : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="SHOPID" />
                      </td>
                      <td align="right" nowrap="nowrap">Shop Name : </td>
                       <td nowrap="nowrap">
                        <xsl:value-of select="SHOPNAME" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Shop Address : </td>
                       <td nowrap="nowrap">
                        <xsl:value-of select="SHOPADDRESS" />
                      </td>
                    <td align="right" nowrap="nowrap">Shop Zip Code : </td>
                       <td nowrap="nowrap">
                        <xsl:value-of select="SHOPZIPCODE" />
                      </td>
                    </tr>
                    <tr class="out">
                      <td align="right" nowrap="nowrap">Shop Telephone Number : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="SHOPTELEPHONENUMBER" />
                      </td>
                    <td align="right" nowrap="nowrap"></td>
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
          <xsl:attribute name="id">pg<xsl:value-of select="$pagerPrefix" />_<xsl:value-of select="$i"/></xsl:attribute>
          <xsl:attribute name="href">javascript:showHidePage(<xsl:value-of select="$i"/>,<xsl:value-of select="count(//ArrayOfSolicitDetails/SolicitDetails)"/>,<xsl:value-of select="$recordCount"/>);</xsl:attribute>
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
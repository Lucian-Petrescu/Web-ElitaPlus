<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="recordCount" />
  <xsl:template match="/">
    <h2 class="dataGridHeader">Search Results for Service Center</h2>
    <table width="100%" class="dataGrid" border="0" cellSpacing="0" cellPadding="0">
      <tbody>
        <tr>
          <td align="right" class="bor" colSpan="9">
            <xsl:value-of select="count(NewDataSet/ELP_SERVICE_CENTER)" /> Service Center(s) found
          </td>
        </tr>
        <xsl:if test="(ceiling(count(//NewDataSet/ELP_SERVICE_CENTER) div $recordCount)) &gt; 1">
          <tr>
            <td colspan="9" class="gridPager" style="text-align:center;">
              <xsl:call-template name="for.loop.pager">
                <xsl:with-param name="i" select="1"></xsl:with-param>
                <xsl:with-param name="count" select="ceiling(count(//NewDataSet/ELP_SERVICE_CENTER) div $recordCount)"></xsl:with-param>
                <xsl:with-param name="pagerPrefix" select='1'></xsl:with-param>
              </xsl:call-template>
            </td>
          </tr>
        </xsl:if>
        <tr>
          <th>
            &#160;
          </th>
          <th>
            <a href="#">Name</a>
          </th>
          <th>
            <a href="#">Address</a>
          </th>
          <th>
            <a href="#">Code</a>
          </th>
          <th>
            <a href="#">Mfg Auth</a>
          </th>
          <th>
            <a href="#">Pref</a>
          </th>
          <th>
            <a href="#">Rank</a>
          </th>
        </tr>
        <xsl:for-each select="NewDataSet/ELP_SERVICE_CENTER">
          <tr>
            <xsl:attribute name="id">tr_<xsl:value-of select="position()" /></xsl:attribute>
            <xsl:if test="position() &gt; $recordCount">
              <xsl:attribute name="style">display:none</xsl:attribute>
            </xsl:if>
            <td>
              <input type="radio" name="serviceCenterSelection">
                <xsl:attribute name="onClick">
                  SelectServiceCenter('<xsl:value-of select="CODE"/>')
                </xsl:attribute>
              </input>
            </td>
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
                  &#160;<xsl:value-of select="DESCRIPTION" />
                </span>
              </div>
            </td>
            <td nowrap="nowrap">
              <span>Address :</span ><xsl:value-of select="ADDRESS1" />
              <xsl:if test ="CITY !=''">
                <br />
                <span>City :</span >
                <xsl:value-of select="CITY" />
              </xsl:if>
              <xsl:if test ="POSTAL_CODE !=''">
                <br />
                <span>Zip :</span >
                <xsl:value-of select="POSTAL_CODE" />
              </xsl:if>
            </td>
            <td>
              <xsl:value-of select="CODE" />
            </td>
            <td>
              <xsl:choose>
                <xsl:when test="MAN_AUTH_FLAG = 'y'">
                  <img width="14" height="13" src="..\App_Themes\Default\Images\tickIcon.png" complete="complete"/>
                </xsl:when>
                <xsl:otherwise>
                  &#160;
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td>
              <xsl:choose>
                <xsl:when test="DEALER_PREF_FLAG = 'y'">
                  <img width="14" height="13" src="..\App_Themes\Default\Images\tickIcon.png" complete="complete"/>
                </xsl:when>
                <xsl:otherwise>
                  &#160;
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td>
              <xsl:choose>
                <xsl:when test="string-length(RATING_CODE) > 0">
                  <xsl:value-of select="RATING_CODE"/>
                </xsl:when>
                <xsl:otherwise>
                  &#160;
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
                    <tr>
                      <td align="right" nowrap="nowrap">Service Center Code :</td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="CODE"/>
                      </td>
                      <td align="right" nowrap="nowrap">Contact Name :</td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="CONTACT_NAME" />
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Service Center Name : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="DESCRIPTION" />
                      </td>
                      <td align="right" nowrap="nowrap">Phone 1 : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="PHONE1" />
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Address : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="ADDRESS1" />
                      </td>
                      <td align="right" nowrap="nowrap">Phone 2 : </td>
                      <td nowrap="nowrap">
                        <xsl:choose>
                          <xsl:when test="string-length(PHONE2) > 0">
                            <xsl:value-of select="PHONE2" />
                          </xsl:when>
                          <xsl:otherwise>
                            -
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Address 2 : </td>
                      <td nowrap="nowrap">
                        <xsl:choose>
                          <xsl:when test="string-length(ADDRESS2) > 0">
                            <xsl:value-of select="ADDRESS2" />
                          </xsl:when>
                          <xsl:otherwise>
                            -
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td align="right" nowrap="nowrap">Fax : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="FAX" />
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">City : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="CITY" />
                      </td>
                      <td align="right" nowrap="nowrap">Business Hours : </td>
                      <td nowrap="nowrap">
                        <xsl:choose>
                          <xsl:when test="string-length(BUSINESS_HOURS) > 0">
                            <xsl:value-of select="BUSINESS_HOURS" />
                          </xsl:when>
                          <xsl:otherwise>
                            -
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">State / Province : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="REGION" />
                      </td>
                      <td align="right" nowrap="nowrap">Processing Fee : </td>
                      <td nowrap="nowrap">
                        <xsl:choose>
                          <xsl:when test="string-length(PROCESSING_FEE) > 0">
                            <xsl:value-of select="PROCESSING_FEE" />
                          </xsl:when>
                          <xsl:otherwise>
                            -
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Country : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="COUNTRY" />
                      </td>
                      <td align="right" nowrap="nowrap">Email : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="EMAIL" />
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Zip : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="ZIP" />
                      </td>
                      <td align="right" nowrap="nowrap">CC Email : </td>
                      <td nowrap="nowrap">
                        <xsl:choose>
                          <xsl:when test="string-length(CC_EMAIL) > 0">
                            <xsl:value-of select="CC_EMAIL" />
                          </xsl:when>
                          <xsl:otherwise>
                            -
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Original Dealer : </td>
                      <td nowrap="nowrap">
                        <xsl:value-of select="ORIGINAL_DEALER" />
                      </td>
                      <td align="right" nowrap="nowrap">Default to Email : </td>
                      <td nowrap="nowrap">
                        <input disabled="disabled" type="checkbox">
                          <xsl:if test="DEFAULT_TO_EMAIL_FLAG = 'Y'">
                            <xsl:attribute name="checked">
                              checked
                            </xsl:attribute>
                          </xsl:if>
                        </input>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">&#160;</td>
                      <td nowrap="nowrap">
                        &#160;
                      </td>
                      <td align="right" nowrap="nowrap">Shipping : </td>
                      <td nowrap="nowrap">
                        <input disabled="disabled" type="checkbox">
                          <xsl:if test="SHIPPING = 'Y'">
                            <xsl:attribute name="checked">
                              checked
                            </xsl:attribute>
                          </xsl:if>
                        </input>
                      </td>
                    </tr>
                    <tr>
                      <td align="right" nowrap="nowrap">Comments : </td>
                      <td nowrap="nowrap" colspan="3">
                        <textarea rows="5" cols="180">
                          <xsl:value-of select="COMMENTS" />
                        </textarea>

                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </td>
          </tr>
        </xsl:for-each>
        <xsl:if test="(ceiling(count(//NewDataSet/ELP_SERVICE_CENTER) div $recordCount)) &gt; 1">
          <tr>
            <td colspan="9" class="gridPager" style="text-align:center;">
              <xsl:call-template name="for.loop.pager">
                <xsl:with-param name="i" select="1"></xsl:with-param>
                <xsl:with-param name="count" select="ceiling(count(//NewDataSet/ELP_SERVICE_CENTER) div $recordCount)"></xsl:with-param>
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
          <xsl:attribute name="href">javascript:showHidePage(<xsl:value-of select="$i"/>,<xsl:value-of select="count(//NewDataSet/ELP_SERVICE_CENTER)"/>,<xsl:value-of select="$recordCount"/>);</xsl:attribute>
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
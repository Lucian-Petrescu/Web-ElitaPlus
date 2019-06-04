<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    
    <div class="table-container">
      <div class="headcol">
        <table class="dataGrid">
          <thead>
            <tr>
              <th colspan="2" class="row-header">
                <div class="container"></div>
              </th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="~yaxis~">
              <tr>
                <td>
                  <div class="container">
                    <xsl:value-of select="Code" />
                  </div>
                </td>
                <td>
                  <div class="container">
                    <xsl:value-of select="Description" />
                  </div>
                </td>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
      </div>
      <div class="right">
        <table class="dataGrid no-border">
          <thead>
            <tr>
            <xsl:for-each select="~xaxis~">
              <th>
                <div class="container">
                  <xsl:value-of select="Description" />
                </div>
              </th>
            </xsl:for-each>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="~yaxis~">
              <xsl:variable name="yrec" select="."/>
              
              <tr>
                <xsl:for-each select="~xaxis~">
                  <xsl:variable name="xrec" select="."/>
                  <xsl:variable name="grant">
                    <xsl:choose>
                      <xsl:when test="/RoleCompanyStatus/Grants/Grant[~xaxisname~ = $xrec/Id and ~yaxisname~ = $yrec/Id and ~zaxisname~ = '~zaxisvalue~']">checked</xsl:when>
                      <xsl:otherwise></xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <xsl:variable name="enabled">
                    <xsl:choose>
                      <xsl:when test="$xrec/Enabled = 'N' or $yrec/Enabled = 'N'">N</xsl:when>
                      <xsl:otherwise>Y</xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <td valign="center" align="center">
                    <div class="container">
                      <input type="checkbox" name="chkGrant" data-company="" value="{Code}-{$yrec/Code}" data-checked="{$grant}" data-enabled="{$enabled}"></input>

                    </div>
                  </td>
                </xsl:for-each>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
      </div>
    </div>
  </xsl:template>
</xsl:stylesheet>







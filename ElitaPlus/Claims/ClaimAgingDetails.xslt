<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <table border="1" class="dataGrid" cellspacing="0" rules="all" style="width:90%;border-collapse:collapse;">
      <tbody>
        <xsl:for-each select="ClaimAgingDetailDs/Headers">
          <tr>
            <th align="center" scope="col" style="color:#12135B;width:20%;">
              <xsl:value-of select="STAGE_NAME" />
            </th>
            <th align="center" scope="col" style="color:#12135B;width:20%;">
              <xsl:value-of select="AGING_START_STATUS" />
            </th>
            <th align="center" scope="col" style="color:#12135B;width:20%;">
              <xsl:value-of select="AGING_END_STATUS" />
            </th>
            <th align="center" scope="col" style="color:#12135B;width:15%;">
              <xsl:value-of select="STATUS_AGING_DAYS" />
            </th>
            <th align="center" scope="col" style="color:#12135B;width:15%;">
              <xsl:value-of select="AGING_SINCE_CLAIM_INCEPTION_DAYS" />
            </th>
          </tr>
        </xsl:for-each>
        <xsl:for-each select="ClaimAgingDetailDs/AGING_DETAILS">
          <xsl:variable name="altColor">
            <xsl:choose>
              <xsl:when test="position() mod 2 = 0">#F1F1F1</xsl:when>
              <xsl:otherwise>#FFFFFF</xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr bgcolor="{$altColor}">
            <td rowspan="2" align="center" valign="middle">
              <xsl:value-of select="STAGE_NAME" />
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_START_STATUS != ''">
                  <xsl:value-of select="AGING_START_STATUS" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_END_STATUS != ''">
                  <xsl:value-of select="AGING_END_STATUS" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_DAYS != ''">
                  <xsl:value-of select="concat(format-number(AGING_DAYS, '0'), ' Day(s)')" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_SINCE_CLAIM_DAYS != ''">
                  <xsl:value-of select="concat(format-number(AGING_SINCE_CLAIM_DAYS, '0'), ' Day(s)')" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
          </tr>

          <tr bgcolor="{$altColor}">
            <td align="center">
              <xsl:attribute name="id">
                <xsl:value-of select="concat('Start', position())" />
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="AGING_START_DATETIME != ''">
                  <script type="text/javascript">
                    FormatDateTime("<xsl:value-of select="AGING_START_DATETIME"/>", "<xsl:value-of select="concat('Start', position())" />");
                  </script>
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:attribute name="id">
                <xsl:value-of select="concat('End', position())" />
              </xsl:attribute>
              <xsl:choose>
                <xsl:when test="AGING_END_DATETIME != ''">
                  <script type="text/javascript">
                    FormatDateTime("<xsl:value-of select="AGING_END_DATETIME"/>", "<xsl:value-of select="concat('End', position())" />");
                  </script>
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_HOURS != ''">
                  <xsl:value-of select="concat(format-number(AGING_HOURS, '0'), ' Hour(s)')" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td align="center">
              <xsl:choose>
                <xsl:when test="AGING_SINCE_CLAIM_HOURS != ''">
                  <xsl:value-of select="concat(format-number(AGING_SINCE_CLAIM_HOURS, '0'), ' Hour(s)')" />
                </xsl:when>
                <xsl:otherwise>
                  -
                </xsl:otherwise>
              </xsl:choose>
            </td>
          </tr>          
        </xsl:for-each>
      </tbody>
    </table>
  </xsl:template>

</xsl:stylesheet>
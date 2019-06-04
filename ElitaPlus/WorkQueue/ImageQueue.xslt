<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="IMAGE_ID" />
  <xsl:param name="SCAN_DATE" />  
  <xsl:template match="/">
    <div class="dataContainer">
      <div class="stepformZone">
        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$IMAGE_ID"/>:</td>
              <td align="left">
                <xsl:value-of select="ImageActionView/ImageId"/>
              </td>
            </tr>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$SCAN_DATE"/>:</td>
              <td align="left">
                <xsl:value-of select="ImageActionView/ScanDate"/>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </xsl:template >
</xsl:stylesheet >
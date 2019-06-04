<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="INSURED" />
  <xsl:param name="CLAIM_NUMBER" />
  <xsl:param name="ISSUE_DATE" />
  <xsl:param name="DATE_OF_LOSS" />
  <xsl:param name="ISSUE" />
  <xsl:param name="TYPE_OF_LOSS" />
  <xsl:param name="HDR_ISSUE_GRID" />
  <xsl:param name="PAGE_SIZE" />
  <xsl:param name="CREATED_DATE" />
  <xsl:param name="CREATED_BY" />
  <xsl:param name="PROCESSED_DATE" />
  <xsl:param name="PROCESSED_BY" />
  <xsl:param name="STATUS" />
  
  <xsl:template match="/">
    <div class="dataContainer">
      <div class="stepformZone">      
        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$INSURED"/>:</td>
              <td align="left">
                <xsl:value-of select="IssueActionView/InsuredName"/>
              </td>              
            </tr>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$CLAIM_NUMBER"/>:</td>
              <td align="left">
                <xsl:value-of select="IssueActionView/ClaimNumber"/>
              </td>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$ISSUE_DATE"/>:
              </td>
              <td align="left">
                <xsl:value-of select="IssueActionView/IssueDate"/>
              </td>             
            </tr>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$DATE_OF_LOSS"/>:
              </td>
              <td align="left">
                <xsl:value-of select="IssueActionView/DateOfLoss"/>
              </td>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$ISSUE"/>:
              </td>
              <td align="left">
                <xsl:value-of select="IssueActionView/IssueDescription"/>
              </td>
            </tr>
            <tr>
              <td nowrap="nowrap" align="right">
                <xsl:value-of select="$TYPE_OF_LOSS"/>:
              </td>
              <td align="left">
                <xsl:value-of select="IssueActionView/TypeOfLoss"/>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="dataContainer">
      <h2 class="dataGridHeader">
        <xsl:value-of select="HDR_ISSUE_GRID"/>
      </h2>
      <table class="dataGrid" width="100%" cellspacing="0" cellpadding="0" border="0">
        <tbody>
          <tr>
            <td class="bor">
              <xsl:value-of select="$PAGE_SIZE"/>
              <select name="select">
                <option>10</option>
              </select>
            </td>
            <td class="bor" align="right" colspan="6">
              <xsl:value-of select="count(IssueActionView/OtherClaimIssuesXML/NewDataSet/OtherClaimIssues)" /> Record(s) found
            </td>
          </tr>
          <tr>
            <th>
              <a href="#">
                <xsl:value-of select="$ISSUE"/>
              </a>
              <span class="sort_indi_des"></span>
            </th>
            <th>
              <a href="#">
                <xsl:value-of select="$CREATED_DATE"/>
              </a>
            </th>
            <th>
              <a href="#">
                <xsl:value-of select="$CREATED_BY"/>
              </a>
            </th>
            <th>
              <a href="#">
                <xsl:value-of select="$PROCESSED_DATE"/>
              </a>
            </th>
            <th>
              <a href="#">
                <xsl:value-of select="$PROCESSED_BY"/>
              </a>
            </th>
            <th>
              <a href="#">
                <xsl:value-of select="$STATUS"/>
              </a>
            </th>

          </tr>

          <xsl:for-each select="IssueActionView/OtherClaimIssuesXML/NewDataSet/OtherClaimIssues">
            <tr>
              <td>
                <xsl:value-of select="Issue_description"/>
              </td>
              <td>
                <xsl:value-of select="Created_Date"/>
              </td>
              <td>
                <xsl:value-of select="Created_By"/>
              </td>
              <td>
                <xsl:value-of select="Processed_Date"/>
              </td>
              <td>
                <xsl:value-of select="Processed_By"/>
              </td>
              <td>
                <xsl:value-of select="Status"/>
              </td>
            </tr>
          </xsl:for-each>

        </tbody>
      </table>
    </div>
  </xsl:template >
</xsl:stylesheet >






  
<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:a="http://tempuri.org/ServiceOrderReport.xsd"
    exclude-result-prefixes="a">
  <xsl:output indent="yes" method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <html>
      <head>
        <style>
          BODY { FONT-SIZE: 11px; FONT-FAMILY: arial;width:auto }
          TD { FONT-SIZE: 11px; FONT-FAMILY: arial;padding:0px;vertical-align:top; }
          TABLE {padding:0px; border:1px solid #000; width: 100%;}
          #company { FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right; FONT-FAMILY:arial }
          #number {padding-left:20px; }
          #title { FONT-WEIGHT: bold; FONT-SIZE: 18px; TEXT-ALIGN: center; padding-bottom:5px;}
          #outertbl {width: 100%; border:none;}
          .rowHd {font-size:13px; font-weight:bold; text-align:center;padding-top:5px;}
          .box {padding:10px;margin-right:40px;}
          .hdTable{border:none;}
          .innerTable{border:none;width:100%;}
          .content{width:50%;border-right:1px solid #000;}
          .bdrRow{border-bottom:1px solid #000;}
          .red {color: #FF0000;}
          .boldText {font-size:13px; font-weight:bold;}
        </style>
      </head>
      <body>
        <table id="outertbl">
          <tr>
            <td style="width:2px;"></td>
            <td style="text-align:center;">
              <table class="hdTable">
                <!--header-->
                <tr>
                  <td style="width:50%;">
                    <xsl:element name="img">
                      <xsl:attribute name="src">
                        <xsl:choose>
                          <xsl:when test="a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH">
                            <xsl:value-of select='concat(a:ServiceOrderReport/a:ServiceOrder/a:IMAGE_PATH,"assurant_logo_acn.jpg")'/>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:text>http://w1.assurant.com/elitalogos/assurant_logo_acn.jpg</xsl:text>
                          </xsl:otherwise>
                        </xsl:choose>
                      </xsl:attribute>
                    </xsl:element>
                  </td>
                  <td id="company">
                    <br/>安信龙咨询有限公司
                    <br/>电话：
                    <br/>传真：
                  </td>
                </tr>
              </table>
              <br/>
              <table class="hdTable">
                <tr>
                  <td colspan="2" id="title">
                    维修派工单
                  </td>
                </tr>
                <tr>
                  <td style="width:50%">
                    日期:  <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED,9,2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 6, 2)" />
                    <xsl:text>-</xsl:text>
                    <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:DATE_CREATED, 1, 4)" />
                  </td>
                  <td style="font-size:14px;font-weight:bold;">
                    授权编码: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CLAIM_NUMBER" />
                  </td>
                </tr>
                <tr>
                  <td>
                    维修网点名称: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_NAME" />
                  </td>
                  <td>
                    维修网点编码: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CODE" />
                  </td>
                </tr>
                <tr>
                  <td>
                    维修商地址: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ADDRESS1" />&#160;
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_CITY" />&#160;
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_STATE_PROVINCE" />
                  </td>
                  <td>
                    邮编: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_ZIP" />
                  </td>
                </tr>
                <tr>
                  <td style="white-space:nowrap;">
                    电话: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_PHONE" />
                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                    传真: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_FAX" />
                  </td>
                  <td style="white-space:nowrap;">
                    电子邮件: <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SVC_EMAIL" />
                  </td>
                </tr>
              </table>
              <table>
                <tr>
                  <td class="rowHd bdrRow">
                    授权
                  </td>
                  <td class="rowHd bdrRow">
                    客户
                  </td>
                </tr>
                <tr>
                  <td class="content bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td style="white-space:nowrap;width:25%">
                          安信龙授权人
                        </td>
                        <td colspan="3">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZED_BY" />
                        </td>
                      </tr>
                      <tr>
                        <td style="white-space:nowrap">
                          延保合同号码
                        </td>
                        <td colspan="3">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CERTIFICATE" />
                        </td>
                      </tr>
                      <tr>
                        <td style="white-space:nowrap">
                          产品购买日期
                        </td>
                        <td colspan="3">
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE,9,2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 6, 2)" />
                          <xsl:text>-</xsl:text>
                          <xsl:value-of select="substring(a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_SALES_DATE, 1, 4)" />
                        </td>
                      </tr>
                      <tr>
                        <td style="white-space:nowrap">
                          延保发票号码
                        </td>
                        <td colspan="3">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:INVOICE_NUMBER" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          授权金额
                        </td>
                        <td width="20%" style="white-space:nowrap;">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:AUTHORIZATION_AMOUNT" />
                        </td>
                        <td style="white-space:nowrap;text-align:right;" width="35%">
                          其中&#160;&#160;安信龙支付金额：
                        </td>
                        <td style="white-space:nowrap;text-align:left;">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ASSURANT_AMOUNT" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                        <td style="white-space:nowrap;text-align:right;" width="35%">
                          顾客支付金额：
                        </td>
                        <td style="white-space:nowrap;text-align:left;">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CONSUMER_AMOUNT" />
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td class="bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td style="width:25%">
                          顾客姓名
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CUSTOMER_NAME" />
                        </td>
                      </tr>
                      <tr>
                        <td style="white-space:nowrap">
                          顾客家庭电话
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:HOME_PHONE" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          顾客手机
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MOBILE_PHONE" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          顾客地址
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ADDRESS1" />
                          <br/>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:CITY" />
                          &#160;<xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:STATE_PROVINCE" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          邮编
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:ZIP" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="rowHd bdrRow">
                    产品信息
                  </td>
                  <td class="rowHd bdrRow">
                    故障描述（顾客通过电话提供）
                  </td>
                </tr>
                <tr>
                  <td class="content bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td style="width:25%">
                          生产商
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MANUFACTURER" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          型号
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:MODEL" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          产品
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PRODUCT_DESCRIPTION" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          IMEI/序列号
                        </td>
                        <td>
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SERIAL_NUMBER" />
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td class="bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td colspan="2" rowspan="4">
                          <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:PROBLEM_DESCRIPTION" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td colspan="2" class="rowHd bdrRow">
                    工作说明/状况
                  </td>
                </tr>
                <tr>
                  <td colspan="2" class="bdrRow">
                    订单类型:&#160;&#160;&#160;&#160;&#160; 送修 <input id="Checkbox1" type="checkbox" class="box" />上门维修<input id="Checkbox1" type="checkbox" class="box" />邮寄维修<input id="Checkbox1" type="checkbox" class="box" />检查/诊断<input id="Checkbox1" type="checkbox" class="box" />更换<input id="Checkbox1" type="checkbox" class="box" />
                    <ol style="font-size:10px;">
                      <li>
                        如果维修商提供送修或者上门维修服务，维修商需要在2小时内联系顾客预约送修时间（送修）或者上门时间（上门）来对产品进行最初的检查。维修商需在下一工作日内提供上门维修服务。如果维修商提供邮寄维修服务，顾客将把产品寄至维修商。通常需要首先检查产品的序列号。
                      </li>
                      <li>
                        在最初的检查后，如果维修费用在授权金额内，维修商需完成维修；如果维修费用超过授权金额，维修商需在继续维修前联系安信龙。任何超出本维修派工单授权金额的维修，均需事先得到安信龙的再次批准后方可进行；否则，安信龙有权拒绝支付超出部分的金额。
                      </li>
                      <li>
                        维修商收到产品后需在2个工作日内完成维修。
                      </li>
                      <li>
                        如果所需维修时间超出维修期限，维修商需要及时联系安信龙。
                      </li>
                      <li>
                        如果提供上门维修服务，维修商指派的维修人员需穿制服并在进入顾客家前向顾客出示工作证；维修人员需要保证工作区域的整洁。
                      </li>
                      <li>
                        在维修工作完成后，维修人员需在顾客或顾客代表面前测试维修好的产品，并由顾客或顾客代表签字确认。
                      </li>
                      <li>
                        维修工作完成后24小时内，须将顾客及维修人员签字的派工单回传安信龙。
                      </li>
                    </ol>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    附加说明（安信龙）<br/>
                    <xsl:value-of select="a:ServiceOrderReport/a:ServiceOrder/a:SPECIAL_INSTRUCTION" />
                    &#160;
                  </td>
                </tr>
              </table>
              <table style="margin-top:3px;">
                <tr>
                  <td class="rowHd" colspan="2">
                    <table class="innerTable">
                      <tr>
                        <td class="boldText" style="text-align:left;white-space: nowrap;vertical-align:bottom;" width="1%">
                          维修商填写以下信息:&#160;&#160;
                        </td>
                        <td class="boldText" style="text-align:left;white-space: nowrap;vertical-align:top;">
                          收到的凭证：<input id="Checkbox1" type="checkbox" style="padding:5px;" /><span style="padding-right:10px">延保发票</span><input id="Checkbox1" type="checkbox" style="padding:5px;" /><span style="padding-right:10px">产品发票</span><input id="Checkbox1" type="checkbox" style="padding:5px;" /><span style="padding-right:10px">延保合同</span><input id="Checkbox1" type="checkbox" style="padding:5px;" /><span style="padding-right:10px">保修卡</span><input id="Checkbox1" type="checkbox" style="padding:5px;" />其他（请注明）
                        </td>
                      </tr>
                      <tr>
                        <td>&#160;</td>
                        <td class="boldText" style="text-align:left" >
                          收到的产品附件（3C产品必须填写）：
                        </td>
                      </tr>
                      <tr>
                        <td style="text-align:left" colspan="2">
                          如旧机已经寄到残值处理中心请填写以下信息：
                        </td>
                      </tr>
                      <tr>
                        <td class="bdrRow" style="text-align:left;white-space:nowrap;" colspan="2">
                          <span style="padding-right:150px">快递单号：</span>寄出时间：
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td  class="rowHd bdrRow" style="width:50%">
                    维修结果
                  </td>
                  <td class="rowHd bdrRow">
                    费用明细（维修完成后填写）
                  </td>
                </tr>
                <tr>
                  <td class="content bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td style="width:50%">
                          产品接收日期:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td style="width:50%">
                          维修开始日期:
                        </td>
                      </tr>
                      <tr>
                        <td style="width:50%">
                          诊断结束日期:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td style="width:50%">
                          维修结束日期:
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          产品交付顾客日期 (或最后一次上门日期):
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td class="bdrRow">
                    <table class="innerTable">
                      <tr>
                        <td style="width:50%">
                          人工费:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td style="width:50%">
                          零件费:
                        </td>
                      </tr>
                      <tr>
                        <td>
                          交通费:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td>
                          快递费：
                        </td>
                      </tr>
                      <tr>
                        <td>
                          其他费用:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td>
                          总计：
                        </td>
                      </tr>
                      <tr>
                        <td>
                          残值:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td  class="rowHd bdrRow" style="width:50%">
                    维修描述
                  </td>
                  <td  class="rowHd bdrRow">
                    维修人员签字
                  </td>
                </tr>
                <tr>
                  <td class="content">
                    <table class="innerTable">
                      <tr>
                        <td>&#160;</td>
                        <td></td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <table class="innerTable">
                      <tr>
                        <td style="width:50%">
                          维修人员签字:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td>
                          日期:
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
              <table style="margin-top:3px;">
                <tr>
                  <td class="rowHd bdrRow" style="text-align:center" colspan="2">
                    顾客填写以下信息
                  </td>
                </tr>
                <tr>
                  <td  class="rowHd bdrRow" style="width:50%;text-align:center">
                    顾客确认维修结果及说明
                  </td>
                  <td  class="rowHd bdrRow" style="text-align:center">
                    顾客签字
                  </td>
                </tr>
                <tr>
                  <td class="content">&#160;</td>
                  <td style="text-align:left;width:50%">
                    <table class="innerTable">
                      <tr>
                        <td style="width:50%">
                          顾客签字:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                        </td>
                        <td>
                          日期:
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
            <td style="width:2px;">&#160;</td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

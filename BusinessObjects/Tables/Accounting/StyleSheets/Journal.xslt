<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:a="http://tempuri.org/AssurantElitaFelita.xsd" exclude-result-prefixes="a">
    <xsl:output method="xml" encoding="UTF-8" indent="yes" />
    <xsl:template match="/">
        <SSC>
            <SunSystemsContext>
                <BusinessUnit>
                    <xsl:value-of select="a:SSC/a:SunSystemsContext/a:BUSINESSUNIT"/>
                </BusinessUnit>
                <BudgetCode>
                    <xsl:value-of select="a:SSC/a:SunSystemsContext/a:BUDGETCODE"/>
                </BudgetCode>
            </SunSystemsContext>
            <MethodContext>
                <LedgerPostingParameters>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:ALLOWBALTRAN">
                        <AllowBalTran>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:ALLOWBALTRAN"/>
                        </AllowBalTran>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:ALLOWOVERBUDGET">
                        <AllowOverBudget>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:ALLOWOVERBUDGET"/>
                        </AllowOverBudget>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:ALLOWPOSTTOSUSPENDED">
                        <AllowPostToSuspended>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:ALLOWPOSTTOSUSPENDED"/>
                        </AllowPostToSuspended>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:BALANCINGOPTIONS">
                        <BalancingOptions>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:BALANCINGOPTIONS"/>
                        </BalancingOptions>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:DEFAULTPERIOD">
                        <DefaultPeriod>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:DEFAULTPERIOD"/>
                        </DefaultPeriod>
                    </xsl:if>
                        <JournalType>ELX</JournalType>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:LAYOUTCODE">
                        <LayoutCode>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:LAYOUTCODE"/>
                        </LayoutCode>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:LOADONLY">
                        <LoadOnly>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:LOADONLY"/>
                        </LoadOnly>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:POSTPROVISIONAL">
                        <PostProvisional>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:POSTPROVISIONAL"/>
                        </PostProvisional>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:POSTTOHOLD">
                        <PostToHold>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:POSTTOHOLD"/>
                        </PostToHold>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:POSTINGTYPE">
                        <PostingType>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:POSTINGTYPE"/>
                        </PostingType>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:REPORTINGACCOUNT">
                        <ReportingAccount>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:REPORTINGACCOUNT"/>
                        </ReportingAccount>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:SUPRESSSUBSTITUTEDMESSAGES">
                        <SuppressSubstitutedMessages>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:SUPRESSSUBSTITUTEDMESSAGES"/>
                        </SuppressSubstitutedMessages>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:SUSPENSEACCOUNT">
                        <SuspenseAccount>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:SUSPENSEACCOUNT"/>
                        </SuspenseAccount>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:TRANSACTIONAMOUNTACCOUNT">
                        <TransactionAmountAccount>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:TRANSACTIONAMOUNTACCOUNT"/>
                        </TransactionAmountAccount>
                    </xsl:if>
                    <xsl:if test="a:SSC/a:LedgerPostingParameters/a:DESCRIPTION">
                        <Description>
                            <xsl:value-of select="a:SSC/a:LedgerPostingParameters/a:DESCRIPTION"/>
                        </Description>
                    </xsl:if>
                    <Print>N</Print>
                    <ReportErrorsOnly>Y</ReportErrorsOnly>
                 </LedgerPostingParameters>
            </MethodContext>
            <Payload>
                <Ledger>
                    <xsl:for-each select="a:SSC/a:Line">
                        <xsl:if test="a:TRANSACTIONAMOUNT > 0">
                            <Line>
                                <xsl:if test="a:ACCOUNTCODE">
                                    <AccountCode>
                                        <xsl:value-of select="a:ACCOUNTCODE"/>
                                    </AccountCode>
                                </xsl:if>
                                <xsl:if test="a:ACCOUNTINGPERIOD">
                                    <AccountingPeriod>
                                        <xsl:value-of select="a:ACCOUNTINGPERIOD"/>
                                    </AccountingPeriod>
                                </xsl:if>
                                <xsl:if test="a:ALLOCATIONMARKER">
                                    <AllocationMarker>
                                        <xsl:value-of select="a:ALLOCATIONMARKER"/>
                                    </AllocationMarker>
                                </xsl:if>
                                <xsl:if test="a:CURRENCYCODE">
                                    <CurrencyCode>
                                        <xsl:value-of select="a:CURRENCYCODE"/>
                                    </CurrencyCode>
                                </xsl:if>
                                <xsl:if test="a:DEBITCREDIT">
                                    <DebitCredit>
                                        <xsl:value-of select="a:DEBITCREDIT"/>
                                    </DebitCredit>
                                </xsl:if>
                                <xsl:if test="a:DESCRIPTION">
                                    <Description>
                                        <xsl:value-of select="a:DESCRIPTION"/>
                                    </Description>
                                </xsl:if>
                                <xsl:if test="a:JOURNALSOURCE">
                                    <JournalSource>
                                        <xsl:value-of select="a:JOURNALSOURCE"/>
                                    </JournalSource>
                                </xsl:if>
                                <xsl:if test="a:JOURNALTYPE">
                                    <JournalType>
                                        <xsl:value-of select="a:JOURNALTYPE"/>
                                    </JournalType>
                                </xsl:if>
                                <xsl:if test="a:TRANSACTIONAMOUNT">
                                    <TransactionAmount>
                                        <xsl:value-of select="a:TRANSACTIONAMOUNT"/>
                                    </TransactionAmount>
                                </xsl:if>
                                <xsl:if test="a:TRANSACTIONAMOUNTDECIMALPLACES">
                                    <TransactionAmountDecimalPlaces>
                                        <xsl:value-of select="a:TRANSACTIONAMOUNTDECIMALPLACES"/>
                                    </TransactionAmountDecimalPlaces>
                                </xsl:if>
                                <xsl:if test="a:TRANSACTIONDATE">
                                    <TransactionDate>
                                        <xsl:value-of select="a:TRANSACTIONDATE"/>
                                    </TransactionDate>
                                </xsl:if>
                                <xsl:if test="a:TRANSACTIONREFERENCE">
                                    <TransactionReference>
                                        <xsl:value-of select="a:TRANSACTIONREFERENCE"/>
                                    </TransactionReference>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE1">
                                    <AnalysisCode1>
                                        <xsl:value-of select="a:ANALYSISCODE1"/>
                                    </AnalysisCode1>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE2">
                                    <AnalysisCode2>
                                        <xsl:value-of select="a:ANALYSISCODE2"/>
                                    </AnalysisCode2>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE3">
                                    <AnalysisCode3>
                                        <xsl:value-of select="a:ANALYSISCODE3"/>
                                    </AnalysisCode3>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE4">
                                    <AnalysisCode4>
                                        <xsl:value-of select="a:ANALYSISCODE4"/>
                                    </AnalysisCode4>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE5">
                                    <AnalysisCode5>
                                        <xsl:value-of select="a:ANALYSISCODE5"/>
                                    </AnalysisCode5>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE6">
                                    <AnalysisCode6>
                                        <xsl:value-of select="a:ANALYSISCODE6"/>
                                    </AnalysisCode6>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE7">
                                    <AnalysisCode7>
                                        <xsl:value-of select="a:ANALYSISCODE7"/>
                                    </AnalysisCode7>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE8">
                                    <AnalysisCode8>
                                        <xsl:value-of select="a:ANALYSISCODE8"/>
                                    </AnalysisCode8>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE9">
                                    <AnalysisCode9>
                                        <xsl:value-of select="a:ANALYSISCODE9"/>
                                    </AnalysisCode9>
                                </xsl:if>
                                <xsl:if test="a:ANALYSISCODE10">
                                    <AnalysisCode10>
                                        <xsl:value-of select="a:ANALYSISCODE10"/>
                                    </AnalysisCode10>
                                </xsl:if>
                                <DetailLad>
                                    <xsl:if test="a:ACCOUNTCODE">
                                        <AccountCode>
                                            <xsl:value-of select="a:ACCOUNTCODE"/>
                                        </AccountCode>
                                    </xsl:if>
                                    <xsl:if test="a:ACCOUNTINGPERIOD">
                                        <AccountingPeriod>
                                            <xsl:value-of select="a:ACCOUNTINGPERIOD"/>
                                        </AccountingPeriod>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION1">
                                        <GeneralDescription1>
                                            <xsl:value-of select="a:GENERALDESCRIPTION1"/>
                                        </GeneralDescription1>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION2">
                                        <GeneralDescription2>
                                            <xsl:value-of select="a:GENERALDESCRIPTION2"/>
                                        </GeneralDescription2>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION3">
                                        <GeneralDescription3>
                                            <xsl:value-of select="a:GENERALDESCRIPTION3"/>
                                        </GeneralDescription3>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION4">
                                        <GeneralDescription4>
                                            <xsl:value-of select="a:GENERALDESCRIPTION4"/>
                                        </GeneralDescription4>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION5">
                                        <GeneralDescription5>
                                            <xsl:value-of select="a:GENERALDESCRIPTION5"/>
                                        </GeneralDescription5>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION6">
                                        <GeneralDescription6>
                                            <xsl:value-of select="a:GENERALDESCRIPTION6"/>
                                        </GeneralDescription6>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION7">
                                        <GeneralDescription7>
                                            <xsl:value-of select="a:GENERALDESCRIPTION7"/>
                                        </GeneralDescription7>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION8">
                                        <GeneralDescription8>
                                            <xsl:value-of select="a:GENERALDESCRIPTION8"/>
                                        </GeneralDescription8>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION9">
                                        <GeneralDescription9>
                                            <xsl:value-of select="a:GENERALDESCRIPTION9"/>
                                        </GeneralDescription9>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION10">
                                        <GeneralDescription10>
                                            <xsl:value-of select="a:GENERALDESCRIPTION10"/>
                                        </GeneralDescription10>
                                    </xsl:if>
                                    <xsl:if test="a:TIMESTAMP">
                                        <GeneralDescription11>
                                            <xsl:value-of select="a:TIMESTAMP"/>
                                        </GeneralDescription11>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION12">
                                        <GeneralDescription12>
                                            <xsl:value-of select="a:GENERALDESCRIPTION12"/>
                                        </GeneralDescription12>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION13">
                                        <GeneralDescription13>
                                            <xsl:value-of select="a:GENERALDESCRIPTION13"/>
                                        </GeneralDescription13>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION14">
                                        <GeneralDescription14>
                                            <xsl:value-of select="a:GENERALDESCRIPTION14"/>
                                        </GeneralDescription14>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION15">
                                        <GeneralDescription15>
                                            <xsl:value-of select="a:GENERALDESCRIPTION15"/>
                                        </GeneralDescription15>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION16">
                                        <GeneralDescription16>
                                            <xsl:value-of select="a:GENERALDESCRIPTION16"/>
                                        </GeneralDescription16>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION17">
                                        <GeneralDescription17>
                                            <xsl:value-of select="a:GENERALDESCRIPTION17"/>
                                        </GeneralDescription17>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION18">
                                        <GeneralDescription18>
                                            <xsl:value-of select="a:GENERALDESCRIPTION18"/>
                                        </GeneralDescription18>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION19">
                                        <GeneralDescription19>
                                            <xsl:value-of select="a:GENERALDESCRIPTION19"/>
                                        </GeneralDescription19>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION20">
                                        <GeneralDescription20>
                                            <xsl:value-of select="a:GENERALDESCRIPTION20"/>
                                        </GeneralDescription20>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION21">
                                        <GeneralDescription21>
                                            <xsl:value-of select="a:GENERALDESCRIPTION21"/>
                                        </GeneralDescription21>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION22">
                                        <GeneralDescription22>
                                            <xsl:value-of select="a:GENERALDESCRIPTION22"/>
                                        </GeneralDescription22>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION23">
                                        <GeneralDescription23>
                                            <xsl:value-of select="a:GENERALDESCRIPTION23"/>
                                        </GeneralDescription23>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION24">
                                        <GeneralDescription24>
                                            <xsl:value-of select="a:GENERALDESCRIPTION24"/>
                                        </GeneralDescription24>
                                    </xsl:if>
                                    <xsl:if test="a:GENERALDESCRIPTION25">
                                        <GeneralDescription25>
                                            <xsl:value-of select="a:GENERALDESCRIPTION25"/>
                                        </GeneralDescription25>
                                    </xsl:if>
                                </DetailLad>
                            </Line>
                        </xsl:if>
                    </xsl:for-each>
                </Ledger>
            </Payload>
        </SSC>

    </xsl:template>
</xsl:stylesheet>
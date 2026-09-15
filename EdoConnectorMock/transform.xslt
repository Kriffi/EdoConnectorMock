<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="utf-8" indent="yes"/>

	<xsl:template match="/CustomERP_Export">
		<xsl:variable name="FileId" select="concat('ON-TRN-', OrderNumber)"/>

		<!-- Парсинг даты из ДД.ММ.ГГГГ в ГГГГ-ММ-ДД -->
		<xsl:variable name="RawDate" select="CreationDate"/>
		<xsl:variable name="Day" select="substring($RawDate, 1, 2)"/>
		<xsl:variable name="Month" select="substring($RawDate, 4, 2)"/>
		<xsl:variable name="Year" select="substring($RawDate, 7, 4)"/>
		<xsl:variable name="FormattedDate" select="concat($Year, '-', $Month, '-', $Day)"/>

		<Файл ИдФайл="{$FileId}">
			<Документ ДатаДок="{$FormattedDate}">
				<Грузоотправитель>
					<ОргНаим>
						<xsl:value-of select="Sender/Name"/>
					</ОргНаим>
					<ИНН>
						<xsl:value-of select="Sender/TaxID"/>
					</ИНН>
					<КПП>
						<xsl:value-of select="Sender/KPP"/>
					</КПП>
				</Грузоотправитель>

				<Груз>
					<xsl:attribute name="Описание">
						<xsl:value-of select="Cargo/Description"/>
					</xsl:attribute>
					<xsl:attribute name="МассаНетто">
						<xsl:value-of select="Cargo/WeightKG"/>
					</xsl:attribute>
				</Груз>
			</Документ>
		</Файл>
	</xsl:template>
</xsl:stylesheet>
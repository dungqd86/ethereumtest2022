README

1. You have create a database example: "ethereum2022"
2. Open the file "ethereum2022.sql" at the root folder and execute SQL in this
3. Open App.Config in "EthereumApp" project folder to edit:
	<connectionStrings>
		<add name="ConnString" connectionString="<YOUR CONNECTION STRING>" />
	</connectionStrings>
	<appSettings>
		<add key="BaseApiUrl" value="https://api.etherscan.io" />
		<add key="ModuleApi" value="api?module=proxy" />
		<add key="APIKey" value="<API_KEY" />
		<add key="StartBlockID" value="12100001" />
		<add key="LimitBlockID" value="12100500" />
		<add key="DurationApp" value="5000"/>
	</appSettings>
4. After then run app
	Logs are writed in the "Logs" folder of "EthereumApp" project folder

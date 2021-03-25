build:
	dotnet build src\PMF.MQTT.Publisher.sln

clean:
	dotnet clean src\PMF.MQTT.Publisher.sln

publish:
	dotnet publish src\PMF.MQTT.Publisher\PMF.MQTT.Publisher.csproj --runtime win-x64 --configuration release /p:PublishSingleFile=true --output ./publishFolder --force

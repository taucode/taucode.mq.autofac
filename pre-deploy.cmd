dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\tests\TauCode.Mq.Autofac.Tests\TauCode.Mq.Autofac.Tests.csproj
dotnet test -c Release .\tests\TauCode.Mq.Autofac.Tests\TauCode.Mq.Autofac.Tests.csproj

nuget pack nuget\TauCode.Mq.Autofac.nuspec

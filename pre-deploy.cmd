dotnet restore

dotnet build TauCode.Mq.Autofac.sln -c Debug
dotnet build TauCode.Mq.Autofac.sln -c Release

dotnet test TauCode.Mq.Autofac.sln -c Debug
dotnet test TauCode.Mq.Autofac.sln -c Release

nuget pack nuget\TauCode.Mq.Autofac.nuspec
#! /bin/sh
dotnet msbuild -p:Configuration=Release -p:TargetFramework=netcoreapp3.1 -p:Platform=x64 -p:StartupObject=ConsoleApp2.BitManipulation -p:UseAppHost=false
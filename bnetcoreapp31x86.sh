#! /bin/sh
dotnet msbuild -p:Configuration=Release -p:TargetFramework=netcoreapp3.1 -p:Platform=x86 -p:UseAppHost=false
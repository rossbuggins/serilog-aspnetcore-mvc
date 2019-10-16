# serilog-aspnetcore-mvc

[![Build Status](https://dev.azure.com/rossbuggins/serilog-aspnetcore-mvc/_apis/build/status/rossbuggins.serilog-aspnetcore-mvc?branchName=master)](https://dev.azure.com/rossbuggins/serilog-aspnetcore-mvc/_build/latest?definitionId=3&branchName=master)

Nuget feed:
https://pkgs.dev.azure.com/rossbuggins/serilog-aspnetcore-mvc/_packaging/serilog-aspnetcore-mvc-public/nuget/v3/index.json


MVC support for Serilog AspNetCore after discussion at https://github.com/serilog/serilog-aspnetcore/issues/131 and https://github.com/serilog/serilog-aspnetcore/pull/133

to use call 
```
mvcBuilder.AddSerilogMvcLogging();
```

Project updated to use

- GitFlow
- GitVersion

## GitFlow Install
choco install GitVersion.Portable

##GitVersion VS2019 Install
https://marketplace.visualstudio.com/items?itemName=vs-publisher-57624.GitFlowforVisualStudio2019

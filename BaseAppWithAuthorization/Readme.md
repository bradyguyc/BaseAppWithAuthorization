# .NET MAUI Starter App with Authorization

This app demonstrates how to use the Microsoft.Identity.Client library to authenticate users and access Microsoft Graph APIs in a .NET MAUI app.  It is based off of the example from [.NET MAUI Authentication](https://github.com/Azure-Samples/ms-identity-ciam-dotnet-tutorial/blob/main/1-Authentication/2-sign-in-maui/README.md)

The intenet of this repo is to create a base MAUI that can be a starting point for creating new applications with the core services already implemented or frameworked out as a good starting point.

## Getting Started

Using 1-Authentication\2-sign-in-maui as an example from the offical example at  [.NET MAUI Authentication](https://github.com/Azure-Samples/ms-identity-ciam-dotnet-tutorial/blob/main/1-Authentication/2-sign-in-maui/README.md)

### Prerequisites
Some updates to the nuget packages are required to get the app to compile.  This may be based on upgrading to .NET SDK 10 preview or just updated changes to the structure of identity packages.



`	
		
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Identity.Client" Version="4.69.1" />		
		<PackageReference Include="Microsoft.Identity.Client.Extensions.Msal" Version="4.69.1" />
		<PackageReference Include="Microsoft.Maui.Controls" Version="$(MauiVersion)" />
		<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="10.0.0-preview.1.25080.5" /
		
`

### Automating ClientID 

The ClientID is stored in the appsettings.json file.  This id comes from registering your app in Azure AD B2C. It is 'hardcode' in serveral files throughout the project and in some places can't be reference as a constant or variable.  Thus this powershell script does a search and replace of the client id in the project files.

Platforms\Android\[MsalActivity.cs,MainActivity.cs,AndroidManifest.xml]
Are updated with the client id using the value appsettings.json file.

This happens with every build so beaware if something unusal starts to happen to check and make sure the script hasn't mucked with anything by acident.

```
<Target Name="Update MSAL Client ID" BeforeTargets="BeforeBuild">
	<Exec Command="powershell.exe  -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $(ProjectDir)updatemsal.ps1" />
</Target>
```

### Custom Branding

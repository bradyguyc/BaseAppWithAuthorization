# .NET MAUI Starter App with Authorization

## 01-Entra External Identity Login
This app demonstrates how to use the Microsoft.Identity.Client library to authenticate users and access Microsoft Graph APIs in a .NET MAUI app.  It is based off of the example from [.NET MAUI Authentication](https://github.com/Azure-Samples/ms-identity-ciam-dotnet-tutorial/blob/main/1-Authentication/2-sign-in-maui/README.md)

The intenet of this repo is to create a base MAUI App that can be a starting point for creating new applications with core functionality and services already implemented or frameworked out as a good starting point.

### Getting Started

Using 1-Authentication\2-sign-in-maui as an example from the offical example at  [.NET MAUI Authentication](https://github.com/Azure-Samples/ms-identity-ciam-dotnet-tutorial/blob/main/1-Authentication/2-sign-in-maui/README.md)

You can follow the readme.md instructions for setting up a Entra External Identity Azure AD B2C or it may be in the process of being renamed to Azure AD Customer.

### Prerequisites
Some updates to the nuget packages are required to get the app to compile.  This may be based on upgrading to .NET SDK 10 preview or just updated changes to the structure of identity packages.
	
```		
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.0-preview.1.25080.5" />
		<PackageReference Include="Microsoft.Identity.Client" Version="4.69.1" />		
		<PackageReference Include="Microsoft.Identity.Client.Extensions.Msal" Version="4.69.1" />
		<PackageReference Include="Microsoft.Maui.Controls" Version="$(MauiVersion)" />
		<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="10.0.0-preview.1.25080.5" /
```


### Automating ClientID 
The ClientID is stored in the appsettings.json file.  This id comes from registering your app in Azure AD B2C. It is 'hardcode' in serveral files throughout the project and in some places can't be reference as a constant or variable.  Thus this powershell script does a search and replace of the client id in the project files.

Platforms\Android\[MsalActivity.cs,MainActivity.cs,AndroidManifest.xml]
Are updated with the client id using the value appsettings.json file.

This happens with every build so beaware if something unusal starts to happen to check and make sure the script hasn't mucked with anything by acident.  You could remove it from the build process and run it manually if you prefer.  

```
<Target Name="Update MSAL Client ID" BeforeTargets="BeforeBuild">
	<Exec Command="powershell.exe  -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $(ProjectDir)updatemsal.ps1" />
</Target>
```
### Moving login to a model view view model
SignIn was being invoked from the code behind of the main page.  This was moved to a view model to better follow the MVVM pattern.  This also allows for the login process to be more easily tested.  A sign in and sign out button was added to the mainpage for testing. Account management or signing in and out will be moved to a settings page later for the starter app.
### Error Handling Exiting out of the Login Process
The sign-in example throws a null exception if the user cancels the login process.  This was caught and handled by the app.  The user is returned to the main page and a message is displayed to the user.  I added a try catch around acquiretoken to catch the exception and return the user to the main page.  This is a temporary solution and should be handled in a more elegant way.  The user should be returned to the main page and a message displayed to the user.  This will be handled in a future update.  It may be that the MSALClient should be updated to throw a "user exited" exception for smoother handling.  *Question* I have an issue open with the MAUI team to see what they think.

For now know that you will need to wrap the acquire token in a try catch to handle the user exiting the login process anytime you want to obtain the token.

### Custom Branding


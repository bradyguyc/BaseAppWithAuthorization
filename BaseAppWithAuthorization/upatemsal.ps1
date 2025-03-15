# Define the paths to the files
$appSettingsPath = "./appsettings.json"
$msalActivityPath = "./Platforms/Android/MsalActivity.cs"

# Read the appsettings.json file
$appSettings = Get-Content -Raw -Path $appSettingsPath | ConvertFrom-Json

# Extract the ClientId
$clientId = $appSettings.ClientId

# Read the MsalActivity.cs file
$msalActivityContent = Get-Content -Path $msalActivityPath

# Define the new DataScheme value
$newDataScheme = "msal" + $clientId

# Replace the DataScheme value in the MsalActivity.cs file
$updatedMsalActivityContent = $msalActivityContent -replace 'DataScheme = "msal[^"]*"', "DataScheme = `"$newDataScheme`""

# Write the updated content back to the MsalActivity.cs file
Set-Content -Path $msalActivityPath -Value $updatedMsalActivityContent

Write-Host "MsalActivity.cs has been updated with the new DataScheme value."

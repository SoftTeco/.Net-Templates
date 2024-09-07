[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string] $Name
)
dotnet ef migrations add -s .\src\API\Host\Host.csproj -p .\src\API\UserManagement\Infrastructure\Infrastructure.csproj -o .\Data\Migrations $Name

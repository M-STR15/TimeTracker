#Získat aktuální datum a čas
$currentDate =Get-Date -Format "yyyy-MM-dd HH:mm:ss"

# Najít kořenový adresář Git repozitáře (relativně ke skriptu)
$scriptPath = $MyInvocation.MyCommand.Path
$scriptDir = Split-Path $scriptPath
Set-Location $scriptDir

# Pokusit se najít kořen repozitáře (pomocí git)
$repoRoot = git rev-parse --show-toplevel 2>$null

# Přesunout se do rootu repozitáře kvůli získání větve
Set-Location $repoRoot

Write-Host $branchName
# Záskat název aktuální větve v Gitu
$branchName = git rev-parse --abbrev-ref HEAD

# Definovat cestu k souboru, kde se bude c# kód ukládat
$outputPath = Join-Path -Path (Split-Path $PSScriptRoot -Parent) -ChildPath "BuldInfo.cs"

# c# kód pro generování třídy s informacemi o sestavení
$code = @"
using System;
using System.Reflection;
namespace TimeTracker.Web.Blazor.Server
{
	public class BuildInfo
	{
		public string BranchName => "$branchName";
		public DateTime BuildDate => DateTime.Parse("$currentDate");
		public string VersionStr => getVersion();

		private string getVersion()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;
			return version == null ? "unknown" : $"{version.Major}.{version.Minor}.{version.Build}";
		}
	}
}
"@

# Uložit vygenerovaný kód do souboru
Set-Content -Path $outputPath -Value $code -Encoding UTF8

# Vytisknout informaci o úspěšném vygenerování souboru
Write-Output "BuildInfo.cs was successfully generated at $outputPath"
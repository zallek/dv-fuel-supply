# Prerequisites

- Install .NET 7 and PowerShell
- Install [Unity Mod Manager](https://www.nexusmods.com/site/mods/21)
- (Optional) Install the mod [RuntimeUnityEditor](https://github.com/ManlyMarco/RuntimeUnityEditor) to ease development

# References Setup

To ensure MSBuild and your IDE can find Derail Valley classes, you'll need to create a `Directory.Build.targets` file to specify your reference paths. This file should be created in the root of the project, next to the LICENSE file.

Here an example for windows
```xml
<Project>
	<PropertyGroup>
		<DvInstallDir>C:\Program Files (x86)\Steam\steamapps\common\Derail Valley</DvInstallDir>
		<ReferencePath>
			$(DvInstallDir)\DerailValley_Data\Managed\
		</ReferencePath>
		<AssemblySearchPaths>$(AssemblySearchPaths);$(ReferencePath);</AssemblySearchPaths>
	</PropertyGroup>
</Project>
```

# Building

To build and copy the mod to the game install folder, simply run `dotnet build`.

# Packaging

To package a build for distribution, run `powershell -executionpolicy bypass .\package.ps1`. It will create a .zip file ready for distribution in the dist directory.

# Useful dev tools

- ILSpy: extract game code
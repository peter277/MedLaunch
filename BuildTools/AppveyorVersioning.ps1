﻿write-host "...Starting MedLaunch pre-build scripts.."

# get current directory (this will be something/something/BuildTools)
# so root would be $loc\..\
$loc = Split-Path $script:MyInvocation.MyCommand.Path
$root = (get-item $loc).parent.FullName

# get fileversion from assemblyinfo
$asi = get-content $root\MedLaunch\Properties\AssemblyInfo.cs
foreach ($line in $asi)
{
    if ($line -like '*AssemblyFileVersion(*')
    {
        $versionDot = $line.Replace("[assembly: AssemblyFileVersion(", "").Replace(")]", "").Replace("`"", "")
        Set-AppVeyorBuildVariable -Name 'MEDLAUNCH_VERSION_DOT' -Value $versionDot
        
        $version = $versionDot.Replace(".", "_")
        Set-AppVeyorBuildVariable -Name 'MEDLAUNCH_VERSION_UNDERSCORE' -Value $version
    }
}
write-host "Detected file version: $version / $versionDot"

# run only if this is within appveyor
if ($env:APPVEYOR -eq $true)
{
    write-host "Appveyor detected"

	# set vars for splashscreen updater
	$splashupdaterexe = "$($root)\SplashScreenUpdater\output\SplashScreenUpdater.exe"
	$imgSource = "$($root)\MedLaunch\Data\Graphics\mediconsplash-base.png"
	$imgDest = "$($root)\MedLaunch\Data\Graphics\mediconsplash-new.png"

    if ($env:APPVEYOR_REPO_BRANCH -eq "dev")
    {
        $buildNo = $env:APPVEYOR_BUILD_NUMBER
        write-host "dev branch detected"
        $filename = "MedLaunch_v$($version)-DEVBUILD-$($buildNo)"
        $buildVer = $filename.Replace(".zip", "")
        
        # set enviroment version
        Set-AppveyorBuildVariable -Name 'APPVEYOR_BUILD_VERSION' -Value $buildVer
        #$env:APPVEYOR_BUILD_VERSION = $buildVer
        
        ## update version
        $newVerDev = "ML-$versionDot-DEV-$buildNo"
        Update-AppveyorBuild -Version $newVerDev
        
        # set custom environment filename
        Set-AppveyorBuildVariable -Name 'ML_ARTIFACT_NAME' -Value $filename
        #$env:ML_ARTIFACT_NAME=$filename

		# update the splashscreen graphic
		Write-Host "Attempting to update MedLaunch splashscreen graphic for DEV"
		& "$splashupdaterexe" "$newVerDev" "$imgSource" "$imgDest"
    }
    if ($env:APPVEYOR_REPO_BRANCH -eq "master")
    {
        $buildNo = $env:APPVEYOR_BUILD_NUMBER
        write-host "master branch detected"
        $filename = "MedLaunch_v$($version)"
        $tmp = $filename.Replace(".zip", "")
        $buildVer = "$($tmp)-MASTER_$($buildNo)"
        
        # set enviroment version
        Set-AppveyorBuildVariable -Name 'APPVEYOR_BUILD_VERSION' -Value $buildVer
        #$env:APPVEYOR_BUILD_VERSION = $buildVer
        
        ## update version
        $newVerMas = "ML-$versionDot-MASTER-$buildNo"
        Update-AppveyorBuild -Version $newVerMas
        
        # set custom environment filename
        Set-AppveyorBuildVariable -Name 'ML_ARTIFACT_NAME' -Value $filename
        #$env:ML_ARTIFACT_NAME=$filename
        
            # set environment variable for release description (if a release description exists)
        $rPath = "$loc\ReleaseNotes\$versionDot.md"
        write-host "Looking for release info @ $rPath"
        if ([System.IO.File]::Exists($rPath))
        {
            write-host "Release info document found."
            $content = Get-Content $rPath
            $newContent = ""
            foreach ($l in $content)
            {
                $newContent += "$l\n"
            }
            Set-AppveyorBuildVariable -Name 'MEDLAUNCH_RELEASE_DESCRIPTION' -Value $newContent
        }
        else
        {
            write-host "Release info document NOT found."
        }
        
        #write-host "Release info: `n`n$newContent"

		# update the splashscreen graphic
		Write-Host "Attempting to update MedLaunch splashscreen graphic for PRODUCTION"
		& "$splashupdaterexe" "ML-$versionDot" "$imgSource" "$imgDest"
    }
}
else
{
    write-host "No Appveyor detected - ignoring branches"
    $filename = "MedLaunch_v$($version).zip"
}

write-host "Artifact name will be: $filename"
write-host "Environment variable for artifact is stored as: $env:ML_ARTIFACT_NAME"
write-host "buildversion has been set to: $buildVer"
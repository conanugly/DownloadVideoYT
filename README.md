## buid .net core IIS
setup dotnet-sdk or .net runtime

## publish setup
Deployment Mode: self-contained
Target Runtime: win-x64(64bit) || win-x86(86bit)

## file bat scrip

@echo off
:: runas /user:Administrator Example1Server.exe
::API_VideoYT
echo stop pool
::api stop pool

%SYSTEMROOT%\System32\inetsrv\appcmd stop apppool /apppool.name:"API_VideoYT" :: pool: API_VideoYT
echo Publishing C# project API...

echo Get win ARCH
::check win
setlocal
	if exist "%ProgramFiles(x86)%" (
		set "ARCH=win-x64"
	) else (
		set "ARCH=win-x86"
	)
	echo %ARCH%

	cd /d D:\Truong\DownloadVideoYT\DownloadVideoYouTobe\DownloadVideoYouTobe 
	::dotnet publish -c Release -o "D:\Truong\1.PublicCode\buildCode\SolutionVideoYT-API"
	dotnet publish -c Debug -o "D:\Truong\1.PublicCode\buildCode\SolutionVideoYT-API" -r %ARCH% --self-contained true

	::api start pool
	
	echo Start Pool
	%SYSTEMROOT%\System32\inetsrv\appcmd start apppool /apppool.name:"API_VideoYT"

	echo Publish completed.
endlocal
echo open site
start chrome --incognito http://localhost:[porrt]/swagger/index.html
pause
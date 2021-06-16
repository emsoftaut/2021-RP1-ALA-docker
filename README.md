rp1

## Generating SSL certificates 

Refer https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0

Windows
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p yourpasswordhere
```
Mac/Linux
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p yourpasswordhere
dotnet dev-certs https -v -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p 
dotnet dev-certs https -v -ep ${HOME}/.aspnet/https/aspnetapp.pfx
```
```
dotnet dev-certs https --trust
```

### Certificate Errors
Refer https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-5.0&tabs=visual-studio#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos and https://github.com/dotnet/AspNetCore.Docs/issues/6199

Run `dotnet dev-certs https --clean` followed by `dotnet dev-certs https --trust` if you have certificate issues.

### Building the docker image
```
docker build --pull -t dotnetapp:alpine -f Dockerfile.alpine-x64 .
```

### Running the docker image

For windows
```
docker run --rm -p 8000:80 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8000 -e ASPNETCORE_Kestrel__Certificates__Default__Password="yourpasswordhere" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ dotnetapp:alpine
```
For mac/linux
```
docker run --rm -p 8000:80 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8000 -e ASPNETCORE_Kestrel__Certificates__Default__Password="yourpasswordhere" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v ${HOME}/.aspnet/https:/https/ dotnetapp:alpine
```
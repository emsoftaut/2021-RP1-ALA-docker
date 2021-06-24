This project is a work in progress docker implementation of the ALA compliant demo calculator similar to the one at https://github.com/johnspray74/ReactiveCalculator. This project uses `ASP.NET Core`, `.NET 5`, which is the cross-platform version of .NET. 

Refer https://docs.microsoft.com/en-us/dotnet/standard/choosing-core-framework-server. 

Read more about ALA here https://abstractionlayeredarchitecture.com

# Using .NET CLI
To run the project use the command `dotnet run`.

For development, start with a file watcher using `dotnet watch run`.

# Using Docker
## Running without HTTPS
### Building the image
```
docker build --pull -t dotnetapp:alpine -f Dockerfile.alpine-x64 .
```
### Running the image
```
docker run -p 8000:80 dotnetapp:alpine
```

## Running with HTTPS (in progress)
Currently facing issues with getting the certificates for SSL. Referred https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-5.0&tabs=visual-studio#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos and https://github.com/dotnet/AspNetCore.Docs/issues/6199


Getting the following error :

`There was an error exporting HTTPS developer certificate to a file.` Some of the steps followed are put below.

### Generating SSL certificates 

Refer https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0

Windows
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p yourpasswordhere
```
Mac/Linux
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p yourpasswordhere
```
### Clean and trust
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
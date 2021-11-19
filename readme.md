# Demo for issue with Azure where Assemblies cannot be loaded

Live demo for issue described here: https://stackoverflow.com/questions/70016975/azure-app-service-cannot-access-files-and-therefore-not-even-load-assemblies

## Steps to reproduce 

1. Create a new App Service 
2. Configure the App Service to use .net 4.8 Runtime, 64-bit and set the app configuration `WEBSITE_FUSIONLOGGING_ENABLED=1`
3. Deploy the application to your app service (I used Rider to deploy it)
4. Try to open the website
5. Notice the `HTTP Error 502.5 - ANCM Out-Of-Process Startup Failure` being reported
6. Go to the Kudu of your app service `https://<yourappservice>.scm.azurewebsites.net/`
7. Open a Debug console and inspect the stdout logs at `C:\home\site\wwwroot\logs`
8. Notice that the reading of `Microsoft.AspNetCore.Hosting.Abstractions.dll` failed despite it being reported as existing in directory listing and through the FileInfo (see example blow)
9. Try to launch the C:\home\site\wwwroot\AzureFileNotFound.exe through the debug console and notice the application starts up fine. 

## Example stdlog output
```
Running under: IIS APPPOOL\#######
Directory exists: C:\home\site\wwwroot
Rule: 
 Ref: Everyone
 Inherted: True
 Rights: DeleteSubdirectoriesAndFiles, Modify, Synchronize
Rule: 
 Ref: BUILTIN\Administrators
 Inherted: True
 Rights: FullControl
C:\home\site\wwwroot\appsettings.Development.json
C:\home\site\wwwroot\appsettings.json
C:\home\site\wwwroot\AzureFileNotFound.deps.json
C:\home\site\wwwroot\AzureFileNotFound.exe
C:\home\site\wwwroot\AzureFileNotFound.exe.config
C:\home\site\wwwroot\AzureFileNotFound.pdb
C:\home\site\wwwroot\Microsoft.AspNetCore.Antiforgery.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authentication.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authentication.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authorization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authorization.Policy.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Connections.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Cors.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Cryptography.Internal.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.DataProtection.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.DataProtection.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Diagnostics.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Diagnostics.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.HostFiltering.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Server.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Html.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Extensions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Features.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.HttpOverrides.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.JsonPatch.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Localization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.ApiExplorer.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Cors.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.DataAnnotations.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Formatters.Json.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Localization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Razor.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Razor.Extensions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.RazorPages.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.TagHelpers.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.ViewFeatures.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.Language.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.Runtime.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.ResponseCaching.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Routing.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Routing.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.IIS.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.IISIntegration.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Https.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.WebUtilities.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.CSharp.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.Razor.dll
C:\home\site\wwwroot\Microsoft.DiaSymReader.Native.amd64.dll
C:\home\site\wwwroot\Microsoft.DotNet.PlatformAbstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Caching.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Caching.Memory.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Binder.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.CommandLine.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.EnvironmentVariables.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.FileExtensions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Json.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.UserSecrets.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyInjection.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyInjection.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyModel.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Composite.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Physical.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileSystemGlobbing.dll
C:\home\site\wwwroot\Microsoft.Extensions.Hosting.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Localization.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Localization.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Configuration.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Console.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Debug.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.EventSource.dll
C:\home\site\wwwroot\Microsoft.Extensions.ObjectPool.dll
C:\home\site\wwwroot\Microsoft.Extensions.Options.ConfigurationExtensions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Options.dll
C:\home\site\wwwroot\Microsoft.Extensions.Primitives.dll
C:\home\site\wwwroot\Microsoft.Extensions.WebEncoders.dll
C:\home\site\wwwroot\Microsoft.Net.Http.Headers.dll
C:\home\site\wwwroot\Microsoft.Win32.Registry.dll
C:\home\site\wwwroot\Newtonsoft.Json.Bson.dll
C:\home\site\wwwroot\Newtonsoft.Json.dll
C:\home\site\wwwroot\System.Buffers.dll
C:\home\site\wwwroot\System.Collections.Immutable.dll
C:\home\site\wwwroot\System.ComponentModel.Annotations.dll
C:\home\site\wwwroot\System.Diagnostics.DiagnosticSource.dll
C:\home\site\wwwroot\System.IO.Pipelines.dll
C:\home\site\wwwroot\System.Memory.dll
C:\home\site\wwwroot\System.Numerics.Vectors.dll
C:\home\site\wwwroot\System.Reflection.Metadata.dll
C:\home\site\wwwroot\System.Runtime.CompilerServices.Unsafe.dll
C:\home\site\wwwroot\System.Security.AccessControl.dll
C:\home\site\wwwroot\System.Security.Cryptography.Cng.dll
C:\home\site\wwwroot\System.Security.Cryptography.Xml.dll
C:\home\site\wwwroot\System.Security.Permissions.dll
C:\home\site\wwwroot\System.Security.Principal.Windows.dll
C:\home\site\wwwroot\System.Text.Encoding.CodePages.dll
C:\home\site\wwwroot\System.Text.Encodings.Web.dll
C:\home\site\wwwroot\System.Threading.Tasks.Extensions.dll
C:\home\site\wwwroot\Web.config
File does exist: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
Failed to check file permissions: System.IO.FileNotFoundException: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
   at System.Security.AccessControl.NativeObjectSecurity.CreateInternal(ResourceType resourceType, Boolean isContainer, String name, SafeHandle handle, AccessControlSections includeSections, Boolean createByName, ExceptionFromErrorCode exceptionFromErrorCode, Object exceptionContext)
   at System.Security.AccessControl.FileSystemSecurity..ctor(Boolean isContainer, String name, AccessControlSections includeSections, Boolean isDirectory)
   at System.Security.AccessControl.FileSecurity..ctor(String fileName, AccessControlSections includeSections)
   at System.IO.File.GetAccessControl(String path)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 61
Directory C:\home\site\wwwroot
Assembly AzureFileNotFound, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
Assembly Location C:\home\site\wwwroot\AzureFileNotFound.exe
Failed to load file: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
FusionLog: 
Failed to start System.IO.FileNotFoundException: Could not find file 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'.
File name: 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'
   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.File.InternalReadAllBytes(String path, Boolean checkHost)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 82

Unhandled Exception: System.IO.FileNotFoundException: Could not find file 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'.
   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.File.InternalReadAllBytes(String path, Boolean checkHost)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 94
   at AzureFileNotFound.Program.Main(String[] args) in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 16
Running under: IIS APPPOOL\filenotfoundexception
Directory exists: C:\home\site\wwwroot
Rule: 
 Ref: Everyone
 Inherted: True
 Rights: DeleteSubdirectoriesAndFiles, Modify, Synchronize
Rule: 
 Ref: BUILTIN\Administrators
 Inherted: True
 Rights: FullControl
C:\home\site\wwwroot\appsettings.Development.json
C:\home\site\wwwroot\appsettings.json
C:\home\site\wwwroot\AzureFileNotFound.deps.json
C:\home\site\wwwroot\AzureFileNotFound.exe
C:\home\site\wwwroot\AzureFileNotFound.exe.config
C:\home\site\wwwroot\AzureFileNotFound.pdb
C:\home\site\wwwroot\Microsoft.AspNetCore.Antiforgery.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authentication.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authentication.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authorization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Authorization.Policy.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Connections.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Cors.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Cryptography.Internal.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.DataProtection.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.DataProtection.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Diagnostics.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Diagnostics.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.HostFiltering.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Server.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Html.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Extensions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Http.Features.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.HttpOverrides.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.JsonPatch.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Localization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.ApiExplorer.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Cors.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.DataAnnotations.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Formatters.Json.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Localization.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Razor.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.Razor.Extensions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.RazorPages.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.TagHelpers.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Mvc.ViewFeatures.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.Language.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Razor.Runtime.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.ResponseCaching.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Routing.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Routing.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.IIS.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.IISIntegration.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Core.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Https.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.dll
C:\home\site\wwwroot\Microsoft.AspNetCore.WebUtilities.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.CSharp.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.dll
C:\home\site\wwwroot\Microsoft.CodeAnalysis.Razor.dll
C:\home\site\wwwroot\Microsoft.DiaSymReader.Native.amd64.dll
C:\home\site\wwwroot\Microsoft.DotNet.PlatformAbstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Caching.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Caching.Memory.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Binder.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.CommandLine.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.EnvironmentVariables.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.FileExtensions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.Json.dll
C:\home\site\wwwroot\Microsoft.Extensions.Configuration.UserSecrets.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyInjection.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyInjection.dll
C:\home\site\wwwroot\Microsoft.Extensions.DependencyModel.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Composite.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileProviders.Physical.dll
C:\home\site\wwwroot\Microsoft.Extensions.FileSystemGlobbing.dll
C:\home\site\wwwroot\Microsoft.Extensions.Hosting.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Localization.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Localization.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Abstractions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Configuration.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Console.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.Debug.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.dll
C:\home\site\wwwroot\Microsoft.Extensions.Logging.EventSource.dll
C:\home\site\wwwroot\Microsoft.Extensions.ObjectPool.dll
C:\home\site\wwwroot\Microsoft.Extensions.Options.ConfigurationExtensions.dll
C:\home\site\wwwroot\Microsoft.Extensions.Options.dll
C:\home\site\wwwroot\Microsoft.Extensions.Primitives.dll
C:\home\site\wwwroot\Microsoft.Extensions.WebEncoders.dll
C:\home\site\wwwroot\Microsoft.Net.Http.Headers.dll
C:\home\site\wwwroot\Microsoft.Win32.Registry.dll
C:\home\site\wwwroot\Newtonsoft.Json.Bson.dll
C:\home\site\wwwroot\Newtonsoft.Json.dll
C:\home\site\wwwroot\System.Buffers.dll
C:\home\site\wwwroot\System.Collections.Immutable.dll
C:\home\site\wwwroot\System.ComponentModel.Annotations.dll
C:\home\site\wwwroot\System.Diagnostics.DiagnosticSource.dll
C:\home\site\wwwroot\System.IO.Pipelines.dll
C:\home\site\wwwroot\System.Memory.dll
C:\home\site\wwwroot\System.Numerics.Vectors.dll
C:\home\site\wwwroot\System.Reflection.Metadata.dll
C:\home\site\wwwroot\System.Runtime.CompilerServices.Unsafe.dll
C:\home\site\wwwroot\System.Security.AccessControl.dll
C:\home\site\wwwroot\System.Security.Cryptography.Cng.dll
C:\home\site\wwwroot\System.Security.Cryptography.Xml.dll
C:\home\site\wwwroot\System.Security.Permissions.dll
C:\home\site\wwwroot\System.Security.Principal.Windows.dll
C:\home\site\wwwroot\System.Text.Encoding.CodePages.dll
C:\home\site\wwwroot\System.Text.Encodings.Web.dll
C:\home\site\wwwroot\System.Threading.Tasks.Extensions.dll
C:\home\site\wwwroot\Web.config
File does exist: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
Failed to check file permissions: System.IO.FileNotFoundException: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
   at System.Security.AccessControl.NativeObjectSecurity.CreateInternal(ResourceType resourceType, Boolean isContainer, String name, SafeHandle handle, AccessControlSections includeSections, Boolean createByName, ExceptionFromErrorCode exceptionFromErrorCode, Object exceptionContext)
   at System.Security.AccessControl.FileSystemSecurity..ctor(Boolean isContainer, String name, AccessControlSections includeSections, Boolean isDirectory)
   at System.Security.AccessControl.FileSecurity..ctor(String fileName, AccessControlSections includeSections)
   at System.IO.File.GetAccessControl(String path)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 61
Directory C:\home\site\wwwroot
Assembly AzureFileNotFound, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
Assembly Location C:\home\site\wwwroot\AzureFileNotFound.exe
Failed to load file: C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll
FusionLog: 
Failed to start System.IO.FileNotFoundException: Could not find file 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'.
File name: 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'
   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.File.InternalReadAllBytes(String path, Boolean checkHost)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 82

Unhandled Exception: System.IO.FileNotFoundException: Could not find file 'C:\home\site\wwwroot\Microsoft.AspNetCore.Hosting.Abstractions.dll'.
   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
   at System.IO.File.InternalReadAllBytes(String path, Boolean checkHost)
   at AzureFileNotFound.Program.DumpSystemInformation() in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 94
   at AzureFileNotFound.Program.Main(String[] args) in D:\Dev\Git\AzureFileNotFound\AzureFileNotFound\Program.cs:line 16

```
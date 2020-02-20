# StaticWebAssetBasePath as wwwroot 

## Problem 
_StaticWebAsset_ is nice way to distribute asset on web project.
For _component_ on asset is not _look_ natural.

![alt text][i-default]

As we can see, the path a bit strange and on razor will show not got the file. Event when we using __StaticWebAssetBasePath__.

### Default

The default tranformation is __wwwroot/pic.jpg__ to **wwwroot/_content/LibraryName/pic.jgp**.

![alt text][i-basepath-as-default]

### StaticWebAssetBasePath

When we had _PropertyGroup_ __StaticWebAssetBasePath__ the transformation is __wwwroot/pic.jpg__ to __wwwroot/StaticWebAssetBasePath/pic.jpg__.

![alt text][i-basepath-as-root]

The transformation change differently from __the physical path__ to __the relative path__ as we look on razor-component that use the image. User from this library will need adjust the path depends on setting. 

### Target

What transformation we want is __wwwroot/override/pic.jpg__ to __wwwroot/StaticWebAssetBasePath/pic.jpg__ and __StaticWebAssetBasePath__ value is __override__. As we can see, the path of image will be same as internal (like razor-component) and outside (web application who use this library).

![alt text][i-basepath-as-override]

## To Solve 
It would be nice if we can make __StaticWebAssetBasePath__ as __wwwroot__. It is more natural and user of the _component_ will easy to _map_ the file path.

![alt text][i-basepath]

The way we can do it by change the way _Microsoft.NET.Sdk.Razor_ SDK collect information on that asset.

![alt text][i-target]

It is just add another property and change the way path is. As show in picture, we make change on path and default implementation does not change.

Unfortunately, it must be done in _Microsoft.NET.Sdk.Razor.StaticWebAssets.targets_. I am do not know how to override that target without modify it.

This is the result making __StaticWebAssetBasePath__ as __wwwroot__ as it reference by NuGet.

![alt text][i-release-asset]

It work also with override sub-folder. For example, folder _say-hi_ can have content from one or more _librarys_ or _packages_ as long the __BasePath__ difference.

![alt text][i-basepath-override]

### Minimum Touch on SDK

If we wanna make only a litte change on SDK Targets then we can make _conditional_ on current target.

![alt text][i-conditional-target]

And on the Project side, we modify with __PropertyGroup__'s, __Override Target__ and add __'Pipeline'__.

![alt text][i-conditional-target-project]

## Test Solution

1. Modify target on _Microsoft.NET.Sdk.Razor.StaticWebAssets.targets_.
2. Build every _Soeleman.Libs.*_ on __Debug__ and __Release__ (for NuGet package).
3. On project _Soeleman.Web_ on __Release__ update NuGet reference.
4. Build project _Soeleman.Web_ to run and test.
5. Test on _Publish_ too.

## Note on Release with NuGet
On case of Parent-Child for this _StaticWebAsset_. The _Soelemen.Libs.Parent_ consume _Soeleman.Libs.Child_ as NuGet package; it is show up on __wwwroot__, so we can consume the path on our _component_ as _normal_ it can be.

![alt text][i-release-ref]

How ever on project _Soeleman.Web_ since we not reference directly package from _Soeleman.Libs.Child_. 

Visual studio will add file _Soeleman.Libs.Parent.StaticWebAssets.xml_ as include (that way the folder __obj__ not hide). It is oke since it does not effect on __run__ or __publish__.

And other thing is, it will show up the _child.js_. This is only show on there without actual file.

![alt text][i-release]

## Note
This issue already mention on ASP.NET Repo [Issue #14586]

[Issue #14586]: https://github.com/aspnet/AspNetCore/issues/14568 "ASPNetCore Issue #14568"

[i-target]: assets/img/ResolveCurrentProjectStaticWebAssetsInputs_Targets.png "ResolveCurrentProjectStaticWebAssetsInputs target"
[i-basepath]: assets/img/BasePath_as_wwwroot.png "BasePath as wwwroot"
[i-basepath-override]: assets/img/BasePath_as_wwwroot_Override.png "BasePath as wwwroot override"
[i-default]: assets/img/Default_as_wwwroot.png "Default as wwwroot"
[i-release]: assets/img/Release_NuGet.png "Configuration Release"
[i-release-asset]: assets/img/Release_NuGet_AssetOnWwwroot.png "Configuration Release asset on wwwroot"
[i-release-ref]: assets/img/Release_NuGet_FromReference.png "Configuration Release from reference"
[i-conditional-target]: assets/img/ResolveCurrentProjectStaticWebAssetsInputs_ConditionOverride.png "Conditional Target on SDK"
[i-conditional-target-project]: assets/img/ResolveCurrentProjectStaticWebAssetsInputs_Project.png "Conditional Target on Project"
[i-basepath-as-default]: assets/img/BasePathAsDefault.png "BasePath as Default"
[i-basepath-as-root]: assets/img/BasePathAsRoot.png "BasePath as Root"
[i-basepath-as-override]: assets/img/BasePathAsOverride.png "BasePath as Override"
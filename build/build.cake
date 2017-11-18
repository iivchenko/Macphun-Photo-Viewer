Task("NuGet")    
    .Does(() =>
{
    NuGetRestore("../Src/PhotoViewer.sln");
});

Task("Default")
  .IsDependentOn("NuGet")
  .Does(() =>
{
  MSBuild("../Src/PhotoViewer.sln");
});

RunTarget("Default");
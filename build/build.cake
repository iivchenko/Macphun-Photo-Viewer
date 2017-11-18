var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("NuGet")    
    .Does(() =>
{
    NuGetRestore("../Src/PhotoViewer.sln");
});

Task("Build")
  .IsDependentOn("NuGet")
  .Does(() =>
{
  MSBuild("../Src/PhotoViewer.sln",  new MSBuildSettings().SetConfiguration(configuration));
});

Task("Zip")
  .IsDependentOn("Build")
  .Does(() =>
{
  var root = System.IO.Path.Combine("../Src/Macphun.PhotoViewer.Wpf.App/bin/", configuration);
  var source = root;
  var destination = System.IO.Path.Combine(root, "macphun-photo-viewer.zip");
  
  Zip(source, destination);
});

Task("Default")
  .IsDependentOn("Build")
  .Does(() =>
{
});

RunTarget(target);
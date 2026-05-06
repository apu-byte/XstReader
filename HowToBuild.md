Two things are required to build the project: the source code and the tool to build it with. 
- The source code can be obtained from [github.com/Dijji/XstReader>](<https://github.com/Dijji/XstReader>) using the 'Clone or Download' button to download the zip file, and unpacking the whole XstReader file structure into a working area on your PC.

- The tool can be obtained by downloading and installing VisualStudio Community from [visualstudio.microsoft.com](https://visualstudio.microsoft.com/). By default, this will include support for C# development, which is what this project needs.

Once installed, double click XstReader.sln in the root folder of the source code to open the project in Visual Studio.

At this point, you should be able to build the solution (Build/Build Solution in the Visual Studio menus), and run it from within Visual Studio (Debug/Start Debugging).


## Safe build/test workflow (Windows VM)

1. Create or revert to a clean Windows VM snapshot.
2. Install Visual Studio 2022 Community with .NET desktop development.
3. Install .NET 6 SDK for `XstPortableExport`.
4. Clone the repository and open `XstReader.sln`.
5. Build using `Build > Build Solution`.
6. For CLI export: `dotnet build XstPortableExport/XstPortableExport.csproj -c Release`.
7. Validate with known-safe test data only. Do **not** execute untrusted PST/OST samples.

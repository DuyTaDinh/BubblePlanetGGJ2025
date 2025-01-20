using UnityEditor;
namespace BubblePlanet.Scripts.Editor
{
	public class ExportPackage
	{
		[MenuItem("Exports/Export Project")]
		static void Export()
		{
			ExportPackageOptions options = ExportPackageOptions.Interactive |
			                               ExportPackageOptions.Recurse |
			                               ExportPackageOptions.IncludeDependencies |
			                               ExportPackageOptions.IncludeLibraryAssets;

			// Gets all the assets in the project
			string[] paths = AssetDatabase.GetAllAssetPaths();

			// Defines the name of the package file. It will be stored on the project's root folder.
			string fileName = PlayerSettings.productName + ".unitypackage";

			// Exports
			AssetDatabase.ExportPackage(paths, fileName, options);
		}
	}
}
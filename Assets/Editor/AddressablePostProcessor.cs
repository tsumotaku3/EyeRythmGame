using System;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

public class AddressablePostProcessor : AssetPostprocessor
{
    private static readonly string targetPath = "Assets/AddressableItems";

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var dirty = false;
        dirty |= ChangeEntry(importedAssets, false);
        dirty |= ChangeEntry(deletedAssets, true);
        dirty |= ChangeEntry(movedAssets, false);

        if (dirty)
        {
            AssetDatabase.SaveAssets();
        }
    }

    private static bool ChangeEntry(string[] paths, bool delete)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var groups = settings.groups;
        var dirty = false;

        foreach (var path in paths)
        {
            if (!path.StartsWith(targetPath))
                continue;

            if (AssetDatabase.IsValidFolder(path))
                continue;

            var assetPath = path.Replace(targetPath, "");

            var targetGroup = settings.DefaultGroup;
            var rootIndex = assetPath.IndexOf(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal);
            if (rootIndex >= 0)
            {
                var rootPath = assetPath.Substring(0, rootIndex);
                var groupIndex = groups.FindIndex(g => g.Name == rootPath);
                if (groupIndex >= 0)
                {
                    targetGroup = groups[groupIndex];
                }
                else
                {
                    var groupTemplate = settings.GetGroupTemplateObject(0) as AddressableAssetGroupTemplate;
                    targetGroup = settings.CreateGroup(rootPath, false, false, false, null, groupTemplate.GetTypes());
                }

                assetPath = assetPath.Substring(rootIndex + 1);
            }

            var guid = AssetDatabase.AssetPathToGUID(path);
            if (delete)
            {
                settings.RemoveAssetEntry(guid);
            }
            else
            {
                var lastIndex = assetPath.LastIndexOf(".", StringComparison.Ordinal);
                var address = assetPath.Substring(0, lastIndex);

                var entry = settings.CreateOrMoveEntry(guid, targetGroup);
                if (entry.address == address)
                    continue;

                entry.address = address;
            }

            dirty = true;
        }

        return dirty;
    }
}
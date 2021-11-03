using ProjectS.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectSAssetPostProcessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
    {
        StaticDataImporter.Import(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
    }
}

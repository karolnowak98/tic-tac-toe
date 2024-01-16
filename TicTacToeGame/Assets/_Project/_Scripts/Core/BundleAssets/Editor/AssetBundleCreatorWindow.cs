using UnityEditor;
using UnityEngine;
using File = System.IO.File;

namespace GlassyCode.TTT.Core.BundleAssets.Editor
{
    public class AssetBundleCreatorWindow : EditorWindow
    {
        private static readonly Object[] Sprites = new Object[4];
        private static string _bundleName;

#if UNITY_EDITOR
        [MenuItem("TicTacToe/Asset Bundle Creator")]
        public static void ShowWindow() => GetWindow<AssetBundleCreatorWindow>("Asset Bundle Creator");
#endif

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            Sprites[0] = EditorGUILayout.ObjectField("X symbol sprite", Sprites[0], typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Sprites[1] = EditorGUILayout.ObjectField("O symbol sprite", Sprites[1], typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Sprites[2] = EditorGUILayout.ObjectField("Background sprite", Sprites[2], typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Sprites[3] = EditorGUILayout.ObjectField("Line sprite", Sprites[3], typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _bundleName = EditorGUILayout.TextField("Asset bundle name", _bundleName);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Build asset bundle"))
            {
                BuildAssetBundle();
            }
        }

        private static void BuildAssetBundle()
        {
            if (!ValidateFields())
            {
                EditorUtility.DisplayDialog("Error", "Some fields are empty!", "ok");
                return;
            }

            var path = $"{Application.streamingAssetsPath}/{_bundleName}";

            if (File.Exists(path) && !EditorUtility.DisplayDialog("Warning", "There is a file with the given name in the streamingAssets folder. Will be overwritten!", "ok", "cancel"))
            {
                return;
            }

            foreach (var sprite in Sprites)
            {
                SetBundleOnAsset(sprite, _bundleName);
            }

            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

            foreach (var sprite in Sprites)
            {
                SetBundleOnAsset(sprite, string.Empty);
            }
        }

        private static void SetBundleOnAsset(Object asset, string bundleName)
        {
            var assetPath = AssetDatabase.GetAssetPath(asset);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(bundleName, string.Empty);
        }

        private static bool ValidateFields()
        {
            return !string.IsNullOrEmpty(_bundleName)
                && Sprites[0] != null
                && Sprites[1] != null
                && Sprites[2] != null
                && Sprites[3] != null;
        }
    }
}
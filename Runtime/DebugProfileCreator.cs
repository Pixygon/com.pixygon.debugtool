using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pixygon.DebugTool
{
    public class DebugProfileCreator : MonoBehaviour
    {
        private const string Prefix = "<Color=#FF00FF><b>Pixygon Debug Tool</b></color>";

        public static DebugProfiles CreateDebugProfiles() {
            Debug.Log($"{Prefix}: Couldn't find DebugProfiles, creating new one...");
            var profile = ScriptableObject.CreateInstance<DebugProfiles>();
            profile._addressable = CreateNewDebugProfile("Addressables");
            profile._tablet = CreateNewDebugProfile("Tablet");
            profile._application = CreateNewDebugProfile("Application");
            profile._avatar = CreateNewDebugProfile("Avatar");
            profile._actor = CreateNewDebugProfile("Actor");
            profile._nft = CreateNewDebugProfile("NFT");
            profile._property = CreateNewDebugProfile("Property");
            profile._camera = CreateNewDebugProfile("Camera");
            profile._pixygonApi = CreateNewDebugProfile("PixygonAPI");
            #if UNITY_EDITOR
            if(!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.CreateAsset(profile, "Assets/Resources/DebugProfiles.asset");
            Debug.Log($"{Prefix}: Saved DebugProfiles to Resources!");
            #endif
            Debug.Log($"{Prefix}: DebugProfiles created!");
            return profile;
        }

        private static DebugProfile CreateNewDebugProfile(string prefix) {
            var profile = ScriptableObject.CreateInstance<DebugProfile>();
            profile._prefix = prefix;
            profile._prefixColor = Random.ColorHSV();
            profile._hexColor = "#"+ColorUtility.ToHtmlStringRGBA(profile._prefixColor);
            profile._showLogs = true;
            
            #if UNITY_EDITOR
            if(!AssetDatabase.IsValidFolder("Assets/_DataObjects"))
                AssetDatabase.CreateFolder("Assets", "_DataObjects");
            if(!AssetDatabase.IsValidFolder("Assets/_DataObjects/DebugProfiles"))
                AssetDatabase.CreateFolder("Assets/_DataObjects", "DebugProfiles");
            AssetDatabase.CreateAsset(profile, $"Assets/_DataObjects/DebugProfiles/{prefix}.asset");
            //Debug.Log("Pixygon Debug Tool: Saved DebugProfiles to Resources!");
            #endif
            return profile;
        }
        
        public static DebugProfile CreateAndAddMissingProfile(DebugGroup group, DebugProfiles profiles) {
            var profile = CreateNewDebugProfile(group.ToString());
            switch (group) {
                case DebugGroup.Addressable:
                    profiles._addressable = profile;
                    break;
                case DebugGroup.Tablet:
                    profiles._tablet = profile;
                    break;
                case DebugGroup.Application:
                    profiles._application = profile;
                    break;
                case DebugGroup.Avatar:
                    profiles._avatar = profile;
                    break;
                case DebugGroup.Actor:
                    profiles._actor = profile;
                    break;
                case DebugGroup.Nft:
                    profiles._nft = profile;
                    break;
                case DebugGroup.Property:
                    profiles._property = profile;
                    break;
                case DebugGroup.Camera:
                    profiles._camera = profile;
                    break;
                case DebugGroup.PixygonApi:
                    profiles._pixygonApi = profile;
                    break;
                case DebugGroup.PixygonMicro:
                    profiles._pixygonMicro = profile;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(group), group, null);
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(profiles);
            AssetDatabase.SaveAssets();
#endif
            return profile;
        }

    }
}

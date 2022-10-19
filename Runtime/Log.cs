using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pixygon.DebugTool
{
    public class Log : MonoBehaviour {

        private static DebugProfiles _debugProfiles;
        private const string Prefix = "<Color=#FF00FF><Bold>{Pixygon Debug Tool}</Bold></color>";
        
        private static void LoadDebugProfile() {
            _debugProfiles = Resources.Load<DebugProfiles>("DebugProfiles");
            if (_debugProfiles == null)
                CreateDebugProfiles();
        }

        private static void CreateDebugProfiles() {
            Debug.Log($"{Prefix}: Couldn't find DebugProfiles, creating new one...");
            _debugProfiles = ScriptableObject.CreateInstance<DebugProfiles>();
            _debugProfiles._addressable = CreateNewDebugProfile("Addressables");
            _debugProfiles._tablet = CreateNewDebugProfile("Tablet");
            _debugProfiles._application = CreateNewDebugProfile("Application");
            _debugProfiles._avatar = CreateNewDebugProfile("Avatar");
            _debugProfiles._actor = CreateNewDebugProfile("Actor");
            _debugProfiles._nft = CreateNewDebugProfile("NFT");
            _debugProfiles._property = CreateNewDebugProfile("Property");
            _debugProfiles._camera = CreateNewDebugProfile("Camera");
            _debugProfiles._pixygonApi = CreateNewDebugProfile("PixygonAPI");
            #if UNITY_EDITOR
            if(!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.CreateAsset(_debugProfiles, "Assets/Resources/DebugProfiles.asset");
            Debug.Log($"{Prefix}: Saved DebugProfiles to Resources!");
            #endif
            Debug.Log($"{Prefix}: DebugProfiles created!");
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
            AssetDatabase.CreateAsset(_debugProfiles, $"Assets/_DataObjects/DebugProfiles/{prefix}.asset");
            //Debug.Log("Pixygon Debug Tool: Saved DebugProfiles to Resources!");
            #endif
            return profile;
        }

        public static void DebugMessage( DebugGroup group, object message, Object sender = null) {
            if (_debugProfiles == null) LoadDebugProfile();
            var currentProfile = group switch {
                DebugGroup.Addressable => _debugProfiles._addressable,
                DebugGroup.Tablet => _debugProfiles._tablet,
                DebugGroup.Application => _debugProfiles._application,
                DebugGroup.Avatar => _debugProfiles._avatar,
                DebugGroup.Actor => _debugProfiles._actor,
                DebugGroup.Nft => _debugProfiles._nft,
                DebugGroup.Property => _debugProfiles._property,
                DebugGroup.Camera => _debugProfiles._camera,
                DebugGroup.PixygonApi => _debugProfiles._pixygonApi,
                _ => throw new ArgumentOutOfRangeException(nameof(group), group, null)
            };

            if (!currentProfile._showLogs) return;
            if(sender == null)
                Debug.Log($"{currentProfile.GetPrefix}{message}");
            else
                Debug.Log($"{currentProfile.GetPrefix}{message}", sender);

        }
    }

    public enum DebugGroup {
        Addressable,
        Tablet,
        Application,
        Avatar,
        Actor,
        Nft,
        Property,
        Camera,
        PixygonApi
    }
}
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

        private static void LoadDebugProfile() {
            _debugProfiles = Resources.Load<DebugProfiles>("DebugProfiles");
            if (_debugProfiles == null)
                CreateDebugProfiles();
        }

        private static void CreateDebugProfiles() {
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
            AssetDatabase.CreateAsset(_debugProfiles, "Assets/Resources");
            #endif
        }

        private static DebugProfile CreateNewDebugProfile(string prefix) {
            var profile = ScriptableObject.CreateInstance<DebugProfile>();
            profile._prefix = prefix;
            profile._prefixColor = Random.ColorHSV();
            profile._hexColor = "#"+ColorUtility.ToHtmlStringRGBA(profile._prefixColor);
            profile._showLogs = true;
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
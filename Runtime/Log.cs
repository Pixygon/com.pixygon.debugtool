using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pixygon.DebugTool
{
    public class Log : MonoBehaviour {

        private static DebugProfiles _debugProfiles;
        
        private static void LoadDebugProfile() {
            _debugProfiles = Resources.Load<DebugProfiles>("DebugProfiles");
            if (_debugProfiles == null)
                _debugProfiles = DebugProfileCreator.CreateDebugProfiles();
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
                DebugGroup.PixygonMicro => _debugProfiles._pixygonMicro,
                _ => throw new ArgumentOutOfRangeException(nameof(group), group, null)
            };

            if (currentProfile == null)
                currentProfile = DebugProfileCreator.CreateAndAddMissingProfile(group, _debugProfiles);

            if (!currentProfile._showLogs) return;
            if(sender == null)
                Debug.Log($"{currentProfile.GetPrefix}{message}");
            else
                Debug.Log($"{currentProfile.GetPrefix}{message}", sender);

        }
    }
}
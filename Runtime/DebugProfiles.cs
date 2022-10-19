using System;
using UnityEngine;

namespace Pixygon.DebugTool {
    [Serializable, CreateAssetMenu(fileName = "DebugProfiles", menuName = "Pixygon/Debugger/DebugProfiles")]
    public class DebugProfiles : ScriptableObject {
        public DebugProfile _addressable;
        public DebugProfile _tablet;
        public DebugProfile _application;
        public DebugProfile _actor;
        public DebugProfile _avatar;
        public DebugProfile _nft;
        public DebugProfile _property;
        public DebugProfile _camera;
        public DebugProfile _pixygonApi;
    }
}
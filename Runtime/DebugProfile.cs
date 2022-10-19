using System;
using UnityEngine;

namespace Pixygon.DebugTool {
    [Serializable, CreateAssetMenu(fileName = "DebugProfile", menuName = "Pixygon/Debugger/DebugProfile")]
    public class DebugProfile : ScriptableObject {
        public bool _showLogs;
        public string _prefix;
        public Color _prefixColor;
        public string _hexColor;
        public string GetPrefix => $"<Color={_hexColor}>{_prefix}</color>: ";
        
        private void OnValidate() {
            _hexColor = "#"+ColorUtility.ToHtmlStringRGBA(_prefixColor);
        }
    }
}
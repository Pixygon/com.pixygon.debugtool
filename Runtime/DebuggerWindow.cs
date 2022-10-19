using TMPro;
using UnityEngine;

namespace Pixygon.DebugTool {
    public class DebuggerWindow : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _debugErrorText;
        [SerializeField] private TextMeshProUGUI _debugWarningText;
        [SerializeField] private TextMeshProUGUI _debugLogText;
        [SerializeField] private TextMeshProUGUI _fpsText;
        private float _deltaTime;
        
        private void Update() {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _fpsText.text = $"{_deltaTime * 1000.0f:0.0} ms ({1.0f / _deltaTime:0.} fps)";
        }

        public void RefreshDebugger() {
            _debugErrorText.text = Debugger._instance._errorText;
            _debugLogText.text = Debugger._instance._logText;
            _debugWarningText.text = Debugger._instance._warningText;
        }

        public void ClearDebugger() {
            Debugger._instance.OnClearDebugLogs();
            _debugErrorText.text = "";
            _debugLogText.text = "";
            _debugWarningText.text = "";
        }
    }
}
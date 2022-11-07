using UnityEngine;

namespace Pixygon.DebugTool {
    public class Analyzer : MonoBehaviour {
        private float _deltaTime;
        private bool _showAnalyzer;
        
        private void Start() {
            Debugger._instance.Console._commandList.Add(new ConsoleCommand("fps", "Toggle showing the fps", "fps", () => {
                _showAnalyzer = !_showAnalyzer;
            }));
        }
        private void Update() {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }
        private void OnGUI() {
            if (!_showAnalyzer) return;
            var ms = _deltaTime * 1000.0f;
            var fps = 1.0f / _deltaTime;
            var text = $"FPS: {fps:0.}\nMS: {ms:0.0}";
            GUI.Label(new Rect(0f, Screen.height-40, 200f, 40f), text);
            GUI.backgroundColor = new Color(0, 0, 0, 0f);
        }
    }
}
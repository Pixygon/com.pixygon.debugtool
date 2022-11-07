using System;
using UnityEngine;

namespace Pixygon.DebugTool {
    public class Debugger : MonoBehaviour {

        public static Debugger _instance;
        public string _errorText;
        public string _logText;
        public string _warningText;

        public ConsoleController Console { get; private set; }
        private void Awake() {
            if(_instance != null) Destroy(this);
            else _instance = this;
            Console = GetComponent<ConsoleController>();
            gameObject.AddComponent<Analyzer>();
        }

        private void OnEnable() {
            Application.logMessageReceived += LogMessage;
        }

        private void OnDisable() {
            Application.logMessageReceived -= LogMessage;
        }
        public void LogMessage(string message, string stackTrace, LogType type) {
            switch (type) {
                case LogType.Error:
                case LogType.Exception: {
                    if(_errorText.Length > 40000)
                        _errorText = $"{message}\nSTACK: {stackTrace}\n";
                    else
                        _errorText += $"{message}\n";
                    break;
                }
                case LogType.Log:
                case LogType.Assert: {
                    if(_logText.Length > 40000)
                        _logText = $"{message}\n";
                    else
                        _logText += $"{message}\n";
                    break;
                }
                case LogType.Warning when _warningText.Length > 40000:
                    _warningText = $"{message}\n";
                    break;
                case LogType.Warning:
                    _warningText += $"{message}\n";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void OnClearDebugLogs() {
            _errorText = "";
            _logText = "";
            _warningText = "";
        }
    }
}
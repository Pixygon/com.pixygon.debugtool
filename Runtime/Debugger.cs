using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.DebugTool {
    public class Debugger : MonoBehaviour {

        public static Debugger _instance;

        [FormerlySerializedAs("errorText")] public string _errorText;
        [FormerlySerializedAs("logText")] public string _logText;
        [FormerlySerializedAs("warningText")] public string _warningText;

        private void Awake() {
            if(_instance != null)
                Destroy(this);
            else {
                _instance = this;
            }
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
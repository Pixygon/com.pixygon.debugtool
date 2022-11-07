using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pixygon.DebugTool {
    public class ConsoleController : MonoBehaviour {
        public List<object> _commandList;
        
        [SerializeField] private InputAction _toggleDebugInput;
        [SerializeField] private InputAction _returnInput;
        
        private bool _showConsole;
        private bool _showHelp;
        private string _input;
        private Vector2 _scroll;


        private void OnToggleDebug(InputAction.CallbackContext value) {
            _showConsole = !_showConsole;
        }
        private void OnReturn(InputAction.CallbackContext value) {
            if (!_showConsole) return;
            HandleInput();
            _input = "";
        }

        private void Awake() {
            _commandList = new List<object> {
                new ConsoleCommand("help", "Toggles a list of commands", "help", () => {
                    _showHelp = !_showHelp;
                })
            };
            _input = "";
        }
        private void OnGUI() {
            if (!_showConsole) return;
            var y = 0f;

            if (_showHelp) {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                var viewport = new Rect(0, 0, Screen.width - 30, 20 * _commandList.Count);
                _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), _scroll, viewport);
                for (var i = 0; i < _commandList.Count; i++) {
                    var command = _commandList[i] as ConsoleCommandBase;
                    if (command == null) continue;
                    var label = $"{command.CommandFormat} - {command.CommandDescription}";
                    var labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                    GUI.Label(labelRect, label);
                }
                GUI.EndScrollView();
                y += 100;
            }
            GUI.Box(new Rect(0f, y, Screen.width, 30f), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0f);
            _input = GUI.TextField(new Rect(10f, y+5f, Screen.width-20f, 20f), _input);
        }
        private void OnEnable() {
            _toggleDebugInput.Enable();
            _returnInput.Enable();
            _toggleDebugInput.performed += OnToggleDebug;
            _returnInput.performed += OnReturn;
        }
        private void OnDisable() {
            _toggleDebugInput.Disable();
            _returnInput.Disable();
            _toggleDebugInput.performed -= OnToggleDebug;
            _returnInput.performed -= OnReturn;
        }
        
        private void HandleInput() {
            if (_input == string.Empty)
                _input = "help";
            Debug.Log($"This is input: <{_input}>");
            var properties = _input.Split(' ');
            var found = false;
            for (var i = 0; i < _commandList.Count; i++) {
                if (_commandList[i] is ConsoleCommandBase commandBase && !_input.Contains(commandBase.CommandId)) continue;
                switch (_commandList[i]) {
                    case ConsoleCommand:
                        ((ConsoleCommand)_commandList[i]).Invoke();
                        found = true;
                        break;
                    case ConsoleCommand<int>:
                        ((ConsoleCommand<int>)_commandList[i]).Invoke(int.Parse(properties[1]));
                        found = true;
                        break;
                }
            }
            if (found) return;
            _input = "help";
            OnReturn(new InputAction.CallbackContext());
        }
    }
}
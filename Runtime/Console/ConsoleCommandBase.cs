using System;

namespace Pixygon.DebugTool
{
    public class ConsoleCommandBase {
        public string CommandId { get; }
        public string CommandDescription { get; }
        public string CommandFormat { get; }

        protected ConsoleCommandBase(string id, string description, string format) {
            CommandId = id;
            CommandDescription = description;
            CommandFormat = format;
        }
    }
    public class ConsoleCommand : ConsoleCommandBase {
        private readonly Action _command;
        public ConsoleCommand(string id, string description, string format, Action command) : base(id, description,
            format) {
            _command = command;
        }
        public void Invoke() {
            _command.Invoke();
        }
    }
    public class ConsoleCommand<T1> : ConsoleCommandBase {
        private readonly Action<T1> _command;
        public ConsoleCommand(string id, string description, string format, Action<T1> command) : base(id, description,
            format) {
            _command = command;
        }
        public void Invoke(T1 value) {
            _command.Invoke(value);
        }
    }
}
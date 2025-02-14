using System.Collections.Generic;
using Utility;

public class CommandInvoker
{
    private List<ICommand> _replayCommands = new List<ICommand>();
    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    private Environment env;

    public CommandInvoker(Environment env)
    {
        this.env = env;
    }

    private void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
        }
    }

    public void ExecuteAndRecord(ICommand command)
    {
        command.Execute();
        _replayCommands.Add(command);
    }

    public void Replay()
    {
        foreach (var command in _replayCommands)
        {
            command.Execute();
        }
    }

    
}
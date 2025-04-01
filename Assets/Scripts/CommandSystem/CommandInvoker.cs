using System;
using System.Collections.Generic;
using CommandSystem.Commands;
using UnityEngine;
using Utility;
[Serializable]
public class CommandInvoker
{
    private List<CommandBase> _replayCommands = new List<CommandBase>();
    private Stack<CommandBase> _commandHistory = new Stack<CommandBase>();

    public List<CommandBase> ReplayCommands => _replayCommands;

    public Stack<CommandBase> CommandHistory => _commandHistory;

    private Environment env;

    public event Action<Environment> CommandUpdate;

    public CommandInvoker(Environment env)
    {
        this.env = env;
    }

    private void ExecuteNoRecord(CommandBase command)
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

    public void Execute(CommandBase command)
    {
        if (command.Parent == null)
        {
            ExecuteAndRecord(command);
        }
        else
        {
            
            ExecuteNoRecord(command);
        }
    }

    private void ExecuteAndRecord(CommandBase command)
    {
        _replayCommands.Add(command);
        command.Execute(); 
        CommandUpdate?.Invoke(env);
    }

    public void Replay()
    {
        foreach (var command in _replayCommands)
        {
            command.Execute();
        }
    }

    
}
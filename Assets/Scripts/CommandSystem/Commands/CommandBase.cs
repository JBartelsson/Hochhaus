using System.Collections.Generic;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public abstract class CommandBase : ICommand
    {

        protected Environment _env;

        public Environment Env => _env;
        
        public CommandBase Parent {get; set;}
        
        public List<CommandBase> Children {get; set;} = new List<CommandBase>();


        protected Item sender;

        public Item Sender => sender;
        protected abstract void ExecuteSingle();
        public abstract void Undo();

        public void Execute()
        {
            ExecuteSingle();
            // Debug.Log("EXECUTING COMMAND: " + this);
            if (Parent != null && Children.Count == 0)
            {
                _env.GameUpdate(Environment.GameStateType.EMPTY);
            }
            foreach (var command in Children)
            {
                // Debug.Log($"{this}// Trying to Execute child {command}");
                command.Execute();
            }
        }

        public void SetEnv(Environment env)
        {
            _env = env;
        }

        public CommandBase(Environment env, Item sender, CommandBase parent)
        {
            _env = env;
            this.sender = sender;
            Parent = parent;
        }

        public CommandBase AddChild(CommandBase command)
        {
            command.Parent = this;
            Children.Add(command);
            return this;
        }
        
        public override string ToString()
        {
            string s = $"{this.GetType()}";
            if (Children.Count == 0)
            {
                return s;
            }

            s += ": ";
            foreach (var commandBase in Children)
            {
                s += commandBase.ToString() + ",";
            }

            s.Remove(s.Length - 1);
            return s;
        }
        
    }


}
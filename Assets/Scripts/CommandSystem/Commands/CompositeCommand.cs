using System.Collections.Generic;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class CompositeCommand : CommandBase
    {

        public CompositeCommand(Environment env, Item sender,  CommandBase parent = null) : base(env, sender, parent)
        {
        }

        protected override void ExecuteSingle()
        {
            foreach (var command in Children)
            {
                _env.CommandInvoker.Execute(command);
                Debug.Log($"Executing Command {command}");
            }
        }

        

        public override void Undo()
        {
        }
    }
}
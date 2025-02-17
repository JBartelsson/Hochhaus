using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class ChangeDrawsCommand : CommandBase
    {
        private int value;
        public int Value => value;

        

        public ChangeDrawsCommand(Environment env, Item sender, int value) : base(env, sender)
        {
            SetEnv(env);
            this.value = value;
        }


        public override void Execute()
        {
            _env.CardSystem.AddDraws(value);
        }

        public override void Undo()
        {
        }
    }
}
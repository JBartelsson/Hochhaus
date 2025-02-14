namespace CommandSystem.Commands
{
    public class AddPointsCommand : CommandBase
    {

        private float points;

        public AddPointsCommand(Environment env, float points)
        {
            SetEnv(env);
            this.points = points;
        }


        public override void Execute()
        {
            _env.PlayerStats.Score.AddPoints(points);
        }

        public override void Undo()
        {
            _env.PlayerStats.Score.RemovePoints(points);

        }
    }
}
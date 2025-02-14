namespace CommandSystem.Commands
{
    public class UpdateScoreCommand : CommandBase
    {
        public float Value => value;

        public Score.ScoreType ScoreType => _scoreType;

        private Score lastScore;
        private Score newScore;

        public Score LastScore => lastScore;

        public Score NewScore => newScore;

        private float value;
        private Score.ScoreType _scoreType;

        public UpdateScoreCommand(Environment env, Score.ScoreType scoreType, float value)
        {
            SetEnv(env);
            _scoreType = scoreType;
            this.value = value;
        }


        public override void Execute()
        {
            lastScore = (Score)_env.PlayerStats.Score.Clone();
            _env.PlayerStats.Score.UpdateScore(_scoreType, value);
            newScore = (Score)_env.PlayerStats.Score.Clone();
        }

        public override void Undo()
        {
            _env.PlayerStats.Score.RemovePoints(value);

        }
    }
}
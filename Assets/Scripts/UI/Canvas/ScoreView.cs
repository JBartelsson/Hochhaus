using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WeekSystem;

namespace UI.Canvas
{
    public class ScoreView : MonoBehaviour, ISubscriber
    {
        [SerializeField] TextMeshProUGUI pointsText;
        [SerializeField] TextMeshProUGUI multText;
        [FormerlySerializedAs("totalText")] [SerializeField] TextMeshProUGUI paintingScoreText;
        [SerializeField] TextMeshProUGUI totalText;
        [SerializeField] TextMeshProUGUI targetText;
        
        public void InitSubscriptions()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
            env.RoundStats.Score.OnPointsChanged += ScoreOnOnPointsChanged;
            env.RoundStats.Score.OnMultiplierChanged += ScoreOnOnMultiplierChanged;
            env.RoundStats.Score.OnScoreChanged += ScoreOnOnScoreChanged;
            env.WeekManager.OnWeekChanged += OnWeekChanged;

        }

        private void OnWeekChanged(object sender, WeekManager e)
        {
            targetText.text = e.GetCurrentWeekInfo().ToString();
        }

        private void ScoreOnOnScoreChanged(object sender, Score score)
        {
            paintingScoreText.text = score.PaintingScore.ToString();
            totalText.text = score.TotalScore.ToString();
        }

        private void ScoreOnOnMultiplierChanged(object sender, Score score)
        {
            multText.text = score.Mult.ToString();

        }

        private void ScoreOnOnPointsChanged(object sender, Score score)
        {
            pointsText.text = score.Points.ToString();

        }

        public void ResetSubscriptions()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();

            env.RoundStats.Score.OnPointsChanged -= ScoreOnOnPointsChanged;
            env.RoundStats.Score.OnMultiplierChanged -= ScoreOnOnMultiplierChanged;
            env.RoundStats.Score.OnScoreChanged -= ScoreOnOnScoreChanged;
        }
    }
}
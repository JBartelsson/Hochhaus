using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WeekSystem;

namespace UI.Canvas
{
    public class ScoreView : UIBase, ISubscriber
    {
        [SerializeField] TextMeshProUGUI pointsText;
        [SerializeField] TextMeshProUGUI multText;
        [FormerlySerializedAs("totalText")] [SerializeField] TextMeshProUGUI paintingScoreText;
        [SerializeField] TextMeshProUGUI totalText;
        [SerializeField] TextMeshProUGUI targetText;
        
        public override void InitSubscriptions(UIController uiController)
        {
            base.InitSubscriptions(uiController);

            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
            env.PlayerStats.Score.OnPointsChanged += ScoreOnOnPointsChanged;
            env.PlayerStats.Score.OnMultiplierChanged += ScoreOnOnMultiplierChanged;
            env.PlayerStats.Score.OnScoreChanged += ScoreOnOnScoreChanged;
            env.WeekManager.OnWeekChanged += OnWeekChanged;

        }

        private void OnWeekChanged(object sender, WeekManager e)
        {
            targetText.text = e.GetCurrentWeekInfo().ToString();
        }

        private void ScoreOnOnScoreChanged(object sender, Score score)
        {
            
            Debug.Log(score.Points);
            Debug.Log(score.Mult);
            Debug.Log(score.RoomScore);
            paintingScoreText.text = score.RoomScore.ToString();
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

        public override void ResetSubscriptions()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();

            env.PlayerStats.Score.OnPointsChanged -= ScoreOnOnPointsChanged;
            env.PlayerStats.Score.OnMultiplierChanged -= ScoreOnOnMultiplierChanged;
            env.PlayerStats.Score.OnScoreChanged -= ScoreOnOnScoreChanged;
        }
    }
}
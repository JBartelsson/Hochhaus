using TMPro;
using UnityEngine;

namespace UI.Canvas
{
    public class ScoreView : MonoBehaviour, ISubscriber
    {
        [SerializeField] TextMeshProUGUI pointsText;
        [SerializeField] TextMeshProUGUI multText;
        [SerializeField] TextMeshProUGUI totalText;
        
        public void InitSubscriptions()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
            env.Score.OnPointsChanged += ScoreOnOnPointsChanged;
            env.Score.OnMultiplierChanged += ScoreOnOnMultiplierChanged;
            env.Score.OnScoreChanged += ScoreOnOnScoreChanged;

        }

        private void ScoreOnOnScoreChanged(object sender, float e)
        {
            totalText.text = e.ToString();
        }

        private void ScoreOnOnMultiplierChanged(object sender, float e)
        {
            multText.text = e.ToString();

        }

        private void ScoreOnOnPointsChanged(object sender, int e)
        {
            pointsText.text = e.ToString();

        }

        public void ResetSubscriptions()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();

            env.Score.OnPointsChanged -= ScoreOnOnPointsChanged;
            env.Score.OnMultiplierChanged -= ScoreOnOnMultiplierChanged;
            env.Score.OnScoreChanged -= ScoreOnOnScoreChanged;
        }
    }
}
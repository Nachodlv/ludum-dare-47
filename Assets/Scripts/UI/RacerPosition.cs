using TMPro;
using UnityEngine;

namespace UI
{
    public class RacerPosition : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private TextMeshProUGUI racerName;

        private static readonly Color PlayerColor = new Color(0, 0.7394791f, 1);

        public void FillWithRacer(Racer racer, int position)
        {
            racerName.text = racer.RacerName;
            positionText.text = position.ToString();

            if (!racer.IsPlayer) return;
            racerName.color = PlayerColor;
            positionText.color = PlayerColor;
        }
    }
}

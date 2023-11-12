using UnityEngine;

namespace RFW.Levels
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelFinishTrigger : MonoBehaviour
    {
        public System.Action OnLevelFinish { get; set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("player"))
            {
                OnLevelFinish?.Invoke();
            }
        }
    }
}
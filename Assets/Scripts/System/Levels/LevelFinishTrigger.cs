using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelFinishTrigger : MonoBehaviour
{
    [SerializeField] private string _soundOpenId = "Open";
    //ToDo: Move sounds to sound event system
    private IAudioPlayer _soundFinish = null;

    private void Start()
    {
        _soundFinish = GetComponent<IAudioPlayer>();
    }

    public System.Action OnLevelFinish { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            //ToDo: Remake hardcode to switch variant
            _soundFinish?.PlaySound(_soundOpenId);
            OnLevelFinish?.Invoke();
        }
    }
}
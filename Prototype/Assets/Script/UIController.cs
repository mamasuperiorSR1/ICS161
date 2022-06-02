using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public JetController jetController;
    [SerializeField] public Destructible destructible;
    [SerializeField] public score score;
    public Text speedText;
    public Text heightText;
    public Text throttleText;
    public Text crash;
    public Text ringCount;

    void Update()
    {
        if (!destructible.destroyed)
        {
            speedText.text = $"{jetController.speed:n0}";
            heightText.text = $"{jetController.height:n0}";
            throttleText.text = $"{jetController.throttle:n0}";
            ringCount.text = $"{score.count:n0}/10";
            crash.text = $"";
        }
        else
        {
            crash.text = $"CRASHED!";
        }
    }
}

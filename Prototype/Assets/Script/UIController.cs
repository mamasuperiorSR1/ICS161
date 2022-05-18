using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public JetController jetController;
    public Text speedText;
    public Text heightText;
    public Text throttleText;

    void Update()
    {
        speedText.text = $"Speed: {jetController.speed:n0}";
        heightText.text = $"Height: {jetController.height:n0}";
        throttleText.text = $"Throttle: {jetController.throttle:n0}";
    }
}

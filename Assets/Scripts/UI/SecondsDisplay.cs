using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SecondsDisplay : MonoBehaviour
{

#pragma warning disable CS0649
    [SerializeField]
    private TimerReference _timerReference;
#pragma warning restore CS0649

    private Text _textComp;

    private void Awake()
    {

        _textComp = gameObject.GetComponent<Text>();

    }

    private void Update()
    {

        _textComp.text = $"{_timerReference.timer} Seconds";

    }

}

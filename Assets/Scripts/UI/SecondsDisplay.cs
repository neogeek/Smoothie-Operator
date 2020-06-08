using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmoothieOperator
{

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

            var timer = TimeSpan.FromSeconds(_timerReference.timer);

            _textComp.text = $"{timer.TotalSeconds} Seconds";

        }

    }

}

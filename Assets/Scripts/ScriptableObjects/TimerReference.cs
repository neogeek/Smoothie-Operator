using UnityEngine;

[CreateAssetMenu(fileName = "TimerReference", menuName = "TimerReference")]
public class TimerReference : ScriptableObject
{

    public float timer;

    public void Reset()
    {

        timer = 0;

    }

}

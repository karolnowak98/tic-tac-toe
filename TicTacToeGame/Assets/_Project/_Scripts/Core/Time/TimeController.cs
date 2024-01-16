using UnityEngine;

namespace GlassyCode.TTT.Core.Time
{
    public class TimeController : MonoBehaviour, ITimeController
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime; 
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        
        public float RegularTime => UnityEngine.Time.time;
        public float FixedTime => UnityEngine.Time.fixedTime;
        public float UnscaledTime => UnityEngine.Time.unscaledTime;

        public static void Pause()
        {
            UnityEngine.Time.timeScale = 0;
        }

        public static void Unpause()
        {
            UnityEngine.Time.timeScale = 1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TimB
{
    public class MainCount : MonoBehaviour
    {
        //Same Delta Time For All Objects
        public static MainCount instance;
        [HideInInspector]
        public float deltaTime;
        [HideInInspector]
        public float fixedDeltaTime;

        private void Awake()
        {
            instance = instance ?? this;
        }

        private void Start()
        {
            instance = instance ?? this;
            InvokeRepeating("OnTimerEverySecond", 1f, 1f);
            InvokeRepeating("OnTimerEveryHalfSecond", 1f, .5f);
        }

        // Update is called once per frame
        void Update()
        {
            deltaTime = Time.deltaTime;
        }

        private void FixedUpdate()
        {
            fixedDeltaTime = Time.fixedDeltaTime;
        }

        public int IntegerRandom(int from, int to)
        {
            return UnityEngine.Random.Range(from, to);
        }

        public float FloatRandom(float from, float to)
        {
            return UnityEngine.Random.Range(from, to);
        }

        public bool BoolRandom()
        {
            // return 0 == Random.Range(0, 2);
            return UnityEngine.Random.value > 0.5f;
        }
        public int PositiveNegativeRandom()
        {
            return BoolRandom() ? 1 : -1;
        }
        public System.Guid TakeId()
        {
            return System.Guid.NewGuid();
        }

        public int DifferentWeightRandom(int[] weights)
        {
            int randomedValue = IntegerRandom(0, weights.Sum());
            for (int i = 0; i < weights.Length; i++)
            {
                randomedValue -= weights[i];
                if (randomedValue <= 0)
                {
                    return i;
                }
            }
            return weights.Length;
        }

        public int DifferentWeightRandom(List<int> weights)
        {
            if(weights.Count>0)
           return DifferentWeightRandom(weights.ToArray());
            return 0;
        }


        //public delegate void TimerEverySecondEventHandler(object source, EventArgs e);

        //public event TimerEverySecondEventHandler TimerEverySecond;

        public EventHandler<EventArgs> TimerEverySecond;

        protected virtual void OnTimerEverySecond()
        {
            if (TimerEverySecond != null)
            {
                TimerEverySecond(this, EventArgs.Empty);
            }
        }

        public EventHandler<EventArgs> TimerEveryHalfSecond;

        protected virtual void OnTimerEveryHalfSecond()
        {
            if (TimerEveryHalfSecond != null)
            {
                TimerEveryHalfSecond(this, EventArgs.Empty);
            }
        }

        public float Distance(Transform from, Transform to)
        {
            return Vector3.Distance(from.position, to.position);
        }

        public bool IsOutRanged(Transform from, Transform to, float range)
        {
            return Distance(from, to)>range;
        }
    }
}

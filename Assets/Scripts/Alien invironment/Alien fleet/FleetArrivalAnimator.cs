using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Unit;
using Random = UnityEngine.Random;

namespace AlienInvironment.Fleet
{
    public class FleetArrivalAnimator : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;

        private Vector3 BottomCentreOfScreen;

        private Coroutine arrivalAnimation = null;

        public UnityEvent OnAnimationStart;
        public UnityEvent OnAnimationOver;

        private void Awake()
        {
            BottomCentreOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * Screen.width, 0f));
        }

        public void Animate(List<Alien> Aliens)
        {
            if (arrivalAnimation != null)
                StopCoroutine(arrivalAnimation);

            Vector2[] startAliensPositions = new Vector2[Aliens.Capacity];
            Vector2[] endAliensPositions = new Vector2[Aliens.Capacity];

            for (var i = 0; i < Aliens.Capacity; i++)
            {
                endAliensPositions[i] = Aliens[i].GetComponent<AlienIdleAnimation>().GetNextPosition();

                Transform AlienTransform = Aliens[i].transform;

                Vector3 AlienPosition = BottomCentreOfScreen - AlienTransform.position * 2;
                AlienPosition += new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);

                AlienTransform.position -= AlienPosition;

                startAliensPositions[i] = AlienTransform.position;
            }

            arrivalAnimation = StartCoroutine(Animator(Aliens, startAliensPositions, endAliensPositions));
        }

        private IEnumerator Animator(List<Alien> Aliens, Vector2[] startAliensPositions, Vector2[] endAliensPositions)
        {
            OnAnimationStart.Invoke();

            float _lerpTime = 0f;

            while (_lerpTime <= 1)
            {
                _lerpTime += Time.fixedDeltaTime * _speed;

                for (var i = 0; i < Aliens.Capacity; i++)
                {
                    try
                    {
                        Vector3 AlienPosition = Vector2.Lerp(startAliensPositions[i], endAliensPositions[i], _lerpTime);

                        Aliens[i].transform.position = AlienPosition;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }
                }

                yield return new WaitForFixedUpdate();
            }

            OnAnimationOver.Invoke();
        }
    }
}
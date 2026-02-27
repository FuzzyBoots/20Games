using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders
{
    public class FormationScript : MonoBehaviour
    {
        [SerializeField] float _horizontalMovementTime = 1f;
        [SerializeField] float _horizontalMovementDelay = 0.5f;
        [SerializeField] float _verticalMovementTime = 0.5f;
        [SerializeField] float _verticalMovementDelay = 0.5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(MoveFormation());
        }

        private IEnumerator MoveFormation()
        {
            while (true)
            {
                Debug.Log("Moving right");
                // Move the formation to the right 2 units over 1 second
                float endTime = Time.time + _horizontalMovementTime;
                while (Time.time < endTime)
                {
                    transform.Translate(Vector3.right * 2f * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_horizontalMovementDelay);

                endTime = Time.time + _verticalMovementTime;
                while (Time.time < endTime)
                {
                    transform.Translate(Vector3.back * 1f * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_verticalMovementDelay); ;

                Debug.Log("Moving left");
                // Move the formation to the left 2 units over 1 second
                endTime = Time.time + _horizontalMovementTime;
                while (Time.time < endTime)
                {
                    transform.Translate(Vector3.left * 2f * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_horizontalMovementDelay);

                endTime = Time.time + _verticalMovementTime;
                while (Time.time < endTime)
                {
                    transform.Translate(Vector3.back * 1f * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_verticalMovementDelay);
            }
        }
    }
}
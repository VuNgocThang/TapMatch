using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using UnityEditor.ShaderGraph.Internal;

namespace ThangVN
{
    public class Square : MonoBehaviour
    {
        public int index;
        public Vector3 originalPos;
        public SpriteRenderer image;
        public bool canClick;

        public void Move(Transform parent, Action checkEat = null)
        {
            transform.SetParent(parent);
            transform.DOLocalMove(Vector3.zero, 0.3f)
                .OnComplete(() =>
                {
                    checkEat?.Invoke();
                });
        }
        private void Update()
        {
            CheckBoxCast();
        }
        public void CheckBoxCast()
        {

            RaycastHit raycastHit;
            float maxDistance = 1f;
            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, -transform.forward, out raycastHit, Quaternion.identity, maxDistance);

            if (isHit)
            {
                canClick = false;
                image.color = new Color(1, 1, 1, 0.5f);
                transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                canClick = true;
                image.color = new Color(1, 1, 1, 1f);
                transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThangVN
{
    public class SelectLevel : MonoBehaviour
    {
        public Button btnOke;
        public TMP_InputField inputField;

        private void Start()
        {
            btnOke.onClick.AddListener(MoveToLevel);
        }

        void MoveToLevel()
        {
            Debug.Log(inputField.text);
        }
    }
}

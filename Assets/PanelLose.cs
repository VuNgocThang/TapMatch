using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThangVN
{
    public class PanelLose : MonoBehaviour
    {
        public Button btnNewGameLose;

        private void Start()
        {
            btnNewGameLose.onClick.AddListener(LogicUI.Ins.ReplayGame);
        }

    }
}

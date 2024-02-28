using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThangVN
{
    public class PanelWin : MonoBehaviour
    {
        public Button btnNewGameWin;
        private void Start()
        {
            btnNewGameWin.onClick.AddListener(LogicUI.Ins.NewGame);
        }
    }
}

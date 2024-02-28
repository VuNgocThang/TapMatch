using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThangVN
{
    public class PanelUIInGame : MonoBehaviour
    {
        public Button btnReplay;
        public Button btnUndo;
        public Button btnHint;
        public Button btnShuffle;
        public TextMeshProUGUI txtLevel;
        public TextMeshProUGUI txtCountSquare;

        private void Start()
        {
            btnReplay.onClick.AddListener(LogicUI.Ins.ReplayGame);
            btnUndo.onClick.AddListener(LogicGame.Instance.Undo);
            btnHint.onClick.AddListener(LogicGame.Instance.Hint);
            btnShuffle.onClick.AddListener(LogicGame.Instance.Shuffle);
        }

        private void Update()
        {
            txtLevel.text = (LogicGame.Instance.indexLevel + 1).ToString();
            txtCountSquare.text = (LogicGame.Instance.listSquaresInGame.Count).ToString();
        }
    }
}

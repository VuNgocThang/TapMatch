using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ThangVN
{
    public class LogicUI : MonoBehaviour
    {
        public Button btnReplay;
        public Button btnUndo;
        public Button btnHint;
        public Button btnShuffle;

        public Button btnNewGameWin;
        public Button btnNewGameLose;

        public GameObject panelWin;
        public GameObject panelLose;

        private void Start()
        {
            btnReplay.onClick.AddListener(ReplayGame);
            btnUndo.onClick.AddListener(LogicGame.Instance.Undo);
            btnHint.onClick.AddListener(LogicGame.Instance.Hint);
            btnShuffle.onClick.AddListener(LogicGame.Instance.Shuffle);

            btnNewGameWin.onClick.AddListener(ReplayGame);
            btnNewGameLose.onClick.AddListener(ReplayGame);
        }

        void ReplayGame()
        {
            SceneManager.LoadScene("SceneGame");
        }
    }
}

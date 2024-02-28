using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ThangVN
{
    public class LogicUI : MonoBehaviour
    {
        public static LogicUI Ins;
        public PanelUIInGame panelUiInGame;
        public PanelWin panelWin;
        public PanelLose panelLose;

        private void Awake()
        {
            Ins = this;
        }
      
        public void ReplayGame()
        {
            SceneManager.LoadScene("SceneGameTapMatch");
        }

        public void NewGame()
        {
            panelWin.gameObject.SetActive(false);
            StartCoroutine(LoadData());
        }

        IEnumerator LoadData()
        {
            yield return new WaitForSeconds(1f);
            LogicGame.Instance.LoadNewData();
            Debug.Log("new game!!!");
        }
    }
}

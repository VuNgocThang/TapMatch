using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace ThangVN
{
    public class LogicGame : MonoBehaviour
    {
        public static LogicGame Instance;

        public LogicUI logicUI;

        public Square prefabSquare;
        public List<int> indexes;
        public List<Sprite> sprites;
        public LayerMask layerMask;
        public List<Square> listGOStored = new List<Square>();
        public List<Transform> listPoints = new List<Transform>();
        public List<Square> listSquaresInGame = new List<Square>();
        public List<Square> listSquareUndo = new List<Square>();

        public int indexLevel;
        public LevelSetMap level;
        public List<LevelSetMap> listLevel;

        public bool checkLose;
        public bool checkWin;
        public bool canEat;
        public bool canClick;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
            InitLevel();
            InitSquare();
        }
        void InitLevel()
        {
            indexLevel = 0;
            level = Instantiate(listLevel[0], transform);
        }
        void InitSquare()
        {
            for (int i = 0; i < level.listSquareLV.Count; i++)
            {
                level.listSquareLV[i].index = indexes[i];
                int z = indexes[i];
                level.listSquareLV[i].image.sprite = sprites[z];
                listSquaresInGame.Add(level.listSquareLV[i]);
            }

        }


        private void Update()
        {
            OnClick();
        }

        void OnClick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit raycastHit;
                bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000f, layerMask);

                if (isHit)
                {
                    Square square = raycastHit.collider.GetComponent<Square>();
                    if (!square.canClick || canClick) return;

                    square.originalPos = square.transform.position;
                    Move(square);

                }
            }
        }

        void Move(Square square)
        {
            if (listGOStored.Count > 6 && !isHint) return;

            listSquaresInGame.Remove(square);
            listSquareUndo.Add(square);

            if (listGOStored.Contains(square)) return;
            listGOStored.Add(square);

            List<Square> tempB = new List<Square>();
            int c = listGOStored.Count;
            while (tempB.Count < c)
            {
                tempB.Add(listGOStored[0]);
                listGOStored.RemoveAt(0);

                for (int i = 0; i < listGOStored.Count; i++)
                {
                    if (tempB[tempB.Count - 1].index == listGOStored[i].index)
                    {
                        tempB.Add(listGOStored[i]);
                        listGOStored.RemoveAt(i);
                        --i;
                    }
                }
            }

            listGOStored = tempB;

            CanEat();

            for (int i = 0; i < listGOStored.Count; i++)
            {
                listGOStored[i].Move(listPoints[i], CheckEat);
            }
            CheckLose();
        }

        void CanEat()
        {
            for (int i = 0; i < listGOStored.Count - 2; ++i)
            {
                if (listGOStored[i].index == listGOStored[i + 1].index && listGOStored[i + 1].index == listGOStored[i + 2].index)
                {
                    canEat = true;
                }
            }
        }

        void CheckEat()
        {

            for (int i = 0; i < listGOStored.Count - 2; i++)
            {
                if (listGOStored[i].index == listGOStored[i + 1].index && listGOStored[i + 1].index == listGOStored[i + 2].index)
                {

                    isHint = false;

                    var g1 = listGOStored[i];
                    var g2 = listGOStored[i + 1];
                    var g3 = listGOStored[i + 2];

                    g1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.31f);
                    g2.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.31f);
                    g3.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f)
                       .OnComplete(() =>
                       {
                           g1.gameObject.SetActive(false);
                           g2.gameObject.SetActive(false);
                           g3.gameObject.SetActive(false);
                           //listSquaresInGame.Remove(g3);

                           listGOStored.Remove(g1);
                           listGOStored.Remove(g2);
                           listGOStored.Remove(g3);

                           listSquareUndo.Remove(g1);
                           listSquareUndo.Remove(g2);
                           listSquareUndo.Remove(g3);

                           CheckDone();
                       });

                    i += 2;
                }
            }
        }

        void CheckDone()
        {
            canEat = false;
            for (int i = 0; i < listGOStored.Count; ++i)
            {
                listGOStored[i].Move(listPoints[i]);
            }
            hinting = false;
            CheckWin();
        }

        void CheckLose()
        {
            if (checkLose || listGOStored.Count <= 6 || canEat || isHint) return;

            Debug.Log("you lose");
            checkLose = true;
            logicUI.panelLose.SetActive(true);
        }

        void CheckWin()
        {
            if (!checkLose && !checkWin && listSquaresInGame.Count <= 0)
            {
                Debug.Log("you Win");
                checkWin = true;
                logicUI.panelWin.SetActive(true);
            }
        }

        public void Undo()
        {
            if (listSquareUndo.Count <= 0 || checkLose || checkWin || canEat) return;

            int index = listSquareUndo.Count - 1;
            Square square = listSquareUndo[index];
            square.transform.DOMove(square.originalPos, 0.3f);
            square.transform.SetParent(level.transform);

            listGOStored.Remove(square);
            listSquareUndo.RemoveAt(index);
            listSquaresInGame.Add(square);

            for (int i = 0; i < listGOStored.Count; i++)
            {
                listGOStored[i].Move(listPoints[i], CheckDone);
            }

        }

        public bool isHint = false;
        public bool hinting = false;
        public void Hint()
        {
            if (checkLose || canEat || listGOStored.Count > 6) return;
            if (hinting) return;

            isHint = true;

            if (listGOStored.Count > 0)
            {
                if (listGOStored.Count > 1)
                {
                    if (listGOStored[0].index == listGOStored[1].index)
                    {
                        for (int i = 0; i < listSquaresInGame.Count; i++)
                        {
                            if (listGOStored[0].index == listSquaresInGame[i].index)
                            {
                                Move(listSquaresInGame[i]);

                                hinting = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < listSquaresInGame.Count; i++)
                        {
                            for (int j = i + 1; j < listSquaresInGame.Count; j++)
                            {
                                if (listGOStored[0].index == listSquaresInGame[i].index && listGOStored[0].index == listSquaresInGame[j].index)
                                {
                                    Move(listSquaresInGame[i]);
                                    Move(listSquaresInGame[j - 1]);

                                    hinting = true;
                                    return;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < listSquaresInGame.Count; i++)
                {
                    for (int j = i + 1; j < listSquaresInGame.Count; j++)
                    {
                        if (listGOStored[0].index == listSquaresInGame[i].index && listGOStored[0].index == listSquaresInGame[j].index)
                        {
                            Move(listSquaresInGame[i]);
                            Move(listSquaresInGame[j - 1]);
                            hinting = true;

                            return;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < listSquaresInGame.Count; i++)
                {
                    for (int j = i + 1; j < listSquaresInGame.Count; j++)
                    {
                        for (int k = j + 1; k < listSquaresInGame.Count; k++)
                        {
                            if (listSquaresInGame[i].index == listSquaresInGame[j].index && listSquaresInGame[j].index == listSquaresInGame[k].index)
                            {
                                Move(listSquaresInGame[i]);
                                Move(listSquaresInGame[j - 1]);
                                Move(listSquaresInGame[k - 2]);
                                hinting = true;

                                return;
                            }
                        }
                    }
                }

            }
        }

        public bool isShuffleing = false;
        public bool canShuffle = true;
        List<Vector3> listNewPosShuffle = new List<Vector3>();
        public void Shuffle()
        {
            if (checkWin || isShuffleing) return;

            listNewPosShuffle.Clear();
            List<Square> list = new List<Square>();
            int r;
            while (listSquaresInGame.Count > 0)
            {
                r = Random.Range(0, listSquaresInGame.Count);
                list.Add(listSquaresInGame[r]);
                listSquaresInGame.RemoveAt(r);
            }

            listSquaresInGame.AddRange(list);

            while (list.Count > 0)
            {
                int i = Random.Range(0, list.Count);
                listNewPosShuffle.Add(list[i].transform.position);
                list.RemoveAt(i);
            }

            for (int i = 0; i < listSquaresInGame.Count; i++)
            {
                int currentIndex = i;
                if (canShuffle)
                {
                    Physics.autoSimulation = false;
                    listSquaresInGame[currentIndex].transform.DOMove(listNewPosShuffle[currentIndex], 0.3f)
                        .OnStart(() =>
                        {
                            canShuffle = false;
                            canClick = false;
                            isShuffleing = true;
                        })

                        .OnComplete(() =>
                        {
                            Physics.autoSimulation = true;
                            canShuffle = true;
                            canClick = true;
                            isShuffleing = false;
                        });
                }
            }
        }
    }

}

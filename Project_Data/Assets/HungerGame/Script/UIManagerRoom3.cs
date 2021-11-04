using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManagerRoom3 : MonoBehaviour
{
    public GameData gameData;

    [Header("This area is for button clue")]
    public Button cluePuzzleBtn1;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button clueBtn4;
    public Button clueBtn5;
    public Button clueBtn6;
    public Button clueBtn7;
    public Button clueBtn8;
    public Button clueBtn9;
    public Button clueBtn10;
    public Button clueBtn11;
    public Button clueBtn12;
    public Button clueBtn13;
    public Button clueBtn14;
    public Button clueBtn15;
    public Button clueBtn16;
    public Button clueBtn17;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;
    public RectTransform clue4;
    public RectTransform clue5;
    public RectTransform clue6;
    public RectTransform clue7;
    public RectTransform clue8;
    public RectTransform clue9;
    public RectTransform clue10;
    public RectTransform clue11;
    public RectTransform clue12;
    public RectTransform clue13;
    public RectTransform clue14;
    public RectTransform clue15;
    public RectTransform clue16;
    public RectTransform clue17;

    [Header("This area is for all correct panel")]
    public RectTransform answerPanel1;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput1;
    public Text notificationDisplay1;

    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzleAnswer1;

    [Header("Check this variable if this is the last puzzle room in the game")]
    public bool lastRoom = false;

    private bool clue1On = false;
    private bool clue2On = false;
    private bool clue3On = false;
    private bool clue4On = false;
    private bool clue5On = false;

    //new system here
    public float tweenDelay = 1f;
    public List<RectTransform> openWindow = new List<RectTransform>();

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the begining of the scene
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        
        clueBtn1.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => NonPuzzleClue(clue3));
        clueBtn4.onClick.AddListener(() => NonPuzzleClue(clue4));
        clueBtn5.onClick.AddListener(() => NonPuzzleClue(clue5));
        clueBtn6.onClick.AddListener(() => NonPuzzleClue(clue6));
        clueBtn7.onClick.AddListener(() => NonPuzzleClue(clue7));
        clueBtn8.onClick.AddListener(() => NonPuzzleClue(clue8));
        clueBtn9.onClick.AddListener(() => NonPuzzleClue(clue9));
        clueBtn10.onClick.AddListener(() => NonPuzzleClue(clue10));
        clueBtn11.onClick.AddListener(() => NonPuzzleClue(clue11));
        clueBtn12.onClick.AddListener(() => NonPuzzleClue(clue12));
        clueBtn13.onClick.AddListener(() => NonPuzzleClue(clue13));
        clueBtn14.onClick.AddListener(() => NonPuzzleClue(clue14));
        clueBtn15.onClick.AddListener(() => NonPuzzleClue(clue15));
        clueBtn16.onClick.AddListener(() => NonPuzzleClue(clue16));
        clueBtn17.onClick.AddListener(() => NonPuzzleClue(clue17));

        CheckButton();
    }

    void CheckButton()
    {
        bool isComplete = GameData.instance.room3PuzzleOpen[1];

        if (isComplete)
        {
            clueBtn2.GetComponent<Image>().color = Color.white;
            clueBtn2.interactable = true;
        }
        
    }
    
    // This is a fungtion to check puzzle 1 answer, if correct a new clue will be open and player can advance to the new map
    public void CheckingAnswer1(bool canAdvance)
    {
        var a = 0;

        for (int i = 0; i < puzzleAnswer1.Length; i++)
        {
            if (puzzleInput1.text.Equals(puzzleAnswer1[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            GameData.instance.room3PuzzleOpen[0] = true;
            SwitchCorrect1(true);
            AnswerCorrect(canAdvance);
        }
        else
        {
            StartCoroutine(AnswerFalse1());
        }
    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.room3PuzzleOpen[0];

        if (isComplete)
        {
            SwitchCorrect1(true);
        }
        else
        {
            ClosePanel();
            OpenPanel(panel);
        }
    }

    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }

    //This function is used to increase the game progression data and close all clue window the is open.
    //It is also serve to check if the last puzzle already solve or not, if yes then the game finish time will be calculated
    public void AnswerCorrect(bool continueMap)
    {
        if (continueMap && !lastRoom)
        {
            gameData.gamePhase++;
        }

        if (clue1On)
        {
            clue1On = false;
        }
    }

    public void CalculateFinishTime()
    {
        if (lastRoom)
        {
            gameData.CalculateFinishTime();
        }
    }

    //This coroutine is used to add a message for the player when player put a wrong answer in puzzle 1 input
    IEnumerator AnswerFalse1()
    {
        string prev = notificationDisplay1.text;

        notificationDisplay1.text = "Nothing happened";
        notificationDisplay1.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay1.text = prev;
        notificationDisplay1.color = Color.black;
    }

    // This function is used to activate a clue when puzzle 1 answer is correct
    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            ClosePanel();
            OpenPanel(answerPanel1);
            clue1On = true;
        }
        else
        {
            ClosePanel();
        }

    }

    void OpenPanel(RectTransform rt)
    {
        rt.DOAnchorPos(Vector2.zero, tweenDelay);
        openWindow.Add(rt);
    }

    public void ClosePanel()
    {
        for (int i = 0; i < openWindow.Count; i++)
        {
            openWindow[i].DOAnchorPos(new Vector2(0, 2000), tweenDelay);
        }

        openWindow.Clear();
    }

}

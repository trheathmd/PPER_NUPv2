using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;
using TMPro;
using GameVanilla.Game.Scenes;
public enum QuestionType
{
    TrueFalse,
    MultipleChoice
}

public class Question
{
    QuestionType QuestionType;
    public string _question;
    public List<string> options;
    public string correctAnswer;
}


public class QuestionManager : MonoBehaviour
{
    int stage = 1;
    public List<Question> questionsList;
    public GameObject TrueFalsePopUp;
    public GameObject MultipleChoicePopup;
    [SerializeField] GameObject HintPopUp;
    [SerializeField] TextMeshProUGUI _hintText;
    [SerializeField] GameObject CorrectAnswerPopUp;
    QuizQuestion quizQuestion;
    public static QuestionManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void OnEnable()
    {
      
        PopQuestionData(GameBoard.instance.questionID);
        GameBoard.instance.questionID++;
        if (GameBoard.instance.questionID >= 46)
        {
            QuestionDownloader._instance.DownloadQuestions(PuzzleMatchManager.instance.lastSelectedLevel);
            GameBoard.instance.questionID = 0;
        }
    }

    // Start is called before the first frame update

    QuestionPopup _currentPopup;
    public void PopQuestionData(int index)
    {
        var QuestionData = QuestionDownloader._instance.GetQuestionList(stage);
        quizQuestion = QuestionData[index];
        

        if(quizQuestion.question_type == "Multiple Choice")
        {
            MultipleChoicePopup.SetActive(true);
            _currentPopup = MultipleChoicePopup.GetComponent<QuestionPopup>();
            _currentPopup.Initialize(quizQuestion);
        }
        else
        {
            TrueFalsePopUp.SetActive(true);
            _currentPopup = TrueFalsePopUp.GetComponent<QuestionPopup>();
            _currentPopup.Initialize(quizQuestion);
        }
    }

    public void CloseHint()
    {
        var animator = HintPopUp.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Close");
            StartCoroutine(DestroyPopup());
        }
    }

    public void EnableHint()
    {
        HintPopUp.SetActive(true);
        _hintText.text = string.IsNullOrWhiteSpace(quizQuestion.hint_text) ? "Sorry there is no hint available for this question" : quizQuestion.hint_text;
    }

    protected virtual IEnumerator DestroyPopup()
    {
            yield return new WaitForSeconds(0.5f);
            HintPopUp.SetActive(false);

    }

    public void OnAnsweredWrong()
    {
        CorrectAnswerPopUp.SetActive(true);
        CorrectAnswerPopUp.GetComponent<CorrectAnswerPopUp>().Initialize(quizQuestion.answer_response_paragraph);
    }

    public void closeAllPanels() {
        GameBoard.instance.ReduceAttempts();
        if (GameBoard.instance.lives == 0)
        {
            print("Game over");
            GameScene.instance.OpenLosePopup();
            GameBoard.instance.lives = 3;
        }
        _currentPopup.GetBoard().QuestionAnswered(false);
        _currentPopup.Close();
        CorrectAnswerPopUp.GetComponent<Animator>().Play("Close");
        Resume();
    }
    public void Resume()
    {
        if (GameBoard.instance.level.limitType == LimitType.Time)
        {
            GameBoard.instance.ResumeTimer();
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

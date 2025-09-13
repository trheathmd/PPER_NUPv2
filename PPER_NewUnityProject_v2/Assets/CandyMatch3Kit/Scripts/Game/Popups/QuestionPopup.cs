using GameVanilla.Core;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.Common;
using TMPro;
using System.Collections;
using UnityEngine;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when a player tries to exit a game.
    /// </summary>
    public abstract class QuestionPopup : Popup
    {
        private GameBoard gameBoard;
        public TextMeshProUGUI _category;
        public TextMeshProUGUI _question;
        public TextMeshProUGUI _questionParagraph;
        public TextMeshProUGUI _correctAnswer;

        private void OnEnable()
        {
            // Replace obsolete FindObjectOfType with FindFirstObjectByType
            gameBoard = FindFirstObjectByType<GameBoard>();
        }

        public abstract void Initialize(QuizQuestion quizQuestion);

        /// <summary>
        /// Called when an answer is given.
        /// </summary>
        public virtual void OnQuestionAnswered(string answer = null)
        {
            if (answer.ToLower() == "true")
            {
                gameBoard.QuestionAnswered(true);
                Close();
                Resume();
            }

            else
            {
                QuestionManager.instance.OnAnsweredWrong();
            }
        }
        public GameBoard GetBoard()
        {
            return gameBoard;
        }

        public void Resume()
        {
            if (GameBoard.instance.level.limitType == LimitType.Time)
            {
                GameBoard.instance.ResumeTimer();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameVanilla.Game.Popups
{
    public class TrueFalseQuestion : QuestionPopup
    {
        QuizQuestion question = null;
        public override void OnQuestionAnswered(string answer)
        {
            if (answer.ToLower() == "true" && question.answer_option_1_correct.ToString() == "1")
            {
                answer = "true";
            }
            else if (answer.ToLower() == "false" && question.answer_option_2_correct.ToString() == "1")
            {
                answer = "true";
            }
            else
            {
                answer = "false";

            }
            base.OnQuestionAnswered(answer);
            QuestionDownloader._instance.PostAnswer(answer.ToLower() == "true"?0:1, answer.ToLower() == "true" ? 1 : 0 , question.question_id);
        }

        public override void Initialize(QuizQuestion quizQuestion)
        {
            question = quizQuestion;
            _category.text = quizQuestion.category;
            _question.text = quizQuestion.question_paragraph;
           // _questionParagraph.text = quizQuestion.question_paragraph;
            quizQuestion.answer_option_1_correct = quizQuestion.answer_option_1_correct == 1.ToString() ? 1.ToString() : "0";
            quizQuestion.answer_option_2_correct = quizQuestion.answer_option_2_correct == 1.ToString() ? 1.ToString() : "0";
        }
    
    }
}

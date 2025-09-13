using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace GameVanilla.Game.Popups
{
    public class MultipleChoiceQuestion : QuestionPopup
    {
        int selectedAnswer =0;
        public TextMeshProUGUI[] choices;
        QuizQuestion question;

        public void UpdateSelectedAnswer(int answer)
        {
            selectedAnswer = answer;
        }

        public override void OnQuestionAnswered(string answer)
        {
            if (selectedAnswer == 0) return;
            if (selectedAnswer == 1 && question.answer_option_1_correct.ToString() == "1")
            {
                answer = "true";
            }
            else if (selectedAnswer == 2 && question.answer_option_2_correct.ToString() == "1")
            {
                answer = "true";
            }
            else if (selectedAnswer == 3 && question.answer_option_3_correct.ToString() == "1")
            {
                answer = "true";
            }
            else if (selectedAnswer == 4 && question.answer_option_4_correct.ToString() == "1")
            {
                answer = "true";
            }
            else
            {
                answer = "false";

            }
            base.OnQuestionAnswered(answer);
            QuestionDownloader._instance.PostAnswer(selectedAnswer, answer.ToLower() == "true" ? 1: 0 , question.question_id);
        }

        public override void Initialize(QuizQuestion quizQuestion)
        {
            question = quizQuestion;
            _category.text = quizQuestion.category;
            _question.text = quizQuestion.question_paragraph;
           // _questionParagraph.text = quizQuestion.question_paragraph;
            choices[0].text = quizQuestion.answer_option_1 ?? "None";
            choices[1].text = quizQuestion.answer_option_2 ?? "None";
            choices[2].text = quizQuestion.answer_option_3 ?? "None";
            choices[3].text = quizQuestion.answer_option_4 ?? "None";

           quizQuestion.answer_option_1_correct = quizQuestion.answer_option_1_correct == 1.ToString() ? 1.ToString() : "0";
           quizQuestion.answer_option_2_correct = quizQuestion.answer_option_2_correct == 1.ToString() ? 1.ToString() : "0";
           quizQuestion.answer_option_3_correct = quizQuestion.answer_option_3_correct == 1.ToString() ? 1.ToString() : "0";
           quizQuestion.answer_option_4_correct = quizQuestion.answer_option_4_correct == 1.ToString() ? 1.ToString() : "0";
        }

    }
}

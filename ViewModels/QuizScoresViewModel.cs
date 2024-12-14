using System;
using System.Collections.Generic;

namespace UsersApp.ViewModels
{
    public class QuizScoresViewModel
    {
        public int QuizId { get; set; }
        public string Topic { get; set; }
        public List<ScoreRecordViewModel> Scores { get; set; }
    }

    public class ScoreRecordViewModel
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
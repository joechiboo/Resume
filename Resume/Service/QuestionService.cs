using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Resume.Service
{
    public class Question { 
        public int id {get;set;}
        public string question { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string translate { get; set; }
        public string solution { get; set; }
    }

    public class QuestionService
    {
        private string _connection = ConfigurationManager.ConnectionStrings["ToeicConnect"].ConnectionString;
        public Question Get(int id = 0)
        {
            Question model = new Question();
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                var sql = @"
                    DECLARE @AnswerCount INT, @CorrectRate INT, @LastQuestionId INT, @ChooseQuestionId INT
                    SELECT @AnswerCount = COUNT(*) FROM Answers AS w WITH(NOLOCK) WHERE AccountId = @AccountId
                    SELECT 
	                    @CorrectRate = T.CorrectTimes * 100 / T.AnswerTimes
                    FROM (
	                    SELECT AccountId
		                    , COUNT(*) AS AnswerTimes	
		                    , SUM(CASE WHEN Correct = 1 THEN 1 ELSE 0 END) AS CorrectTimes
	                    FROM Answers AS w WITH(NOLOCK) 
	                    WHERE AccountId = @AccountId
	                    GROUP BY AccountId
                    ) AS T
                    SELECT TOP 1 @LastQuestionId = QuestionId FROM Answers AS a WITH(NOLOCK) WHERE AccountId = @AccountId
			
                    -- 若答題數 < 10
	                    -- 或者正確率 > 70% ,取得未作答
                    IF @AnswerCount < 10
	                    OR @CorrectRate > 70
	                    BEGIN
		                    SELECT TOP 1 @ChooseQuestionId = q.id FROM Questions AS q WITH(NOLOCK)
			                    LEFT JOIN Answers AS w WITH(NOLOCK) ON q.id = w.QuestionId AND AccountId = @AccountId
		                    WHERE w.id IS NULL
	                    END
                    -- 若正確率 > 40% ,取得曾經做過題目複習
                    ELSE IF @CorrectRate > 40
	                    BEGIN
		                    SELECT @ChooseQuestionId = QuestionId FROM (
			                    SELECT TOP 1 QuestionId, ABS(CHECKSUM(NewId())) % 100 AS [RandomNumber] FROM (
				                    SELECT DISTINCT QuestionId FROM Answers AS a WITH(NOLOCK) WHERE AccountId = @AccountId 
					                    AND QuestionId <> @LastQuestionId
				                    ) AS T
			                    ORDER BY 2 DESC
		                    ) AS R
	                    END
                    -- 若正確率 < 40% ,取得錯誤題目複習
                    ELSE
	                    BEGIN
		                    SELECT @ChooseQuestionId = QuestionId FROM (
			                    SELECT TOP 1 QuestionId, ABS(CHECKSUM(NewId())) % 100 AS [RandomNumber] FROM (
				                    SELECT DISTINCT QuestionId FROM Answers AS a WITH(NOLOCK) WHERE AccountId = @AccountId
					                    AND Correct = 0
					                    AND QuestionId <> @LastQuestionId
				                    ) AS T
			                    ORDER BY 2 DESC
		                    ) AS R
	                    END
	
                    SELECT TOP 1 * FROM Questions AS q WITH(NOLOCK) WHERE id = @ChooseQuestionId
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@AccountId", System.Data.SqlDbType.Int).Value = id;
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            model.id = rd[rd.GetOrdinal("id")] == DBNull.Value ? 0 : rd.GetInt32(rd.GetOrdinal("id"));
                            model.question = rd[rd.GetOrdinal("question")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("question"));
                            model.option1 = rd[rd.GetOrdinal("option1")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("option1"));
                            model.option2 = rd[rd.GetOrdinal("option2")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("option2"));
                            model.option3 = rd[rd.GetOrdinal("option3")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("option3"));
                            model.option4 = rd[rd.GetOrdinal("option4")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("option4"));
                            model.translate = rd[rd.GetOrdinal("translate")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("translate"));
                            model.solution = rd[rd.GetOrdinal("solution")] == DBNull.Value ? "" : rd.GetString(rd.GetOrdinal("solution"));
                        }
                    }
                }
            }

            return model;
        }

        public void Answer(int id, int questionId, string answer)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    DECLARE @Correct BIT
                    SET @Correct = 0

                    SELECT TOP 1 @Correct = 1 FROM Questions AS q WITH(NOLOCK) WHERE id = @questionId AND solution = @answer
                    INSERT Answers(AccountId, QuestionId, Answer, Correct, CreateTime)VALUES(@id, @questionId, @answer, @Correct, DATEADD(HOUR, 15, GETDATE()))
                ", conn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@questionId", System.Data.SqlDbType.Int).Value = questionId;
                    cmd.Parameters.Add("@answer", System.Data.SqlDbType.VarChar).Value = answer;
                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
    }
}
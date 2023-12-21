using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TechnicalTest.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamScoreController : ControllerBase
    {
        [HttpPost]
        public IActionResult ProcessExamScoresData([FromBody] ExamScores[] examScoresArray)
        {



            List<StudentScores> processedData = processStudentsData(examScoresArray);
            processedData.Sort((a, b) => a.StudentID - b.StudentID);

            // Create a response object
            var response = new
            {
                Message = "Student processed successfully",
                ProcessedStudents = processedData
            };

            return Ok(response.ProcessedStudents);
        }

        private List<StudentScores> processStudentsData(ExamScores[] examScoresArray)
        { 
    
            // Create an instance of the custom comparer
            SubjectScoreComparer comparer = new SubjectScoreComparer();

            //Create list of studentScores to group all the scores of each student
            List<StudentScores> studentScoresArray = new List<StudentScores> { };


            foreach (var examScore in examScoresArray)
            {

                int targetStudentID = examScore.StudentID;

                //Check if student is already exists in the list if yes append and sort his scores
                StudentScores targetStudent = studentScoresArray.FirstOrDefault(x => x.StudentID == targetStudentID);

                if (targetStudent != null)
                {
                    Scores newScore = createNewScore(examScore);
                    targetStudent.Scores.Add(newScore);

                    //Sort scores according to each subject scoring method
                    comparer.getSubjectScoreOrder(targetStudent.Subject);
                    targetStudent.Scores.Sort(comparer);
                    

                }
                else
                {
                 
                    List<Scores> newScoresList = createNewScoresList(examScore);
                    StudentScores newStudent = createNewStundent(examScore);
                    newStudent.Scores = newScoresList;

                    studentScoresArray.Add(newStudent);
                   
                }
            }

            return studentScoresArray;

        }

        private Scores createNewScore(ExamScores targetStudent)
        {
            Scores newScore = new Scores { LearningObjective = targetStudent.LearningObjective, Score = targetStudent.Score };

            return newScore;
        }

        private List<Scores> createNewScoresList(ExamScores targetStudent)
        {
            List<Scores> newScoresList = new List<Scores>{
                new Scores {LearningObjective= targetStudent.LearningObjective, Score= targetStudent.Score}
                };

            return newScoresList;
        }

        private StudentScores createNewStundent(ExamScores targetStudent)
        {
            StudentScores newStudent = new StudentScores
            {
                StudentID = targetStudent.StudentID,
                Name = targetStudent.Name,
                Subject = targetStudent.Subject
            };
            return newStudent;
        }
    }
}

namespace TechnicalTest.Server
{
    public class SubjectScoreComparer : IComparer<Scores>
    {
        private static readonly string[] EnglishScoreOrder = { "8", "7", "6", "5", "4", "3", "2", "1" };
        private static readonly string[] MathsScoreOrder = { "A", "B", "C", "D", "E", "F" };
        private static readonly string[] ScienceScoreOrder = { "Excellent", "Good", "Average", "Poor", "Very Poor" };

        private string[] subjectScoreOrder;
        public void getSubjectScoreOrder(string subject)
        {
            switch (subject)
            {
                case "English":
                    subjectScoreOrder = EnglishScoreOrder;
                    break;
                case "Maths":
                    subjectScoreOrder = MathsScoreOrder;
                    break;
                case "Science":
                    subjectScoreOrder = ScienceScoreOrder;
                    break;

            }
        }
        public int Compare(Scores x, Scores y)
        {
            int indexX = Array.IndexOf(subjectScoreOrder, x.Score);
            int indexY = Array.IndexOf(subjectScoreOrder, y.Score);
            return indexX.CompareTo(indexY);
        }
    }
}

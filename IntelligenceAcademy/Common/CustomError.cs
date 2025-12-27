namespace IntelligenceAcademy.Common
{
    public class CustomError
    {
        public class AppCustomException : Exception
        {
            public AppCustomException(string message) : base(message) { }
        }
    }
}

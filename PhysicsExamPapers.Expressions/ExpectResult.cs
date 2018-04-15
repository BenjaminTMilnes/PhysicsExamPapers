namespace PhysicsExamPapers.Expressions
{
    public class ExpectResult<T>
    {
        public bool IsSuccessful { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
        public string ResultText { get; set; }
        public T ResultObject { get; set; }
    }
}

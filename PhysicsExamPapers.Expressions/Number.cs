namespace PhysicsExamPapers.Expressions
{
    public class Number<T> : Expression
    {
        public T Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

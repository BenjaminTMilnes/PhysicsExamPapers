namespace PhysicsExamPapers.Expressions
{
    public class Number<T> : Expression
    {
        public T Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Number<T>)
            {
                return Value.Equals((obj as Number<T>).Value);
            }

            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

namespace PhysicsExamPapers.Expressions
{
    public class Variable : Expression
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

namespace PhysicsExamPapers.Expressions
{
    public class AssignmentStatement : Expression
    {
        public Expression LeftHandSide { get; set; }
        public Expression RightHandSide { get; set; }

        public override string ToString()
        {
            return LeftHandSide.ToString() + " = " + RightHandSide.ToString();
        }
    }
}

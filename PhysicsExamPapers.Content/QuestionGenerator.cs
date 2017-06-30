using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionGenerator : IQuestionGenerator
    {
        public abstract IQuestion Generate(Random random);

        public IQuestion Generate()
        {
            return Generate(new Random());
        }

        protected int GenerateRandomNumberBetweenLimits(Random random, int lowerLimit, int upperLimit)
        {
            return (int)Math.Round(random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit);
        }
    }
}

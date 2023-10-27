using System;
using System.Data.Common;
using Moq;
using Xunit;

namespace mozadatak
{

    public class CalculationsTest
    {
        [Fact]
        public void OnlyOneSolutionTest()
        {
            //testiranje metode OnlyOneExpress gde se stampa samo jednan rezultat iz TextExpression-a
            // Arrange
            var mock = new Mock<ExpressionEvaluator>();
            var mockResult = new Dictionary<double, List<Expression>>()
                 {
                    { 10, new List<Expression>(){ new Expression { Target = 10 } } }
                 };
            mock.Setup(x => x.EvaluateExpressionsTest(new int[] { 1, 2, 3 }, 10)).Returns(mockResult);
            var mockValidator = new Mock<INumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new Calculations(mockValidator.Object, mock.Object);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            sut.OnlyOneExpress(new int[] { 1, 2, 3 }, 10);
            // Assert
            mock.Verify(x => x.EvaluateExpressionsTest(new int[] { 1, 2, 3 }, 10), Times.Once);

            //proveravanje da li ocekivan TextExpression u listi TextExpression-a 
            var expressions = sut.GetExpressionsByTarget(new int[] { 1, 2, 3 }, 10).ToList();
            Assert.Equal(1, expressions.Count);
            Assert.Equal(10, expressions[0].Target);


        }


        [Fact]
        public void GetExpressionsByTargetTest()
        {
            //testiranje metode GetExpressionsByTarget gde se ispituje da li su rezultati jednaki ciljanom broju
            //arrange
            var mock = new Mock<ExpressionEvaluator>();

            var mockResult = new Dictionary<double, List<Expression>>()
            {
                { 15, new List<Expression>(){ new Expression {Target = 15} } },
                { 10, new List<Expression>(){ new Expression { Target = 10 }, new Expression {Target = 10}}},
            };
            mock.Setup(x => x.EvaluateExpressionsTest(It.IsAny<int[]>(), It.IsAny<int>())).Returns(mockResult);
            Mock<INumberValidator> mockValidator = new Mock<INumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new Calculations(mockValidator.Object, mock.Object);
            //act
            var result = sut.GetExpressionsByTarget(new int[] { 1, 2, 3 }, 15);
            //assert
            Assert.Collection(result, item => Assert.Equal(15, item.Target));
        }

        [Fact]
        public void ApproximateSolutionTest()
        {
            //testiranje metode ApproximateSolution gde se trazi priblizan rezultat
            //u slucaju da nema izraza ciji su rezultati jednaki ciljanom broju
            //arrange
            var mock = new Mock<ExpressionEvaluator>();

            var mockResult = new Dictionary<double, List<Expression>>()
            {
                { 16, new List<Expression>(){ new Expression {Target = 15} } },
                { 11, new List<Expression>(){ new Expression { Target = 10 }, new Expression {Target = 10}}},
            };
            mock.Setup(x => x.EvaluateExpressionsTest(It.IsAny<int[]>(), It.IsAny<int>())).Returns(mockResult);
            Mock<INumberValidator> mockValidator = new Mock<INumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new Calculations(mockValidator.Object, mock.Object);
            //act
            var result = sut.ApproximateSolution(new int[] { 1, 2, 3 }, 16);
            //assert
            Assert.Collection(result, item => Assert.Equal(15, item.Target));
        }

        [Fact]

        public void GetSimplestSolutionTest()
        {
            // Arrange
            var mock = new Mock<ExpressionEvaluator>();
            var mockResult = new Dictionary<double, List<Expression>>()
                 {
                     { 10, new List<Expression>()
                         {
                             new Expression {Target = 10, Input = new List<int> { 1, 2, 3, 4 }, Operators = new List<string> { "+", "+", "+", "+" }, TextExpression = "1+2+3+4", Text = "10"}
                         }
                     },
                    { 15, new List<Expression>()
                         {
                            new Expression { Target = 15, Input = new List<int> { 1, 5, 3}, Operators = new List<string> { "*" },  TextExpression = "5*3", Text = "15" }
                         }
                    }
                 };
            mock.Setup(x => x.EvaluateExpressionsTest(It.IsAny<int[]>(), It.IsAny<int>())).Returns(mockResult);

            Mock<INumberValidator> mockValidator = new Mock<INumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new Calculations(mockValidator.Object, mock.Object);

            var input = new int[] { 1, 2, 5, 10 };
            var target = 10;

            // Act
            var results = sut.GetSimplestSolution(input, target);
           

            // Assert
            Assert.Collection(results, item =>
            {
                Assert.Equal(10, item.Target);
                Assert.Equal("1+2+3+4", item.TextExpression);
            });

        }


        [Fact]
        public void GetSelectingSolutionWithFindedSolutionTest()
        {
            //testiranje metode SelectingSolution pomocu koje se prikazuju izrazi
            //koji sadrze selektovani broj i selektovani operator
            var mock = new Mock<ExpressionEvaluator>();
            var mockResult = new Dictionary<double, List<Expression>>()
                 {

                { 15, new List<Expression>(){ new Expression {Target = 15, Input = new List<int> { 1, 2, 3, 4, 5}, Operators = new List<string> { "+", "+", "+", "+"}  , TextExpression = "1+2+3+4+5"} } }

                 };
            mock.Setup(x => x.EvaluateExpressionsTest(It.IsAny<int[]>(), It.IsAny<int>())).Returns(mockResult);
            var mockValidator = new Mock<INumberValidator>();
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = new Calculations(mockValidator.Object, mock.Object);

            var results = sut.GetSelectingSolution(new int[] { 1, 2, 3, 4, 5 }, 15, 1, "+");

            Assert.Collection(results, item => Assert.Equal(15, item.Target));

        }

    }
}

    

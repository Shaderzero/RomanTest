using System.Linq.Expressions;
using Sprache;

namespace RomanCalculator
{
    internal class Syntax
    {
        public static readonly Parser<Expression<Func<double>>> ParseLambda = 
            from body in Parse.Ref(() => Expr).End() 
            select Expression.Lambda<Func<double>>(body);
        static readonly Parser<ExpressionType> Operator = 
            Parse.Chars('*').Return(ExpressionType.Multiply)
            .Or(Parse.Chars('/').Return(ExpressionType.Divide))
            .Or(Parse.Char('+').Return(ExpressionType.Add))
            .Or(Parse.Char('-').Return(ExpressionType.Subtract))
          .Token();

        static readonly Parser<Expression> Operand = 
            Parse.Ref(() => Number)
            .Or(Parse.Ref(() => Parenthesized))
            .Or(Parse.Ref(() => Negate))
            .Token();

        static readonly Parser<Expression> Expr = 
            Parse.ChainOperator(Operator, Operand, Expression.MakeBinary);

        static readonly Parser<Expression> Number = 
            from number in Parse.Decimal 
            select Expression.Constant(Convert.ToDouble(number));

        static readonly Parser<Expression> Parenthesized = 
            from openParen in Parse.Char('(')
            from operand in Parse.Ref(() => Expr)
            from closeParen in Parse.Char(')')
            select operand;

        static readonly Parser<Expression> Negate = 
            from negative in Parse.Char('-')
            from expression in Parse.Ref(() => Operand)
            select Expression.Negate(expression);
    }
}

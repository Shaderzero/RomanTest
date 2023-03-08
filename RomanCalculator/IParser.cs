using System.Linq.Expressions;

namespace RomanCalculator
{
    public interface IParser<TResult>
    {
        Expression<Func<TResult>> Parse(string input);
    }
}

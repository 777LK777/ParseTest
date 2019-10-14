using AngleSharp;

namespace ParseLib.Model
{
    interface IParser<T> where T : class
    {
        T Parse(IBrowsingContext context);
    }
}

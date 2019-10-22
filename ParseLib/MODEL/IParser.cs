using AngleSharp.Dom;

namespace ParseLib.Model
{
    public interface IParser<T> where T : class
    {
        T Parse(IDocument document);
    }
}

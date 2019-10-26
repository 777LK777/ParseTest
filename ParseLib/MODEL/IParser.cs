using AngleSharp.Dom;
using System.Collections.Generic;

namespace ParseLib.Model
{
    public interface IParser<T> where T : class
    {
        T Parse(IDocument document);
        T Parse(List<IDocument> documents);
    }
}

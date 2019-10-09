using AngleSharp.Html.Parser;
using ParseLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.MODEL.Work
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        public bool IsActive
        {
            get;
            private set;
        }

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        HtmlLoader loader;

        public IParser<T> Parser
        {
            get => parser;
            set => parser = value;
        }

        public IParserSettings Settings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
            
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public void Start()
        {
            IsActive = true;
            Worker();
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void Worker()
        {
            for(int i = parserSettings.StartPoint; i<= parserSettings.EndPoint; i++)
            {
                if (!IsActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }
                    

                var source = await loader.GetSource();
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            IsActive = false;
        }
    }
}

using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ParseLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.MODEL.Work
{
    public class ParserWorker<T> where T : class
    {
        #region Fields
        List<IParserSettings> parserSettings;
        List<HtmlLoader> loaders;
        #endregion


        

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;


        public bool IsActive
        {
            get;
            private set;
        }

        public IParser<T> Parser { get; set; }

        private void SetParserSettings(List<IParserSettings> settings)
        {
            if(settings.Count != 0)
            {
                for (int i = 0; i < settings.Count; i++)
                {
                    loaders.Add(new HtmlLoader(settings[i].BaseURI));
                }
            }
            else
            {
                throw new InvalidOperationException("Список адресов для парсинга пустой");
            }
        }

        public List<IParserSettings> Settings
        {
            get => parserSettings;
        }

        public ParserWorker(IParser<T> parser, List<IParserSettings> settings)
        {
            loaders = new List<HtmlLoader>();
            SetParserSettings(settings);
            this.Parser = parser;
        }               

        public void Start()
        {
            if (this.Parser != null)
            {
                IsActive = true;
                Worker();
            }
            else
            {
                throw new InvalidOperationException("Адрес сайта не определен!!!");
            }
            
        }

        public void Abort()
        {
            IsActive = false;
        }

        private void Worker()
        {
            if (!IsActive)
            {
                OnCompleted?.Invoke(this);
                return;
            }

            List<Task> tasks = new List<Task>();
            List<T> results = new List<T>();
            List<IDocument> documents = new List<IDocument>();

            foreach (var loader in loaders)
            {
                documents.Add(loader.GetDocument());
            }

            var result = Parser.Parse(documents);

            OnNewData?.Invoke(this, result);

            OnCompleted?.Invoke(this);
            IsActive = false;
        }
    }
}

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
        IParserSettings parserSettings;
        HtmlLoader loader;

        #endregion


        

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;


        public bool IsActive
        {
            get;
            private set;
        }

        public IParser<T> Parser { get; set; }

        public IParserSettings Settings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value.BaseURI);
            }

        }

        public ParserWorker() { }

        public ParserWorker(IParser<T> parser, IParserSettings settings)
        {
            this.Parser = parser;
            this.Settings = settings;
        }               

        public void Start()
        {
            if (this.Parser != null)
            {
                IsActive = true;
                Task.Run(() => Worker());
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

            var result = Parser.Parse(loader.GetDocument());

            OnNewData?.Invoke(this, result);

            OnCompleted?.Invoke(this);
            IsActive = false;
        }
    }
}

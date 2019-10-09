using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.Model
{
    interface IParserSettings
    {
        string BaseURI { get; set; }

        int StartPoint { get; set; }

        int EndPoint { get; set; }
    }
}

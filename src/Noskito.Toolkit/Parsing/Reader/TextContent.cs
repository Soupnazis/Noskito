using System.Collections.Generic;

namespace Noskito.Toolkit.Parsing.Reader
{
    public class TextContent : TextRegion
    {
        public TextContent(IEnumerable<TextLine> lines) : base(lines)
        {
        }
    }
}
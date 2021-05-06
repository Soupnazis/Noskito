using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noskito.Toolkit.Parsing.Reader
{
    public class TextRegion
    {
        protected TextRegion(IEnumerable<TextLine> lines) => Lines = lines;

        public IEnumerable<TextLine> Lines { get; }

        public IEnumerable<TextRegion> GetRegions(string regionSeparator)
        {
            TextLine[] lines = Lines.ToArray();

            var regions = new List<List<TextLine>>();
            var region = new List<TextLine>();
            foreach (TextLine line in lines)
            {
                if (line.StartWith(regionSeparator))
                {
                    region = new List<TextLine>();
                    regions.Add(region);
                }

                region.Add(line);
            }

            return regions.Select(x => new TextRegion(x));
        }

        public IEnumerable<TextLine> GetLines(Predicate<TextLine> predicate)
        {
            return Lines.Where(predicate.Invoke);
        }

        public IEnumerable<TextLine> GetLines(string start)
        {
            return GetLines(x => x.StartWith(start));
        }

        public TextLine GetLine(Predicate<TextLine> predicate)
        {
            return Lines.FirstOrDefault(predicate.Invoke);
        }

        public TextLine GetLine(string start)
        {
            return GetLine(x => x.StartWith(start));
        }

        public string AsString()
        {
            var sb = new StringBuilder();
            foreach (TextLine line in Lines)
            {
                sb.Append(line.AsString()).Append("\r");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
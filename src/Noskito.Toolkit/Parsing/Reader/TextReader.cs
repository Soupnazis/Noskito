using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Noskito.Toolkit.Parsing.Reader
{
    public class TextReader
    {
        private readonly string[] content;
        private readonly List<Predicate<string>> skipConditions;
        private char separator;

        private bool trim;

        private TextReader(string[] content)
        {
            this.content = content;
            skipConditions = new List<Predicate<string>>();
        }

        public static TextReader FromString(string content)
        {
            return new TextReader(content.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
        }

        public static TextReader FromFile(string path)
        {
            return new TextReader(File.ReadAllLines(path));
        }

        public static TextReader FromFile(FileInfo fileInfo)
        {
            return FromFile(fileInfo.FullName);
        }

        public TextReader SkipEmptyLines()
        {
            return SkipLines(string.IsNullOrEmpty);
        }

        public TextReader SkipCommentedLines(string commentTag)
        {
            return SkipLines(x => x.StartsWith(commentTag));
        }

        public TextReader TrimLines()
        {
            trim = true;
            return this;
        }

        public TextReader SplitLineContent(char separator)
        {
            this.separator = separator;
            return this;
        }

        public TextReader SkipLines(Predicate<string> predicate)
        {
            skipConditions.Add(predicate);
            return this;
        }

        public TextContent GetContent()
        {
            var lines = new List<TextLine>();
            foreach (string line in content)
            {
                if (skipConditions.Any(x => x.Invoke(line)))
                {
                    continue;
                }

                string content = line;

                if (trim)
                {
                    content = content.Trim();
                }

                lines.Add(new TextLine(content.Split(separator), separator));
            }

            return new TextContent(lines);
        }
    }
}
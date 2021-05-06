using System;

namespace Noskito.Toolkit.Parsing.Reader
{
    public class TextLine
    {
        private readonly string[] content;
        private readonly char separator;

        public TextLine(string[] content, char separator)
        {
            this.content = content;
            this.separator = separator;
        }

        public string GetValue(int index)
        {
            if (index >= content.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return content[index];
        }

        public T GetValue<T>(int index)
        {
            return (T)Convert.ChangeType(GetValue(index), typeof(T));
        }

        public string[] GetValues()
        {
            return content;
        }

        public string GetFirstValue()
        {
            return content[0];
        }

        public string GetLastValue()
        {
            return content[content.Length - 1];
        }

        public T GetFirstValue<T>()
        {
            return (T)Convert.ChangeType(GetFirstValue(), typeof(T));
        }

        public T GetLastValue<T>()
        {
            return (T)Convert.ChangeType(GetLastValue(), typeof(T));
        }

        public bool StartWith(string value)
        {
            return content[0].Equals(value);
        }

        public bool EndWith(string value)
        {
            return content[content.Length - 1].Equals(value);
        }

        public string AsString()
        {
            return string.Join(separator.ToString(), content);
        }
    }
}
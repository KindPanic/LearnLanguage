using System;
using System.Collections.Generic;
using System.Text;

namespace LearnLanguage.BL.Model
{
    [Serializable]
    public class Words
    {
        public string Word { get; set; }

        public string Translate { get; set; }

        public Words(string word, string translate)
            {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("Поле не может быть равно NULL", nameof(word));
            }

            if (string.IsNullOrWhiteSpace(translate))
            {
                throw new ArgumentException("Поле не может быть равно NULL", nameof(translate));
            }
            Word = word;
            Translate = translate;
        }

        public override string ToString()
        {
            return Word + " - " + Translate;      
        }

    }
}

using LearnLanguage.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace LearnLanguage.BL.Controller
{
    public class WordsController
    {
        List<Words> words = new List<Words>(); //Наш основной список слов.
       [NonSerialized]List<Words> wordsForTest = new List<Words>(); //Список слов для изучения и теста, постоянно обновляется.

        /// <summary>
        /// Добавляем новое слово в коллекцию
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="translate">Перевод</param>
        public void Add(string word, string translate)
        {
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(translate))
            {
                Console.WriteLine("Не добавлено!!");
                Console.WriteLine("Одно из полей было пустое.");
                Console.WriteLine("Enter - продолжать!");
                Console.ReadLine();
            }
            else if (words.FirstOrDefault(x => x.Word == word) != null)
            {
                Console.WriteLine("Такое слово уже добавлено в словарь.");
                Console.WriteLine(words.FirstOrDefault(x => x.Word == word));
                Console.WriteLine("Enter - продолжать!");
                Console.ReadLine();
            }
            else
            {
                Words _word = new Words(word, translate);
                words.Add(_word);
                Console.WriteLine("Успешно добавлено в словарь.");
                Console.WriteLine("Enter - продолжать!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Вывести всю коллекцию на экран консоли.
        /// </summary>
        public void ShowAll()
        {
            foreach (var item in words)
            {
                Console.WriteLine((words.IndexOf(item)+1) + ". " +  item);
            }
            Console.WriteLine("Enter - продолжить.");
            Console.ReadLine();
        }

        /// <summary>
        /// Меняем слово и перевод по слову.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="translate"></param>
        public void Edit(string word, string translate)
        {
           
            if (string.IsNullOrWhiteSpace(word) || (words.FirstOrDefault(x => x.Word == word) == null))
            {
                Console.WriteLine("Не найдено!!!!");
                Console.WriteLine("Enter - продолжить.");
                Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(translate))
            {
                Console.WriteLine("Поле не может быть пустым или равно NULL");
                Console.WriteLine("Enter - продолжить.");
                Console.ReadLine();
            }
            else
            {
                int i = words.FindIndex(x => x.Word == word);
                words.RemoveAt(i);
                words.Insert(i, new Words(word, translate));
                Console.WriteLine("Исправлено!");
                Console.WriteLine("Enter - продолжить.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Сохраняем нашу колекцию слов.
        /// </summary>
        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream("data.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, words);
            }
        }

        /// <summary>
        /// Выгружаем нашу коллекцу слов из файла
        /// </summary>
        /// <returns> List<Words> words</returns>
        public List<Words> Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream("data.bin", FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && formatter.Deserialize(fileStream) is List<Words>_words)
                {
                    return _words;
                }
                return new List<Words>();
            }
            
           

        }

        public WordsController()
        {
           words = Load();
        }

        /// <summary>
        /// Заполняем новый список для изучения и тестирования
        /// </summary>
        /// <param name="value">Кол-во слов которое выбирается из основного списка в наш для теста.</param>
        public bool newListForLearn(int value)
        {
            wordsForTest.Clear();
            var rnd = new Random();


            if (value <= words.Count) //Проверка входных данных на вхождение в нашу индексацию.
            {
                
                for (int i = 0; i < value;)
                {
                    var word = words.ElementAt(rnd.Next(0, words.Count));
                    if (!wordsForTest.Contains(word))
                    {
                        wordsForTest.Add(word);
                        i++;
                    }
                }
                foreach (var item in wordsForTest)
                {
                    Console.WriteLine((wordsForTest.IndexOf(item) + 1) + ". " + item);
                }
                return true;
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вышли за рамки нашей коллекции.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Enter - Что бы продолжить.");
                Console.ReadLine();
                return false;
            }
            
        }
        /// <summary>
        /// Наш тест.
        /// </summary>
        public void LearnTestingRus()
        {
            Console.Clear();
            var rnd = new Random();
            int value = 0;
            List<Words> test = new List<Words>();
            for (int i = 0; i < wordsForTest.Count; )
            {
                
                var word = wordsForTest.ElementAt(rnd.Next(0,wordsForTest.Count));
                if (!test.Contains(word))
                {
                    test.Add(word);
                    i++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Как переводится слово" + " " + word.Word);
                    Console.Write("Введите перевод слова: ");
                    string translate = Console.ReadLine();
                    if (translate == word.Translate)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Правильно.");
                        value++;
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не верно.");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Правильно - " + word.Translate);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Верно отвечено на {0} из {1}",value,test.Count);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter - Что бы продолжить.");
            Console.ReadLine();


        }

        /// <summary>
        /// Очищаем наш файл полностью.
        /// </summary>
        public void ClearAll()
        {
            words.Clear();
        }


    }
}

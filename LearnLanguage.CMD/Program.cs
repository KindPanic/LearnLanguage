using LearnLanguage.BL.Controller;
using System;

namespace LearnLanguage.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Close = true;
            string word;
            string translate;
            WordsController wordsController = new WordsController();
            
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("Вас приветствует программа помощи по изучению иностранных языков\n");
               
                Console.WriteLine("Выберите действие.");
                Console.WriteLine("1.Добавить новое слово в словарь.");
                Console.WriteLine("2.Изменить слово, если ошиблись.");
                Console.WriteLine("3.Показать все слова в словаре.");
                Console.WriteLine("4.Ввести кол-во слов для изучения и теста.");
                Console.WriteLine("0.Выход.");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    switch (value)
                    {
                        case 1:
                            Console.Clear();
                            Console.Write("Введите слово: ");
                            word = Console.ReadLine();
                            Console.Write("Введите перевод: ");
                           translate = Console.ReadLine();
                            wordsController.Add(word, translate);
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Console.Write("Введите слово: ");
                            word = Console.ReadLine();
                            Console.Write("Введите перевод: ");
                            translate = Console.ReadLine();
                            wordsController.Edit(word,translate);
                            break;

                        case 3:
                            Console.Clear();
                            wordsController.ShowAll();
                            break;
                        case 4:
                            Console.WriteLine("Введите на какое кол-во слов вы хотите пройти тестирование.");
                            if (int.TryParse(Console.ReadLine(), out int result))
                            {
                                Console.Clear();
                                if (wordsController.newListForLearn(result))
                                {
                                    Console.WriteLine("Y - Начать тест?");

                                    if (Console.ReadKey().Key == ConsoleKey.Y)
                                    {
                                        wordsController.LearnTestingRus();
                                    }
                                }
                            }
                            else 
                            {
                                Console.WriteLine("Вы ввели не число.");
                            }
                            
                            break;

                        case 0:
                            Close = false;
                            break;

                        case 404:

                            wordsController.ClearAll();
                            break;

                        default:
                            break;
                    }
                }
                wordsController.Save();

            } while (Close);
          




        }
    }
}

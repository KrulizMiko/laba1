using System;
using System.Collections.Generic;

class Program
{
    // Множество символов
    private static char[] V = { '0', '1', '2' };
    //данный тип данных предоставляет один символ 

    static void Main(string[] args)
    {
        char[] key = GenerateSubstitutionKey();
        Console.WriteLine($"Сгенерированный ключ подстановки: {new string(key)}\n");

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Шифровать сообщение");
            Console.WriteLine("2. Дешифровать сообщение");
            Console.WriteLine("3. Выйти");
            Console.Write("Ваш действие: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Шифрование
                Console.Write("Введите текст для шифрования (только символы 0, 1, 2): ");
                string input = Console.ReadLine();
                try
                {
                    string encryptedMessage = Encrypt(input, key);
                    Console.WriteLine($"Зашифрованное сообщение: {encryptedMessage}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (choice == "2")
            {
                // Дешифрование
                Console.Write("Введите текст для дешифрования: ");
                string input = Console.ReadLine();
                try
                {
                    string decryptedMessage = Decrypt(input, key);
                    Console.WriteLine($"Расшифрованное сообщение: {decryptedMessage}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Досвидание!");
                break;
            }
            else
            {
                Console.WriteLine("Выберите действие от 1 до 3!");
            }

            Console.WriteLine(); 
        }
    }

    // Метод генерации ключа подстановки
    private static char[] GenerateSubstitutionKey()//Сгенерировать ключ постановки
    {
        Random random = new Random();
        List<char> temp = new List<char>(V);
        char[] key = new char[V.Length];

        // Перемешивание массива
        for (int i = 0; i < key.Length; i++)
        {
            int index = random.Next(temp.Count);
            key[i] = temp[index];
            temp.RemoveAt(index);
        }

        return key;
    }

    // Метод шифрования
    private static string Encrypt(string input, char[] key)//Зашифровать
    {
        char[] encrypted = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            int index = Array.IndexOf(V, input[i]);
            if (index >= 0)
            {
                encrypted[i] = key[index];
            }
            else
            {
                throw new ArgumentException("Ввод должен содержать только символы 0, 1, 2.");
            }
        }

        return new string(encrypted);
    }

    // Метод дешифрования
    private static string Decrypt(string input, char[] key)//Расшифровать
    {
        char[] decrypted = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            int index = Array.IndexOf(key, input[i]);
            if (index >= 0)
            {
                decrypted[i] = V[index];
            }
            else
            {
                throw new ArgumentException("Расшифрованный текст содержит недопустимые символы.");
            }
        }

        return new string(decrypted);
    }
}

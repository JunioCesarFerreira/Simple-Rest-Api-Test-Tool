using System;
using Newtonsoft.Json;
using WebApplicationApiTest.Models;

namespace WebApplicationApiTest
{
    internal static class ConLog
    {
        private static void PrintColor(string text, ConsoleColor color)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = tmp;
        }

        public static void Json(KeyValueModel keyValue)
        {
            string json = JsonConvert.SerializeObject(keyValue, Formatting.Indented);
            PrintColor(json, ConsoleColor.Yellow);
        }

        public static void Success(string msgSuccess)
        {
            PrintColor(msgSuccess, ConsoleColor.Green);
        }

        public static void Info(string msgInfo)
        {
            PrintColor(msgInfo, ConsoleColor.Cyan);
        }

        public static void Warning(string msgWarning)
        {
            PrintColor(msgWarning, ConsoleColor.Yellow);
        }

        public static void Erro(string msgErro)
        {
            PrintColor(msgErro, ConsoleColor.Red);
        }
    }
}

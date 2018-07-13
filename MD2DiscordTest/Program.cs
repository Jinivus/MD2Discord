using System;
using MD2Discord;

namespace MD2DiscordTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mardownText = System.IO.File.ReadAllText("test.md");
            Markdig.Markdown.ToHtml(mardownText);
            var export = Markdown.ToDiscord(mardownText);
        }
    }
}
using Markdig.Renderers;
using Markdig.Syntax;

namespace MD2Discord.Discord
{
    public abstract class DiscordObjectRenderer<TObject> : MarkdownObjectRenderer<DiscordRenderer, TObject> where TObject : MarkdownObject
    {
    }
}
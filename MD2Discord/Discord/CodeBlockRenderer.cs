using Markdig.Syntax;

namespace MD2Discord.Discord
{
    public class CodeBlockRenderer : DiscordObjectRenderer<CodeBlock>
    {
        protected override void Write(DiscordRenderer renderer, CodeBlock obj)
        {
            renderer.EnsureLine();
            renderer.WriteLine("```");
            renderer.WriteLeafRawLines(obj, true, true);
            renderer.WriteLine("```");
        }
    }
}
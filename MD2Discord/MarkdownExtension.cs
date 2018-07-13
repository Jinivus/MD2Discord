using System;
using System.IO;
using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;

namespace MD2Discord
{
    public static partial class Markdown
    {
        public static string ToDiscord(string markdown, MarkdownPipeline pipeline = null)
        {
            if (markdown == null)
                throw new ArgumentNullException(nameof (markdown));
            var stringWriter = new StringWriter();
            ToDiscord(markdown, (TextWriter) stringWriter, pipeline);
            return stringWriter.ToString();
        }
        
        public static MarkdownDocument ToDiscord(string markdown, TextWriter writer, MarkdownPipeline pipeline = null)
        {
            if (markdown == null)
                throw new ArgumentNullException(nameof (markdown));
            if (writer == null)
                throw new ArgumentNullException(nameof (writer));
            pipeline = pipeline ?? new MarkdownPipelineBuilder().Build();
            var discordRenderer = new DiscordRenderer(writer);
            pipeline.Setup((IMarkdownRenderer) discordRenderer);
            var markdownDocument = Markdig.Markdown.Parse(markdown, pipeline);
            discordRenderer.Render((MarkdownObject) markdownDocument);
            writer.Flush();
            return markdownDocument;
        }
    }
}
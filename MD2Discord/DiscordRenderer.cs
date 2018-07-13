using System;
using System.IO;
using System.Runtime.CompilerServices;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax;
using MD2Discord.Discord;

namespace MD2Discord
{
    public class DiscordRenderer : TextRendererBase<DiscordRenderer>
    {
        public DiscordRenderer(TextWriter writer) : base(writer)
        {
            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer());
            ObjectRenderers.Add(new ListRenderer());
            ObjectRenderers.Add(new HeadingRenderer());
            ObjectRenderers.Add(new HtmlBlockRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            ObjectRenderers.Add(new QuoteBlockRenderer());
            ObjectRenderers.Add(new ThematicBreakRenderer());

            EnableHtmlEscape = true;
        }
        
        public DiscordRenderer WriteLeafRawLines(LeafBlock leafBlock, bool writeEndOfLines, bool escape, bool softEscape = false)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            if (leafBlock.Lines.Lines != null)
            {
                var lines = leafBlock.Lines;
                var slices = lines.Lines;
                for (var i = 0; i < lines.Count; i++)
                {
                    if (!writeEndOfLines && i > 0)
                    {
                        WriteLine();
                    }
                    if (escape)
                    {
                        WriteEscape(ref slices[i].Slice, softEscape);
                    }
                    else
                    {
                        Write(ref slices[i].Slice);
                    }
                    if (writeEndOfLines)
                    {
                        WriteLine();
                    }
                }
            }
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DiscordRenderer WriteEscape(ref StringSlice slice, bool softEscape = false)
        {
            if (slice.Start > slice.End)
            {
                return this;
            }
            return WriteEscape(slice.Text, slice.Start, slice.Length, softEscape);
        }
        
        public DiscordRenderer WriteEscape(string content, int offset, int length, bool softEscape = false)
        {
            if (string.IsNullOrEmpty(content) || length == 0)
                return this;

            var end = offset + length;
            var previousOffset = offset;
            for (; offset < end; offset++)
            {
                switch (content[offset])
                {
                    case '<':
                        Write(content, previousOffset, offset - previousOffset);
                        if (EnableHtmlEscape)
                        {
                            Write("&lt;");
                        }
                        previousOffset = offset + 1;
                        break;

                    case '>':
                        if (!softEscape)
                        {
                            Write(content, previousOffset, offset - previousOffset);
                            if (EnableHtmlEscape)
                            {
                                Write("&gt;");
                            }
                            previousOffset = offset + 1;
                        }
                        break;

                    case '&':
                        Write(content, previousOffset, offset - previousOffset);
                        if (EnableHtmlEscape)
                        {
                            Write("&amp;");
                        }
                        previousOffset = offset + 1;
                        break;

                    case '"':
                        if (!softEscape)
                        {
                            Write(content, previousOffset, offset - previousOffset);
                            if (EnableHtmlEscape)
                            {
                                Write("&quot;");
                            }
                            previousOffset = offset + 1;
                        }
                        break;
                }
            }

            Write(content, previousOffset, end - previousOffset);
            return this;
        }

        public bool EnableHtmlEscape { get; set; }

        public override object Render(MarkdownObject markdownObject)
        {
            return base.Render(markdownObject);
        }
    }
}
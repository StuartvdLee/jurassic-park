using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace JurassicPark.Mcp.Local.Prompts;

[McpServerPromptType]
public class DinosaurPrompt
{
    [McpServerPrompt]
    [Description("Prompt to get a list of dinosaurs with a maximum limit.")]
    public ChatMessage GetDinosaurs([Description("Dinosaur limit")] int limit)
    {
        return new ChatMessage(ChatRole.User, $"Get a list of {limit} dinosaurs");
    }
}

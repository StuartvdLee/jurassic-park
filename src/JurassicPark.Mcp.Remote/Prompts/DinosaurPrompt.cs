using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace JurassicPark.Mcp.Remote.Prompts;

[McpServerPromptType]
public class DinosaurPrompt
{
    [McpServerPrompt(Name = "DinosaurPrompt")]
    public ChatMessage GetDinosaurs([Description("Dinosaur limit")] int? limit)
    {
        return new ChatMessage(ChatRole.User, $"Get a list of {limit} dinosaurs");
    }
}

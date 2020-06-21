using GameEngine.Core;

namespace Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            var app = new Sandbox(new WindowProp { Width = 800, Height = 600, Title = "Game" });
            EntryPoint entry = new EntryPoint(app, args);
        }
    }
}

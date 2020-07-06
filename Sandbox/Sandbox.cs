using GameEngine.Core;
using GameEngine.Core.Window;

namespace Sandbox
{
    public class Sandbox : Application
    {
        public Sandbox(WindowProp prop) : base(prop)
        {
            var asd = Input.GetMousePosition();
        }
    }
}

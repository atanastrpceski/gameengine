using GameEngine.Core;
using GameEngine.Core.Events;
using GameEngine.Core.Window;
using GameEngine.Layers;

namespace Sandbox
{
    public class TestLayer : Layer
    {
        public TestLayer() : base("Test Layer")
        {

        }

        public override void OnUpdate()
        {
            if (Input.IsKeyPressed(KeyCode.Tab))
                Log.ClientLogger.Info("Tab key is pressed (poll)");

            if(Input.IsMouseButtonPressed(MouseButtonCode.MOUSE_BUTTON_1))
                Log.ClientLogger.Info("Left mouse button pressed (poll)");

            base.OnUpdate();
        }

        public override void OnEvent(Event e)
        {
            if (e.GetEventType() == EventType.KeyPressed)
            {
                if (((KeyPressedEvent)e).GetKeyCode() == KeyCode.Tab)
                    Log.ClientLogger.Info("Tab key is pressed (event)");
            }

            if (e.GetEventType() == EventType.MouseButtonPressed)
            {
                if (((MouseButtonEvent)e).GetMouseButton() == MouseButtonCode.MOUSE_BUTTON_1)
                    Log.ClientLogger.Info("Left mouse button pressed (event)");

                if (((MouseButtonEvent)e).GetMouseButton() == MouseButtonCode.MOUSE_BUTTON_2)
                    Log.ClientLogger.Info("Right mouse button pressed (event)");

                if (((MouseButtonEvent)e).GetMouseButton() == MouseButtonCode.MOUSE_BUTTON_3)
                    Log.ClientLogger.Info("Middle mouse button pressed (event)");
            }

            base.OnEvent(e);
        }
    }


    public class Sandbox : Application
    {
        public Sandbox(WindowProp prop) : base(prop)
        {
            this.PushLayer(new TestLayer());
            this.PushOverlay(new ImGuiLayer());
        }
    }
}

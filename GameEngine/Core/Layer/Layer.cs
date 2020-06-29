using GameEngine.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public class Layer
    {
        private string _name;

        public Layer(string name = "Layer")
        {
            _name = name;
        }

        public virtual void OnAttach() { }
        public virtual void OnDetach() { }
        public virtual void OnUpdate() { }
        public virtual void OnEvent(Event @event) { }

        public string GetName() {
            return _name;
        }
    }
}

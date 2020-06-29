using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Core
{
    class LayerStack
    {
        IList<Layer> _layers;
        IList<Layer> _overlays;

        public LayerStack()
        {
            _layers = new List<Layer>();
            _overlays = new List<Layer>();
        }

        public void PushLayer(Layer layer)
        {
            _layers.Add(layer);
        }

        public void PushOverlay(Layer overlay)
        {
            _overlays.Add(overlay);
        }

        public void PopLayer(Layer layer)
        {
            _layers.Remove(layer);
        }
        
        public void PopOverlay(Layer layer)
        {
            _overlays.Remove(layer);
        }

        public IEnumerable<Layer> Layers {
            get
            {
                foreach (var layer in _layers)
                {
                    yield return layer;
                }

                foreach (var overlay in _overlays)
                {
                    ////yield return overlay;
                }
            }
        }
    }
}

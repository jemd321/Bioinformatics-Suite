using BioinformaticsSuite.Module.Enums;
using Prism.Events;

namespace BioinformaticsSuite.Module.Events
{
    public class WindowSizeChanged : PubSubEvent<WindowSize>
    {
        // event broadcast for window resizing.
    }
}

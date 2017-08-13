using Microsoft.Practices.Unity;
using testModuleAppPrism.Models;

namespace testModuleAppPrism.ViewModels
{
    class HelloWorldViewModel
    {
        [Dependency]
        public testMessageProvider TestMessageProvider { get; set; }
    }
}

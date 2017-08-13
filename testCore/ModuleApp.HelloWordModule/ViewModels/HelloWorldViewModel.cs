using Microsoft.Practices.Unity;
using testModuleAppPrism.Models;

namespace testModuleAppPrism.ViewModels
{
    class HelloWorldViewModel
    {
        [Dependency]
        public MessageProvider MessageProvider { get; set; }
    }
}

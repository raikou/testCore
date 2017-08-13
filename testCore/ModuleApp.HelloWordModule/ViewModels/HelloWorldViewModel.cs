using Microsoft.Practices.Unity;
using testModuleAppPrism.Models.Models;

namespace testModuleAppPrism.Models.ViewModels
{
    class HelloWorldViewModel
    {
        [Dependency]
        public MessageProvider MessageProvider { get; set; }
    }
}

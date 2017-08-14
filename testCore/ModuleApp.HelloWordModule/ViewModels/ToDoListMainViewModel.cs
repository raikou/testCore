using Microsoft.Practices.Unity;
using testModuleAppPrism.Models;

namespace testModuleAppPrism.ViewModels
{
    public class ToDoListMainViewModel
    {
		//利用するモデル
	    private ToDoList toDoList;

		//コンストラクタ
	    public ToDoListMainViewModel()
	    {
			toDoList = new ToDoList();
		}

		[Dependency]
        public testMessageProvider TestMessageProvider { get; set; }
    }
}

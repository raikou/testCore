using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using WpfAppPrism.Views;

namespace WpfAppPrism
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			// this.ContainerでUnityのコンテナが取得できるので
			// そこからShellを作成する
			return this.Container.Resolve<Shell>();
		}

		protected override void InitializeShell()
		{
			// Shellを表示する
			((Window)this.Shell).Show();
		}

	}
}

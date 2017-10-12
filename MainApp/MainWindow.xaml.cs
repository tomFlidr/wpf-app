using App.Forms;
using Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow: Window {
		public MainWindow () {
			InitializeComponent();


			
			//this.tabs.Close += 
		}

		private void tree1Handler (object sender, Components.ItemDoubleClickEventArgs args) {
			var d = args.ModelClassInstance as Dealer;
			var tab = new TabItem {
				Header = args.ModelClassInstance.GetTreeNodeHeader(),
				Content = new DealerForm(d)
			};
			this.tabs.Add(tab);
			//this.tabs.UpdateLayout();
		}
	}
}

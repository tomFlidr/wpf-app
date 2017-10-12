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

namespace App.Components {
	/// <summary>
	/// Interaction logic for TabPanelClosable.xaml
	/// </summary>
	public partial class TabPanelClosableHeader: UserControl {
		private string header;
		private TabItem tab;
		private TabPanelClosable tabs;

		public TabPanelClosableHeader () {
			InitializeComponent();
		}

		public TabPanelClosableHeader (TabPanelClosable tabs, string header, TabItem tab) {
			InitializeComponent();
			this.tab = tab;
			this.tabs = tabs;
			this.label.Content = header;
			this.closeBtn.Click += (o, args) => {
				this.tabs.OnClose(this.tab);
			};
		}
	}
}

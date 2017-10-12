using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace App.Components {
	public class TabCloseEventArgs: EventArgs {
		public TabItem Tab { get; set; }

		public bool Handled {
			get { return _handled; }
			set { _handled = value; }
		}
		private bool _handled = false;
	}
	public delegate void TabCloseEventHandler (
		TabPanelClosable sender, TabCloseEventArgs args
	);
	public class TabPanelClosable: TabControl {

		public event TabCloseEventHandler Close;
		public event TabCloseEventHandler BeforeClose;

		private string header;
		
		public int Add (TabItem tab) {

			string header = tab.Header.ToString();
			tab.Header = new TabPanelClosableHeader(this, header, tab);

			var r = this.Items.Add(tab);
			this.SelectedItem = tab;
			return r;
		}
		public void OnClose (TabItem tab) {
			var args = new TabCloseEventArgs() {
				Tab = tab,
				Handled = false
			};
			if (this.BeforeClose != null) {
				this.BeforeClose.Invoke(this, args);
			}
			if (!args.Handled) {
				this.Items.Remove(tab);
			}
			if (this.Close != null) {
				this.Close.Invoke(this, args);
			}
		}
	}
}

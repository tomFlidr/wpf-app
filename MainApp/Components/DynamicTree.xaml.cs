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

	public class ItemDoubleClickEventArgs: EventArgs {
		public IDynamicTreeModel ModelClassInstance { get; set; }
		public TreeViewItem TreeViewItem { get; set; }
	}

	public delegate void ItemDoubleClickHandler (object sender, ItemDoubleClickEventArgs args);

	/// <summary>
	/// Interaction logic for DynamicTree.xaml
	/// </summary>
	public partial class DynamicTree: UserControl {


		public event ItemDoubleClickHandler ItemDoubleClick;


		public string ModelClass {
			get { return _modelClass.FullName; }
			set {

				//Type.GetType(value);
				Type modelClassType = Tools.GetTypeGlobaly(value);
				if (modelClassType == null) return;
				Type interfaceType = typeof(IDynamicTreeModel);

				bool implemented = interfaceType.IsAssignableFrom(modelClassType);
				bool implemented2 = modelClassType.GetInterface("IDynamicTreeModel") != null;
#if !DEBUG
				if (!implemented) throw new Exception($"ModelClass not found: '{value}'.");
#endif

				_modelClass = modelClassType;


				IDynamicTreeModel dummyInstance = (IDynamicTreeModel)Activator.CreateInstance(
					modelClassType
				);
				List<IDynamicTreeModel> fistNodes = dummyInstance.GetChildsByParentId("");
				foreach (IDynamicTreeModel item in fistNodes) {
					TreeViewItem treeItem = new TreeViewItem() {
						Header = item.GetTreeNodeHeader()
					};
					if (item.GetHashChilds()) treeItem.Items.Add(null);
					treeItem.Expanded += this.onExpanded;
					treeItem.MouseDoubleClick += this.onDoubleClick;
					treeItem.DataContext = item;
					treeItem.Tag = false;

					this.tree.Items.Add(treeItem);

					treeItem.IsExpanded = true; // load root node subitems
				}
			}
		}
		private Type _modelClass = null;

		private void _loadSubItems (IDynamicTreeModel item, ItemCollection items) {
			if (items.Count == 1 && items[0] == null) items.Clear();
			List<IDynamicTreeModel> childs = item.GetChildsByParentId(item.GetId());
			TreeViewItem treeItem;
			foreach (var child in childs) {
				// vizuální položka ve stromě
				treeItem = new TreeViewItem() {
					Header = child.GetTreeNodeHeader()
				};
				if (child.GetHashChilds()) treeItem.Items.Add(null);

				treeItem.Expanded += this.onExpanded;
				treeItem.PreviewMouseDoubleClick += this.onDoubleClick;
				treeItem.DataContext = child; // databázová entity
				treeItem.Tag = false; // nebyly načtené podpoložky

				items.Add(treeItem);
			}
		}

		private void onDoubleClick (object sender, MouseButtonEventArgs e) {
			TreeViewItem treeItem = sender as TreeViewItem;
			if (this.ItemDoubleClick != null) {
				this.ItemDoubleClick.Invoke(this, new ItemDoubleClickEventArgs {
					ModelClassInstance = treeItem.DataContext as IDynamicTreeModel,
					TreeViewItem = treeItem
				});
			}
			e.Handled = true;
		}

		private void onExpanded (object sender, RoutedEventArgs e) {
			TreeViewItem treeItem = sender as TreeViewItem;
			if (!(bool)treeItem.Tag) {
				treeItem.Tag = true;
				IDynamicTreeModel dataObject = treeItem.DataContext as IDynamicTreeModel;
				this._loadSubItems(dataObject, treeItem.Items);
			}
		}

		public DynamicTree () {
			this.InitializeComponent();
		}
	}
}

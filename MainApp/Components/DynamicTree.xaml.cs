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
	/// Interaction logic for DynamicTree.xaml
	/// </summary>
	public partial class DynamicTree: UserControl {

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
					this._loadSubItems(
						item,
						this.tree.Items
					);
				}
			}
		}
		private Type _modelClass = null;

		private void _loadSubItems (IDynamicTreeModel item, ItemCollection items) {
			Console.WriteLine(items.Count);
		}

		public DynamicTree () {
			this.InitializeComponent();
		}
	}
}

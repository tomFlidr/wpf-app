using App.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
	public class FileSystem: IDynamicTreeModel {

		public string FullPath {
			get { return $"{this.DirPath}/{this.Name}"; }
		}

		public string DirPath { get; set; }
		public string Name { get; set; }
		public List<IDynamicTreeModel> GetChildsByParentId (string id = "") {
			if (String.IsNullOrEmpty(id)) {

			} else {

			}
		}

		public bool GetHashChilds () {
			
		}

		public string GetId () {
			return this.FullPath;
		}

		public string GetTreeNodeHeader () {
			return this.Name;
		}

		private List<FileSystem> GetChids () {
			var di = new DirectoryInfo(this.DirPath);
			return (
				from d in di.GetDirectories()
				orderby d.Name
				select new FileSystem {
					Name = d.Name,
					DirPath = this.DirPath
				}
			).ToList().AddRange(
				(from f in di.GetFiles()
				orderby f.Name
				select new FileSystem {
					Name = f.Name,
					DirPath = this.DirPath
				}).ToLookup()
			);
		}
	}
}

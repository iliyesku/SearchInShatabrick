using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SearchInShatabrick
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			watcher.IncludeSubdirectories = true;
			
			watcher.NotifyFilter = NotifyFilters.Size;

			watcher.Filter = "IgnorePref*.ini";

			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);

			watcher.EnableRaisingEvents = true;
		}
		
		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			try
			{
				string s = File.ReadAllText(e.FullPath);
				if (s != string.Empty)
				{
					Process.Start("http://www.shatabrick.com/cco/kw/index.php?g=kw&a=sp&name=" + s.Remove(0, s.IndexOf('=')+2));
				}
			}
			catch
			{
				MessageBox.Show("Unable to process file: " +  e.FullPath/* + " " + e.ChangeType + " " + "Content: " + s*/);
			}
		}
		
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Application.Exit();
		}
	}
}

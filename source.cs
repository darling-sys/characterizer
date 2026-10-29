using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Diagnostics; // for "Process" type
using System.Globalization;
namespace characterizer
{
	public class constance // helpful constance gives us info
	{
		public static readonly string version = "0.0.1";
		public static readonly string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "characterizer";
		public static readonly string save = folder + Path.DirectorySeparatorChar + "save.data";
		public static readonly string log = folder + Path.DirectorySeparatorChar + "log.data";
		public static readonly string[] menuOptions = {"1. New", "2. Load", "3. Exit"};
	}
	public class Program // program starting point
	{
		public static void Main(string[] args)
		{
			string lc = "characterizer started\n";
			Console.CancelKeyPress += delegate
			{
				lc = lc + "[ctrl] - [C] pressed, exiting\n";
				File.WriteAllText(constance.log, lc);
			};
			data d = new data();
			data f = new data();
			if(File.Exists(constance.save))
			{
				try
				{
					f = deserialize(constance.save);
					lc = lc + "save file loaded\n";
				}
				catch
				{
					f = d;
					File.Delete(constance.save);
					lc = lc + "save file corrupted, deleting\n";
				}
				d = f;
				lc = lc + "data ready\n";
			}
			else
			{
				if(!(Directory.Exists(constance.folder)))
				{
					Directory.CreateDirectory(constance.folder);
					lc = lc + "directory created\n";
				}
				File.Create(constance.save);
				lc = lc + "save file created\n";
			}
			lc = lc + "file work done\n";
			bool cont = true;
			while(cont)
			{
				Console.Clear();
				foreach(string s in constance.menuOptions)
				{
					Console.WriteLine(s);
				}
				ConsoleKeyInfo pressedKey = Console.ReadKey(true);
				switch(pressedKey.KeyChar)
				{
					case '1':
					{
						//
						break;
					}
					case '2':
					{
						//
						break;
					}
					case '3':
					{
						cont = false;
						lc = lc + "user quits\n";
						break;
					}
					default:
					{
						lc = lc + "unknown input: " + pressedKey.KeyChar.ToSTring() + "\n";
						break;
					}
				}
			}
			lc = lc + "saving data\n";
			serialize(d, constance.save);
			File.WriteAllText(constance.log, lc);
			Environment.Exit(0);
		}
		public static data deserialize(string path)
		{
			data d = new data();
			using(FileStream fs = new FileStream(constance.save, FileMode.Open))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				d = (data)formatter.Deserialize(fs);
			}
			return d;
		}
		public static data serialize(data d, string path)
		{
			using(FileStream fs = new FileStream(constance.save, FileMode.Create))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, d);
			}
			return d;
		}
	}
	[Serializable]
	public class data
	{
		public List<slot> slots = new List<slot>();
	}
	[Serializable]
	public class slot
	{
		public string name;
	}
}


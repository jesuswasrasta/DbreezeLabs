using System.IO;


namespace DbreezeLabs
{
	public static class DiretoryInfoExtensions
	{
		public static void Clean(this DirectoryInfo folder)
		{
			foreach (FileInfo file in folder.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in folder.GetDirectories())
			{
				dir.Delete(true);
			}
		}
	}
}
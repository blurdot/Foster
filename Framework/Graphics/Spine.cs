
using System.Diagnostics;
using static Foster.Framework.Aseprite;

namespace Foster.Framework
{
	public static class Spine
	{
		internal static void SpineAtlasCreateFromFile(string path)
		{
			// TODO: Verofy path
			var resource = Platform.SpineAtlasCreateFromFile(path, Graphics.Renderer);
			if (resource == IntPtr.Zero)
			{
				throw new Exception($"Failed to create Atlas from file: {path}");
			}
		}
	}
}

using System.Runtime.InteropServices;
using System.Text;

namespace Foster.Framework;

internal static class SpinePlatform
{
	public const string DLL = "FosterPlatform";

	// Spine
	[DllImport(DLL)]
	public static extern IntPtr SpineAtlasCreateFromFile(string path, IntPtr renderer);
}

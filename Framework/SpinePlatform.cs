using System.Runtime.InteropServices;
using System.Text;

namespace Foster.Framework;

internal static class SpinePlatform
{
	public const string DLL = "FosterPlatform";

	// Spine
	[DllImport(DLL)]
	public static extern IntPtr Spine_Atlas_CreateFromFile(string path);

	[DllImport(DLL)]
	public static extern IntPtr Spine_SkeletonJson_Create(IntPtr atlas);

	[DllImport(DLL)]
	public static extern IntPtr Spine_SkeletonJson_Update(IntPtr skeletonJson, Spine.SkeletonJson data);
}

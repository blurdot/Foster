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
	public static extern IntPtr Spine_SkeletonJson_Set(IntPtr skeletonJson, SpineAPI.SkeletonJson data); // UPDATE FUNCTION

	[DllImport(DLL)]
	public static extern IntPtr Spine_SkeletonJson_ReadSkeletonDataFile(IntPtr skeletonJson, string path);

	[DllImport(DLL)]
	public static extern IntPtr Spine_AnimationStateData_Create(IntPtr animationStateData);

	[DllImport(DLL)]
	public static extern IntPtr Spine_AnimationStateData_Set(IntPtr animationStateData, SpineAPI.AnimationStateData data); // UPDATE FUNCTION

	[DllImport(DLL)]
	public static extern IntPtr Spine_SkeletonDrawable_Create(IntPtr skeletonData, IntPtr animationStateData);

	[DllImport(DLL)]
	public static extern IntPtr Spine_SkeletonDrawable_Set(IntPtr skeletonDrawable, SpineAPI.SkeletonDrawable data); // UPDATE FUNCTION

	[DllImport(DLL)]
	public static extern IntPtr Spine_Skeleton_Set(IntPtr skeleton, SpineAPI.Skeleton data); // UPDATE FUNCTION

	[DllImport(DLL)]
	public static extern void Spine_Skeleton_SetToSetupPose(IntPtr skeleton);

	[DllImport(DLL)]
	public static extern void Spine_SkeletonDrawable_Draw(IntPtr skeletonDrawable);

	[DllImport(DLL)]
	public static extern void Spine_SkeletonDrawable_Update(IntPtr skeletonDrawable, float delta);

	[DllImport(DLL)]
	public static extern IntPtr Spine_AnimationState_SetAnimationByName(IntPtr animationState, int trackIndex, string animationName, int /*bool*/ loop);

	[DllImport(DLL)]
	public static extern IntPtr Spine_AnimationState_AddAnimationByName(IntPtr animationState, int trackIndex, string animationName, int /*bool*/ loop, float delay);

	// SDL takeover (TODO: Remove these later if we can figure out DrawCommand integration)
	[DllImport(DLL)]
	public static extern int Spine_SDL_SetRenderDrawColor(byte r, byte g, byte b, byte a);
	[DllImport(DLL)]
	public static extern int Spine_SDL_RenderClear();
	[DllImport(DLL)]
	public static extern void Spine_SDL_RenderPresent();

	/*
	[DllImport(DLL)]
	public static extern IntPtr Spine_Update(IntPtr item, SpineAPI.IUpdatable data); // UPDATE FUNCTION
	*/
}

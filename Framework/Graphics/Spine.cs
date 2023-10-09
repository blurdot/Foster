
using System.Runtime.InteropServices;

namespace Foster.Framework
{
	public static class Spine
	{
		public static IntPtr Atlas_CreateFromFile(string path)
		{
			// TODO: Verify path?
			var ptr = SpinePlatform.Spine_Atlas_CreateFromFile(path);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to create Atlas from file: {path}");
			}

			return ptr;
		}

		public static SkeletonJson SkeletonJson_Create(IntPtr atlas)
		{
			var ptr = SpinePlatform.Spine_SkeletonJson_Create(atlas);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to create SkeletonJson from atlas");
			}

			SkeletonJson skeletonJson = Marshal.PtrToStructure<SkeletonJson>(ptr);
			skeletonJson.ptr = ptr;
			skeletonJson.scale = 0.5f;
			skeletonJson.Update();

			return skeletonJson;
		}

		public static SkeletonJson SkeletonJson_Update(SkeletonJson skeletonJson)
		{
			IntPtr ptr = SpinePlatform.Spine_SkeletonJson_Update(skeletonJson.ptr, skeletonJson);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to update SkeletonJson");
			}

			SkeletonJson skeletonJsonUpdated = Marshal.PtrToStructure<SkeletonJson>(ptr);
			skeletonJsonUpdated.ptr = ptr;

			return skeletonJsonUpdated;
		}

		public struct SkeletonJson
		{
			public float scale;
			public IntPtr attachmentLoader;
			public string error;
			public IntPtr ptr;

			public void Update()
			{
				SkeletonJson_Update(this);
			}
		}
	}
}

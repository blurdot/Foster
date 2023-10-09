
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using static Foster.Framework.SpineAPI;

namespace Foster.Framework
{
	public static class SpineAPI
	{
		public static bool TryGet<T>(IntPtr ptr, out T item) where T : IMarshalable
		{
			try
			{
				item = Marshal.PtrToStructure<T>(ptr);
				item.SetPtr(ptr);
				return true;
			}
			catch
			{
				Log.Error($"TryGet {typeof(T)} failed!");
				item = default(T);
				return false;
			}
		}

		/* Future optimization for generic update?
		public static bool TryUpdate<T>(ref T item) where T : IUpdatable, IMarshalable
		{
			IntPtr ptr = SpinePlatform.Spine_Update(item.GetPtr(), item);
			if (ptr == IntPtr.Zero)
			{
				Log.Error($"Failed to update {typeof(T)}");
				return false;
			}

			if (!TryGet(ptr, out item))
			{
				return false;
			}

			return true;
		}
		*/

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

			if (TryGet(ptr, out SkeletonJson skeletonJson))
			{
				// TODO: Handle errors
			}

			return skeletonJson;
		}

		public static bool SkeletonJson_Set(ref SkeletonJson skeletonJson)
		{
			IntPtr ptr = SpinePlatform.Spine_SkeletonJson_Set(skeletonJson.ptr, skeletonJson);
			if (ptr == IntPtr.Zero)
			{
				Log.Error($"Failed to update SkeletonJson");
				return false;
			}

			if (!TryGet(ptr, out skeletonJson))
			{
				return false;
			}

			return true;
		}

		public static IntPtr SkeletonJson_ReadSkeletonDataFile(SkeletonJson skeletonJson, string path)
		{
			var ptr = SpinePlatform.Spine_SkeletonJson_ReadSkeletonDataFile(skeletonJson.ptr, path);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to ReadSkeletonDataFile from path {path}");
			}

			if (!TryGet(ptr, out SkeletonData skeletonData))
			{
				// TODO: handle errors
			}

			return ptr;
		}

		public static AnimationStateData AnimationStateData_Create(IntPtr skeletonData)
		{
			var ptr = SpinePlatform.Spine_AnimationStateData_Create(skeletonData);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to create SkeletonJson from atlas");
			}

			if (!TryGet(ptr, out AnimationStateData animationStateData))
			{
				// TODO: handle errors
			}

			return animationStateData;
		}

		public static bool AnimationStateData_Set(ref AnimationStateData animationStateData)
		{
			IntPtr ptr = SpinePlatform.Spine_AnimationStateData_Set(animationStateData.ptr, animationStateData);
			if (ptr == IntPtr.Zero)
			{
				Log.Error($"Failed to update AnimationStateData");
				return false;
			}

			if (!TryGet(ptr, out animationStateData))
			{
				return false;
			}

			return true;
		}

		public static SkeletonDrawable SkeletonDrawable_Create(IntPtr skeletonData, AnimationStateData animationStateData)
		{
			var ptr = SpinePlatform.Spine_SkeletonDrawable_Create(skeletonData, animationStateData.ptr);
			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to create SkeletonDrawable from atlas");
			}

			if (!TryGet(ptr, out SkeletonDrawable skeletonDrawable))
			{
				// TODO: handle errors
			}

			return skeletonDrawable;
		}

		public static bool SkeletonDrawable_Set(ref SkeletonDrawable skeletonDrawable)
		{
			IntPtr ptr = SpinePlatform.Spine_SkeletonDrawable_Set(skeletonDrawable.ptr, skeletonDrawable);
			if (ptr == IntPtr.Zero)
			{
				Log.Error($"Failed to update SkeletonDrawable");
				return false;
			}

			if (!TryGet(ptr, out skeletonDrawable))
			{
				return false;
			}

			return true;
		}

		public static bool Skeleton_Set(ref Skeleton skeleton)
		{
			IntPtr ptr = SpinePlatform.Spine_Skeleton_Set(skeleton.ptr, skeleton);
			if (ptr == IntPtr.Zero)
			{
				Log.Error($"Failed to update Skeleton");
				return false;
			}

			if (!TryGet(ptr, out skeleton))
			{
				return false;
			}

			return true;
		}

		public static void Skeleton_SetToSetupPose(ref Skeleton skeleton)
		{
			SpinePlatform.Spine_Skeleton_SetToSetupPose(skeleton.ptr);

			if (!TryGet(skeleton.GetPtr(), out skeleton))
			{
				// TODO: handle errors
			}
		}

		public static void SkeletonDrawable_Draw(SkeletonDrawable skeletonDrawable)
		{
			SpinePlatform.Spine_SkeletonDrawable_Draw(skeletonDrawable.GetPtr());
		}

		public static void SkeletonDrawable_Update(SkeletonDrawable skeletonDrawable, float delta)
		{
			SpinePlatform.Spine_SkeletonDrawable_Update(skeletonDrawable.GetPtr(), delta);
		}

		public static TrackEntry AnimationState_SetAnimationByName(AnimationState animationState, int trackIndex, string animationName, bool loop)
		{
			var ptr = SpinePlatform.Spine_AnimationState_SetAnimationByName(animationState.GetPtr(), trackIndex, animationName, loop ? 0 : -1);

			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to set TrackEntry for SetAnimationByName");
			}

			if (!TryGet(ptr, out TrackEntry trackEntry))
			{
				// TODO: handle errors
			}

			return trackEntry;
		}

		public static int SDL_SetRenderDrawColor(byte r, byte g, byte b, byte a)
		{
			return SpinePlatform.Spine_SDL_SetRenderDrawColor(r, g, b, a);
		}

		public static int SDL_RenderClear()
		{
			return SpinePlatform.Spine_SDL_RenderClear();
		}

		public static void SDL_RenderPresent()
		{
			SpinePlatform.Spine_SDL_RenderPresent();
		}

		public static TrackEntry AnimationState_AddAnimationByName(AnimationState animationState, int trackIndex, string animationName, bool loop, float delay)
		{
			var ptr = SpinePlatform.Spine_AnimationState_AddAnimationByName(animationState.GetPtr(), trackIndex, animationName, loop ? 0 : -1, delay);

			if (ptr == IntPtr.Zero)
			{
				throw new Exception($"Failed to set TrackEntry for AddAnimationByName");
			}

			if (!TryGet(ptr, out TrackEntry trackEntry))
			{
				// TODO: handle errors
			}

			return trackEntry;
		}

		//
		// Spine data structures
		//

		public interface IMarshalable
		{
			public void SetPtr(IntPtr ptr);
			public IntPtr GetPtr();
		}

		public interface ISettable
		{
			public bool TrySet();
		}

		public struct SkeletonJson : IMarshalable, ISettable
		{
			public float scale;
			public IntPtr attachmentLoader;
			public string error;
			public IntPtr ptr;

			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }

			public bool TrySet()
			{
				return SkeletonJson_Set(ref this);
			}
		}

		// TODO: Throwing access violations on marshal
		public struct SkeletonData : IMarshalable
		{
			public string version;
			public string hash;
			public float x, y, width, height;
			public float fps;
			public string imagesPath;
			public string audioPath;

			// TODO: would need to make a getter for these **s

			public int stringsCount;
			public IntPtr strings;
			public int bonesCount;
			public IntPtr bones;
			public int slotsCount;
			public IntPtr slots;
			public int skinsCount;
			public IntPtr skins;
			public IntPtr defaultSkin;
			public int eventsCount;
			public IntPtr events;
			public int animationsCount;
			public IntPtr animations;
			public int ikConstraintsCount;
			public IntPtr ikConstraints;
			public int transformConstraintsCount;
			public IntPtr transformConstraints;
			public int pathConstraintsCount;
			public IntPtr pathConstraints;
			public IntPtr ptr;
			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }
		}

		public struct AnimationStateData : IMarshalable
		{
			public IntPtr skeletonData;
			public float defaultMix;
			public IntPtr entries;
			public IntPtr ptr;

			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }

			public bool TryUpdate()
			{
				return AnimationStateData_Set(ref this);
			}
		}

		public struct AnimationState : IMarshalable
		{
			public IntPtr data; //AnimationStateData
			public int tracksCount;
			public IntPtr tracks;
			public IntPtr listener;
			public float timeScale;
			public IntPtr renderObject;
			public IntPtr userData;
			public int unkeyedState;
			public IntPtr ptr;

			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }
		}

		public struct SkeletonDrawable : IMarshalable
		{
			public IntPtr skeleton;
			public IntPtr animationState;
			public IntPtr clipper;
			public IntPtr worldVertices;
			public IntPtr sdlVertices;
			public IntPtr sdlIndices;
			public IntPtr ptr;

			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }
		}

		public struct Skeleton : IMarshalable, ISettable
		{
			public IntPtr data; // SkeletonData

			public int bonesCount;
			public IntPtr bones;
			public IntPtr root;

			public int slotsCount;
			public IntPtr slots;
			public IntPtr drawOrder;

			public int ikConstraintsCount;
			public IntPtr ikConstraints;

			public int transformConstraintsCount;
			public IntPtr transformConstraints;

			public int pathConstraintsCount;
			public IntPtr pathConstraints;

			public IntPtr skin;
			public Vector4 color;
			public float scaleX, scaleY;
			public float x, y;

			public IntPtr ptr;
			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }

			public bool TrySet()
			{
				return Skeleton_Set(ref this);
			}
		}

		public struct TrackEntry : IMarshalable, ISettable
		{
			public IntPtr animation;
			public IntPtr previous;
			public IntPtr next;
			public IntPtr mixingFrom;
			public IntPtr mixingTo;
			public IntPtr listener;
			public int trackIndex;
			public bool loop;
			public bool holdPrevious;
			public bool reverse;
			public bool shortestRotation;
			public float eventThreshold, attachmentThreshold, drawOrderThreshold;
			public float animationStart, animationEnd, animationLast, nextAnimationLast;
			public float delay, trackTime, trackLast, nextTrackLast, trackEnd, timeScale;
			public float alpha, mixTime, mixDuration, interruptAlpha, totalAlpha;
			public int mixBlend; // ENUM
			public IntPtr timelineMode;
			public IntPtr timelineHoldMix;
			public IntPtr timelinesRotation;
			public int timelinesRotationCount;
			public IntPtr rendererObject;
			public IntPtr userData;

			public IntPtr ptr;
			public IntPtr GetPtr() { return this.ptr; }
			public void SetPtr(IntPtr ptr) { this.ptr = ptr; }

			public bool TrySet()
			{
				throw new NotImplementedException();
			}
		};
	}
}

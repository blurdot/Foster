namespace Foster.Framework;

public static class Time
{
	public static class Rates
	{
		/// <summary>
		/// The default behavior is to lock the render rate to the update rate.
		/// </summary>
		public static TimeSpan RenderLocked = TimeSpan.Zero;

		/// <summary>
		/// Allows Render to be called as fast as user's monitor will allow.
		/// </summary>
		public static TimeSpan RenderUnlocked = TimeSpan.MinValue;

		public static TimeSpan FPS_60 = TimeSpan.FromSeconds(0.01666666666666666666666666666667);
		public static TimeSpan FPS_30 = TimeSpan.FromSeconds(0.03333333333333333333333333333334);
	}

	/// <summary>
	/// Time in Seconds since our last Update.
	/// In Fixed Timestep this always returns a constant value.
	/// </summary>
	public static float Delta;

	/// <summary>
	/// Time in Seconds since our last Render.
	/// </summary>
	public static float RenderDelta;

	/// <summary>
	/// An Accumulation of the Delta Time, incremented each Update.
	/// In Fixed Timestep this is incremented by a constant value.
	/// </summary>
	public static TimeSpan Duration;

	/// <summary>
	/// An Accumulation of the Delta Time of all Renders, incremented each Render.
	/// </summary>
	public static TimeSpan RenderDuration;

	/// <summary>
	/// Requests the current time since the Application was started.
	/// This is different than Time.Duration which is only incremented once per frame.
	/// This is also not affected by Fixed Timestep.
	/// </summary>
	public static TimeSpan Now => App.Timer.Elapsed;

	/// <summary>
	/// Current frame index
	/// </summary>
	public static ulong Frame = 0;
	public static ulong RenderFrame = 0;

	/// <summary>
	/// If the Application should run in Fixed Timestep mode
	/// </summary>
	public static bool FixedStep = true;

	/// <summary>
	/// What the Fixed Timestep target is (defaults to 60fps, or 0.016s per frame)
	/// </summary>
	public static TimeSpan FixedStepTarget = TimeSpan.FromSeconds(1.0f / 60.0f);

	/// <summary>
	/// When running with FixedStep = true, tries to make sure the render rate won't exceed this value.
	/// Default is to lock RenderRateTarget to FixedStepTarget by setting this value to RenderRateLocked.
	/// For uncapped render rate (e.g. as fast as the user's monitor wil allow), set this to RenderRateUncapped.
	/// </summary>
	public static TimeSpan RenderRateTarget = Rates.RenderLocked;

	/// <summary>
	/// The maximum amount of time a Fixed Update is allowed to take before the Application starts dropping frames.
	/// </summary>
	public static TimeSpan FixedStepMaxElapsedTime = TimeSpan.FromSeconds(5.0f / 60.0f);

	/// <summary>
	/// Advances the Time.Duration by the given delta, and assigns Time.Delta.
	/// </summary>
	public static void Advance(TimeSpan delta)
	{
		Delta = (float)delta.TotalSeconds;
		Duration += delta;
	}

	/// <summary>
	/// Advances the Time.RenderDuration by the given delta, and assigns Time.RenderDelta.
	/// </summary>
	public static void AdvanceRender(TimeSpan delta)
	{
		RenderDelta = (float)delta.TotalSeconds;
		RenderDuration += delta;
	}

	/// <summary>
	/// Returns true when the elapsed time passes a given interval based on the delta time
	/// </summary>
	public static bool OnInterval(double time, double delta, double interval, double offset)
	{
		return Math.Floor((time - offset - delta) / interval) < Math.Floor((time - offset) / interval);
	}

	/// <summary>
	/// Returns true when the elapsed time passes a given interval based on the delta time
	/// </summary>
	public static bool OnInterval(double delta, double interval, double offset)
	{
		return OnInterval(Duration.TotalSeconds, delta, interval, offset);
	}

	/// <summary>
	/// Returns true when the elapsed time passes a given interval based on the delta time
	/// </summary>
	public static bool OnInterval(double interval, double offset = 0.0)
	{
		return OnInterval(Duration.TotalSeconds, Delta, interval, offset);
	}

	/// <summary>
	/// Returns true when the elapsed time is between the given interval. Ex: an interval of 0.1 will be false for 0.1 seconds, then true for 0.1 seconds, and then repeat.
	/// </summary>
	public static bool BetweenInterval(double time, double interval, double offset)
	{
		return (time - offset) % (interval * 2) >= interval;
	}

	/// <summary>
	/// Returns true when the elapsed time is between the given interval. Ex: an interval of 0.1 will be false for 0.1 seconds, then true for 0.1 seconds, and then repeat.
	/// </summary>
	public static bool BetweenInterval(double interval, double offset = 0.0)
	{
		return BetweenInterval(Duration.TotalSeconds, interval, offset);
	}

	/// <summary>
	/// Sine-wave a value between `from` and `to` with a period of `duration`.
	/// You can use `offsetPercent` to offset the sine wave.
	/// </summary>
	public static float SineWave(float from, float to, float duration, float offsetPercent)
	{
		float total = (float)Duration.TotalSeconds;
		float range = (to - from) * 0.5f;
		return from + range + MathF.Sin(((total + duration * offsetPercent) / duration) * MathF.Tau) * range;
	}
}

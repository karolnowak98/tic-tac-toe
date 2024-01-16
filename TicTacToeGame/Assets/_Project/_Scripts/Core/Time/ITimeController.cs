namespace GlassyCode.TTT.Core.Time
{
    public interface ITimeController
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
        float UnscaledDeltaTime { get; }
        float RegularTime { get; }
        float FixedTime { get; }
        float UnscaledTime { get; }
    }
}

using KinshipCalculate.Tools;

namespace KinshipCalculate.Markers
{
    public abstract class KinMarker : IGenMarker
    {
        public readonly int Generation, Count;

        public KinMarker(int generation, int count) => (Generation, Count) = (generation, Math.Abs(count));

        public KinMarker[] NextForward => Forward(1);

        public KinMarker[] Forward(int count)
        {
            count = count == 0 ? 1 : Math.Abs(count);
            if (Generation == 0 || Count != 0) return new KinMarker[] { Get(Generation, Count + 1) };
            List<KinMarker> result = new() { Get(Generation, 1), Get(Generation.SubAbs(1)) };
            if (count == 1) return result.ToArray();
            result.AddRange(Forward(count - 1));
            return result.ToArray();
        }

        public static KinMarker Get(int generation = 0, int count = 0)
        {
            if (generation == 0) return count == 0 ? SelfMarker.Get : count > 0 ? new ProgenitorMarker(count) : new ProgenyMarker(count);
            return generation > 0 ? new ProgenitorMarker(generation, count) : new ProgenyMarker(generation, count);
        }
    }
}
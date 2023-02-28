namespace RelCalculate
{
    public abstract class KinCount : IEquatable<KinCount>
    {
        public readonly int Generation, Count;

        public KinCount(int generation, int count) => (Generation, Count) = (generation, Math.Abs(count));

        public abstract IKin[] Values { get; }

        public KinCount[] NextForward => Forward(1);

        public bool Equals(KinCount? other) => other is not null && Generation == other.Generation && Count == other.Count;

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public KinCount[] Forward(int count)
        {
            count = count == 0 ? 1 : Math.Abs(count);
            if (Generation == 0 || Count != 0) return new KinCount[] { Get(Generation, Count + 1) };
            List<KinCount> result = new() { Get(Generation, 1), Get(Generation.SubAbs(1)) };
            if (count == 1) return result.ToArray();
            result.AddRange(Forward(count - 1));
            return result.ToArray();
        }

        public static KinCount Get(int generation = 0, int count = 0)
        {
            if (generation == 0) return new SelfCount(count);
            return generation > 0 ? new ProgenitorCount(generation, count) : new ProgenyCount(generation, count);
        }

        public static bool operator ==(KinCount left, KinCount right) => left.Equals(right);

        public static bool operator !=(KinCount left, KinCount right) => !(left == right);
    }
}
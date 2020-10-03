namespace Utils.Pooler
{
    public class HotterPooler<T>: ObjectPooler<T> where T : Pooleable
    {
        private int _lastActiveObject = -1;
        public override T GetNextObject()
        {
            var i = (_lastActiveObject + 1) % Objects.Count;
            while (true)
            {
                if (i == _lastActiveObject) Grow();
                var pooleable = Objects[i];
                if (!pooleable.IsActive)
                {
                    _lastActiveObject = i;
                    pooleable.Activate();
                    return pooleable;
                }
                i = (i + 1) % Objects.Count;
            }
        }
    }
}
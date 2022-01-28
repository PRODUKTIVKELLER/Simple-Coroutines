namespace Produktivkeller.SimpleCoroutines
{
    internal class Linear : IMappingFunction
    {
        public float MapToValueBetween0And1(float valueBetween0And1)
        {
            return valueBetween0And1;
        }
    }
}

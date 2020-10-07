namespace IdentityGeneration
{
    internal interface ICountGenerator
    {
        (int, long) Generate();
    }
}
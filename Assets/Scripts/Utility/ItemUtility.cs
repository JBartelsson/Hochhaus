namespace Utility
{
    public class ItemUtility
    {
        //Function that checks if a number is smaller than a random number between 0 and 1
        public static bool CheckRandom(float chance)
        {
            return chance <= UnityEngine.Random.Range(0f, 1f);
        }
        
    }
}
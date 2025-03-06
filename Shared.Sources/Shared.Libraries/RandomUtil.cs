namespace Shared.Libraries;

public class RandomUtil
{
    private static Random RANDOM = new Random();
    private static string CHARACTERS = "1234567890ABCDEFGHIJKLMNOPQRSTUVXYWZ!@#$%&*";

    public static string RandomString(int length)
    {
        var text = "";

        while (text.Length < length)
            text += CHARACTERS[RANDOM.Next(CHARACTERS.Length)];

        return text;
    }

    public static int RandomNumber(int min, int max)
        => RANDOM.Next(min, max);
}
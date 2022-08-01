public static class StringExtensions
{
    public static string parseCamelcase(this string str)
    {
        string parsedStr = "";

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (i == 0) //Make first char uppercase if it isn't already
                c = char.ToUpper(c);
            else if (((char.IsLetter(c) && char.IsUpper(c)) || char.IsDigit(c)) && !char.IsWhiteSpace(str[i - 1]))
                parsedStr += ' '; //Add space whenever there is a capital letter or a number and the last char was not whitespace

            parsedStr += c;
        }

        return parsedStr;
    }
}

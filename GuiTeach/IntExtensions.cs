using System.ComponentModel;

namespace GuiTeach;

public static class IntExtensions
{
    public static string ToOrdinal(this int number)
    {
        if (number < 1)
        {
            throw new InvalidEnumArgumentException("Number must be >= 1.");
        }

        string suffix = "th";

        if (number < 20)
        {
            suffix = number switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        else if (number <= 100 && number % 10 < 4)
        {
            suffix = (number % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        else if (number % 100 < 20 && number > 100)
        {
            suffix = "th";
        }
        else if (number % 10 < 4)
        {
            suffix = (number % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        
        return $"{number}{suffix}";
    }
}
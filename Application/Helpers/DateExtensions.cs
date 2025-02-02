namespace Application.Helpers;

public static class DateExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var age = today.Year - dob.Year;

        if (dob > today.AddYears(-age)) age--;

        return age;
    }

    public static bool BeAtLeast16YearsOld(this DateOnly dob) => dob.CalculateAge() >= 16;
}

using System.ComponentModel.DataAnnotations;

namespace DigitalFamilyCookbook.Core.Helpers;

public static class Validation
{
    public static bool IsValidEmailAddress(this string address) => address != null && new EmailAddressAttribute().IsValid(address);
}
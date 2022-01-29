using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RoleType : IdentityRole
{
    public string RoleTypeId { get; set; } = string.Empty;

    public static RoleType None() => new RoleType();

    public static RoleType FromDto(RoleTypeDto dto)
    {
        return new RoleType
        {
            RoleTypeId = dto.RoleTypeId,
            Name = dto.Name,
            NormalizedName = dto.NormalizedName,
            ConcurrencyStamp = dto.ConcurrencyStamp,
            Id = dto.Id,
        };
    }
}

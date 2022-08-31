using System.ComponentModel.DataAnnotations;

namespace DigitalFamilyCookbook.Data.Dtos;

public class BaseDto
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }
}
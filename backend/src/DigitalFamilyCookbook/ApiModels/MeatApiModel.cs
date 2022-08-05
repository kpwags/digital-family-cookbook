namespace DigitalFamilyCookbook.ApiModels;

public class MeatApiModel : BaseApiModel
{
    public string Id { get; set; } = string.Empty;

    public int MeatId { get; set; }

    public string Name { get; set; } = string.Empty;

    public static MeatApiModel None() => new MeatApiModel();

    public static MeatApiModel FromDomainModel(Meat meat)
    {
        return new MeatApiModel
        {
            Id = meat.Id,
            MeatId = meat.MeatId,
            Name = meat.Name,
        };
    }
}

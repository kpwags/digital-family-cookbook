namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeMeatRepository
{
    Task DeleteForMeat(int meatId);
}

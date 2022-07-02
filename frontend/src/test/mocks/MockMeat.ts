import DataGenerator from '@test/DataGenerator';
import { Meat } from '@models/Meat';

export const MockMeat = (id = 0): Meat => ({
    id: DataGenerator.GenerateGuid(),
    meatId: id === 0 ? DataGenerator.GenerateInteger(1, 250) : id,
    name: DataGenerator.GenerateString(),
});

export const MockMeatList = (length = 10): Meat[] => {
    const meats: Meat[] = [];

    for (let i = 0; i < length; i += 1) {
        meats.push(MockMeat(i + 1));
    }

    return meats;
};

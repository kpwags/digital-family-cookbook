import DataGenerator from '@test/DataGenerator';
import { Meat } from '@models/Meat';

export const MockMeat = (): Meat => ({
    id: DataGenerator.GenerateGuid(),
    meatId: DataGenerator.GenerateInteger(1, 250),
    name: DataGenerator.GenerateString(),
});

export const MockMeatList = (length = 10): Meat[] => {
    const meats: Meat[] = [];

    for (let i = 0; i < length; i += 1) {
        meats.push(MockMeat());
    }

    return meats;
};

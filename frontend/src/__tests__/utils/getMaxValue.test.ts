import getMaxValue from '@utils/getMaxValue';

describe('@utils/getMaxValue', () => {
    test('It gets the max value of sorted array', () => {
        const arr = [1, 2, 3, 4, 5, 6];

        const maxValue = getMaxValue(arr);

        expect(maxValue).toBe(6);
    });

    test('It gets the max value of an unsorted array', () => {
        const arr = [24, 45, 56, 2, 44, 32];

        const maxValue = getMaxValue(arr);

        expect(maxValue).toBe(56);
    });

    test('It gets the max value of an array with duplicates', () => {
        const arr = [24, 45, 56, 2, 44, 56];

        const maxValue = getMaxValue(arr);

        expect(maxValue).toBe(56);
    });

    test('It handles an empty array', () => {
        const arr: number[] = [];

        const maxValue = getMaxValue(arr);

        expect(maxValue).toBe(0);
    });
});

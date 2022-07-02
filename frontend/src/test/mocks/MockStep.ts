import DataGenerator from '@test/DataGenerator';
import Step from '@models/Step';

export const MockStep = (recipeId: number, id = 0): Step => ({
    id: DataGenerator.GenerateGuid(),
    stepId: id === 0 ? DataGenerator.GenerateInteger(1, 250) : id,
    direction: DataGenerator.GenerateString(),
    recipeId,
    sortOrder: DataGenerator.GenerateInteger(1, 20),
});

export const MockStepList = (recipeId: number, length = 10): Step[] => {
    const steps: Step[] = [];

    for (let i = 0; i < length; i += 1) {
        steps.push(MockStep(recipeId));
    }

    return steps;
};

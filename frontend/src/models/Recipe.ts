import { Category } from './Category';
import Ingredient from './Ingredient';
import { Meat } from './Meat';
import Note from './Note';
import Step from './Step';

interface Recipe {
    recipeId: number
    id: string
    name: string
    description?: string
    isPublic: boolean
    source?: string
    sourceUrl?: string
    time?: number
    activeTime?: number
    imageUrl?: string
    imageUrlLarge?: string
    calories?: number
    carbohydrates?: number
    sugar?: number
    fat?: number
    protein?: number
    fiber?: number
    cholesterol?: number
    meats: Meat[]
    categories: Category[]
    ingredients: Ingredient[]
    steps: Step[]
    notes: Note[]
    dateCreated: Date
    dateUpdated: Date
}

export default Recipe;

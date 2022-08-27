import { Category } from './Category';
import Ingredient from './Ingredient';
import { Meat } from './Meat';
import Note from './Note';
import Step from './Step';
import { UserAccount } from './UserAccount';

interface Recipe {
    recipeId: number
    id: string
    name: string
    description?: string
    isPublic: boolean
    servings: number
    source?: string
    sourceUrl?: string
    time?: number
    activeTime?: number
    imageUrl: string
    imageData: string
    imageUrlLarge: string
    largeImageData: string
    calories?: number | null
    carbohydrates?: number | null
    sugar?: number | null
    fat?: number | null
    protein?: number | null
    fiber?: number | null
    cholesterol?: number | null
    meats: Meat[]
    categories: Category[]
    ingredients: Ingredient[]
    steps: Step[]
    notes: Note[]
    dateCreated: Date
    dateUpdated: Date
    userAccountId?: string
    userAccount?: UserAccount
    isFavorite: boolean
}

export default Recipe;

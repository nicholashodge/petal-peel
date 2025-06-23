export const getAllIngredients = async () => {
    return await fetch("/api/Ingredient").then((res) => res.json())
}
export const getAllIngredients = async () => {
    return await fetch("/api/ingredient").then((res) => res.json())
}
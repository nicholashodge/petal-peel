export const getAllIngredients = () => {
    return fetch("/api/Mocktail").then((res) => res.json())
}
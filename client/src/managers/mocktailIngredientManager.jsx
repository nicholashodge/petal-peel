export const deleteMocktailIngredient = async (id) => {
    return await fetch(`/api/mocktailIngredient/${id}`,
    {
        method: "DELETE"
    })
}
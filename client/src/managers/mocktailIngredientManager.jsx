export const deleteMocktailIngredient = async (id) => {
    return await fetch(`/api/MocktailIngredient/${id}`,
    {
        method: "DELETE"
    })
}
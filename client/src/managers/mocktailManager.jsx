export const getAllMocktails = async () => {
    return await fetch("/api/Mocktail").then((res) => res.json())
}

export const getMocktailById = async (id) => {
    return await fetch(`/api/Mocktail/${id}`).then((res) => res.json())
}

export const createMocktail = async (mocktail) => {
    return await fetch("/api/Mocktail",
    {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(mocktail)
    }).then((res) => res.json())
}

export const updateMocktail = async (mocktail, id) => {
    return await fetch(`/api/Mocktail/${id}`,
    {
        method: "PUT",
        headers: 
        {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(mocktail)
    })
}

export const deleteMocktail = async (id) => {
    return await fetch(`/api/Mocktail/${id}`,
        {
            method: "DELETE"
        }
    )
}
import { useEffect, useState } from "react"
import { getAllMocktails } from "../../managers/mocktailManager"

export const Mocktails = () => {
    const [allMocktails, setAllMocktails] = useState([])

    const loadAllMocktails = async () => {
        const mocktailArray = await getAllMocktails()
        setAllMocktails(mocktailArray)
    }

    useEffect(() => {
        loadAllMocktails();
    }, [])

    return(
        <div>
            <h2>Mocktails</h2>
        </div>
    )
}
import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import { Mocktails } from "./views/Mocktails";
import Register from "./auth/Register";
import Login from "./auth/Login";
import MocktailDetails from "./views/MocktailDetails";
import EditMocktail from "./views/EditMocktail";
import MyRecipes from "./views/MyRecipes";
import AccountDetails from "./views/AccountDetails";
import EditAccount from "./views/EditAccount";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
    return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
                <Mocktails loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route path="login" element={<Login setLoggedInUser={setLoggedInUser} />} />
        <Route path="register" element={<Register setLoggedInUser={setLoggedInUser} />} />
        <Route path="account" element={<AccountDetails loggedInUser={loggedInUser} />} />
        <Route path="account/edit" element={<EditAccount loggedInUser={loggedInUser} />} />
        <Route path="mocktails" element={<Mocktails />} />
        <Route path="mocktails/:id" element={<MocktailDetails />} />
        <Route path="mocktails/edit/:id" element={<EditMocktail />} />
        <Route path="mocktail/mine" element={<MyRecipes />} />
        </Route>
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>

    )
}
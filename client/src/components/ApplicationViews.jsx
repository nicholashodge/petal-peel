import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import { Mocktails } from "./views/Mocktails";
import Register from "./auth/Register";
import Login from "./auth/Login";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
    return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
                <Mocktails />
            </AuthorizedRoute>
          }
        />
        <Route
        path="login"
        element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
        path="register"
        element={<Register setLoggedInUser={setLoggedInUser} />}
        />
        </Route>
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>

    )
}
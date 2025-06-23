import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "../../styling/mocktailDetails.css";

export default function MocktailDetails() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [mocktail, setMocktail] = useState(null);
  const [currentUser, setCurrentUser] = useState(null);

  // Get mocktail by ID
  useEffect(() => {
    fetch(`/api/mocktail/${id}`)
      .then((res) => res.json())
      .then(setMocktail)
      .catch((err) => console.error("Failed to load mocktail", err));
  }, [id]);

  // Get current logged-in user
  useEffect(() => {
    fetch("/api/auth/me", {
      credentials: "include",
    })
      .then((res) => {
        if (res.ok) return res.json();
        throw new Error("Not logged in");
      })
      .then(setCurrentUser)
      .catch(() => setCurrentUser(null));
  }, []);

  if (!mocktail) return <p>Loading...</p>;

  const isAuthor = currentUser && mocktail.author?.id === currentUser.id;

  return (
    <div className="mocktail-details-container">
      <div className="mocktail-details-card">
        <h2>{mocktail.name}</h2>
        <p className="mocktail-description">{mocktail.description}</p>
        <p><strong>Created by:</strong> {mocktail.author?.fullName}</p>

        <h4>Ingredients</h4>
        <ul className="mocktail-ingredients">
          {mocktail.mocktailIngredients?.map((mi) => (
            <li key={mi.id}>
              {mi.quantity} {mi.ingredient?.measurement || ""} {mi.ingredient?.name}
            </li>
          ))}
        </ul>

        <h4>Instructions</h4>
        <div className="mocktail-instructions">
          <p>{mocktail.instructions}</p>
        </div>

        {isAuthor && (
          <button className="edit-mocktail-btn" onClick={() => navigate(`/mocktails/edit/${mocktail.id}`)}>
            Edit
          </button>
        )}
      </div>
    </div>
  );
}

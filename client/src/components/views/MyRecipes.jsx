import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import '../../styling/myRecipes.css';
import '../../styling/modalStyles.css';

export default function MyRecipes() {
  const [mocktails, setMocktails] = useState([]);
  const [selectedMocktail, setSelectedMocktail] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    fetch("/api/mocktail/mine")
      .then((res) => res.json())
      .then(setMocktails)
      .catch((err) => console.error("Failed to load user mocktails", err));
  }, []);

  const openModal = (mocktail) => {
    setSelectedMocktail(mocktail);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
    setSelectedMocktail(null);
  };

  return (
    <div className="mocktail-container">
      <h2 className="page-title">My Recipes</h2>
      <div className="mocktail-grid">
        {mocktails.map((mocktail) => (
          <div
            key={mocktail.id}
            className="mocktail-card"
            onClick={() => openModal(mocktail)}
          >
            <h3>{mocktail.name}</h3>
            <p className="description">{mocktail.description}</p>
          </div>
        ))}
      </div>

      {modalOpen && selectedMocktail && (
        <div className="modal-overlay" onClick={closeModal}>
          <div
            className="modal-content"
            onClick={(e) => e.stopPropagation()}
          >
            <h2>{selectedMocktail.name}</h2>
            <p>{selectedMocktail.description}</p>
            <h4>Ingredients</h4>
            <ul>
              {selectedMocktail.mocktailIngredients.map((mi) => (
                <li key={mi.id}>
                  {mi.quantity} {mi.ingredient?.measurement || ""} {mi.ingredient?.name}
                </li>
              ))}
            </ul>
            <h4>Instructions</h4>
            <p>{selectedMocktail.instructions}</p>
            <div className="modal-actions">
              <button onClick={() => navigate(`/mocktails/${selectedMocktail.id}`)}>
                View Details
              </button>
              <button onClick={closeModal}>Close</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

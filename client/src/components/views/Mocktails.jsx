import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Select from "react-select";
import { Modal, ModalHeader, ModalBody, Button, Form, FormGroup, Label, Input } from "reactstrap";
import { getAllMocktails, createMocktail } from "../../managers/mocktailManager";
import { getAllIngredients } from "../../managers/ingredientManager";
import '../../styling/mocktail.css';

export const Mocktails = ({loggedInUser}) => {
  const [mocktails, setMocktails] = useState([]);
  const [selectedMocktail, setSelectedMocktail] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [newMocktail, setNewMocktail] = useState({ name: '', description: '', instructions: '' });
  const [ingredients, setIngredients] = useState([]);
  const [selectedIngredients, setSelectedIngredients] = useState([]);

  useEffect(() => {
    getAllMocktails().then(setMocktails);
    getAllIngredients().then(setIngredients);
  }, []);

  const toggleModal = (mocktail = null) => {
    setSelectedMocktail(mocktail);
    setModalOpen(!modalOpen);
    setNewMocktail({ name: '', description: '', instructions: '' });
    setSelectedIngredients([]);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewMocktail(prev => ({ ...prev, [name]: value }));
  };

  const handleIngredientChange = (selectedOptions) => {
    const updated = selectedOptions.map(option => {
      const existing = selectedIngredients.find(i => i.ingredientId === option.value);
      return existing || { ingredientId: option.value, quantity: 1, name: option.label };
    });
    setSelectedIngredients(updated);
  };

  const handleQuantityChange = (ingredientId, quantity) => {
    setSelectedIngredients(prev =>
      prev.map(i =>
        i.ingredientId === ingredientId ? { ...i, quantity: parseFloat(quantity) || 0 } : i
      )
    );
  };
  
  const handleDelete = async (id) => {
  if (window.confirm("Are you sure you want to delete this recipe?")) {
    await deleteMocktail(id);
    const updated = await getAllMocktails();
    setMocktails(updated);
    toggleModal();
  }
};

const handleSubmit = async (e) => {
  e.preventDefault();

  const mocktailToSend = {
    name: newMocktail.name,
    description: newMocktail.description,
    instructions: newMocktail.instructions,
    mocktailIngredients: selectedIngredients.map(i => ({
      ingredientId: i.ingredientId,
      quantity: i.quantity
    }))
  };

  const response = await createMocktail(mocktailToSend);

  if (response?.id) {
    const updated = await getAllMocktails();
    setMocktails(updated);
    toggleModal();
  } else {
    alert("Failed to save mocktail. Check the console for details.");
    console.error("Failed response:", response);
  }
};

  return (
    <div className="container mt-5">
      <Button className="mb-4 border border-dark" color="light" onClick={() => toggleModal({})}>
        Add a recipe <span className="ms-2 text-purple">➕</span>
      </Button>
      <div className="row">
        {mocktails.map((m) => (
          <div
            key={m.id}
            className="col-md-4 mb-4"
            onClick={() => toggleModal(m)}
            style={{ cursor: "pointer" }}
          >
            <div className="p-3 border text-center">
              <h5>{m.name}</h5>
              <p>{m.description}</p>
            </div>
          </div>
        ))}
      </div>

      <Modal isOpen={modalOpen} toggle={() => toggleModal(null)}>
        <ModalHeader toggle={() => toggleModal(null)}>
          {selectedMocktail?.id ? "Mocktail Details" : "Add New Recipe"}
        </ModalHeader>
        <ModalBody>
          {selectedMocktail?.id ? (
            <>
              <h4>{selectedMocktail.name}</h4>
              <p>{selectedMocktail.description}</p>
              <Link
                to={`/mocktails/${selectedMocktail.id}`}
                className="btn btn-outline-primary"
              >
                View Details
              </Link>
            </>
          ) : (
            <Form onSubmit={handleSubmit}>
              <FormGroup>
                <Label for="name">Name</Label>
                <Input type="text" name="name" value={newMocktail.name} onChange={handleChange} required />
              </FormGroup>
              <FormGroup>
                <Label for="description">Description</Label>
                <Input type="text" name="description" value={newMocktail.description} onChange={handleChange} required />
              </FormGroup>
              <FormGroup>
                <Label for="ingredients">Ingredients</Label>
                <Select
                  isMulti
                  options={ingredients.map(i => ({ value: i.id, label: i.name }))}
                  value={selectedIngredients.map(i => ({ value: i.ingredientId, label: i.name }))}
                  onChange={handleIngredientChange}
                />
              </FormGroup>
              {selectedIngredients.map(i => (
                <FormGroup key={i.ingredientId} className="d-flex align-items-center mb-2">
                  <Label className="me-2 mb-0">{i.name}</Label>
                  <Input
                    type="number"
                    value={i.quantity}
                    onChange={e => handleQuantityChange(i.ingredientId, e.target.value)}
                    className="me-2"
                    style={{ width: "80px" }}
                  />
                  <span>{ingredients.find(ing => ing.id === i.ingredientId)?.measurement || ''}</span>
                </FormGroup>
              ))}
              <FormGroup>
                <Label for="instructions">Instructions</Label>
                <Input type="textarea" name="instructions" value={newMocktail.instructions} onChange={handleChange} required />
              </FormGroup>
              <Button type="submit" color="primary">Save Recipe</Button>
            </Form>
          )}
        </ModalBody>
      </Modal>
    </div>
  );
};

import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Select from "react-select";
import {
  getMocktailById,
  updateMocktail,
  deleteMocktail
} from "../../managers/mocktailManager";
import { getAllIngredients } from "../../managers/ingredientManager";
import {
  Form,
  FormGroup,
  Label,
  Input,
  Button,
  Container
} from "reactstrap";

export default function EditMocktail() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [mocktail, setMocktail] = useState(null);
  const [ingredients, setIngredients] = useState([]);
  const [selectedIngredients, setSelectedIngredients] = useState([]);

  useEffect(() => {
    getMocktailById(id).then((data) => {
      setMocktail({
        name: data.name,
        description: data.description,
        instructions: data.instructions
      });
      setSelectedIngredients(
        data.mocktailIngredients.map((mi) => ({
          ingredientId: mi.ingredientId,
          name: mi.ingredient.name,
          quantity: mi.quantity
        }))
      );
    });
    getAllIngredients().then(setIngredients);
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setMocktail((prev) => ({ ...prev, [name]: value }));
  };

  const handleIngredientChange = (selectedOptions) => {
    const updated = selectedOptions.map((option) => {
      const existing = selectedIngredients.find(
        (i) => i.ingredientId === option.value
      );
      return (
        existing || {
          ingredientId: option.value,
          quantity: 1,
          name: option.label
        }
      );
    });
    setSelectedIngredients(updated);
  };

  const handleQuantityChange = (ingredientId, quantity) => {
    setSelectedIngredients((prev) =>
      prev.map((i) =>
        i.ingredientId === ingredientId
          ? { ...i, quantity: parseFloat(quantity) || 0 }
          : i
      )
    );
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updated = {
      ...mocktail,
      id: parseInt(id),
      mocktailIngredients: selectedIngredients.map((i) => ({
        ingredientId: i.ingredientId,
        quantity: i.quantity,
        ingredient: null,
        mocktailId: parseInt(id)
      }))
    };
    await updateMocktail(updated, id);
    navigate(`/mocktails/${id}`);
  };

  const handleDelete = async () => {
    if (window.confirm("Are you sure you want to delete this recipe?")) {
      await deleteMocktail(id);
      navigate("/");
    }
  };

  if (!mocktail) return <p>Loading...</p>;

  return (
    <Container className="mt-5">
      <h2>Edit Recipe</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="name">Name</Label>
          <Input
            type="text"
            name="name"
            value={mocktail.name}
            onChange={handleChange}
            required
          />
        </FormGroup>
        <FormGroup>
          <Label for="description">Description</Label>
          <Input
            type="text"
            name="description"
            value={mocktail.description}
            onChange={handleChange}
            required
          />
        </FormGroup>
        <FormGroup>
          <Label for="ingredients">Ingredients</Label>
          <Select
            isMulti
            options={ingredients.map((i) => ({ value: i.id, label: i.name }))}
            value={selectedIngredients.map((i) => ({
              value: i.ingredientId,
              label: i.name
            }))}
            onChange={handleIngredientChange}
          />
        </FormGroup>
        {selectedIngredients.map((i) => (
          <FormGroup
            key={i.ingredientId}
            className="d-flex align-items-center mb-2"
          >
            <Label className="me-2 mb-0">{i.name}</Label>
            <Input
              type="number"
              value={i.quantity}
              onChange={(e) =>
                handleQuantityChange(i.ingredientId, e.target.value)
              }
              className="me-2"
              style={{ width: "80px" }}
            />
            <span>
              {ingredients.find((ing) => ing.id === i.ingredientId)?.measurement || ""}
            </span>
          </FormGroup>
        ))}
        <FormGroup>
          <Label for="instructions">Instructions</Label>
          <Input
            type="textarea"
            name="instructions"
            value={mocktail.instructions}
            onChange={handleChange}
            required
          />
        </FormGroup>
        <div className="d-flex justify-content-between">
          <Button color="primary">Save Changes</Button>
          <Button color="danger" onClick={handleDelete}>Delete</Button>
        </div>
      </Form>
    </Container>
  );
}

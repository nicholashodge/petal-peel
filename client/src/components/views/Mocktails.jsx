import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { Modal, ModalHeader, ModalBody, Button } from "reactstrap";
import { getAllMocktails } from "../../managers/mocktailManager"; // Backend call

export const Mocktails = () => {
  const [mocktails, setMocktails] = useState([]);
  const [selectedMocktail, setSelectedMocktail] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);

  useEffect(() => {
    getAllMocktails().then(setMocktails);
  }, []);

  const toggleModal = (mocktail = null) => {
    setSelectedMocktail(mocktail);
    setModalOpen(!modalOpen);
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
          {selectedMocktail ? (
            <>
              <h4>{selectedMocktail.name}</h4>
              <p>{selectedMocktail.description}</p>
              <Link
                    to={`/mocktails/${selectedMocktail.id}`}
                    className="btn btn-outline-primary"
                    >
                    View Details
                </Link>
              {/* You can add more details here */}
            </>
          ) : (
            <p>Form to add a new recipe goes here.</p>
          )}
        </ModalBody>
      </Modal>
    </div>
  );
};

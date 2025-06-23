import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { tryGetLoggedInUser } from "../../managers/authManager";
import { updateUserProfile } from "../../managers/userManager";
import "../../styling/account.css";

export default function EditAccount({loggedInUser}) {
  const [user, setUser] = useState(null);
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    address: "",
    email: "",
    userName: "",
    password: ""
  });

  const navigate = useNavigate();

    useEffect(() => {
    tryGetLoggedInUser().then((data) => {
        if (!data) {
        console.error("No logged-in user found");
        return;
        }
        setUser(data);
        setFormData({
        firstName: data.firstName || "",
        lastName: data.lastName || "",
        address: data.address || "",
        email: data.email || "",
        userName: data.userName || "",
        password: ""
        });
    });
    }, []);

  useEffect(() => {
  if (loggedInUser) setUser(loggedInUser);
  }, [loggedInUser]);


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Sending to update:", user.id, formData);
    await updateUserProfile(user.id, formData);
    navigate("/account");
  };

  if (!user) return <p>Loading account info...</p>;

  return (
    <div className="account-container">
      <h2 className="page-title">Edit Account</h2>
      <Form className="account-form" onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="firstName">First Name</Label>
          <Input type="text" name="firstName" value={formData.firstName} onChange={handleChange} required />
        </FormGroup>
        <FormGroup>
          <Label for="lastName">Last Name</Label>
          <Input type="text" name="lastName" value={formData.lastName} onChange={handleChange} required />
        </FormGroup>
        <FormGroup>
          <Label for="userName">Username</Label>
          <Input type="text" name="userName" value={formData.userName} onChange={handleChange} required />
        </FormGroup>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input type="email" name="email" value={formData.email} onChange={handleChange} required />
        </FormGroup>
        <FormGroup>
          <Label for="address">Address</Label>
          <Input type="text" name="address" value={formData.address} onChange={handleChange} />
        </FormGroup>
        <FormGroup>
          <Label for="password">New Password (optional)</Label>
          <Input type="password" name="password" value={formData.password} onChange={handleChange} />
        </FormGroup>
        <div className="form-actions">
          <Button color="primary" type="submit">
            Save Changes
          </Button>
          <Button color="secondary" onClick={() => navigate("/account")}>Cancel</Button>
        </div>
      </Form>
    </div>
  );
}

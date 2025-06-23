import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../styling/account.css";

export default function AccountDetails() {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetch("/api/auth/me")
      .then((res) => {
        if (!res.ok) throw new Error("Failed to load user profile");
        return res.json();
      })
      .then(setUser)
      .catch((err) => console.error(err));
  }, []);

  if (!user) return <p>Loading account info...</p>;

  return (
    <div className="account-container">
      <h2 className="account-title">My Account</h2>

      <div className="account-card">
        <div className="account-info">
          <p><strong>Full Name:</strong> {user.fullName}</p>
          <p><strong>Username:</strong> {user.userName}</p>
          <p><strong>Email:</strong> {user.email}</p>
          <p><strong>Address:</strong> {user.address}</p>
          <p><strong>Roles:</strong> {user.roles?.join(", ")}</p>
        </div>
      </div>

      <button
        className="edit-account-btn"
        onClick={() => navigate("/account/edit")}
      >
        Edit Account
      </button>
    </div>
  );
}

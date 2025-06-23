export const getUserProfile = async () => {
  const res = await fetch("/api/UserProfile/me", {
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error("Failed to fetch user profile");
  }

  return await res.json();
};

export const updateUserProfile = async (id, updatedProfile) => {
  const response = await fetch(`/api/userprofile/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(updatedProfile)
  });

  if (!response.ok) {
    throw new Error("Failed to update user profile");
  }
};

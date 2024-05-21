import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children }) => {
  const isAuthenticated = localStorage.getItem("token") !== null;

  const handleBack = () => {
    alert("Please login");
    return <Navigate to="/" replace />;
  };

  return isAuthenticated ? children : handleBack();
};

export default ProtectedRoute;

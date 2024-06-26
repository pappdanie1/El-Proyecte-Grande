import { useState } from "react";
import { Navigate} from "react-router-dom";
import LoginForm from "../Components/LoginForm";

function LoginPage({setIsAuthenticated}) {
    const [formData, setFormData] = useState({
        email: '',
        password:''
    });

    const [errorMessage, setErrorMessage] = useState('');

    const [loginSuccessful, setLoginSuccessful] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("/api/Auth/Login", {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData);
      }

      const data = await response.json();
      localStorage.setItem("token", data.token);
      localStorage.setItem("name", data.name)
      localStorage.setItem("username", data.userName)
      localStorage.setItem("phone", data.phoneNumber)
      localStorage.setItem("email", data.email)
      localStorage.setItem("loginTime", Date.now());
      setIsAuthenticated(true);

      setLoginSuccessful(true);

      console.log("Login successful");
    } catch (error) {
      setErrorMessage(error.message);
    }
  };

  return (
    <div className="full-height">
      {!loginSuccessful ? (
        <LoginForm
          formData={formData}
          handleSubmit={handleSubmit}
          handleChange={handleChange}
        />
      ) : (
        <Navigate to="/" />
      )}
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
}


export default LoginPage

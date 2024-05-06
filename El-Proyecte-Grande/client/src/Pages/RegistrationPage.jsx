import { useState } from "react"
import RegistrationForm from "../Components/RegistrationForm";

function RegistrationPage() {
    const [formData, setFormData] = useState({
        email: '',
        username: '',
        password: ''
    });

    const [errorMessage, setErrorMessage] = useState('');
    const [registrationSuccess, setRegistrationSuccess] = useState(false);

    const handleChange = (e) => {
        const {name, value} = e.target;
        setFormData((prevFormData) => ({
            ...prevFormData,
            [name]: value
        }))
    }

    const handleSubmit = async(e) => {
        e.preventDefault();

        try {
            const response = await fetch("/api/Auth/Register", {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json',
                },
                body: JSON.stringify(formData),
            })

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData);
            }

            setRegistrationSuccess(true);
            console.log("Registration successful");
        } catch (error) {
            setErrorMessage(error.message);
        }
    }
    return (
        <>
        <RegistrationForm formData={formData} handleChange={handleChange} handleSubmit={handleSubmit}/>
        {errorMessage && <p>{errorMessage}</p>}
        {registrationSuccess && <p>Registration successful!</p>}
        </>
    )
}

export default RegistrationPage

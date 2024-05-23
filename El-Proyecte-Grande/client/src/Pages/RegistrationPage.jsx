import { useState } from "react"
import { useNavigate } from "react-router-dom";
import RegistrationForm from "../Components/RegistrationForm";
import './Pages_css/RegistrationPage.css'

function RegistrationPage() {
    const [formData, setFormData] = useState({
        email: '',
        username: '',
        name: '',
        phoneNumber: '',
        password: ''
    });

    const [errorMessage, setErrorMessage] = useState('');
    const [registrationSuccess, setRegistrationSuccess] = useState(false);
    const navigate = useNavigate();

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
            navigate('/login')

            setRegistrationSuccess(true);
            console.log("Registration successful");
        } catch (error) {
            setErrorMessage(error.message);
        }
    }
    return (
        <div className="full-height">
        <RegistrationForm formData={formData} handleChange={handleChange} handleSubmit={handleSubmit}/>
        {errorMessage && <p>{errorMessage}</p>}
        {registrationSuccess && <p>Registration successful!</p>}
        </div>
    )
}

export default RegistrationPage

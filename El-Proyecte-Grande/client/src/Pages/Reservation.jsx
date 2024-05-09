import Auditorium from "../Components/Auditorium"
import { useEffect, useState } from "react"
import './Pages_css/Reservation.css'

const  Reservation = ({ screening, selectedSeats }) => {
    const [seats, setSeats] = useState([]);
    const [seatIds, setSeatIds] = useState([]);
    const [errorMessage, setErrorMessage] = useState();
    const [successful, setSuccessful] = useState(false);
    
    const findSeat = (selectedSeats) => {
        const seatArray = []

        selectedSeats.forEach(seat => {
            const findSeat = screening.auditorium.seats.find(s => s.id == seat)
                seatArray.push(findSeat)
        });
        return seatArray;
    }

    const findSeatId = (selectedSeats) => {
        const seatArray = []

        selectedSeats.forEach(seat => {
            const findSeat = screening.auditorium.seats.find(s => s.id == seat)
                seatArray.push(findSeat.id)
        });
        return seatArray;
    }

    useEffect(() => {
        setSeats(findSeat(selectedSeats))
        setSeatIds(findSeatId(selectedSeats))
    }, [])

    const requestBody = {
        "id": 0,
        "screening": {
            "id": 0,
            "start": "2024-05-08T15:44:32.951Z",
            "auditorium": {
                "id": 0,
                "name": "string"
            },
            "movie": {
                "id": 0,
                "title": "string",
                "director": "string",
                "cast": "string",
                "description": "string",
                "durationInSec": "string",
                "poster": "string",
                "genres": "string"
            }
        },
        "customer": {
            "id": "string",
            "userName": "string",
            "normalizedUserName": "string",
            "email": "string",
            "normalizedEmail": "string",
            "emailConfirmed": true,
            "passwordHash": "string",
            "securityStamp": "string",
            "concurrencyStamp": "string",
            "phoneNumberConfirmed": true,
            "twoFactorEnabled": true,
            "lockoutEnd": "2024-05-08T15:44:32.951Z",
            "lockoutEnabled": true,
            "accessFailedCount": 0,
            "name": "string",
            "phoneNumber": "string"
        }
    }

    const handleReserve = async () => {
        try {
            const seatParams = seatIds.map(id => `seatIds=${id}`).join('&');
            const url = `/api/Reservation/AddReservation?${seatParams}&screeningId=${screening.id}`;
            const response = await fetch(url, {
                method: 'Post',
                headers: { 'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem('token')}` },
                body: JSON.stringify(requestBody)
            })

            setSuccessful(true)

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error({});
            }

        } catch (error) {
            setErrorMessage(error.message);
            console.error(error.message);
        }
    }


    return (
        <div>
             <table>
                <tbody>
                    <tr>
                        <td>Username:</td>
                        <td>{localStorage.getItem("name")}</td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>{localStorage.getItem("email")}</td>
                    </tr>
                    <tr>
                        <td>Phone:</td>
                        <td>{localStorage.getItem("phone")}</td>
                    </tr>
                    <tr>
                        <td>Movie:</td>
                        <td>{screening.movie.title}</td>
                    </tr>
                    <tr>
                        <td>Screening time:</td>
                        <td>{screening.start.split("T").join(" ")}</td>
                    </tr>
                    <tr>
                        <td>Auditorium:</td>
                        <td>{screening.auditorium.id}</td>
                    </tr>
                    <tr>
                        <td>Selected seats:</td>
                        <td>{seats.map((s) => (              
                            <p key={s.id}>{`Row:${s.row}  Seat:${s.number}`}</p>
                        ))}</td>
                    </tr>
                </tbody>
            </table>
            <button onClick={handleReserve}>Reserve</button>
            {successful && <p>Successful Reservation</p>}
            {errorMessage && <p>{errorMessage}</p>}
        </div>
    )
}

export default Reservation

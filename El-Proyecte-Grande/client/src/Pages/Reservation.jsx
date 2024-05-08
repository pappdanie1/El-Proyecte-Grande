import Auditorium from "../Components/Auditorium"
import { useEffect, useState } from "react"
import './Pages_css/Reservation.css'

const  Reservation = ({ screening, selectedSeats }) => {
    const [seats, setSeats] = useState([]);
    const [errorMessage, setErrorMessage] = useState();
    const [successful, setSuccessful] = useState(false);
    
    const findSeat = (selectedSeats) => {
        const seatArray = []
        console.log(selectedSeats);

        selectedSeats.forEach(seat => {
            const findSeat = screening.auditorium.seats.find(s => s.id == seat)
                seatArray.push(findSeat)
        });
        return seatArray;
    }

    useEffect(() => {
        setSeats(findSeat(selectedSeats))
    }, [])

    const requestBody = {
        "id": 0,
        "seats": seats.map(seat => ({
            "id": 0, // You may need to assign a unique ID if required
            "seat": {
                "id": seat.id, // Assuming seatId corresponds to seat ID
                "row": seat.row,
                "number": seat.number,
                "auditorium": {
                    "id": screening.auditorium.id,
                    "name": screening.auditorium.name,
                    "seatNo": screening.auditorium.seatNo
                }
            },
            "screening": {
                "id": screening.id, // If you need screening ID, you can assign it here
                "start": screening.start,
                "auditorium": {
                    "id": screening.auditorium.id,
                    "name": screening.auditorium.name,
                },
                "movie": {
                    "id": screening.movie.id,
                    "title": screening.movie.title,
                    "director": screening.movie.director,
                    "cast": screening.movie.cast,
                    "description": screening.movie.description,
                    "durationInSec": screening.movie.durationInSec,
                    "poster": screening.movie.poster,
                    "genres": screening.movie.genres
                }
            }
        })),
        "screening": {
            "id": screening.id, // If you need screening ID, you can assign it here
                "start": screening.start,
                "auditorium": {
                    "id": screening.auditorium.id,
                    "name": screening.auditorium.name,
                },
                "movie": {
                    "id": screening.movie.id,
                    "title": screening.movie.title,
                    "director": screening.movie.director,
                    "cast": screening.movie.cast,
                    "description": screening.movie.description,
                    "durationInSec": screening.movie.durationInSec,
                    "poster": screening.movie.poster,
                    "genres": screening.movie.genres
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
            "lockoutEnd": "2024-05-07T16:11:32.587Z",
            "lockoutEnabled": true,
            "accessFailedCount": 0,
            "name": "string",
            "phoneNumber": "string"
        }
    }

    const handleReserve = async () => {
        try {
            const response = await fetch('/api/Reservation/AddReservation', {
                method: "POST",
                headers: { 'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}` },
                body: JSON.stringify(requestBody)
            })

            setSuccessful(true)

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData);
            }

        } catch (error) {
            setErrorMessage(error.message);
        }
    }
    

    console.log(seats);

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

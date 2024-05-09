import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import Seat from "./Seat.jsx";
import "./Component_css/Auditorium.css"
import Loading from "./Loading.jsx";

function Auditorium({ setScreening, screening, seats, setSeats, selectedSeats, setSelectedSeats }) {
  const [loading, setLoading] = useState(true);

  const { id } = useParams();

  useEffect(() => {
    const FetchAuditoriums = async () => {
      const response = await fetch(`/api/Screening/${id}`);
      const jsonData = await response.json();
      setSeats(jsonData.auditorium.seats);
      setScreening(jsonData)
      setLoading(false);
    };

    FetchAuditoriums();
  }, [id]);

  // Function to group seats by row
  const groupSeatsByRow = () => {
    const seatsByRow = {};
    seats.forEach((seat) => {
      const row = seat.row;
      if (!seatsByRow[row]) {
        seatsByRow[row] = [];
      }
      seatsByRow[row].push(seat);
    });
    return seatsByRow;
  };

  const seatsByRow = groupSeatsByRow();

  //Toggling seats
  const handleSeatSelect = (seatId) => {
    setSelectedSeats((prevSelectedSeats) => {
      if (prevSelectedSeats.includes(seatId)) {
        return prevSelectedSeats.filter((id) => id !== seatId);
      } else {
        return [...prevSelectedSeats, seatId];
      }
    });
  };

  if (loading) {
    return <Loading/>
  }

  return (
    <div className="auditorium">
      <h2>SCREEN</h2>
      <h3>{screening.movie.title}</h3>
      <p>Screening time: {screening.start.split("T").join(" ")}</p>
      <div className="seats">
        {Object.keys(seatsByRow).map((row) => (
          <div key={row} className="seat-row">
            <div className="row-number">Row: {row} </div>
            {seatsByRow[row].map((seat) => (
              <Seat
                key={seat.id}
                seat={seat}
                isSelected={selectedSeats.includes(seat.id)}
                onSelect={handleSeatSelect}
              />
            ))}
          </div>
        ))}
      </div>
        <Link to="/reservation">Reserve</Link>
    </div>
  );
}

export default Auditorium;

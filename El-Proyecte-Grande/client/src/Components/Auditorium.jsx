import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Seat from "./Seat.jsx";
import "./Component_css/Auditorium.css"
import Loading from "./Loading.jsx";

function Auditorium() {
  const [selectedSeats, setSelectedSeats] = useState([]);
  const [seats, setSeats] = useState([]);
  const [loading, setLoading] = useState(true);

  const { id } = useParams();

  useEffect(() => {
    const FetchAuditoriums = async () => {
      const response = await fetch(`/api/Screening/${id}`);
      const jsonData = await response.json();
      setSeats(jsonData.auditorium.seats);
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
      <div className="seats">
        {Object.keys(seatsByRow).map((row) => (
          <div key={row} className="seat-row">
            <div className="row-number">Row: {row} </div>
            {seatsByRow[row].map((seat) => (
              <Seat
                key={seat.id}
                number={seat.number}
                isSelected={selectedSeats.includes(seat.id)}
                onSelect={handleSeatSelect}
              />
            ))}
          </div>
        ))}
      </div>
    </div>
  );
}

export default Auditorium;

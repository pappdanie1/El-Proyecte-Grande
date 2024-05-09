import "./Component_css/Seat.css";

function Seat({ seat, isSelected, onSelect, isReserved }) {
  const handleClick = () => {
    onSelect(seat.id);
  };

  const handleNothing = () => {

  }

  return (
    <div
      className={`seat ${isSelected ? "selected" : isReserved ? "reserved": ""}`}
      onClick={isReserved ? handleNothing : handleClick} 
    >
      {seat.number}
    </div>
  );
}

export default Seat;

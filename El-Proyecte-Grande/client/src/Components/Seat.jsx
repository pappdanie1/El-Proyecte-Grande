import "./Component_css/Seat.css";

function Seat({ seat, isSelected, onSelect }) {
  const handleClick = () => {
    onSelect(seat.id);
  };

  return (
    <div
      className={`seat ${isSelected ? "selected" : ""}`}
      onClick={handleClick}
    >
      {seat.number}
    </div>
  );
}

export default Seat;

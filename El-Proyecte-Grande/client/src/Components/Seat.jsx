import "./Component_css/Seat.css";

function Seat({ number, isSelected, onSelect }) {
  const handleClick = () => {
    onSelect();
  };

  return (
    <div
      className={`seat ${isSelected ? "selected" : ""}`}
      onClick={handleClick}
    >
      {number}
    </div>
  );
}

export default Seat;

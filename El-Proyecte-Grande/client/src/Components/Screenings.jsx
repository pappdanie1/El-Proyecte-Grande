import "./Component_css/Screenings.css";
import {Link} from "react-router-dom";

function Screenings({ screenings, movie }) {

  return (
    <div className="screening-times">
      {screenings
      .filter((s) => s.movie.id === movie.id)
      .map((screening, index) => (
        <Link to={`/auditorium/${screening.id}`} className="screening-time" key={index}>{screening.start.split("T")[1].slice(0,5)}</Link>
      ))}
    </div>
  );
}

export default Screenings;

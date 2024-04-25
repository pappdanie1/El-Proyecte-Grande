import "./Component_css/Screenings.css";

function Screenings({ screenings, movie }) {

  console.log(screenings)
  console.log("----------")
  console.log(movie)
  return (
    <div className="screening-times">
      {screenings
      .filter((s) => s.movie.id === movie.id)
      .map((screening, index) => (
        <button className="screening-time" key={index}>{screening.start.split("T")[1].slice(0,5)}</button>
      ))}
    </div>
  );
}

export default Screenings;

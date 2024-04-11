import { useEffect, useState } from "react";
import "./Component_css/Movie.css";
import Schedule from "./Schedule";

const Movie = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("http://localhost:5229/Movie");
      const jsonData = await response.json();
      setData(jsonData);
    };
    fetchData();
  }, []);
  console.log(data);

  return (
    <>
    <h2 className="screenings-title">Screenings</h2>
    <Schedule/>
    <div>
      {data.map((item, index) => (
        <div className="movie-details-container" key={index}>
          <div className="movie-poster">
            <img
              src={`https://image.tmdb.org/t/p/w200${item.poster}`}
              alt={item.title}
            />
          </div>
          <div className="movie-info">
            <h2>{item.title}</h2>
            <p>
              <strong>Genre:</strong> {item.genre}
            </p>
            <p>
              <strong>Runtime:</strong> {item.runtime}
            </p>
            <div className="screening-times">
            <button className="screening-time">18:00</button>
            <button className="screening-time">20:00</button>
            <button className="screening-time">22:00</button>
          </div>
          </div>
        </div>
      ))}
    </div>
    </>
  );
};

export default Movie;

import "./Component_css/MovieInfo.css";
import { useParams } from "react-router-dom";
import Screenings from "./Screenings";
import Schedule from "./Schedule";
import Loading from "./Loading";
import { useState, useEffect } from "react";

function MovieInfo({data, screenings, handleSelectDay, selectedDay, filteredScreenings}) {
  const { movieId } = useParams();
  const [movie, setMovie] = useState(null);

  const daysOfWeek = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const response = await fetch(`/api/Movie/${movieId}`);
        if (!response.ok) {
          throw new Error("Failed to fetch movie");
        }
        const movieData = await response.json();
        setMovie(movieData);
      } catch (error) {
        console.error("Error fetching movie:", error);
      }
    };

    fetchMovie();
  }, [movieId]);

  if (!movie) {
    return <Loading />;
  }

  return (
    <>
      <div className="movie-details-container">
        <div className="movie-details-content">
          <h1 className="movie-title">{movie.title}</h1>
          <p className="movie-runtime">
            <strong>Runtime:</strong> {movie.durationInSec}
          </p>
          <p className="movie-description">{movie.description}</p>
          <div className="movie-info-container">
            <table className="movie-info-table">
              <tbody>
                <tr>
                  <td><strong>Genres:</strong></td>
                  <td>{movie.genres}</td>
                </tr>
                <tr>
                  <td><strong>Director:</strong></td>
                  <td>{movie.director}</td>
                </tr>
                <tr>
                  <td><strong>Cast:</strong></td>
                  <td>{movie.cast}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <div className="movie-poster">
          <img src={movie.poster} alt={movie.title} />
        </div>
      </div>
      <hr className="divider" />
      <div className="movie-details-container">
        <div className="Screenings">
          <h2>Buy your tickets</h2>
          <Schedule data={data} screenings={screenings} handleSelectDay={handleSelectDay} selectedDayFrom={selectedDay} filteredScreenings={filteredScreenings}/>
          <hr className="divider" />
          <h4 className="screenings-today" >Screenings on {daysOfWeek[selectedDay]}:</h4>
          <Screenings screenings={filteredScreenings} movie={movie}/>
        </div>
      </div>
    </>
  );
}

export default MovieInfo;

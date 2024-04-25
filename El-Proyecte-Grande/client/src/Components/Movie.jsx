import "./Component_css/Movie.css";
import Schedule from "./Schedule";
import Loading from "./Loading";
import MovieDropdown from "./MovieDropdown";
import { Link } from "react-router-dom";
import Screenings from "./Screenings";
import { useState} from "react";

const Movie = ({ data, screenings }) => {
  const [selectedMovieId, setSelectedMovieId] = useState("");

  if (!data) {
    return <Loading />;
  }

  const handleSelectMovie = (movieId) => {
    setSelectedMovieId(movieId);
  };

  const selectedMovie = data.find(movie => movie.id === parseInt(selectedMovieId));

  return (
    <>
      <h2 className="screenings-title">Screenings</h2>
      <Schedule data={data} />
      <MovieDropdown movies={data} onSelectMovie={handleSelectMovie} />
      {selectedMovieId ? (
        <div className="movie-details-container">
          <div className="movie-poster">
            <Link to={`/movieDetails/${selectedMovie.id}`}>
              <img src={selectedMovie.poster} alt={selectedMovie.title} />
            </Link>
          </div>
          <div className="movie-info">
            <Link to={`/movieDetails/${selectedMovie.id}`}>
              <h2>{selectedMovie.title}</h2>
            </Link>
            <p>
              <strong>Genre:</strong> {selectedMovie.genres}
            </p>
            <p>
              <strong>Runtime: </strong> {selectedMovie.durationInSec}
            </p>
            <Screenings screenings={screenings} movie={selectedMovie} />
          </div>
        </div>
      ) : (
        data.map((movie, index) => (
          <div className="movie-details-container" key={index}>
            <div className="movie-poster">
              <Link to={`/movieDetails/${movie.id}`}>
                <img src={movie.poster} alt={movie.title} />
              </Link>
            </div>
            <div className="movie-info">
              <Link to={`/movieDetails/${movie.id}`}>
                <h2>{movie.title}</h2>
              </Link>
              <p>
                <strong>Genre:</strong> {movie.genres}
              </p>
              <p>
                <strong>Runtime: </strong> {movie.durationInSec}
              </p>
              <Screenings screenings={screenings} movie={movie}/>
            </div>
          </div>
        ))
      )}
    </>
  );
};

export default Movie;

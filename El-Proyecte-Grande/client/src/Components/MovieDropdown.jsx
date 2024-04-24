import { useState } from 'react';
import "./Component_css/MovieDropdown.css";

const MovieDropdown = ({ movies, onSelectMovie }) => {
  const [selectedMovieId, setSelectedMovieId] = useState('');

  const handleChange = (event) => {
    const selectedId = event.target.value;
    setSelectedMovieId(selectedId);
    onSelectMovie(selectedId);
  };

  return (
    <select value={selectedMovieId} onChange={handleChange}>
      <option value="">Select a movie</option>
      {movies.map(movie => (
        <option key={movie.id} value={movie.id}>{movie.title}</option>
      ))}
    </select>
  );
};

export default MovieDropdown;

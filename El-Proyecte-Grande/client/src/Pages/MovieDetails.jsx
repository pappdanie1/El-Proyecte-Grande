import Slider from "../Components/Slider";
import MovieInfo from "../Components/MovieInfo";

const MovieDetails = ({ data, screenings, handleSelectDay, selectedDay, filteredScreenings }) => {
  return (
    <>
      <Slider />
      <MovieInfo data={data} screenings={screenings} handleSelectDay={handleSelectDay} selectedDay={selectedDay} filteredScreenings={filteredScreenings}/>
    </>
  );
};

export default MovieDetails;

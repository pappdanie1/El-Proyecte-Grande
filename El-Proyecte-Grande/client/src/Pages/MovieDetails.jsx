import Slider from "../Components/Slider";
import MovieInfo from "../Components/MovieInfo";

const MovieDetails = ({ data, screenings }) => {
  return (
    <>
      <Slider />
      <MovieInfo data={data} screenings={screenings} />
    </>
  );
};

export default MovieDetails;

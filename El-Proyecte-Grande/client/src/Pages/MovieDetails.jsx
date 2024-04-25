import Header from "../Components/Header";
import Slider from "../Components/Slider";
import Footer from "../Components/Footer";
import MovieInfo from "../Components/MovieInfo";


const MovieDetails = ({data, screenings}) => {
 
  return (
    <>
    <Header/>
    <Slider/>
    <MovieInfo data={data} screenings={screenings}/>
    <Footer/>
    </>
  );
};

export default MovieDetails;
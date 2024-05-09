
import Movie from "../Components/Movie";
import Slider from "../Components/Slider";

const Home = ({data, screenings}) => {
  return (
    <>
      <Slider />
      <Movie data={data} screenings={screenings}/>
    </>
  );
};

export default Home;

import Header from "../Components/Header";
import Movie from "../Components/Movie";
import Slider from "../Components/Slider";
import Footer from "../Components/Footer";

const Home = ({data, screenings}) => {
  return (
    <>
      <Header />
      <Slider />
      <Movie data={data} screenings={screenings}/>
      <Footer/>
    </>
  );
};

export default Home;

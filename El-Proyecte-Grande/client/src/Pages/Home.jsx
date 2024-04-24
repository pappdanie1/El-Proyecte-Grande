import Header from "../Components/Header";
import Movie from "../Components/Movie";
import Slider from "../Components/Slider";
import Footer from "../Components/Footer";

const Home = ({data}) => {
  return (
    <>
      <Header />
      <Slider />
      <Movie data={data}/>
      <Footer/>
    </>
  );
};

export default Home;

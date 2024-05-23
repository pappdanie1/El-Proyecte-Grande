
import Movie from "../Components/Movie";
import Slider from "../Components/Slider";

const Home = ({data, screenings, handleSelectDay, selectedDay, filteredScreenings}) => {
  return (
    <>
      <Slider />
      <Movie data={data} screenings={screenings} handleSelectDay={handleSelectDay} selectedDay={selectedDay} filteredScreenings={filteredScreenings}/>
    </>
  );
};

export default Home;

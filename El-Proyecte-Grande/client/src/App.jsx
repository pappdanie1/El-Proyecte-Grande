import "./App.css";
import { useState, useEffect } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home";
import MovieDetails from "./Pages/MovieDetails";
import Reservation from "./Pages/Reservation";
import PageNotFound from "./Pages/PageNotFound/PageNotFound";

function App() {

  const [data, setData] = useState([]);
  const [screenings, setScreenings] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("http://localhost:5229/Movie");
      const jsonData = await response.json();
      setData(jsonData);

      const screeningResponse = await fetch("http://localhost:5229/Screening");
      const screeningJsonData = await screeningResponse.json();
      setScreenings(screeningJsonData);
    };

    fetchData();
  }, []);
  console.log(data);

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home data={data} screenings={screenings}/>} />
        <Route path="movieDetails/:movieId" element={<MovieDetails data={data} screenings={screenings}/>} />
        <Route path="reservation" element={<Reservation />} />
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

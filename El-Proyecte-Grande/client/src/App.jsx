import "./App.css";
import { useState, useEffect } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home";
import MovieDetails from "./Pages/MovieDetails";
import Reservation from "./Pages/Reservation";
import PageNotFound from "./Pages/PageNotFound/PageNotFound";
import RegistrationPage from "./Pages/RegistrationPage";
import LoginPage from "./Pages/LoginPage";
import Header from "./Components/Header";
import Footer from "./Components/Footer";

function App() {

  const [data, setData] = useState([]);
  const [screenings, setScreenings] = useState([]);
  const isAuthenticated = localStorage.getItem('token') !== null;

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("/api/Movie");
      console.log(response)
      const jsonData = await response.json();
      setData(jsonData);

      const screeningResponse = await fetch("/api/Screening");
      const screeningJsonData = await screeningResponse.json();
      setScreenings(screeningJsonData);
    };

    fetchData();
  }, [isAuthenticated]);
  console.log(data);

  return (
    <BrowserRouter>
    <Header isAuthenticated={isAuthenticated}/>
      <Routes>
        <Route path="/" element={<Home data={data} screenings={screenings}/>} />
        <Route path="movieDetails/:movieId" element={<MovieDetails data={data} screenings={screenings}/>} />
        <Route path="reservation" element={<Reservation />} />
        <Route path="/register" element={<RegistrationPage/>}/>
        <Route path="/login" element={<LoginPage/>} />
        <Route path="*" element={<PageNotFound />} />
      </Routes>
      <Footer/>
    </BrowserRouter>
  );
}

export default App;

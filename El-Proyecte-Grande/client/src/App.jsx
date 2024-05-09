import "./App.css";
import { useState, useEffect } from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Home from "./Pages/Home";
import MovieDetails from "./Pages/MovieDetails";
import Reservation from "./Pages/Reservation";
import PageNotFound from "./Pages/PageNotFound/PageNotFound";
import RegistrationPage from "./Pages/RegistrationPage";
import LoginPage from "./Pages/LoginPage";
import Header from "./Components/Header";
import Footer from "./Components/Footer";
import Auditorium from "./Components/Auditorium";
import ProtectedRoute from "./Components/ProtectedRoute";
import Redirect from "./Pages/Redirect/Redirect";

function App() {
  const [data, setData] = useState([]);
  const [screenings, setScreenings] = useState([]);
  const [isAuthenticated, setIsAuthenticated] = useState(
    localStorage.getItem("token") !== null
  );
  const [selectedSeats, setSelectedSeats] = useState([]);
  const [screening, setScreening] = useState();
  const [seats, setSeats] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("/api/Movie");
      const jsonData = await response.json();
      setData(jsonData);

      const screeningResponse = await fetch("/api/Screening");
      const screeningJsonData = await screeningResponse.json();
      setScreenings(screeningJsonData);
    };

    fetchData();
  }, []);

  const redirectToHomeIfLoggedIn = () => {
    return isAuthenticated ? <Navigate to="/" /> : null;
  };

  return (
    <BrowserRouter>
      <Header
        isAuthenticated={isAuthenticated}
        setIsAuthenticated={setIsAuthenticated}
      />
      <Routes>
        <Route
          path="/"
          element={<Home data={data} screenings={screenings} />}
        />
        <Route
          path="movieDetails/:movieId"
          element={<MovieDetails data={data} screenings={screenings} />}
        />
        <Route
          path="/register"
          element={redirectToHomeIfLoggedIn() || <RegistrationPage />}
        />
        <Route
          path="/login"
          element={
            redirectToHomeIfLoggedIn() || (
              <LoginPage setIsAuthenticated={setIsAuthenticated} />
            )
          }
        />
        <Route path="/auditorium/:id" element={<ProtectedRoute><Auditorium setScreening={setScreening} screening={screening} setSeats={setSeats} seats={seats} selectedSeats={selectedSeats} setSelectedSeats={setSelectedSeats}/></ProtectedRoute>}/>
        <Route path="*" element={<PageNotFound />} />
        <Route path="/reservation" element={<Reservation screening={screening} selectedSeats={selectedSeats}/>}/>
        <Route path="/redirect" element={<Redirect />} /> 
      </Routes>
      <Footer />
    </BrowserRouter>
  );
}

export default App;

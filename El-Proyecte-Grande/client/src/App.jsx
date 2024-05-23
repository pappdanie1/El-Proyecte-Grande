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
import Profile from "./Pages/Profile/Profile";
import About from "./Pages/About/About";
import Pricing from "./Pages/Pricing/Pricing";
import Offers from "./Pages/Offers/Offers";

function App() {
  const [data, setData] = useState([]);
  const [screenings, setScreenings] = useState([]);
  const [isAuthenticated, setIsAuthenticated] = useState(
    localStorage.getItem("token") !== null
  );
  const [selectedSeats, setSelectedSeats] = useState([]);
  const [screening, setScreening] = useState();
  const [seats, setSeats] = useState([]);

  const [selectedDay, setSelectedDay] = useState(new Date().getDay() + 1);

  const handleSelectDay = (dayIndex) => {
    setSelectedDay(dayIndex);
  };

  const filterScreeningsByDay = (screenings, dayIndex) => {
    const dayNames = [
      "Sunday",
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday",
    ];
    const dayName = dayNames[dayIndex];
    return screenings.filter((screening) => {
      const screeningDate = new Date(screening.start);
      return (
        screeningDate.toLocaleDateString("en-US", { weekday: "long" }) ===
        dayName
      );
    });
  };

  const filteredScreenings = filterScreeningsByDay(screenings, selectedDay);
  //console.log(filteredScreenings);

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

  const logoutUser = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("loginTime");
    localStorage.removeItem("name");
    localStorage.removeItem("phone");
    localStorage.removeItem("username");
    setIsAuthenticated(false);
  };

  const redirectToHomeIfLoggedIn = () => {
    return isAuthenticated ? <Navigate to="/" /> : null;
  };

  return (
    <BrowserRouter>
      <Header
        isAuthenticated={isAuthenticated}
        setIsAuthenticated={setIsAuthenticated}
        logoutUser={logoutUser}
      />
      <Routes>
        <Route
          path="/"
          element={
            <Home
              data={data}
              screenings={screenings}
              handleSelectDay={handleSelectDay}
              selectedDay={selectedDay}
              filteredScreenings={filteredScreenings}
            />
          }
        />
        <Route path="/about" element={<About/>}/>
        <Route path="/pricing" element={<Pricing/>}/>
        <Route path="/offers" element={<Offers/>}/>
        <Route
          path="movieDetails/:movieId"
          element={
            <MovieDetails
              data={data}
              screenings={screenings}
              handleSelectDay={handleSelectDay}
              selectedDay={selectedDay}
              filteredScreenings={filteredScreenings}
            />
          }
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
        <Route
          path="/auditorium/:id"
          element={
            <ProtectedRoute>
              <Auditorium
                setScreening={setScreening}
                screening={screening}
                setSeats={setSeats}
                seats={seats}
                selectedSeats={selectedSeats}
                setSelectedSeats={setSelectedSeats}
              />
            </ProtectedRoute>
          }
        />
        <Route path="*" element={<PageNotFound />} />
        <Route
          path="/reservation"
          element={
            <Reservation screening={screening} selectedSeats={selectedSeats} />
          }
        />
        <Route path="/redirect" element={<Redirect />} />
        <Route
          path="/profile/:username"
          element={
            <ProtectedRoute>
              <Profile />
            </ProtectedRoute>
          }
        />
      </Routes>
      <Footer />
    </BrowserRouter>
  );
}

export default App;

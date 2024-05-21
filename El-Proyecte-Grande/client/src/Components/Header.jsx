import "./Component_css/Header.css";
import { Link } from "react-router-dom";
import { useEffect } from "react";

const Header = ({ isAuthenticated, setIsAuthenticated, logoutUser }) => {

  const handleClick = (e) => {
    e.preventDefault();
    localStorage.removeItem('token');
    localStorage.removeItem('name');
    localStorage.removeItem('phone');
    localStorage.removeItem('username');
    localStorage.removeItem("loginTime");
    setIsAuthenticated(false);
  }

    // Check if token is expired
    useEffect(() => {
      const loginTime = localStorage.getItem("loginTime");
      console.log(loginTime);
      if (loginTime) {
        // elapsed Time since login in milliseconds
        const elapsedTime = Date.now() - parseInt(loginTime, 10);
        console.log(elapsedTime);
        if (elapsedTime > 1 * 60 * 1000) {
          logoutUser();
        } else {
          // Set a timeout for the remaining time
          setTimeout(logoutUser, 120 * 60 * 1000 - elapsedTime);
        }
      }
    }, [isAuthenticated, logoutUser]);

  return (
    <nav className="navbar">
      <div className="left">
        <Link to="/">
          <div className="cinema-name">Cinema</div>
        </Link>
      </div>
      <div className="center">
        <ul className="nav-links">
          <li>
            <a href="#">Now Playing</a>
          </li>
          <li>
            <a href="#">Offers</a>
          </li>
          <li>
            <a href="#">Pricing</a>
          </li>
          <li>
            <a href="#">About</a>
          </li>
        </ul>
      </div>
      <div className="right">
        <div className="search-container">
          <form action="/search" method="GET">
            <input type="text" name="q" placeholder="Search..." />
            <button type="submit">
              <i className="fas fa-search"></i>
            </button>
          </form>
        </div>
        {isAuthenticated ? (
          <>
            <Link className="btn" to={`/profile/${localStorage.getItem("username")}`} >Profile</Link>
            <button onClick={handleClick} className="btn">Logout</button>
          </>
        ) : (
          <>
            <Link to="/register" className="btn">
              Register
            </Link>
            <Link to="/login" className="btn">
              Login
            </Link>
          </>
        )}
      </div>
    </nav>
  );
};

export default Header;

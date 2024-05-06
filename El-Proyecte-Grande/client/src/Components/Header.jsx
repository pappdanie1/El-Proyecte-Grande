import "./Component_css/Header.css";
import { Link } from "react-router-dom";

const Header = ({ isAuthenticated }) => {

  const handleClick = (e) => {
    e.preventDefault();
    localStorage.removeItem('token');
  }

  return (
    <nav className="navbar">
      <div className="left">
        <div className="cinema-name">Cinema</div>
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
        {
          isAuthenticated ? (
            <button onClick={handleClick} className="btn">
              Logout
            </button>
          ) : (
            <>
              <Link to="/register" className="btn">
                Register
              </Link>
              <Link to="/login" className="btn">
                Login
              </Link>
            </>
          )
        }
      </div>
    </nav>
  );
};

export default Header;

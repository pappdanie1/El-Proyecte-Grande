import "./Component_css/Header.css";

const Header = () => {
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
        <button className="btn">Register</button>
        <button className="btn">Login</button>
      </div>
    </nav>
  );
};

export default Header;

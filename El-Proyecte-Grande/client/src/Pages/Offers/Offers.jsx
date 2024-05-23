import Slider from "../../Components/Slider";
import "./Offers.css";

const Offers = () => {
  return (
    <>
      <Slider />
      <h1>Special Offers</h1>
      <div className="offers-container">
        <div className="offer-card">
          <img src="/images/offer1.jpg" alt="Offer 1" />
          <h2>Weekend Movie Marathon</h2>
          <p>Watch unlimited movies all weekend for just $25!</p>
        </div>
        <div className="offer-card">
          <img src="/images/offer2.jpg" alt="Offer 2" />
          <h2>Family Movie Night</h2>
          <p>Family ticket (4 persons) + popcorn & drinks for $40!</p>
        </div>
        <div className="offer-card">
          <img src="/images/offer3.jpg" alt="Offer 3" />
          <h2>Student Discount</h2>
          <p>Show your student ID and get 20% off on all tickets!</p>
        </div>
      </div>
    </>
  );
};

export default Offers;

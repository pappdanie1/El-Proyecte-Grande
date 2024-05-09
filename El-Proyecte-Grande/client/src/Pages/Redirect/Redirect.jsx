import './Redirect.css'
import { useNavigate } from 'react-router-dom';

const Redirect = () => {
    const navigate = useNavigate();

    setTimeout(() => {
        navigate('/')
      }, 3000);


  return (
      <div className="full-height">
          <div className="redirect-container">
              <h1 className="heading">Thank you!</h1>
              <h2 className="sub-heading">Reservation successful</h2>
              <p>Now redirecting to the main page!</p>
              <div class="loader">
                  <div class="circle">
                      <div class="dot"></div>
                      <div class="outline"></div>
                  </div>
                  <div class="circle">
                      <div class="dot"></div>
                      <div class="outline"></div>
                  </div>
                  <div class="circle">
                      <div class="dot"></div>
                      <div class="outline"></div>
                  </div>
                  <div class="circle">
                      <div class="dot"></div>
                      <div class="outline"></div>
                  </div>
              </div>
          </div>
      </div>
  );
};

export default Redirect;
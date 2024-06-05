import "./About.css";
import Slider from "../../Components/Slider";

const AboutPage = () => {
  return (
    <>
      <Slider />
      <div className="about-container">
        <h1>About Our Web Application</h1>
        <p>
          Our web application was developed as a teamproject in the Advanced -
          ASP .NET module of our studies at Codecool Hungary. It was created
          over the course of 5 sprints, each focusing on different aspects of
          development.
        </p>
        <h2>Backend</h2>
        <p>
          We utilized Entity Framework and Identity frameworks for the backend,
          providing seamless data management and user authentication. Our
          database runs on PostgreSQL.
        </p>
        <h2>Frontend</h2>
        <p>
          The frontend of our application was built using React, a popular
          JavaScript library for building user interfaces. React allows for
          efficient rendering and state management, providing a smooth user
          experience.
        </p>
        <h2>Contact us</h2>
          <div className="contact-link">
            <a href="https://www.linkedin.com/in/andreanichter/">Andrea Nichter</a>
            <a href="https://www.linkedin.com/in/pappdanie1/">Dániel Papp</a>
            <a href="https://www.linkedin.com/in/csaba-gy00/">Csaba Győri</a>
          </div>
      </div>
    </>
  );
};

export default AboutPage;

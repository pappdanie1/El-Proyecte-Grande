import { useState, useEffect } from "react";
import "./Component_css/Slider.css"

function Slider() {
  const [currentSlide, setCurrentSlide] = useState(0);
  const slides = [
    "/images/img1.jpg",
    "/images/img2.jpg",
    "/images/img3.jpg",
    "/images/img4.jpg",
    "/images/img5.jpg",
    "/images/img6.jpg",
  ];

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentSlide(
        currentSlide === slides.length - 1 ? 0 : currentSlide + 1
      );
    }, 4000); 

    return () => clearInterval(interval);
  }, [currentSlide, slides.length]);

  const goToPrevSlide = () => {
    setCurrentSlide(currentSlide === 0 ? slides.length - 1 : currentSlide - 1);
  };

  const goToNextSlide = () => {
    setCurrentSlide(currentSlide === slides.length - 1 ? 0 : currentSlide + 1);
  };

  return (
    <div className="slider-container">
      <img
        src={slides[currentSlide]}
        alt={`Slide ${currentSlide + 1}`}
        className="slider-image"
      />
      <button className="slider-arrow left-arrow" onClick={goToPrevSlide}>
        {"<"}
      </button>
      <button className="slider-arrow right-arrow" onClick={goToNextSlide}>
        {">"}
      </button>
    </div>
  );
}

export default Slider;

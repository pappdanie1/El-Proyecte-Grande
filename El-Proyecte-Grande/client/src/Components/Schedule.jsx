import "./Component_css/Schedule.css";
import { useState } from "react";

const Schedule = (props) => {  
  const currentDay = new Date().getDay() + 1; 
  const daysOfWeek = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];

  function addDays(date, days) {
    const newDate = new Date(date);
    newDate.setDate(date.getDate() + days);
    return newDate;
  }

  const newDate = addDays(new Date(), props.selectedDayFrom - 2).toDateString();


  const reorderedDaysOfWeek = daysOfWeek.slice(currentDay).concat(daysOfWeek.slice(0, currentDay));

  const [selectedDay, setSelectedDay] = useState(currentDay);
  let dates = new Set();

  const handleDayClick = (index) => {
    const actualIndex = (currentDay + index) % 7;
    setSelectedDay(actualIndex);
    props.handleSelectDay(actualIndex); 
  };

  for (const screening of props.screenings) {
    dates.add(new Date(screening.start.split("T")[0]).getDay())
  }

  return (
    <div className="schedule-container">
      <div className="day-info-container">
        <div className="day-selector">
          {reorderedDaysOfWeek.map((day, index) => (
             <button
             key={day}
             className={selectedDay === (currentDay + index) % 7 ? "active" : ""}
             onClick={() => handleDayClick(index)}
           >
             {day}
           </button>
          ))}
        </div>
      </div>
      <div className="date-info-container">
        <p className="date-label">Date:</p>
        <p className="today-date">{newDate}</p>
      </div>
    </div>
  );
};

export default Schedule;

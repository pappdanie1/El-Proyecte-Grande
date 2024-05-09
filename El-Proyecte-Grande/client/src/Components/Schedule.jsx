import "./Component_css/Schedule.css";

const Schedule = () => {
  

  const daysOfWeek = [
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
    "Sunday",
  ];

  return (
    <div className="schedule-container">
      <div className="day-info-container">
        <div className="day-selector">
          {daysOfWeek.map((day) => (
            <button key={day}>{day}</button>
          ))}
        </div>
      </div>
      <div className="date-info-container">
        <p className="date-label">Today's Date:</p>
        <p className="today-date">{new Date().toLocaleDateString()}</p>
      </div>
    </div>
  );
};

export default Schedule;

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
        <div className="today-info">
          <p>{new Date().toLocaleDateString('en-US', { weekday: 'long' })}</p>
          <p>{new Date().toLocaleDateString()}</p>
        </div>
      </div>
    </div>
  );
};

export default Schedule;

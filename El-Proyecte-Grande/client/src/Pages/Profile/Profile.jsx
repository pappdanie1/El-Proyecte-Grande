import './Profile.css'
import { useEffect, useState } from 'react'
import { resolvePath, useParams} from 'react-router-dom'

const Profile = () => {
  const { username } = useParams()
  const [ reservation, setReservation] = useState();

  useEffect(() => {
    const fetchReservation = async () => {
      const response = await fetch(
        `/api/Reservation/AllByUser?username=${username}`,
        {
          headers: {
            "Content-type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );

      const result = await response.json();

      setReservation(result);
    };

    fetchReservation();
  }, []);

  console.log(reservation)

  return (
    <div className="full-height">
			<h2>Reservation Tickets</h2>
      {reservation && reservation.map((ticket) => (
					<div key={ticket.id} className="profile-ticket">
						<div className="profile-left">
							<div className="profile-image">
								<div className="profile-movie-image">
									<img className="poster-image" src={ticket.screening.movie.poster} alt="poster" />
								</div>
							</div>
							<div className="profile-ticket-info">
								<p className="profile-date">
									<span className="profile-june-29">{ticket.screening.start.split("T")[0]}</span>
								</p>
								<div className="profile-show-name">
									<h1>{ticket.screening.movie.title}</h1>
								</div>
								<div className="profile-time">
									<p>
										Starting time: {ticket.screening.start.split("T")[1].slice(0, 5)}
									</p>
									<p>
										Auditorium: {ticket.screening.auditorium.id}
									</p>
								</div>
								<p className="profile-location">
									<span>ASP Cinema</span>
									<span>|</span>
									<span>Silicon Valley</span>
								</p>
							</div>
						</div>
						<div className="profile-right">
							<div className="profile-right-info-container">
								<div className="profile-show-name"></div>
								{ticket.reservedSeats.map((item) => (
									<div key={item.id}className="profile-time">
									<p>
										Row: {item.seat.row}<span>|</span> Seat: {item.seat.number}
									</p>
								</div>
								))}
								<div className="profile-barcode">
									<img
										src="https://external-preview.redd.it/cg8k976AV52mDvDb5jDVJABPrSZ3tpi1aXhPjgcDTbw.png?auto=webp&s=1c205ba303c1fa0370b813ea83b9e1bddb7215eb"
										alt="QR code"
									/>
								</div>
							</div>
						</div>
					</div>
      ))}
    </div>
  );
}

export default Profile
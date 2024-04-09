import {useEffect, useState} from 'react'
import './App.css'

function App() {
    const [data, setData] = useState([])

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch('http://localhost:5229/WeatherForecast')
            const jsonData = await response.json()
            setData(jsonData)
        }
        fetchData()
    }, [])
    console.log(data)

    return (<>
            <div>
                {data.map((item, index) => (
                    <div key={index}>
                        <h2>{item.date}</h2>
                    </div>
                ))}
            </div>
        </>
    )
}

export default App